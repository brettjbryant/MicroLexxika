using Microsoft.AspNetCore.Mvc;
using MicroLexxika.Api.Models;
using MicroLexxika.Api.Services.Interfaces;
using MicroLexxika.Domain.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace MicroLexxika.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthenticationController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        private readonly HashingHelper _hashingHelper;
        private readonly TokenHelper _tokenHelper;

        public AuthenticationController(IConfiguration configuration, IUserService userService, HashingHelper hashingHelper, TokenHelper tokenHelper)
        {
            _configuration = configuration;
            _userService = userService;
            _hashingHelper = hashingHelper;
            _tokenHelper = tokenHelper;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("register")]
        public IActionResult Register()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public IActionResult Login(LoginRequest request)
        {
            var users = _userService.GetAll();
            var user = users.FirstOrDefault(x => x.Username == request.Username);

            if (user != null)
            {
                if (_hashingHelper.VerifyPassword(request.Password!, user.Password!))
                {
                    var secret = _configuration["Auth:Secret"]!;
                    var expiration = _configuration.GetValue<int>("Auth:Expiry");
                    var token = _tokenHelper.CreateToken(
                        user.Username, 
                        user.Role.ToString(),
                        secret,
                        expiration);

                    return Ok(new LoginResult
                    {
                        Token = token.Value,
                        UserId = user.Id.ToString(),
                        Expiry = token.Expires
                    });
                }

                return Unauthorized();
            }

            return NoContent();
        }
    }
}
