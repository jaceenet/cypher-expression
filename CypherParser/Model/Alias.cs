namespace CypherExpression.Model;

public struct Alias
{
    public Alias(string value)
    {
        this.Value = value;
    }

    public string Value { get; }

    public static implicit operator Alias(string s)
    {
        return new Alias(s);
    }

    public static Alias Undefined = new (string.Empty);
    public bool HasValue => !string.IsNullOrEmpty(Value);
}