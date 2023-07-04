// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using SpeedyWheels.Models;

namespace SpeedyWheels.Areas.Identity.Pages.Account.Manage
{
    public class TwoFactorAuthenticationModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<TwoFactorAuthenticationModel> _logger;

        private readonly IUserStore<User> _userStore;
        private readonly IUserEmailStore<User> _emailStore;
        private readonly RentalDataContext _rentalDataContext;
        private readonly Client _clientContext;

        public TwoFactorAuthenticationModel(
            UserManager<User> userManager, SignInManager<User> signInManager, ILogger<TwoFactorAuthenticationModel> logger,
            IUserStore<User> userStore,
            RentalDataContext rentalDataContext
            )
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _rentalDataContext = rentalDataContext;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public bool HasAuthenticator { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public int RecoveryCodesLeft { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public bool Is2faEnabled { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        /// 
        public string ReturnUrl { get; set; }

        public bool IsMachineRemembered { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        public class InputModel 
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email*")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password*")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }


        }

        public async Task<IActionResult> OnGetAsync(string returnUrl = null)
        {
            //ReturnUrl = returnUrl;
            //ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            //HasAuthenticator = await _userManager.GetAuthenticatorKeyAsync(user) != null;
            //Is2faEnabled = await _userManager.GetTwoFactorEnabledAsync(user);
            //IsMachineRemembered = await _signInManager.IsTwoFactorClientRememberedAsync(user);
            //RecoveryCodesLeft = await _userManager.CountRecoveryCodesAsync(user);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            var newUser = CreateUser();
            var newClient = CreateClient();
            newUser.IsActive = true;

            newClient.Name = "EMPTY";
            newClient.Surname = "EMPTY"; 
            newClient.Address = "EMPTY"; 
            newClient.PhoneNumber = "EMPTY"; 
            newClient.DriverLicenseNr = "EMPTY"; 
            newClient.BirthDate = DateTime.Today.ToUniversalTime();
            newClient.IsActive = true;

            
            await _userStore.SetUserNameAsync(newUser, Input.Email, CancellationToken.None);
            await _emailStore.SetEmailAsync(newUser, Input.Email, CancellationToken.None);
            var result = await _userManager.CreateAsync(newUser, Input.Password);

            _rentalDataContext.Clients.Add(newClient);
            await _rentalDataContext.SaveChangesAsync();
            var cl = new System.Security.Claims.Claim("Operator", "true");
            await _userManager.AddClaimAsync(newUser, cl);

            await _signInManager.SignInAsync(newUser, isPersistent: false);

            var userId = await _userManager.GetUserIdAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            //await _signInManager.ForgetTwoFactorClientAsync();
            //StatusMessage = "The current browser has been forgotten. When you login again from this browser you will be prompted for your 2fa code.";
            return RedirectToPage();
        }

        private User CreateUser() {
            try {
                return Activator.CreateInstance<User>();
            }
            catch {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(User)}'. " +
                    $"Ensure that '{nameof(User)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }
        private Client CreateClient() {
            try {
                return Activator.CreateInstance<Client>();
            }
            catch {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(Client)}'. " +
                    $"Ensure that '{nameof(Client)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }
        private IUserEmailStore<User> GetEmailStore() {
            if (!_userManager.SupportsUserEmail) {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<User>)_userStore;
        }
    }
}
