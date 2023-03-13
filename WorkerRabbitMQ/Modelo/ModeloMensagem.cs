using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerRabbitMQ.Enums;

namespace WorkerRabbitMQ.Modelo
{
    public class ModeloMensagem
    {
        public Guid IdMsg { get; set; } 
        public string? Assunto { get; set; }
        public DateTime Data { get; set; }
        public int NumeroEnvelope { get; set; }
        public StatusMensagem StatusMensagem { get; set; }
        public string? TextoMsg { get; set; }
    }
}
