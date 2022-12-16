using Microsoft.AspNetCore.Mvc;
using StanzaBonanza.DataAccess.Repositories;
using StanzaBonanza.Models.ViewModels;

namespace StanzaBonanza.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PoemsController : ControllerBase
{
    private readonly ILogger<PoemsController> _logger;
    private readonly IPoemRepository _poemRepository;
    private readonly IAuthorPoemJoinService _authorPoemJoinService;

    public PoemsController(ILogger<PoemsController> logger, IPoemRepository poemRepository, IAuthorPoemJoinService authorPoemJoinService)
    {
        _logger = logger;
        _poemRepository = poemRepository ?? throw new ArgumentNullException(nameof(poemRepository));
        _authorPoemJoinService = authorPoemJoinService ?? throw new ArgumentNullException(nameof(authorPoemJoinService));
    }

    [HttpGet]
    [Route("{id?}")]
    public async Task<IActionResult> GetPoemByIdAsync(int id)
    {
        try
        {
            _logger.LogInformation("Request received for poem by ID " + id);

            var authorPoemJoins = await _authorPoemJoinService.GetAuthorPoemJoinAsync();
            var targetJoin = authorPoemJoins?.FirstOrDefault(join => join?.Poem?.Id == id);

            if (targetJoin == null)
                return new BadRequestObjectResult("Poem not found");

            var poemViewModel = new PoemViewModel(targetJoin);
            return Ok(poemViewModel);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting poems");
            return BadRequest(ex);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetPoemsAsync()
    {
        try
        {
            _logger.LogInformation("Request received for poems");

            var authorPoemJoins = await _authorPoemJoinService.GetAuthorPoemJoinAsync();

            if (authorPoemJoins == null)
                throw new NullReferenceException("Author-poem join result was null");

            var poemsViewModel = new PoemsViewModel(authorPoemJoins);
            return Ok(poemsViewModel);
        }   
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting poems");
            return BadRequest(ex);
        }
    }
}
