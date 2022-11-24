using Microsoft.AspNetCore.Mvc;
using StanzaBonanza.DataAccess.Repositories;

namespace StanzaBonanza.Controllers;

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
            var poem = await _poemRepository.GetByIdAsync(id);
            return Ok(poem);
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
