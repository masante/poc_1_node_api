using Microsoft.AspNetCore.Mvc;
using PersonApi.Models;
using PersonApi.Services;

namespace PersonApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccreditationsController : ControllerBase
{
    private readonly IAccreditationService _accreditationService;

    public AccreditationsController(IAccreditationService accreditationService)
    {
        _accreditationService = accreditationService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Accreditation>>> GetAllAccreditations()
    {
        var accreditations = await _accreditationService.GetAllAccreditationsAsync();
        return Ok(accreditations);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Accreditation>> GetAccreditationById(string id)
    {
        var accreditation = await _accreditationService.GetAccreditationByIdAsync(id);
        if (accreditation == null)
            return NotFound();
        return Ok(accreditation);
    }

    [HttpGet("bymaterial/{material}")]
    public async Task<ActionResult<List<Accreditation>>> GetAccreditationsByMaterial(string material)
    {
        var accreditations = await _accreditationService.GetAccreditationsByMaterialAsync(material);
        return Ok(accreditations);
    }

    [HttpGet("bytype/{wasteProcessingType}")]
    public async Task<ActionResult<List<Accreditation>>> GetAccreditationsByWasteProcessingType(string wasteProcessingType)
    {
        var accreditations = await _accreditationService.GetAccreditationsByWasteProcessingTypeAsync(wasteProcessingType);
        return Ok(accreditations);
    }

    [HttpGet("bystatus/{status}")]
    public async Task<ActionResult<List<Accreditation>>> GetAccreditationsByStatus(string status)
    {
        var accreditations = await _accreditationService.GetAccreditationsByStatusAsync(status);
        return Ok(accreditations);
    }

    [HttpPost]
    public async Task<ActionResult<Accreditation>> CreateAccreditation([FromBody] Accreditation accreditation)
    {
        if (string.IsNullOrWhiteSpace(accreditation.Status) || 
            string.IsNullOrWhiteSpace(accreditation.Material) ||
            string.IsNullOrWhiteSpace(accreditation.WasteProcessingType))
            return BadRequest("Status, Material, and WasteProcessingType are required");

        var createdAccreditation = await _accreditationService.CreateAccreditationAsync(accreditation);
        return CreatedAtAction(nameof(GetAccreditationById), new { id = createdAccreditation.Id }, createdAccreditation);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAccreditation(string id, [FromBody] Accreditation accreditation)
    {
        if (string.IsNullOrWhiteSpace(accreditation.Status))
            return BadRequest("Status is required");

        var updated = await _accreditationService.UpdateAccreditationAsync(id, accreditation);
        if (!updated)
            return NotFound();

        return Ok(accreditation);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAccreditation(string id)
    {
        var deleted = await _accreditationService.DeleteAccreditationAsync(id);
        if (!deleted)
            return NotFound();

        return NoContent();
    }
}
