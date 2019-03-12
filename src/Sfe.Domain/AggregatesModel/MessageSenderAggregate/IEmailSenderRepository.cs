using System.Threading.Tasks;

namespace Sfe.Domain.AggregatesModel.MessageSenderAggregate
{
    public interface IEmailSenderRepository
    {
        Task SendEmailAsync(string email, string subject, string message);
        Task SendEmailWithAnexoAsync(string email, string subject, string message, string FileName);
    }
}
