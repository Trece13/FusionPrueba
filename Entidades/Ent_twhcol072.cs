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
    public class Ent_twhcol072
    {
        [DataMember(Order = 0)]
        [Column(Name = "SOUR")]
        public int sour { get; set; }

        [DataMember(Order = 1)]
        [Column(Name = "ORNO")]
        public string orno { get; set; }

        [DataMember(Order = 2)]
        [Column(Name = "PONO")]
        public int pono { get; set; }

        [DataMember(Order = 3)]
        [Column(Name = "DWMS")]
        public string dwms { get; set; }

        [DataMember(Order = 4)]
        [Column(Name = "PWMS")]
        public int pwms { get; set; }

        [DataMember(Order = 5)]
        [Column(Name = "CWAR")]
        public string cwar { get; set; }

        [DataMember(Order = 6)]
        [Column(Name = "ITEM")]
        public string item { get; set; }

        [DataMember(Order = 7)]
        [Column(Name = "QANA")]
        public decimal qana { get; set; }

        [DataMember(Order = 8)]
        [Column(Name = "CUNI")]
        public string cuni { get; set; }

        [DataMember(Order = 9)]
        [Column(Name = "CLOT")]
        public string clot { get; set; }

        [DataMember(Order = 10)]
        [Column(Name = "INDT")]
        public string indt { get; set; }

        [DataMember(Order = 11)]
        [Column(Name = "PRDT")]
        public string prdt { get; set; }

        [DataMember(Order = 12)]
        [Column(Name = "PROC")]
        public int proc { get; set; }

        [DataMember(Order = 13)]
        [Column(Name = "RCNO")]
        public string rcno { get; set; }

        [DataMember(Order = 14)]
        [Column(Name = "RWMS")]
        public string rwms { get; set; }

        [DataMember(Order = 15)]
        [Column(Name = "LOGN")]
        public string logn { get; set; }

        [DataMember(Order = 16)]
        [Column(Name = "ERRO")]
        public string erro { get; set; }

        [DataMember(Order = 17)]
        [Column(Name = "SEQN")]
        public int seqn { get; set; }

        [DataMember(Order = 18)]
        [Column(Name = "REFCNTD")]
        public int refcntd { get; set; }

        [DataMember(Order = 19)]
        [Column(Name = "REFCNTU")]
        public int refcntu { get; set; }

        public Ent_twhcol072()
        {
            sour = 0;
            orno = string.Empty;
            pono = 0;
            dwms = string.Empty;
            pwms = 0;
            cwar = string.Empty;
            item = string.Empty;
            qana = 0;
            cuni = string.Empty;
            clot = string.Empty;
            indt = string.Empty;
            prdt = string.Empty;
            proc = 0;
            rcno = string.Empty;
            rwms = string.Empty;
            logn = string.Empty;
            erro = string.Empty;
            seqn = 0;
            refcntd = 0;
            refcntu = 0;
        }

        public Ent_twhcol072(   int     _sour,		string  _orno,		int     _pono,		string  _dwms,		int     _pwms,		
                                string  _cwar,		string  _item,		decimal _qana,		string  _cuni,		string  _clot,		
                                string  _indt,		string  _prdt,		int     _proc,		string  _rcno,		string  _rwms,		
                                string  _logn,		string  _erro,		int     _seqn,		int     _refcntd,	int     _refcntu)
        {
            this.sour = _sour;
            this.orno = _orno;
            this.pono = _pono;
            this.dwms = _dwms;
            this.pwms = _pwms;
            this.cwar = _cwar;
            this.item = _item;
            this.qana = _qana;
            this.cuni = _cuni;
            this.clot = _clot;
            this.indt = _indt;
            this.prdt = _prdt;
            this.proc = _proc;
            this.rcno = _rcno;
            this.rwms = _rwms;
            this.logn = _logn;
            this.erro = _erro;
            this.seqn = _seqn;
            this.refcntd = _refcntd;
            this.refcntu = _refcntu;
        }
    }
}