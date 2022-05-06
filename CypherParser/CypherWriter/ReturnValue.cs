namespace CypherParser.CypherWriter;

public struct ReturnValue
{
    private readonly Alias name;
    private readonly Alias @alias;
    
    public ReturnValue(Alias name, Alias alias)
    {
        this.name = name;
        this.alias = alias;
    }
    
    public ReturnValue(Alias name)
    {
        this.name = name;
        this.alias = Alias.Undefined;
    }

    public override string ToString()
    {
        if (alias.HasValue)
        {
            return $"{name.Value} as {alias.Value}";
        }
        return name.Value;
    }
}