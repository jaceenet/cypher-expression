using CypherExpression.Model;
using Superpower;
using Superpower.Parsers;

namespace CypherExpression.Parser;

public static class ParserTypes
{
    internal static TokenListParser<CypherToken, string> TypeKind { get; } =
        from c in Token.EqualTo(CypherToken.Colon)
        from kind in Token.EqualTo(CypherToken.NamedString)
        select $":{kind.ToStringValue()}";

    internal static TokenListParser<CypherToken, string> NameOptionalType { get; } =
        from name in Token.EqualTo(CypherToken.NamedString)
        from kind in TypeKind.OptionalOrDefault()
        select $"{name.ToStringValue()}{(kind ?? "")}";

    public static class ParserEntityTypes
    {
        public static TokenListParser<CypherToken, Entity> Entity { get; } =
            from start in Token.EqualTo(CypherToken.LParenthesis)
            from sss in NameOptionalType.OptionalOrDefault()
            from ss in Token.EqualTo(CypherToken.RParenthesis)
            select new Entity(sss);

        public static TokenListParser<CypherToken, Entity> EntityWithRelation { get; } =
            from node in Entity
            from rel in ParserRelationTypes.Relationship
            select new Entity("rels");
        
        public static TokenListParser<CypherToken, Entity> EntityPattern { get; } =
            from n in Entity
                .Or(EntityWithRelation)
            from rel in ParserRelationTypes.Relationship.OptionalOrDefault()
            select n;
    }

    public static class ParserRelationTypes
    {
        internal static TokenListParser<CypherToken, string> SquaredRelation { get; } =
            from start in Token.EqualTo(CypherToken.LSquareBracket)
            from named in NameOptionalType.OptionalOrDefault() 
            from end in Token.EqualTo(CypherToken.RSquareBracket)
            select $"-[{named}]-";

        internal static TokenListParser<CypherToken, string> Relationship { get; } =
            from begin in Token.EqualTo(CypherToken.Dash)
            from end2 in SquaredRelation.OptionalOrDefault()
            from end in Token.EqualTo(CypherToken.Dash)
            from nodeEnd in ParserEntityTypes.EntityPattern
            //select $"relationship {ship} {name.ToStringValue()}";
            select "relationship";
    }

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
            from start in Token.EqualTo(CypherToken.LParenthesis).OptionalOrDefault()
            from a in Field.Try()
                .Or(Functions.Count)
                .Or(AsValue)
            from end in Token.EqualTo(CypherToken.RParenthesis).OptionalOrDefault()
            from b in AsDeclared.Optional()
            
            select new ReturnValue(a, b.HasValue ? b.Value : Alias.Undefined);
    }

    public static class Functions
    {
        internal static TokenListParser<CypherToken, Field> Count { get; } =
            from n in Token.EqualToValue(CypherToken.NamedString, "count")
            from start in Token.EqualTo(CypherToken.LParenthesis)
            from field in ReturnKinds.AnyField
            from end in Token.EqualTo(CypherToken.RParenthesis).OptionalOrDefault()
            select new Field("count");
    }
}