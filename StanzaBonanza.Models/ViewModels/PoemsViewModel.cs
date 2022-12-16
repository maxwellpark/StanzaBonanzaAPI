using StanzaBonanza.Models.Models;

namespace StanzaBonanza.Models.ViewModels
{
    public class PoemsViewModel
    {
        public IEnumerable<PoemViewModel> Poems { get; set; }

        public PoemsViewModel(IEnumerable<AuthorPoemJoin> authorPoemJoins)
        {
            Poems = authorPoemJoins?.Select(join => new PoemViewModel(join));
        }
    }
}
