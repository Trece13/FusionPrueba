using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using whusa.Entidades;
using whusa.Entidades.Documentos;
using System.Data;
using whusa.DAL;

namespace whusa.Interfases
{
    public class InterfazDAL_tcisli245
    {
        tcisli245 dal = new tcisli245();

        public InterfazDAL_tcisli245() 
        {
            //Constructor
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
