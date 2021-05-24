using System;
using System.IO;
using System.Net;
using System.Reflection;
using Autofac;
using AutoMapper;
using FluentValidation.AspNetCore;
using Hahn.ApplicatonProcess.February2021.Data.Context;
using Hahn.ApplicatonProcess.February2021.Domain.Validators;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Formatting.Compact;
using Swashbuckle.AspNetCore.SwaggerUI;
using VueCliMiddleware;

//using VueCliMiddleware;

namespace Hahn.ApplicatonProcess.February2021.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        // ReSharper disable once UnassignedGetOnlyAutoProperty
        public Serilog.ILogger Logger { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .ReadFrom.Configuration(Configuration)
                .WriteTo.Console(new RenderedCompactJsonFormatter())
                .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<HahnDbContext>(b =>
                b.UseInMemoryDatabase("HahnDb")
            );

            services.AddAutoMapper(typeof(Startup));

            var mapperConfig = new MapperConfiguration(mc => mc.AddProfile(new Domain.AutoMapper()));
            services.AddSingleton(mapperConfig.CreateMapper());

            services.AddProblemDetails(options =>
            {
                // Control when an exception is included
                options.IncludeExceptionDetails = (ctx, ex) => true;

                options.Map<Exception>(exception =>
                    {
                        Exception realError = exception;

                        while (realError.InnerException != null) realError = realError.InnerException;

                        return new ProblemDetails
                        {
                            Status = (int) HttpStatusCode.BadRequest,
                            Detail = realError.Message
                        };
                    }
                );
            });

            services.AddSpaStaticFiles(configuration => { configuration.RootPath = "ClientApp/dist"; });

            services.AddControllers()
                .AddFluentValidation(s =>
                    {
                        s.RegisterValidatorsFromAssemblyContaining<AssetValidator>();
                        s.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                    }
                );

            // Register the Swagger generator
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Asset API",
                    Description = "A simple example ASP.NET Core Web API"
                  
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseSwagger(c => { c.SerializeAsV2 = true; });
            // Enable middleware,
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                //c.DocExpansion(DocExpansion.None);
                c.DefaultModelExpandDepth(2);
                c.DefaultModelRendering(ModelRendering.Model);
                //c.DefaultModelsExpandDepth(-1);
                c.DisplayOperationId();
                c.DisplayRequestDuration();
                c.EnableDeepLinking();
                c.EnableFilter();
                c.ShowExtensions();
                c.EnableValidator();
            });

            if (!env.IsDevelopment())
            {
                app.UseHsts();
                app.UseSpaStaticFiles();
            }

            loggerFactory.AddSerilog(Logger);

            app.UseProblemDetails();
            app.UseHttpsRedirection();
            app.UseSerilogRequestLogging();

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(env.ContentRootPath, "assets")),
                RequestPath = "/assets"
            });

            app.UseRouting();
            app.UseEndpoints(endpoints => endpoints.MapControllers());

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseVueCli("start", forceKill: true);
                }
            });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            // Add any Autofac modules or registrations.
            // This is called AFTER ConfigureServices so things you
            // register here OVERRIDE things registered in ConfigureServices.
            //
            // You must have the call to `UseServiceProviderFactory(new AutofacServiceProviderFactory())`
            // when building the host or this won't be called.
            builder.RegisterModule(new AutofacModule());
        }
    }
}