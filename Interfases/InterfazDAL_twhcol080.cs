using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using whusa.DAL;
using System.Data;
using whusa.Entidades;

namespace whusa.Interfases
{
    public class InterfazDAL_twhcol080
    {
        twhcol080 dal = new twhcol080();

        public InterfazDAL_twhcol080()
        {
            //Constructor
        }

        public int insertRecord(ref Ent_twhcol080 datatwhcol080, ref string strError)
        {
            int retorno = -1;
            try
            {
                retorno = dal.insertRecord(ref datatwhcol080, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }

        public bool updateRecord(ref Ent_twhcol080 datatwhcol080, ref string strError)
        {
            bool retorno = false;
            try
            {
                retorno = dal.updateRecord(ref datatwhcol080, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }

        public bool updateRecordPicking(ref Ent_twhcol080 parametro, ref string strError)
        {
            bool retorno = false;
            try
            {
                retorno = dal.updateRecordPicking(ref parametro, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }

        public bool updateRecordConfPicking(ref Ent_twhcol080 parametro, ref string strError)
        {
            bool retorno = false;
            try
            {
                retorno = dal.updateRecordConfPicking(ref parametro, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }

        public bool updateRecordConfirm(ref Ent_twhcol080 parametro, ref string strError)
        {
            bool retorno = false;
            try
            {
                retorno = dal.updateRecordConfirm(ref parametro, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }

        public DataTable findRecordByProc(ref string orderNumber, ref string strError) 
        {
            DataTable retorno = new DataTable();
            try
            {
                retorno = dal.findRecordByProc(ref orderNumber, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }

        public DataTable findRecord(ref Ent_twhcol080 datatwhcol080, ref string strError)
        {
            DataTable retorno = new DataTable();
            try
            {
                retorno = dal.findRecord(ref datatwhcol080, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }

        public DataTable findRecordByOrnoSour(ref string orderNumber, ref string sour, ref string strError)
        {
            DataTable retorno = new DataTable();
            try
            {
                retorno = dal.findRecordByOrnoSour(ref orderNumber, ref sour, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }    
    }
}
