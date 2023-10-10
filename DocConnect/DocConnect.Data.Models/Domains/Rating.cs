using DocConnect.Data.Models.Utilities.Constants;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocConnect.Data.Models.Domains;

public partial class Rating
{
    public int Id { get; set; }

    /// <summary>
    /// The doctor that is being rated
    /// </summary>
    public int DoctorId { get; set; }

    /// <summary>
    /// Navigation property for the doctor relationship
    /// </summary>
    [ForeignKey(nameof(DoctorId))]
    public Doctor Doctor { get; set; } = null!;

    /// <summary>
    /// The patient that is rating
    /// </summary>
    public int PatientId { get; set; }

    /// <summary>
    /// Navigation property for the patient relationship
    /// </summary>
    [ForeignKey(nameof(PatientId))]
    public virtual Patient Patient { get; set; } = null!;


    /// <summary>
    /// The rating value measured by the 1-5 scale
    /// </summary>
    public int RatingPoints { get; set; }

    /// <summary>
    /// The comment that the patient left for this doctor, between 3-500 characters
    /// </summary>
    [MaxLength(RatingConstants.CommentsMaxLength)]
    public string Comments { get; set; } = null!;

    /// <summary>
    /// Whether this record is deleted or not
    /// </summary>
    public bool IsDeleted { get; set; }
}
