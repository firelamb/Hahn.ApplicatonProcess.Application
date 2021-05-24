using System;
using System.Linq;
using Hahn.ApplicatonProcess.February2021.Data.Context;
using Hahn.ApplicatonProcess.February2021.Data.Entities;
using Hahn.ApplicatonProcess.February2021.Data.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Hahn.ApplicatonProcess.February2021.Web
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new HahnDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<HahnDbContext>>()
            );

            // Look for any department.
            if (context.Department.Any())
            {
                return; // Data was already seeded
            }

            context.Department.AddRange(
                new Department {ID = DepartmentEnum.HQ, Name = "HQ"},
                new Department {ID = DepartmentEnum.MantenanceStation, Name = "Mantenance Station"},
                new Department {ID = DepartmentEnum.Store1, Name = "Store 1"},
                new Department {ID = DepartmentEnum.Store2, Name = "Store 2"},
                new Department {ID = DepartmentEnum.Store3, Name = "Store 3"}
            );

            context.SaveChanges();
        }
    }
}