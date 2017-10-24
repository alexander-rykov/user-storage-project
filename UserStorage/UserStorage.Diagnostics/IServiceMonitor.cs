using System.ServiceModel;

namespace UserStorage.Diagnostics
{
    [ServiceContract(Name = "Monitor")]
    public interface IServiceMonitor
    {
        [OperationContract(Name = "GetServicesCount")]
        int GetCount();

        [OperationContract(Name = "QueryServices")]
        ServiceInfo[] Query(Query.ServiceQuery queryType);
    }
}
