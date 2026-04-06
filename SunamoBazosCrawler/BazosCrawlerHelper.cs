namespace SunamoBazosCrawler;

/// <summary>
/// Helper class for crawling and parsing dating advertisements from Bazos website.
/// </summary>
public class BazosCrawlerHelper
{
    /// <summary>
    /// Parses dating advertisements from the specified URL.
    /// </summary>
    /// <param name="url">The URL to parse advertisements from.</param>
    /// <param name="downloadContentFunc">Function to download or read content from the URL.</param>
    public static async Task ParseFromOnline(string url,
        Func<string, Task<string>> downloadContentFunc)
    {
        var result = new List<DatingAd>();
        await parseFromOnline(url, result, downloadContentFunc);
    }

    /// <summary>
    /// Parses dating advertisements from the specified URL and adds them to the provided list.
    /// </summary>
    /// <param name="url">The URL to parse advertisements from.</param>
    /// <param name="list">List to store parsed advertisements.</param>
    /// <param name="downloadContentFunc">Function to download or read content from the URL.</param>
    private static async Task parseFromOnline(string url, List<DatingAd> list,
        Func<string, Task<string>> downloadContentFunc)
    {
        var html = await downloadContentFunc(url);
        var htmlDocument = HtmlAgilityHelper.CreateHtmlDocument();
        htmlDocument.LoadHtml(html);
        var mainContent =
            HtmlAgilityHelper.NodeWithAttr(htmlDocument.DocumentNode, true, HtmlTags.Div, HtmlAttrs.CssClass, "maincontent");

        if (mainContent == null)
            return;

        var advertisements =
            HtmlAgilityHelper.NodesWithAttr(mainContent, true, HtmlTags.Div, HtmlAttrs.CssClass, "inzeraty inzeratyflex");
        foreach (var item in advertisements)
        {
            var advertisement = new DatingAd
            {
                Title = HtmlAssistant.InnerText(item, true, HtmlTags.H2, HtmlAttrs.CssClass, "nadpis"),
                Description = HtmlAssistant.InnerText(item, true, HtmlTags.Div, HtmlAttrs.CssClass, "popis"),
                Price = HtmlAssistant.InnerText(item, true, HtmlTags.Div, HtmlAttrs.CssClass, "inzeraty"),
                Location = HtmlAssistant.InnerText(item, true, HtmlTags.Div, HtmlAttrs.CssClass, "inzeratylok")
            };
            list.Add(advertisement);
        }
    }
}
