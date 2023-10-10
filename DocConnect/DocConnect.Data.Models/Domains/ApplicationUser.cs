using System.ComponentModel.DataAnnotations;
using DocConnect.Data.Models.Utilities.Constants;
using Microsoft.AspNetCore.Identity;

namespace DocConnect.Data.Models.Domains
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(ApplicationUserConstants.FirstNameMaxLength,
            MinimumLength = ApplicationUserConstants.FirstNameMinLength,
            ErrorMessage = ApplicationUserConstants.FirstNameErrorMessage)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(ApplicationUserConstants.LastNameMaxLength,
        MinimumLength = ApplicationUserConstants.LastNameMinLength,
        ErrorMessage = ApplicationUserConstants.LastNameErrorMessage)]
        public string LastName { get; set; }
    }
}

