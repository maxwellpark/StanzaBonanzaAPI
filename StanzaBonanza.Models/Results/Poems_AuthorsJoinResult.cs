using StanzaBonanza.Models.Models;

namespace StanzaBonanza.Models.Results
{
    public class Poems_AuthorsJoinResult
    {
        public Poem Poem { get; set; }
        public HashSet<Author> Authors { get; set; }
    }
}
