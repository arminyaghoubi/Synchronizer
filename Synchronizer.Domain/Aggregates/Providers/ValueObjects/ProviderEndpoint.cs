using Synchronizer.Domain.Common;

namespace Synchronizer.Domain.Aggregates.Providers.ValueObjects;

public sealed record ProviderEndpoint : ValueObject
{
    public string Value { get; } = null!;
    public int TimeoutSeconds { get; }

    private ProviderEndpoint(string url, int timeoutSeconds)
    {
        Value = url;
        TimeoutSeconds = timeoutSeconds;
    }

    public static Result<ProviderEndpoint> Create(string url, int timeoutSeconds)
    {
        if (string.IsNullOrEmpty(url))
            return Result<ProviderEndpoint>.Failure("Url is required for provider");

        if (timeoutSeconds <= 0 || timeoutSeconds > 30)
            return Result<ProviderEndpoint>.Failure("TimeoutSeconds should be between 1 and 30 seconds");

        if (!Uri.TryCreate(url, UriKind.Absolute, out _))
            return Result<ProviderEndpoint>.Failure("Invalid url value");

        ProviderEndpoint endpoint = new(url, timeoutSeconds);
        return Result<ProviderEndpoint>.Success(endpoint);
    }
}