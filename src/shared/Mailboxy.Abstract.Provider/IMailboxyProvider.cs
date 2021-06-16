using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mailboxy
{
    public interface IMailboxyProvider
    {
        string ProviderName { get; }
        Task<IEnumerable<MailMessage>> FetchMessages();
    }
}
