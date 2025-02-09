using VideoRequestConsumer;
using VideoRequestConsumer.Extensions;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<RabbitMqWorker>();

builder.Services.AddUseCase();
builder.Services.AddControllerLayer();
builder.Services.AddInfrastructure();

var host = builder.Build();
host.Run();
