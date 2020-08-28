using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using whusa.DAL;
using whusa.Entidades;

namespace whusa.Interfases
{
    public class InterfazDAL_tticol095
    {
        tticol095 dal = new tticol095();

        public InterfazDAL_tticol095()
        {
            //Constructor
        }

        public int insertarRegistro(ref Ent_tticol095 parametrosIn, ref string strError)
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

        public int consultaSeqn(ref string mcno, ref string cwar, ref string item, ref string strError)
        {
            int retorno = -1;
            try
            {
                retorno = dal.consultaSeqn(ref mcno, ref cwar, ref item, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }
    }
}
