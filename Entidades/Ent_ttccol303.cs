using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Data.Linq.Mapping;

namespace whusa.Entidades
{
    [DataContract]
    public class Ent_ttccol303
    {
        [DataMember(Order = 0)]
        [Column(Name = "Orden")]
        public string orden         {get;set;}

        [DataMember(Order = 1)]
        [Column(Name = "Programa")]
        public string programa      {get;set;}

        [DataMember(Order = 2)]
        [Column(Name = "Boton")]
        public string boton         {get;set;}

        [DataMember(Order = 3)]
        [Column(Name = "MESP")]
        public string MESP          {get;set;}

        [DataMember(Order = 4)]
        [Column(Name = "MENG")]
        public string MENG          {get;set;}

        [DataMember(Order = 5)]
        [Column(Name = "Categoria")]
        public string idCategoria { get; set; }

        [DataMember(Order = 6)]
        [Column(Name = "Categoria")]
        public string categoria     {get;set;}

        [DataMember(Order = 7)]
        [Column(Name = "strSess")]
        public string strSess { get; set; }

        
        [DataMember(Order = 7)]
        [Column(Name = "MENUPADRE")]
        public string MENUPADRE { get; set; }

        [DataMember(Order = 7)]
        [Column(Name = "MENUID")]
        public string MENUID { get; set; }
    }
}
