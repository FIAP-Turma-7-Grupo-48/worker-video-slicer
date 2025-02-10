using Domain.Clients;
using Domain.ValueObjects;
using Infrastructure.Clients.Dtos;
using Infrastructure.Clients.RabbbitMq;
using RabbitMQ.Client;

namespace Infrastructure.Clients.RabbitMq;

public class StorageSlicedVideoRabbitMqClient : RabbitMQPublisher<StorageSlicedVideoSendDto>, IStorageSlicedVideoClient
{
    public const string QueueName = "SlicedVideo";
    public StorageSlicedVideoRabbitMqClient(IConnectionFactory factory) : base(factory, QueueName)
    {
    }

    public async Task SendAsync(string requestId, StorageFile file, CancellationToken cancellationToken)
    {

        var dto = new StorageSlicedVideoSendDto
        {
            RequestId = requestId,
            File = file,
            Status = RequestStatus.Processed
        };

        await PublishMessageAsync(dto, cancellationToken);
    }


}
