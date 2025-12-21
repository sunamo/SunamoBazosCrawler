namespace SunamoBazosCrawler._sunamo;

internal class SHSplit
{
    internal static List<string> Split(string text, params string[] delimiters)
    {
        return text.Split(delimiters, StringSplitOptions.RemoveEmptyEntries).ToList();
    }
}