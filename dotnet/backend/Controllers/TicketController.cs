using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using modkaz.Backend.Interfaces;
using modkaz.Backend.Interfaces.Service;
using modkaz.Backend.Models;

namespace modkaz.Backend.Controllers;

/// <summary>
/// <para>Implements restfull API for working with entities</para>
/// <para>Thread safe.</para>
/// </summary>
[ApiController]
[Route("backend/ticket")]
public class TicketController : ControllerBase
{
	/// <summary>
	/// Logger.
	/// </summary>
	private readonly ILogger<TicketController> _logger;

	/// <summary>
	/// Tickets service.
	/// </summary>
	private readonly ITicketsService _ticketsService;
	
	/// <summary>
	/// Messages-Tickets service.
	/// </summary>
	private readonly IMessagesTicketsService _messagesTicketsService;
	
	
	/// <summary>
	/// Messages service.
	/// </summary>
	private readonly IMessagesService _messagesService;
	
	/// <summary>
	/// Users service.
	/// </summary>
	private readonly IUsersService _usersService;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="logger">Logger to use. Injected.</param>
	/// <param name="ticketsService">Tickets service to use. Injected.</param>
	/// <param name="usersService">Users service to use. Injected.</param>
	/// <param name="messagesTicketsService">Messages-tickets service to use. Injected.</param>
	/// <param name="messagesService">Messages service to use. Injected</param>
	public TicketController(ILogger<TicketController> logger, ITicketsService ticketsService,
		IUsersService usersService, IMessagesTicketsService messagesTicketsService, IMessagesService messagesService)
	{
		_logger = logger;
		_ticketsService = ticketsService;
		_usersService = usersService;
		_messagesTicketsService = messagesTicketsService;
		_messagesService = messagesService;
	}
	
	/// <summary>
	/// List entities.
	/// </summary>
	/// <returns>A list of entities.</returns>
	/// <response code="500">On exception.</response>
	[HttpGet("load")]
	[ProducesResponseType(typeof(List<TicketForListing>), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> Load(int? ticketId)
	{
		_logger.LogInformation("Got request to /backend/ticket/load?ticketId={TicketId}", ticketId);

		if (ticketId == null)
		{
			return BadRequest("TicketId is null");
		}

		var ticket = (await _ticketsService.GetOneByIdAsync(ticketId.Value));

		var user = (await _usersService.GetAllAsync())
			.OrderBy(it => it.id)
			.Where(x => x.id == ticket.fk_userId)
			.Select(UserForListing.DatabaseToObject)
			.FirstOrDefault();

		if (user == null)
		{
			return new StatusCodeResult(500);
		}

		return Ok(ticket);
	}

	/// <summary>
	/// List entities.
	/// </summary>
	/// <returns>A list of entities.</returns>
	/// <response code="500">On exception.</response>
	[HttpGet("list")]
	[ProducesResponseType(typeof(List<TicketForListing>), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> List()
	{
		_logger.LogInformation("Got request to /backend/ticket/list");
		
		var tickets = (await _ticketsService.GetAllAsync())
			.OrderBy(it => it.id)
			.Select(TicketForListing.DatabaseToObject)
			.ToList();
		
		var users = (await _usersService.GetAllAsync())
			.OrderBy(it => it.id)
			.Select(UserForListing.DatabaseToObject)
			.ToList();
		
		foreach (var ticket in tickets)
		{
			foreach (var user in users.Where(user => ticket.FK_UserID == user.Id))
			{
				ticket.UserName = user.Name;
			}
		}
		
		_logger.LogInformation("Listed {Count} ticket entities", tickets.Count);
		
		return Ok(tickets);
	}
	
	/// <summary>
	/// Loads data for a single entity.
	/// </summary>
	/// <param name="userId">ID of the ticket to load.</param>
	/// <returns>Data of entity loaded.</returns>
	/// <response code="404">If entity with given ID can't be loaded.</response>
	/// <response code="500">On exception.</response>
	[HttpGet("list/by-user")]
	[Authorize(Roles = "user")]
	[ProducesResponseType(typeof(List<TicketForListing>), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> ListByUser(int? userId)
	{
		_logger.LogInformation("Got request to /backend/ticket/list/by-user with userId={UserId}", userId);
		
		if (userId == null)
		{
			throw new ArgumentException("Argument 'userId' is null.");
		}

		var ticketsByUser = (await _ticketsService.GetTicketsByUserIdAsync(userId.Value))
			.Select(TicketForListing.DatabaseToObject)
			.ToList();

		_logger.LogInformation("Loaded {Count} ticket entities", ticketsByUser.Count);

		return Ok(ticketsByUser);
	}
	
	/// <summary>
	/// List entities.
	/// </summary>
	/// <returns>A list of entities.</returns>
	/// <response code="500">On exception.</response>
	[HttpGet("messages")]
	[ProducesResponseType(typeof(List<TicketForListing>), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> TicketMessages(int? ticketId)
	{
		_logger.LogInformation("Got request to /backend/ticket/messages?ticketId={TicketId}", ticketId);

		if (ticketId == null)
		{
			return BadRequest("TicketId is null");
		}
		
		var messageIds = (await _messagesTicketsService.GetByTicketIdAsync(ticketId.Value))
			.OrderBy(it => it.fk_messageId)
			.Select(it => it.fk_messageId)
			.ToList();
		
		var messages = (await _messagesService.GetMessagesByIds(messageIds))
			.OrderBy(it => it.id)
			.Select(MessageForListing.DatabaseToObject)
			.ToList();

		var users = (await _usersService.GetAllAsync());
		
		foreach (var message in messages)
		{
			foreach (var user in users.Where(user => message.FK_UserID == user.id))
			{
				message.UserName = user.name;
			}
		}

		_logger.LogInformation("Listed {Count} ticket message entities", messages.Count);
		
		return Ok(messages);
	}
}
