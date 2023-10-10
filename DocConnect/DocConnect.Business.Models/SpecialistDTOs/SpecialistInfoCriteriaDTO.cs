using Microsoft.AspNetCore.Mvc;

namespace DocConnect.Business.Models.SpecialistDTOs
{
    [BindProperties]
    public class SpecialistInfoCriteriaDTO
    {
        public string DoctorName { get; set; }
        public int SpecialtyId { get; set; }
        public int CityId { get; set; }
    }
}
