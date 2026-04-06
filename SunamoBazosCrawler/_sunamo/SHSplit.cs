namespace SunamoBazosCrawler._sunamo;

/// <summary>
/// String helper for splitting operations.
/// </summary>
internal class SHSplit
{
    /// <summary>
    /// Splits a string by the specified delimiters and removes empty entries.
    /// </summary>
    /// <param name="text">Text to split.</param>
    /// <param name="delimiters">Delimiters to split by.</param>
    /// <returns>List of split parts without empty entries.</returns>
    internal static List<string> Split(string text, params string[] delimiters)
    {
        return text.Split(delimiters, StringSplitOptions.RemoveEmptyEntries).ToList();
    }
}
