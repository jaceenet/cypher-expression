namespace CypherExpression.Model;

public readonly struct MatchQuery : ICypherQuery
{
    public readonly Entity Entity;

    public MatchQuery(Entity entity)
    {
        this.Entity = entity;
    }

    public string Visit(IQueryWriter visitor)
    {
        return visitor.Write(this);
    }
}