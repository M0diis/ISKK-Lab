using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace db.Entities;

[Keyless]
[Table("messages_tickets")]
public partial class MessagesTicketsEntity
{
    [Required]
    [Column(TypeName = "int(11)")]
    public int fk_ticketId { get; set; }    
    [Required]
    [Column(TypeName = "int(11)")]
    public int fk_messageId { get; set; }
}

