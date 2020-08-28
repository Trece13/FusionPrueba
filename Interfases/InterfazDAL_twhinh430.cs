using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace whusa.Interfases
{
    public class InterfazDAL_twhinh430
    {
        twhinh430 dal = new twhinh430();

        public InterfazDAL_twhinh430()
        {
            //Constructor
        }

        public DataTable findByDocumentNumber(ref string documentNumber, ref string strError) 
        {
            DataTable retorno = new DataTable();
            try
            {
                retorno = dal.findByDocumentNumber(ref documentNumber, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }

        public DataTable getInformationDocumentNational(ref string numeroFactura, ref string strError)
        {
            DataTable retorno = new DataTable();
            try
            {
                retorno = dal.getInformationDocumentNational(ref numeroFactura, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }
    }
}
