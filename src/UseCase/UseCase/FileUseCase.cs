using Domain.Clients;
using Domain.ValueObjects;
using Domain.ValueObjects.Extensions;
using UseCase.UseCase.Interfaces;

namespace UseCase.UseCase;

public class FileUseCase : IFileUseCase
{
    private readonly IBucketClient _bucketClient;
    public FileUseCase(IBucketClient bucketClient)
    {
        _bucketClient = bucketClient;   
    }
    public Task<StorageFile> UploadZippedFile(string requestId, IEnumerable<FileModel> files, CancellationToken cancellationToken)
    {
        var zippedFile = files.ZipFiles();

        return _bucketClient.UploadAsync(requestId,zippedFile, cancellationToken);
    }

    public Task<FileModel> DownloadAsync(StorageFile file, CancellationToken cancellationToken)
    { 
        return _bucketClient.DownloadAsync(file, cancellationToken);
    }
}
