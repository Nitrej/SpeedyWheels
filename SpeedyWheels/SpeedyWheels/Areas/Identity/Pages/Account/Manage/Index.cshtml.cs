// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SpeedyWheels.Models;

namespace SpeedyWheels.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RentalDataContext _rentalDataContext;
        public IndexModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            RentalDataContext rentalDataContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _rentalDataContext = rentalDataContext;
        }



        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            [DataType(DataType.Text)]
            [Display(Name = "FirsName")]
            public string FirstName { get; set; }

            [DataType(DataType.Text)]
            [Display(Name = "LastName")]
            public string LastName { get; set; }

            [DataType(DataType.Date)]
            [Display(Name = "BirthDate")]
            public string Adres { get; set; }

            [DataType(DataType.Text)]
            [Display(Name = "DriverLicense")]
            public string DriverLicenseNumber { get; set; }
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }
        }

        private async Task LoadAsync(User user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            var userId = await _userManager.GetUserIdAsync(user);

            //var cli = await _rentalDataContext.Find<Client>(c => c.UserId ==  userId);
            //var client = await _rentalDataContext.Clients.FindAsync(_rentalDataContext.Clients.Where(o => o.UserId == userId));
            //var cos = _rentalDataContext.Clients;
            var client = await _rentalDataContext.Clients.FindAsync(_rentalDataContext.Clients.SingleOrDefault(i => i.UserId == userId).Id);
            //var client = await _rentalDataContext.Clients.FirstOrDefaultAsync(c => c.User == user);

            Username = userName;

            Input = new InputModel {
                PhoneNumber = client.PhoneNumber,
                FirstName = client.Name,
                LastName = client.Surname,
                Adres = client.Address,
                DriverLicenseNumber = client.DriverLicenseNr
            };
            //Input.FirstName = client.Name; 
            //Input.LastName = client.Name;
            //Input.PhoneNumber = client.PhoneNumber;
            //Input.DriverLicenseNumber = client.DriverLicenseNr;
            //Input.Adres = client.Address;






            //var clien = await _rentalDataContext.Clients.FindAsync(_rentalDataContext.Clients.Where(o => o.UserId == userId));
        }
        
        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {

            var user = await _userManager.GetUserAsync(User);
            var userId = await _userManager.GetUserIdAsync(user);
            var client = await _rentalDataContext.Clients.FirstOrDefaultAsync(c => c.UserId == userId);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }
            client.Name = Input.FirstName;
            client.Surname = Input.LastName;
            client.Address = Input.Adres;
            client.DriverLicenseNr = Input.DriverLicenseNumber;
            client.PhoneNumber = Input.PhoneNumber;
            //var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            //if (Input.PhoneNumber != phoneNumber)
            //{
            //    var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
            //    if (!setPhoneResult.Succeeded)
            //    {
            //        StatusMessage = "Unexpected error when trying to set phone number.";
            //        return RedirectToPage();
            //    }
            //}

            await _signInManager.RefreshSignInAsync(user);

            _rentalDataContext.Clients.Update(client);
            await _rentalDataContext.SaveChangesAsync();
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }

    }
}
