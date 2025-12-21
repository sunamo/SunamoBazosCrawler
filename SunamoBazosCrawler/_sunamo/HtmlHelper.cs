namespace SunamoBazosCrawler._sunamo;

internal class HtmlHelper
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
}