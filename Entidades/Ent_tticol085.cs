using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace whusa.Entidades
{
    public class Ent_tticol085
    {
        public string mcno { get; set; }

        public int type { get; set; }

        public int tano { get; set; }

        public string cwtt { get; set; }

        public decimal hrea { get; set; }

        public string comm { get; set; }

        public string logn { get; set; }

        public string date { get; set; }

        public int proc { get; set; }

        public int chec { get; set; }

        public string mess { get; set; }

        public int refcntd { get; set; }

        public int refcntu { get; set; }

        public Ent_tticol085() 
        {
            mcno = String.Empty;
            type = 0;
            tano = 0;
            cwtt = String.Empty;
            hrea = 0;
            comm = String.Empty;
            logn = String.Empty;
            date = String.Empty;
            proc = 2;
            chec = 2;
            mess = " ";
            refcntd = 0;
            refcntu = 0;
        }
    }
}
