using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using whusa.DAL;
using System.Data;
using whusa.Entidades;

namespace whusa.Interfases
{
    public class InterfazDAL_twhinh210
    {
         twhinh210 dal = new twhinh210();

         public InterfazDAL_twhinh210()
        {
            //Constructor
        }

         public DataTable findByNumberReceipt(ref string numberReceipt, ref string strError) 
        {
            DataTable retorno = new DataTable();
            try
            {
                retorno = dal.findByNumberReceipt(ref numberReceipt, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }

         public DataTable findByNumberReceiptAndOrigin(ref string numberReceipt, ref string origin, ref string strError)
         {
             DataTable retorno = new DataTable();
             try
             {
                 retorno = dal.findByNumberReceiptAndOrigin(ref numberReceipt, ref origin ,ref strError);
                 return retorno;
             }
             catch (Exception ex)
             {
                 throw new Exception(strError += "\nPila: " + ex.Message);
             }
         }

         public DataTable findByOrderNumberAndOrigin(ref Ent_twhinh210 data, ref string strError)
         {
             DataTable retorno = new DataTable();
             try
             {
                 retorno = dal.findByOrderNumberAndOrigin(ref data, ref strError);
                 return retorno;
             }
             catch (Exception ex)
             {
                 throw new Exception(strError += "\nPila: " + ex.Message);
             }
         }
    }
}
