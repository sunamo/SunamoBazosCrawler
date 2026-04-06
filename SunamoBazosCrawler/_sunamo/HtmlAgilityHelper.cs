namespace SunamoBazosCrawler._sunamo;

/// <summary>
/// Helper class for working with HtmlAgilityPack library.
/// </summary>
internal class HtmlAgilityHelper
{
    internal const string TextNode = "#text";
    internal static bool ShouldTrimTexts = true;

    /// <summary>
    /// Creates a configured HtmlDocument instance for parsing HTML.
    /// </summary>
    /// <returns>Configured HtmlDocument.</returns>
    internal static HtmlDocument CreateHtmlDocument()
    {
        var htmlDocument = new HtmlDocument();
        htmlDocument.OptionOutputOriginalCase = true;
        htmlDocument.OptionAutoCloseOnEnd = false;
        htmlDocument.OptionOutputAsXml = false;
        htmlDocument.OptionFixNestedTags = false;
        return htmlDocument;
    }

    /// <summary>
    /// Finds the first HTML node matching the specified tag and attribute criteria.
    /// </summary>
    /// <param name="node">Parent node to search within.</param>
    /// <param name="isRecursive">Whether to search recursively through child nodes.</param>
    /// <param name="tag">HTML tag name to search for.</param>
    /// <param name="attribute">Attribute name to match.</param>
    /// <param name="expectedValue">Expected attribute value.</param>
    /// <param name="isContainsCheck">Whether to use Contains instead of exact match.</param>
    /// <returns>First matching HTML node or null.</returns>
    internal static HtmlNode? NodeWithAttr(HtmlNode node, bool isRecursive, string tag, string attribute, string expectedValue,
        bool isContainsCheck = false)
    {
        return NodesWithAttrWorker(node, isRecursive, tag, attribute, expectedValue, false, isContainsCheck).FirstOrDefault();
    }

    /// <summary>
    /// Finds all HTML nodes matching the specified tag and attribute criteria.
    /// </summary>
    /// <param name="node">Parent node to search within.</param>
    /// <param name="isRecursive">Whether to search recursively through child nodes.</param>
    /// <param name="tag">HTML tag name to search for.</param>
    /// <param name="attribute">Attribute name to match.</param>
    /// <param name="expectedValue">Expected attribute value.</param>
    /// <param name="isContainsCheck">Whether to use Contains instead of exact match.</param>
    /// <returns>List of matching HTML nodes.</returns>
    internal static List<HtmlNode> NodesWithAttr(HtmlNode node, bool isRecursive, string tag, string attribute,
        string expectedValue, bool isContainsCheck = false)
    {
        return NodesWithAttrWorker(node, isRecursive, tag, attribute, expectedValue, false, isContainsCheck);
    }

    /// <summary>
    /// Worker method that performs the actual search for HTML nodes matching tag and attribute criteria.
    /// </summary>
    /// <param name="node">Parent node to search within.</param>
    /// <param name="isRecursive">Whether to search recursively through child nodes.</param>
    /// <param name="tag">HTML tag name to search for.</param>
    /// <param name="attribute">Attribute name to match.</param>
    /// <param name="expectedValue">Expected attribute value.</param>
    /// <param name="isWildcard">Whether to use wildcard matching.</param>
    /// <param name="isContainsCheck">Whether to use Contains instead of exact match.</param>
    /// <param name="isSingleStringSearch">Whether to search for single string or multiple parts.</param>
    /// <returns>List of matching HTML nodes.</returns>
    private static List<HtmlNode> NodesWithAttrWorker(HtmlNode node, bool isRecursive, string tag, string attribute,
        string expectedValue, bool isWildcard, bool isContainsCheck, bool isSingleStringSearch = true)
    {
        var result = new List<HtmlNode>();
        RecursiveReturnTagsWithContainsAttr(result, node, isRecursive, tag, attribute, expectedValue, isWildcard,
            isContainsCheck, isSingleStringSearch);
        if (tag != TextNode) result = TrimTexts(result);
        return result;
    }

