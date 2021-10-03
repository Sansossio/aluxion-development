using System;
using FileHandler.Dto;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace FileHandler.Services
{
  public class MailService
  {
    private static string host = Environment.GetEnvironmentVariable("SMTP_HOST");
    private static int port = Int32.Parse(Environment.GetEnvironmentVariable("SMTP_PORT"));
    private static string user = Environment.GetEnvironmentVariable("SMTP_USER");
    private static string password = Environment.GetEnvironmentVariable("SMTP_PASSWORD");
    private static string from = Environment.GetEnvironmentVariable("SMTP_FROM");

    public static void Send(SendMail data)
    {
      // create email message
      var email = new MimeMessage();
      email.From.Add(MailboxAddress.Parse(MailService.from));
      email.To.Add(MailboxAddress.Parse(data.To));
      email.Subject = data.Subject;
      email.Body = new TextPart(TextFormat.Html) { Text = data.Body };

      // send email
      var smtp = new SmtpClient();
      smtp.CheckCertificateRevocation = false;
      if (Environment.GetEnvironmentVariable("SMTP_SSL") == "true")
      {
        smtp.Connect(MailService.host, MailService.port, SecureSocketOptions.StartTls);
        smtp.Authenticate(MailService.user, MailService.password);
      }
      else
      {
        smtp.Connect(MailService.host, MailService.port, SecureSocketOptions.None);
      }
      smtp.Send(email);
      smtp.Disconnect(true);
    }
  }
}