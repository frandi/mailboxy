using System.Collections.Generic;

namespace Mailboxy
{
    public class MailboxyConfig
    {
        public string Version { get; set; }
        public string DefaultProvider { get; set; }
        public IList<string> ActiveActions { get; set; }
        public IList<MailboxyPluginConfig> Providers { get; set; }
        public IList<MailboxyPluginConfig> Actions { get; set; }
    }

    public class MailboxyPluginConfig
    {
        public string Name { get; set; }
        public string DllFile { get; set; }
        public IDictionary<string, string> Args { get; set; }
    }
}