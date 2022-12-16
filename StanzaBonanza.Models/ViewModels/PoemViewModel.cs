using StanzaBonanza.Models.Models;

namespace StanzaBonanza.Models.ViewModels;

public class PoemViewModel
{
    public string Title { get; set; }
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
    }

    public PoemViewModel(AuthorPoemJoin authorPoemJoin) : this(authorPoemJoin?.Poem)
    {
        AuthorName = authorPoemJoin?.Author?.Name;
    }
}
