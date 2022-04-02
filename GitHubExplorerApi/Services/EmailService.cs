using Core.Entities;
using Core.Interfaces;
using GitHubExplorerApi.Dtos;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace GitHubExplorerApi.Services
{
    public class EmailService: IEmailService
    {
        private IConfigurationRoot _config;
        public EmailService(IConfiguration config)
        {
            _config = (IConfigurationRoot)config;
        }
        public void SendEmail(string emailAddress, GitHubRepository repoDto)
        {
            string fromEmail = _config["EmailCredential:emailAddress"];
            string fromEmailPassword = _config["EmailCredential:password"];


            StringBuilder htmlString = new StringBuilder();

            htmlString.AppendLine($"<h1>{repoDto.name}</h1>");
            htmlString.AppendLine($"<img src='{repoDto.AvatarUrl}'>");
            htmlString.AppendLine($"<p>{repoDto.description}</p>");

            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            message.From = new MailAddress(fromEmail);
            message.To.Add(new MailAddress(emailAddress));
            message.Subject = "You have just added a repo to youre bookmarks!";
            message.IsBodyHtml = true; //to make message body as html  
            message.Body = htmlString.ToString();
            smtp.Port = 587;
            smtp.Host = "smtp.gmail.com"; //for gmail host  
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(fromEmail, fromEmailPassword);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Send(message);
            
        }

    }
}
