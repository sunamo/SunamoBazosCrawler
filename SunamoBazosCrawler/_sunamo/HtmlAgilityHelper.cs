namespace SunamoBazosCrawler._sunamo;

internal class HtmlAgilityHelper
{
    internal const string TextNode = "#text";

    internal static bool ShouldTrimTexts { get; set; } = true;

    internal static HtmlDocument CreateHtmlDocument()
    {
        var htmlDocument = new HtmlDocument();
        htmlDocument.OptionOutputOriginalCase = true;
        htmlDocument.OptionAutoCloseOnEnd = false;
        htmlDocument.OptionOutputAsXml = false;
        htmlDocument.OptionFixNestedTags = false;
        return htmlDocument;
    }

    internal static HtmlNode? NodeWithAttr(HtmlNode node, bool isRecursive, string tag, string attribute, string expectedValue,
        bool isContainsCheck = false)
    {
        return nodesWithAttrWorker(node, isRecursive, tag, attribute, expectedValue, false, isContainsCheck).FirstOrDefault();
    }

    internal static List<HtmlNode> NodesWithAttr(HtmlNode node, bool isRecursive, string tag, string attribute,
        string expectedValue, bool isContainsCheck = false)
    {
        return nodesWithAttrWorker(node, isRecursive, tag, attribute, expectedValue, false, isContainsCheck);
    }

    private static List<HtmlNode> nodesWithAttrWorker(HtmlNode node, bool isRecursive, string tag, string attribute,
        string expectedValue, bool isWildcard, bool isContainsCheck, bool isSingleStringSearch = true)
    {
        var result = new List<HtmlNode>();
        RecursiveReturnTagsWithContainsAttr(result, node, isRecursive, tag, attribute, expectedValue, isWildcard,
            isContainsCheck, isSingleStringSearch);
        if (tag != TextNode) result = TrimTexts(result);
        return result;
    }

    internal static List<HtmlNode> TrimTexts(List<HtmlNode> nodes)
    {
        return TrimTexts(nodes, true);
    }

    internal static List<HtmlNode> TrimTexts(List<HtmlNode> nodes, bool isRemovingTextNodes, bool isRemovingComments = false)
    {
        if (!ShouldTrimTexts) return nodes;
        var result = new List<HtmlNode>();
        var shouldAdd = true;
        foreach (var item in nodes)
        {
            shouldAdd = true;
            if (isRemovingTextNodes)
                if (item.Name == TextNode)
                    shouldAdd = false;
            if (isRemovingComments)
                if (item.Name == "#comment")
                    shouldAdd = false;
            if (shouldAdd) result.Add(item);
        }
        return result;
    }

    private static bool hasTagName(HtmlNode node, string tag)
    {
        if (tag == "*") return true;
        return node.Name == tag;
    }

    private static bool hasTagAttr(HtmlNode node, string attribute, string expectedValue, bool isWildcard,
        bool isContainsCheck, bool isSingleStringSearch)
    {
        if (expectedValue == "*") return true;
        var isMatch = false;
        var actualValue = HtmlAssistant.GetValueOfAttribute(attribute, node);
        if (isContainsCheck)
        {
            if (isSingleStringSearch)
            {
                if (isWildcard)
                    isMatch = SH.MatchWildcard(actualValue, expectedValue);
                else
                    isMatch = actualValue.Contains(expectedValue);
            }
            else
            {
                var isMatchingAll = true;
                var expectedParts = SHSplit.Split(expectedValue, " ");
                foreach (var part in expectedParts)
                    if (!actualValue.Contains(part))
                    {
                        isMatchingAll = false;
                        break;
                    }
                isMatch = isMatchingAll;
            }
        }
        else
        {
            isMatch = actualValue == expectedValue;
        }
        return isMatch;
    }

    internal static void RecursiveReturnTagsWithContainsAttr(List<HtmlNode> result, HtmlNode parentNode, bool isRecursive,
        string tag, string attribute, string expectedValue, bool isWildcard, bool isContainsCheck,
        bool isSingleStringSearch = true)
    {
        tag = tag.ToLower();
        if (parentNode == null) return;
        foreach (var node in parentNode.ChildNodes)
        {
            var actualValue = HtmlAssistant.GetValueOfAttribute(attribute, node);
            if (hasTagName(node, tag) && hasTagAttr(node, attribute, expectedValue, isWildcard, isContainsCheck,
                    isSingleStringSearch))
                result.Add(node);
            if (isRecursive)
                RecursiveReturnTagsWithContainsAttr(result, node, isRecursive, tag, attribute, actualValue, isWildcard,
                    isContainsCheck, isSingleStringSearch);
        }
    }
}
