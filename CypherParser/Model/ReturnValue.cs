namespace CypherExpression.Model;

public readonly struct ReturnValue : ICypherQuery
{
    public readonly Field Field;
    public readonly Alias Alias;

    public ReturnValue(Field field, Alias alias)
    {
        this.Field = field;
        this.Alias = alias;
    }
    
    public ReturnValue(Field field)
    {
        this.Field = field;
        this.Alias = Alias.Undefined;
    }

    public string Visit(IQueryWriter writer)
    {
        return writer.Write(this);
    }
}