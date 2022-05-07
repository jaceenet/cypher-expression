namespace CypherExpression.Model;

public readonly struct ReturnQuery : ICypherQuery
{
    public ReturnQuery(ReturnValue[] args)
    {
        this.Args = args;
    }

    public ReturnValue[] Args { get; } = Array.Empty<ReturnValue>();

    public string Visit(IQueryWriter visitor)
    {
        return visitor.Write(this);
    }
}