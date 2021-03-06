using System.Linq;
using CypherExpression.Model;
using CypherExpression.Parser;
using Superpower.Model;
using Xunit;
using Xunit.Abstractions;

namespace CypherParser.Tests;

public abstract class CypherTest
{
    private readonly ITestOutputHelper helper;

    public CypherTest(ITestOutputHelper helper)
    {
        this.helper = helper;
    }
    
    public Cypher? TestQuery(string query)
    {
        var s = CypherExpression.Parser.CypherParser
            .TryParse(query, out Cypher? res, out string error, out Position pos);

        this.helper.WriteLine(query);

        if (!string.IsNullOrEmpty(error))
        {
            this.helper.WriteLine(new string(' ', pos.Column - 1) + "^");
            this.helper.WriteLine("Error: " + error);
        }
        
        Assert.True(s);
        var writer = new QueryWriter();
        this.helper.WriteLine("Compiled to => ");
        this.helper.WriteLine(writer.Write(res.Value));
        return res.Value;
    }

    public void TestTokens(string q, params CypherToken[] tokens)
    {
        var res = CypherTokenizer.Instance.TryTokenize(q);
        Assert.True(res.HasValue);
        Assert.Equal(tokens, res.Value.Select(x => x.Kind).ToArray());
    }
}