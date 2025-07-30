using Microsoft.AspNetCore.Mvc;
using PopsicleFactory.Domain.Interfaces;
using PopsicleFactory.Application.Dtos;
using PopsicleFactory.Domain.Entities;
using MapsterMapper;
using Microsoft.AspNetCore.JsonPatch;


namespace PopsicleFactory.WebAPI.Controllers;
//korterra Assesment Popsicles Controller 

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

    [HttpGet] // Get ALl Popsicles
    public async Task<IActionResult> SearchPopsicles()
    {
        var popsicles = await _repository.GetAllAsync();
        return Ok(_mapper.Map<IEnumerable<PopsicleDto>>(popsicles));
    }

    [HttpGet("{id:guid}")] // Get by ID
    public async Task<IActionResult> GetPopsicle(Guid id)
    {
        var popsicle = await _repository.GetByIdAsync(id);
        if (popsicle is null)
            return NotFound($"Popsicle with ID {id} not found.");

        return Ok(_mapper.Map<PopsicleDto>(popsicle));
    }

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
    
        // Create a DTO from the existing entity to apply the patch to
        var popsicleToPatch = _mapper.Map<UpdatePopsicleDto>(existingPopsicle);
    
        // Apply the patch operations
        patchDoc.ApplyTo(popsicleToPatch, ModelState);
    
        // Check if the patch operations were valid
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        // Map the patched DTO back to the original entity
        _mapper.Map(popsicleToPatch, existingPopsicle);
        
        await _repository.UpdateAsync(existingPopsicle);
    
        return NoContent();
    }

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
