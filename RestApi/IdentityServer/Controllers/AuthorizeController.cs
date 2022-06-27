using System.Threading.Tasks;
using DataAccessLayer.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.Controllers
{
    [Route("api/ids/")]
    [ApiController]
    public class AuthorizeController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AuthorizeController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost ("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationModel registrationModel)
        {
            var user = new IdentityUser(registrationModel.Name)
            {
                Email = registrationModel.Email
            };

            var result = await _userManager.CreateAsync(user, registrationModel.Password);

            if(result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return Ok();
            }

            return BadRequest();
        }

        [HttpPost ("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            var result = await _signInManager.PasswordSignInAsync(loginModel.Name, loginModel.Password, false, false);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(loginModel.Name);
                return Ok(user);
            }

            return BadRequest();
        }

        [HttpPost ("logout")]
        public IActionResult Logout()
        {
            _signInManager.SignOutAsync();
            return Ok();
        }

        [HttpGet("user")]
        public async Task<IActionResult> GetUser(string name)
        {
            var user = await _userManager.FindByNameAsync(name);
            return Ok(user);
        }
    }
}
