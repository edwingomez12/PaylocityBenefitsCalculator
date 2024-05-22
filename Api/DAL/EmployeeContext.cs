using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Api.DAL
{
    public class EmployeeContext : DbContext
    {
    
        public EmployeeContext(DbContextOptions<EmployeeContext> options) : base(options)
        {
        }
        
        public DbSet<GetEmployeeDto> Employees { get; set; }
        public DbSet<GetDependentDto> Dependents { get; set; }

    }
}