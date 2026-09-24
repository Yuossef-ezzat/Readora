using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Readora.Application.Common;
using Readora.Application.Interfaces.Persistence;
using Readora.Domain.Entities;

namespace Readora.Application.Features.Categories.Commands;

public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Result>
{
	private readonly IUnitOfWork _unitOfWork;

	public UpdateCategoryCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Result> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
	{
		Category category = await _unitOfWork.Repository<Category>().GetByIdAsync(request.Id, cancellationToken);
		if (category == null)
		{
			return Result.Failure(Error.NotFound("Categories.NotFound", "Category not found."));
		}
		category.Name = request.Name;
		category.Description = request.Description;
		_unitOfWork.Repository<Category>().Update(category);
		await _unitOfWork.SaveChangesAsync(cancellationToken);
		return Result.Success();
	}
}
