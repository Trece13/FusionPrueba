using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Data.Linq.Mapping;

namespace whusap.Entidades
{
    [DataContract]
    public class Ent_ttdcol524
    {
        [DataMember(Order = 0)]
        [Column(Name = "OrderNumber")]
        public string orderNumber { get; set; }

        [DataMember(Order = 1)]
        [Column(Name = "AdjustmentDate")]
        public string adjustmentDate { get; set; }

        [DataMember(Order = 2)]
        [Column(Name = "Employee")]
        public string employee { get; set; }

        [DataMember(Order = 3)]
        [Column(Name = "Warehouse")]
        public string warehouse { get; set; }

        [DataMember(Order = 4)]
        [Column(Name = "Item")]
        public string item { get; set; }

        [DataMember(Order = 5)]
        [Column(Name = "adjustmentQuantity")]
        public string adjustmentQuantity { get; set; }
            
        [DataMember(Order = 6)]
        [Column(Name = "Unit")]
        public string unit { get; set; }

        [DataMember(Order = 7)]
        [Column(Name = "adjustmentReason")]
        public string adjustmentReason { get; set; }

        [DataMember(Order = 8)]
        [Column(Name = "Status")]
        public string status { get; set; }

        [DataMember(Order = 9)]
        [Column(Name = "AdjustmentBaanOrder")]
        public string adjustmentBaanOrder { get; set; }

        [DataMember(Order = 10)]
        [Column(Name = "AdjustmentBaanOrderLine")]
        public string adjustmentBaanOrderLine { get; set; }

        [DataMember(Order = 11)]
        [Column(Name = "REFCNTD")]
        public int refcntd { get; set; }

        [DataMember(Order = 12)]
        [Column(Name = "REFCNTU")]
        public int refcntu { get; set; }
    }
}

