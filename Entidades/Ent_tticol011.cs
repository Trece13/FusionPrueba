using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Data.Linq.Mapping;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace whusa.Entidades
{
    [DataContract]
    public class Ent_tticol011
    {
        [DataMember(Order = 0)]
        [Column(Name = "MCNO")]
        public string mcno { get; set; }


        [DataMember(Order = 1)]
        [Column(Name = "PDNO")]
        public string pdno { get; set; }


        [DataMember(Order = 2)]
        [Column(Name = "PRDT")]
        public string prdt { get; set; }


        [DataMember(Order = 3)]
        [Column(Name = "CMDT")]
        public string cmdt { get; set; }


        [DataMember(Order = 4)]
        [Column(Name = "STAT")]
        public int stat { get; set; }


        [DataMember(Order = 5)]
        [Column(Name = "INIB")]
        public string inib { get; set; }


        [DataMember(Order = 6)]
        [Column(Name = "COMB")]
        public string comb { get; set; }


        [DataMember(Order = 7)]
        [Column(Name = "REFCNTD")]
        public int refcntd { get; set; }


        [DataMember(Order = 8)]
        [Column(Name = "REFCNTU")]
        public int refcntu { get; set; }


        [DataMember(Order = 9)]
        [Column(Name = "IDRECORD")]
        public string idrecord { get; set; }

        [DataMember(Order = 10)]
        public bool Error { get; set; }

        [DataMember(Order = 11)]
        public string TypeMsgJs { get; set; }

        [DataMember(Order = 12)]
        public string ErrorMsg { get; set; }

        [DataMember(Order = 13)]
        public string SuccessMsg { get; set; }

        [DataMember(Order = 14)]
        public bool Acceso { get; set; }

        public Ent_tticol011()
        {
            mcno = string.Empty;
            pdno = string.Empty;
            prdt = string.Empty;
            cmdt = string.Empty;
            stat = 0;
            inib = string.Empty;
            comb = string.Empty;
            refcntd = 0;
            refcntu = 0;
            idrecord = string.Empty;
            Error = false;
            TypeMsgJs = string.Empty;
            ErrorMsg  = string.Empty;
            SuccessMsg = string.Empty;
            Acceso = true;
        }

        public Ent_tticol011(string pmcno, string ppdno, string pprdt, string pcmdt, int pstat, string pinib, 
                             string pcomb, int prefcntd, int prefcntu, string pidrecord )
        {
            this.mcno = pmcno;
            this.pdno = ppdno;
            this.prdt = pprdt;
            this.cmdt = pcmdt;
            this.stat = pstat;
            this.inib = pinib;
            this.comb = pcomb;
            this.refcntd = prefcntd;
            this.refcntu = prefcntu;
            this.idrecord = pidrecord;
            this.Acceso = true;
            
        }

    }
    
}
