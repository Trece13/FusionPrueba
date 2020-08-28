using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using whusa.Entidades;
using whusa.DAL;

namespace whusa.Interfases
{
    public class InterfazDAL_twhltc100
    {
        twhltc100 dal = new twhltc100();

        public InterfazDAL_twhltc100()
        {
        }

        public DataTable listaRegistro_Clot(ref Ent_twhltc100 Parametros, ref string strError)
        {
            DataTable retorno;
            try
            {
                retorno = dal.listaRegistro_Clot(ref Parametros, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public DataTable listaRegistro_SiExiste(ref Ent_twhltc100 Parametros, ref string strError)
        {
            DataTable retorno;
            try
            {
                retorno = dal.listaRegistro_SiExiste(ref Parametros, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public DataTable listaRegistros_ObtieneItem(ref Ent_twhltc100 Parametros, ref string strError)
        {
            DataTable retorno;
            try
            {
                retorno = dal.listaRegistros_ObtieneItem(ref Parametros, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }
    }
}