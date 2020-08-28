using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using whusa.Entidades;
using System.Data;

namespace whusa.Interfases
{
    public class InterfazDAL_twhcol019  //
    {
        twhcol019 dal = new twhcol019();

        public bool insertRegistertwhcol019(ref Ent_twhcol019 objTwhcol019, ref string strError)
        {
            bool retorno = false;
            try
            {
                retorno = dal.insertRegistertwhcol019(ref objTwhcol019, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }

        public DataTable Consetwhcol019(string paid, ref string strError)
        {
            DataTable retorno;
            try
            {
                retorno = dal.Consetwhcol019(paid, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }
    }
}
