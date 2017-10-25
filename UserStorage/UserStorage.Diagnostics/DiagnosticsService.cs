using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;

namespace UserStorage.Diagnostics
{
    [ServiceBehavior(Name = "Diagnostics", InstanceContextMode = InstanceContextMode.Single, ConfigurationName = "DiagnosticsService")]
    public abstract class DiagnosticsService : IServiceMonitor
    {
        protected abstract ReadOnlyCollection<ServiceInfo> ServiceInfoCollection { get; }

        public int GetCount()
        {
            return ServiceInfoCollection.Count;
        }

        public ServiceInfo[] Query(Query.ServiceQuery query)
        {
            if (query.GetType() == typeof(Query.All))
            {
                return ServiceInfoCollection.ToArray();
            }

            if (query.GetType() == typeof(Query.NameEquals))
            {
                var nameEquals = (Query.NameEquals)query;
                return ServiceInfoCollection.Where(s => s.Name == nameEquals.Name).ToArray();
            }

            if (query.GetType() == typeof(Query.NameContains))
            {
                var nameContains = (Query.NameContains)query;
                return ServiceInfoCollection.Where(s => s.Name.Contains(nameContains.Text)).ToArray();
            }

            if (query.GetType() == typeof(Query.TypeEquals))
            {
                var typeEquals = (Query.TypeEquals)query;
                return ServiceInfoCollection.Where(s => s.Type == typeEquals.Type).ToArray();
            }

            if (query.GetType() == typeof(Query.ServiceQuery))
            {
                return new ServiceInfo[] { };
            }

            throw new InvalidOperationException(query.GetType().Name);
        }
    }
}
