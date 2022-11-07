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
	public string Title { get; set; }

	/// <summary>
	/// Content.
	/// </summary>
	public string Content { get; set; }
	
	/// <summary>
	/// Created Timestamp.
	/// </summary>
	public DateTime CreatedTimestamp { get; set; }

	/// <summary>
	/// User ID.
	/// </summary>
	public long FK_UserID { get; set; }
	
	public String UserName { get; set; }

	/// <summary>
	/// Create instance from DB entity.
	/// </summary>
	/// <param name="postEntity">DB entity to create from.</param>
	/// <returns>A corresponding instance.</returns>
	public static PostForListing DatabaseToObject(PostsEntity postEntity)
	{
		return new PostForListing
		{
			Id = postEntity.id,
			Title = postEntity.title,
			Content = postEntity.content,
			CreatedTimestamp = postEntity.created_timestamp,
			FK_UserID = postEntity.fk_userId
		};
	}
}

public class PostForCreateUpdate
{
	/// <summary>
	/// Title.
	/// </summary>
	public string Title { get; set; }

	/// <summary>
	/// Content.
	/// </summary>
	public string Content { get; set; }
	
	/// <summary>
	/// User ID.
	/// </summary>
	public int FK_UserID { get; set; }
	
	
	/// <summary>
	/// Copy data to DB entity. Will not copy ID field.
	/// </summary>
	/// <param name="postEntity">DB entity to fill in.</param>
	public void ToDatabaseObject(PostsEntity postEntity)
	{
		postEntity.content = Content;
		postEntity.title = Title;
		postEntity.fk_userId = FK_UserID;
		postEntity.created_timestamp = DateTime.Now;
	}
	
	/// <summary>
	/// Create a new DB entity.
	/// </summary>
	public PostsEntity ToDatabaseObject()
	{
		PostsEntity postEntity = new PostsEntity();
		
		ToDatabaseObject(postEntity);
		
		return postEntity;
	}
}