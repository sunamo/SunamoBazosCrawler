namespace SunamoBazosCrawler._sunamo;

internal class HtmlAssistant
{
    internal static string GetValueOfAttribute(string attributeName, HtmlNode node, bool trim = false)
    {
        object attribute = node.Attributes[attributeName]; // node.GetAttributeValue(attributeName, null);//
        if (attribute != null)
        {
            var value = ((HtmlAttribute)attribute).Value;
            if (trim) value = value.Trim();

            if (value == string.Empty) return "(null)";

            return value;
        }

        return string.Empty;
    }

    internal static string InnerText(HtmlNode node, bool recursive, string tag, string attr, string attrValue,
        bool contains = false)
    {
        return InnerContentWithAttr(node, recursive, tag, attr, attrValue, false, contains);
    }
    internal static string HtmlDecode(string htmlText)
    {
        return WebUtility.HtmlDecode(htmlText);
    }
    internal static string InnerContentWithAttr(HtmlNode node, bool recursive, string tag, string attr,
        string attrValue, bool html, bool contains = false)
    {
        var node2 = HtmlAgilityHelper.NodeWithAttr(node, recursive, tag, attr, attrValue, contains);
        if (node2 != null)
        {
            var content = string.Empty;
            if (html)
                content = node2.InnerHtml;
            else
                content = node2.InnerText;
            return HtmlDecode(content.Trim());
        }
        return string.Empty;
    }
}