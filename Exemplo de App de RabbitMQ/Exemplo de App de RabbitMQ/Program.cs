using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace Exemplo_de_App_de_RabbitMQ
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Informe o seu email?");
            string email = Console.ReadLine();

            Console.WriteLine("Informe o Assunto do email?");
            string assunto = Console.ReadLine();

            Console.WriteLine("Informe o Corpo do email?");
            string Corpo = Console.ReadLine();


            // Configuração da conexão com o RabbitMQ
            var factory = new ConnectionFactory() { HostName = "localhost", Port = 5672 };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                // Declaração da fila
                channel.QueueDeclare(queue: "fila_teste",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                // Mensagem a ser enviada
                string mensagem = email + ";" + assunto + ";" + Corpo;

                // Conversão da mensagem para bytes
                var body = Encoding.UTF8.GetBytes(mensagem);

                // Publica a mensagem na fila
                channel.BasicPublish(exchange: "",
                                     routingKey: "fila_teste",
                                     basicProperties: null,
                                     body: body);
                Console.WriteLine("Mensagem enviada: {0}", mensagem);

            }
        }

    }
}



