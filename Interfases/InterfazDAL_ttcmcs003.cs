using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using whusa.DAL;
using System.Data;

namespace whusa.Interfases
{
    public class InterfazDAL_ttcmcs003
    {
        ttcmcs003 dal = new ttcmcs003();

        public InterfazDAL_ttcmcs003()
        {
            //Constructor
        }

        public DataTable findRecordByCwar(ref string cwar, ref string strError)
        {
            DataTable retorno = new DataTable();
            try
            {
                retorno = dal.findRecordByCwar(ref cwar, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }
        public DataTable listRecordCwar(ref string strError)
        {
            DataTable retorno = new DataTable();
            try
            {
                retorno = dal.listRecordCwar(ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }
    }
}
