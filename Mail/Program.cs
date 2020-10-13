using MailKit;
using MailKit.Net.Imap;
using MimeKit;
using System;
using System.Configuration;
using System.Linq;
using System.Net; // NetworkCredentials
using System.Net.Mail;
using System.Threading.Tasks;
using Kit = MailKit.Net.Smtp;

namespace Mail
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // smtp - simple mail transfer protocol   25, 587
            // POP3, IMAP 993
            //    Smtp();
            // Imap();
            await SmtpMailKitAsync();
        }

        private async static Task SmtpMailKitAsync()
        {
            Kit.SmtpClient client = new Kit.SmtpClient();
            await client.ConnectAsync("smtp.gmail.com", 587);
            var login = ConfigurationManager.AppSettings["login"];
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal);
            var app = config.AppSettings;
            var passw = ConfigurationManager.AppSettings["pass"];
            await client.AuthenticateAsync(login, passw);

            MimeMessage mail = new MimeMessage();
            mail.From.Add(new MailboxAddress("projectsprog1@gmail.com"));
            mail.To.Add(new MailboxAddress("valentyna.paniuk@gmail.com"));
            mail.To.Add(new MailboxAddress("denyshchuk_ak16@nuwm.edu.ua"));
            mail.To.Add(new MailboxAddress("pfefferberhorion@gmail.com"));
            mail.To.Add(new MailboxAddress("artlynx0@gmail.com"));
            mail.To.Add(new MailboxAddress("kozyar.ant@gmail.com"));
            mail.To.Add(new MailboxAddress("victor.palamarchuk@gmail.com"));
            mail.To.Add(new MailboxAddress("tsntania@gmail.com"));
            mail.To.Add(new MailboxAddress("projectsprog1@gmail.com"));

            mail.Subject = "MailKit test smtp";

            var builder = new BodyBuilder();

            // Set the plain-text version of the message text
            builder.TextBody = @"Hey copy\paste from official documentation";

            mail.Body = builder.ToMessageBody();

            await client.SendAsync(mail);
            Console.WriteLine("OK");

        }
        private static void Imap()
        {
            using (ImapClient imap = new ImapClient())
            {
                imap.Connect("imap.gmail.com", 993, true);
                imap.Authenticate("projectsprog1@gmail.com", "qqwwee11!!");
                if (imap.IsAuthenticated)
                    Console.WriteLine("user ok");

                var inbox = imap.Inbox;
                inbox.Open(FolderAccess.ReadOnly);
                try
                {
                    for (int i = inbox.Count - 1; i >= 0; i--)
                    {
                        MimeMessage message = inbox.GetMessage(i);

                        Console.WriteLine($"Subject: {message.Subject}\tSender: {message.From.Mailboxes.First().Name} \t\tDate: {message.Date}");
                    }

                }
                catch { }
            }
        }

        private static void Smtp()
        {
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("projectsprog1@gmail.com", "qqwwee11!!");

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("projectsprog1@gmail.com");

            mail.To.Add(new MailAddress("valentyna.paniuk@gmail.com"));
            mail.To.Add(new MailAddress("denyshchuk_ak16@nuwm.edu.ua"));
            mail.To.Add(new MailAddress("pfefferberhorion@gmail.com"));
            mail.To.Add("artlynx0@gmail.com");
            mail.To.Add("kozyar.ant@gmail.com");
            mail.To.Add("victor.palamarchuk@gmail.com");
            mail.To.Add("tsntania@gmail.com");
            mail.To.Add("projectsprog1@gmail.com");

            mail.Subject = "Test smtp client";
            mail.Body = "Message from custom client";

            mail.IsBodyHtml = true;
            mail.Attachments.Add(new Attachment("D:\\1.jpg"));

            client.Send(mail);
            Console.WriteLine("OK");
        }
    }
}
