// variables names: ok
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
    internal static HtmlNode NodeWithAttr(HtmlNode node, bool isRecursive, string tag, string attribute, string expectedValue,
        bool isContainsCheck = false)
    {
        return NodesWithAttrWorker(node, isRecursive, tag, attribute, expectedValue, false, isContainsCheck).FirstOrDefault();
    }
    internal static List<HtmlNode> NodesWithAttr(HtmlNode node, bool isRecursive, string tag, string attribute,
        string expectedValue, bool isContainsCheck = false)
    {
        return NodesWithAttrWorker(node, isRecursive, tag, attribute, expectedValue, false, isContainsCheck);
    }
    private static List<HtmlNode> NodesWithAttrWorker(HtmlNode node, bool isRecursive, string tag, string attribute,
        string expectedValue, bool isWildCard, bool isContainsCheck, bool isSingleStringSearch = true)
    {
        var result = new List<HtmlNode>();
        RecursiveReturnTagsWithContainsAttr(result, node, isRecursive, tag, attribute, expectedValue, isWildCard,
            isContainsCheck, isSingleStringSearch);
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
    /// <param name="isRemovingTextNodes"></param>
    /// <param name="isRemovingComments"></param>
    internal static List<HtmlNode> TrimTexts(List<HtmlNode> nodes, bool isRemovingTextNodes, bool isRemovingComments = false)
    {
        if (!ShouldTrimTexts) return nodes;
        var result = new List<HtmlNode>();
        var add = true;
        foreach (var item in nodes)
        {
            add = true;
            if (isRemovingTextNodes)
                if (item.Name == TextNode)
                    add = false;
            if (isRemovingComments)
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
    private static bool HasTagAttr(HtmlNode node, string attribute, string expectedValue, bool isWildCard,
        bool isContainsCheck, bool isSingleStringSearch)
    {
        if (expectedValue == "*") return true;
        var contains = false;
        var actualValue = HtmlAssistant.GetValueOfAttribute(attribute, node);
        if (isContainsCheck)
        {
            if (isSingleStringSearch)
            {
                if (isWildCard)
                    contains = SH.MatchWildcard(actualValue, expectedValue);
                else
                    contains = actualValue.Contains(expectedValue);
                //
            }
            else
            {
                var containsAll = true;
                var parts = SHSplit.Split(expectedValue, " ");
                foreach (var part in parts)
                    if (!actualValue.Contains(part))
                    {
                        containsAll = false;
                        break;
                    }
                contains = containsAll;
            }
        }
        else
        {
            contains = actualValue == expectedValue;
        }
        return contains;
    }

    internal static void RecursiveReturnTagsWithContainsAttr(List<HtmlNode> result, HtmlNode htmlNode, bool isRecursive,
        string tag, string attribute, string expectedValue, bool isWildCard, bool isContainsCheck,
        bool isSingleStringSearch = true)
    {
        tag = tag.ToLower();
        //attribute = attribute.ToLower();
        //attributeValue = attribute.ToLower();
        if (htmlNode == null) return;
        foreach (var node in htmlNode.ChildNodes)
        {
            var actualValue = HtmlAssistant.GetValueOfAttribute(attribute, node);
            if (HasTagName(node, tag))
            {
                if (HasTagAttr(node, attribute, expectedValue, isWildCard, isContainsCheck,
                        isSingleStringSearch)) result.Add(node);
                if (isRecursive)
                    RecursiveReturnTagsWithContainsAttr(result, node, isRecursive, tag, attribute, actualValue, isWildCard,
                        isContainsCheck, isSingleStringSearch);
            }
            else
            {
                if (isRecursive)
                    RecursiveReturnTagsWithContainsAttr(result, node, isRecursive, tag, attribute, actualValue, isWildCard,
                        isContainsCheck, isSingleStringSearch);
            }
        }
    }
}