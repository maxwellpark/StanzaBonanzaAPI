using StanzaBonanza.Models.Models;

namespace StanzaBonanza.Models.Results
{
    public class Poems_AuthorsJoinResult
    {
        public Poem Poem { get; set; }
        public List<Author> Authors { get; set; }
    }
}
