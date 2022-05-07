using CypherExpression.Model;
using Superpower;
using Superpower.Parsers;

namespace CypherExpression.Parser;

public static class CypherParserTypes {
    public static TokenListParser<CypherToken, Entity> Entity { get; } =
        from start in Token.EqualTo(CypherToken.NodeStart)
        from sss in Token.EqualTo(CypherToken.NamedString)
        from ss in Token.EqualTo(CypherToken.NodeEnd)
        select new Entity(sss.ToStringValue());
}