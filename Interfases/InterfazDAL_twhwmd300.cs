using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using whusa.Entidades;
using whusa.DAL;

namespace whusa.Interfases
{
    public class InterfazDAL_twhwmd300
    {
        twhwmd300 dal = new twhwmd300();

        public InterfazDAL_twhwmd300()
        {
        }

        public DataTable listaRegistro_ObtieneAlmacen(ref Ent_twhwmd300 Parametros, ref string strError)
        {
            DataTable retorno;
            try
            {
                retorno = dal.listaRegistro_ObtieneAlmacen(ref Parametros, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public DataTable listaRegistro_AlmacenUbicaion(ref Ent_twhwmd300 Parametros, ref string strError)
        {
            DataTable retorno;
            try
            {
                retorno = dal.listaRegistro_AlmacenUbicaion(ref Parametros, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public DataTable consultaSourceAndDestinationWarehouse(ref string CWARSOURCE, ref string CWARDESTINATION, ref string strError) 
        {
            DataTable retorno;
            try
            {
                retorno = dal.consultaSourceAndDestinationWarehouse(ref CWARSOURCE, ref CWARDESTINATION, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public DataTable validateExistsLocation(ref string strLocation, ref string sourceWarehouse, ref string strError) 
        {
            DataTable retorno = new DataTable();
            try
            {
                retorno = dal.validateExistsLocation(ref strLocation, ref sourceWarehouse, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public DataTable validaLocationWithPdno(ref string LOCA, ref string PDNO, ref string strError) 
        {
            DataTable retorno = new DataTable();
            try
            {
                retorno = dal.validaLocationWithPdno(ref LOCA, ref PDNO, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public DataTable validaLocationWithPdnoConOrigen(ref string LOCA, ref string PDNO, ref string strError)
        {
            DataTable retorno = new DataTable();
            try
            {
                retorno = dal.validaLocationWithPdnoConOrigen(ref LOCA, ref PDNO, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public DataTable consultaPorAlmacenUbicacion(ref string clot, ref string item, ref string cwar, ref string loca, ref string strError)
        {
            DataTable retorno = new DataTable();
            try
            {
                retorno = dal.consultaPorAlmacenUbicacion(ref clot, ref item, ref cwar, ref loca, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public DataTable consultaPlantasTransfer(ref string strError)
        {
            DataTable retorno = new DataTable();
            try
            {
                retorno = dal.consultaPlantasTransfer(ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public DataTable findLocationByLoctAndBtrr(ref string loct, ref string btrr, ref string loca, ref string strError)
        {
            DataTable retorno = new DataTable();
            try
            {
                retorno = dal.findLocationByLoctAndBtrr(ref loct, ref btrr, ref loca, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }
    }
}
