using Microsoft.AspNetCore.Mvc;
using PersonApi.Models;
using PersonApi.Services;

namespace PersonApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrganisationsController : ControllerBase
{
    private readonly IOrganisationService _organisationService;

    public OrganisationsController(IOrganisationService organisationService)
    {
        _organisationService = organisationService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Organisation>>> GetAllOrganisations()
    {
        var organisations = await _organisationService.GetAllOrganisationsAsync();
        return Ok(organisations);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Organisation>> GetOrganisationById(string id)
    {
        var organisation = await _organisationService.GetOrganisationByIdAsync(id);
        if (organisation == null)
            return NotFound();
        return Ok(organisation);
    }

    [HttpGet("byorgid/{orgId}")]
    public async Task<ActionResult<Organisation>> GetOrganisationByOrgId(string orgId)
    {
        var organisation = await _organisationService.GetOrganisationByOrgIdAsync(orgId);
        if (organisation == null)
            return NotFound();
        return Ok(organisation);
    }

    [HttpGet("bytype/{businessType}")]
    public async Task<ActionResult<List<Organisation>>> GetOrganisationsByBusinessType(string businessType)
    {
        var organisations = await _organisationService.GetOrganisationsByBusinessTypeAsync(businessType);
        return Ok(organisations);
    }

    [HttpPost]
    public async Task<ActionResult<Organisation>> CreateOrganisation([FromBody] Organisation organisation)
    {
        if (string.IsNullOrWhiteSpace(organisation.OrgId) || string.IsNullOrWhiteSpace(organisation.BusinessType))
            return BadRequest("OrgId and BusinessType are required");

        var createdOrganisation = await _organisationService.CreateOrganisationAsync(organisation);
        return CreatedAtAction(nameof(GetOrganisationById), new { id = createdOrganisation.Id }, createdOrganisation);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOrganisation(string id, [FromBody] Organisation organisation)
    {
        if (string.IsNullOrWhiteSpace(organisation.BusinessType))
            return BadRequest("BusinessType is required");

        var updated = await _organisationService.UpdateOrganisationAsync(id, organisation);
        if (!updated)
            return NotFound();

        return Ok(organisation);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrganisation(string id)
    {
        var deleted = await _organisationService.DeleteOrganisationAsync(id);
        if (!deleted)
            return NotFound();

        return NoContent();
    }
}
