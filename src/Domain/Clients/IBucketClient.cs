using Domain.ValueObjects;

namespace Domain.Clients;

public interface IBucketClient
{
    Task<StorageFile> UploadAsync(string fileKey, FileModel file, CancellationToken cancellationToken);
    Task<FileModel> DownloadAsync(StorageFile storageFile, CancellationToken cancellationToken);
}
