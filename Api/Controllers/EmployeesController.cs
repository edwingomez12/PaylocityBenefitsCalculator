using System.Data.Entity.Validation;
using System.Net;
using Api.DAL;
using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class EmployeesController : ControllerBase
{
private readonly EmployeeContext _employeeContext;
    public EmployeesController(EmployeeContext employeeContext)
    {
        _employeeContext = employeeContext;
    }

    [SwaggerOperation(Summary = "Get employee by id")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<GetEmployeeDto>>> Get(int id)
    {
        var employeesWithDependents = _employeeContext.Employees
            .Where(em => em.Id == id)
            .Select(employee => new GetEmployeeDto
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Salary = employee.Salary,
                DateOfBirth = employee.DateOfBirth,
                Dependents = employee.Dependents.Select(dependent => new GetDependentDto
                {
                    Id = dependent.Id,
                    FirstName = dependent.FirstName,
                    LastName = dependent.LastName,
                    DateOfBirth = dependent.DateOfBirth,
                    Relationship = dependent.Relationship
                }).ToList()
            }).ToList();

            if(employeesWithDependents.Count > 0)
            {
                var response = new ApiResponse<GetEmployeeDto> 
                {
                    Data = employeesWithDependents.FirstOrDefault(),
                    Success = true
                };
                return response;
            }
            else 
            {
               
                return NotFound();
            }
    }

    [SwaggerOperation(Summary = "Get all employees")]
    [HttpGet("")]
    public async Task<ActionResult<ApiResponse<List<GetEmployeeDto>>>> GetAll()
    {

        var employeesWithDependents = _employeeContext.Employees
            .Select(employee => new GetEmployeeDto
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Salary = employee.Salary,
                DateOfBirth = employee.DateOfBirth,
                Dependents = employee.Dependents.Select(dependent => new GetDependentDto
                {
                    Id = dependent.Id,
                    FirstName = dependent.FirstName,
                    LastName = dependent.LastName,
                    DateOfBirth = dependent.DateOfBirth,
                    Relationship = dependent.Relationship
                }).ToList()
            }).ToList();

        

        var result = new ApiResponse<List<GetEmployeeDto>>
        {
            Data = employeesWithDependents,
            Success = true
        };

        return result;
    }

    [SwaggerOperation(Summary = "Calculate paycheck")]
    [HttpGet("CalculatePaycheck/{id}")]
    public async Task<ActionResult<ApiResponse<decimal>>> CalculatePaycheck(int id)
    {
        var employeesWithDependents = _employeeContext.Employees
            .Where(em => em.Id == id)
            .Select(employee => new GetEmployeeDto
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Salary = employee.Salary,
                DateOfBirth = employee.DateOfBirth,
                Dependents = employee.Dependents.Select(dependent => new GetDependentDto
                {
                    Id = dependent.Id,
                    FirstName = dependent.FirstName,
                    LastName = dependent.LastName,
                    DateOfBirth = dependent.DateOfBirth,
                    Relationship = dependent.Relationship
                }).ToList()
            }).ToList();
        
        return new ApiResponse<decimal>
        {
            Data = CalculatePaycheck(employeesWithDependents.FirstOrDefault()),
            Success = true
        };
    }

    private decimal CalculatePaycheck(GetEmployeeDto employee)
    {
        decimal employeeBaseCostPerMonth = 1000m;
        decimal dependentCostPerMonth = 600m;
        decimal highSalaryThreshold = 80000m;
        decimal highSalaryAdditionalPercentage = 0.02m;
        decimal dependentAdditionalCostOver50 = 200m;

        decimal monthlyEmployeeCost = employeeBaseCostPerMonth;

        decimal monthlyDependentsCost = 0;
        foreach (var dependent in employee.Dependents)
        {
            monthlyDependentsCost += dependentCostPerMonth;
            int age = DateTime.Now.Year - dependent.DateOfBirth.Year;
            if (age > 50)
            {
                monthlyDependentsCost += dependentAdditionalCostOver50;
            }
        }

        decimal additionalHighSalaryCost = 0;
        if (employee.Salary > highSalaryThreshold)
        {
            additionalHighSalaryCost = employee.Salary * highSalaryAdditionalPercentage / 12;
        }

        decimal totalMonthlyCost = monthlyEmployeeCost + monthlyDependentsCost + additionalHighSalaryCost;

        decimal totalYearlyCost = totalMonthlyCost * 12;

        decimal paycheckDeduction = totalYearlyCost / 26;

        decimal paycheck = (employee.Salary / 26) - paycheckDeduction;

        return paycheck;
    }
}
