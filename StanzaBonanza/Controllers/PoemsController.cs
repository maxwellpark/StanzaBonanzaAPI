using Microsoft.AspNetCore.Mvc;
using StanzaBonanza.DataAccess.Repositories.Interfaces;
using StanzaBonanza.Models.ViewModels;
using StanzaBonanza.Services.Interfaces;

namespace StanzaBonanza.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PoemsController : ControllerBase
{
    private readonly ILogger<PoemsController> _logger;
    private readonly IPoemAuthorJoinService _authorPoemJoinService;

    public PoemsController(ILogger<PoemsController> logger, IPoemRepository poemRepository, IPoemAuthorJoinService authorPoemJoinService)
    {
        _logger = logger;
        _authorPoemJoinService = authorPoemJoinService ?? throw new ArgumentNullException(nameof(authorPoemJoinService));
    }

    [HttpGet]
    [Route("{id?}")]
    public async Task<IActionResult> GetPoemByIdAsync(int id)
    {
        try
        {
            _logger.LogInformation("Request received for poem by ID " + id);

            var authorPoemsJoins = await _authorPoemJoinService.GetPoems_AuthorsJoinAsync();

            if (authorPoemsJoins == null)
                throw new NullReferenceException("Author-poem join result was null");

            var targetResult = authorPoemsJoins?.FirstOrDefault(join => join?.Poem?.PoemId == id);

            if (targetResult == null)
                return new BadRequestObjectResult("Poem not found by ID " + id);

            var poemViewModel = new PoemViewModel(targetResult);
            return Ok(poemViewModel);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting poem by ID " + id);
            return BadRequest(ex);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetPoemsAsync()
    {
        try
        {
            _logger.LogInformation("Request received for poems");

            var authorPoemsJoins = await _authorPoemJoinService.GetPoems_AuthorsJoinAsync();

            if (authorPoemsJoins == null)
                throw new NullReferenceException("Author-poem join result was null");

            var poemsViewModel = new PoemsViewModel(authorPoemsJoins);
            return Ok(poemsViewModel);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting poems");
            return BadRequest(ex);
        }
    }
}
