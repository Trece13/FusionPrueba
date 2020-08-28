using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using whusa.Entidades;
using whusa.DAL;
using System.Data;

namespace whusa.Interfases
{
    public class InterfazDAL_tticol125
    {
        tticol125 dal = new tticol125();

        public InterfazDAL_tticol125()
        {
        }

        public int insertarRegistro(ref List<Ent_tticol125> parametrosIn, ref string strError, string Aplicacion = "")
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

        public int actualizarRegistro_Param(ref List<Ent_tticol125> parametrosIn, ref string strError, string Aplicacion = "", bool updHist = false)
        {
            int retorno = -1;
            try
            {
                retorno = dal.actualizarRegistro_Param(ref parametrosIn, ref strError, Aplicacion, updHist);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }

        public int enviaDatos(string nit, string NombreCliente, string ApellidoCliente, string Nit)
        {
            return 0;
        }

        public int listaRegistrosPendConfItem_Param(ref Ent_tticol125 parametros, ref string strError)
        {
            int retorno = 0;
            try
            {
                retorno = dal.listaRegistrosPendConfItem_Param(ref parametros, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }

        }

        public double cantidadMaximaPorLote(ref Ent_tticol125 parametros, ref string strError)
        {
            double retorno = 0.0;
            try
            {
                retorno = dal.cantidadMaximaPorLote(ref parametros, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }

        }

        public void guardaErrores(ref string strError, string Aplicacion)
        {

        }

        public DataTable consultaPorOrnoItem(ref string orno, ref string item, ref string strError)
        {
            //int retorno = -1;
            DataTable retorno;
            try
            {
                retorno = dal.consultaPorOrnoItem(ref orno, ref item, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }

        }

        public DataTable listaRegistrosporConfirmar_Param(ref Ent_tticol125 parametros, ref string strError, bool print = false)
        {
            //int retorno = -1;
            DataTable retorno;
            try
            {
                retorno = dal.listaRegistrosporConfirmar_Param(ref parametros, ref strError, print);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public DataTable vallidatePalletID(ref Ent_tticol125 parametros, ref string strError)
        {
            //int retorno = -1;
            DataTable retorno;
            try
            {
                retorno = dal.vallidatePalletID(ref parametros, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }
        public DataTable vallidatePalletData(ref Ent_tticol125 parametros, ref string strError)
        {
            //int retorno = -1;
            DataTable retorno;
            try
            {
                retorno = dal.vallidatePalletData(ref parametros, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }
        public DataTable vallidatePalletIDMRB(ref Ent_tticol125 parametros, ref string strError)
        {
            //int retorno = -1;
            DataTable retorno;
            try
            {
                retorno = dal.vallidatePalletIDMRB(ref parametros, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }
        public DataTable getReasonCodes(ref string strError)
        {
            //int retorno = -1;
            DataTable retorno;
            try
            {
                retorno = dal.getReasonCodes(ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public DataTable getCostCenters(ref string strError)
        {
            //int retorno = -1;
            DataTable retorno;
            try
            {
                retorno = dal.getCostCenters(ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public DataTable invGetPalletInfo(ref Ent_tticol125 parametros, ref string strError)
        {
            //int retorno = -1;
            DataTable retorno;
            try
            {
                retorno = dal.invGetPalletInfo(ref parametros, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public DataTable getReasonCode(ref Ent_tticol125 parametros, ref string strError)
        {
            //int retorno = -1;
            DataTable retorno;
            try
            {
                retorno = dal.getReasonCode(ref parametros, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }


        public DataTable getCostCenter(ref Ent_tticol125 parametros, ref string strError)
        {
            //int retorno = -1;
            DataTable retorno;
            try
            {
                retorno = dal.getCostCenter(ref parametros, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }
        public bool updataPalletStatus(ref Ent_tticol125 parametros, ref string strError)
        {
            bool retorno;
            try
            {
                retorno = dal.updataPalletStatus(ref parametros, ref strError);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }





        public DataTable listaRegistrosOrden_Param(ref Ent_tticol125 parametros, ref string strError, bool print = false)
        {
            //int retorno = -1;
            DataTable retorno;
            try
            {
                retorno = dal.listaRegistrosOrden_Param(ref parametros, ref strError, print);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }

        }

        public DataTable listaRegistrosOrden_ParamHis(ref Ent_tticol125 parametros, ref string strError)
        {
            //int retorno = -1;
            DataTable retorno;
            try
            {
                retorno = dal.listaRegistrosOrden_ParamHis(ref parametros, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }

        }

        public DataTable listaRegistrosLoteItem_Param(ref Ent_tticol125 parametros, ref string strError)
        {
            //int retorno = -1;
            DataTable retorno;
            try
            {
                retorno = dal.listaRegistrosLoteItem_Param(ref parametros, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }

        }

        public DataTable listaRegistrosLoteItem_Param(ref Ent_tticol125 parametros)
        {
            //int retorno = -1;
            DataTable retorno;
            try
            {
                retorno = dal.listaRegistrosLoteItem_Param(ref parametros);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }

        }

        public DataTable ConsultarRegistroPalletID(ref Ent_tticol125 parametros, ref string strError, bool print = false)
        {
            //int retorno = -1;
            DataTable retorno;
            try
            {
                retorno = dal.ConsultarRegistroPalletID(ref parametros, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public DataTable ConsultarQtdl(ref Ent_tticol125 obj, ref string strError)
        {
            try
            {
                DataTable retorno = dal.ConsultarQtdl(obj, strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }
    }
}
