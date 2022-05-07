namespace CypherExpression.Model;

public struct Field
{
    public Alias Name { get; } = string.Empty;
    
    public string FieldName { get; set; }

    public bool IsNode => FieldName == string.Empty;

    public Field(Alias name)
    {
        Name = name;
        FieldName = string.Empty;
    }
    
    public Field(Alias alias, string field)
    {
        Name = alias;
        FieldName = field;
    }

    
}