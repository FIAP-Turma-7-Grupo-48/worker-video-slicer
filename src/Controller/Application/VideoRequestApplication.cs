using Controller.Application.Interfaces;
using Controller.Dto;
using Controller.Utils.Interfaces;
using UseCase.UseCase.Interfaces;

namespace Controller.Application;

public class VideoRequestApplication : IVideoRequestApplication
{
    private readonly IFileUseCase _fileUseCase;
    private readonly IVideoUseCase _videoUseCase;
    private readonly IControllerVariables _controllerVariables;
    public VideoRequestApplication(IFileUseCase fileUseCase, IVideoUseCase videoSlicerUseCase, IControllerVariables controllerVariables)
    {
        _fileUseCase = fileUseCase;
        _videoUseCase = videoSlicerUseCase;
        _controllerVariables = controllerVariables;
    }

    public async Task ProcessVideo(VideoRequestProcessRequestDto request, CancellationToken cancellationToken)
    {
        var file = await _fileUseCase.DownloadAsync(request.StorageFile, cancellationToken);

        var videoSlicedFiles = await _videoUseCase.SliceAsync(file, _controllerVariables.VideoSlicerInterval, cancellationToken);

        var storageFile = await _fileUseCase.UploadZippedFile(request.RequestId, videoSlicedFiles, cancellationToken);
    }
}
