using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PROJECT_CLIENT.Controllers
{
    public class ManageByAuthorController : Controller
    {
        [Authorize(Roles = "AUTHOR")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
