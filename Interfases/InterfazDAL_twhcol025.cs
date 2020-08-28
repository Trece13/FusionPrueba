using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using whusa.Entidades;
using whusa.DAL;

namespace whusa.Interfases
{
    public class InterfazDAL_twhcol025
    {
        twhcol025 dal = new twhcol025();

        static InterfazDAL_twhcol025()
        {
        }

        public int insertRegistrItemAdjustment(ref List<Ent_twhcol025> parametros, ref string strError)
        {
            int retorno = -1;
            try
            {
                retorno = dal.insertRegistrItemAdjustment(ref parametros, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }
    }
}
