using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UserStorage.Diagnostics;
using ServiceConfiguration = ServiceConfigurationSection.ServiceConfigurationSection;

namespace UserStorageApp
{
    public class MyDiagnostics : DiagnosticsService
    {
        private readonly ReadOnlyCollection<ServiceInfo> _collection;

        public MyDiagnostics(IEnumerable<ServiceInfo> services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            this._collection = new ReadOnlyCollection<ServiceInfo>(services.ToArray());
        }

        protected override ReadOnlyCollection<ServiceInfo> ServiceInfoCollection
        {
            get
            {
                return this._collection;
            }
        }

        public static MyDiagnostics Create(ServiceConfiguration serviceConfiguration)
        {
            if (serviceConfiguration == null)
            {
                throw new ArgumentNullException(nameof(serviceConfiguration));
            }

            var services = serviceConfiguration.ServiceInstances.Select(si => new ServiceInfo
            {
                Type = si.Type,
                Name = si.Name,
                Url = string.Format("http://localhost:{0}/userStorage", si.ApiPort),
                DebugInfo = string.Format("Users available: {0}", 0)
            }).ToArray();

            return new MyDiagnostics(services);
        }
    }
}
