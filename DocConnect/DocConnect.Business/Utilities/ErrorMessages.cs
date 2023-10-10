namespace DocConnect.Business.Utilities
{
    public static class ErrorMessages
    {
        public const string NotFound = "Error. Not Found.";

        public const string CommonErrorMessage = "An error occurred. Please check your information and try again.";

        public const string RegistrationErrorCommonMessage = "An error occurred during registration. Please check your information and try again.";

        public const string LogInErrorCommonMessage = "An error occurred during login. Please check your information and try again.";

        public const string EmailAlreadyTaken = "This email is already registered by another user.";

        public const string InvalidEmailOrPassword = "Incorrect email or password. Please try again.";

        public const string ErrorOccurredAtRequest = "An error occured during executing {0}";

        public const string EmailVerificationNotSend = "An error occured during sending an email for verification.";

        public const string TokenIsExpiredOrNotValid = "INVALID_TOKEN";

        public const string EmailConfirmationFailed = "ERROR";

        public const string SendVerificationEmailError = "ERROR";

        public const string EmailWithTokenAlreadySent = "EMAIL_ALREADY_SENT";

        public const string AlreadyConfirmedEmail = "ALREADY_VERIFIED";

        public const string UserNotFound = "User not found.";

        public const string PasswordResetFailed = "INVALID_TOKEN_PASSWORD_RESET";

        public const string AppointmentHourOutOfRange = "The appointment hour should be between 9 and 16!";

        public const string DoctorAppointmentTaken = "The doctor already has this appointment hour taken!";

        public const string PatientAppointmentTaken = "The patient already has this appointment hour taken!";

        public const string AppointmentPastDate = "You can't book an appointment in the past.";

        public const string AppointmentLessThanDay = "Appointment time is less than 24 hours from now.";

        public const string AppointmentMoreThanOneMonth = "You can't book an appointment further than one month from now.";
    }
}
