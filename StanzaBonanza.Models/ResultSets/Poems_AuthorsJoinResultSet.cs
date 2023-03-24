using StanzaBonanza.Models.Results;

namespace StanzaBonanza.Models.ResultSets
{
    public class Poems_AuthorsJoinResultSet
    {
        public Dictionary<int, Poems_AuthorsJoinResult> JoinResults { get; } = new();
    }
}
