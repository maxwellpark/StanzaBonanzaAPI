using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StanzaBonanza.Models.Models;

public class Poem
{
    [Required]
    [Key]
    public int Id { get; set; }

    [Required]
    [ForeignKey("Author")]
    public int AuthorCreatorId { get; set; }

    [Required]
    [MaxLength(255)]
    public string Title { get; set; }

    [Required]
    public string Body { get; set; }

    [Required]
    [DataType(DataType.Date)]
    [Column(TypeName = "Date")]
    public DateTime CreatedDate { get; set; }

    public ICollection<Author> Authors { get; set; }
}