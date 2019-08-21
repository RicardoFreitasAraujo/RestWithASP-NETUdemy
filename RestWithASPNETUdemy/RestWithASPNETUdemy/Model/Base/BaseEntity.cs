using System.Runtime.Serialization;

namespace RestWithASPNETUdemy.Model.Base
{
    [DataContract]
    public class BaseEntity
    {
        [DataMember]
        public long Id { get; set; }
    }
}
