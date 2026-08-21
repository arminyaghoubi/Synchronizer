using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Synchronizer.Application.Abstractions;
using Synchronizer.Application.Contracts;
using Synchronizer.Domain.Aggregates.Providers.ValueObjects;
using Synchronizer.Domain.Common;
using System.Text.Json;

namespace Synchronizer.Infrastructure.Adapters.TMobile;

public sealed class TMobileAdapter(
    ILogger<TMobileAdapter> logger) : IProviderAdapter
{
    public async Task<Result<IReadOnlyList<ExternalProductDto>>> FetchProductAsync(ProviderEndpoint endpoint, CancellationToken cancellation)
    {
        // Mock data
        List<ExternalProductDto> externalProducts = new()
        {
            new("1","T‑Mobile 5G Home Internet",20,10,null),
            new("2","T‑Mobile 4G Home Internet",17,10,null),
            //new("3","T‑Mobile 3G Home Internet",12,10,null),
            //new("4","T‑Mobile 2G Home Internet",5,10,null),
        };

        return Result<IReadOnlyList<ExternalProductDto>>.Success(externalProducts);
    }
}