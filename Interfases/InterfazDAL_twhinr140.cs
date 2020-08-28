using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using whusa.Entidades;
using whusa.DAL;


namespace whusa.Interfases
{
    public class InterfazDAL_twhinr140
    {

        twhinr140 dal = new twhinr140();

        public InterfazDAL_twhinr140()
        {
        }

        public DataTable listaRegistros_ObtieneItem(ref Ent_twhinr140 Parametros, ref string strError)
        {
            //int retorno = -1;
            DataTable retorno;
            try
            {
                retorno = dal.listaRegistros_ObtieneItem(ref Parametros, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public DataTable consultaPorAlmacenItem(ref string cwar, ref string item, ref string strError)
        {
            //int retorno = -1;
            DataTable retorno;
            try
            {
                retorno = dal.consultaPorAlmacenItem(ref cwar, ref item, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public DataTable consultaCantidadItemLote(ref string cwar, ref string item, ref string strError)
        {
            //int retorno = -1;
            DataTable retorno;
            try
            {
                retorno = dal.consultaCantidadItemLote(ref cwar, ref item, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public DataTable consultaPorAlmacenUbicacion(ref string cwar, ref string loca, ref string strError)
        {
            //int retorno = -1;
            DataTable retorno;
            try
            {
                retorno = dal.consultaPorAlmacenUbicacion(ref cwar, ref loca, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public DataTable consultaPorAlmacenItemLote(ref string cwar, ref string item, ref string clot, ref string strError)
        {
            //int retorno = -1;
            DataTable retorno;
            try
            {
                retorno = dal.consultaPorAlmacenItemLote(ref cwar, ref item, ref clot, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public DataTable consultaPorAlmacenItemUbicacion(ref string cwar, ref string item, ref string loca, ref string strError)
        {
            //int retorno = -1;
            DataTable retorno;
            try
            {
                retorno = dal.consultaPorAlmacenItemUbicacion(ref cwar, ref item, ref loca, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public DataTable consultaPorAlmacenItemUbicacionLote(ref string cwar, ref string item, ref string loca, ref string lot, ref string strError)
        {
            //int retorno = -1;
            DataTable retorno;
            try
            {
                retorno = dal.consultaPorAlmacenItemUbicacionLote(ref cwar, ref item, ref loca, ref lot, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public DataTable consultaStks(ref string CWAR, ref string ITEM, ref string CLOT, ref string LOCA, ref string strError)
        {
            //int retorno = -1;
            DataTable retorno;
            try
            {
                retorno = dal.consultaStks(ref CWAR, ref ITEM, ref CLOT, ref LOCA, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }
    }
}
