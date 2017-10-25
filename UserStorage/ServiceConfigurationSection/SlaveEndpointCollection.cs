namespace ServiceConfigurationSection
{
    public class SlaveEndpointCollection : ConfigurationElementCollection<SlaveEndpointElement>
    {
        protected override string ElementName
        {
            get
            {
                return "slaveEndpoint";
            }
        }
    }
}
