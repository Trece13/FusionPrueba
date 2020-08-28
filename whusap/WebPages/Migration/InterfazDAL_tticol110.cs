using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using whusa.DAL;
using System.Data;

namespace whusap.WebPages.Migration
{
    public class InterfazDAL_tticol110
    {
        tticol110 dal = new tticol110();

        public InterfazDAL_tticol110()
        {
            //Constructor
        }

        public DataTable LimitePorArticuloyMaquinaTticol110(ref string mcno, ref string strError)
        {
            DataTable retorno = new DataTable();
            try
            {
                //retorno = dal.findRecordsByMcnoTimeRegistration(ref mcno, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }

    }
}
