// variables names: ok
namespace SunamoBazosCrawler._sunamo;

/// <summary>
/// EN: String Helper - utility class for string operations
/// CZ: String Helper - pomocná třída pro operace s řetězci
/// </summary>
internal class SH
{
    /// <summary>
    /// EN: Matches a string against a wildcard pattern using ? and * wildcards
    /// CZ: Porovná řetězec s wildcard vzorem používajícím ? a * wildcardy
    /// </summary>
    /// <param name="name">EN: String to match / CZ: Řetězec k porovnání</param>
    /// <param name="mask">EN: Wildcard pattern to match against / CZ: Wildcard vzor k porovnání</param>
    /// <returns>EN: True if string matches the pattern, false otherwise / CZ: True pokud řetězec odpovídá vzoru, jinak false</returns>
    internal static bool MatchWildcard(string name, string mask)
    {
        return IsMatchRegex(name, mask, '?', '*');
    }

    /// <summary>
    /// EN: Checks if input matches a regex pattern with custom wildcard characters
    /// CZ: Zkontroluje zda vstup odpovídá regex vzoru s vlastními wildcard znaky
    /// </summary>
    /// <param name="input">EN: Input string to match / CZ: Vstupní řetězec k porovnání</param>
    /// <param name="pattern">EN: Pattern with wildcards / CZ: Vzor s wildcardy</param>
    /// <param name="singleWildcard">EN: Character for single character wildcard (typically '?') / CZ: Znak pro wildcard jednoho znaku (typicky '?')</param>
    /// <param name="multipleWildcard">EN: Character for multiple character wildcard (typically '*') / CZ: Znak pro wildcard více znaků (typicky '*')</param>
    /// <returns>EN: True if input matches the pattern, false otherwise / CZ: True pokud vstup odpovídá vzoru, jinak false</returns>
    private static bool IsMatchRegex(string input, string pattern, char singleWildcard, char multipleWildcard)
    {
        // If I compared .vs with .vs, return false before
        if (input == pattern) return true;
        var escapedSingle = Regex.Escape(new string(singleWildcard, 1));
        var escapedMultiple = Regex.Escape(new string(multipleWildcard, 1));
        pattern = Regex.Escape(pattern);
        pattern = pattern.Replace(escapedSingle, ".");
        pattern = "^" + pattern.Replace(escapedMultiple, ".*") + "$";
        var regex = new Regex(pattern);
        return regex.IsMatch(input);
    }
}