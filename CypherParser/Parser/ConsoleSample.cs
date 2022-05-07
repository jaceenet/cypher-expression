namespace CypherExpression.CypherReader;

public class ConsoleSample
{
    internal static void Query(string query)
    {
        if (CypherParser.TryParse(query, out var value, out var error, out var errorPosition))
        {
            Console.WriteLine(value);
        }
        else
        {
            Console.WriteLine(query);
            Console.WriteLine($"{new string(' ', errorPosition.Column)}^");   
            Console.WriteLine(error);
        }
    }
}