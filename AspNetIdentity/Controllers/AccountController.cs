using AspNetIdentity.Services;
using AspNetIdentity.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AspNetIdentity.Controllers
{

    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly ILogger<AccountController> _logger;
        public AccountController(IAccountService accountService, ILogger<AccountController> logger)
        {
            _accountService = accountService;
            _logger = logger;
        }
        // GET: /Account/Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        // POST: /Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(model);
                var result = await _accountService.RegisterUserAsync(model);
                if (result.Succeeded)
                    return RedirectToAction("RegistrationConfirmation");
                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during registration for email: {Email}", model.Email);
                ModelState.AddModelError("", "An unexpected error occurred. Please try again later.");
                return View(model);
            }
        }
        // GET: /Account/RegistrationConfirmation
        [HttpGet]
        public IActionResult RegistrationConfirmation()
        {
            return View();
        }
        // GET: /Account/ConfirmEmail
        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(Guid userId, string token)
        {
            try
            {
                if (userId == Guid.Empty || string.IsNullOrEmpty(token))
                    return BadRequest("Invalid email confirmation request.");
                var result = await _accountService.ConfirmEmailAsync(userId, token);
                if (result.Succeeded)
                    return View("EmailConfirmed");
                // Combine errors into one message or pass errors to the view
                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);
                return View("Error");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error confirming email for UserId: {UserId}", userId);
                ModelState.AddModelError("", "An unexpected error occurred during email confirmation.");
                return View("Error");
            }
        }
        // GET: /Account/Login
        [HttpGet]
        public IActionResult Login(string? ReturnUrl = null)
        {
            LoginViewModel model = new LoginViewModel()
            {
                ReturnURL = ReturnUrl
            };
            //ViewData["ReturnUrl"] = ReturnUrl;
            return View(model);
        }
        // POST: /Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(model);

                var result = await _accountService.LoginUserAsync(model);

                if (result.Succeeded)
                {
                    // Redirect back to original page if ReturnUrl exists and is local
                    if (!string.IsNullOrEmpty(model.ReturnURL) && Url.IsLocalUrl(model.ReturnURL))
                        return Redirect(model.ReturnURL);

                    // Otherwise, redirect to a default page (like user profile)
                    return RedirectToAction("Profile", "Account");
                }

                // Handle login failure (e.g., invalid credentials or unconfirmed email)
                if (result.IsNotAllowed)
                    ModelState.AddModelError("", "Email is not confirmed yet.");
                else
                    ModelState.AddModelError("", "Invalid login attempt.");

                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during login for email: {Email}", model.Email);
                ModelState.AddModelError("", "An unexpected error occurred. Please try again later.");
                return View(model);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            if (string.IsNullOrEmpty(email))
                return RedirectToAction("Login", "Account");
            try
            {
                var model = await _accountService.GetUserProfileByEmailAsync(email);
                return View(model);
            }
            catch (ArgumentException)
            {
                return View("Error");
            }
        }
        // POST: /Account/Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await _accountService.LogoutUserAsync();
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during logout");
                // Optionally redirect to error page or home with message
                return RedirectToAction("Index", "Home");
            }
        }
        // GET: /Account/ResendEmailConfirmation
        [HttpGet]
        public IActionResult ResendEmailConfirmation()
        {
            return View();
        }
        // POST: /Account/ResendEmailConfirmation
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResendEmailConfirmation(ResendConfirmationEmailViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(model);
                await _accountService.SendEmailConfirmationAsync(model.Email);
                ViewBag.Message = "If the email is registered, a confirmation link has been sent.";
                return View("ResendEmailConfirmationSuccess");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending email confirmation to: {Email}", model.Email);
                ModelState.AddModelError("", "An unexpected error occurred. Please try again later.");
                return View(model);
            }
        }
    }
}

