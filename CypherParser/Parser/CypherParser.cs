using System.Diagnostics.CodeAnalysis;
using CypherExpression.Model;
using Superpower;
using Superpower.Model;

namespace CypherExpression.Parser;

public static class CypherParser
{
    internal static TokenListParser<CypherToken, Cypher> CypherExpression { get; } =
        from begin in CypherOperations.Match.AtLeastOnce()
        from res in CypherOperations.CypherReturn
        select begin.Any() ? new Cypher(begin
            .Cast<ICypherQuery>()
            .Append(res)
            .ToArray()) : new Cypher(Array.Empty<ICypherQuery>());
    
    public static bool TryParse(string query, out Cypher? value, 
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
}