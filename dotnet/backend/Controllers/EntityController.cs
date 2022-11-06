using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using modkaz.DBs;
using modkaz.DBs.Entities;
using modkaz.Backend.Models.Entity;


namespace modkaz.Backend.Controllers.Entity;

/// <summary>
/// <para>Implements restfull API for working with entities</para>
/// <para>Thread safe.</para>
/// </summary>
[ApiController]
[Route("backend/entity")]
public class EntityController : ControllerBase
{
	/// <summary>
	/// Logger.
	/// </summary>
	private readonly ILogger<EntityController> _logger;

	private readonly MyDatabase _database;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="logger">Logger to use. Injected.</param>
	/// <param name="database">Database context</param>
	public EntityController(ILogger<EntityController> logger, MyDatabase database)
	{
		_logger = logger;
		_database = database;
	}

	/// <summary>
	/// List entities.
	/// </summary>
	/// <returns>A list of entities.</returns>
	/// <response code="500">On exception.</response>
	[HttpGet("list")]
	// [Authorize(Roles = "user")]
	[ProducesResponseType(typeof(List<EntityForL>), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public IActionResult List()
	{
		//load entities from DB, convert into listing views
		// var ents =
		// 	_dataContext.DemoEntities
		// 		.OrderBy(it => it.id)
		// 		.Select(it => EntityForL.FromDb(it))
		// 		.ToList();

		//
		
		testobj obj = new testobj();
		
		obj.id = 1;
		
		obj.name = "test";
		
		List<testobj> list = new List<testobj>();
		
		list.Add(obj);
		list.Add(obj);
		list.Add(obj);

		return Ok(list);
	}
	
	class testobj 
	{
		public string name { get; set; }
		public int id { get; set; }
	}

	/// <summary>
	/// Loads data for a single entity.
	/// </summary>
	/// <param name="id">ID of the entity to load.</param>
	/// <returns>Data of entity loaded.</returns>
	/// <response code="404">If entity with given ID can't be loaded.</response>
	/// <response code="500">On exception.</response>
	[HttpGet("load")]
	[Authorize(Roles = "user")]
	[ProducesResponseType(typeof(List<EntityForCU>), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public IActionResult Load(int? id)
	{
		//validate inputs
		if (id == null)
			throw new ArgumentException("Argument 'id' is null.");

		//load entity from DB, convert into create/update view
		var ent =
			_database.DemoEntities
				.Where(it => it.id == id.Value)
				.Select(it => EntityForCU.FromDb(it))
				.FirstOrDefault();

		//entity not found?
		if (ent == null)
			return NotFound();

		//
		return Ok(ent);
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
	public IActionResult Create(EntityForCU ent)
	{
		//Validate model
		if (!ModelState.IsValid)
		{
			return BadRequest(ModelState);
		}

		//save to DB
		var dbEnt = new DemoEntities();
		ent.CopyToDb(dbEnt);

		_database.DemoEntities.Add(dbEnt);
		_database.SaveChanges();

		//
		return Ok();
	}

	/// <summary>
	/// Updates given entity.
	/// </summary>
	/// <param name="ent">Data of entity to update.</param>
	/// <returns>ID of new entity</returns>
	/// <response code="400">On validation failure.</response>
	/// <response code="500">On exception.</response>
	[HttpPost("update")]
	[Authorize(Roles = "user")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public IActionResult Update(EntityForCU ent)
	{
		//Validate model
		if (!ModelState.IsValid)
		{
			return BadRequest(ModelState);
		}

		//save to DB
		//load entity to update
		var dbEnt =
			_database.DemoEntities
				.Where(it => it.id == ent.Id)
				.FirstOrDefault();

		//load failed? abort
		if (dbEnt == null)
			return BadRequest("Entity not found in DB.");

		//update DB entity
		ent.CopyToDb(dbEnt);
		_database.Update(dbEnt);

		//
		_database.SaveChanges();

		//
		return Ok();
	}

	/// <summary>
	/// Deletes given entity.
	/// </summary>
	/// <param name="id">ID of the entity to delete.</param>
	/// <response code="404">If entity with given ID can't be found.</response>
	/// <response code="400">If entity with given ID is not marked as deletable.</response>
	/// <response code="500">On exception.</response>
	[HttpGet("delete")]
	[Authorize(Roles = "user")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public IActionResult Delete(int? id)
	{
		//validate inputs
		if (id == null)
			return BadRequest("Argument 'id' is null.");

		//
		//load entity from DB, convert into create/update view
		var ent =
			_database.DemoEntities
				.Where(it => it.id == id.Value)
				.FirstOrDefault();

		//entity not found?
		if (ent == null)
			return NotFound();

		//entity not deletable
		if (!ent.deletable)
			return BadRequest();

		//delete entity
		_database.DemoEntities.Remove(ent);
		_database.SaveChanges();

		//
		return Ok(ent);
	}
}
