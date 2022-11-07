using System.ComponentModel.DataAnnotations;

using modkaz.DBs.Entities;


namespace modkaz.Backend.Models.Entity;

/// <summary>
/// <para>Post view model for listing purposes.</para>
/// <para>Static members are thread safe, instance members are not.</para>
/// </summary>
public class PostForListing
{
	/// <summary>
	/// Post ID.
	/// </summary>
	public int Id { get; set; }

	/// <summary>
	/// Title.
	/// </summary>
	[Required(ErrorMessage = "Title is required.")]
	public string Title { get; set; }

	/// <summary>
	/// Content.
	/// </summary>
	[Required(ErrorMessage = "Post content is required.")]
	public string Content { get; set; }
	
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
	/// <param name="post">DB entity to create from.</param>
	/// <returns>A corresponding instance.</returns>
	public static PostForListing DatabaseToObject(Posts post)
	{
		return new PostForListing
		{
			Id = post.id,
			Title = post.title,
			Content = post.content,
			CreatedTimestamp = post.created_timestamp,
			FK_UserID = post.fk_userId
		};
	}
}

public class PostForCreateUpdate
{
	/// <summary>
	/// Title.
	/// </summary>
	[Required(ErrorMessage = "Post title is required.")]
	public string Title { get; set; }

	/// <summary>
	/// Content.
	/// </summary>
	[Required(ErrorMessage = "Post content is required.")]
	public string Content { get; set; }
	
	/// <summary>
	/// User ID.
	/// </summary>
	[Required(ErrorMessage = "User ID is required.")]
	public int FK_UserID { get; set; }
	
	
	/// <summary>
	/// Copy data to DB entity. Will not copy ID field.
	/// </summary>
	/// <param name="post">DB entity to fill in.</param>
	public void ToDatabase(Posts post)
	{
		post.content = Content;
		post.title = Title;
		post.fk_userId = FK_UserID;
	}
}