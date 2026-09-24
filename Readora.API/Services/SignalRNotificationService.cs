using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Readora.API.Hubs;
using Readora.Application.Interfaces.Services;

namespace Readora.API.Services;

public class SignalRNotificationService : INotificationService
{
	private readonly IHubContext<NotificationHub> _hubContext;

	public SignalRNotificationService(IHubContext<NotificationHub> hubContext)
	{
		_hubContext = hubContext;
	}

	public async Task SendNotificationToUserAsync(int userId, string message, CancellationToken cancellationToken = default(CancellationToken))
	{
		await _hubContext.Clients.User(userId.ToString()).SendAsync("ReceiveNotification", message, cancellationToken);
	}

	public async Task SendNotificationToAllAsync(string message, CancellationToken cancellationToken = default(CancellationToken))
	{
		await _hubContext.Clients.All.SendAsync("ReceiveNotification", message, cancellationToken);
	}
}
