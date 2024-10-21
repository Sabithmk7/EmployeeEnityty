using AutoMapper;
using EmployeeEnityty.Dto;
using EmployeeEnityty.Models;
using EmployeeEnityty.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EmployeeEnityty.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeServices _services;
        private readonly IMapper _mapper;
        
        public EmployeeController(IEmployeeServices services, IMapper mapper)
        {
            _services = services;
            _mapper = mapper;
        }

        [HttpGet("getemployees")]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _services.GetAllEmployeesAsync();
            return Ok(employees);
        }

        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(int id)
        {
            var employee = await _services.GetByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        [HttpPut("UpdateEmployee")]
        public async Task<IActionResult> UpdateEmployee([FromBody] EmployeeDto emp)
        {
            var existingEmployee = await _services.GetByIdAsync(emp.Id);
            if (existingEmployee == null)
            {
                return NotFound("Employee not found");
            }
            var empDto = _mapper.Map<Employee>(emp);
            await _services.UpdateEmployeeAsync(empDto);
            return Ok("Successfully updated");
        }

        [HttpPost("addEmployee")]
        public async Task<IActionResult> AddEmployee(EmployeeDto emp)
        {
            try
            {
                var empDto = _mapper.Map<Employee>(emp);
                await _services.AddEmployeeAsync(empDto);
                return Ok("Added successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Server issue: " + ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var emp = await _services.GetByIdAsync(id);
            if (emp == null)
            {
                return NotFound("Employee not found");
            }

            try
            {
                await _services.DeleteEmployeeAsync(id);
                return Ok("Successfully deleted");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server issue: " + ex.Message);
            }
        }
    }
}
