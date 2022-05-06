namespace CypherParser.CypherWriter;

public struct Alias
{
    private readonly string _value;

    public Alias(string value)
    {
        _value = value;
    }
    
    public static implicit operator Alias(string s)
    {
        return new Alias(s);
    }

    public static Alias Undefined = new Alias("");
    public bool HasValue => _value != string.Empty;
    public string Value => _value;
}

public struct Field
{
    public Field(Entity node, Alias name)
    {
        
    }
}