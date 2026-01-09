// variables names: ok
namespace SunamoBazosCrawler._sunamo;

/// <summary>
/// EN: Helper class for working with HtmlAgilityPack library
/// CZ: Pomocná třída pro práci s HtmlAgilityPack knihovnou
/// </summary>
internal class HtmlAgilityHelper
{
    internal const string TextNode = "#text";
    internal static bool ShouldTrimTexts = true;

    /// <summary>
    /// EN: Creates a configured HtmlDocument instance for parsing HTML
    /// CZ: Vytvoří nakonfigurovanou instanci HtmlDocument pro parsování HTML
    /// </summary>
    /// <returns>EN: Configured HtmlDocument / CZ: Nakonfigurovaný HtmlDocument</returns>
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

    /// <summary>
    /// EN: Finds the first HTML node matching the specified tag and attribute criteria
    /// CZ: Najde první HTML uzel odpovídající zadaným kritériím tagu a atributu
    /// </summary>
    /// <param name="node">EN: Parent node to search within / CZ: Rodičovský uzel ve kterém se má hledat</param>
    /// <param name="isRecursive">EN: Whether to search recursively through child nodes / CZ: Zda hledat rekurzivně skrze potomky</param>
    /// <param name="tag">EN: HTML tag name to search for / CZ: Název HTML tagu který se má hledat</param>
    /// <param name="attribute">EN: Attribute name to match / CZ: Název atributu který se má shodovat</param>
    /// <param name="expectedValue">EN: Expected attribute value / CZ: Očekávaná hodnota atributu</param>
    /// <param name="isContainsCheck">EN: Whether to use Contains instead of exact match / CZ: Zda použít Contains místo přesné shody</param>
    /// <returns>EN: First matching HTML node or null / CZ: První odpovídající HTML uzel nebo null</returns>
    internal static HtmlNode? NodeWithAttr(HtmlNode node, bool isRecursive, string tag, string attribute, string expectedValue,
        bool isContainsCheck = false)
    {
        return NodesWithAttrWorker(node, isRecursive, tag, attribute, expectedValue, false, isContainsCheck).FirstOrDefault();
    }

    /// <summary>
    /// EN: Finds all HTML nodes matching the specified tag and attribute criteria
    /// CZ: Najde všechny HTML uzly odpovídající zadaným kritériím tagu a atributu
    /// </summary>
    /// <param name="node">EN: Parent node to search within / CZ: Rodičovský uzel ve kterém se má hledat</param>
    /// <param name="isRecursive">EN: Whether to search recursively through child nodes / CZ: Zda hledat rekurzivně skrze potomky</param>
    /// <param name="tag">EN: HTML tag name to search for / CZ: Název HTML tagu který se má hledat</param>
    /// <param name="attribute">EN: Attribute name to match / CZ: Název atributu který se má shodovat</param>
    /// <param name="expectedValue">EN: Expected attribute value / CZ: Očekávaná hodnota atributu</param>
    /// <param name="isContainsCheck">EN: Whether to use Contains instead of exact match / CZ: Zda použít Contains místo přesné shody</param>
    /// <returns>EN: List of matching HTML nodes / CZ: Seznam odpovídajících HTML uzlů</returns>
    internal static List<HtmlNode> NodesWithAttr(HtmlNode node, bool isRecursive, string tag, string attribute,
        string expectedValue, bool isContainsCheck = false)
    {
        return NodesWithAttrWorker(node, isRecursive, tag, attribute, expectedValue, false, isContainsCheck);
    }

    /// <summary>
    /// EN: Worker method that performs the actual search for HTML nodes matching tag and attribute criteria
    /// CZ: Pracovní metoda která provádí skutečné vyhledávání HTML uzlů odpovídajících kritériím tagu a atributu
    /// </summary>
    /// <param name="node">EN: Parent node to search within / CZ: Rodičovský uzel ve kterém se má hledat</param>
    /// <param name="isRecursive">EN: Whether to search recursively through child nodes / CZ: Zda hledat rekurzivně skrze potomky</param>
    /// <param name="tag">EN: HTML tag name to search for / CZ: Název HTML tagu který se má hledat</param>
    /// <param name="attribute">EN: Attribute name to match / CZ: Název atributu který se má shodovat</param>
    /// <param name="expectedValue">EN: Expected attribute value / CZ: Očekávaná hodnota atributu</param>
    /// <param name="isWildcard">EN: Whether to use wildcard matching / CZ: Zda použít shodu s wildcard znaky</param>
    /// <param name="isContainsCheck">EN: Whether to use Contains instead of exact match / CZ: Zda použít Contains místo přesné shody</param>
    /// <param name="isSingleStringSearch">EN: Whether to search for single string or multiple parts / CZ: Zda hledat jeden řetězec nebo více částí</param>
    /// <returns>EN: List of matching HTML nodes / CZ: Seznam odpovídajících HTML uzlů</returns>
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
    /// EN: Trims text nodes from the list (removes #text nodes)
    /// CZ: Odstraní textové uzly ze seznamu (odebere #text uzly)
    /// </summary>
    /// <param name="nodes">EN: List of HTML nodes to trim / CZ: Seznam HTML uzlů k oříznutí</param>
    /// <returns>EN: Trimmed list of HTML nodes / CZ: Oříznutý seznam HTML uzlů</returns>
    internal static List<HtmlNode> TrimTexts(List<HtmlNode> nodes)
    {
        return TrimTexts(nodes, true);
    }
    /// <summary>
    /// EN: Trims text nodes and/or comment nodes from the list
    /// CZ: Odstraní textové uzly a/nebo komentáře ze seznamu
    /// </summary>
    /// <param name="nodes">EN: List of HTML nodes to trim / CZ: Seznam HTML uzlů k oříznutí</param>
    /// <param name="isRemovingTextNodes">EN: Whether to remove #text nodes / CZ: Zda odebrat #text uzly</param>
    /// <param name="isRemovingComments">EN: Whether to remove #comment nodes / CZ: Zda odebrat #comment uzly</param>
    /// <returns>EN: Trimmed list of HTML nodes / CZ: Oříznutý seznam HTML uzlů</returns>
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
    /// EN: Checks if the node has the specified tag name
    /// CZ: Zkontroluje zda uzel má zadaný název tagu
    /// </summary>
    /// <param name="node">EN: HTML node to check / CZ: HTML uzel ke kontrole</param>
    /// <param name="tag">EN: Tag name to match (use "*" for any tag) / CZ: Název tagu k porovnání (použij "*" pro jakýkoliv tag)</param>
    /// <returns>EN: True if tag matches, false otherwise / CZ: True pokud tag odpovídá, jinak false</returns>
    private static bool HasTagName(HtmlNode node, string tag)
    {
        if (tag == "*") return true;
        return node.Name == tag;
    }

