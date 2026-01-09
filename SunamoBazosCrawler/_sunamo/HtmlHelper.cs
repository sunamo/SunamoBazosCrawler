// variables names: ok
namespace SunamoBazosCrawler._sunamo;

/// <summary>
/// EN: Helper class for HTML operations (DUPLICATE - same functionality as HtmlAssistant)
/// CZ: Pomocná třída pro HTML operace (DUPLIKÁT - stejná funkcionalita jako HtmlAssistant)
/// </summary>
internal class HtmlHelper
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
}