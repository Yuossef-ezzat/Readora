using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Readora.API.Hubs;

[Authorize]
public class NotificationHub : Hub
{
	public override async Task OnConnectedAsync()
	{
		await base.OnConnectedAsync();
	}
}
