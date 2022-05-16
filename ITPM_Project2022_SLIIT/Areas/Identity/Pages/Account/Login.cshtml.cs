using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using ITPM_Project2022_SLIIT.Areas.Identity.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using ITPM_Project2022_SLIIT.Controllers;
using ITPM_Project2022_SLIIT.Models;

namespace ITPM_Project2022_SLIIT.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly UserManager<ATMSUser> _userManager;
        private readonly SignInManager<ATMSUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;

        private ATMSDbContext _context;
        private string uName;

        public LoginModel(SignInManager<ATMSUser> signInManager, 
            ILogger<LoginModel> logger,
            UserManager<ATMSUser> userManager,
            ATMSDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _context = context;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required]
            [DataType(DataType.Text)]
            public string UserName { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl ??= Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            string returnUrlFLM = Url.Content("~/HFlightLists/Index");
            string returnUrlTM = Url.Content("~/CBookTickets/Index");
            string returnUrlFm = Url.Content("~/NFirstClassFoods/MainView");

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(Input.UserName, Input.Password, Input.RememberMe, lockoutOnFailure: false);
                uName = Input.UserName.ToString();
                if (result.Succeeded)
                {
                    //validation
                    AspNetUsersController aspNetUsersController = new AspNetUsersController(_context);
                    string data = aspNetUsersController.Details(uName);

                    if(data != null)
                    {
                        if(data == "hasini@gmail.com")
                        {
                            _logger.LogInformation("User logged in.");
                            return LocalRedirect(returnUrlFLM);
                        }else if (data == "chathuranga@gmail.com")
                        {
                            _logger.LogInformation("User logged in.");
                            return LocalRedirect(returnUrlTM);
                        }else if (data == "nadeeka@gmail.com")
                        {
                            _logger.LogInformation("User logged in.");
                            return LocalRedirect(returnUrlFm);
                        }
                        else
                        {
                            _logger.LogInformation("User logged in.");
                            return LocalRedirect(returnUrl);
                        }
                    }
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return Page();
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
