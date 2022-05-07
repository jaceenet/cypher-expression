namespace CypherExpression.Model;

public struct Entity : IQueryVisitor
{
    private readonly string[] types;
    private readonly Alias alias;

    public Entity(string[] types)
    {
        this.alias = Alias.Undefined;
        this.types = types;
    }
    
    public Entity(string alias)
    {
        this.types = Array.Empty<string>();
        this.alias = alias;
    }
    
    public Entity(Alias alias, string[] types)
    {
        this.alias = alias;
        this.types = types;
    }

    public string Name => this.alias.HasValue ? this.alias.Value : (":" + string.Join(':', this.types));

    public override string ToString()
    {
        return $"({(alias.HasValue ? alias.Value : "")}{string.Join(':', this.types)})";
    }

    public string Visit(IQueryWriter visitor)
    {
        return visitor.Write(this);
    }
}