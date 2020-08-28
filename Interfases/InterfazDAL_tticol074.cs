using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using whusa;
using System.Data;

namespace whusa.Interfases
{
    public class InterfazDAL_tticol074
    {
        tticol074 dal = new tticol074();

        public int insertarRegistro(ref List<Ent_tticol074> parametrosIn, ref string strError)
        {
            int retorno = -1;
            try
            {
                retorno = dal.insertarRegistro(ref parametrosIn, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }

        public int eliminarRegistro(string orden, DateTime dtStart, DateTime dtEnd, ref string strError)
        {
            int retorno = -1;
            try
            {
                retorno = dal.eliminarRegistro(orden, dtStart, dtEnd, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }

        public DataTable ObtenerConsolidado(string ordenes, DateTime dtStart, DateTime dtEnd, ref string strError)
        {
            //int retorno = -1;
            DataTable retorno;
            try
            {
                retorno = dal.ObtenerConsolidado(ordenes, dtStart, dtEnd, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        
    }
}
