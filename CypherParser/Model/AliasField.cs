namespace CypherExpression.Model;

public struct AliasField
{
    public Alias Alias { get; }
    public Field Field { get; }

    public AliasField(Alias alias, Field field)
    {
        Alias = alias;
        Field = field;
    }
}