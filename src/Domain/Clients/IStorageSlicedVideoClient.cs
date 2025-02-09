using Domain.ValueObjects;

namespace Domain.Clients;

public interface IStorageSlicedVideoClient
{
    Task SendAsync(string requestId, StorageFile file, CancellationToken cancellationToken);
}
