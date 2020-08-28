using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using whusa.DAL;
using System.Data;

namespace whusa.Interfases
{
    public class InterfazDAL_tticol111
    {
        tticol111 dal = new tticol111();

        public InterfazDAL_tticol111()
        {
            //Constructor
        }

        public DataTable findRecordsByMcnoTimeRegistration(ref string mcno, ref string strError)
        {
            DataTable retorno = new DataTable();
            try
            {
                retorno = dal.findRecordsByMcnoTimeRegistration(ref mcno, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }

        public DataTable findRecordByMcno(ref string mcno, ref string strError)
        {
            DataTable retorno = new DataTable();
            try
            {
                retorno = dal.findRecordByMcno(ref mcno, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }

        public DataTable findRecordByPdno(ref string pdno, ref string strError)
        {
            DataTable retorno = new DataTable();
            try
            {
                retorno = dal.findRecordByPdno(ref pdno, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }
    }
}
