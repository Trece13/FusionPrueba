using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using whusa.DAL;

namespace whusa.Interfases
{
    public class InterfazDAL_twhinh431
    {
        twhinh431 dal = new twhinh431();

        public InterfazDAL_twhinh431() 
        {
            //Constructor
        }

        public DataTable getLotInfo(ref string shpm, ref string strError)
        {
            DataTable retorno = new DataTable();
            try
            {
                retorno = dal.getLotInfo(ref shpm, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }

        public DataTable getLotSize(ref string shpm, ref string clot, ref string strError) 
        {
            DataTable retorno = new DataTable();
            try
            {
                retorno = dal.getLotSize(ref shpm, ref clot, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }
    }
}
