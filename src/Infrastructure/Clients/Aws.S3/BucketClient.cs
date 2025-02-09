using Amazon.S3;
using Amazon.S3.Model;
using Domain.Clients;
using Domain.ValueObjects;
using Domain.ValueObjects.Exceptions;
using Infrastructure.Clients.Exceptions;

namespace Infrastructure.Clients.Aws.S3;

public class BucketClient : IBucketClient
{
    private readonly AmazonS3Client _client;
    private readonly string _bucketName;

    public BucketClient(AmazonS3Client client, string bucketName)
    {
        _client = client;
        _bucketName = bucketName;
    }
    public async Task<StorageFile> UploadAsync(string fileKey, FileModel file, CancellationToken cancellationToken)
    {
        NullFileException.ThrowIfFileIsNull(file);

        var contentType = GetContentType(file);

        var storageFile = await UploadAsync(file.Data, contentType, fileKey, cancellationToken);

        return storageFile;
    }

    public async Task<FileModel> DownloadAsync(StorageFile storageFile, CancellationToken cancellationToken)
    {
        var byteArrary = await DownloadAsync(storageFile.Key, cancellationToken);

        var FileModel = new FileModel()
        {
            FileName = storageFile.FileName,
            ContentType = storageFile.ContentType,
            Data = byteArrary
        };

        return FileModel;
    }

    private async Task<StorageFile> UploadAsync(byte[] data, string contentType, string fileKey, CancellationToken cancellationToken)
    {

        BucketClientParameterNullException.ThrowIfNull(fileKey, nameof(fileKey));
        BucketClientParameterNullException.ThrowIfNull(data, nameof(data));

        using var inputStream = new MemoryStream(data, 0, data.Length);

        var fileRequest = new PutObjectRequest()
        {
            Key = fileKey,
            BucketName = _bucketName,
            CannedACL = S3CannedACL.Private,
            StorageClass = S3StorageClass.Standard,
            InputStream = inputStream,
        };


        fileRequest.Metadata.Add("Content-Type", contentType);

        var result = await _client.PutObjectAsync(fileRequest, cancellationToken);

        BucketClientRequestException.ThrowIfNull(result, _bucketName);

        var storageFile = new StorageFile()
        {
            ContentType = contentType,
            Key = fileKey,
        };

        return storageFile;
    }

    private async Task<byte[]> DownloadAsync(string Key, CancellationToken cancellationToken)
    {
        var request = new GetObjectRequest
        {
            BucketName = _bucketName,
            Key = Key,
        };

        var response = await _client.GetObjectAsync(request, cancellationToken);

        using var memoryStream = new MemoryStream();
        response.ResponseStream.CopyTo(memoryStream);

        var byteArray = memoryStream.ToArray();
        return byteArray;
    }

    private string GetContentType(FileModel file)
    {

        if (string.IsNullOrWhiteSpace(file.ContentType))
        {
            return "application/octet-stream";
        }

        return file.ContentType;

    }
}
