using System;
using System.Threading.Tasks;
using Hahn.ApplicatonProcess.February2021.Data.Context;
using Hahn.ApplicatonProcess.February2021.Data.Entities;
using Hahn.ApplicatonProcess.February2021.Data.Interfaces;

namespace Hahn.ApplicatonProcess.February2021.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(HahnDbContext db)
        {
            _db = db;
        }

        private readonly HahnDbContext _db;
        private IRepository<Asset> _assetRepository;
        private IRepository<Department> _departmentRepository;

        public IRepository<Department> Department
        {
            get
            {
                _departmentRepository ??= new Repository<Department>(_db);

                return _departmentRepository;
            }
        }

        public IRepository<Asset> Asset
        {
            get
            {
                _assetRepository ??= new Repository<Asset>(_db);

                return _assetRepository;
            }
        }

        public int Save()
        {
            return _db.SaveChanges();
        }

        public async Task<int> SaveAsync()
        {
            return await _db.SaveChangesAsync();
        }

        private bool _disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
            }

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}