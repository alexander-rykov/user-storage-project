using System.Configuration;
using System.Diagnostics;

namespace ServiceConfigurationSection
{
    [DebuggerDisplay("ServiceInstances = {ServiceInstances.Count}")]
    public class ServiceConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("serviceInstances", IsRequired = true, IsDefaultCollection = false)]
        public ServiceInstanceCollection ServiceInstances
        {
            get
            {
                return (ServiceInstanceCollection)this["serviceInstances"];
            }
        }

        [ConfigurationProperty("xmlns", IsRequired = false)]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public string XmlNamespace
        {
            get
            {
                return (string)this["xmlns"];
            }
        }

        [ConfigurationProperty("xmlns:xsi", IsRequired = false)]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public string Xsi
        {
            get
            {
                return (string)this["xmlns:xsi"];
            }
        }

        [ConfigurationProperty("xsi:noNamespaceSchemaLocation", IsRequired = false)]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public string NamespaceSchemaLocation
        {
            get
            {
                return (string)this["xsi:noNamespaceSchemaLocation"];
            }
        }
    }
}
