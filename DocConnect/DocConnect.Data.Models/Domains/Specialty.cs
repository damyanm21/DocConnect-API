using DocConnect.Data.Models.Utilities.Constants;
using System.ComponentModel.DataAnnotations;

namespace DocConnect.Data.Models.Domains
{
    /// <summary>
    /// Entity representing the a record in database table Specialties..
    /// </summary>
    public class Specialty
    {
        /// <summary>
        /// Identificator.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Image Url of the Specialty.
        /// </summary>
        [Required]
        [StringLength(SpecialtyConstants.ImageUrlMaxLength,
         MinimumLength = SpecialtyConstants.ImageUrlMinLength)]
        public string ImageUrl { get; set; } = null!;

        /// <summary>
        /// Name of the Specialty.
        /// </summary>
        [Required]
        [StringLength(SpecialtyConstants.NameMaxLength,
         MinimumLength = SpecialtyConstants.NameMinLength)]
        public string Name { get; set; } = null!;

        /// <summary>
        /// Delete flag used for filtering.
        /// </summary>
        [Required]
        public bool IsDeleted { get; set; }
    }
}

