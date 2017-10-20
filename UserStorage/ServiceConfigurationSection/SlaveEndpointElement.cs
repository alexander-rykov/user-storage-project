using System.Configuration;
using System.Diagnostics;

namespace ServiceConfigurationSection
{
    [DebuggerDisplay("Name = {Name}")]
    public class SlaveEndpointElement : ConfigurationElement
    {
        [ConfigurationProperty("name", IsRequired = true, IsKey = true)]
        [StringValidator(InvalidCharacters = "~!@#$%^&*()[]{}/;'\"|\\")]
        public string Name
        {
            get
            {
                return (string)this["name"];
            }
        }

        [ConfigurationProperty("host", IsRequired = true)]
        [StringValidator(InvalidCharacters = "~!@#$%^&*()[]{}/;'\"|\\")]
        public string Host
        {
            get
            {
                return (string)this["host"];
            }
        }

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
