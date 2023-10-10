namespace DocConnect.Data.Models.Utilities.Constants
{
    /// <summary>
    /// Static class used for validating the ApplicationUser Entity.
    /// </summary>
    public static class ApplicationUserConstants
    {
        public const string InvalidEmail = "Please enter a valid email address.";

        public const int FirstNameMinLength = 1;
        public const int FirstNameMaxLength = 50;
        public const string FirstNameErrorMessage = "First name must be less than 50 characters long.";

        public const int LastNameMinLength = 1;
        public const int LastNameMaxLength = 50;
        public const string LastNameErrorMessage = "Last name must be less than 50 characters long.";

        public const int PasswordMinLength = 8;
        public const int PasswordMaxLength = 100;
        public const string PasswordTooShort = "Password must be at least 8 characters long.";
        public const string PasswordTooLong = "Password must be less than 100 characters long.";
        public const string PasswordIsWeak = "Your password must have at least 8 characters, with a mix of uppercase, lowercase, numbers and symbols";
        public const string ConfirmPasswordDoesntMatch = "Those passwords didn’t match.Please try again.";
        public const string NewPasswordSameAsOld = "The new password must be different from the old one.";

        public const string UserPasswordRegex = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!% *? &+~|_{}:;<>/])[A-Za-z\d$@$!%*?&+~|_{}:;<>/]{8,100}$";
    }
}

