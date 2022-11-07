using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace modkaz.DBs.Entities;

[Table("reviews")]
public partial class ReviewsEntity
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }
    [Required]
    [Column(TypeName = "mediumtext")]
    public string data { get; set; }
    [Required]
    [Column(TypeName = "timestamp")]
    public DateTime created_timestamp { get; set; }
    [Required]
    [Column(TypeName = "int(11)")]
    public int fk_userId { get; set; }
}

