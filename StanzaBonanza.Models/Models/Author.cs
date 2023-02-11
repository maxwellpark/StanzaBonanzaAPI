using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace StanzaBonanza.Models.Models;

public class Author
{
    [Required]
    [Key]
    public int AuthorId { get; set; }

    [Required]
    [MaxLength(64)]
    public string Name { get; set; }

    [Required]
    [DataType(DataType.Date)]
    [Column(TypeName = "Date")]
    public DateTime RegisteredDate { get; set; }

    // Navigation
    [JsonIgnore]
    public List<Poem_Author> Poem_Authors { get; set; }

    public Author()
    {
    }

    public Author(int authorId, string name, DateTime registeredDate)
    {
        AuthorId = authorId;
        Name = name;
        RegisteredDate = registeredDate;
    }
}
