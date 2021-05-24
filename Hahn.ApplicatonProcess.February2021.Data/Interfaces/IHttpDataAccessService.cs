using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.February2021.Data.Interfaces
{
    public interface IHttpDataAccessService
    {
        Task<bool> ValidateCountry(string countryName);
    }
}
