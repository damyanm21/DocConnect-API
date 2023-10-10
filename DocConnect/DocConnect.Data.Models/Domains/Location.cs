using DocConnect.Data.Models.Utilities.Constants;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocConnect.Data.Models.Domains;

public partial class Location
{
    public int Id { get; set; }

    /// <summary>
    /// ISO 3166 three-digit country number
    /// </summary>
    public int CountryId { get; set; }

    /// <summary>
    /// Navigation property for the country relationship
    /// </summary>
    [ForeignKey(nameof(CountryId))]
    public virtual Country Country { get; set; } = null!;

    /// <summary>
    /// The name of the city/town as a Unicode string (e.g. Goiânia)
    /// </summary>
    [MaxLength(LocationConstants.CityNameMaxLength)]
    public string CityName { get; set; } = null!;

    /// <summary>
    /// ASCII string of the city/town (e.g. Goiania)
    /// </summary>
    [MaxLength(LocationConstants.CityAsciiMaxLength)]
    public string CityAscii { get; set; } = null!;

    /// <summary>
    /// The latitude of the city/town
    /// </summary>
    [DataType(LocationConstants.LatitudeDataTypeAndPrecision)]
    public decimal Latitude { get; set; }

    /// <summary>
    /// The longitude of the city/town
    /// </summary>
    [DataType(LocationConstants.LongitudeDataTypeAndPrecision)]
    public decimal Longitude { get; set; }

    /// <summary>
    /// Whether this record is deleted or not
    /// </summary>
    public bool IsDeleted { get; set; }

    /// <summary>
    /// Navigation property for the doctors relationship
    /// </summary>
    public virtual ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();
}
