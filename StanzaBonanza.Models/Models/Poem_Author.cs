using System.ComponentModel.DataAnnotations;

namespace StanzaBonanza.Models.Models
{
    /// <summary>
    /// Junction table 
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
    }
}
