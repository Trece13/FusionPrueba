using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using whusa.Entidades;
using whusa.DAL;

namespace whusa.Interfases
{
    public class InterfazDAL_tticol042
    {
        tticol042 dal = new tticol042();

        static InterfazDAL_tticol042()
        {
        }

        public int insertarRegistro(ref List<Ent_tticol042> parametros, ref string strError)
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

        public int wrapRegrind_ActualizaRegistro(ref List<Ent_tticol042> parametrosIn, ref string strError)
        {
            int retorno = -1;
            try
            {
                retorno = dal.wrapRegrind_ActualizaRegistro(ref parametrosIn, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }

        public int insertarRegistroSimple(ref Ent_tticol042 parametros, ref string strError)
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

        public int insertarRegistroSimpleD(ref Ent_tticol042 parametros, ref string strError)
        {
            int retorno = -1;
            try
            {
                retorno = dal.insertarRegistroSimpleD(ref parametros, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }

        public int InsertarRegistroTicol242(ref Ent_tticol042 data042, ref string strError)
        {
            int retorno = -1;
            try
            {
                retorno = dal.InsertarRegistroTicol242(ref data042, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }

        public int actualizaRegistro_ConfirmedRegrind(ref List<Ent_tticol042> parametrosIn, ref string strError)
        {
            int retorno = -1;
            try
            {
                retorno = dal.actualizaRegistro_ConfirmedRegrind(ref parametrosIn, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }

        public int actualizaRegistro_LocationRegrind(ref List<Ent_tticol042> parametrosIn, ref string strError)
        {
            int retorno = -1;
            try
            {
                retorno = dal.actualizaRegistro_LocationRegrind(ref parametrosIn, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }

        public int ActualizaRegistro_ReprintRegrind(ref List<Ent_tticol042> parametrosIn, ref string strError)
        {
            int retorno = -1;
            try
            {
                retorno = dal.ActualizaRegistro_ReprintRegrind(ref parametrosIn, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }

        public bool ActualizarUbicacionTicol242(string PDNO, string SQNB, string ACLO, string CWAR)
        {
            string strError = string.Empty;
            try
            {
                bool retorno = dal.ActualizarUbicacionTicol242(PDNO, SQNB, ACLO, CWAR);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }


        public DataTable listaCantidadRegrind(ref Ent_tticol042 ParametrosIn, ref string strError)
        {
            DataTable retorno;
            try
            {
                retorno = dal.listaCantidadRegrind(ref ParametrosIn, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public DataTable listaRegistroXSQNB(ref Ent_tticol042 Parametros, ref string strError)
        {
            DataTable retorno;
            try
            {
                retorno = dal.listaRegistroXSQNB(ref Parametros, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public DataTable listaRegistroXSQNB_ConfirmedRegrind(ref Ent_tticol042 Parametros, ref string strError)
        {
            DataTable retorno;
            try
            {
                retorno = dal.listaRegistroXSQNB_ConfirmedRegrind(ref Parametros, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public DataTable listaRegistroXSQNB_FindLocation(ref Ent_tticol042 Parametros, ref string strError)
        {
            DataTable retorno;
            try
            {
                retorno = dal.listaRegistroXSQNB_FindLocation(ref Parametros, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public DataTable listaRegistroXSQNB_LocatedRegrind(ref Ent_tticol042 Parametros, ref string strError)
        {
            DataTable retorno;
            try
            {
                retorno = dal.listaRegistroXSQNB_LocatedRegrind(ref Parametros, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public DataTable ListaRegistro_ReprintRegrind(ref Ent_tticol042 Parametros, ref string strError)
        {
            DataTable retorno;
            try
            {
                retorno = dal.ListaRegistro_ReprintRegrind(ref Parametros, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public bool insertarRegistroTticon242(ref List<Ent_tticol042> parameterCollectionRegrind, ref string strError)
        {
            bool retorno = false;
            //try
            //{
                retorno = dal.insertarRegistroTticon242(ref parameterCollectionRegrind, ref strError);
                return retorno;
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception(strError += "\nPila: " + ex.Message);
            //}
        }

        public bool ActualizarRegistroTticon242(ref List<Ent_tticol042> parameterCollectionRegrind, ref string strError)
        {
            bool retorno = false;
            try
            {
                retorno = dal.ActualizarRegistroTticon242(ref parameterCollectionRegrind, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }

        public bool ActualizarCantidadAlmacenRegistroTicol242(string _operator, double ACQT, string ACLO, string CWAR, string PAID)
        {
            string strError = string.Empty;
            try
            {
                bool retorno = dal.ActualizarCantidadAlmacenRegistroTicol242(_operator, ACQT, ACLO, CWAR, PAID);
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
                retorno = dal.SecuenciaMayor042(id);
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
    }
}
