namespace CypherExpression.CypherWriter;

public class Value
{
    private readonly long _l;
    private readonly string _s;

    public Value(string s)
    {
        _s = s;
        _l = 0;
    }
    
    
    public Value(long l)
    {
        _l = l;
        _s = null;
    }
}