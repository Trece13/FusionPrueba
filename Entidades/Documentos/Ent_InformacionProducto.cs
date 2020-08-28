using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace whusa.Entidades.Documentos
{
    public class Ent_InformacionProducto
    {
        public string descripcionCaracteristica { get; set; }

        public string unidad { get; set; }

        public string limiteInferior { get; set; }

        public string limiteSuperior { get; set; }

        public string resultados { get; set; }

        public string muestra { get; set; }

        public string iorn { get; set; }

        public string valorNormalizado { get; set; }

        public string descripcionCaracteristicaIngles { get; set; }

        public string muestraIngles { get; set; }

        public List<string> atributos { get; set; }
        //Resultados y Muestra

        public Ent_InformacionProducto() 
        {
            atributos = new List<string>();
        }
    }
}
