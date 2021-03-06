﻿using System;
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
    public class Ent_tticol080
    {
        [DataMember(Order = 0)]
        [Column(Name = "ORNO")]
        public string orno { get; set; }

        [DataMember(Order = 1)]
        [Column(Name = "PONO")]
        public int pono { get; set; }

        [DataMember(Order = 2)]
        [Column(Name = "ITEM")]
        public string item { get; set; }

        [DataMember(Order = 3)]
        [Column(Name = "CWAR")]
        public string cwar { get; set; }

        [DataMember(Order = 4)]
        [Column(Name = "QUNE")]
        public decimal qune { get; set; }

        [DataMember(Order = 5)]
        [Column(Name = "LOGN")]
        public string logn { get; set; }

        [DataMember(Order = 6)]
        [Column(Name = "DATE")]
        public string date { get; set; }

        [DataMember(Order = 7)]
        [Column(Name = "PROC")]
        public int proc { get; set; }

        [DataMember(Order = 8)]
        [Column(Name = "REFCNTD")]
        public int refcntd { get; set; }

        [DataMember(Order = 9)]
        [Column(Name = "REFCNTU")]
        public int refcntu { get; set; }

        [DataMember(Order = 10)]
        [Column(Name = "CLOT")]
        public string clot { get; set; }

        [DataMember(Order = 11)]
        [Column(Name = "IDRECORD")]
        public string idrecord { get; set; }

        [DataMember(Order = 12)]
        public bool Error { get; set; }
        [DataMember(Order = 13)]
        public string TypeMsgJs { get; set; }
        [DataMember(Order = 14)]
        public string ErrorMsg { get; set; }
        [DataMember(Order = 15)]
        public string SuccessMsg { get; set; }

        [DataMember(Order = 16)]
        [Column(Name = "OORG")]
        public string oorg { get; set; }

        [DataMember(Order = 17)]
        [Column(Name = "PDAT")]
        public string pdat { get; set; }

        [DataMember(Order = 18)]
        [Column(Name = "PICK")]
        public int pick { get; set; }

        public Ent_tticol080()
        {

            orno = string.Empty;
            pono = 0;
            item = string.Empty;
            cwar = string.Empty;
            qune = 0;
            logn = string.Empty;
            date = string.Empty;
            proc = 0;
            refcntd = 0;
            refcntu = 0;
            clot = string.Empty;
            idrecord = string.Empty;
            pdat = string.Empty;
            pick = 0;
            oorg = "0";

            Error = false;
            TypeMsgJs = string.Empty;
            ErrorMsg = string.Empty;
            SuccessMsg = string.Empty;
        }

        public Ent_tticol080(string _orno, int _pono, string _item, string _cwar, decimal _qune, string _logn, string _date,
                              int _proc, int _refcntd, int _refcntu, string _clot, string _idrecord,string _pdat, int _pick, string _oorg)
        {
            this.orno = _orno;
            this.pono = _pono;
            this.item = _item;
            this.cwar = _cwar;
            this.qune = _qune;
            this.logn = _logn;
            this.date = _date;
            this.proc = _proc;
            this.refcntd = _refcntd;
            this.refcntu = _refcntu;
            this.clot = _clot;
            this.idrecord = _idrecord;
            this.pdat = _pdat ;
            this.pick = _pick ;
            this.oorg = _oorg ;
        }

        
    }
}




