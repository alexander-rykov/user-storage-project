using System.Runtime.Serialization;

namespace UserStorage.Diagnostics.Query
{
    [DataContract(Name = "SelectServicesWithNameContainsQuery")]
    public class NameContains : ServiceQuery
    {
        [DataMember(Name = "NameText")]
        public string Text { get; set; }
    }
}
