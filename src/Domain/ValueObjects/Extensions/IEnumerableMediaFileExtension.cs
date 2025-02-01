using System.IO.Compression;

namespace Domain.ValueObjects.Extensions;

public static class IEnumerableMediaFileExtension
{
    public static FileModel ZipFiles(this IEnumerable<FileModel> files)
    {
        using var zipMemory = new MemoryStream();

        using(var zipArchive = new ZipArchive(zipMemory, ZipArchiveMode.Create, true))
        {
            foreach(var file in files)
            {
                using var memoryStream = new MemoryStream(file.Data!);

                var entry = zipArchive.CreateEntry(file.FileName);
                var entryStream = entry.Open();

                memoryStream.CopyTo(entryStream);

                entryStream.Close();

            }
        }

        zipMemory.Position = 0;

        var response = new FileModel()
        {
            FileName = "images.zip",
            ContentType = "application/zip",
            Data = zipMemory.ToArray()
        };

        return response;
    }
}
