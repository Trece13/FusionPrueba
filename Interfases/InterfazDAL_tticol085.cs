using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using whusa.DAL;
using whusa.Entidades;

namespace whusa.Interfases
{
    public class InterfazDAL_tticol085
    {
        tticol085 dal = new tticol085();

        public InterfazDAL_tticol085()
        {
            //Constructor
        }

        public int insertarRegistro(ref Ent_tticol085 parametrosIn, ref string strError)
        {
            int retorno = -1;
            try
            {
                retorno = dal.insertarRegistro(ref parametrosIn, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }
    }
}
