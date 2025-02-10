using UseCase.UseCase.Interfaces;
using UseCase.UseCase;
using Domain.Clients;
using Amazon;
using Amazon.S3;
using Infrastructure.Clients.Aws.S3;
using Infrastructure.Clients.Dtos;
using Infrastructure.Clients.RabbitMq;
using RabbitMQ.Client;

namespace VideoRequestConsumer.Extensions;

internal static class InfrastructureExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        return
            services
                .AddClients()
                .AddRabbitMqConnectionFactory();
    }

    private static IServiceCollection AddClients(this IServiceCollection services)
    {
        return services
            .AddSingleton<IBucketClient, BucketClient>()
            .AddSingleton<IStorageSlicedVideoClient, StorageSlicedVideoRabbitMqClient>()
            .AddStorageClient();
    }

    private static IServiceCollection AddStorageClient(this IServiceCollection services) {

        var awsAccessKeyId = Environment.GetEnvironmentVariable("AWS_ACESS_KEY_ID");
        var awsSecretAccessKey= Environment.GetEnvironmentVariable("AWS_SECRET_ACESS_KEY");
        //ToDo: Validar se as configs existem
        var amazonS3Client = new AmazonS3Client(awsAccessKeyId, awsSecretAccessKey, RegionEndpoint.SAEast1);

        return services.
            AddSingleton(amazonS3Client);
    }

    private static IServiceCollection AddRabbitMqConnectionFactory(this IServiceCollection services)
    {
        var hostName = Environment.GetEnvironmentVariable("RabbitMqHostName");
        var port = int.Parse(Environment.GetEnvironmentVariable("RabbitMqPort"));
        var user = Environment.GetEnvironmentVariable("RabbitMqUserName");
        var password = Environment.GetEnvironmentVariable("RabbitMqPassword");

        return
            services
                .AddSingleton<IConnectionFactory>(
                    new ConnectionFactory()
                    {
                        HostName = hostName,
                        Port = port,
                        UserName = user,
                        Password = password
                    }
                );
    }
}
