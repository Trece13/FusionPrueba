using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using whusa.Entidades;
using whusa.DAL;

namespace whusa.Interfases
{
    public class InterfazDAL_tticol022
    {
        tticol022 dal = new tticol022();
        // Constructor
        static InterfazDAL_tticol022()
        {
        }

        public int insertarRegistro(ref List<Ent_tticol022> parametros, ref List<Ent_tticol020> parametros020, ref string strError)
        {
            int retorno = -1;
            try
            {
                retorno = dal.insertarRegistro(ref parametros, ref parametros020, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }

        public bool insertarRegistroAux(Ent_tticol022 Objtticol022, Ent_tticol020 Objtticol020)
        {
            bool retorno = false;
            try
            {
                retorno = dal.insertarRegistroAux(Objtticol022, Objtticol020);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception("\nPila: " + ex.Message);
            }
        }

        public int insertarRegistroSimple(ref Ent_tticol022 parametros, ref string strError)
        {
            int retorno = -1;
            try
            {
                retorno = dal.insertarRegistroSimple(ref parametros, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }

        public int actualizarRegistro_Param(ref List<Ent_tticol022> parametrosIn, ref string strError)
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

        public int countRecordsByPdnoAndDele(ref string pdno, ref string dele, ref string strError)
        {
            int retorno = -1;
            try
            {
                retorno = dal.countRecordsByPdnoAndDele(ref pdno, ref dele, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }

        public string invLabel_generaSecuenciaOrden(ref Ent_tticol022 Parametros, ref string strError)
        {
            //int retorno = -1;
            string retorno;
            try
            {
                retorno = dal.invLabel_generaSecuenciaOrden(ref Parametros, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public bool invLabel_tiempoGrabacion(Ent_tticol022 Parametros, ref string strError)
        {

            //int retorno = -1;
            bool retorno = false;
            try
            {
                retorno = dal.invLabel_tiempoGrabacion(Parametros, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public bool actualizaRegistroSugUbicaciones(ref Ent_tticol022 data, ref string strError)
        {
            bool retorno = false;
            try
            {
                retorno = dal.actualizaRegistroSugUbicaciones(ref data, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }

        public bool actualizaRegistroConfirmReceipt(ref Ent_tticol022 data, ref string strError)
        {
            bool retorno = false;
            try
            {
                retorno = dal.actualizaRegistroConfirmReceipt(ref data, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }

        public bool actualizaRegistroAnuncioOrd(ref Ent_tticol022 data, ref string strError)
        {
            bool retorno = false;
            try
            {
                retorno = dal.actualizaRegistroAnuncioOrd(ref data, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }

        public DataTable selectMaxSqnbByPdno(ref string pdno, ref string qtdlzero, ref string strError)
        {
            //int retorno = -1;
            DataTable retorno = new DataTable();
            try
            {
                retorno = dal.selectMaxSqnbByPdno(ref pdno, ref qtdlzero, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public DataTable selectDatesBySqnbPdno(ref string pdno, ref string sqnb, ref string strError)
        {
            //int retorno = -1;
            DataTable retorno = new DataTable();
            try
            {
                retorno = dal.selectDatesBySqnbPdno(ref pdno, ref sqnb, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public DataTable findBySqnbPdnoAndQtdl(ref string pdno, ref string sqnb, ref string qtdl, ref string strError)
        {
            //int retorno = -1;
            DataTable retorno = new DataTable();
            try
            {
                retorno = dal.findBySqnbPdnoAndQtdl(ref pdno, ref sqnb, ref qtdl, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public DataTable consultambpl(ref string strError)
        {
            DataTable retorno = new DataTable();
            try
            {
                retorno = dal.consultambpl(ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public DataTable findBySqnbPdnoLabelPallet(ref string pdno, ref string sqnb, ref string strError)
        {
            //int retorno = -1;
            DataTable retorno = new DataTable();
            try
            {
                retorno = dal.findBySqnbPdnoLabelPallet(ref pdno, ref sqnb, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        //        public DataTable findRecordBySqnbRejectedPlant(ref string sqnb, ref string strError)
        //        {
        //            //int retorno = -1;
        //            DataTable retorno = new DataTable();
        //            try
        //            {
        //                retorno = dal.findRecordBySqnbRejectedPlant(ref sqnb, ref strError);												
        //                return retorno;
        //            }
        //            catch (Exception ex)
        //            {
        //                throw new Exception(ex.InnerException.ToString());
        //            }
        //        }

        public DataTable findRecordBySqnbRejectedPlant(ref string sqnb, ref string strError)
        {
            //int retorno = -1;
            DataTable retorno = new DataTable();
            try
            {
                retorno = dal.findRecordBySqnbRejectedPlantMRBRejection(ref sqnb, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public DataTable findRecordBySqnbRejectedPlantMRBRejection(ref string sqnb, ref string strError)
        {
            //int retorno = -1;
            DataTable retorno = new DataTable();
            try
            {
                retorno = dal.findRecordBySqnbRejectedPlantMRBRejection(ref sqnb, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }


        public DataTable invLabel_registroImprimir_Param(ref Ent_tticol022 Parametros, ref string strError)
        {
            //int retorno = -1;
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

        public DataTable wrapValidation_listaRegistroSec_param(ref Ent_tticol022 Parametros, ref string strError)
        {
            //int retorno = -1;
            DataTable retorno;
            try
            {
                retorno = dal.wrapValidation_listaRegistroSec_param(ref Parametros, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public DataTable validarRegistroByPalletId(ref string palledID, ref string bodegaori, ref string bodegades, ref string strError)
        {
            //int retorno = -1;
            DataTable retorno = new DataTable();
            try
            {
                retorno = dal.validarRegistroByPalletId(ref palledID, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public DataTable consultambrl(ref string strError)
        {
            DataTable retorno = new DataTable();
            try
            {
                retorno = dal.consultambrl(ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public DataTable validateTimeSaveRecord(ref string pdno, ref int tiempo, ref string strError)
        {
            //int retorno = -1;
            DataTable retorno = new DataTable();
            try
            {
                retorno = dal.validateTimeSaveRecord(ref pdno, ref tiempo, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public int InsertarRegistroTicol222(ref Ent_tticol022 data022, ref string strError)
        {
            int retorno = -1;
            try
            {
                retorno = dal.InsertarRegistroTicol222(ref data022, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }

        public string WharehouseTisfc001(string PDNO, ref string strError)
        {
            try
            {
                string retorno = dal.WharehouseTisfc001(PDNO, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }

        public bool ActualizarRegistroTicol222(string USER, string PDNO, string SQNB)
        {
            string strError = string.Empty;
            try
            {
                bool retorno = dal.ActualizarRegistroTicol222(USER, PDNO, SQNB);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }

        public bool ActualizarUbicacionTicol222(string PDNO, string SQNB, string ACLO, string CWAR)
        {
            string strError = string.Empty;
            try
            {
                bool retorno = dal.ActualizarUbicacionTicol222(PDNO, SQNB, ACLO, CWAR);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }

        public bool ActualizarCantidadRegistroTicol222(decimal ACQT, string PDNO)
        {
            string strError = string.Empty;
            try
            {
                bool retorno = dal.ActualizarCantidadRegistroTicol222(ACQT, PDNO);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }

        public bool ActualizarNorpTicol022(Ent_tticol022 Obj_tticol022)
        {
            string strError = string.Empty;
            try
            {
                bool retorno = dal.ActualizarNorpTicol022(Obj_tticol022);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }

        public DataTable ConsultaPorPalletID(ref string PDNO, ref string strError)
        {

            DataTable retorno = new DataTable();

            try
            {

                retorno = dal.ConsultarPorPalletID(ref PDNO, ref strError);

                return retorno;

            }

            catch (Exception ex)
            {

                throw new Exception(strError += "\nPila: " + ex.Message);

            }

        }



        public bool ActualizacionPalletId(string PAID, string STAT, string strError)
        {

            DataTable DT = dal.ActualizacionPalletId(PAID, STAT, strError);

            return true;

        }


        public bool ActualizarCantidadAlmacenRegistroTicol222(string _operator, decimal ACQT, string ACLO, string CWAR, string PAID)
        {
            string strError = string.Empty;
            try
            {
                bool retorno = dal.ActualizarCantidadAlmacenRegistroTicol222(_operator, ACQT, ACLO, CWAR, PAID);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }

        public DataTable SecuenciaMayor(string id)
        {
            string strError = "";
            DataTable retorno;
            try
            {
                retorno = dal.SecuenciaMayor022(id);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }

        public DataTable SecuenciaMayorRT(string id)
        {
            string strError = "";
            DataTable retorno;
            try
            {
                retorno = dal.SecuenciaMayor022RT(id);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }

    }


}

