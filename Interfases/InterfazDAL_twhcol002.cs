using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using whusa.Entidades;
using whusa.DAL;

namespace whusa.Interfases
{
    public class InterfazDAL_twhcol002
    {
        twhcol002 dal = new twhcol002();

        public InterfazDAL_twhcol002()
        {
        }

        public DataTable listaRegistro_ObtieneConteo(ref Ent_twhcol002 Parametros, ref string strError)
        {
            DataTable retorno;
            try
            {
                retorno = dal.listaRegistro_ObtieneConteo(ref Parametros, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }
    }
}