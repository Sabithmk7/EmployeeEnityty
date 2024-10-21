using EmployeeEnityty.Dto;
using EmployeeEnityty.EmployeeDb;
using EmployeeEnityty.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeEnityty.Services
{
    public interface IEmployeeServices
    {
        Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync();
        Task<Employee> GetByIdAsync(int id);
        Task UpdateEmployeeAsync(Employee newEmployee);
        Task AddEmployeeAsync(Employee employee);
        Task DeleteEmployeeAsync(int id);
    }

    public class EmployeeServices : IEmployeeServices
    {
        private readonly EmployeeDbContext _dbContext;

        public EmployeeServices(EmployeeDbContext context)
        {
            _dbContext = context;
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync()
        {
            var em =  await _dbContext.Employees.Include(x => x.Department).ToListAsync();
            var e = em.Select(emp => new EmployeeDto
            {
                Id = emp.Id,
                Name = emp.Name,
                DepartmentName = emp?.Department.Name,
                Salary = emp.Salary
            });
            return e;
        }

        public async Task<Employee> GetByIdAsync(int id)
        {
            return await _dbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateEmployeeAsync(Employee newEmployee)
        {
            var employee = await GetByIdAsync(newEmployee.Id);
            if (employee != null)
            {
                employee.Name = newEmployee.Name;
                employee.Salary = newEmployee.Salary;

                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task AddEmployeeAsync(Employee employee)
        {
            await _dbContext.AddAsync(employee);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            var employee = await GetByIdAsync(id);
            if (employee != null)
            {
                _dbContext.Remove(employee);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
