using UseCase.UseCase.Interfaces;
using UseCase.UseCase;
using Domain.Clients;
using Infrastructure.Clients;
using Amazon;
using Amazon.S3;

namespace VideoRequestConsumer.Extensions;

internal static class InfrastructureExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        return
            services
                .AddClients();
    }

    private static IServiceCollection AddClients(this IServiceCollection services)
    {
        return services
            .AddSingleton<IBucketClient, BucketClient>()
            .AddStorageClient();
    }

    private static IServiceCollection AddStorageClient(this IServiceCollection services) {

        var awsAccessKeyId = Environment.GetEnvironmentVariable("AWS_ACESS_KEY_ID");
        var awsSecretAccessKey= Environment.GetEnvironmentVariable("AWS_SECRET_ACESS_KEY");
        //ToDo: Validar se as configs existem
        var amazonS3Client = new AmazonS3Client(awsAccessKeyId, awsSecretAccessKey, RegionEndpoint.USEast1);

        return services.
            AddSingleton(amazonS3Client);
    }
}
