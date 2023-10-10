using DocConnect.Data.Models.Utilities.Constants;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocConnect.Data.Models.Domains;

public partial class Doctor
{
    public int Id { get; set; }

    /// <summary>
    /// User Account associated with this doctor
    /// </summary>
    [ForeignKey(nameof(UserId))]
    public string UserId { get; set; } = null!;

    /// <summary>
    /// The specialty of this doctor
    /// </summary>
    public int SpecialtyId { get; set; }

    /// <summary>
    /// Navigational property for the speciality relationship
    /// </summary>
    [ForeignKey(nameof(SpecialtyId))]
    public Specialty Specialty { get; set; }

    /// <summary>
    /// The doctors first name
    /// </summary>
    [MaxLength(DoctorConstants.FirstNameMaxLength)]
    public string FirstName { get; set; }

    /// <summary>
    /// The doctors last name
    /// </summary>
    [MaxLength(DoctorConstants.LastNameMaxLength)]
    public string LastName { get; set; }

    /// <summary>
    /// The url to the image of the doctor
    /// </summary>
    [MaxLength(DoctorConstants.ImageUrlMaxLength)]
    public string ImageUrl { get; set; }

    /// <summary>
    /// The location of this doctor
    /// </summary>
    public int LocationId { get; set; }

    /// <summary>
    /// Navigation property for the Location relationship
    /// </summary>
    [ForeignKey(nameof(LocationId))]
    public Location Location { get; set; }

    /// <summary>
    /// Years of experience in numbers
    /// </summary>
    public int? YearsOfExperience { get; set; }

    /// <summary>
    /// Includes info as: past employment, certifications, etc
    /// </summary>
    public string? DoctorSummary { get; set; }

    /// <summary>
    /// Contains info regarding all the education that doctor has
    /// </summary>
    public string? EducationSummary { get; set; }

    /// <summary>
    /// The address where the doctor is located
    /// </summary>
    [MaxLength(DoctorConstants.AddressMaxLength)]
    public string Address { get; set; }

    /// <summary>
    /// Whether this record is deleted or not
    /// </summary>
    public bool IsDeleted { get; set; }

    /// <summary>
    /// Navigational property for all doctor ratings
    /// </summary>
    public ICollection<Rating> Ratings { get; set; } = new List<Rating>();

    /// <summary>
    /// Navigational property for all doctor appointments
    /// </summary>
    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
