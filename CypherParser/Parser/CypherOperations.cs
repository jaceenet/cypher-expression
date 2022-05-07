using CypherExpression.Model;
using Superpower;
using Superpower.Parsers;

namespace CypherExpression.Parser;

public static class CypherOperations
{
    public static TokenListParser<CypherToken, MatchQuery> Match { get; } =
        from begin in Token.EqualTo(CypherToken.Match)
        from node in CypherParserTypes.Entity
        select new MatchQuery(node);
    
    internal static TokenListParser<CypherToken, ReturnQuery> CypherReturn { get; } =
        from begin in Token.EqualTo(CypherToken.Return)
        from args in ReturnKinds.AnyField
            .ManyDelimitedBy(Token.EqualTo(CypherToken.Comma))
            .AtEnd()
        select new ReturnQuery(args.ToArray());

    public static class ReturnKinds
    {
        static TokenListParser<CypherToken, string> QuotedString { get; } =
            Token.EqualTo(CypherToken.String)
                .Apply(CypherTokenParser.String)
                .Select(s => s);
        
        static TokenListParser<CypherToken, string> NamedString { get; } =
            Token.EqualTo(CypherToken.NamedString)
                .Apply(CypherTokenParser.NamedString)
                .Select(s => s);

        internal static TokenListParser<CypherToken, Alias> AsDeclared { get; } =
            from d in Token.EqualTo(CypherToken.As)
            from a in NamedString
                .Or(QuotedString)
            select new Alias(a);

        internal static TokenListParser<CypherToken, Field> Field { get; } =
            from a in Token.EqualTo(CypherToken.NamedString)
            from b in Token.EqualTo(CypherToken.Dot)
            from c in Token.EqualTo(CypherToken.NamedString)
            select new Field(a.ToStringValue(), c.ToStringValue());
        
        internal static TokenListParser<CypherToken, Field> AsValue { get; } =
            from a in Token.EqualTo(CypherToken.NamedString)
            select new Field(a.ToStringValue());
        
        internal static TokenListParser<CypherToken, ReturnValue> AnyField { get; } =
            from a in Field.Try()
                .Or(AsValue)
                from b in AsDeclared.Optional()
            
            select new ReturnValue(a, b.HasValue ? b.Value : Alias.Undefined);
    }
}