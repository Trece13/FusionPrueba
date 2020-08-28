using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using whusa.DAL;
using whusa.Entidades;
using System.Data;

namespace whusa.Interfases
{
    public class InterfazDAL_tticol025
    {
        tticol025 dal = new tticol025();

        public InterfazDAL_tticol025()
        {
            //Constructor
        }

        public int insertarRegistro(ref Ent_tticol025 parametrosIn, ref string strError)
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
        
        public DataTable sumQtdlByOrderNumber(ref string pdno, ref string strError) 
        {
            DataTable retorno = new DataTable();
            try
            {
                retorno = dal.sumQtdlByOrderNumber(ref pdno, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }
    }
}
