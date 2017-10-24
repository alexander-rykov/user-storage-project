using System;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace UserStorageApp
{
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

        public ApiServiceHost(T obj, string uri)
        {
            if (string.IsNullOrEmpty(uri))
            {
                throw new ArgumentNullException(nameof(uri));
            }

            _baseAddress = new Uri(uri);
            _serviceHost = new ServiceHost(obj, _baseAddress);

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
}
