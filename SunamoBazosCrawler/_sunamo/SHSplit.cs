namespace SunamoBazosCrawler._sunamo;

/// <summary>
/// EN: String Helper for splitting operations
/// CZ: String Helper pro operace rozdělování řetězců
/// </summary>
internal class SHSplit
{
    /// <summary>
    /// EN: Splits a string by the specified delimiters and removes empty entries
    /// CZ: Rozdělí řetězec podle zadaných oddělovačů a odstraní prázdné položky
    /// </summary>
    /// <param name="text">EN: Text to split / CZ: Text k rozdělení</param>
    /// <param name="delimiters">EN: Delimiters to split by / CZ: Oddělovače podle kterých rozdělit</param>
    /// <returns>EN: List of split parts without empty entries / CZ: Seznam rozdělených částí bez prázdných položek</returns>
    internal static List<string> Split(string text, params string[] delimiters)
    {
        return text.Split(delimiters, StringSplitOptions.RemoveEmptyEntries).ToList();
    }
}