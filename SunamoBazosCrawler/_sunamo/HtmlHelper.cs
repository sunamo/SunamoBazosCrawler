// variables names: ok
namespace SunamoBazosCrawler._sunamo;

internal class HtmlHelper
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
}