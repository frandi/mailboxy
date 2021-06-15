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
        private readonly IMailboxService _mailboxService;

        public WatchCommand(IMailboxService mailboxService) 
            : base("watch", "Watch mailbox for messages that match the search criteria")
        {
            AddOption(new Option<string>(new []{"--provider", "-p"}, "Mailbox provider to watch"));
            AddOption(new Option<string>(new []{"--actions", "-a"}, "Active actions to run in the pipeline"));

            Handler = CommandHandler.Create<string, string, IConsole, CancellationToken>(WatchMailboxHandler);
            
            _mailboxService = mailboxService;
        }

        async void WatchMailboxHandler(string provider, string actions, IConsole console, CancellationToken token)
        {
            console.Out.Write($"[{DateTime.Now.ToLongTimeString()}] Starting mailbox watch..\n");

            var isContinue = true;
            while (isContinue && !token.IsCancellationRequested)
            {
                try
                {
                    var msgList = await _mailboxService.FetchMessages();

                    if (msgList.Any())
                    {
                        console.Out.Write($"[{DateTime.Now.ToLongTimeString()}] No new message was found\n");
                    }
                    else
                    {
                        console.Out.Write($"[{DateTime.Now.ToLongTimeString()}] No new message was found\n");
                    }    

                    console.Out.Write($"[{DateTime.Now.ToLongTimeString()}] Before Task.Delay\n");
                    Task.Delay(5000, token).Wait();
                    console.Out.Write($"[{DateTime.Now.ToLongTimeString()}] After Task.Delay\n");
                }
                catch (OperationCanceledException)
                {
                    console.Out.Write($"[{DateTime.Now.ToLongTimeString()}] Stopping mailbox watch..\n");

                    isContinue = false;
                }
                catch (Exception ex)
                {
                    console.Error.Write($"[{DateTime.Now.ToLongTimeString()}] Error: ({ex.GetType().Name}) {ex.Message}\n");
                    
                    isContinue = false;
                }
            }
        }
    }
}