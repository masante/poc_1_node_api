using Microsoft.AspNetCore.Mvc;
using PersonApi.Models;
using PersonApi.Services;

namespace PersonApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RegistrationsController : ControllerBase
{
    private readonly IRegistrationService _registrationService;

    public RegistrationsController(IRegistrationService registrationService)
    {
        _registrationService = registrationService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Registration>>> GetAllRegistrations()
    {
        var registrations = await _registrationService.GetAllRegistrationsAsync();
        return Ok(registrations);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Registration>> GetRegistrationById(string id)
    {
        var registration = await _registrationService.GetRegistrationByIdAsync(id);
        if (registration == null)
            return NotFound();
        return Ok(registration);
    }

    [HttpGet("bymaterial/{material}")]
    public async Task<ActionResult<List<Registration>>> GetRegistrationsByMaterial(string material)
    {
        var registrations = await _registrationService.GetRegistrationsByMaterialAsync(material);
        return Ok(registrations);
    }

    [HttpGet("bytype/{wasteProcessingType}")]
    public async Task<ActionResult<List<Registration>>> GetRegistrationsByWasteProcessingType(string wasteProcessingType)
    {
        var registrations = await _registrationService.GetRegistrationsByWasteProcessingTypeAsync(wasteProcessingType);
        return Ok(registrations);
    }

    [HttpGet("bystatus/{status}")]
    public async Task<ActionResult<List<Registration>>> GetRegistrationsByStatus(string status)
    {
        var registrations = await _registrationService.GetRegistrationsByStatusAsync(status);
        return Ok(registrations);
    }

    [HttpPost]
    public async Task<ActionResult<Registration>> CreateRegistration([FromBody] Registration registration)
    {
        if (string.IsNullOrWhiteSpace(registration.Status) || 
            string.IsNullOrWhiteSpace(registration.Material) ||
            string.IsNullOrWhiteSpace(registration.WasteProcessingType))
            return BadRequest("Status, Material, and WasteProcessingType are required");

        var createdRegistration = await _registrationService.CreateRegistrationAsync(registration);
        return CreatedAtAction(nameof(GetRegistrationById), new { id = createdRegistration.Id }, createdRegistration);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRegistration(string id, [FromBody] Registration registration)
    {
        if (string.IsNullOrWhiteSpace(registration.Status))
            return BadRequest("Status is required");

        var updated = await _registrationService.UpdateRegistrationAsync(id, registration);
        if (!updated)
            return NotFound();

        return Ok(registration);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRegistration(string id)
    {
        var deleted = await _registrationService.DeleteRegistrationAsync(id);
        if (!deleted)
            return NotFound();

        return NoContent();
    }
}
