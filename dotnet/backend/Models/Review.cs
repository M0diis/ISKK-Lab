using modkaz.DBs.Entities;

namespace modkaz.Backend.Models;

/// <summary>
/// <para>Post view model for listing purposes.</para>
/// <para>Static members are thread safe, instance members are not.</para>
/// </summary>
public class ReviewForListing
{
	/// <summary>
	/// Post ID.
	/// </summary>
	public int Id { get; set; }

	/// <summary>
	/// Review content.
	/// </summary>
	public string Data { get; set; }

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
	public String UserName { get; set; }

	/// <summary>
	/// Create instance from DB entity.
	/// </summary>
	/// <param name="reviewEntity">DB entity to create from.</param>
	/// <returns>A corresponding instance.</returns>
	public static ReviewForListing DatabaseToObject(ReviewsEntity reviewEntity)
	{
		return new ReviewForListing
		{
			Id = reviewEntity.id,
			Data = reviewEntity.data,
			CreatedTimestamp = reviewEntity.created_timestamp,
			FK_UserID = reviewEntity.fk_userId
		};
	}
}