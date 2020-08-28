using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using whusa.Entidades;
using whusa.DAL;
using System.Data;

namespace whusa.Interfases
{
    public class InterfazDAL_ttscol100
    {
        ttscol100 dal = new ttscol100();

        public InterfazDAL_ttscol100()
        {
        }

        public int insertarRegistro(ref List<Ent_ttscol100> parametrosIn, ref string strError)
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

        public int actualizarRegistro_Param(ref List<Ent_ttscol100> parametrosIn, ref string strError, string Aplicacion = "", bool updHist = false)
        {
            int retorno = -1;
            try
            {
                retorno = dal.actualizarRegistro_Param(ref parametrosIn, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }

        public DataTable spareDelivery_verificaOrdenes_Param(ref Ent_ttscol100 Parametros, ref string strError)
        {
            //int retorno = -1;
            DataTable retorno;
            try
            {
                retorno = dal.spareDelivery_verificaOrdenes_Param(ref Parametros, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public DataTable spareDelivery_listaRegistroItem_Param(ref Ent_ttscol100 Parametros, ref string strError)
        {
            //int retorno = -1;
            DataTable retorno;
            try
            {
                retorno = dal.spareDelivery_listaRegistroItem_Param(ref Parametros, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public DataTable spareDelivery_listaRegistroUbicacion_Param(ref Ent_ttscol100 Parametros, ref string strError)
        {
            //int retorno = -1;
            DataTable retorno;
            try
            {
                retorno = dal.spareDelivery_listaRegistroUbicacion_Param(ref Parametros, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public DataTable spareDelivery_listaRegistrosOrdenParam(ref Ent_ttscol100 Parametros, ref string strError)
        {
            //int retorno = -1;
            DataTable retorno;
            try
            {
                retorno = dal.spareDelivery_listaRegistrosOrdenParam(ref Parametros, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }
    }
}
