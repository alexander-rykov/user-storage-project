using System.Runtime.Serialization;

namespace UserStorage.Diagnostics.Query
{
    [DataContract(Name = "SelectServicesWithSpecifiedTypeQuery")]
    public class TypeEquals : ServiceQuery
    {
        [DataMember(Name = "Type")]
        public string Type { get; set; }
    }
}
