using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using whusa.Entidades;
using whusa.DAL;


namespace whusa.Interfases
{
    public class InterfazDAL_ttccol300
    {
        ttccol300 dal = new ttccol300();

        public InterfazDAL_ttccol300()
        {
        }

        public int insertarRegistro(ref List<Ent_ttccol300> parametrosIn, ref string strError)
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

        public int actualizarRegistro(ref List<Ent_ttccol300> parametros, ref string strError)
        {
            int retorno = -1;
            try
            {
                retorno = dal.actualizarRegistro(ref parametros, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }

        public bool updateUFIN(ref string user, ref string pass, ref string strError)
        {
            bool retorno = false;
            try
            {
                retorno = dal.updateUFIN(ref user, ref pass, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }
        

        public DataTable listaRegistro_Param(ref Ent_ttccol300 ParametrosIn, ref string strError)
        {
            //int retorno = -1;
            DataTable retorno;
            try
            {
                retorno = dal.listaRegistro_Param(ref ParametrosIn, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }


        public void  RecordarContraseña(Ent_ttccol300 obj, ref string strError)
        {
            try
            {
                 dal.RecordarContraseña(obj, ref strError);
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }
    }
}
