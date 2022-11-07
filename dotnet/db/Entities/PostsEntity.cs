using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace modkaz.DBs.Entities;

[Table("posts")]
public partial class PostsEntity
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }
    [Required]
    [Column(TypeName = "mediumtext")]
    public string title { get; set; }
    [Required]
    [Column(TypeName = "longtext")]
    public string content { get; set; }
    [Required]
    [Column(TypeName = "timestamp")]
    public DateTime created_timestamp { get; set; }
    [Required]
    [Column(TypeName = "int(11)")]
    public int fk_userId { get; set; }
}

