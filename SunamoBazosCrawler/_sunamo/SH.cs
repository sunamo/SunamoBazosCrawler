namespace SunamoBazosCrawler._sunamo;

/// <summary>
/// String helper - utility class for string operations.
/// </summary>
internal class SH
{
    /// <summary>
    /// Matches a string against a wildcard pattern using ? and * wildcards.
    /// </summary>
    /// <param name="text">String to match.</param>
    /// <param name="pattern">Wildcard pattern to match against.</param>
    /// <returns>True if string matches the pattern, false otherwise.</returns>
    internal static bool MatchWildcard(string text, string pattern)
    {
        return isMatchRegex(text, pattern, '?', '*');
    }

    /// <summary>
    /// Checks if input matches a regex pattern with custom wildcard characters.
    /// </summary>
    /// <param name="text">Text string to match against the pattern.</param>
    /// <param name="pattern">Pattern with wildcards.</param>
    /// <param name="singleWildcard">Character for single character wildcard (typically '?').</param>
    /// <param name="multipleWildcard">Character for multiple character wildcard (typically '*').</param>
    /// <returns>True if input matches the pattern, false otherwise.</returns>
    private static bool isMatchRegex(string text, string pattern, char singleWildcard, char multipleWildcard)
    {
        if (text == pattern) return true;
        var escapedSingle = Regex.Escape(new string(singleWildcard, 1));
        var escapedMultiple = Regex.Escape(new string(multipleWildcard, 1));
        pattern = Regex.Escape(pattern);
        pattern = pattern.Replace(escapedSingle, ".");
        pattern = "^" + pattern.Replace(escapedMultiple, ".*") + "$";
        var regex = new Regex(pattern);
        return regex.IsMatch(text);
    }
}
