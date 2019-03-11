using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sfe.Application.ViewModels.ControllersGperfil;
using Sfe.Domain.AggregatesModel.UserAggregate;
using Sfe.UI.Web.Controllers;

namespace Sfe.UI.Web._2_ControllersGperfil
{
        [Authorize]
        public class GperfilAccountController : Controller
        {
            private readonly UserManager<User> _userManager;
            private readonly SignInManager<User> _signInManager;
            // private readonly IEmailSender _emailSender;
            private readonly ILogger _logger;

            public GperfilAccountController(
                UserManager<User> userManager,
                SignInManager<User> signInManager,
                //  IEmailSender emailSender,
                ILoggerFactory loggerFactory)
            {
                _userManager = userManager;
                _signInManager = signInManager;
                // _emailSender = emailSender;
                _logger = loggerFactory.CreateLogger<GperfilAccountController>();
            }

            //
            // GET: /Account/Login
            [HttpGet]
            [AllowAnonymous]
            [Route("login")]
            public async Task<IActionResult> Login(string returnUrl = null)
            {
                // Clear the existing external cookie to ensure a clean login process
                // await HttpContext.Authentication.SignOutAsync(_externalCookieScheme);
                await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
                ViewData["ReturnUrl"] = returnUrl;
                return View();
            }

            //
            // POST: /Account/Login
            [HttpPost]
            [AllowAnonymous]
            [ValidateAntiForgeryToken]
            [Route("login")]
            public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
            {
                ViewData["ReturnUrl"] = returnUrl;
                if (ModelState.IsValid)
                {
                    // This doesn't count login failures towards account lockout
                    // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                    var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: true);
                    if (result.Succeeded)
                    {
                        _logger.LogInformation(1, "User logged in.");                       
                        return RedirectToLocal(returnUrl);
                    }
                    if (result.IsLockedOut)
                    {
                        _logger.LogWarning(2, "User account locked out.");
                        return View("Lockout");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                        return View(model);
                    }
                }

                // If we got this far, something failed, redisplay form
                return View(model);
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Logout()
            {
                await _signInManager.SignOutAsync();
                _logger.LogInformation(4, "User logged out.");
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            #region Helpers

            private void AddErrors(IdentityResult result)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            private IActionResult RedirectToLocal(string returnUrl)
            {
                if (Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                 return RedirectToAction("Index", "Admin");
                }
            }

            #endregion
        }
    
}