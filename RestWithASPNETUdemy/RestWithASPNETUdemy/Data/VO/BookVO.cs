using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace RestWithASPNETUdemy.Data.VO
{
    [DataContract]
    public class BookVO
    {
        [DataMember(Order = 1 , Name = "Codigo")]
        public long Id { get; set; }
        [DataMember(Order = 2, Name = "Title")]
        public string Title { get; set; }
        [DataMember(Order = 3, Name = "Author")]
        public string Author { get; set; }
        [DataMember(Order = 4, Name = "Price")]
        public decimal? Price { get; set; }
        [DataMember(Order = 5, Name = "LaunchDate")]
        public DateTime? LaunchDate { get; set; }
    }
}
