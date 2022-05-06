namespace CypherParser.CypherWriter;

public abstract class CypherBuilder
{
    public abstract string Compile();
}

// public class Pattern : CypherBuilder
// {
//         
// }

public class Match : CypherBuilder 
{
    public string Label { get; }
    public string Node { get; }

    public Match(string label, string node)
    {
        Label = label;
        Node = node;
    }

    public override string Compile()
    {
        if (!string.IsNullOrEmpty(this.Label))
        {
            return $"MATCH ('{Label}:{Node}')";
        }

        return $"MATCH ('{Node}')";
    }
}

public struct Delete
{
    public Delete(Alias a)
    {
        
    }
}