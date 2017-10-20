using System.Configuration;
using System.Diagnostics;

namespace ServiceConfigurationSection
{
    [DebuggerDisplay("Name = {Name}, Mode = {Mode}")]
    public class ServiceInstanceElement : ConfigurationElement
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

        [ConfigurationProperty("type", IsRequired = true)]
        [StringValidator(InvalidCharacters = "~!@#$%^&*()[]{}/;'\"|\\")]
        public string Type
        {
            get
            {
                return (string)this["type"];
            }
        }

        [ConfigurationProperty("apiPort", IsRequired = true)]
        [StringValidator(InvalidCharacters = "~!@#$%^&*()[]{}/;'\"|\\")]
        public string ApiPort
        {
            get
            {
                return (string)this["apiPort"];
            }
        }

        [ConfigurationProperty("master")]
        public SlaveEndpointCollection Master
        {
            get
            {
                return (SlaveEndpointCollection)this["master"];
            }
        }

        [ConfigurationProperty("slave")]
        public SlaveElement Slave
        {
            get
            {
                return (SlaveElement)this["slave"];
            }
        }

        public ServiceInstanceMode Mode
        {
            get
            {
                if (this.Master.ElementInformation.IsPresent)
                {
                    return ServiceInstanceMode.Master;
                }

                if (this.Slave.ElementInformation.IsPresent)
                {
                    return ServiceInstanceMode.Slave;
                }

                return ServiceInstanceMode.Unknown;
            }
        }
    }
}
