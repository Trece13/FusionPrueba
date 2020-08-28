using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using whusa.DAL;
using System.Data;

namespace whusa.Interfases
{
    public class InterfazDAL_twhcol125
    {
        twhcol125 dal = new twhcol125();

        public InterfazDAL_twhcol125()
        {
            //Constructor
        }

        public bool updateLocation(ref string loca, ref string paid, ref string strError)
        {
            bool retorno = false;
            try
            {
                retorno = dal.updateLocation(ref loca, ref paid, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }

        public DataTable validateExistsPalletId(ref string palletId, ref string uniqueId, ref string bodegaori, ref string bodegades, ref string strError) 
        {
            DataTable retorno = new DataTable();
            try
            {
                retorno = dal.validateExistsPalletId(ref palletId, ref uniqueId, ref bodegaori, ref bodegades, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }
    }
}
