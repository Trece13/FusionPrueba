using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using whusa;
using whusa.Entidades;

namespace whusa.Interfases
{
    public class InterfazDAL_tticol075
    {
        tticol075 dal = new tticol075();

        public int insertarRegistro(ref List<Ent_tticol075> parametrosIn, ref string strError)
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
    }
}
