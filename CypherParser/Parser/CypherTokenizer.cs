using Superpower;
using Superpower.Parsers;
using Superpower.Tokenizers;

namespace CypherExpression.CypherReader;

public class CypherTokenizer
{
    public static Tokenizer<CypherToken> Instance { get; } = 
        new TokenizerBuilder<CypherToken>()
            .Ignore(Span.WhiteSpace)
            .Match(CypherTokenParser.Match, CypherToken.Match)
            .Match(CypherTokenParser.Return, CypherToken.Return)
            .Match(Span.EqualToIgnoreCase("as"), CypherToken.As)
            .Match(CypherTokenParser.NodeStart, CypherToken.NodeStart)
            .Match(CypherTokenParser.NodeEnd, CypherToken.NodeEnd)
            
            
            .Match(Character.EqualTo('\"'), CypherToken.Quote)
            
            .Match(Character.EqualTo('['), CypherToken.RelationStart)
            .Match(Character.EqualTo(']'), CypherToken.RelationEnd)
            .Match(Character.EqualTo(':'), CypherToken.Colon)
            .Match(Character.EqualTo(','), CypherToken.Comma)
            .Match(Character.EqualTo('>'), CypherToken.ArrowRight)
            .Match(Character.EqualTo('<'), CypherToken.ArrowLeft)
            .Match(Character.EqualTo('-'), CypherToken.Dash)
            
            .Match(CypherTokenParser.String, CypherToken.String)
            .Build();
}