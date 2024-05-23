using Api.DAL;
using Api.Dtos.Dependent;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class DependentsController : ControllerBase
{
    private EmployeeContext _employeeContext;
    public DependentsController(EmployeeContext employeeContext)
    {
        _employeeContext = employeeContext;
    }
    [SwaggerOperation(Summary = "Get dependent by id")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<GetDependentDto>>> Get(int id)
    {
         var dependents = from d in _employeeContext.Dependents
                        .Where(de => de.Id == id)
                        select new GetDependentDto()
                        {
                            Id = d.Id,
                            FirstName = d.FirstName,
                            LastName = d.LastName,
                            DateOfBirth = d.DateOfBirth,
                            Relationship = d.Relationship
                        };
        var singleDependent = dependents.ToList();
        if(singleDependent.Count > 0 )
        {
            var response = new ApiResponse<GetDependentDto>
            {
                Data = singleDependent.FirstOrDefault(),
                Success = true
            };
            return response;
        }
        else 
        {
            return NotFound();
        }
       
    }

    [SwaggerOperation(Summary = "Get all dependents")]
    [HttpGet("")]
    public async Task<ActionResult<ApiResponse<List<GetDependentDto>>>> GetAll()
    {
        var dependents = from d in _employeeContext.Dependents
                        select new GetDependentDto()
                        {
                            Id = d.Id,
                            FirstName = d.FirstName,
                            LastName = d.LastName,
                            DateOfBirth = d.DateOfBirth,
                            Relationship = d.Relationship
                        };
        var response = new ApiResponse<List<GetDependentDto>> {
            Data = dependents.ToList(),
            Success = true
        };
        return response;
    }
}
