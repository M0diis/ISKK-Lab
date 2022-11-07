using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace db.Entities;

[Table("messages")]
public partial class MessagesEntity
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }
    [Required]
    [Column(TypeName = "mediumtext")]
    public string content { get; set; }
    [Column(TypeName = "timestamp")]
    public DateTime created_timestamp { get; set; }
    [Column(TypeName = "int(11)")]
    public int fk_userId { get; set; }
}

