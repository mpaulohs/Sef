using Microsoft.AspNetCore.Mvc;

namespace Sfe.UI.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }       
    }
}
