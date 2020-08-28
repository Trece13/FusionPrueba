using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using whusa.DAL;
using System.Data;
using whusap.Entidades;

namespace whusa.Interfases
{
    public class InterfazDAL_ttdcol524
    {
        ttdcol524 dal = new ttdcol524();

        public int insertarDatos(ref List<Ent_ttdcol524> parametros, ref string strError)
        {
            int retorno = -1;
            try
            {
                retorno = dal.insertarDatos(ref parametros, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += ex.Message);
            }
        }

        public int actualizarContadores(ref string strError)
        {
            int retorno = -1;
            try
            {
                retorno = dal.actualizarContadores(ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += ex.Message);
            }
        }

        public DataTable GetData(ref Ent_ttdcol524 ParametrosIn, ref string strError)
        {
            DataTable retorno;
            try
            {
                retorno = dal.GetData(ref ParametrosIn, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }
    }
}

