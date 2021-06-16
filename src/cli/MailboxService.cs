using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Mailboxy
{
    public class MailboxService : IMailboxService
    {
        private readonly MailboxyConfig _config;

        public MailboxService(MailboxyConfig config)
        {
            _config = config;
        }

        public Task<IEnumerable<MailMessage>> FetchMessages(IMailboxyProvider provider)
        {
            return provider.FetchMessages();
        }
    }
}