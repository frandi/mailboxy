using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Mailboxy
{
    public class PluginService : IPluginService
    {
        private const string actionsFolder = "actions";
        private const string providersFolder = "providers";

        private readonly MailboxyConfig _config;

        public PluginService(MailboxyConfig config)
        {
            _config = config;
        }

        public Task<IEnumerable<IMailboxyAction>> GetActions()
        {
            throw new System.NotImplementedException();
        }

        public Task<IMailboxyProvider> GetProvider()
        {
            return GetProvider(_config.DefaultProvider);
        }

        public async Task<IMailboxyProvider> GetProvider(string providerName)
        {
            // get config for the provider
            var providerConfig = _config.Providers.FirstOrDefault(p => p.Name == providerName);
            if (providerConfig == null)
                throw new Exception("Config for default provider was not found");
            
            // load provider plugin
            var pluginPath = Path.Combine(providersFolder, providerConfig.DllFile);
            var pluginAssembly = await LoadPlugin(pluginPath);

            // instantiate an object for the provider
            IMailboxyProvider provider = null;
            foreach (Type type in pluginAssembly.GetTypes())
            {
                if (typeof(IMailboxyProvider).IsAssignableFrom(type))
                {
                    provider = Activator.CreateInstance(type) as IMailboxyProvider;
                    if (provider != null)
                        break;                    
                }
            }

            return provider;
        }

        private async Task<Assembly> LoadPlugin(string relativePath)
        {
            var pluginLocation = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, relativePath.Replace('\\', Path.DirectorySeparatorChar)));

            var loadContext = new PluginLoadContext(pluginLocation);
            var assemblyName = new AssemblyName(Path.GetFileNameWithoutExtension(pluginLocation));
            
            return await Task.FromResult(loadContext.LoadFromAssemblyName(assemblyName));
        }
    }
}