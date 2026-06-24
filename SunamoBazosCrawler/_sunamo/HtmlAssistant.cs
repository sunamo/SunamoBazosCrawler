namespace SunamoBazosCrawler._sunamo;

internal class HtmlAssistant
{
    internal static string GetValueOfAttribute(string attributeName, HtmlNode node, bool isTrimming = false)
    {
        object attributeObject = node.Attributes[attributeName];
        if (attributeObject != null)
        {
            var attributeValue = ((HtmlAttribute)attributeObject).Value;
            if (isTrimming) attributeValue = attributeValue.Trim();

            if (attributeValue == string.Empty) return "(null)";

            return attributeValue;
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
        var foundNode = HtmlAgilityHelper.NodeWithAttr(node, isRecursive, tag, attribute, expectedValue, isContainsCheck);
        if (foundNode != null)
        {
            var content = string.Empty;
            if (isHtml)
                content = foundNode.InnerHtml;
            else
                content = foundNode.InnerText;
            return HtmlDecode(content.Trim());
        }
        return string.Empty;
    }
}
