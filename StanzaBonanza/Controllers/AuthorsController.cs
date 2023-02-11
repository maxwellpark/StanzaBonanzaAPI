using Microsoft.AspNetCore.Mvc;
using StanzaBonanza.DataAccess.Repositories.Interfaces;

namespace StanzaBonanza.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthorsController : ControllerBase
{
    private readonly ILogger<AuthorsController> _logger;
    private readonly IAuthorRepository _authorRepository;

    public AuthorsController(ILogger<AuthorsController> logger, IAuthorRepository authorRepository)
    {
        _logger = logger;
        _authorRepository = authorRepository ?? throw new ArgumentNullException(nameof(authorRepository));
    }

    [HttpGet]
    [Route("{id?}")]
    public async Task<IActionResult> GetAuthorByIdAsync(int id)
    {
        try
        {
            var poem = await _authorRepository.GetByIdAsync(id);
            return Ok(poem);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting poems");
            return BadRequest(ex);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAuthorsAsync()
    {
        try
        {
            var poems = await _authorRepository.GetAllAsync();
            return Ok(poems);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting poems");
            return BadRequest(ex);
        }
    }
}
