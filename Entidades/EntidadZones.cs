using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace whusa
{
    // entida encargada  mostrar los campos del pallet disponible

   public class EntidadZones
    {
       public EntidadZones()
        {

            this.CWAR = string.Empty;
            this.ZONE = string.Empty;
            this.DSCA = string.Empty;
            this.BALL = string.Empty;
            this.BINB = string.Empty;
            this.BOUT = string.Empty;
            this.BTRR = string.Empty;
            this.BTRI = string.Empty;
            this.BASS = string.Empty;
            this.EMNO = string.Empty;
            this.PRTR = string.Empty;
            this.REFCNTD = string.Empty;
            this.REFCNTU = string.Empty; 

            this.error = false;
            this.errorMsg = string.Empty;

        }

        public string CWAR { get; set; }
        public string ZONE { get; set; }
        public string DSCA { get; set; }
        public string BALL { get; set; }
        public string BINB { get; set; }
        public string BOUT { get; set; }
        public string BTRR { get; set; }
        public string BTRI { get; set; }
        public string BASS { get; set; }
        public string EMNO { get; set; }
        public string PRTR { get; set; }
        public string REFCNTD { get; set; }
        public string REFCNTU { get; set; }

        public bool error { get; set; }
        public string typeMsgJs { get; set; }
        public string errorMsg { get; set; }
       
    }
}
