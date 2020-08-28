using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using whusa.DAL;
using System.Data;

namespace whusa.Interfases
{
    public class InterfazDAL_twhcol119
    {
        twhcol119 dal = new twhcol119();

        public InterfazDAL_twhcol119()
        {
            //Constructor
        }

        public DataTable validateLocation(ref string sourceWareHouse, ref string strError)
        {
            DataTable retorno = new DataTable();
            try
            {
                retorno = dal.validateLocation(ref sourceWareHouse, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }
    }
}
