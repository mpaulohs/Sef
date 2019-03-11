using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Sfe.UI.Web.ViewComponents.AdminViewComponent
{
    public class AdminMensagens : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            await Task.FromResult<object>(null);
            return View();
        }
    }
}
