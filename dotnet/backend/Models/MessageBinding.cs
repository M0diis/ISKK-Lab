using db.Entities;

namespace modkaz.Backend.Models;

/// <summary>
/// <para>User view model for listing purposes.</para>
/// <para>Static members are thread safe, instance members are not.</para>
/// </summary>
public class MessageForListing
{
	/// <summary>
	/// Message ID.
	/// </summary>
	public int Id { get; set; }
	
	/// <summary>
	/// Message content.
	/// </summary>
	public string Content { get; set; }

	/// <summary>
	/// Created timestamp.
	/// </summary>
	public DateTime CreatedTimestamp { get; set; }
	
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
	/// <param name="messagesEntity">DB entity to create from.</param>
	/// <returns>A corresponding instance.</returns>
	public static MessageForListing DatabaseToObject(MessagesEntity messagesEntity)
	{
		return new MessageForListing
		{
			Id = messagesEntity.id,
			Content = messagesEntity.content,
			CreatedTimestamp = messagesEntity.created_timestamp,
			FK_UserID = messagesEntity.fk_userId
		};
	}
}