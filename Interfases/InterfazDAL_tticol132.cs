using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using whusa.DAL;
using whusa.Entidades;
using System.Data;

namespace whusa.Interfases
{
    public class InterfazDAL_tticol132
    {
        tticol132 dal = new tticol132();

        public int insertarRegistro(ref List<Ent_tticol132> parametrosIn, ref string strError)
        {
            int retorno = -1;
            try
            {
                retorno = dal.insertarRegistro(ref parametrosIn, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += ex.Message);
            }
        }

        public DataTable ObtenerBox(ref Ent_tticol132 parametros, ref string strError)
        {
            //int retorno = -1;
            DataTable retorno;
            try
            {
                retorno = dal.ObtenerBox(ref parametros, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public DataTable ValidarMaquina(string sMachine, ref string strError)
        {
            DataTable retorno;

            try
            {
                retorno = dal.ValidarMaquina(sMachine, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public DataTable listarItemByBarCode(string sBarCode, string sTipo, ref string strError)
        {
            DataTable retorno;
            try
            {
                retorno = dal.listarItemByBarCode(sBarCode, sTipo, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }
    }
}