    /// <summary>
    /// Trims text nodes from the list (removes #text nodes).
    /// </summary>
    /// <param name="nodes">List of HTML nodes to trim.</param>
    /// <returns>Trimmed list of HTML nodes.</returns>
    internal static List<HtmlNode> TrimTexts(List<HtmlNode> nodes)
    {
        return TrimTexts(nodes, true);
    }

    /// <summary>
    /// Trims text nodes and/or comment nodes from the list.
    /// </summary>
    /// <param name="nodes">List of HTML nodes to trim.</param>
    /// <param name="isRemovingTextNodes">Whether to remove #text nodes.</param>
    /// <param name="isRemovingComments">Whether to remove #comment nodes.</param>
    /// <returns>Trimmed list of HTML nodes.</returns>
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

    /// <summary>
    /// Checks if the node has the specified tag name.
    /// </summary>
    /// <param name="node">HTML node to check.</param>
    /// <param name="tag">Tag name to match (use "*" for any tag).</param>
    /// <returns>True if tag matches, false otherwise.</returns>
    private static bool HasTagName(HtmlNode node, string tag)
    {
        if (tag == "*") return true;
        return node.Name == tag;
    }

    /// <summary>
    /// Checks if the node has an attribute with the expected value.
    /// </summary>
    /// <param name="node">HTML node to check.</param>
    /// <param name="attribute">Attribute name.</param>
    /// <param name="expectedValue">Expected attribute value.</param>
    /// <param name="isWildcard">Whether to use wildcard matching.</param>
    /// <param name="isContainsCheck">Whether to use Contains instead of exact match.</param>
    /// <param name="isSingleStringSearch">Whether to search for single string or multiple parts.</param>
    /// <returns>True if attribute matches, false otherwise.</returns>
    private static bool HasTagAttr(HtmlNode node, string attribute, string expectedValue, bool isWildcard,
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

    /// <summary>
    /// Recursively searches for HTML nodes matching the specified tag and attribute criteria.
    /// </summary>
    /// <param name="result">List to store matching nodes.</param>
    /// <param name="parentNode">Parent node to search within.</param>
    /// <param name="isRecursive">Whether to search recursively through child nodes.</param>
    /// <param name="tag">HTML tag name to search for.</param>
    /// <param name="attribute">Attribute name to match.</param>
    /// <param name="expectedValue">Expected attribute value.</param>
    /// <param name="isWildcard">Whether to use wildcard matching.</param>
    /// <param name="isContainsCheck">Whether to use Contains instead of exact match.</param>
    /// <param name="isSingleStringSearch">Whether to search for single string or multiple parts.</param>
    internal static void RecursiveReturnTagsWithContainsAttr(List<HtmlNode> result, HtmlNode parentNode, bool isRecursive,
        string tag, string attribute, string expectedValue, bool isWildcard, bool isContainsCheck,
        bool isSingleStringSearch = true)
    {
        tag = tag.ToLower();
        if (parentNode == null) return;
        foreach (var node in parentNode.ChildNodes)
        {
            var actualValue = HtmlAssistant.GetValueOfAttribute(attribute, node);
            if (HasTagName(node, tag))
            {
                if (HasTagAttr(node, attribute, expectedValue, isWildcard, isContainsCheck,
                        isSingleStringSearch)) result.Add(node);
                if (isRecursive)
                    RecursiveReturnTagsWithContainsAttr(result, node, isRecursive, tag, attribute, actualValue, isWildcard,
                        isContainsCheck, isSingleStringSearch);
            }
            else
            {
                if (isRecursive)
                    RecursiveReturnTagsWithContainsAttr(result, node, isRecursive, tag, attribute, actualValue, isWildcard,
                        isContainsCheck, isSingleStringSearch);
            }
        }
    }
}
