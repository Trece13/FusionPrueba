using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Data.Linq.Mapping;

namespace whusa.Entidades
{
    [DataContract]
    public class Ent_twhinh225
    {
        [DataMember(Order = 0)]
        [Column(Name = "ORNO")]
        public string orno { get; set; }

        [DataMember(Order = 1)]
        [Column(Name = "OSET")]
        public string oset { get; set; }

        [DataMember(Order = 2)]
        [Column(Name = "USER")]
        public string pono { get; set; }

        [DataMember(Order = 3)]
        [Column(Name = "SERN")]
        public string sern { get; set; }

        [DataMember(Order = 4)]
        [Column(Name = "SEQN")]
        public string seqn { get; set; }

        [DataMember(Order = 5)]
        [Column(Name = "CWAR")]
        public string cwar { get; set; }

        [DataMember(Order = 6)]
        [Column(Name = "SLOC")]
        public string twhwmd200_sloc { get; set; }
    }
}
