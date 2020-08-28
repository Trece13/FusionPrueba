using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using whusa.Entidades;
using whusa.DAL;

namespace whusa.Interfases
{
    public class InterfazDAL_ttisfc001
    {
        ttisfc001 dal = new ttisfc001();

        public InterfazDAL_ttisfc001()
        {
            //Constructor
        }

        public DataTable GenericProducts_listaRegistroOrden_Param(ref string pdno, ref string strError)
        {
            //int retorno = -1;
            DataTable retorno;
            try
            {
                retorno = dal.GenericProducts_listaRegistroOrden_Param(ref pdno, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public DataTable findByOrderNumber(ref string pdno, ref string strError)
        {
            //int retorno = -1;
            DataTable retorno;
            try
            {
                retorno = dal.findByOrderNumber(ref pdno, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public DataTable findByPdnoArticulo(ref string pdno, ref string strError)
        {
            //int retorno = -1;
            DataTable retorno;
            try
            {
                retorno = dal.findByPdnoArticulo(ref pdno, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public DataTable findByOrderNumberSugUbicacion(ref string pdno, ref string strError)
        {
            //int retorno = -1;
            DataTable retorno;
            try
            {
                retorno = dal.findByOrderNumberSugUbicacion(ref pdno, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public DataTable findByOrderNumberConfirmRecep(ref string pdno, ref string sqnb, ref bool isPro1False, ref string strError)
        {
            //int retorno = -1;
            DataTable retorno;
            try
            {
                retorno = dal.findByOrderNumberConfirmRecep(ref pdno, ref sqnb, ref isPro1False, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public DataTable findByOrderNumberAnuncioOrd(ref string pdno, ref string strError)
        {
            //int retorno = -1;
            DataTable retorno;
            try
            {
                retorno = dal.findByOrderNumberAnuncioOrd(ref pdno, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public DataTable findByOrderNumberPalletTags(ref string pdno, ref string strError)
        {
            //int retorno = -1;
            DataTable retorno;
            try
            {
                retorno = dal.findByOrderNumberPalletTags(ref pdno, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public DataTable findBySqnbPdnoNoClot(ref string loca, ref string cwar, ref string pdno, ref string sqnb, ref string strError)
        {
            //int retorno = -1;
            DataTable retorno;
            try
            {
                retorno = dal.findBySqnbPdnoNoClot(ref loca, ref cwar, ref pdno, ref sqnb, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public DataTable findBySqnbPdnoYesClot(ref string loca, ref string cwar, ref string pdno, ref string sqnb, ref string strError)
        {
            //int retorno = -1;
            DataTable retorno;
            try
            {
                retorno = dal.findBySqnbPdnoYesClot(ref loca, ref cwar, ref pdno, ref sqnb, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public DataTable findByPdnoSqnbAndPro1(ref string pdno, ref string sqnb, ref string pro1, ref string strError)
        {
            DataTable retorno;
            try
            {
                retorno = dal.findByPdnoSqnbAndPro1(ref pdno, ref sqnb, ref pro1,ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public DataTable findByOrderNumberTime(ref string pdno, ref string strError)
        {
            //int retorno = -1;
            DataTable retorno;
            try
            {
                retorno = dal.findByOrderNumberTime(ref pdno, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public DataTable findByOrderMaterialRejected(ref string pdno, ref string strError)
        {
            //int retorno = -1;
            DataTable retorno;
            try
            {
                retorno = dal.findByOrderMaterialRejected(ref pdno, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public DataTable findByPdnoMaterialRejected(ref string pdno, ref string strError)
        {
            //int retorno = -1;
            DataTable retorno;
            try
            {
                retorno = dal.findByPdnoMaterialRejected(ref pdno, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        //Validar si se maneja o no el parametro de validar produccion
        public DataTable findProdValidation_Parameter(ref string strError)
        {
            DataTable retorno;
            try
            {
                retorno = dal.findProdValidation_Parameter(ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }
    }
}
