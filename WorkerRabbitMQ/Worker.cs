using WorkerRabbitMQ.Servico.Interface;

namespace WorkerRabbitMQ
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServicoRabbitMQ _servicoRabbitMQ;
        public Worker(ILogger<Worker> logger, IServicoRabbitMQ servicoRabbitMQ)
        {
            _logger = logger;
            _servicoRabbitMQ = servicoRabbitMQ;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Worker Inicializando");

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker rodando: {time}", DateTimeOffset.Now);

                _servicoRabbitMQ.ConsumerMensagem();

                await Task.Delay(1000, stoppingToken);
            }

            _logger.LogInformation("Worker parando Parando");

        }
    }
}