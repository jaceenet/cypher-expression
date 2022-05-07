namespace CypherExpression.Model;

public interface IQueryWriter
{
    string Write(Cypher query);
    string Write(MatchQuery query);
    string Write(Entity entity);
    string Write(ReturnValue entity);
    string Write(ReturnQuery entity);
}

public interface IQueryVisitor
{
    string Visit(IQueryWriter writer);
}