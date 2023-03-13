using WorkerRabbitMQ;
using WorkerRabbitMQ.Servico.Interface;
using WorkerRabbitMQ.Servico;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();

        services.AddSingleton<IServicoRabbitMQ, ServicoRabbitMQ>();

    })
    .Build();

await host.RunAsync();
