namespace BoostUp.Areas.Identity.Pages.Account.Manage
{
    using System;
    using System.Threading.Tasks;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    using Common;
    using BoostUp.Data.Models;

    using static BoostUp.Common.DataConstants.User;
    using static BoostUp.Common.DataConstants.Address;

    public partial class IndexModel : PageModel
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public IndexModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Display(Name = "First name")]
            [Required(ErrorMessage = "{0} is required.")]
            [StringLength(NameMaxLength, MinimumLength = NameMinLength, ErrorMessage = "{0} must be with a minimum length of {2} and a maximum length of {1}.")]
            public string FirstName { get; set; }

            [Display(Name = "Last name")]
            [StringLength(NameMaxLength, MinimumLength = NameMinLength, ErrorMessage = "{0} must be with a minimum length of {2} and a maximum length of {1}.")]
            public string LastName { get; set; }

            public GenderType Gender { get; set; }

            [Required(ErrorMessage = "{0} is required.")]
            [RegularExpression(@"[a-z A-Z]+", ErrorMessage = "{0} should not contains any digits.")]
            [StringLength(CountryMaxLength, MinimumLength = CountryMinLength, ErrorMessage = "{0} must be with a minimum length of {2} and a maximum length of {1}.")]
            public string Country { get; set; }

            [Required(ErrorMessage = "{0} is required.")]
            [RegularExpression(@"[a-z A-Z]+", ErrorMessage = "{0} should not contains any digits.")]
            [StringLength(CityMaxLength, MinimumLength = CityMinLength, ErrorMessage = "{0} must be with a minimum length of {2} and a maximum length of {1}.")]
            public string City { get; set; }

            [Display(Name = "Date of birth")]
            public DateTime? DateOfBirth { get; set; }

            [Display(Name = "About")]
            public string About { get; set; }

            [Display(Name = "Education")]
            public string Education { get; set; }

            [Display(Name = "Experience")]
            public string Experience { get; set; }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            if (user == null)
            {
                return this.NotFound($"Unable to load user with ID '{this.userManager.GetUserId(this.User)}'.");
            }
            // await this.LoadAsync(user);
            return this.Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await this.userManager.GetUserAsync(User);

            if (user == null)
            {
                return this.NotFound($"Unable to load user with ID '{this.userManager.GetUserId(this.User)}'.");
            }

            user.LastName = this.Input.LastName;
            user.FirstName = this.Input.FirstName;
            user.Gender = this.Input.Gender;
            user.DateOfBirth = this.Input.DateOfBirth;
            user.About = this.Input.About;
            user.Education = this.Input.Education;
            user.Experience = this.Input.Experience;

            await userManager.UpdateAsync(user);

            if (!this.ModelState.IsValid)
            {
                // await this.LoadAsync(user);
                return this.Page();
            }

            await this.signInManager.RefreshSignInAsync(user);
            this.StatusMessage = "Your profile has been updated";
            return Page();
        }
    }
}
