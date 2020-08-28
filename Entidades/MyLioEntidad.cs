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
    public class MyLioEntidad
    {
        public string PDNO { get; set; }
        public string PONO { get; set; }
        public string CWAR { get; set; }
        public string CLOT { get; set; }
        public string SITM { get; set; }
        public string DSCA { get; set; }
        public string CUNI { get; set; }
        public string CANT_EST { get; set; }
        public string ACT_CANT { get; set; }
        public string ISWH { get; set; }
        public string OQMF { get; set; }
        public string CANTD { get; set; }
        public string MCNO { get; set; }
        public string cant_reg { get; set; }
        public string cant_max { get; set; }
        public string cant_proc { get; set; }
        public string cant_hidden { get; set; }
        public string fecha { get; set; }
        public string Error { get; set; }
        public string STOCK { get; set; }
    }
}


