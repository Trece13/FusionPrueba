using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using whusa.Entidades;
using whusa.DAL;

namespace whusa.Interfases
{
    public class InterfazDAL_tticol080
    {
        tticol080 dal = new tticol080();
        // Constructor
        static InterfazDAL_tticol080()
        { 
        }

        public int insertarRegistro(ref List<Ent_tticol080> parametros, ref string strError, ref string strTagId )
        {
            int retorno = -1;
            try
            {
                retorno = dal.insertarRegistro(ref parametros, ref strError, ref strTagId);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }

        public int insertarRegistro_regrindM(ref List<Ent_tticol080> parametros, ref string strError, ref string strTagId)
        {
            int retorno = -1;
            try
            {
                retorno = dal.insertarRegistro_regrindM(ref parametros, ref strError, ref strTagId);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }        
        
        public int actualizarRegistro_Param(ref List<Ent_tticol080> parametros, ref string strError, ref string strTagId)
        {
            int retorno = -1;
            try
            {
                retorno = dal.actualizarRegistro_Param(ref parametros, ref strError, ref strTagId);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }

        public bool updateRecordCosts(ref Ent_tticol080 data, ref string strError)
        {
            //int retorno = -1;
            bool retorno;
            try
            {
                retorno = dal.updateRecordCosts(ref data, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public bool updateRecordRollAnnounce(ref Ent_tticol080 data, ref string strError)
        {
            //int retorno = -1;
            bool retorno;
            try
            {
                retorno = dal.updateRecordRollAnnounce(ref data, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public DataTable findRecordByOrnoPono(ref string orno, ref string pono, ref string strError)
        {
            //int retorno = -1;
            DataTable retorno;
            try
            {
                retorno = dal.findRecordByOrnoPono(ref orno, ref pono, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public DataTable listaRegistroImprimir_Param(ref Ent_tticol080 ParametrosIn, ref string strError)
        {
            //int retorno = -1;
            DataTable retorno;
            try
            {
                retorno = dal.listaRegistroImprimir_Param(ref ParametrosIn, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public DataTable findRecordByOrnoPonoItem(ref string orno, ref string pono, ref string item, ref string strError)
        {
            //int retorno = -1;
            DataTable retorno;
            try
            {
                retorno = dal.findRecordByOrnoPonoItem(ref orno, ref pono, ref item, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }
    }
}
