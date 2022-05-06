using System.Runtime.CompilerServices;

namespace CypherParser.CypherWriter;

public class CypherQuery
{
    public static CypherQuery Match()
    {
        return new CypherQuery();
    }
}