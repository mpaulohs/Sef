using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Sfe.Domain.AggregatesModel.UserAggregate;
using System.Threading.Tasks;

namespace Sfe.UI.Web.ViewComponents.AdminViewComponent
{
    public class AdminMenuTopo : ViewComponent
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public AdminMenuTopo(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            
            if (_signInManager.IsSignedIn(HttpContext.User))
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                ViewBag.Email = user.Email;
                return View();
            }
            else
            {
                ViewBag.Email = "";
                return View();
            }
        }
    }
}
