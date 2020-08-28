using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.ServiceModel.Syndication;
using whusa.Entidades;
using whusa.DAL;

namespace whusa.Interfases
{
    public class InterfazDAL_ttcibd001
    {
        ttcibd001 dal = new ttcibd001();

        public InterfazDAL_ttcibd001()
        {
        }

        public DataTable listaRegistro_ObtieneClotUnitCOnv(ref Ent_ttcibd001 Parametros, ref string strError)
        {
            DataTable retorno;
            try
            {
                retorno = dal.listaRegistro_ObtieneClotUnitCOnv(ref Parametros, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public DataTable listaRegistro_ObtieneDescripcionUnidad(ref Ent_ttcibd001 Parametros, ref string strError)
        {
            DataTable retorno;
            try
            {
                retorno = dal.listaRegistro_ObtieneDescripcionUnidad(ref Parametros, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public DataTable listaRegistro_ObtieneDescUnidNOLote(ref Ent_ttcibd001 Parametros, ref string strError)
        {
            DataTable retorno;
            try
            {
                retorno = dal.listaRegistro_ObtieneDescUnidNOLote(ref Parametros, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public DataTable findRecordTransfers(ref string item, ref bool withLot, ref string strError)
        {
            DataTable retorno;
            try
            {
                retorno = dal.findRecordTransfers(ref item, ref withLot, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public DataTable findRecordsSupplies(ref string strError)
        {
            DataTable retorno;
            try
            {
                retorno = dal.findRecordsSupplies(ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }

        }

        public DataTable listaWarehouseLotePorItem(string item)
        {
            return dal.listaLotesPorItem(item);
        }

        public DataTable listaLocalizacionesPorWarehouses(string cwar)
        {
            return dal.listaLocalizacionesPorWarehouses(cwar);
        }

        public DataTable ListaItems()
        {
            return dal.ListaItems();
        }

        public DataTable ListaWarehouses()
        {
            return dal.ListaWarehouses();
        }

        public DataTable listaLotesPorItem(string Item)
        {
            return dal.listaLotesPorItem(Item);
        }

        public int CantidadDevueltaStock(string ITEM, string CLOT, string CWAR, string LOCA)
        {
            return dal.CantidadDevueltaStock(ITEM, CLOT, CWAR, LOCA);
        }

        public DataTable findItem(string ITEM)
        {
            return dal.findItem(ITEM);
        }
    }
}