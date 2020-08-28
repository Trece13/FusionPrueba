using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using whusa.Entidades;
using whusa.DAL;

namespace whusa.Interfases
{
    public class InterfazDAL_twhwmd200
    {
        twhwmd200 dal = new twhwmd200();

        public InterfazDAL_twhwmd200()
        {
        }

        public DataTable listaRegistro_ObtieneAlmacenLocation(ref Ent_twhwmd200 Parametros, ref string strError)
        {
            DataTable retorno;
            try
            {
                retorno = dal.listaRegistro_ObtieneAlmacenLocation(ref Parametros, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }
    }
}
