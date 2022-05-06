using System.Diagnostics.CodeAnalysis;
using CypherParser.CypherWriter;
using Superpower;
using Superpower.Model;
using Superpower.Parsers;

namespace CypherParser.CypherReader;

static class CypherParser
{
    public static TokenListParser<CypherToken, object?> CypherQuery { get; } =
        from begin in Token.EqualTo(CypherToken.Match)
        from node in Entity
        select (object) $"match {node}";
    
    public static TokenListParser<CypherToken, ReturnValue[]> CypherReturn { get; } =
        from begin in Token.EqualTo(CypherToken.Return)
        from args in ReturnParam
            .ManyDelimitedBy(Token.EqualTo(CypherToken.Comma))
            .AtEnd()
        select args;

    public static TokenListParser<CypherToken, object?> CypherExpression { get; } =
        from begin in CypherQuery
        from res in CypherReturn
        select (object)$"Recompiled expression: \r\n {begin} \r\n\treturn {string.Join(',', res)}";

    public static TokenListParser<CypherToken, Entity> Entity { get; } =
        from start in Token.EqualTo(CypherToken.NodeStart)
        from sss in Token.EqualTo(CypherToken.String)
        from ss in Token.EqualTo(CypherToken.NodeEnd)
        select new Entity(sss.ToStringValue());
    

    public static TokenListParser<CypherToken, Alias> AsParam { get; } =
        from d in Token.EqualTo(CypherToken.As)
        from d2 in Token.EqualTo(CypherToken.String)
        select new Alias(d2.ToStringValue());

    public static TokenListParser<CypherToken, ReturnValue> ReturnParam { get; } =
        from d in Token.EqualTo(CypherToken.String)
        from d2 in AsParam.Optional()
        select new ReturnValue(d.ToStringValue(), d2.HasValue ? d2.Value : Alias.Undefined);
    
    public static bool TryParse(string query, out object? value, 
        [MaybeNullWhen(true)] out string error, 
        out Position errorPosition)
    {
        var tokens = CypherTokenizer.Instance.TryTokenize(query);
        
        if (!tokens.HasValue)
        {
            value = null;
            error = tokens.ToString();
            errorPosition = tokens.ErrorPosition;
            return false;
        }

        var parsed = CypherExpression.TryParse(tokens.Value);
        if (!parsed.HasValue)
        {
            value = null;
            error = parsed.ToString();
            errorPosition = parsed.ErrorPosition;
            return false;
        }

        value = parsed.Value;
        error = null;
        errorPosition = Position.Empty;
        return true;
    }
    
    // private static TokenListParser<CypherToken, string> MatchExpression { get; } = 
    //     Token.EqualTo(CypherToken.Match)
    //     .Apply(CypherTokenParser.Match)
    //     .Select(s => "test");
}