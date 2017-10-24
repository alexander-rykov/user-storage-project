using System.Runtime.Serialization;

namespace UserStorage.Diagnostics.Query
{
    [DataContract(Name = "SelectServicesWithSpecifiedNameQuery")]
    public class NameEquals : ServiceQuery
    {
        [DataMember(Name = "Name")]
        public string Name { get; set; }
    }
}
