using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using whusa.Entidades;
using whusa.DAL;


namespace whusa.Interfases
{
    public class InterfazDAL_ttccol301
    {
        ttccol301 dal = new ttccol301();

        static InterfazDAL_ttccol301()
        {
        }

        public int insertarRegistro(ref List<Ent_ttccol301> parametros, ref string strError)
        {
            int retorno = -1;
            try
            {
                retorno = dal.insertarRegistro(ref parametros, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }
    }
}
