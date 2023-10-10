using System.ComponentModel.DataAnnotations.Schema;

namespace DocConnect.Data.Models.Domains;

public partial class Patient
{
    public int Id { get; set; }

    /// <summary>
    /// User Account associated with this patient
    /// </summary>
    public string UserId { get; set; }

    /// <summary>
    /// Navigation property for User relationship.
    /// </summary>
    [ForeignKey(nameof(UserId))]
    public ApplicationUser User { get; set; }

    /// <summary>
    /// Whether this record is deleted or not
    /// </summary>
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public bool IsDeleted { get; set; }

    /// <summary>
    /// Navigation property for the Ratings relationship
    /// </summary>
    public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();

    /// <summary>
    /// Navigational property for the appointemnts relationship
    /// </summary>
    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
