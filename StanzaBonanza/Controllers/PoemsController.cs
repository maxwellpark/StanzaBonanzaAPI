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
            var poem = await _poemRepository.GetByIdAsync(id);

            if (poem == null)
                throw new NullReferenceException($"Poem object returned by {nameof(_poemRepository.GetByIdAsync)} method was null.");

            var authorPoemJoins = await _authorPoemJoinService.GetAuthorPoemJoinAsync();
            var join = authorPoemJoins?.FirstOrDefault(join => join.Poem?.Id == id);

            var poemViewModel = new PoemViewModel(join);
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
            var poems = await _poemRepository.GetAllAsync();
            return Ok(poems);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting poems");
            return BadRequest(ex);
        }
    }
}
