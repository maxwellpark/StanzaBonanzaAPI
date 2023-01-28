using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StanzaBonanza.DataAccess.Repositories.Interfaces;
using StanzaBonanza.Models.ViewModels;

namespace StanzaBonanza.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Poem_AuthorsController : ControllerBase
    {
        private readonly ILogger<Poem_AuthorsController> _logger;
        private readonly IPoem_AuthorRepository _poem_authorRepository;

        public Poem_AuthorsController(ILogger<Poem_AuthorsController> logger, IPoem_AuthorRepository poem_authorRepository)
        {
            _logger = logger;
            _poem_authorRepository = poem_authorRepository ?? throw new ArgumentNullException(nameof(poem_authorRepository));
        }

        [HttpGet]
        public async Task<IActionResult> GetPoemsAsync()
        {
            try
            {
                var poem_authors = await _poem_authorRepository.GetAllAsync();
                return Ok(poem_authors);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                return BadRequest(ex);
            }
        }
    }
}
