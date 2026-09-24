using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Readora.Application.Common;
using Readora.Application.Features.Borrowings.Queries;
using Readora.Application.Interfaces.Persistence;
using Readora.Application.Interfaces.Services;
using Readora.Domain.Entities;

namespace Readora.Application.Features.Borrowings.Commands;

public class ProcessOverdueBorrowingsCommandHandler : IRequestHandler<ProcessOverdueBorrowingsCommand, Result>
{
	private readonly IUnitOfWork _unitOfWork;

	private readonly ILogger<ProcessOverdueBorrowingsCommandHandler> _logger;

	private readonly INotificationService _notificationService;

	public ProcessOverdueBorrowingsCommandHandler(IUnitOfWork unitOfWork, ILogger<ProcessOverdueBorrowingsCommandHandler> logger, INotificationService notificationService)
	{
		_unitOfWork = unitOfWork;
		_logger = logger;
		_notificationService = notificationService;
	}

	public async Task<Result> Handle(ProcessOverdueBorrowingsCommand request, CancellationToken cancellationToken)
	{
		OverdueBorrowingsSpecification spec = new OverdueBorrowingsSpecification();
		foreach (Borrowing borrowing in await _unitOfWork.Repository<Borrowing>().ListAsync(spec, cancellationToken))
		{
			_logger.LogInformation("Borrowing {BorrowingId} is overdue. User {UserId} needs to return BookCopy {BookCopyId}.", borrowing.Id, borrowing.UserId, borrowing.BookCopyId);
			await _notificationService.SendNotificationToUserAsync(borrowing.UserId, $"Your borrowed book (Copy ID: {borrowing.BookCopyId}) is overdue! Please return it immediately.", cancellationToken);
		}
		return Result.Success();
	}
}
