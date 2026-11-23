namespace SunamoBazosCrawler;

/// <summary>
/// Represents the dating ad type.
/// </summary>
public class DatingAd
{
    /// <summary>
    /// Gets or sets the description.
    /// </summary>
    public required string Description { get; set; }
    /// <summary>
    /// Gets or sets the location.
    /// </summary>
    public required string Location { get; set; }
    /// <summary>
    /// Gets or sets the price.
    /// </summary>
    public required string Price { get; set; }
    /// <summary>
    /// Gets or sets the title.
    /// </summary>
    public required string Title { get; set; }
}
