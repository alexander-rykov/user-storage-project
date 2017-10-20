namespace ServiceConfigurationSection
{
    public class ServiceInstanceCollection : ConfigurationElementCollection<ServiceInstanceElement>
    {
        protected override string ElementName
        {
            get
            {
                return "serviceInstance";
            }
        }
    }
}
