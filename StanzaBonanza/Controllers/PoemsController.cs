using Microsoft.AspNetCore.Mvc;
using StanzaBonanza.DataAccess.UnitOfWork;
using StanzaBonanza.Models.Models;
using StanzaBonanza.Models.ViewModels;

namespace StanzaBonanza.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PoemsController : ControllerBase
{
    private readonly ILogger<PoemsController> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public PoemsController(ILogger<PoemsController> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    [HttpGet]
    [Route("{id?}")]
    public async Task<IActionResult> GetPoemByIdAsync(int id)
    {
        try
        {
            _logger.LogInformation("Request received for poem by ID " + id);

            var poemsRepo = _unitOfWork.GetRepository<Poem>();
            var poem = await poemsRepo.GetByIdAsync(id).ConfigureAwait(false);
            return poem != null ? Ok(new PoemViewModel(poem)) : NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting poem by ID " + id);
            return Problem(ex.Message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetPoemsAsync()
    {
        try
        {
            _logger.LogInformation("Request received for poems");

            var poemsRepo = _unitOfWork.GetRepository<Poem>();
            var poems = await poemsRepo.GetAllAsync().ConfigureAwait(false);
            return poems.Any() ? Ok(new PoemsViewModel(poems)) : NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting poems");
            return Problem(ex.Message);
        }
    }
}
