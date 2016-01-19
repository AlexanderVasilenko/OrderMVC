using System.Net;
using System.Net.Mail;
using System.Text;
using OrderForm.Domain.Abstract;
using OrderForm.Domain.Entities;

namespace OrderForm.Domain.Processors
{
    public class EmailSettings
    {
        //public string MailFromAddress = "no-reply@order.com";
        //public bool UseSsl = true;
        //public string ServerName = "smtp.gmail.com";
        //public int ServerPort = 587;
        public string MailToAddress;
        public string MailFromAddress;
        public bool UseSsl;
        public string Username;
        public string Password;
        public string ServerName;
        public int ServerPort;
    }

    public class MailOrderProcessor:IOrderProcessor
    {
        private EmailSettings emailSettings;

        public MailOrderProcessor(EmailSettings settings)
        {
            emailSettings = settings;
        }
        public void ProcessOrder(Order order,string country)
        {
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.EnableSsl = emailSettings.UseSsl;
                smtpClient.Host = emailSettings.ServerName;
                smtpClient.Port = emailSettings.ServerPort;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(emailSettings.Username, emailSettings.Password);
                //var fileContents = System.IO.File.ReadAllLines(HostingEnvironment.MapPath(@"~/Core/GmailSmtp.txt"));
                //smtpClient.Credentials
                //    = new NetworkCredential(fileContents[0].Split(':')[1], fileContents[1].Split(':')[1]);

                StringBuilder body = new StringBuilder()
                    .AppendLine("The new Order was processed")
                    .AppendLine("---")
                    .AppendLine("Info:");

                body.AppendFormat("Customer Name: {0:c}", order.Name)
                     .AppendLine("---")
                     .AppendLine("Delivery Data:")
                     .AppendLine(country)
                     .AppendLine(order.City)
                     .AppendLine(order.Address ?? "")
                     .AppendLine("---")
                     .AppendFormat("Safety  pack: {0}",
                         order.IsPacked? "Yes" : "No");

                MailMessage mailMessage = new MailMessage(
                    emailSettings.MailFromAddress,
                    emailSettings.MailToAddress,
                    //fileContents[2].Split(':')[1],
                    "New Order",
                    body.ToString());

                smtpClient.Send(mailMessage);
            }
        }
    }
}