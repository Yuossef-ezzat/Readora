using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Readora.Application.Common;
using Readora.Application.DTOs.Categories;
using Readora.Application.Interfaces.Persistence;
using Readora.Domain.Entities;

namespace Readora.Application.Features.Categories.Queries;

public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, Result<CategoryDto>>
{
	private readonly IUnitOfWork _unitOfWork;

	public GetCategoryByIdQueryHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Result<CategoryDto>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
	{
		Category category = await _unitOfWork.Repository<Category>().GetByIdAsync(request.Id, cancellationToken);
		if (category == null)
		{
			return Result.Failure<CategoryDto>(Error.NotFound("Categories.NotFound", "Category not found."));
		}
		return Result.Success(new CategoryDto
		{
			Id = category.Id,
			Name = category.Name,
			Description = category.Description
		});
	}
}
