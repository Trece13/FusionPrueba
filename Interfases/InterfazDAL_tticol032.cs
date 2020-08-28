using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using whusa.DAL;
using whusa.Entidades;
using System.Data;

namespace whusa.Interfases
{
    public class InterfazDAL_tticol032
    {
        tticol032 dal = new tticol032();

        static InterfazDAL_tticol032()
        {
        }

        public int insertarRegistro(ref Ent_tticol032 parametros, ref string strError)
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

        public int consultarConsecutivoRegistro(ref string pdno, ref string strError) 
        {
            int retorno = -1;
            try
            {
                retorno = dal.consultarConsecutivoRegistro(ref pdno, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }

        public DataTable consultaPorPalletBodegas(ref string sqnb, ref string cwar, ref string loca, ref string strError)
        {
            DataTable retorno = new DataTable();
            try
            {
                retorno = dal.consultaPorPalletBodegas(ref sqnb, ref cwar, ref loca, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }
    }
}
