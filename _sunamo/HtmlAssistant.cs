namespace SunamoBazosCrawler._sunamo;

internal class HtmlAssistant
{
    internal static string InnerText(HtmlNode node, bool recursive, string tag, string attr, string attrValue,
        bool contains = false)
    {
        return InnerContentWithAttr(node, recursive, tag, attr, attrValue, false, contains);
    }
    internal static string HtmlDecode(string v)
    {
        return WebUtility.HtmlDecode(v);
    }
    internal static string InnerContentWithAttr(HtmlNode node, bool recursive, string tag, string attr,
        string attrValue, bool html, bool contains = false)
    {
        var node2 = HtmlAgilityHelper.NodeWithAttr(node, true, tag, attr, attrValue, contains);
        if (node2 != null)
        {
            var c = string.Empty;
            if (html)
                c = node2.InnerHtml;
            else
                c = node2.InnerText;
            return HtmlDecode(c.Trim());
        }
        return string.Empty;
    }
}