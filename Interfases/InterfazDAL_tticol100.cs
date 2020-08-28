using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using whusa.DAL;
using System.Data;
using whusa.Entidades;

namespace whusa.Interfases
{
    public class InterfazDAL_tticol100
    {
        tticol100 dal = new tticol100();

        public InterfazDAL_tticol100()
        {
            //Constructor
        }

        public int insertRecord(ref Ent_tticol100 parametrosIn, ref string strError)
        {
            int retorno = -1;
            try
            {
                retorno = dal.insertRecord(ref parametrosIn, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }

        public int ActualizaRegistro_ticol022(ref Ent_tticol100 parametrosIn, ref string strError)
        {
            int retorno = -1;
            try
            {
                retorno = dal.ActualizaRegistro_ticol022(ref parametrosIn, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }
        public int ActualizaRegistro_located(ref Ent_tticol100 parametrosIn,ref string updstatus,ref string tableName, ref string strError)
        {
            int retorno = -1;
            try
            {
                retorno = dal.ActualizaRegistro_located(ref parametrosIn,ref updstatus,ref tableName, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }
         public int ActualUpdateWarehouse_ticol222(ref Ent_tticol100 data, ref string strError)
        {
            int retorno = -1;
            try
            {
                retorno = dal.ActualUpdateWarehouse_ticol222(ref data, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }
         public int ActualUpdateStockWarehouse_ticol222(ref string tableName,ref string stockw,ref string palletId, ref string strError)
         {
             int retorno = -1;
             try
             {
                 retorno = dal.ActualUpdateStockWarehouse_ticol222(ref tableName, ref stockw, ref palletId, ref strError);
                 return retorno;
             }
             catch (Exception ex)
             {
                 throw new Exception(strError += "\nPila: " + ex.Message);
             }
         }
         public int ActualUpdateWarehouse_whcol131(ref Ent_tticol100 data, ref string strError)
         {
             int retorno = -1;
             try
             {
                 retorno = dal.ActualUpdateWarehouse_whcol131(ref data, ref strError);
                 return retorno;
             }
             catch (Exception ex)
             {
                 throw new Exception(strError += "\nPila: " + ex.Message);
             }
         }

        public DataTable findMaxSeqnByPdno(ref string pdno, ref string strError)
        {
            DataTable retorno = new DataTable();
            try
            {
                retorno = dal.findMaxSeqnByPdno(ref pdno, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }
        public DataTable findMaxSeqnByPdnoPono(ref string pdno, ref string pono, ref string strError)
        {
            DataTable retorno = new DataTable();
            try
            {
                retorno = dal.findMaxSeqnByPdnoPono(ref pdno, ref pono, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }

        public DataTable findRecordByPdnoSeqnAndPono(ref string pdno, ref string seqn, ref string comparative, ref string strError)
        {
            DataTable retorno = new DataTable();
            try
            {
                retorno = dal.findRecordByPdnoSeqnAndPono(ref pdno, ref seqn,ref comparative, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }
    }
}
