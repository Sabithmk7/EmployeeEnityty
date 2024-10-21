using AutoMapper;
using EmployeeEnityty.Dto;
using EmployeeEnityty.Models;
using EmployeeEnityty.Services;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class DepartmentController : ControllerBase
{
    private readonly IDepartmentServices _departmentServices;
    private readonly IMapper _mapper;

    public DepartmentController(IDepartmentServices services, IMapper mapper)
    {
        _departmentServices = services;
        _mapper = mapper;
    }

    [HttpGet("getdepartment")]
    public async Task<IActionResult> Get()
    {
        var departments = await _departmentServices.GetDepartmentsAsync();
        return Ok(departments);
    }

    [HttpPost("AddDepartment")]
    public async Task<IActionResult> Post(DepartmentDto department)
    {
        var dep = _mapper.Map<Department>(department);
        await _departmentServices.AddDepartmentAsync(dep);
        return Ok("Successfully Added");
    }
}
