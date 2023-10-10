using System.ComponentModel.DataAnnotations;
using static DocConnect.Data.Models.Utilities.Constants.ApplicationUserConstants;

namespace DocConnect.Business.Models.UserDTOs
{
    public class UserEmailDTO
    {
        [Required]
        [EmailAddress(ErrorMessage = InvalidEmail)]
        public string EmailAddress { get; set; }
    }
}
