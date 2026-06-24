namespace SunamoBazosCrawler;

public class BazosCrawlerHelper
{
    public static async Task ParseFromOnline(string url,
        Func<string, Task<string>> downloadContentFunc)
    {
        var result = new List<DatingAd>();
        await parseFromOnline(url, result, downloadContentFunc);
    }

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
