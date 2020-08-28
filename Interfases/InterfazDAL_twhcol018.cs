using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using whusa.Entidades;
using whusa.DAL;
using System.Data;

namespace whusa.Interfases
{
    public class InterfazDAL_twhcol018
    {
        twhcol018 dal = new twhcol018();

        public InterfazDAL_twhcol018()
        {
        }

        public int insertarRegistro(ref List<Ent_twhcol018> parametrosIn, ref string strError, ref string strTagId)
        {
            int retorno = -1;
            try
            {
                retorno = dal.insertarRegistro(ref parametrosIn, ref strError, ref strTagId);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }
    }
}
