using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using whusa.DAL;
using System.Data;

namespace whusa.Interfases
{
    public class InterfazDAL_twhcol123
    {
        twhcol123 dal = new twhcol123();

        public InterfazDAL_twhcol123()
        {
            //Constructor
        }

        public DataTable validarRegistroByPalletId(ref string palletId, ref string uniqueId, ref string bodegaori, ref string bodegades, ref string strError) 
        {
            DataTable retorno = new DataTable();
            try
            {
                retorno = dal.validarRegistroByPalletId(ref palletId, ref uniqueId, ref bodegaori, ref bodegades, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }

        public DataTable validarRegistroByUniqueId(ref string uniqueId, ref string strError)
        {
            DataTable retorno = new DataTable();
            try
            {
                retorno = dal.validarRegistroByUniqueId(ref uniqueId, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }
    }
}
