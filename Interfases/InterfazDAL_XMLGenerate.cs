using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using whusa.Entidades;
using whusa.DAL;
using System.Data;

namespace whusa.Interfases
{
    public class InterfazDAL_XMLGenerate
    {
        XMLGenerate dal = new XMLGenerate();

        public InterfazDAL_XMLGenerate()
        {
        }

        public DataTable listaRegistrosMaquinas(ref string strError)
        {

            //int retorno = -1;
            DataTable retorno;
            try
            {
                retorno = dal.listaRegistrosMaquinas(ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public DataTable listaRegistrosItems(ref string lstMachines, ref string strError, int last)
        {
            //int retorno = -1;
            DataTable retorno;
            try
            {
                retorno = dal.listaRegistrosItems(ref lstMachines, ref strError, last);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public DataTable listaRegistrosItemsUSA(ref string lstMachines, ref string strError, int last)
        {
            DataTable retorno = new DataTable();
            try
            {
                retorno = dal.listaRegistrosItemsUSA(ref lstMachines, ref strError, last);
                return retorno;
            }
            catch (Exception ex)
            {
                strError = ex.InnerException != null ?
                    ex.Message + " (" + ex.InnerException.Message + ")" :
                    ex.Message;
            }
            return retorno;
        }

    }
}
