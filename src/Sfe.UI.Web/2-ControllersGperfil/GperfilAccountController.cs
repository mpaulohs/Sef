using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sfe.Application.ViewModels.ControllersGperfil;
using Sfe.Domain.AggregatesModel.MessageSenderAggregate;
using Sfe.Domain.AggregatesModel.UserAggregate;
using Sfe.UI.Web.Controllers;

namespace Sfe.UI.Web._2_ControllersGperfil
{
    [Authorize]
    public class GperfilAccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger _logger;
        private readonly IEmailSenderRepository _emailSender;

        public GperfilAccountController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IEmailSenderRepository emailSender,
            ILoggerFactory loggerFactory)
        {
            _userManager = userManager;
            _signInManager = signInManager;
             _emailSender = emailSender;
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

        // GET: /Account/Register
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {         

            if (ModelState.IsValid)
            {
                var user = new User { UserName = model.Email, Email = model.Email, Name = model.Name  };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Action("ConfirmEmail", "GperfilAccount", new { userId = user.Id, code = token }, protocol: HttpContext.Request.Scheme);
                    await _emailSender.SendEmailAsync(model.Email, "Confirm your account",
                        "Please confirm your account by clicking this link: <a href=\"" + callbackUrl + "\">link</a>");

                    //var code_ = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    //var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code_ }, protocol: HttpContext.Request.Scheme);
                    //var body = ReturnBody(model.Email, model.Password, callbackUrl);
                    //await _emailSender.SendEmailAsync(model.Email, "Confirme sua conta. [Gazeta do Tocantins])", body);
                    //_logger.LogInformation(3, "User created a new account with password.");
                    // return RedirectToLocal(returnUrl);
                    return View("DisplayEmail");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }


        // GET: /Account/ConfirmEmail
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return View("Error");
            }
            var result = await _userManager.ConfirmEmailAsync(user, code);

            if (result.Succeeded)
            {
                ViewBag.Email = user.Email;
                return View("ConfirmEmail");
            }
            return View("Error");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult DisplayEmail()
        {
            return View();
        }

        [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Logout()
            {
                await _signInManager.SignOutAsync();
                _logger.LogInformation(4, "User logged out.");
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }
        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                //For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=532713
                //Send an email with this link
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var callbackUrl = Url.Action(nameof(ResetPassword), "GperfilAccount", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
                await _emailSender.SendEmailAsync(model.Email, "Reset Password",
                   $"Por favor, redefinar sua senha clicando aqui:: <a href='{callbackUrl}'>link</a>");
                return View("ForgotPasswordConfirmation");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }


        // GET: /Account/ResetPassword
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string code = null)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction(nameof(GperfilAccountController.ResetPasswordConfirmation), "GperfilAccount");
            }
            var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(GperfilAccountController.ResetPasswordConfirmation), "GperfilAccount");
            }
            AddErrors(result);
            return View();
        }


        //
        // GET: /Account/ResetPasswordConfirmation
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        #region Helpers

        private string ReturnBody(string login, string senha, string callbackUrl)
        {
            string body = "<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.0 Transitional//EN\">";
            body += "<HTML><HEAD><META http-equiv=Content-Type content=\"text/html; charset=iso-8859-1\">";
            body += "</HEAD><BODY><DIV>Por favor confirme sua conta clicando neste link: <A href=\"" + callbackUrl + "\">link";
            body += "</A></DIV><BR/>Login: " + login + " <BR/>Senha: " + senha + "</BODY></HTML>";

            return body;

        }
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