using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using whusa.Entidades;
using whusa.DAL;
using System.Data;

namespace whusa.Interfases
{
    public class InterfazDAL_ttwhcol017
    {
        ttwhcol017 dal = new ttwhcol017();

        public InterfazDAL_ttwhcol017()
        {
        }

        public int insertarRegistro(ref List<Ent_ttwhcol017> parametrosIn, ref string strError) 
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

        public DataTable TakeMaterialInv_listaConsLabel_Param(ref Ent_ttwhcol017 Parametros, ref string strError)
        {
            //int retorno = -1;
            DataTable retorno;
            try
            {
                retorno = dal.TakeMaterialInv_listaConsLabel_Param(ref Parametros, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public DataTable TakeMaterialInv_verificaBodegaZone_Param(ref Ent_ttwhcol016 Parametros, ref string strError)
        {
            //int retorno = -1;
            DataTable retorno;
            try
            {
                retorno = dal.TakeMaterialInv_verificaBodegaZone_Param(ref Parametros, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }                

    }
}
