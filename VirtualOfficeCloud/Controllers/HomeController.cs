using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VirtualOfficeCloud.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return Ok("This is OK");
            }
            else
            {
                return Ok("This is Wrong");
            }
            
        }
    }
}
