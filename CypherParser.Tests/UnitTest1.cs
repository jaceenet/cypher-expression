using System.Linq;
using CypherExpression.Model;
using CypherExpression.Parser;
using Xunit;
using Xunit.Abstractions;

namespace CypherParser.Tests;

public class UnitTest1 : CypherTest
{
    public UnitTest1(ITestOutputHelper helper) : base(helper)
    {
    }
    
    [Fact]
    public void Match_N()
    {
        this.TestQuery("match (n) return n");       
    }
    
    [Fact]
    public void Return_N_DotNamed()
    {
        this.TestQuery("match (n) return n.id");       
    }
    
    [Fact]
    public void Match_N_Match_B()
    {
        var q = "match (n) match (b) return n, b";
        this.TestTokens(q, 
            CypherToken.Match, 
            CypherToken.NodeStart, 
            CypherToken.NamedString, 
            CypherToken.NodeEnd,
            CypherToken.Match, 
            CypherToken.NodeStart, 
            CypherToken.NamedString, 
            CypherToken.NodeEnd,
            CypherToken.Return,
            CypherToken.NamedString,
            CypherToken.Comma,
            CypherToken.NamedString
            );
        
        this.TestQuery(q);       
    }
    
    [Fact]
    public void Return_as()
    {
        this.TestQuery("match (n) return n as n");       
    }
    
    [Fact]
    public void Match_N_on_type()
    {
        this.TestQuery("match (this_is_my_node) return n as this_is_my_node");       
    }
    
    [Fact]
    public void ReturnAsQuoted()
    {
        var c = this.TestQuery("match (n) return n.name as 'my name'");
        var res = c.Value.Queries.Last() is ReturnQuery ? (ReturnQuery) c.Value.Queries.Last() : default;
        Assert.NotNull(res);
        Assert.NotNull(res.Args[0]);
        Assert.Equal("n", res.Args[0].Field.Name);
        Assert.Equal("my name", res.Args[0].Alias.Value);
    }
}