using Microsoft.AspNetCore.Mvc;
using StanzaBonanza.Models.ResultSets;
using StanzaBonanza.Services.Interfaces;

namespace StanzaBonanza.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Poems_AuthorsController : ControllerBase
    {
        private readonly ILogger<Poems_AuthorsController> _logger;
        private readonly IPoemAuthorJoinService _poemAuthorJoinService;

        public Poems_AuthorsController(ILogger<Poems_AuthorsController> logger, IPoemAuthorJoinService poemAuthorJoinService)
        {
            _logger = logger;
            _poemAuthorJoinService = poemAuthorJoinService ?? throw new ArgumentNullException(nameof(poemAuthorJoinService));
        }

        [HttpGet]
        public async Task<ActionResult<Poems_AuthorsJoinResultSet>> GetPoems_AuthorsAsync()
        {
            try
            {
                var joinResultSet = await _poemAuthorJoinService.GetPoems_AuthorsJoinResultSet();
                return joinResultSet.JoinResults.Any() ? Ok(joinResultSet) : NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                return Problem(ex.Message);
            }
        }
    }
}
