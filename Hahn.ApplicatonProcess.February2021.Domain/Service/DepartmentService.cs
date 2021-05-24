using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hahn.ApplicatonProcess.February2021.Data.Context;
using Hahn.ApplicatonProcess.February2021.Domain.Interfaces;
using Hahn.ApplicatonProcess.February2021.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Hahn.ApplicatonProcess.February2021.Domain.Service
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IMapper _mapper;
        private readonly HahnDbContext _db;

        public DepartmentService(IMapper mapper, HahnDbContext dataContext)
        {
            _db = dataContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DepartmentModel>> GetAll()
        {
            return await _db.Department.ProjectTo<DepartmentModel>(_mapper.ConfigurationProvider).ToListAsync();
        }
    }
}