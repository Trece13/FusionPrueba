using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace whusa.Entidades
{
    public class Ent_tticol222
    {
        public Ent_tticol222()
        {

            //this.pdno = string.Empty;
            //this.sqnb = string.Empty;
            this.drpt = DateTime.Now;
            this.urpt = string.Empty;
            this.acqt = decimal.MinValue;
            this.cwaf = string.Empty;
            this.cwat = string.Empty;
            this.aclo = string.Empty;
            this.allo = decimal.MinValue;
               
              /*drpt
                urpt
                acqt
                cwaf
                cwat
                aclo*/

        }

        public DateTime drpt { get; set; }
        public string urpt { get; set; }
        public decimal acqt { get; set; }
        public string cwaf { get; set; }
        public string cwat { get; set; }
        public string aclo { get; set; }
        public decimal allo { get; set; }

    }
}
