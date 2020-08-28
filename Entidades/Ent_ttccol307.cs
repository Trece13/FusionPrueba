using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Data.Linq.Mapping;

namespace whusa
{
    
    public class Ent_ttccol307
    {
        public Ent_ttccol307()
        {
            this.Error = false;
            this.ErrorMsg = string.Empty;
        }

        [DataMember(Order = 2)]
        [Column(Name = "USRR")]
        public string USRR { get; set; }


        [DataMember(Order = 3)]
        [Column(Name = "STAT")]
        public string STAT { get; set; }


        [DataMember(Order = 4)]
        [Column(Name = "PAID")]
        public string PAID { get; set; }


        [DataMember(Order = 5)]
        [Column(Name = "REFCNTD")]
        public int REFCNTD { get; set; }

        [DataMember(Order = 6)]
        [Column(Name = "REFCNTU")]
        public int REFCNTU { get; set; }

        [DataMember(Order = 7)]
        [Column(Name = "PROC")]
        public string PROC { get; set; }

        public bool Error { get; set; }
        public string TypeMsgJs { get; set; }
        public string ErrorMsg { get; set; }
        public string SuccessMsg { get; set; }

    }
}
