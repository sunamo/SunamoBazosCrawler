namespace SunamoBazosCrawler._sunamo;

internal class HtmlAssistant
{
    internal static string GetValueOfAttribute(string attributeName, HtmlNode node, bool isTrimming = false)
    {
        object attribute = node.Attributes[attributeName]; // node.GetAttributeValue(attributeName, null);//
        if (attribute != null)
        {
            var value = ((HtmlAttribute)attribute).Value;
            if (isTrimming) value = value.Trim();

            if (value == string.Empty) return "(null)";

            return value;
        }

        return string.Empty;
    }

    internal static string InnerText(HtmlNode node, bool isRecursive, string tag, string attribute, string expectedValue,
        bool isContainsCheck = false)
    {
        return InnerContentWithAttr(node, isRecursive, tag, attribute, expectedValue, false, isContainsCheck);
    }
    internal static string HtmlDecode(string htmlText)
    {
        return WebUtility.HtmlDecode(htmlText);
    }
    internal static string InnerContentWithAttr(HtmlNode node, bool isRecursive, string tag, string attribute,
        string expectedValue, bool isHtml, bool isContainsCheck = false)
    {
        var node2 = HtmlAgilityHelper.NodeWithAttr(node, isRecursive, tag, attribute, expectedValue, isContainsCheck);
        if (node2 != null)
        {
            var content = string.Empty;
            if (isHtml)
                content = node2.InnerHtml;
            else
                content = node2.InnerText;
            return HtmlDecode(content.Trim());
        }
        return string.Empty;
    }
}