using System.ComponentModel.DataAnnotations;

namespace StanzaBonanza.Models.Models
{
    /// <summary>
    /// Junction table with naming convention TableA_TableB.
    /// </summary>
    public class Poem_Author
    {
        [Key]
        [Required]
        public int Poem_AuthorId { get; set; }

        // Navigation properties 
        public int PoemId { get; set; }
        public Poem Poem { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }

        public Poem_Author()
        {
        }

        public Poem_Author(Poem poem, Author author) : this(poem.PoemId, author.AuthorId)
        {
            Poem = poem;
            Author = author;
        }

        public Poem_Author(int poemId, int authorId)
        {
            PoemId = poemId;
            AuthorId = authorId;
        }
    }
}
