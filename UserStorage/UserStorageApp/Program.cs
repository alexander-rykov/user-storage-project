using System;
using ServiceConfiguration = ServiceConfigurationSection.ServiceConfigurationSection;

namespace UserStorageApp
{
    // // netsh http add urlacl url=http://+:8080/diagnostics user=vashanka
    public class Program
    {
        public static void Main(string[] args)
        {
            // Loading configuration from the application configuration file. This configuration is not used yet.
            var serviceConfiguration = (ServiceConfiguration)System.Configuration.ConfigurationManager.GetSection("serviceConfiguration");

            var md = MyDiagnostics.Create(serviceConfiguration);
            var uri = "http://localhost:8080/diagnostics";
            using (var host = new ApiServiceHost<MyDiagnostics>(md, uri))
            {
                host.Open();

                var client = new Client();

                client.Run();

                Console.WriteLine("The service is ready at {0}", uri);
                Console.WriteLine("Press <Enter> to stop the service.");
                Console.ReadLine();

                host.Close();
            }
        }
    }
}
