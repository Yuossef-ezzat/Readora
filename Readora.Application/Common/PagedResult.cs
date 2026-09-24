using System.Collections.Generic;

namespace Readora.Application.Common;

public class PagedResult<T>
{
	public IReadOnlyList<T> Items { get; }

	public int TotalCount { get; }

	public int Page { get; }

	public int PageSize { get; }

	public bool HasNextPage => Page * PageSize < TotalCount;

	public bool HasPreviousPage => Page > 1;

	public PagedResult(IReadOnlyList<T> items, int totalCount, int page, int pageSize)
	{
		Items = items;
		TotalCount = totalCount;
		Page = page;
		PageSize = pageSize;
	}
}
