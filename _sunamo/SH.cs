namespace SunamoBazosCrawler._sunamo;

internal class SH
{
    internal static bool MatchWildcard(string name, string mask)
    {
        return IsMatchRegex(name, mask, '?', '*');
    }
    private static bool IsMatchRegex(string str, string pat, char singleWildcard, char multipleWildcard)
    {
        // If I compared .vs with .vs, return false before
        if (str == pat) return true;
        var escapedSingle = Regex.Escape(new string(singleWildcard, 1));
        var escapedMultiple = Regex.Escape(new string(multipleWildcard, 1));
        pat = Regex.Escape(pat);
        pat = pat.Replace(escapedSingle, ".");
        pat = "^" + pat.Replace(escapedMultiple, ".*") + "$";
        var reg = new Regex(pat);
        return reg.IsMatch(str);
    }
}