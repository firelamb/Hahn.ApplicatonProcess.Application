using System;
using System.Threading.Tasks;
using AutoMapper;
using Hahn.ApplicatonProcess.February2021.Data.Entities;
using Hahn.ApplicatonProcess.February2021.Data.Interfaces;
using Hahn.ApplicatonProcess.February2021.Domain.Interfaces;
using Hahn.ApplicatonProcess.February2021.Domain.Models;

namespace Hahn.ApplicatonProcess.February2021.Domain.Service
{
    public class AssetService : IAssetService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public AssetService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<AssetModel> Insert(AssetModel model)
        {
            var entity = _mapper.Map<Asset>(model);

            entity.ID = 0;
            await _unitOfWork.Asset.InsertAsync(entity);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<AssetModel>(entity);
        }

        public async Task<AssetModel> GetById(int id)
        {
            var entity = await _unitOfWork.Asset.GetByIdAsync(id);
            return _mapper.Map<AssetModel>(entity);
        }

        public async Task<AssetModel> Update(AssetModel model)
        {
            var result = await _unitOfWork.Asset.GetByIdAsync(model.ID);

            if (result == null) throw new Exception("Asset not found");

            var entity = _mapper.Map<Asset>(model);
            entity.ID = model.ID;

            _unitOfWork.Asset.Update(entity);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<AssetModel>(entity);
        }

        public async Task<bool> Delete(int id)
        {
            var result = await _unitOfWork.Asset.GetByIdAsync(id);

            if (result == null) throw new Exception("Asset not found");

            _unitOfWork.Asset.Delete(id);

            return await _unitOfWork.SaveAsync() > 0;
        }
    }
}