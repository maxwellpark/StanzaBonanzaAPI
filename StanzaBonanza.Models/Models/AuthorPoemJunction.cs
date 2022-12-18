namespace StanzaBonanza.Models.Models
{
    public class AuthorPoemJunction
    {
        public int AuthorId { get; set; }
        public Author Author { get; set; }
        public int PoemId { get; set; }
        public Poem Poem { get; set; }
    }
}
