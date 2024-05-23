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
        
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Dependent> Dependents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Dependent>()
            .HasOne(d => d.Employee)
            .WithMany(e => e.Dependents)
            .HasForeignKey(d => d.EmployeeId);
            modelBuilder.Entity<Employee>().HasData(EmployeeInitializer.Seed());
            modelBuilder.Entity<Dependent>().HasData(EmployeeInitializer.SeedDependents());
    }

    public void ClearDatabase()
{
    Employees.RemoveRange(Employees);
    Dependents.RemoveRange(Dependents);
    SaveChanges();

    Employees.AddRange(EmployeeInitializer.Seed());
    Dependents.AddRange(EmployeeInitializer.SeedDependents());
    SaveChanges();
}


    }
}