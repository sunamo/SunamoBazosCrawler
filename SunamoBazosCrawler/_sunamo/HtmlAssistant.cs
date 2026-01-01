namespace SunamoBazosCrawler._sunamo;

/// <summary>
/// EN: Assistant class for HTML manipulation and extraction operations
/// CZ: Pomocná třída pro manipulaci s HTML a extrakci dat
/// </summary>
internal class HtmlAssistant
{
    /// <summary>
    /// EN: Gets the value of the specified attribute from an HTML node
    /// CZ: Získá hodnotu zadaného atributu z HTML uzlu
    /// </summary>
    /// <param name="attributeName">EN: Name of the attribute to retrieve / CZ: Název atributu k získání</param>
    /// <param name="node">EN: HTML node to get attribute from / CZ: HTML uzel ze kterého získat atribut</param>
    /// <param name="isTrimming">EN: Whether to trim the attribute value / CZ: Zda oříznout hodnotu atributu</param>
    /// <returns>EN: Attribute value or empty string if not found / CZ: Hodnota atributu nebo prázdný řetězec pokud nebyl nalezen</returns>
    internal static string GetValueOfAttribute(string attributeName, HtmlNode node, bool isTrimming = false)
    {
        object attributeObject = node.Attributes[attributeName]; // node.GetAttributeValue(attributeName, null);//
        if (attributeObject != null)
        {
            var value = ((HtmlAttribute)attributeObject).Value;
            if (isTrimming) value = value.Trim();

            if (value == string.Empty) return "(null)";

            return value;
        }

        return string.Empty;
    }

    /// <summary>
    /// EN: Gets the inner text of an HTML node matching the specified criteria
    /// CZ: Získá vnitřní text HTML uzlu odpovídajícího zadaným kritériím
    /// </summary>
    /// <param name="node">EN: Parent node to search within / CZ: Rodičovský uzel ve kterém se má hledat</param>
    /// <param name="isRecursive">EN: Whether to search recursively / CZ: Zda hledat rekurzivně</param>
    /// <param name="tag">EN: HTML tag name / CZ: Název HTML tagu</param>
    /// <param name="attribute">EN: Attribute name / CZ: Název atributu</param>
    /// <param name="expectedValue">EN: Expected attribute value / CZ: Očekávaná hodnota atributu</param>
    /// <param name="isContainsCheck">EN: Whether to use Contains match / CZ: Zda použít Contains shodu</param>
    /// <returns>EN: Inner text of the found node or empty string / CZ: Vnitřní text nalezeného uzlu nebo prázdný řetězec</returns>
    internal static string InnerText(HtmlNode node, bool isRecursive, string tag, string attribute, string expectedValue,
        bool isContainsCheck = false)
    {
        return InnerContentWithAttr(node, isRecursive, tag, attribute, expectedValue, false, isContainsCheck);
    }

    /// <summary>
    /// EN: Decodes HTML-encoded text to plain text
    /// CZ: Dekóduje HTML-enkódovaný text na obyčejný text
    /// </summary>
    /// <param name="htmlText">EN: HTML-encoded text / CZ: HTML-enkódovaný text</param>
    /// <returns>EN: Decoded plain text / CZ: Dekódovaný obyčejný text</returns>
    internal static string HtmlDecode(string htmlText)
    {
        return WebUtility.HtmlDecode(htmlText);
    }

    /// <summary>
    /// EN: Gets the inner content (HTML or text) of an HTML node matching the specified criteria
    /// CZ: Získá vnitřní obsah (HTML nebo text) HTML uzlu odpovídajícího zadaným kritériím
    /// </summary>
    /// <param name="node">EN: Parent node to search within / CZ: Rodičovský uzel ve kterém se má hledat</param>
    /// <param name="isRecursive">EN: Whether to search recursively / CZ: Zda hledat rekurzivně</param>
    /// <param name="tag">EN: HTML tag name / CZ: Název HTML tagu</param>
    /// <param name="attribute">EN: Attribute name / CZ: Název atributu</param>
    /// <param name="expectedValue">EN: Expected attribute value / CZ: Očekávaná hodnota atributu</param>
    /// <param name="isHtml">EN: Whether to return HTML instead of text / CZ: Zda vrátit HTML místo textu</param>
    /// <param name="isContainsCheck">EN: Whether to use Contains match / CZ: Zda použít Contains shodu</param>
    /// <returns>EN: Inner content of the found node or empty string / CZ: Vnitřní obsah nalezeného uzlu nebo prázdný řetězec</returns>
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