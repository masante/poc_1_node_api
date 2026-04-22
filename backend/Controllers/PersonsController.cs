using Microsoft.AspNetCore.Mvc;
using PersonApi.Models;
using PersonApi.Services;

namespace PersonApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PersonsController : ControllerBase
{
    private readonly IPersonService _personService;

    public PersonsController(IPersonService personService)
    {
        _personService = personService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Person>>> GetAllPersons()
    {
        var persons = await _personService.GetAllPersonsAsync();
        return Ok(persons);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Person>> GetPersonById(string id)
    {
        var person = await _personService.GetPersonByIdAsync(id);
        if (person == null)
            return NotFound();
        return Ok(person);
    }

    [HttpPost]
    public async Task<ActionResult<Person>> CreatePerson([FromBody] Person person)
    {
        if (string.IsNullOrWhiteSpace(person.FirstName) || string.IsNullOrWhiteSpace(person.LastName))
            return BadRequest("FirstName and LastName are required");

        var createdPerson = await _personService.CreatePersonAsync(person);
        return CreatedAtAction(nameof(GetPersonById), new { id = createdPerson.Id }, createdPerson);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePerson(string id, [FromBody] Person person)
    {
        if (string.IsNullOrWhiteSpace(person.FirstName) || string.IsNullOrWhiteSpace(person.LastName))
            return BadRequest("FirstName and LastName are required");

        var updated = await _personService.UpdatePersonAsync(id, person);
        if (!updated)
            return NotFound();

        return Ok(person);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePerson(string id)
    {
        var deleted = await _personService.DeletePersonAsync(id);
        if (!deleted)
            return NotFound();

        return NoContent();
    }
}
