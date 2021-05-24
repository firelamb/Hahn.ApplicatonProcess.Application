using System.Collections.Generic;
using System.Threading.Tasks;
using Hahn.ApplicatonProcess.February2021.Domain.Models;

namespace Hahn.ApplicatonProcess.February2021.Domain.Interfaces
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentModel>> GetAll();
    }
}