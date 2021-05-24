using Hahn.ApplicatonProcess.February2021.Data.Entities;
using Hahn.ApplicatonProcess.February2021.Data.Enums;
using Microsoft.EntityFrameworkCore;

namespace Hahn.ApplicatonProcess.February2021.Data.Context
{
    public class HahnDbContext : DbContext
    {
        public DbSet<Asset> Asset { get; set; }
        public DbSet<Department> Department { get; set; }

        public HahnDbContext(DbContextOptions<HahnDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Asset>(entity =>
            {
                entity.HasKey(k => k.ID);
                entity.Property(k => k.ID).ValueGeneratedOnAdd();
                entity.Property(ug => ug.AssetName).IsRequired().HasMaxLength(50);
                entity.Property(ug => ug.Department).IsRequired();
                entity.Property(ug => ug.CountryOfDepartment).IsRequired().HasMaxLength(50);
                entity.Property(ug => ug.EMailAdressOfDepartment).IsRequired().HasMaxLength(50);
                entity.Property(ug => ug.PurchaseDate).IsRequired();
                entity.Property(ug => ug.Broken).IsRequired();

                entity
                    .HasOne(p => p.DepartmentEntity)
                    .WithMany(b => b.Assets)
                    .HasForeignKey(p => p.Department);
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasKey(k => k.ID);
                entity.Property(ug => ug.Name).IsRequired().HasMaxLength(50);

                entity.HasData(
                    new Department {ID = DepartmentEnum.HQ, Name = "HQ"},
                    new Department {ID = DepartmentEnum.MantenanceStation, Name = "Mantenance Station"},
                    new Department {ID = DepartmentEnum.Store1, Name = "Store 1"},
                    new Department {ID = DepartmentEnum.Store2, Name = "Store 2"},
                    new Department {ID = DepartmentEnum.Store3, Name = "Store 3"}
                );
            });
        }
    }
}