using Domain.ValueObjects;

namespace UseCase.UseCase.Interfaces;

public interface IVideoUseCase
{
    Task<IEnumerable<FileModel>> SliceAsync(FileModel file, TimeSpan interval, CancellationToken cancellationToken);
}
