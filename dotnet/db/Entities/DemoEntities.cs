using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace modkaz.DBs.Entities
{
    public partial class DemoEntities
    {
        [Key]
        [Column(TypeName = "int(11)")]
        public int id { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime date { get; set; }
        [Required]
        [Column(TypeName = "mediumtext")]
        public string name { get; set; }
        [Column(TypeName = "int(11)")]
        public int condition { get; set; }
        public bool deletable { get; set; }
    }
}
