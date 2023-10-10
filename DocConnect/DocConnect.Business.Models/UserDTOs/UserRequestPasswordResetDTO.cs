using System.ComponentModel.DataAnnotations;
using static DocConnect.Data.Models.Utilities.Constants.ApplicationUserConstants;

namespace DocConnect.Business.Models.UserDTOs
{
    public class UserRequestPasswordResetDTO
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(PasswordMaxLength,
           MinimumLength = PasswordMinLength,
           ErrorMessage = PasswordTooShort)]
        [RegularExpression(UserPasswordRegex,
           ErrorMessage = PasswordIsWeak)]
        public string NewPassword { get; set; }

        [Compare(nameof(NewPassword), ErrorMessage = ConfirmPasswordDoesntMatch)]
        public string RepeatPassword { get; set; }
    }
}
