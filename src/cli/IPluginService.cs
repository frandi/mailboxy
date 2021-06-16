using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mailboxy
{
    public interface IPluginService
    {
        Task<IEnumerable<IMailboxyAction>> GetActions();
        Task<IMailboxyProvider> GetProvider();
        Task<IMailboxyProvider> GetProvider(string providerName);
    }
}