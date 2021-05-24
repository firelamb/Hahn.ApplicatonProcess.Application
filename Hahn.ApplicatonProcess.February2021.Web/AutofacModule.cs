using Autofac;
using Hahn.ApplicatonProcess.February2021.Data.Interfaces;
using Hahn.ApplicatonProcess.February2021.Domain.Interfaces;

namespace Hahn.ApplicatonProcess.February2021.Web
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IAssetService).Assembly, typeof(IHttpDataAccessService).Assembly)
                .Where(t => t.Name.EndsWith("Service") || t.Name.Equals("UnitOfWork"))
                .AsImplementedInterfaces();
        }
    }
}