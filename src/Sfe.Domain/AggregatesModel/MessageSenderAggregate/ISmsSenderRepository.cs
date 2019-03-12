using System.Threading.Tasks;

namespace Sfe.Domain.AggregatesModel.MessageSenderAggregate
{
    public interface ISmsSenderRepository
    {
        Task SendSmsAsync(string number, string message);
    }
}
