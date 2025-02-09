using Domain.Clients;
using Domain.ValueObjects;
using Domain.ValueObjects.Extensions;
using UseCase.UseCase.Interfaces;

namespace UseCase.UseCase;

public class FileUseCase : IFileUseCase
{
    private readonly IBucketClient _bucketClient;
    private readonly IStorageSlicedVideoClient _storageSlicedVideoClient;
    public FileUseCase(IBucketClient bucketClient, IStorageSlicedVideoClient storageSlicedVideoClient)
    {
        _bucketClient = bucketClient;
        _storageSlicedVideoClient = storageSlicedVideoClient;
    }
    public async Task<StorageFile> UploadZippedFile(string requestId, IEnumerable<FileModel> files, CancellationToken cancellationToken)
    {
        var zippedFile = files.ZipFiles();

        var fileKey = $"zippedFile_{requestId}";
        var storageFile = await _bucketClient.UploadAsync(fileKey, zippedFile, cancellationToken);

        await _storageSlicedVideoClient.SendAsync(requestId, storageFile, cancellationToken);

        return storageFile;
    }

    public Task<FileModel> DownloadAsync(StorageFile file, CancellationToken cancellationToken)
    { 
        return _bucketClient.DownloadAsync(file, cancellationToken);
    }
}
