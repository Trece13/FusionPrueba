using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using whusa.DAL;
using System.Data;

namespace whusa.Interfases
{
    public class InterfazDAL_ttirou003
    {
        ttirou003 dal = new ttirou003();

        public InterfazDAL_ttirou003()
        {
            ////Constructor
        }

        public DataTable listRecordsByCkot(ref string ckot, ref string strError) 
        {
            DataTable retorno = new DataTable();
            try
            {
                retorno = dal.listRecordsByCkot(ref ckot, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }
    }
}
