using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using WorkerRabbitMQ.Modelo;
using WorkerRabbitMQ.Servico.Interface;

namespace WorkerRabbitMQ.Servico
{
    public class ServicoRabbitMQ : IServicoRabbitMQ
    {
        private readonly ILogger<ServicoRabbitMQ> _logger;

        public ServicoRabbitMQ(ILogger<ServicoRabbitMQ> logger)
        {
            _logger = logger;
        }

        public void ConsumerMensagem()
        {
            #region ConsumerMessagem
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "MensagemQueue",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            _logger.LogInformation("Aguardando Mensagem.");

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var messageJason = Encoding.UTF8.GetString(body);

                var modeloMensagem = System.Text.Json.JsonSerializer.Deserialize<ModeloMensagem>(messageJason);
                System.Threading.Thread.Sleep(1000);

                _logger.LogInformation($"Mensagem recebida com Id: {modeloMensagem.IdMsg} as {DateTimeOffset.Now}");
            };

            channel.BasicConsume(queue: "MensagemQueue",
                                 autoAck: true,
                                 consumer: consumer);

            #endregion
        }
    }
}
