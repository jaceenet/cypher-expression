using Superpower.Display;

namespace CypherParser.CypherReader;

public enum CypherToken
{
    None,

    [Token(Category = "operator", Example = "MATCH")]
    Match,
    Return,
    [Token(Category = "operator", Example = "AS")]
    As,
    NodeStart,
    NodeEnd,
    Quote,
    RelationStart,
    RelationEnd,
    Colon,
    
    String,

    Dash,
    ArrowRight,
    ArrowLeft,
    Comma
}