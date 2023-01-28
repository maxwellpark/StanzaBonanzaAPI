﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
    public List<Poem_Author> Poem_Authors { get; set; }
}
