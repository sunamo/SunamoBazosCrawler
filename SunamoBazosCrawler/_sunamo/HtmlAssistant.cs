namespace SunamoBazosCrawler._sunamo;

/// <summary>
/// Assistant class for HTML manipulation and extraction operations.
/// </summary>
internal class HtmlAssistant
{
    /// <summary>
    /// Gets the value of the specified attribute from an HTML node.
    /// </summary>
    /// <param name="attributeName">Name of the attribute to retrieve.</param>
    /// <param name="node">HTML node to get attribute from.</param>
    /// <param name="isTrimming">Whether to trim the attribute value.</param>
    /// <returns>Attribute value or empty string if not found.</returns>
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

    /// <summary>
    /// Gets the inner text of an HTML node matching the specified criteria.
    /// </summary>
    /// <param name="node">Parent node to search within.</param>
    /// <param name="isRecursive">Whether to search recursively.</param>
    /// <param name="tag">HTML tag name.</param>
    /// <param name="attribute">Attribute name.</param>
    /// <param name="expectedValue">Expected attribute value.</param>
    /// <param name="isContainsCheck">Whether to use Contains match.</param>
    /// <returns>Inner text of the found node or empty string.</returns>
    internal static string InnerText(HtmlNode node, bool isRecursive, string tag, string attribute, string expectedValue,
        bool isContainsCheck = false)
    {
        return InnerContentWithAttr(node, isRecursive, tag, attribute, expectedValue, false, isContainsCheck);
    }

    /// <summary>
    /// Decodes HTML-encoded text to plain text.
    /// </summary>
    /// <param name="htmlText">HTML-encoded text.</param>
    /// <returns>Decoded plain text.</returns>
    internal static string HtmlDecode(string htmlText)
    {
        return WebUtility.HtmlDecode(htmlText);
    }

    /// <summary>
    /// Gets the inner content (HTML or text) of an HTML node matching the specified criteria.
    /// </summary>
    /// <param name="node">Parent node to search within.</param>
    /// <param name="isRecursive">Whether to search recursively.</param>
    /// <param name="tag">HTML tag name.</param>
    /// <param name="attribute">Attribute name.</param>
    /// <param name="expectedValue">Expected attribute value.</param>
    /// <param name="isHtml">Whether to return HTML instead of text.</param>
    /// <param name="isContainsCheck">Whether to use Contains match.</param>
    /// <returns>Inner content of the found node or empty string.</returns>
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
