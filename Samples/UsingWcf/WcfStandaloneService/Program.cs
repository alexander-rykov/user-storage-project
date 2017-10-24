using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace WcfStandaloneService
{
    [ServiceContract(Name = "SomeService")]
    public interface IMyService
    {
        [OperationContract]
        string SayHello(string name);
    }

    [ServiceBehavior(Name = "MySuperService")]
    public class MyService : IMyService
    {
        public string SayHello(string name)
        {
            return string.Format("Hello, {0}", name);
        }
    }

    public class ApiServiceHost<T> : IDisposable
    {
        private readonly Uri _baseAddress;
        private readonly ServiceHost _serviceHost;

        public ApiServiceHost(string uri)
        {
            if (string.IsNullOrEmpty(uri))
            {
                throw new ArgumentNullException(nameof(uri));
            }

            _baseAddress = new Uri(uri);
            _serviceHost = new ServiceHost(typeof(T), _baseAddress);

            // Enable metadata publishing.
            ServiceMetadataBehavior smb = new ServiceMetadataBehavior
            {
                HttpGetEnabled = true,
            };
            smb.MetadataExporter.PolicyVersion = PolicyVersion.Policy15;
            _serviceHost.Description.Behaviors.Add(smb);
        }

        public void Open()
        {
            // Open the ServiceHost to start listening for messages. Since
            // no endpoints are explicitly configured, the runtime will create
            // one endpoint per base address for each service contract implemented
            // by the service.
            _serviceHost.Open();
        }

        public void Close()
        {
            _serviceHost.Close();
        }

        public void Dispose()
        {
            (_serviceHost as IDisposable).Dispose();
        }
    }

    // netsh http add urlacl url=http://+:8080/hello user=vashanka
    public class Program
    {
        public static void Main(string[] args)
        {
            var uri = "http://localhost:8080/hello";
            using (var host = new ApiServiceHost<MyService>(uri))
            {
                host.Open();

                Console.WriteLine("The service is ready at {0}", uri);
                Console.WriteLine("Press <Enter> to stop the service.");
                Console.ReadLine();

                host.Close();
            }
        }
    }
}
