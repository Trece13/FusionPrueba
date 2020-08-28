using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using whusa.DAL;
using System.Data;

namespace whusa.Interfases
{
    public class InterfazDAL_tqmcol040
    {
        tqmcol040 dal = new tqmcol040();

        public InterfazDAL_tqmcol040() 
        {
            //Constructor
        }

        public DataTable getSampleSize(ref string stbp, ref string tamLote, ref string strError)
        {
            DataTable retorno = new DataTable();
            try
            {
                retorno = dal.getSampleSize(ref stbp, ref tamLote ,ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }
    }
}
