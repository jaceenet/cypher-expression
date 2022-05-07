namespace CypherExpression.Model;

public struct Alias
{
    public Alias(string value, bool quoted = false)
    {
        this.Value = value;
        Quoted = quoted;
    }

    public string Value { get; }
    public bool Quoted { get; }

    public static implicit operator Alias(string s)
    {
        return new Alias(s);
    }

    public static Alias Undefined = new (string.Empty);
    public bool HasValue => !string.IsNullOrEmpty(Value);
}