using CypherExpression.Model;
using Superpower;
using Superpower.Parsers;

namespace CypherExpression.Parser;

public static class CypherOperations
{
    public static TokenListParser<CypherToken, MatchQuery> Match { get; } =
        from begin in Token.EqualTo(CypherToken.Match)
        from node in ParserTypes.ParserEntityTypes.EntityPattern
        select new MatchQuery(new Entity("test"));
    
    internal static TokenListParser<CypherToken, ReturnQuery> CypherReturn { get; } =
        from begin in Token.EqualTo(CypherToken.Return)
        from args in ParserTypes.ReturnKinds.AnyField
            .ManyDelimitedBy(Token.EqualTo(CypherToken.Comma))
            .AtEnd()
        select new ReturnQuery(args.ToArray());

    
}