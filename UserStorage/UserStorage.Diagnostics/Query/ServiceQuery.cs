using System.Runtime.Serialization;

namespace UserStorage.Diagnostics.Query
{
    [DataContract(Name = "EmptyQuery")]
    [KnownType(typeof(All))]
    [KnownType(typeof(NameEquals))]
    [KnownType(typeof(NameContains))]
    [KnownType(typeof(TypeEquals))]
    public class ServiceQuery
    {
    }
}
