using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace whusa.Entidades
{
    public class Ent_tticol100
    {
        public string pdno { get; set; }
        public int pono { get; set; }
        public int seqn { get; set; }
        public string seqnR { get; set; }
        public string mcno { get; set; }
        public string shif { get; set; }
        public string item { get; set; }
        public double qtyr { get; set; }
        public string cdis { get; set; }
        public int rejt { get; set; }
        public string clot { get; set; }
        public string obse { get; set; }
        public string logr { get; set; }
        public string datr { get; set; }
        public int disp { get; set; }
        public string logn { get; set; }
        public string date { get; set; }
        public int proc { get; set; }
        public string mess { get; set; }
        public int refcntd { get; set; }
        public int refcntu { get; set; }
        public string dsca { get; set; }
        public string Unit { get; set; }
        public string cwar { get; set; }
        public string paid { get; set; }

		public Ent_tticol100() 
        {
            pdno = String.Empty;
            pono = 0;
            seqn = 0;
            seqnR =String.Empty;
            mcno = String.Empty;
            shif = String.Empty;
            item = String.Empty;
            qtyr = 0;
            cdis = String.Empty;
            rejt = 0;
            clot = String.Empty;
            obse = String.Empty;
            logr = String.Empty;
            datr = String.Empty;
            disp = 4;
            logn = " ";
            date = String.Empty;
            proc = 1;
            mess = " ";
            refcntd = 0;
            refcntu = 0;
            Unit = string.Empty;
            cwar = string.Empty;
            paid = string.Empty;
        }
    }
}
