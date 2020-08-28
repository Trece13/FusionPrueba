using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace whusap
{
    public class Tabla
    {
        public DataTable tabla = new DataTable();

        public DataTable crearTabla()
        {
            tabla = new DataTable();
            tabla.Columns.Add(new DataColumn("t$pono", typeof(int)));
            tabla.Columns.Add(new DataColumn("t$item", typeof(string)));
            tabla.Columns.Add(new DataColumn("Descripcion", typeof(string)));
            tabla.Columns.Add(new DataColumn("t$cwar", typeof(string)));
            tabla.Columns.Add(new DataColumn("unidad", typeof(string)));
            tabla.Columns.Add(new DataColumn("T$REQT", typeof(int)));
            tabla.Columns.Add(new DataColumn("T$CLOT", typeof(string)));
            tabla.Columns.Add(new DataColumn("T$PRIN", typeof(string)));
            tabla.Columns.Add(new DataColumn("ID", typeof(string)));

            tabla.Rows.Add(new String[]{ "10", "ABCD", "PRUEBA1", "I287", "CJ", "5", "OI12345678", "1", "WWABCDEAAAAX" });
            tabla.Rows.Add(new String[] { "20", "DEFG", "PRUEBA2", "I288", "UN", "6", "OI15678678", "2", "AAABCDEAAAAX" });
            tabla.Rows.Add(new String[] { "30", "GHIJ", "PRUEBA3", "I289", "CJ", "7", "OI127896078", "1", "BBABCDEAAAAX" });

//            whusap.WCFService.InterfazServicioWhusaClient inter = new whusap.WCFService.InterfazServicioWhusaClient();
//            inter.BeginlistaRegistrosOrden_Param();
            return tabla;

        }

    }
}