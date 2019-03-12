using Sfe.Domain.AggregatesModel.MessageSenderAggregate;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Sfe.Infra.Data.Repositories
{
    public class EmailSenderRepository : IEmailSenderRepository
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            // Credentials:
            var credentialUserName = "marcos.phs@outlook.com";
            var sentFrom = "marcos.phs@outlook.com";
            var pwd = "Florida#05";

            // Configure the client:
            System.Net.Mail.SmtpClient client =
                new System.Net.Mail.SmtpClient("smtp.outlook.com");

            client.Port = 587;
            client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;

            // Create the credentials:
            System.Net.NetworkCredential credentials =
                new System.Net.NetworkCredential(credentialUserName, pwd);

            client.EnableSsl = true;
            client.Credentials = credentials;

            // Create the message:
            // string file = "data.xls";

            var mail = new System.Net.Mail.MailMessage(sentFrom, email);
            mail.IsBodyHtml = true;
            mail.Subject = subject;
            mail.Body = message;

            //Attachment data = new Attachment(file, MediaTypeNames.Application.Octet);
            //// Add time stamp information for the file.
            //ContentDisposition disposition = data.ContentDisposition;
            //disposition.CreationDate = System.IO.File.GetCreationTime(file);
            //disposition.ModificationDate = System.IO.File.GetLastWriteTime(file);
            //disposition.ReadDate = System.IO.File.GetLastAccessTime(file);
            //// Add the file attachment to this e-mail message.
            //mail.Attachments.Add(data);


            // Send:
            return client.SendMailAsync(mail);
        }

        public Task SendEmailWithAnexoAsync(string email, string subject, string message, string FileName)
        {
            // Credentials:
            var credentialUserName = "marcos.phs@outlook.com";
            var sentFrom = "marcos.phs@outlook.com";
            var pwd = "Florida#05";

            // Configure the client:
            System.Net.Mail.SmtpClient client =
                new System.Net.Mail.SmtpClient("smtp.outlook.com");

            client.Port = 587;
            client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;

            // Create the credentials:
            System.Net.NetworkCredential credentials =
                new System.Net.NetworkCredential(credentialUserName, pwd);

            client.EnableSsl = true;
            client.Credentials = credentials;

            // Create the message:
            // string FileName = "orcamento" + "1" + ".pdf";
            string file = Path.GetTempPath() + FileName;

            var mail = new System.Net.Mail.MailMessage(sentFrom, email);
            mail.IsBodyHtml = true;
            mail.Subject = subject;
            mail.Body = message;
            // mail.Attachments.Add(new Attachment(tempPath));

            Attachment data = new Attachment(file, MediaTypeNames.Application.Octet);
            // Add time stamp information for the file.
            ContentDisposition disposition = data.ContentDisposition;
            disposition.CreationDate = System.IO.File.GetCreationTime(file);
            disposition.ModificationDate = System.IO.File.GetLastWriteTime(file);
            disposition.ReadDate = System.IO.File.GetLastAccessTime(file);
            // Add the file attachment to this e-mail message.
            mail.Attachments.Add(data);


            // Send:
            return client.SendMailAsync(mail);
        }
    }
}
