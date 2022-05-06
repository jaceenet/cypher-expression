// See https://aka.ms/new-console-template for more information

using CypherParser.CypherReader;
using Superpower.Model;


var m2 = @"
    MATCH (n)
    match (b) 
    return n as test, b";
var m1 = "Match (n)";

var tokens = CypherTokenParser.Match(new TextSpan(m1));

Console.WriteLine("Parsing: " + m2);
ConsoleSample.Query(m2);



