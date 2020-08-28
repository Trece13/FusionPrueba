using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Data.Linq.Mapping;

namespace whusa.Entidades
{
    [DataContract]
    public class Ent_tticol075
    {
        [DataMember(Order = 0)]
        [Column(Name = "REFCNTD")]
        public int refcntd { get; set; }

        [DataMember(Order = 1)]
        [Column(Name = "REFCNTU")]
        public int refcntu { get; set; }

        [DataMember(Order = 2)]
        [Column(Name = "CHEQUEADO")]
        public int chequeado { get; set; }

        [DataMember(Order = 3)]
        [Column(Name = "COMENTARIO")]
        public string comentario { get; set; }

        [DataMember(Order = 4)]
        [Column(Name = "TARIFAHORARIA")]
        public string tarifaHoraria { get; set; }

        [DataMember(Order = 5)]
        [Column(Name = "FECHA")]
        public DateTime fecha { get; set; }

        [DataMember(Order = 6)]
        [Column(Name = "HORAS")]
        public decimal horas { get; set; }

        [DataMember(Order = 7)]
        [Column(Name = "USUARIO")]
        public string usuario { get; set; }

        [DataMember(Order = 8)]
        [Column(Name = "MENSAJE")]
        public string mensaje { get; set; }

        [DataMember(Order = 9)]
        [Column(Name = "NUMOPERACION")]
        public int numOperacion { get; set; }

        [DataMember(Order = 10)]
        [Column(Name = "ORDEN")]
        public string orden { get; set; }

        [DataMember(Order = 11)]
        [Column(Name = "PROCESADO")]
        public int procesado { get; set; }
    }
}
