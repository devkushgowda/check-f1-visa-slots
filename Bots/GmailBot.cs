using System;
using System.Net;
using System.Net.Mail;

namespace ConsoleApp4.Bots
{
    public class GmailBot : IBot
    {
        private const string GmailUsername = "kushanraj00@gmail.com";
        private const string GmailPassword = "hmubiihmwenkxyyu";
        private const string ToEmail = "kushanraj13@yahoo.in";
        private const string CCEmail = "amruthadr9@gmail.com";
        private const string SMTPServer = "smtp.gmail.com";
        private const int SMTPPort = 587;

        private static readonly Lazy<SmtpClient> _lazySmtmClient = new Lazy<SmtpClient>(() => CreateSmtpClient());

        private static SmtpClient CreateSmtpClient()
        {
            var smtpClient = new SmtpClient(SMTPServer, SMTPPort)
            {
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                EnableSsl = true
            };
            smtpClient.Credentials = new NetworkCredential(GmailUsername, GmailPassword);
            return smtpClient;
        }

        private static void SendMail(string subject, string body)
        {
            var message = new MailMessage(new MailAddress(GmailUsername, "CheckVisaSlotsPro"), new MailAddress(ToEmail, ToEmail));
            message.Subject = subject;
            message.Body = body;
            message.Priority = MailPriority.High;
            message.CC.Add(new MailAddress(CCEmail, CCEmail));
            _lazySmtmClient.Value.Send(message);
        }

        public void Send(string message, string subject = null)
        {
            SendMail(subject, message);
        }
    }
}
