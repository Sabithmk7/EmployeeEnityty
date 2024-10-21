using AutoMapper;
using EmployeeEnityty.Dto;
using EmployeeEnityty.Models;

namespace EmployeeEnityty
{
    public class AutoMap:Profile
    {
        public AutoMap()
        {
            CreateMap<Department,DepartmentDto>().ReverseMap();
            CreateMap<Employee, EmployeeDto>().ReverseMap();
        }
    }
}
