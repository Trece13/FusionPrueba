using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using whusa.DAL;
using whusa.Entidades;
using System.Data;

namespace whusa.Interfases
{
    public class InterfazDAL_twhcol124
    {
        twhcol124 dal = new twhcol124();

        public InterfazDAL_twhcol124()
        {
            //Constructor
        }

        public int insertarRegistro(ref Ent_twhcol124 parametrosIn, ref string strError)
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

        public DataTable validarRegistroByUniqueId(ref string palletId, ref string strError)
        {
            DataTable retorno = new DataTable();
            try
            {
                retorno = dal.validarRegistroByUniqueId(ref palletId, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public DataTable validarPalletsRecibidosByUniqueId(ref string palletId, ref string strError)
        {
            DataTable retorno = new DataTable();
            try
            {
                retorno = dal.validarPalletsRecibidosByUniqueId(ref palletId, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public DataTable findByOrderNumberSugUbicacionConOrigen(ref string numeroOrden, ref string origen, ref string strError)
        {
            DataTable retorno = new DataTable();
            try
            {
                retorno = dal.findByOrderNumberSugUbicacionConOrigen(ref numeroOrden, ref origen, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }

        public DataTable findBySqnbPdnoNoClot(ref string loca, ref string cwar, ref string pdno, ref string strError)
        {
            //int retorno = -1;
            DataTable retorno;
            try
            {
                retorno = dal.findBySqnbPdnoNoClot(ref loca, ref cwar, ref pdno, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public DataTable findBySqnbPdnoYesClot(ref string loca, ref string cwar, ref string pdno, ref string strError)
        {
            //int retorno = -1;
            DataTable retorno;
            try
            {
                retorno = dal.findBySqnbPdnoYesClot(ref loca, ref cwar, ref pdno, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public DataTable validateLocationByPaid(ref string paid, ref string strError)
        {
            DataTable retorno = new DataTable();
            try
            {
                retorno = dal.validateLocationByPaid(ref paid, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }
    }
}
