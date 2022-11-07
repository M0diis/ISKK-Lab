using System.ComponentModel.DataAnnotations;

using modkaz.DBs.Entities;


namespace modkaz.Backend.Models.Entity;

/// <summary>
/// <para>User view model for listing purposes.</para>
/// <para>Static members are thread safe, instance members are not.</para>
/// </summary>
public class UserBindingModel
{
	/// <summary>
	/// User ID.
	/// </summary>
	public int Id { get; set; }

	/// <summary>
	/// Name.
	/// </summary>
	[Required(ErrorMessage = "Name is required.")]
	public string Name { get; set; }

	/// <summary>
	/// Email.
	/// </summary>
	[Required(ErrorMessage = "Email is required.")]
	public string Email { get; set; }
	
	/// <summary>
	/// Password
	/// </summary>
	[Required(ErrorMessage = "Password is required.")]
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
	public static UserBindingModel DatabaseToObject(Users user)
	{
		return new UserBindingModel
		{
			Id = user.id,
			Name = user.name,
			Password = user.password,
			CreatedTimestamp = user.created_timestamp,
			Admin = user.admin
		};
	}
}