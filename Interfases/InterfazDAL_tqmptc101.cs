using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using whusa.DAL;
using System.Data;

namespace whusa.Interfases
{
    public class InterfazDAL_tqmptc101
    {
        tqmptc101 dal = new tqmptc101();

        public InterfazDAL_tqmptc101() 
        {
            //Constructor
        }

        public DataTable getProductInfo(ref string iorn, ref string strError)
        {
            DataTable retorno = new DataTable();
            try
            {
                retorno = dal.getProductInfo(ref iorn, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }

        public DataTable getAttributesProduct(ref string ctxt, ref string strError) 
        {
            DataTable retorno = new DataTable();
            try
            {
                retorno = dal.getAttributesProduct(ref ctxt, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }
    }
}
