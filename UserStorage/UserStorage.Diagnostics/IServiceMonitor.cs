using System.ServiceModel;

namespace UserStorage.Diagnostics
{
    [ServiceContract(Name = "Monitor", ConfigurationName = "ServiceMonitor")]
    public interface IServiceMonitor
    {
        [OperationContract(Name = "GetServicesCount")]
        int GetCount();

        [OperationContract(Name = "QueryServices")]
        ServiceInfo[] Query(Query.ServiceQuery queryType);
    }
}
