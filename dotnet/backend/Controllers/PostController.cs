using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using modkaz.Backend.Interfaces;
using modkaz.DBs;
using modkaz.Backend.Models.Entity;
using modkaz.Backend.Services;
using modkaz.DBs.Entities;

namespace modkaz.Backend.Controllers.Post;

/// <summary>
/// <para>Implements restfull API for working with entities</para>
/// <para>Thread safe.</para>
/// </summary>
[ApiController]
[Route("backend/post")]
public class PostController : ControllerBase
{
	/// <summary>
	/// Logger.
	/// </summary>
	private readonly ILogger<PostController> _logger;

	private readonly IUsersService _usersService;
	
	private readonly IPostsService _postsService;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="logger">Logger to use.</param>
	/// <param name="usersService">Users service.</param>
	/// <param name="postsService">Posts service</param>
	public PostController(ILogger<PostController> logger, IUsersService usersService, IPostsService postsService)
	{
		_logger = logger;
		_usersService = usersService;
		_postsService = postsService;
	}

	/// <summary>
	/// List entities.
	/// </summary>
	/// <returns>A list of entities.</returns>
	/// <response code="500">On exception.</response>
	[HttpGet("list")]
	// [Authorize(Roles = "user")]
	[ProducesResponseType(typeof(List<PostForListing>), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> List()
	{
		_logger.LogInformation("Got request to /backend/post/list");
		
		var users = (await _usersService.GetUsersAsync())
			.OrderBy(it => it.id)
			.Select(UserForListing.DatabaseToObject)
			.ToList();

		var posts = (await _postsService.GetPostsAsync())
			.OrderBy(it => it.id)
			.Select(PostForListing.DatabaseToObject)
			.ToList();
		
		foreach (var post in posts)
		{
			foreach (var user in users.Where(user => post.FK_UserID == user.Id))
			{
				post.UserName = user.Name;
			}
		}
		_logger.LogInformation("Listed {Count} entities", posts.Count);
		
		return Ok(posts);
	}
	
	/// <summary>
	/// Creates new entity.
	/// </summary>
	/// <returns>ID of new entity</returns>
	/// <response code="400">On validation failure.</response>
	/// <response code="500">On exception.</response>
	[HttpPost("create")]
	[Authorize(Roles = "user")]
	[ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public IActionResult Create(PostForCreateUpdate post)
	{
		if (!ModelState.IsValid)
		{
			return BadRequest(ModelState);
		}

		_postsService.CreatePost(post.ToDatabase());
		
		return Ok();
	}
	
	/// <summary>
	/// Deletes given entity.
	/// </summary>
	/// <param name="id">ID of the entity to delete.</param>
	/// <response code="404">If entity with given ID can't be found.</response>
	/// <response code="500">On exception.</response>
	[HttpGet("delete")]
	[Authorize(Roles = "user")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> Delete(int? id)
	{
		_logger.LogInformation("Got request to /backend/post/delete?id={ID}", id);
		
		if (id == null)
		{
			return BadRequest("Argument 'id' is null.");
		}

		var ent = await _postsService.GetPostByIdAsync(id.Value);
		
		if (ent == null)
		{
			return NotFound();
		}
		
		_postsService.DeletePost(ent);
		
		return Ok(ent);
	}
}
