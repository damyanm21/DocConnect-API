namespace DocConnect.Data.Models.Utilities.Constants
{
    public static class DoctorConstants
    {
        public const string DoctorFullName = "{0} {1}";

        public const int FirstNameMaxLength = 80;

        public const int LastNameMaxLength = 80;

        public const int ImageUrlMaxLength = 2048;

        public const int AddressMaxLength = 100;

        public const int FirstAppointmentHour = 9;
        public const int LastAppointmentHour = 16;
        public const string SpecialistNotFound = "There's no specialist corresponding to this id.";

        public const string ImageUrlBuilder = "{0}/{1}";
        public const string FullAddressBuilder = "{0}, {1}";

    }
}
