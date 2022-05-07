using Superpower;
using Superpower.Model;
using Superpower.Parsers;

namespace CypherExpression.Parser;

static class CypherTokenParser
{
    internal static TextParser<TextSpan> Match { get; } = Span.EqualToIgnoreCase("match");
    internal static TextParser<TextSpan> Return { get; } = Span.EqualToIgnoreCase("return");

    //internal static TextParser<TextSpan> Expression { get; } = Parse.OneOf(Match, Return);
    
    internal static TextParser<string> String { get; } =
        from quote in Character.EqualTo('\'')
        from chars in Character
            //.IgnoreThen(Character.EqualTo('\''))
            //.Named("escaped")    
            .ExceptIn('\'')
            .Many()
        from close in Character.EqualTo('\'')
        select new string(chars);
    
    internal static TextParser<string> NamedString { get; } =
        //from l in Character.Letter
        from chars in Character
            .ExceptIn('(', ')', '[', ']', ':', '.', '\'', ' ', ',')
        //     //.IgnoreThen(Character.EqualTo('\''))
        //     .Named("escaped")
            .Many()
        select new string(chars);
    
    
}