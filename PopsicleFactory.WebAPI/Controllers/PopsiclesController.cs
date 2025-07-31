using Microsoft.AspNetCore.Mvc;
using PopsicleFactory.Domain.Interfaces;
using PopsicleFactory.Application.Dtos;
using PopsicleFactory.Domain.Entities;
using MapsterMapper;
using Microsoft.AspNetCore.JsonPatch;

namespace PopsicleFactory.WebAPI.Controllers;
//korterra Assessment Popsicles Controller 

[ApiController]
[Route("api/[controller]")]
public class PopsiclesController : ControllerBase
{
    private readonly IPopsicleRepository _repository;
    private readonly IMapper _mapper;

    public PopsiclesController(IPopsicleRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    /*
    Scenario: Promise Broken - Popsicle Request is Invalid
    NOTE: This scenario is handled automatically by the ASP.NET Core pipeline
    and FluentValidation middleware *before* the request reaches the controller.
    If the request DTO is invalid, the middleware intercepts it and returns
    a 400 Bad Request with validation errors.
    */

    /*
    Scenario: Search Popsicles
    Given a Popsicle request is valid
    And Popsicles exist that match the search criteria
    When the search request is received from the web service
    Then an appropriate status code will be returned
    And the payload should contain the list of Popsicles that matched the search criteria.
    */

    [HttpGet] // Get ALl Popsicles
    public async Task<IActionResult> SearchPopsicles()
    {
        var popsicles = await _repository.GetAllAsync();
        return Ok(_mapper.Map<IEnumerable<PopsicleDto>>(popsicles));
    }
    /*
    Scenario: Get Popsicle
    Given the Popsicle request is valid
    And a Popsicle exists
    When the get request is received from the web service
    Then an appropriate status code will be returned
    And the Popsicle View Model will be returned.

    Scenario: Promise Broken - Popsicle does not exist
    Given a Popsicle request is valid
    And the Popsicle requested does not exist
    When the request is received from the web service
    Then an appropriate status code will be returned
    And the message explaining that the Popsicle does not exist should be returned
    */

    [HttpGet("{id:guid}")] // Get by ID
    public async Task<IActionResult> GetPopsicle(Guid id)
    {
        var popsicle = await _repository.GetByIdAsync(id);
        if (popsicle is null)
            return NotFound($"Popsicle with ID {id} not found.");

        return Ok(_mapper.Map<PopsicleDto>(popsicle));
    }
    /*
    Scenario: Create Popsicle
    Given the Popsicle request is valid
    When the create request is received from the web service
    Then an appropriate status code will be returned
    And the Popsicle will be persisted
    And a view model of the Popsicle will be returned.
    */
    [HttpPost] // Post
    public async Task<IActionResult> CreatePopsicle([FromBody] CreatePopsicleDto createDto)
    {
        var popsicle = _mapper.Map<Popsicle>(createDto);
        popsicle.Id = Guid.NewGuid();
        popsicle.CreatedAt = DateTimeOffset.UtcNow;

        await _repository.AddAsync(popsicle);

        var popsicleDto = _mapper.Map<PopsicleDto>(popsicle);
        return CreatedAtAction(nameof(GetPopsicle), new { id = popsicleDto.Id }, popsicleDto);
    }

    /*
    Scenario: Replace Popsicle
    Given the Popsicle request is valid
    And a Popsicle exists
    When the replace request is received from the web service
    Then an appropriate status code will be returned
    And the Popsicle will be persisted with all properties overwritten
    And a view model of the Popsicle will be returned.
    
    Scenario: Promise Broken - Popsicle does not exist
    (This method also covers the "not found" case for a PUT request)
    */

    [HttpPut("{id:guid}")] // Update by ID
    public async Task<IActionResult> ReplacePopsicle(Guid id, [FromBody] UpdatePopsicleDto updateDto)
    {
        var existingPopsicle = await _repository.GetByIdAsync(id);
        if (existingPopsicle is null)
            return NotFound($"Popsicle with id number :  {id} not found");

        _mapper.Map(updateDto, existingPopsicle);
        await _repository.UpdateAsync(existingPopsicle);

        return Ok(_mapper.Map<PopsicleDto>(existingPopsicle));
    }

    /*
    Scenario: Update Popsicle (optional for challenge)
    Given the Popsicle request is valid
    And a Popsicle exists
    When the update request is received from the web service
    Then an appropriate status code will be returned
    And the Popsicle will be persisted with only the properties that were changed
    And a view model of the Popsicle will be returned.

    Scenario: Promise Broken - Popsicle does not exist
    (This method also covers the "not found" case for a PATCH request)
    */

    [HttpPatch("{id:guid}")]
    public async Task<IActionResult> UpdatePopsicle(Guid id, [FromBody] JsonPatchDocument<UpdatePopsicleDto> patchDoc)
    {
        if (patchDoc is null)
        {
            return BadRequest();
        }

        var existingPopsicle = await _repository.GetByIdAsync(id);
        if (existingPopsicle is null)
        {
            return NotFound($"Popsicle with ID {id} not found.");
        }

        var popsicleToPatch = _mapper.Map<UpdatePopsicleDto>(existingPopsicle);
        patchDoc.ApplyTo(popsicleToPatch, ModelState);

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        _mapper.Map(popsicleToPatch, existingPopsicle);

        await _repository.UpdateAsync(existingPopsicle);
        
        //return Ok(_mapper.Map<PopsicleDto>(existingPopsicle));

        return NoContent();
    }
    /*
    Scenario: Remove Popsicle (optional for challenge)
    Given the Popsicle request is valid
    And a Popsicle exists
    When the remove request is received from the web service
    Then an appropriate status code will be returned
    And the Popsicle will be removed from storage.

    Scenario: Promise Broken - Popsicle does not exist
    (This method also covers the "not found" case for a DELETE request)
    */
    [HttpDelete("{id:guid}")] // Delete by ID
    public async Task<IActionResult> RemovePopsicle(Guid id)
    {
        var existingPopsicle = await _repository.GetByIdAsync(id);
        if (existingPopsicle is null)
            return NotFound($"Popsicle with id : {id} not found");

        await _repository.DeleteAsync(id);
        return NoContent();
    }
}
