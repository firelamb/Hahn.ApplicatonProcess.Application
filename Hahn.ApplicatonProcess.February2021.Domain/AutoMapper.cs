using AutoMapper;
using Hahn.ApplicatonProcess.February2021.Data.Entities;
using Hahn.ApplicatonProcess.February2021.Domain.Models;

namespace Hahn.ApplicatonProcess.February2021.Domain
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<Asset, AssetModel>().ReverseMap();
            CreateMap<Department, DepartmentModel>().ReverseMap();
        }
    }
}