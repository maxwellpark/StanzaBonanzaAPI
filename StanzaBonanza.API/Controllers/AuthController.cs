using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using StanzaBonanza.Services.Interfaces;

namespace StanzaBonanza.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly ITokenService _tokenService;

        public AuthController(IConfiguration config, ITokenService tokenService)
        {
            _config = config;
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        }

        /// <summary>
        /// GET: /auth/login - initiates the OAuth flow by redirecting the user to the Google login page.
        /// </summary>
        /// <param name="returnUrl">The redirect URI where the user ends up after authenticating.</param>
        [HttpGet("login")]
        public IActionResult Login(string returnUrl = "/")
        {
            var properties = new AuthenticationProperties { RedirectUri = returnUrl };
            return Challenge(properties, "Google");
        }

        /// <summary>
        /// GET /auth/callback - handles the callback from the Google login page.
        /// </summary>
        [HttpGet("callback")]
        public async Task<IActionResult> Callback(string returnUrl = "/")
        {
            var result = await HttpContext.AuthenticateAsync();
            if (!result.Succeeded)
            {
                return BadRequest("Authentication failed");
            }

            var claims = result.Principal.Claims;
            // Use the claims to create a new user in your database or log in an existing user

            var token = _tokenService.GenerateToken(claims, _config["Jwt:Key"], _config["Jwt:Issuer"]);
            return Redirect($"{returnUrl}?token={token}");
        }
    }
}
