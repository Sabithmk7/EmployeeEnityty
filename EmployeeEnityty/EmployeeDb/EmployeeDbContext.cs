using EmployeeEnityty.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeEnityty.EmployeeDb
{
    public class EmployeeDbContext:DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }

        private readonly IConfiguration _configuration;
        public EmployeeDbContext(IConfiguration configuration)
        {
            _configuration=configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasOne(x => x.Department)
                .WithMany(x => x.Employees)
                .HasForeignKey(x => x.DepartmentId);
        }
    }
}
