namespace SunamoBazosCrawler._sunamo;

internal class SH
{
    internal static bool MatchWildcard(string text, string pattern)
    {
        return isMatchRegex(text, pattern, '?', '*');
    }

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
