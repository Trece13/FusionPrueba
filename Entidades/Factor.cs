using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace whusa
{
    public class Factor
    {
        // Clase para factores
        public Factor()
        {
            this.FactorD = null;
            this.FactorB = null;
            this.ErrorMsg = "";
            this.Error = false;
        }

        public string Tipo { get; set; }
        public decimal? FactorD { get; set; }
        public decimal? FactorB { get; set; }
        public string MsgError { get; set; }
        public string BASU { get; set; }
        public string UNIT { get; set; }
        public string POTENCIA { get; set; }
        public string FACTOR { get; set; }
        public string ErrorMsg { get; set; }
        public bool Error { get; set; }
    }
}
