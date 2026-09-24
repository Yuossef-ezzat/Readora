namespace Readora.Application.Common.Caching;

public interface ICacheInvalidatorCommand
{
	string[] CacheKeys { get; }
}
