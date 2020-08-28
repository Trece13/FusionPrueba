using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Data.Linq.Mapping;

namespace whusa.Entidades
{
    [DataContract]
    public class Ent_tticol180
    {
        [DataMember(Order = 1)]
        [Column(Name = "DOCN")]
        public string docn { get; set; }

        [DataMember(Order = 2)]
        [Column(Name = "USER")]
        public string user { get; set; }

        [DataMember(Order = 3)]
        [Column(Name = "SEND")]
        public int send { get; set; }

        [DataMember(Order = 4)]
        [Column(Name = "PATH")]
        public string path { get; set; }

        [DataMember(Order = 5)]
        [Column(Name = "MSSH")]
        public string mssh { get; set; }

        [DataMember(Order = 6)]
        [Column(Name = "DATE")]
        public string date { get; set; }

        [DataMember(Order = 7)]
        [Column(Name = "REFCNTD")]
        public int refcntd { get; set; }

        [DataMember(Order = 8)]
        [Column(Name = "REFCNTU")]
        public int refcntu { get; set; }

        public string tgbrg835_name { get; set; }

        public string tgbrg835_mail { get; set; }

        public Ent_tticol180() 
        {
            docn = String.Empty;
            user = String.Empty;
            send = 2;
            path = String.Empty;
            mssh = String.Empty;
            refcntd = 0;
            refcntu = 0;
            tgbrg835_mail = String.Empty;
            tgbrg835_name = String.Empty;
        }
    }
}
