using db.Entities;

namespace modkaz.Backend.Models;

/// <summary>
/// <para>Post view model for listing purposes.</para>
/// <para>Static members are thread safe, instance members are not.</para>
/// </summary>
public class TicketForListing
{
	/// <summary>
	/// Ticket ID.
	/// </summary>
	public int Id { get; set; }

	/// <summary>
	/// Title.
	/// </summary>
	public string Title { get; set; }

	/// <summary>
	/// Description.
	/// </summary>
	public string Description { get; set; }
	
	/// <summary>
	/// Created Timestamp.
	/// </summary>
	public DateTime CreatedTimestamp { get; set; }
	
	/// <summary>
	/// Closed.
	/// </summary>
	public bool Closed { get; set; }

	/// <summary>
	/// User ID.
	/// </summary>
	public long FK_UserID { get; set; }
	
	/// <summary>
	/// User name.
	/// </summary>
	public string UserName { get; set; }

	/// <summary>
	/// Create instance from DB entity.
	/// </summary>
	/// <param name="ticketEntity">DB entity to create from.</param>
	/// <returns>A corresponding instance.</returns>
	public static TicketForListing DatabaseToObject(TicketsEntity ticketEntity)
	{
		return new TicketForListing
		{
			Id = ticketEntity.id,
			Title = ticketEntity.title,
			Description = ticketEntity.description,
			CreatedTimestamp = ticketEntity.created_timestamp,
			FK_UserID = ticketEntity.fk_userId,
			Closed = ticketEntity.closed
		};
	}
}