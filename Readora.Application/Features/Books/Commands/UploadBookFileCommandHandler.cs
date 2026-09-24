using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Readora.Application.Common;
using Readora.Application.Interfaces.Persistence;
using Readora.Application.Interfaces.Services;
using Readora.Domain.Entities;

namespace Readora.Application.Features.Books.Commands;

public class UploadBookFileCommandHandler : IRequestHandler<UploadBookFileCommand, Result<string>>
{
	private readonly IUnitOfWork _unitOfWork;

	private readonly IStorageService _storageService;

	public UploadBookFileCommandHandler(IUnitOfWork unitOfWork, IStorageService storageService)
	{
		_unitOfWork = unitOfWork;
		_storageService = storageService;
	}

	public async Task<Result<string>> Handle(UploadBookFileCommand request, CancellationToken cancellationToken)
	{
		Book book = await _unitOfWork.Repository<Book>().GetByIdAsync(request.BookId, cancellationToken);
		if (book == null)
		{
			return Result.Failure<string>(Error.NotFound("Books.NotFound", "Book not found."));
		}
		string folderName = (request.IsCoverImage ? "covers" : "pdfs");
		string prefix = $"{folderName}/book-{request.BookId}";
		string fileKey = await _storageService.UploadFileAsync(request.FileStream, request.FileName, request.ContentType, prefix);
		string fileUrl = _storageService.GetFileUrl(fileKey);
		if (request.IsCoverImage)
		{
			book.CoverImageUrl = fileUrl;
		}
		else
		{
			book.FileKey = fileKey;
		}
		_unitOfWork.Repository<Book>().Update(book);
		await _unitOfWork.SaveChangesAsync(cancellationToken);
		return Result.Success(fileUrl);
	}
}
