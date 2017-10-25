using System.Runtime.Serialization;

namespace UserStorage.Diagnostics.Query
{
    [DataContract(Name = "SelectAllServicesQuery")]
    public class All : ServiceQuery
    {
    }
}
