using System.Threading.Tasks;

namespace Mailboxy
{
    public interface IConfigurationService
    {
        Task<MailboxyConfig> LoadConfig();
    }    
}