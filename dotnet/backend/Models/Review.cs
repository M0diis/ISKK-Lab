using modkaz.DBs.Entities;

namespace modkaz.Backend.Models.Entity;

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
	/// <param name="review">DB entity to create from.</param>
	/// <returns>A corresponding instance.</returns>
	public static ReviewForListing DatabaseToObject(Reviews review)
	{
		return new ReviewForListing
		{
			Id = review.id,
			Data = review.data,
			CreatedTimestamp = review.created_timestamp,
			FK_UserID = review.fk_userId
		};
	}
}