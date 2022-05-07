using Superpower.Display;

namespace CypherExpression.Parser;

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
    Comma,
    NamedString,
    Dot
}