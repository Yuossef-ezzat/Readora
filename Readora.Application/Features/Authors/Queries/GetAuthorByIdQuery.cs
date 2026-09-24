using MediatR;
using Readora.Application.Common;
using Readora.Application.DTOs.Authors;

namespace Readora.Application.Features.Authors.Queries;

public record GetAuthorByIdQuery(int Id) : IRequest<Result<AuthorDto>>, IBaseRequest;
