using RabbitMQ.Client;
using System;
using System.Configuration;
using System.Text;

namespace Exemplo_de_Consumo_App_de_Rabbit
{
    public class Program
    {
        private static string emailDefault = ConfigurationManager.AppSettings["EmailPadrao"];
        private static string SenhaEmailDefault = ConfigurationManager.AppSettings["SenhaEmailPadrao"];

        public static void Main(string[] args)
        {
            // Configuração da conexão com o RabbitMQ
            var factory = new ConnectionFactory() { HostName = "localhost", Port = 5672 };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                // Consumo da mensagem
                var consumer = new RabbitMQ.Client.Events.EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var message = Encoding.UTF8.GetString(ea.Body.ToArray());
                    if (message.Contains(";"))
                    {
                        var arrays = message.Split(';');
                        string smtpServer = "smtp.host.com.br";//"smtp.gmail.com"; // 
                        int port = 587; // Porta padrão para SMTP
                        string senderEmail = emailDefault;//"seu-email@example.com";
                        string senderPassword = SenhaEmailDefault;//"sua-senha";
                        string recipientEmail = arrays[0];//"destinatario@example.com";
                        string subject = arrays[1];//"Assunto do e-mail";
                        string body = arrays[2];//"Conteúdo do e-mail.";
                        new Email(senderEmail, senderPassword, smtpServer, port).SendEmail(recipientEmail, subject, body);
                    }

                    Console.WriteLine("Mensagem recebida: {0}", message);
                };
                channel.BasicConsume(queue: "fila_teste",
                                     autoAck: true,
                                     consumer: consumer);

                Console.WriteLine("Pressione qualquer tecla para sair");
                Console.ReadKey();

            }
        }
    }
}
