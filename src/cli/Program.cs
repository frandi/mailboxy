using System.CommandLine;
using System.Threading.Tasks;

namespace Mailboxy
{
    class Program
    {
        static async Task<int> Main(string[] args)
        {
            var rootCommand = new RootCommand("Simple application to watch your mailbox and chain it with certain actions");

            var configService = new ConfigurationService();
            var config = await configService.LoadConfig();
            
            var watchCommand = new WatchCommand(new PluginService(config), new MailboxService(config));
            
            var configCommand = new ConfigCommand();
            
            rootCommand.Add(watchCommand);
            rootCommand.Add(configCommand);

            return await rootCommand.InvokeAsync(args);
        }

        
    }
}
