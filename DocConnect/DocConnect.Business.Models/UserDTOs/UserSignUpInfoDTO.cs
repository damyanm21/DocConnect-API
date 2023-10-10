using System.ComponentModel.DataAnnotations;
using static DocConnect.Data.Models.Utilities.Constants.ApplicationUserConstants;

namespace DocConnect.Business.Models.UserDTOs
{
    public class UserSignUpInfoDTO
    {
        [Required]
        [EmailAddress(ErrorMessage = InvalidEmail)]
        public string EmailAddress { get; set; }

        [Required]
        [StringLength(FirstNameMaxLength,
           MinimumLength = FirstNameMinLength,
           ErrorMessage = FirstNameErrorMessage)]
        [RegularExpression("^(?!\\s)(?!.*\\s{2,})(?:(?!\\s{2,})\\S{1,}\\s{0,1}){1,4}\\S{1,}$")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(LastNameMaxLength,
           MinimumLength = LastNameMinLength,
           ErrorMessage = LastNameErrorMessage)]
        [RegularExpression("^(?!\\s)(?!.*\\s{2,})(?:(?!\\s{2,})\\S{1,}\\s{0,1}){1,4}\\S{1,}$")]
        public string LastName { get; set; }

        [Required]
        [StringLength(PasswordMaxLength,
           MinimumLength = PasswordMinLength,
           ErrorMessage = PasswordTooShort)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!% *? &+~|_{}:;<>/])[A-Za-z\d$@$!%*?&+~|_{}:;<>/]{8,100}$", 
           ErrorMessage = PasswordIsWeak)]
        public string Password { get; set; }

        [Required]
        [Compare(nameof(Password), ErrorMessage = ConfirmPasswordDoesntMatch)]
        public string ConfirmPassword { get; set; }
    }
}
