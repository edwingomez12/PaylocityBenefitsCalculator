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
        throw new NotImplementedException();
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
