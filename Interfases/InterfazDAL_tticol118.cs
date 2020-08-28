using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using whusa.Entidades;
using whusa.DAL;
using System.Data;

namespace whusa.Interfases
{
    public class InterfazDAL_tticol118
    {
        tticol118 dal = new tticol118();
        tticol118_2 dal2 = new tticol118_2();
        
        static InterfazDAL_tticol118()
        { 
        }

        public int insertarRegistro(ref List<Ent_tticol118> parametros, ref string strError)
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

        public int actualizarRegistro_Param(ref List<Ent_tticol118> parametros, ref string strError, string Aplicacion = "")
        {
            int retorno = -1;
            try
            {
                retorno = dal.actualizarRegistro_Param(ref parametros, ref strError, Aplicacion);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }

        public DataTable listaRegistros_Param(ref Ent_tticol118 ParametrosIn, ref string strError)
        {
            DataTable retorno;
            try
            {
                retorno = dal.listaRegistros_Param(ref ParametrosIn, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public DataTable listaProveedores_ParamMRB(ref Ent_tticol118 ParametrosIn, ref string strError)
        {
            DataTable retorno;
            try
            {
                retorno = dal.listaProveedores_ParamMRB(ref ParametrosIn, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }
        public DataTable listaProveedores_Param(ref Ent_tticol118 ParametrosIn, ref string strError)
        {
            DataTable retorno;
            try
            {
                retorno = dal.listaProveedores_Param(ref ParametrosIn, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }
        public DataTable ListaProveedoresProducto(ref Ent_tticol118 ParametrosIn, ref string strError)
        {
            DataTable retorno;
            try
            {
                retorno = dal.ListaProveedoresProducto(ref ParametrosIn, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public DataTable listaReason_Param(ref string strError)
        {
            DataTable retorno;
            try
            {
                retorno = dal.listaReason_Param(ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public DataTable listaStockw_Param(ref string strError)
        {
            DataTable retorno;
            try
            {
                retorno = dal.listaStockw_Param(ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public DataTable listaRegrind_Param(ref string strError)
        {
            DataTable retorno;
            try
            {
                retorno = dal.listaRegrind_Param(ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public DataTable listaRegrind_ParamV2(ref string item, ref string strError)
        {
            DataTable retorno;
            try
            {
                retorno = dal.listaRegrind_ParamV2(ref item, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public DataTable invLabel_registroImprimir_Param(ref Ent_tticol118 Parametros, ref string strError)
        {
            DataTable retorno;
            try
            {
                retorno = dal.invLabel_registroImprimir_Param(ref Parametros, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }
        public DataTable inv_datospesos(ref Ent_tticol118 Parametros, ref string strError)
        {
            DataTable retorno;
            try
            {
                retorno = dal.inv_datospesos(ref Parametros, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }
        public DataTable invRegrid_Indentifier(ref Ent_tticol118 Parametros, ref string strError)
        {
            DataTable retorno;
            try
            {
                retorno = dal.invRegrid_Indentifier(ref Parametros, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }
        public int UpdatePalletStatus_ticol022(ref Ent_tticol118 parametrosIn, ref string strError)
        {
            int retorno = -1;
            try
            {
                retorno = dal2.UpdatePalletStatus_ticol022(ref parametrosIn, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }
        public DataTable findRecordByItemClotCwarQtyr(ref Ent_tticol118 data, ref string strError)
        {
            DataTable retorno = new DataTable();
            try
            {
                retorno = dal2.findRecordByItemClotCwarQtyr(ref data, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }
    }
}
