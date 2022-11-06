using System.ComponentModel.DataAnnotations;

using modkaz.DBs.Entities;


namespace modkaz.Backend.Models.Entity;

/// <summary>
/// <para>User view model for listing purposes.</para>
/// <para>Static members are thread safe, instance members are not.</para>
/// </summary>
public class UserForListing
{
	/// <summary>
	/// User ID.
	/// </summary>
	public int Id { get; set; }

	/// <summary>
	/// Name.
	/// </summary>
	public string Name { get; set; }

	/// <summary>
	/// Email.
	/// </summary>
	public string Email { get; set; }
	
	/// <summary>
	/// Password
	/// </summary>
	public string Password { get; set; }

	/// <summary>
	/// Admin.
	/// </summary>
	public bool Admin { get; set; }
	
	/// <summary>
	/// Created timestamp.
	/// </summary>
	public DateTime CreatedTimestamp { get; set; }
	
	/// <summary>
	/// Create instance from DB entity.
	/// </summary>
	/// <param name="user">DB entity to create from.</param>
	/// <returns>A corresponding instance.</returns>
	public static UserForListing DatabaseToObject(Users user)
	{
		return new UserForListing
		{
			Id = user.id,
			Name = user.name,
			Password = user.password,
			CreatedTimestamp = user.created_timestamp,
			Admin = user.admin
		};
	}
}