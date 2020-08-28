using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace whusa.Entidades.Documentos
{
    public class Ent_DocumentoNacional
    {
        public Ent_EncabezadoNacional informacionEncabezado { get; set; }

        public Ent_LoteDespacho informacionLote { get; set; }

        public List<Ent_InformacionProducto> informacionProducto { get; set; }

        public Ent_DocumentoNacional() 
        {
            informacionProducto = new List<Ent_InformacionProducto>();
        }
    }
}
