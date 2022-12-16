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

    public PoemsController(ILogger<PoemsController> logger, IPoemRepository poemRepository)
    {
        _logger = logger;
        _poemRepository = poemRepository ?? throw new ArgumentNullException(nameof(poemRepository));
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

            var poemViewModel = new PoemViewModel(poem);
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
