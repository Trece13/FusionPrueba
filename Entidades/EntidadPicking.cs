using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace whusa
{
    // entida encargada  mostrar los campos del pallet disponible

   public class EntidadPicking
    {
         public EntidadPicking()
        {

            this.PALLETID = string.Empty;
            this.ITEM = string.Empty;
            this.DESCRIPTION = string.Empty;
            this.LOT = string.Empty;
            this.WRH = string.Empty;
            this.DESCWRH = string.Empty;
            this.QTY = string.Empty;
            this.UN = string.Empty;
            this.PRIO = string.Empty;
            this.LOCA = string.Empty;
            this.ROWN = string.Empty;
            this.OORG = string.Empty;
            this.ORNO = string.Empty;
            this.OSET= string.Empty;
            this.PONO =string.Empty;
            this.SQNB = string.Empty;
            this.ADVS = string.Empty;

            this.error = false;
            this.errorMsg = string.Empty;

        }

        public string PALLETID { get; set; }
        public string ITEM { get; set; }
        public string DESCRIPTION { get; set; }
        public string LOT { get; set; }
        public string WRH { get; set; }
        public string DESCWRH { get; set; }
        public string QTY { get; set; }
        public string UN { get; set; }
        public string PRIO { get; set; }
        public string LOCA { get; set; }
        public string ROWN { get; set; }
        public string OORG { get; set; }
        public string ORNO { get; set; }
        public string OSET { get; set; }
        public string PONO { get; set; }
        public string SQNB { get; set; }
        public string ADVS { get; set; }
       
        public bool error { get; set; }
        public string errorMsg { get; set; }



        public string QTYT { get; set; }

        public string typeMsgJs { get; set; }

        public string STAT { get; set; }

        public string SLOC { get; set; }
    }
}
