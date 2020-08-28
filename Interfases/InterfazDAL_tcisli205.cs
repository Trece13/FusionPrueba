using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace whusa.Interfases
{
    public class InterfazDAL_tcisli205
    {
        tcisli205 dal = new tcisli205();

        public InterfazDAL_tcisli205()
        {
            //Constructor
        }

        public DataTable findByDocumentNumber(ref string documentNumber, ref string strError) 
        {
            DataTable retorno = new DataTable();
            try
            {
                retorno = dal.findByDocumentNumber(ref documentNumber, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }
    }
}
