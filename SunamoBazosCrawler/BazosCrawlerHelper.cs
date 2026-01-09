// variables names: ok
namespace SunamoBazosCrawler;

/// <summary>
/// EN: Helper class for crawling and parsing dating advertisements from Bazos website
/// CZ: Pomocná třída pro crawlování a parsování seznamovacích inzerátů z webu Bazos
/// </summary>
public class BazosCrawlerHelper
{
    /// <summary>
    /// EN: Parses dating advertisements from the specified URL
    /// CZ: Parsuje seznamovací inzeráty ze zadané URL
    /// </summary>
    /// <param name="url">EN: The URL to parse advertisements from / CZ: URL ze které se mají parsovat inzeráty</param>
    /// <param name="downloadContentFunc">EN: Function to download or read content from the URL / CZ: Funkce pro stažení nebo načtení obsahu z URL</param>
    public static async Task ParseFromOnline(string url,
        Func<string, Task<string>> downloadContentFunc)
    {
        var result = new List<DatingAd>();
        await ParseFromOnline(url, result, downloadContentFunc);
    }

    /// <summary>
    /// EN: Parses dating advertisements from the specified URL and adds them to the result list
    /// CZ: Parsuje seznamovací inzeráty ze zadané URL a přidává je do seznamu výsledků
    /// </summary>
    /// <param name="url">EN: The URL to parse advertisements from / CZ: URL ze které se mají parsovat inzeráty</param>
    /// <param name="result">EN: List to store parsed advertisements / CZ: Seznam pro uložení naparsovaných inzerátů</param>
    /// <param name="downloadContentFunc">EN: Function to download or read content from the URL / CZ: Funkce pro stažení nebo načtení obsahu z URL</param>
    private static async Task ParseFromOnline(string url, List<DatingAd> result,
        Func<string, Task<string>> downloadContentFunc)
    {
        var html = await downloadContentFunc(url);
        var htmlDocument = HtmlAgilityHelper.CreateHtmlDocument();
        htmlDocument.LoadHtml(html);
        var mainContent =
            HtmlAgilityHelper.NodeWithAttr(htmlDocument.DocumentNode, true, HtmlTags.Div, HtmlAttrs.C, "maincontent");

        if (mainContent == null)
            return;

        var advertisements =
            HtmlAgilityHelper.NodesWithAttr(mainContent, true, HtmlTags.Div, HtmlAttrs.C, "inzeraty inzeratyflex");
        foreach (var item in advertisements)
        {
            var advertisement = new DatingAd
            {
                Title = HtmlAssistant.InnerText(item, true, HtmlTags.H2, HtmlAttrs.C, "nadpis"),
                Description = HtmlAssistant.InnerText(item, true, HtmlTags.Div, HtmlAttrs.C, "popis"),
                Price = HtmlAssistant.InnerText(item, true, HtmlTags.Div, HtmlAttrs.C, "inzeraty"),
                Location = HtmlAssistant.InnerText(item, true, HtmlTags.Div, HtmlAttrs.C, "inzeratylok")
            };
            result.Add(advertisement);
        }
    }
}