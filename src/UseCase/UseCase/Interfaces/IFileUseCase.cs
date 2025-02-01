using Domain.ValueObjects;

namespace UseCase.UseCase.Interfaces;

public interface IFileUseCase
{
    Task<StorageFile> UploadZippedFile(string requestId, IEnumerable<FileModel> files, CancellationToken cancellationToken);
    Task<FileModel> DownloadAsync(StorageFile file, CancellationToken cancellationToken);
}
