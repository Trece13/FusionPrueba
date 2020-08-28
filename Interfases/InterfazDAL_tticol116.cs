using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using whusa.DAL;
using System.Data;
using whusa.Entidades;

namespace whusa.Interfases
{
    public class InterfazDAL_tticol116
    {
        tticol116 dal = new tticol116();

         public InterfazDAL_tticol116()
        {
            //Constructor
        }

         public int insertarRegistro(ref Ent_tticol116 parametrosIn, ref string strError)
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

        public DataTable findRecordByWarehouseItemLocation(ref string item, ref string cwar, ref string loca, ref string strError) 
        {
            DataTable retorno = new DataTable();
            try
            {
                retorno = dal.findRecordByWarehouseItemLocation(ref item, ref cwar, ref loca, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }

	        public int UpdatePalletStatus_ticol022(ref Ent_tticol116 parametrosIn, ref string strError)
        {
            int retorno = -1;
            try
            {
                retorno = dal.UpdatePalletStatus_ticol022(ref parametrosIn, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }
        public int ActualUpdateWarehouse_ticol222(ref Ent_tticol116 parametrosIn, ref string strError)
        {

            int retorno = -1;
            try
            {
                retorno = dal.ActualUpdateWarehouse_ticol222(ref parametrosIn, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }
        public int ActualUpdateWarehouse_whcol131(ref Ent_tticol116 parametrosIn, ref string strError)
        {

            int retorno = -1;
            try
            {
                retorno = dal.ActualUpdateWarehouse_whcol131(ref parametrosIn, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }
        public bool updateRecordRejectedWarehouse(ref string item, ref string cwar, ref string loca, ref double qtyr, ref string strError)
        {
            bool retorno;
            try
            {
                retorno = dal.updateRecordRejectedWarehouse(ref item, ref cwar, ref loca, ref qtyr, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }
    }
}
