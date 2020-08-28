using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using whusa.DAL;
using System.Data;

namespace whusa.Interfases
{
    public class InterfazDAL_tqmptc011
    {
        tqmptc011 dal = new tqmptc011();

        public InterfazDAL_tqmptc011() 
        {
            //Constructor
        }

        public DataTable findRecordByItem(ref string item, ref string strError)
        {
            DataTable retorno = new DataTable();
            try
            {
                retorno = dal.findRecordByItem(ref item, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }

        public DataTable findRecordByItemConOrigen(ref string item, ref string oorg, ref string strError)
        {
            DataTable retorno = new DataTable();
            try
            {
                retorno = dal.findRecordByItemConOrigen(ref item, ref oorg, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }
    }
}
