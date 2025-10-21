// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
namespace SunamoBazosCrawler._sunamo;

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