using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using whusa.DAL;
using System.Data;
using whusa.Entidades;

namespace whusa.Interfases
{
    public class IntefazDAL_twhinh225
    {
        twhinh225 dal = new twhinh225();

        public IntefazDAL_twhinh225()
        {
            //Constructor
        }

        public DataTable findRecordByOrderNumber(ref Ent_twhinh225 data, ref string strError)
        {
            DataTable retorno = new DataTable();
            try
            {
                retorno = dal.findRecordByOrderNumber(ref data, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }
    }
}
