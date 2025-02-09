using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text.Json;
using System.Text;
using Controller.Application.Interfaces;
using Controller.Dto;

namespace VideoRequestConsumer;

public class RabbitMqWorker : BackgroundService
{
    private readonly IConnectionFactory _connectionFactory;
    private readonly IVideoRequestApplication _videoRequestApplication;

    private readonly JsonSerializerOptions _options = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public RabbitMqWorker(IConnectionFactory factory, IVideoRequestApplication videoRequestApplication)
    {
        _connectionFactory = factory;
        _videoRequestApplication = videoRequestApplication;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var connection = await _connectionFactory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(queue: "RequestVideoSlicer", durable: true, exclusive: false, autoDelete: false,
                arguments: null);

            Console.WriteLine(" [*] Waiting for messages.");

            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += (model, ea) =>
            {
                try
                {
                    var body = ea.Body.ToArray();
                    var messageJson = Encoding.UTF8.GetString(body);

                    return ProcessMessage(messageJson, stoppingToken);
                }
                catch (Exception)
                {

                    throw;
                }

            };

            await channel.BasicConsumeAsync("RequestVideoSlicer", autoAck: true, consumer: consumer);
        }
    }

    private async Task ProcessMessage(string json, CancellationToken cancellationToken)
    {
        var message = JsonSerializer.Deserialize<VideoRequestProcessRequestDto>(json, _options);

            await _videoRequestApplication.ProcessVideo(message, cancellationToken);
    }
}
