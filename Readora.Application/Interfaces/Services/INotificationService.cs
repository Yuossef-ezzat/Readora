using System.Threading;
using System.Threading.Tasks;

namespace Readora.Application.Interfaces.Services;

public interface INotificationService
{
	Task SendNotificationToUserAsync(int userId, string message, CancellationToken cancellationToken = default(CancellationToken));

	Task SendNotificationToAllAsync(string message, CancellationToken cancellationToken = default(CancellationToken));
}
