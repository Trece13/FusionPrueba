using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace whusa.Entidades
{
    public class Ent_tticol116
    {
        public string item { get; set; }

        public string cwar { get; set; }

        public string loca { get; set; }

        public string clot { get; set; }

        public double qtyr { get; set; }

        public string cdis { get; set; }

        public string obse { get; set; }

        public string logr { get; set; }

        public string date { get; set; }

        public int disp { get; set; }

        public int proc { get; set; }

        public string mess { get; set; }

        public string suno { get; set; }

        public int refcntd { get; set; }

        public int refcntu { get; set; }
        public string paid { get; set; }
        public string cwam { get; set; }

        public Ent_tticol116()
        {
            item = String.Empty;
            cwar = String.Empty;
            loca = String.Empty;
            clot = String.Empty;
            qtyr = 0;
            cdis = String.Empty;
            obse = String.Empty;
            logr = String.Empty;
            disp = 4;
            proc = 2;
            mess = " ";
            suno = String.Empty;
            refcntd = 0;
            refcntu = 0;
            paid = String.Empty;
            cwam = String.Empty;
        }
    }
}