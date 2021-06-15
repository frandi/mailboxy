using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Threading;
using System.Threading.Tasks;

namespace Mailboxy
{
    public class ConfigCommand : Command
    {
        public ConfigCommand() 
            : base("config", "Configure settings")
        {
            Handler = CommandHandler.Create<IConsole, CancellationToken>(ConfigurationHandler);
        }

        async void ConfigurationHandler(IConsole console, CancellationToken token)
        {
            console.Out.Write($"[{DateTime.Now.ToLongTimeString()}] Configuring Mailboxy settings..\n");
            
            await Task.CompletedTask;
        }
    }
}