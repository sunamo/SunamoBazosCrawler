namespace SunamoBazosCrawler;

public class BazosCrawlerHelper
{
    public static void ParseFromOnline(string url, int priceMax)
    {
        List<DatingAd> result = new List<DatingAd>();
        ParseFromOnline(url, priceMax, result);
    }

    private static void ParseFromOnline(string url, int priceMax, List<DatingAd> result)
    {
        var html = HttpRequestHelper.DownloadOrRead(url);

        var hd = HtmlAgilityHelper.CreateHtmlDocument();

        hd.LoadHtml(html);

        var maincontent = HtmlAgilityHelper.NodeWithAttr(hd.DocumentNode, true, HtmlTags.div, HtmlAttrs.c, "maincontent");

        var inzeraty = HtmlAgilityHelper.NodesWithAttr(maincontent, true, HtmlTags.div, HtmlAttrs.c, "inzeraty inzeratyflex");

        foreach (var item in inzeraty)
        {
            var ad = new DatingAd();

            ad.Title = HtmlAssistant.InnerText(item, true, HtmlTags.h2, HtmlAttrs.c, "nadpis");
            ad.Description = HtmlAssistant.InnerText(item, true, HtmlTags.div, HtmlAttrs.c, "popis");
            ad.Price = HtmlAssistant.InnerText(item, true, HtmlTags.div, HtmlAttrs.c, "inzeraty");
            ad.Lokalita = HtmlAssistant.InnerText(item, true, HtmlTags.div, HtmlAttrs.c, "inzeratylok");

            var s = 0;

            result.Add(ad);
        }


    }
}
