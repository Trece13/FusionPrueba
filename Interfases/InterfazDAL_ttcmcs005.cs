using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using whusa.DAL;
using System.Data;

namespace whusa.Interfases
{
    public class InterfazDAL_ttcmcs005
    {
        ttcmcs005 dal = new ttcmcs005();

        public InterfazDAL_ttcmcs005()
        {
            //Constructor
        }

        public DataTable findRecords(ref string rstp, ref string strError)
        {
            DataTable retorno = new DataTable();
            try
            {
                retorno = dal.findRecords(ref rstp, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }
    }
}
