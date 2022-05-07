namespace CypherExpression.Model;

public class QueryWriter : IQueryWriter
{
    public string Write(Cypher query)
    {
        var s = (from q in query.Queries
            select q.Visit(this));

        return string.Join(' ', s);
    }
    
    public string Write(MatchQuery query)
    {
        return $"MATCH {this.Write(query.Entity)}";
    }
    
    public string Write(ReturnQuery query)
    {
        var s = (from q in query.Args
            select q.Visit(this));

        return "RETURN " + string.Join(", ", s);
    }

    public string Write(Entity entity)
    {
        return "(" + entity.Name + ")";
    }

    public string Write(ReturnValue entity)
    {
        if (entity.Alias.HasValue)
        {
            return $"{Write(entity.Field)} as {(entity.Alias.Quoted ? string.Concat("'" + entity.Alias.Value + "'") : entity.Alias.Value)}";
        }
        
        return Write(entity.Field);
    }

    public string Write(Field entityField)
    {
        if (entityField.IsNode)
        {
            return entityField.Name.Value;
        }

        return $"{entityField.Name.Value}.{entityField.FieldName}";
    }
}