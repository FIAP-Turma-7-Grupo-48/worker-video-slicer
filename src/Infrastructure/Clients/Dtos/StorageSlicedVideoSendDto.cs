using Domain.ValueObjects;

namespace Infrastructure.Clients.Dtos;

public record StorageSlicedVideoSendDto
{
    public string RequestId { get; init; } = string.Empty;
    public StorageFile file { get; init; }
}
