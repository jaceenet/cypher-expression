using Superpower;
using Superpower.Model;
using Superpower.Parsers;

namespace CypherExpression.CypherReader;

static class CypherTokenParser
{
    internal static TextParser<TextSpan> Match { get; } = Span.EqualToIgnoreCase("match");
    internal static TextParser<TextSpan> Return { get; } = Span.EqualToIgnoreCase("return");

    internal static TextParser<char> NodeStart { get; } = Character.EqualTo('(');

    internal static TextParser<char> NodeEnd { get; } = Character.EqualTo(')');

    internal static TextParser<TextSpan> Expression { get; } = Parse.OneOf(Match, Return);
    
    internal static TextParser<string> String { get; } =
        from chars in Character
            .ExceptIn('(', ')', ' ', ',', '[', ']', '-', '>', '<', '=', '.')
            .Many()
        select new string(chars);
}