    /// <summary>
    /// EN: Checks if the node has an attribute with the expected value
    /// CZ: Zkontroluje zda uzel má atribut s očekávanou hodnotou
    /// </summary>
    /// <param name="node">EN: HTML node to check / CZ: HTML uzel ke kontrole</param>
    /// <param name="attribute">EN: Attribute name / CZ: Název atributu</param>
    /// <param name="expectedValue">EN: Expected attribute value / CZ: Očekávaná hodnota atributu</param>
    /// <param name="isWildcard">EN: Whether to use wildcard matching / CZ: Zda použít shodu s wildcard znaky</param>
    /// <param name="isContainsCheck">EN: Whether to use Contains instead of exact match / CZ: Zda použít Contains místo přesné shody</param>
    /// <param name="isSingleStringSearch">EN: Whether to search for single string or multiple parts / CZ: Zda hledat jeden řetězec nebo více částí</param>
    /// <returns>EN: True if attribute matches, false otherwise / CZ: True pokud atribut odpovídá, jinak false</returns>
    private static bool HasTagAttr(HtmlNode node, string attribute, string expectedValue, bool isWildcard,
        bool isContainsCheck, bool isSingleStringSearch)
    {
        if (expectedValue == "*") return true;
        var contains = false;
        var actualValue = HtmlAssistant.GetValueOfAttribute(attribute, node);
        if (isContainsCheck)
        {
            if (isSingleStringSearch)
            {
                if (isWildcard)
                    contains = SH.MatchWildcard(actualValue, expectedValue);
                else
                    contains = actualValue.Contains(expectedValue);
                //
            }
            else
            {
                var containsAll = true;
                var expectedParts = SHSplit.Split(expectedValue, " ");
                foreach (var part in expectedParts)
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

    /// <summary>
    /// EN: Recursively searches for HTML nodes matching the specified tag and attribute criteria
    /// CZ: Rekurzivně vyhledává HTML uzly odpovídající zadaným kritériím tagu a atributu
    /// </summary>
    /// <param name="result">EN: List to store matching nodes / CZ: Seznam pro uložení odpovídajících uzlů</param>
    /// <param name="parentNode">EN: Parent node to search within / CZ: Rodičovský uzel ve kterém se má hledat</param>
    /// <param name="isRecursive">EN: Whether to search recursively through child nodes / CZ: Zda hledat rekurzivně skrze potomky</param>
    /// <param name="tag">EN: HTML tag name to search for / CZ: Název HTML tagu který se má hledat</param>
    /// <param name="attribute">EN: Attribute name to match / CZ: Název atributu který se má shodovat</param>
    /// <param name="expectedValue">EN: Expected attribute value / CZ: Očekávaná hodnota atributu</param>
    /// <param name="isWildcard">EN: Whether to use wildcard matching / CZ: Zda použít shodu s wildcard znaky</param>
    /// <param name="isContainsCheck">EN: Whether to use Contains instead of exact match / CZ: Zda použít Contains místo přesné shody</param>
    /// <param name="isSingleStringSearch">EN: Whether to search for single string or multiple parts / CZ: Zda hledat jeden řetězec nebo více částí</param>
    internal static void RecursiveReturnTagsWithContainsAttr(List<HtmlNode> result, HtmlNode parentNode, bool isRecursive,
        string tag, string attribute, string expectedValue, bool isWildcard, bool isContainsCheck,
        bool isSingleStringSearch = true)
    {
        tag = tag.ToLower();
        //attribute = attribute.ToLower();
        //attributeValue = attribute.ToLower();
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