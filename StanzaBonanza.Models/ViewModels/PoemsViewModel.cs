using StanzaBonanza.Models.Results;

namespace StanzaBonanza.Models.ViewModels
{
    public class PoemsViewModel
    {
        public IEnumerable<PoemViewModel> Poems { get; set; }

        public PoemsViewModel(IEnumerable<AuthorPoemJoinResult> authorPoemJoins)
        {
            Poems = authorPoemJoins?.Select(join => new PoemViewModel(join));
        }
    }
}
