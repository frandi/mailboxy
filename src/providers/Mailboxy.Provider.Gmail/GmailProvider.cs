using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mailboxy.Provider.Gmail
{
    public class GmailProvider : IMailboxyProvider
    {
        public string ProviderName => "Gmail";

        public async Task<IEnumerable<MailMessage>> FetchMessages()
        {
            var dummy = new List<MailMessage>
            {
                new MailMessage { Subject = "Dummy message 1" },
                new MailMessage { Subject = "Dummy message 2" }
            };

            return await Task.FromResult(dummy);
        }
    }
}
