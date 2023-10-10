using DocConnect.Data.Models.Utilities.Constants;
using System.ComponentModel.DataAnnotations;

namespace DocConnect.Data.Models.Domains;

public partial class Country
{
    /// <summary>
    /// ISO 3166 three-digit country number
    /// </summary>
    [StringLength(CountryConstants.CountryIdLength,
    MinimumLength = CountryConstants.CountryIdLength)]
    public int Id { get; set; }

    /// <summary>
    /// The name of the country
    /// </summary>
    [MaxLength(CountryConstants.CountryNameLength)]
    public string CountryName { get; set; } = null!;

    /// <summary>
    /// ISO 3166 two-letter country codes
    /// </summary>
    [StringLength(CountryConstants.Iso2NameLength,
    MinimumLength = CountryConstants.Iso2NameLength)]
    public string Iso2Name { get; set; } = null!;

    /// <summary>
    /// ISO 3166 three-letter country codes
    /// </summary>
    [StringLength(CountryConstants.Iso3NameLength,
    MinimumLength = CountryConstants.Iso3NameLength)]
    public string Iso3Name { get; set; } = null!;

    /// <summary>
    /// Whether this record is deleted or not
    /// </summary>
    public bool IsDeleted { get; set; }

    /// <summary>
    /// Navigation property for the locations relationship
    /// </summary>
    public virtual ICollection<Location> Locations { get; set; } = new List<Location>();
}
