using Microsoft.AspNetCore.Mvc;
using StanzaBonanza.Services.Interfaces;

namespace StanzaBonanza.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Poem_AuthorsController : ControllerBase
    {
        private readonly ILogger<Poem_AuthorsController> _logger;
        private readonly IPoemAuthorJoinService _authorPoemJoinService;

        public Poem_AuthorsController(ILogger<Poem_AuthorsController> logger, IPoemAuthorJoinService authorPoemJoinService)
        {
            _logger = logger;
            _authorPoemJoinService = authorPoemJoinService ?? throw new ArgumentNullException(nameof(authorPoemJoinService));
        }

        [HttpGet]
        public async Task<IActionResult> GetPoem_AuthorsAsync()
        {
            try
            {
                var poemsAuthorsJoinResultSet = await _authorPoemJoinService.GetPoems_AuthorsJoinResultSet();
                return Ok(poemsAuthorsJoinResultSet);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                return BadRequest(ex);
            }
        }
    }
}
