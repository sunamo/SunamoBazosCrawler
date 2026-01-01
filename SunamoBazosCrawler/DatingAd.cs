namespace SunamoBazosCrawler;

/// <summary>
/// EN: Represents a dating advertisement from Bazos website
/// CZ: Reprezentuje seznamovací inzerát z webu Bazos
/// </summary>
public class DatingAd
{
    /// <summary>
    /// EN: Description of the dating advertisement
    /// CZ: Popis seznamovacího inzerátu
    /// </summary>
    public required string Description { get; set; }

    /// <summary>
    /// EN: Location where the advertisement was posted
    /// CZ: Lokalita kde byl inzerát zveřejněn
    /// </summary>
    public required string Location { get; set; }

    /// <summary>
    /// EN: Price mentioned in the advertisement
    /// CZ: Cena uvedená v inzerátu
    /// </summary>
    public required string Price { get; set; }

    /// <summary>
    /// EN: Title of the dating advertisement
    /// CZ: Nadpis seznamovacího inzerátu
    /// </summary>
    public required string Title { get; set; }
}