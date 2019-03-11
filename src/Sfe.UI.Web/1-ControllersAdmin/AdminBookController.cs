using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Sfe.UI.Web._1_ControllersAdmin
{
    public class AdminBookController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}