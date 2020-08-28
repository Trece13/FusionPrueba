using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using whusa.Entidades;
using whusa.DAL;

namespace whusa.Interfases
{
    public class InterfazDAL_twhcol015
    {
        twhcol015 dal = new twhcol015();

        static InterfazDAL_twhcol015()
        {
        }

        public int insertarRegistro(ref List<Ent_twhcol015> parametros, ref string strError)
        {
            int retorno = -1;
            try
            {
                retorno = dal.insertarRegistro(ref parametros, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }

        public DataTable listaRegistro_ObtieneSecuencia(ref Ent_twhcol015 Parametros, ref string strError)
        {
            DataTable retorno;
            try
            {
                retorno = dal.listaRegistro_ObtieneSecuencia(ref Parametros, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public DataTable listaRegistro_ObtieneSecuenciaNolote(ref Ent_twhcol015 Parametros, ref string strError)
        {
            DataTable retorno;
            try
            {
                retorno = dal.listaRegistro_ObtieneSecuenciaNolote(ref Parametros, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public DataTable listaRegistro_ObtieneSecuenciaLOCATION(ref Ent_twhcol015 Parametros, ref string strError)
        {
            DataTable retorno;
            try
            {
                retorno = dal.listaRegistro_ObtieneSecuenciaLOCATION(ref Parametros, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public DataTable listaRegistro_ObtieneSecuenciaNoloteLOCATION(ref Ent_twhcol015 Parametros, ref string strError)
        {
            DataTable retorno;
            try
            {
                retorno = dal.listaRegistro_ObtieneSecuenciaNoloteLOCATION(ref Parametros, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }
    }
}
