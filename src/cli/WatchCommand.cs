using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Mailboxy
{
    public class WatchCommand : Command
    {
        private readonly IPluginService _pluginService;
        private readonly IMailboxService _mailboxService;

        public WatchCommand(IPluginService pluginService, IMailboxService mailboxService) 
            : base("watch", "Watch mailbox for messages that match the search criteria")
        {
            AddOption(new Option<string>(new []{"--provider", "-p"}, "Mailbox provider to watch"));
            AddOption(new Option<string>(new []{"--actions", "-a"}, "Active actions to run in the pipeline"));

            Handler = CommandHandler.Create<string, string, IConsole, CancellationToken>(WatchMailboxHandler);
            _pluginService = pluginService;
            _mailboxService = mailboxService;
        }

        private async void WatchMailboxHandler(string provider, string actions, IConsole console, CancellationToken token)
        {
            try
            {
                console.Out.Write($"[{DateTime.Now.ToLongTimeString()}] Loading mailboxy provider..\n");

                IMailboxyProvider mailboxyProvider = null;
                if (!string.IsNullOrEmpty(provider))    
                    mailboxyProvider = await _pluginService.GetProvider(provider);
                else
                    mailboxyProvider = await _pluginService.GetProvider();

                if (mailboxyProvider == null)
                    throw new Exception("Mailboxy provider was not found or failed to load");

                console.Out.Write($"[{DateTime.Now.ToLongTimeString()}] Found '{mailboxyProvider.ProviderName}' provider\n");

                console.Out.Write($"[{DateTime.Now.ToLongTimeString()}] Starting to watch the mailbox..\n");

                while (!token.IsCancellationRequested)
                {
                    var msgList = await _mailboxService.FetchMessages(mailboxyProvider);
                    if (msgList.Any())
                    {
                        console.Out.Write($"[{DateTime.Now.ToLongTimeString()}] {msgList.Count()} messages was found\n");
                    }
                    else
                    {
                        console.Out.Write($"[{DateTime.Now.ToLongTimeString()}] No new message was found\n");
                    }    

                    Task.Delay(5000, token).Wait();
                }                
            }
            catch (OperationCanceledException)
            {
                console.Out.Write($"[{DateTime.Now.ToLongTimeString()}] Stopping mailbox watch..\n");
            }
            catch (Exception ex)
            {
                console.Error.Write($"[{DateTime.Now.ToLongTimeString()}] Error: ({ex.GetType().Name}) {ex.Message}\n");
            }            
        }
    }
}