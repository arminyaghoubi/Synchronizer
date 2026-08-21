namespace Synchronizer.Application.Contracts;

public sealed record ExternalProductDto(
    string ExternalId,
    string Name,
    decimal Price,
    decimal VAT,
    string ExtraData);
