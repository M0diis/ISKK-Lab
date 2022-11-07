using System.ComponentModel.DataAnnotations;

using modkaz.DBs.Entities;


namespace modkaz.Backend.Models.Entity;

/// <summary>
/// <para>Post view model for listing purposes.</para>
/// <para>Static members are thread safe, instance members are not.</para>
/// </summary>
public class TicketForListing
{
	/// <summary>
	/// Post ID.
	/// </summary>
	public int Id { get; set; }

	/// <summary>
	/// Title.
	/// </summary>
	[Required(ErrorMessage = "Ticket title is required.")]
	public string Title { get; set; }

	/// <summary>
	/// Description.
	/// </summary>
	[Required(ErrorMessage = "Ticket description is required.")]
	public string Description { get; set; }
	
	/// <summary>
	/// Created Timestamp.
	/// </summary>
	public DateTime CreatedTimestamp { get; set; }

	/// <summary>
	/// User ID.
	/// </summary>
	[Required(ErrorMessage = "User ID is required.")]
	public long FK_UserID { get; set; }
	
	public String UserName { get; set; }

	/// <summary>
	/// Create instance from DB entity.
	/// </summary>
	/// <param name="ticket">DB entity to create from.</param>
	/// <returns>A corresponding instance.</returns>
	public static TicketForListing DatabaseToObject(Tickets ticket)
	{
		return new TicketForListing
		{
			Id = ticket.id,
			Title = ticket.title,
			Description = ticket.description,
			CreatedTimestamp = ticket.created_timestamp,
			FK_UserID = ticket.fk_userId
		};
	}
}