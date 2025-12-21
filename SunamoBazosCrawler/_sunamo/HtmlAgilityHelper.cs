namespace SunamoBazosCrawler._sunamo;

internal class HtmlAgilityHelper
{
    internal const string TextNode = "#text";
    internal static bool ShouldTrimTexts = true;
    internal static HtmlDocument CreateHtmlDocument()
    {
        var htmlDocument = new HtmlDocument();
        htmlDocument.OptionOutputOriginalCase = true;
        // false - even so the tag ending with / converts to </Page>. Tags that I don't want to terminate must still be deleted from HtmlAgilityPack.HtmlNode.ElementsFlags.Remove("form"); before loading XML https://html-agility-pack.net/knowledge-base/7104652/htmlagilitypack-close-form-tag-automatically
        htmlDocument.OptionAutoCloseOnEnd = false;
        htmlDocument.OptionOutputAsXml = false;
        htmlDocument.OptionFixNestedTags = false;
        //when OptionCheckSyntax = false, raise NullReferenceException in Load/LoadHtml
        //htmlDocument.OptionCheckSyntax = false;
        return htmlDocument;
    }
    internal static HtmlNode NodeWithAttr(HtmlNode node, bool recursive, string tag, string attr, string attrValue,
        bool contains = false)
    {
        return NodesWithAttrWorker(node, recursive, tag, attr, attrValue, false, contains).FirstOrDefault();
    }
    internal static List<HtmlNode> NodesWithAttr(HtmlNode node, bool recursive, string tag, string attr,
        string attrValue, bool contains = false)
    {
        return NodesWithAttrWorker(node, recursive, tag, attr, attrValue, false, contains);
    }
    private static List<HtmlNode> NodesWithAttrWorker(HtmlNode node, bool recursive, string tag, string attribute,
        string attributeValue, bool isWildCard, bool enoughIsContainsAttribute, bool searchAsSingleString = true)
    {
        var result = new List<HtmlNode>();
        RecursiveReturnTagsWithContainsAttr(result, node, recursive, tag, attribute, attributeValue, isWildCard,
            enoughIsContainsAttribute, searchAsSingleString);
        if (tag != TextNode) result = TrimTexts(result);
        return result;
    }
    internal static List<HtmlNode> TrimTexts(List<HtmlNode> nodes)
    {
        return TrimTexts(nodes, true);
    }
    /// <summary>
    ///     A2 =remove #text
    ///     A3 = remove #comment
    /// </summary>
    /// <param name="nodes"></param>
    /// <param name="texts"></param>
    /// <param name="comments"></param>
    internal static List<HtmlNode> TrimTexts(List<HtmlNode> nodes, bool texts, bool comments = false)
    {
        if (!ShouldTrimTexts) return nodes;
        var result = new List<HtmlNode>();
        var add = true;
        foreach (var item in nodes)
        {
            add = true;
            if (texts)
                if (item.Name == TextNode)
                    add = false;
            if (comments)
                if (item.Name == "#comment")
                    add = false;
            if (add) result.Add(item);
        }
        return result;
    }
    private static bool HasTagName(HtmlNode node, string tag)
    {
        if (tag == "*") return true;
        return node.Name == tag;
    }
    private static bool HasTagAttr(HtmlNode item, string attribute, string attributeValue, bool isWildCard,
        bool enoughIsContainsAttribute, bool searchAsSingleString)
    {
        if (attributeValue == "*") return true;
        var contains = false;
        var attrValue = HtmlAssistant.GetValueOfAttribute(attribute, item);
        if (enoughIsContainsAttribute)
        {
            if (searchAsSingleString)
            {
                if (isWildCard)
                    contains = SH.MatchWildcard(attrValue, attributeValue);
                else
                    contains = attrValue.Contains(attributeValue);
                //
            }
            else
            {
                var containsAll = true;
                var parts = SHSplit.Split(attributeValue, " ");
                foreach (var item2 in parts)
                    if (!attrValue.Contains(item2))
                    {
                        containsAll = false;
                        break;
                    }
                contains = containsAll;
            }
        }
        else
        {
            contains = attrValue == attributeValue;
        }
        return contains;
    }
    internal static void RecursiveReturnTagsWithContainsAttr(List<HtmlNode> result, HtmlNode htmlNode, bool recursively,
        string tag, string attribute, string attributeValue, bool isWildCard, bool enoughIsContainsAttribute,
        bool searchAsSingleString = true)
    {
        /*
isWildCard -
         */
        tag = tag.ToLower();
        //attribute = attribute.ToLower();
        //attributeValue = attribute.ToLower();
        if (htmlNode == null) return;
        foreach (var item in htmlNode.ChildNodes)
        {
            var attrValue = HtmlAssistant.GetValueOfAttribute(attribute, item);
            if (HasTagName(item, tag))
            {
                if (HasTagAttr(item, attribute, attributeValue, isWildCard, enoughIsContainsAttribute,
                        searchAsSingleString)) result.Add(item);
                if (recursively)
                    RecursiveReturnTagsWithContainsAttr(result, item, recursively, tag, attribute, attributeValue, isWildCard,
                        enoughIsContainsAttribute, searchAsSingleString);
            }
            else
            {
                if (recursively)
                    RecursiveReturnTagsWithContainsAttr(result, item, recursively, tag, attribute, attributeValue, isWildCard,
                        enoughIsContainsAttribute, searchAsSingleString);
            }
        }
    }
}