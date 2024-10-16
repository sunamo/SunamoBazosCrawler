namespace SunamoBazosCrawler._sunamo;
using HtmlAgilityPack;

internal class HtmlHelper
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
}