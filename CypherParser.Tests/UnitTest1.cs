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
    
    [Theory]
    [InlineData("match (n) return n")]
    [InlineData("match () return n")]
    [InlineData("match (n)--() return n")]
    public void MatchEntities(string query)
    {
        this.TestQuery(query);       
    }
    
    [Theory]
    [InlineData("match (n) return n as this_is_my_node")]
    [InlineData("match (n) return n")]
    [InlineData("match (n) return n.id")]
    [InlineData("match (n) return n as 'the n'")]
    [InlineData("match (n) return n as 'the n', n, n.id")]
    [InlineData("match (n) return n as 'the n', count(n), n.id")]
    [InlineData("match (n) return n as 'the n', count(n) as 'counter', n.id")]
    public void Returns(string query)
    {
        this.TestQuery(query);       
    }

    [Theory]
    [InlineData("match (a)-[a]-(b) return a")]
    [InlineData("match (a)--(b) return a")]
    [InlineData("match (a)-[a:test]-(b) return a")]
    [InlineData("match (a)--(b) return a")]
    [InlineData("match (a)--(b)--(c) return a")]
    [InlineData("match (a)-[r1]-(b)-[r2]-(c) return a")]
    public void Match_Relationship(string query)
    {
        this.TestQuery(query);       
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