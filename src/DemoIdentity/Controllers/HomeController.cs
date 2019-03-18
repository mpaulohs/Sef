using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DemoIdentity.Models;

namespace DemoIdentity.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            string callbackUrl = Url.Action("Index", "Home", new { id = 17, code= 92929 });

            ViewBag.Url = callbackUrl;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
