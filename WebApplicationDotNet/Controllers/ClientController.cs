using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApplicationDotNet.Controllers
{
    [Authorize(Roles = "user")]
    public class ClientController : Controller
    {
        public IActionResult dashboard()
        {
            return View();
        }
    }
}
