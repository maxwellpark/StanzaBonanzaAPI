using StanzaBonanza.Models.Models;
using StanzaBonanza.Models.Results;

namespace StanzaBonanza.Models.ViewModels;

public class PoemViewModel
{
    public string Title { get; set; }
    public int CharacterCount { get; set; }
    public string Body { get; set; }
    public string CreatedDate { get; set; }
    public string AuthorName { get; set; }

    public PoemViewModel()
    {
    }

    public PoemViewModel(Poem poem)
    {
        Title = poem?.Title;
        Body = poem?.Body;
        CreatedDate = poem?.CreatedDate.ToShortDateString();
        CharacterCount = Body != null ? Body.Length : 0;
    }

    public PoemViewModel(AuthorPoemJoinResult authorPoemJoin) : this(authorPoemJoin?.Poem)
    {
        AuthorName = authorPoemJoin?.Author?.Name;
    }
}
