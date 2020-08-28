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
    public class Ent_ticol125448
    {
        [DataMember(Order = 0)]
        [Column(Name = "WORKORDER")]
        public string workorder {get; set;}

        [DataMember(Order = 1)]
        [Column(Name = "POSITION")]        
        public Int32 position {get; set;}

        [DataMember(Order = 2)]
        [Column(Name = "ITEM")]
        public string item{get; set;}

        [DataMember(Order = 3)]
        [Column(Name = "WAREHOUSE")]
        public string warehouse { get; set; }

        [DataMember(Order = 4)]
        [Column(Name = "LOTCODE")]
        public string lotcode { get; set; }

        [DataMember(Order = 5)]
        [Column(Name = "RETURNEDQUANTITY")]
        public Int32 returnedquantity { get; set; }

        [DataMember(Order = 6)]
        [Column(Name = "USER")]
        public string user { get; set; }

        [DataMember(Order = 7)]
        [Column(Name = "DATE")]
        public string date { get; set; }

        [DataMember(Order = 8)]
        [Column(Name = "PRINTED")]
        public string printed { get; set; }

        [DataMember(Order = 9)]
        [Column(Name = "CONFIRMED")]
        public string confirmed { get; set; }

        public Ent_ticol125448()
        {
            workorder = String.Empty;
            position = 0;
            item = String.Empty;
            warehouse= 0;
            lotcode= string.Empty;
            returnedquantity= 0;
            user = string.Empty;
            date = DateTime.Now.ToString();
            printed = string.Empty;
            confirmed = String.Empty;
        }

        public Ent_ticol125448( string pworkorder, int pposition, string pitem, string pwarehouse, string plotcode,
                                int preturnedquantity, string puser, string pdate, string pprinted, string pconfirmed)
        {

            this.workorder = pworkorder;
            this.position = pposition;
            this.item = pitem;
            this.warehouse = pwarehouse;
            this.lotcode = plotcode;
            this.returnedquantity = preturnedquantity;
            this.user = puser;
            this.date = pdate;
            this.printed = pprinted;
            this.confirmed = pconfirmed;

        }
    


    
    }
}
