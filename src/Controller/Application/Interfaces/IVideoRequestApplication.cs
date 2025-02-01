using Controller.Dto;

namespace Controller.Application.Interfaces;

public interface IVideoRequestApplication
{
    Task ProcessVideo(VideoRequestProcessRequestDto request, CancellationToken cancellationToken);
}
