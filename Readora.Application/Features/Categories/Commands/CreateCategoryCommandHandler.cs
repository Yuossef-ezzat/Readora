using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Readora.Application.Common;
using Readora.Application.Interfaces.Persistence;
using Readora.Domain.Entities;

namespace Readora.Application.Features.Categories.Commands;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Result<int>>
{
	private readonly IUnitOfWork _unitOfWork;

	public CreateCategoryCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Result<int>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
	{
		Category category = new Category
		{
			Name = request.Name,
			Description = request.Description
		};
		await _unitOfWork.Repository<Category>().AddAsync(category, cancellationToken);
		await _unitOfWork.SaveChangesAsync(cancellationToken);
		return Result.Success(category.Id);
	}
}
