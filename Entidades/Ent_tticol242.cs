using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;

namespace whusa.Entidades
{
    public class Ent_tticol242
    {
        public Ent_tticol242()
        {

            //this.pdno = string.Empty;
            //this.sqnb = string.Empty;
            this.drpt = DateTime.Now;
            this.urpt = string.Empty;
            this.acqt = double.MinValue;
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
        public double acqt { get; set; }
        public string cwaf { get; set; }
        public string cwat { get; set; }
        public string aclo { get; set; }
        public decimal allo { get; set; }

    }
}
