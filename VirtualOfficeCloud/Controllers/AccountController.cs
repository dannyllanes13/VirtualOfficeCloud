using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VirtualOfficeCloud.Data.Models;
using VirtualOfficeCloud.Dto.User;
using VirtualOfficeCloud.Utils.Interfaces;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VirtualOfficeCloud.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IStoreUserService _userService;
        private readonly IToken _token;

        public AccountController(ILogger<AccountController> logger, IStoreUserService userService, IToken token)
        {
            _logger = logger;
            _userService = userService;
            _token = token;
        }

        //this is to log in user and create tokens
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateToken([FromBody]LoginDto model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userService.FindByNameAsync(model.Username);
                if (user != null)
                {
                    var result = await _userService.SignInAsyncByPassword(user, model.Password, false);
                    if (result)
                    {
                        var results = _token.CreateToken(user.UserName, user.Email);                       
                        return Ok(results);
                    }
                }
            }

            return BadRequest("Please check login values");
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _userService.SignOutAsync();
            return Ok("Log out");
        }

    }
}
