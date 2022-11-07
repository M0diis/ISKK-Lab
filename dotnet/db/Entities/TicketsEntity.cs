using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace modkaz.DBs.Entities;

[Table("tickets")]
public partial class TicketsEntity
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }
    [Required]
    [Column(TypeName = "mediumtext")]
    public string title { get; set; }
    [Required]
    [Column(TypeName = "mediumtext")]
    public string description { get; set; }
    [Required]
    [Column(TypeName = "tinyint(1)")]
    public bool closed { get; set; }
    [Required]
    [Column(TypeName = "timestamp")]
    public DateTime created_timestamp { get; set; }
    [Column(TypeName = "int(11)")]
    public int fk_userId { get; set; }
}

