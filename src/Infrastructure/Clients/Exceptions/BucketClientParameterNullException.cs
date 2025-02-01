using Amazon.S3.Model;

namespace Infrastructure.Clients.Exceptions;

public class BucketClientParameterNullException : Exception
{
    private const string DEFAULT_MESSAGE = "ERROR IN REQUEST AMAZON S3: {0}";

    private BucketClientParameterNullException(string parameterName) : base(string.Format(DEFAULT_MESSAGE, parameterName))
    {

    }

    public static void ThrowIfNull(string? value, string parameterName)
    {
        if (value == null)
        {
            throw new BucketClientParameterNullException(parameterName);
        }
    }

    public static void ThrowIfNull(byte[]? value, string parameterName)
    {
        if (value == null || value.Length == 0)
        {
            throw new BucketClientParameterNullException(parameterName);
        }
    }
}
