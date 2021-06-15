using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mailboxy
{
    public interface IMailboxService
    {
        Task<IEnumerable<MailMessage>> FetchMessages();
    }
}