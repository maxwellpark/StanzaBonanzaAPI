using Microsoft.AspNetCore.Mvc;
using StanzaBonanza.DataAccess.UnitOfWork;
using StanzaBonanza.Models.Models;

namespace StanzaBonanza.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthorsController : ControllerBase
{
    private readonly ILogger<AuthorsController> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public AuthorsController(ILogger<AuthorsController> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    [HttpGet]
    [Route("{id?}")]
    public async Task<IActionResult> GetAuthorByIdAsync(int id)
    {
        var authorsRepo = _unitOfWork.GetRepository<Author>();

        try
        {
            var author = await authorsRepo.GetByIdAsync(id);
            return author != null ? Ok(author) : NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting author by ID " + id);
            return BadRequest(ex);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAuthorsAsync()
    {
        var authorsRepo = _unitOfWork.GetRepository<Author>();

        try
        {
            var authors = await authorsRepo.GetAllAsync();
            return Ok(authors);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting authors");
            return BadRequest(ex);
        }
    }
}
