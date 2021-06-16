using System;
using System.IO;
using System.Threading.Tasks;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Mailboxy
{
    public class ConfigurationService : IConfigurationService
    {
        public async Task<MailboxyConfig> LoadConfig()
        {
            string configText = "";
            using (var reader = new StreamReader(Path.Combine(AppContext.BaseDirectory, "config.yml")))
            {
                configText = await reader.ReadToEndAsync();
            }

            if (!string.IsNullOrEmpty(configText))
            {
                var deserializer = new DeserializerBuilder()
                    .WithNamingConvention(CamelCaseNamingConvention.Instance)
                    .Build();
                var configObject = deserializer.Deserialize<MailboxyConfig>(configText);

                return configObject;
            }

            return null;
        }
    }
}