using System.Runtime.Serialization;

namespace UserStorage.Diagnostics
{
    [DataContract]
    public class ServiceInfo
    {
        [DataMember(Name = "ServiceName")]
        public string Name { get; set; }

        [DataMember(Name = "ServiceType")]
        public string Type { get; set; }

        [DataMember(Name = "ServiceUrl")]
        public string Url { get; set; }

        [DataMember(Name = "ServiceDebugInfo")]
        public string DebugInfo { get; set; }
    }
}
