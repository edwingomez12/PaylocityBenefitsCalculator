﻿using System.Data.Entity.Validation;
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
            {var response = new ApiResponse<GetEmployeeDto> 
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
    [HttpGet("Calculate/{id}")]
    public async Task<ActionResult<ApiResponse<decimal>>> Calculate(int id)
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
        var salary = employeesWithDependents.FirstOrDefault().Salary;
        var age = DateTime.Now - employeesWithDependents.FirstOrDefault().DateOfBirth;
        var dependentCount = employeesWithDependents.FirstOrDefault().Dependents.Count;
        //do calulations based on requirements
        salary = salary - (1000 *12);
        salary = salary - (600 * dependentCount);
        salary = salary / 26;
        return new ApiResponse<decimal>
        {
            Data = salary,
            Success = true
        };
    }
}
