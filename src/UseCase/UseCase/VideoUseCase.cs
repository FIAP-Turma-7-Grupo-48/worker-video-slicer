using Domain.ValueObjects;
using FFMpegCore;
using System.Drawing;
using UseCase.UseCase.Interfaces;

namespace UseCase.UseCase;

public class VideoUseCase : IVideoUseCase
{
    public async Task<IEnumerable<FileModel>> SliceAsync(FileModel file, TimeSpan interval, CancellationToken cancellationToken)
    {
        //ToDo: Implementar validação se o arquivo realmente é video
        var folderName = Guid.NewGuid().ToString();
        Directory.CreateDirectory(folderName);
        var path = $@"{folderName}\{file.FileName}";
        try
        {
            File.WriteAllBytes(path, file.Data);

            var duration = await GetVideoDurationAsync(path, cancellationToken);

            var tasks = new List<Task>();
            for (var currentTime = TimeSpan.Zero; currentTime < duration; currentTime += interval)
            {
                var task = GetSnapshotAsync(currentTime, path, folderName, cancellationToken);
                tasks.Add(task);
            }

            await Task.WhenAll(tasks);

            List<FileModel> response = [];
            foreach (var imgPath in Directory.EnumerateFiles(folderName, "*.png"))
            {
                var mediaFile = await  ReadFileAsync(imgPath, cancellationToken);
                response.Add(mediaFile);
            }

            return response;
        }
        finally
        {
            DirectoryInfo di = new DirectoryInfo(folderName);
            di.Delete(true);
        }
    }

    private async Task GetSnapshotAsync(TimeSpan currentTime, string path, string folderName, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Processando frame: {currentTime}");

        var outputPath = Path.Combine(folderName, $"frame_at_{currentTime.TotalSeconds}{Guid.NewGuid()}.jpg");
        await FFMpeg.SnapshotAsync(path, outputPath, new Size(1920, 1080), currentTime);
    }

    private async Task<FileModel> ReadFileAsync(string path, CancellationToken cancellationToken)
    {
        var contents = await File.ReadAllBytesAsync(path, cancellationToken);
        var mediaFile = new FileModel()
        {
            FileName = Guid.NewGuid().ToString(),
            ContentType = "image/png",
            Data = contents
        };
        return mediaFile;
    }

    private async Task<TimeSpan> GetVideoDurationAsync(string path, CancellationToken cancellationToken)
    {
        var videoInfo = await FFProbe.AnalyseAsync(path, null, cancellationToken);
        return videoInfo.Duration;
    }
}
