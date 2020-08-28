using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using whusa.DAL;
using System.Data;
using whusa.Entidades;

namespace whusa.Interfases
{
    public class InterfazDAL_twhcol020
    {
        twhcol020 dal = new twhcol020();

        public InterfazDAL_twhcol020()
        {
            //Constructor
        }

        public int insertarRegistro(ref Ent_twhcol020 parametrosIn, ref string strError)
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

        public int consultarConsecutivoRegistro(ref string clot, ref string strError)
        {
            int retorno = -1;
            try
            {
                retorno = dal.consultarConsecutivoRegistro(ref clot, ref strError);

                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }

        public int consultarConsecutivoRegistroPorItem(ref string item, ref string lode, ref string strError)
        {
            int retorno = -1;
            try
            {
                retorno = dal.consultarConsecutivoRegistroPorItem(ref item, ref lode, ref strError);

                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }

        public int consultarConsecutivoMaximo(ref string strError)
        {
            int retorno = -1;
            try
            {
                retorno = dal.consultarConsecutivoMaximo(ref strError);

                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }
    }
}
