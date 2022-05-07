namespace CypherExpression.Model;

public struct Cypher
{
    public ICypherQuery[] Queries { get; }

    public Cypher()
    {
        Queries = Array.Empty<ICypherQuery>();
    }

    public Cypher(ICypherQuery[] queries)
    {
        Queries = queries;
    }
}