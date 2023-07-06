using MailKit.Security;
using MimeKit;
using Org.BouncyCastle.Bcpg;
using ShopMates.ViewModels.Mail;
using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ShopMates.Application.Mail
{
    public class EmailService : IEmailService
    {
        public async Task<int> SendMail(EmailViewModel request)
        {
            try
            {
                // Create a MailMessage object
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(request.fromname, request.frommail));
                message.To.Add(MailboxAddress.Parse(request.toemail));
                message.Subject = request.subject;
                var builder  = new BodyBuilder();
                builder.HtmlBody = request.message;
                message.Body = builder.ToMessageBody();

                using (var smtp = new MailKit.Net.Smtp.SmtpClient())
                {
                    await smtp.ConnectAsync("axyres.devops-nhattien.asia", 587, MailKit.Security.SecureSocketOptions.StartTls);
                    await smtp.AuthenticateAsync("tiennhat.lam@devops-nhattien.asia", "Avata@123"); // Replace with your email account credentials

                    await smtp.SendAsync(message);
                    await smtp.DisconnectAsync(true);
                }

                // Return a success status
                return 0;
            }
            catch (Exception ex)
            {
                // Handle any exceptions and return an error status
                Console.WriteLine($"Error sending email: {ex.Message}");
                return -1; // Rethrow the exception or handle it accordingly
            }
        }
    }
}
