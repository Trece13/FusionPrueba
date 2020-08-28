using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using whusa.DAL;
using System.Data;

namespace whusa.Interfases
{
    public class InterfazDAL_tticol077
    {
        tticol077 dal = new tticol077();

        public InterfazDAL_tticol077()
        {
            //Constructor
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
    }
}
