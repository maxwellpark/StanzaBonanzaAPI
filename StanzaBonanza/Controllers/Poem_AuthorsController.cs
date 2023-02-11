using Microsoft.AspNetCore.Mvc;
using StanzaBonanza.Services.Interfaces;

namespace StanzaBonanza.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Poem_AuthorsController : ControllerBase
    {
        private readonly ILogger<Poem_AuthorsController> _logger;
        private readonly IAuthorPoemJoinService _authorPoemJoinService;

        public Poem_AuthorsController(ILogger<Poem_AuthorsController> logger, IAuthorPoemJoinService authorPoemJoinService)
        {
            _logger = logger;
            _authorPoemJoinService = authorPoemJoinService ?? throw new ArgumentNullException(nameof(authorPoemJoinService));
        }

        [HttpGet]
        public async Task<IActionResult> GetPoemsAsync()
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
