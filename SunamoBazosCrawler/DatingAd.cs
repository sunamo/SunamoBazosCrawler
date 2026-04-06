// variables names: ok
namespace SunamoBazosCrawler;

/// <summary>
/// Represents a dating advertisement from Bazos website.
/// </summary>
public class DatingAd
{
    /// <summary>
    /// Description of the dating advertisement.
    /// </summary>
    public required string Description { get; set; }

    /// <summary>
    /// Location where the advertisement was posted.
    /// </summary>
    public required string Location { get; set; }

    /// <summary>
    /// Price mentioned in the advertisement.
    /// </summary>
    public required string Price { get; set; }

    /// <summary>
    /// Title of the dating advertisement.
    /// </summary>
    public required string Title { get; set; }
}
