using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace StanzaBonanza.Models.Models;

public class Poem
{
    [Required]
    [Key]
    public int PoemId { get; set; }

    [Required]
    [MaxLength(255)]
    public string Title { get; set; }

    [Required]
    public string Body { get; set; }

    [Required]
    [DataType(DataType.Date)]
    [Column(TypeName = "Date")]
    public DateTime CreatedDate { get; set; }

    // Navigation
    [Required]
    [ForeignKey("Author")]
    [JsonIgnore]
    public int AuthorCreatorId { get; set; }

    [JsonIgnore]
    public List<Poem_Author> Poem_Authors { get; set; }

    public Poem()
    {
    }

    public Poem(int poemId, string title, string body, DateTime createdDate)
    {
        PoemId = poemId;
        Title = title;
        Body = body;
        CreatedDate = createdDate;
    }
}
