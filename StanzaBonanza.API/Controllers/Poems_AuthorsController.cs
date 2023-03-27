using Microsoft.AspNetCore.Mvc;
using StanzaBonanza.Dtos.PoemDto;
using StanzaBonanza.Models.Models;
using StanzaBonanza.Models.ResultSets;
using StanzaBonanza.Services.Interfaces;

namespace StanzaBonanza.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Poems_AuthorsController : ControllerBase
    {
        private readonly ILogger<Poems_AuthorsController> _logger;
        private readonly IPoem_AuthorService _poemAuthorService;

        public Poems_AuthorsController(ILogger<Poems_AuthorsController> logger, IPoem_AuthorService poemAuthorService)
        {
            _logger = logger;
            _poemAuthorService = poemAuthorService ?? throw new ArgumentNullException(nameof(poemAuthorService));
        }

        [HttpGet]
        public async Task<ActionResult<Poems_AuthorsJoinResultSet>> GetPoems_AuthorsAsync()
        {
            try
            {
                var joinResultSet = await _poemAuthorService.GetPoems_AuthorsJoinResultSet();
                return joinResultSet.JoinResults.Any() ? Ok(joinResultSet) : NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                return Problem(ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Poem>> AddPoemAsync([FromBody] PoemDto poemDto)
        {
            try
            {
                var poem = await _poemAuthorService.AddPoemAsync(poemDto);
                //return CreatedAtAction(nameof(GetPoems_AuthorsAsync), new PoemDto(poem), poemDto);
                return Ok(poem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to add poem");
                return BadRequest();
            }
        }
    }
}
