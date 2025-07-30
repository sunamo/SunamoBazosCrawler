namespace SunamoBazosCrawler._sunamo;

internal class HtmlAssistant
{
    internal static string GetValueOfAttribute(string p, HtmlNode divMain, bool _trim = false)
    {
        object o = divMain.Attributes[p]; // divMain.GetAttributeValue(p, null);//
        if (o != null)
        {
            var st = ((HtmlAttribute)o).Value;
            if (_trim) st = st.Trim();

            if (st == string.Empty) return "(null)";

            return st;
        }

        return string.Empty;
    }

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
        var node2 = HtmlAgilityHelper.NodeWithAttr(node, recursive, tag, attr, attrValue, contains);
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