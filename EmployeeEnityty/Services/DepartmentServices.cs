using AutoMapper;
using EmployeeEnityty.Dto;
using EmployeeEnityty.EmployeeDb;
using EmployeeEnityty.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeEnityty.Services
{
    public interface IDepartmentServices
    {
        Task<IEnumerable<DepartmentDto>> GetDepartmentsAsync();
        Task AddDepartmentAsync(Department department);
    }

    public class DepartmentServices : IDepartmentServices
    {
        private readonly EmployeeDbContext _dbContext;
        private readonly IMapper _mapper;

        public DepartmentServices(EmployeeDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DepartmentDto>> GetDepartmentsAsync()
        {
            var departments = await _dbContext.Departments.ToListAsync();
            return _mapper.Map<IEnumerable<DepartmentDto>>(departments);
        }

        public async Task AddDepartmentAsync(Department department)
        {
            await _dbContext.Departments.AddAsync(department);
            await _dbContext.SaveChangesAsync();
        }
    }

}
