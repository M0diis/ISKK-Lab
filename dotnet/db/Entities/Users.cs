using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace modkaz.DBs.Entities;

public partial class Users
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }
    [Required]
    [Column(TypeName = "mediumtext")]
    public string name { get; set; }
    [Required]
    [Column(TypeName = "mediumtext")]
    public string email { get; set; }
    [Required]
    [Column(TypeName = "mediumtext")]
    public string password { get; set; }
    [Required]
    [Column(TypeName = "tinyint(1)")]
    public bool admin { get; set; }
    [Required]
    [Column(TypeName = "timestamp")]
    public DateTime created_timestamp { get; set; }
}

