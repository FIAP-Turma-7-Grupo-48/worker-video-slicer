using Amazon.S3.Model;

namespace Infrastructure.Clients.Exceptions;

public class BucketClientRequestException : Exception
{
    private const string DEFAULT_MESSAGE = "ERROR IN REQUEST AMAZON S3: {0}";

    private BucketClientRequestException(string bucketName) : base(string.Format(DEFAULT_MESSAGE, bucketName))
    {

    }

    public static void ThrowIfNull(PutObjectResponse? response, string bucketName)
    {
        if (response == null)
        {
            throw new BucketClientRequestException(bucketName);
        }
    }
}
