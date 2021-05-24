using System.Threading.Tasks;
using Hahn.ApplicatonProcess.February2021.Domain.Models;

namespace Hahn.ApplicatonProcess.February2021.Domain.Interfaces
{
    public interface IAssetService
    {
        Task<AssetModel> Insert(AssetModel model);
        Task<AssetModel> Update(AssetModel model);
        Task<bool> Delete(int id);
        Task<AssetModel> GetById(int id);
    }
}