using System.ComponentModel.DataAnnotations;

using modkaz.DBs.Entities;


namespace modkaz.Backend.Models.Entity;

/// <summary>
/// <para>Entity view model for listing purposes.</para>
/// <para>Static members are thread safe, instance members are not.</para>
/// </summary>
public class EntityForL
{
	/// <summary>
	/// Entity ID.
	/// </summary>
	public int Id {get; set;}

	/// <summary>
	/// Date.
	/// </summary>
	public string Date {get; set;}

	/// <summary>
	/// Name.
	/// </summary>
	public string Name {get; set;}

	/// <summary>
	/// Condition. In range [0;10].
	/// </summary>
	public int Condition {get; set;}

	/// <summary>
	/// Indicates if entity is deletable.
	/// </summary>
	public bool Deletable {get; set;}


	/// <summary>
	/// Create instance from DB entity.
	/// </summary>
	/// <param name="dbEnt">DB entity to create from.</param>
	/// <returns>A corresponding instance.</returns>
	public static EntityForL FromDb(DemoEntities dbEnt)
	{
		var inst = 
			new EntityForL() {
				Id = dbEnt.id,
				Date = String.Format("{0:yyyy-MM-dd}", dbEnt.date),
				Name = dbEnt.name,
				Condition = dbEnt.condition,
				Deletable = dbEnt.deletable
			};

		return inst;
	}
}

/// <summary>
/// <para>Entity view model for create/update purposes.</para>
/// <para>Static members are thread safe, instance members are not.</para>
/// </summary>
public class EntityForCU
{
	/// <summary>
	/// Entity ID. Ignored when creating.
	/// </summary>
	public int Id {get; set;}

	/// <summary>
	/// Date. Required.
	/// </summary>
	[Required]
	public DateTime Date {get; set;}

	/// <summary>
	/// Name. Required.
	/// </summary>
	[Required]
	public string Name {get; set;}

	/// <summary>
	/// Condition. In range [0;10].
	/// </summary>
	[Range(0, 10)]
	public int Condition {get; set;}

	/// <summary>
	/// Indicates if entity is deletable.
	/// </summary>
	public bool Deletable {get; set;}


	/// <summary>
	/// Create instance from DB entity.
	/// </summary>
	/// <param name="dbEnt">DB entity to create from.</param>
	/// <returns>A corresponding instance.</returns>
	public static EntityForCU FromDb(DemoEntities dbEnt)
	{
		var inst = 
			new EntityForCU() {
				Id = dbEnt.id,
				Date = dbEnt.date,
				Name = dbEnt.name,
				Condition = dbEnt.condition,
				Deletable = dbEnt.deletable
			};

		return inst;
	}

	/// <summary>
	/// Copy data to DB entity. Will not copy ID field.
	/// </summary>
	/// <param name="dst">DB entity to fill in.</param>
	public void CopyToDb(DemoEntities dst)
	{
		dst.date = Date;
		dst.name = Name;
		dst.condition = Condition;
		dst.deletable = Deletable;
	}
}