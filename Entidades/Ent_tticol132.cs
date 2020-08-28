using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Data.Linq.Mapping;

namespace whusa.Entidades
{
    [DataContract]
    public class Ent_tticol132
    {
        [DataMember(Order = 0)]
        [Column(Name = "BARCODE")]
        public string barcode { get; set; }

        [DataMember(Order = 1)]
        [Column(Name = "DATE")]
        public DateTime date { get; set; }

        [DataMember(Order = 2)]
        [Column(Name = "TYPE")]
        public int type { get; set; }

        [DataMember(Order = 3)]
        [Column(Name = "ITEM")]
        public string item { get; set; }

        [DataMember(Order = 4)]
        [Column(Name = "USER")]
        public string user { get; set; }

        [DataMember(Order = 5)]
        [Column(Name = "STATUS")]
        public int status { get; set; }

        [DataMember(Order = 6)]
        [Column(Name = "MACHINE")]
        public string machine { get; set; }

        [DataMember(Order = 7)]
        [Column(Name = "REFCNTD")]
        public int refcntd { get; set; }

        [DataMember(Order = 8)]
        [Column(Name = "REFCNTU")]
        public int refcntu { get; set; }
    }
}
