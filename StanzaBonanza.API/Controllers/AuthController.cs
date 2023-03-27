using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using StanzaBonanza.Services.Interfaces;

namespace StanzaBonanza.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;

        public AuthController(ITokenService tokenService)
        {
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        }

        /// <summary>
        /// GET: /auth/login - initiates the OAuth flow by redirecting the user to the Google login page.
        /// </summary>
        /// <param name="returnUrl">The redirect URI where the user ends up after authenticating.</param>
        [HttpGet("login")]
        public IActionResult Login(string returnUrl = "/")
        {
            // Set the return URL for successful authentication
            var properties = new AuthenticationProperties { RedirectUri = returnUrl };

            // Redirect to the Google login page
            return Challenge(properties, "Google");
        }

        /// <summary>
        /// GET /auth/callback - handles the callback from the Google login page.
        /// </summary>
        [HttpGet("callback")]
        public async Task<IActionResult> Callback(string returnUrl = "/")
        {
            // Authenticate the user with Google
            var result = await HttpContext.AuthenticateAsync();
            if (!result.Succeeded)
            {
                // Return a 400 Bad Request if authentication failed
                return BadRequest("Authentication failed");
            }

            // Extract the claims from the authenticated user's principal
            var claims = result.Principal.Claims;

            // Use the claims to create a new user in your database or log in an existing user
            // In this example, we'll simply return the claims to the client as JSON

            // Convert the claims to a dictionary for easier serialization
            var claimsDictionary = claims.ToDictionary(c => c.Type, c => c.Value);

            // Return the claims as JSON
            return Ok(claimsDictionary);
        }
    }
}
