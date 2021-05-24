using System;
using System.Threading.Tasks;
using Hahn.ApplicatonProcess.February2021.Data.Entities;

namespace Hahn.ApplicatonProcess.February2021.Data.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Asset> Asset { get; }
        IRepository<Department> Department { get; }
        int Save();

        Task<int> SaveAsync();
    }
}