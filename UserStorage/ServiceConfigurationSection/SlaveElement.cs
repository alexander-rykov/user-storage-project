using System.Configuration;
using System.Diagnostics;

namespace ServiceConfigurationSection
{
    [DebuggerDisplay("Port = {Port}")]
    public class SlaveElement : ConfigurationElement
    {
        [ConfigurationProperty("port", IsRequired = true)]
        public string Port
        {
            get
            {
                return (string)this["port"];
            }
        }
    }
}
