using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mailboxy
{
    public class MailboxService : IMailboxService
    {
        public async Task<IEnumerable<MailMessage>> FetchMessages()
        {
            //throw new System.NotImplementedException();
            return await Task.FromResult(new List<MailMessage>());
        }
    }
}