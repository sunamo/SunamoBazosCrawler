namespace SunamoBazosCrawler._sunamo;

internal class SH
{
    internal static bool MatchWildcard(string name, string mask)
    {
        return IsMatchRegex(name, mask, '?', '*');
    }
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