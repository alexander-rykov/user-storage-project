using System;
using System.Linq;
using UserStorageMonitor.UserStorageServiceReference;

namespace UserStorageMonitor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var client = new MonitorClient();
            var servicesCount = client.GetServicesCount();

            Console.WriteLine("Services available: {0}", servicesCount);
            Console.WriteLine();

            ServiceInfo[] services;

            services = client.QueryServices(new EmptyQuery());
            Console.WriteLine("EmptyQuery returns empty result set = {0}", services.Length == 0);

            services = client.QueryServices(new SelectServicesWithSpecifiedNameQuery
            {
                Name = "master-us"
            });
            Console.WriteLine("(master-us node exists && count == 1) == {0}", services.Any() && services.Length == 1);

            services = client.QueryServices(new SelectServicesWithNameContainsQuery
            {
                NameText = "slave"
            });
            Console.WriteLine("(*slave* nodes exist && count == 2) == {0}", services.Any() && services.Length == 2);

            services = client.QueryServices(new SelectServicesWithSpecifiedTypeQuery
            {
                Type = "UserStorageSlave"
            });
            Console.WriteLine("(UserStorageSlave type nodes exist && count == 2) == {0}", services.Any() && services.Length == 2);

            services = client.QueryServices(new SelectAllServicesQuery());
            Console.WriteLine("\nServices:");
            foreach (var service in services)
            {
                Console.WriteLine("\tName: {0}, Type: {1}, Url: {2} ({3})", service.ServiceName, service.ServiceType, service.ServiceUrl, service.ServiceDebugInfo);
            }

            Console.WriteLine("\nPress Enter to continue.");
            Console.ReadLine();
        }
    }
}
