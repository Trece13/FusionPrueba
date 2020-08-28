using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using whusa.DAL;
using System.Data;

namespace whusa.Interfases
{
    public class InterfazDAL_tticol130
    {
        tticol130 dal = new tticol130();

        public DataTable listarItems(string sItem, ref string strError)
        {
            DataTable retorno;
            try
            {
                retorno = dal.listarItems(sItem, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                strError = ex.InnerException != null ?
                    ex.Message + " (" + ex.InnerException + ") " :
                    ex.Message;
                retorno = null;
            }
            return retorno;
        }
    }
}
