using DocConnect.Data.Models.Domains;

namespace DocConnect.Business.Models.SpecialistDTOs
{
    public class SpecialistDetailedInfoDTO
    {
        public string ImageUrl { get; set; }
        public string SpecialityName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string DoctorSummary { get; set; }
        public double Rating { get; set; }
    }
}
