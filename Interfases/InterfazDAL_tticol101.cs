using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using whusa.DAL;
using System.Data;

namespace whusa.Interfases
{
    public class InterfazDAL_tticol101
    {
        tticol101 dal = new tticol101();

        public InterfazDAL_tticol101()
        {
            //Constructor
        }

        public DataTable findRecordByPdnoClotSeqn(ref string pdno, ref string clot, ref string seqn, ref string strError)
        {
            DataTable retorno = new DataTable();
            try
            {
                retorno = dal.findRecordByPdnoClotSeqn(ref pdno, ref clot, ref seqn, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }

        public DataTable findRecordByPdnoSeqnAndPono(ref string pdno, ref string seqn, ref string comparative, ref string strError)
        {
            DataTable retorno = new DataTable();
            try
            {
                retorno = dal.findRecordByPdnoSeqnAndPono(ref pdno, ref seqn, ref comparative, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }
    }
}
