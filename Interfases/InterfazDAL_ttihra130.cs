using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using whusa.DAL;

namespace whusa.Interfases
{
    public class InterfazDAL_ttihra130
    {
        ttihra130 dal = new ttihra130();

        public InterfazDAL_ttihra130()
        {
            ////Constructor
        }

        public DataTable listRecords(ref string strError) 
        {
            DataTable retorno = new DataTable();
            try
            {
                retorno = dal.listRecords(ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }

        public DataTable listRecordsRSM(ref string strError)
        {
            DataTable retorno = new DataTable();
            try
            {
                retorno = dal.listRecordsRSM(ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }
    }
}
