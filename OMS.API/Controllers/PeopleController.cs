using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OMS.API.Dtos.Tables;
using OMS.BL.IServices.Tables;
using OMS.BL.Models.Tables;

namespace OMS.API.Controllers;

/// <summary>
/// Handles CRUD operations for managing people in the system.
/// </summary>
[Route("api/people")]
[ApiController]
public class PeopleController : ControllerBase
{
    private readonly IPersonService _personService;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="PeopleController"/> class.
    /// </summary>
    /// <param name="personService">Person service interface.</param>
    /// <param name="mapper">AutoMapper instance for entity mapping.</param>
    public PeopleController(IPersonService personService, IMapper mapper)
    {
        _personService = personService;
        _mapper = mapper;
    }

    /// <summary>
    /// Retrieves a list of all people stored in the database.
    /// </summary>
    /// <returns>A collection of people.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<PersonDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<PersonDto>>> GetAllAsync()
    {
        try
        {
            var peopleModel = await _personService.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<PersonDto>>(peopleModel));
        }
        catch (Exception ex)
        {
            return Problem(detail: ex.Message,
                           statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Retrieves a person by their unique ID.
    /// </summary>
    /// <param name="personId">The ID of the person to retrieve.</param>
    /// <returns>Person details if found, or an error message if not.</returns>
    [HttpGet("{personId:int}", Name = "GetByIdAsync")]
    [ProducesResponseType(typeof(PersonDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<PersonDto>> GetByIdAsync([FromRoute] int personId)
    {
        if (personId <= 0) return BadRequest($"Invalid PersonId: [{personId}]");

        try
        {
            var personModel = await _personService.GetByIdAsync(personId);
            return personModel != null
                ? Ok(_mapper.Map<PersonDto>(personModel))
                : NotFound($"No person found with ID: [{personId}]");
        }
        catch (Exception ex)
        {
            return Problem(detail: ex.Message,
                           statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Adds a new person to the database.
    /// </summary>
    /// <param name="personDto">The details of the new person.</param>
    /// <returns>The created person with assigned ID.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(PersonDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<PersonDto>> AddAsync([FromBody] PersonDto personDto)
    {
        try
        {
            var personModel = _mapper.Map<PersonModel>(personDto);
            var isSuccess = await _personService.AddAsync(personModel);

            if (isSuccess)
            {
                personDto.PersonId = personModel.PersonId;
                return CreatedAtRoute(nameof(GetByIdAsync), new { personId = personModel.PersonId }, personDto);
            }

            return BadRequest("Failed to save person in the database.");
        }
        catch (Exception ex)
        {
            return Problem(detail: ex.Message,
               statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Updates an existing person’s details.
    /// </summary>
    /// <param name="personId">The ID of the person to update.</param>
    /// <param name="personDto">The new details of the person.</param>
    /// <returns>A message indicating success or failure.</returns>
    [HttpPut("{personId:int}")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateAsync([FromRoute] int personId, [FromBody] PersonDto personDto)
    {
        if (personId != personDto.PersonId) return BadRequest("Identifier mismatch.");

        try
        {
            var personModel = _mapper.Map<PersonModel>(personDto);
            var isSuccess = await _personService.UpdateAsync(personModel);

            return isSuccess
                ? Ok($"Person with ID: [{personId}]. Successfully updated.")
                : BadRequest("Failed to update person in the database.");
        }
        catch (Exception ex)
        {
            return Problem(detail: ex.Message,
                           statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Deletes a person from the database.
    /// </summary>
    /// <param name="personId">The ID of the person to delete.</param>
    /// <returns>A message indicating success or failure.</returns>
    [HttpDelete("{personId:int}")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteAsync([FromRoute] int personId)
    {
        if (personId <= 0) return BadRequest($"Invalid PersonId: [{personId}]");

        try
        {
            var isSuccess = await _personService.DeleteAsync(personId);

            return isSuccess
                ? Ok("Person successfully deleted.")
                : BadRequest($"Deletion failed: The specified person with ID: [{personId}] does not exist.");

        }
        catch (Exception ex)
        {
            return Problem(detail: ex.Message,
                           statusCode: StatusCodes.Status500InternalServerError,
                           title: "The operation may have been blocked due to database restrictions.");
        }
    }
}
