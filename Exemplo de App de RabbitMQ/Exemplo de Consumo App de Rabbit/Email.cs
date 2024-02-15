using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace Exemplo_de_Consumo_App_de_Rabbit
{
    public class Email
    {

        private string _senderEmail;
        private string _senderPassword;
        private string _smtpServer;
        private int _port;

        public Email(string senderEmail, string senderPassword, string smtpServer, int port)
        {
            _senderEmail = senderEmail;
            _senderPassword = senderPassword;
            _smtpServer = smtpServer;
            _port = port;
        }

        public void SendEmail(string recipientEmail, string subject, string body)
        {
            try
            {
                using (var mail = new MailMessage())
                {
                    mail.From = new MailAddress(_senderEmail);
                    mail.To.Add(recipientEmail);
                    mail.Subject = subject;
                    mail.Body = body;
                    mail.IsBodyHtml = true;

                    using (var smtp = new SmtpClient(_smtpServer, _port))
                    {
                        smtp.EnableSsl = true;
                        smtp.Credentials = new NetworkCredential(_senderEmail, _senderPassword);

                        // Configurar a validação do certificado SSL
                        ServicePointManager.ServerCertificateValidationCallback =
                            delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                            {
                                return true; // Ignorar erros de validação do certificado SSL
                            };

                        mail.Priority = MailPriority.High;

                        smtp.Send(mail);
                    }

                    Console.WriteLine("E-mail enviado com sucesso para: " + recipientEmail);
                }
            }
            catch (SmtpFailedRecipientException ex)
            {
                Console.WriteLine("Erro ao enviar e-mail: {0}", ex.Message);
            }
            catch (SmtpException ex)
            {
                Console.WriteLine("Erro ao enviar e-mail: {0}", ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao enviar e-mail: {0}", ex.Message);
            }
        }
    }
}






