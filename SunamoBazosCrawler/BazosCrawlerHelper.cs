namespace SunamoBazosCrawler;

public class BazosCrawlerHelper
{
    public static async Task ParseFromOnline(string url,
        Func<string, Task<string>> httpRequestHelperDownloadOrRead)
    {
        var result = new List<DatingAd>();
        await ParseFromOnline(url, result, httpRequestHelperDownloadOrRead);
    }
    private static async Task ParseFromOnline(string url, List<DatingAd> result,
        Func<string, Task<string>> httpRequestHelperDownloadOrRead)
    {
        var html = await httpRequestHelperDownloadOrRead(url);
        var htmlDocument = HtmlAgilityHelper.CreateHtmlDocument();
        htmlDocument.LoadHtml(html);
        var maincontent =
            HtmlAgilityHelper.NodeWithAttr(htmlDocument.DocumentNode, true, HtmlTags.Div, HtmlAttrs.C, "maincontent");
        var inzeraty =
            HtmlAgilityHelper.NodesWithAttr(maincontent, true, HtmlTags.Div, HtmlAttrs.C, "inzeraty inzeratyflex");
        foreach (var item in inzeraty)
        {
            var ad = new DatingAd();
            ad.Title = HtmlAssistant.InnerText(item, true, HtmlTags.H2, HtmlAttrs.C, "nadpis");
            ad.Description = HtmlAssistant.InnerText(item, true, HtmlTags.Div, HtmlAttrs.C, "popis");
            ad.Price = HtmlAssistant.InnerText(item, true, HtmlTags.Div, HtmlAttrs.C, "inzeraty");
            ad.Lokalita = HtmlAssistant.InnerText(item, true, HtmlTags.Div, HtmlAttrs.C, "inzeratylok");
            result.Add(ad);
        }
    }
}