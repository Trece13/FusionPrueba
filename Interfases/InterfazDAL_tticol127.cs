using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using whusa.Entidades;
using whusa.DAL;
using System.Data;

namespace whusa.Interfases
{
    public class InterfazDAL_tticol127
    {
        tticol127 dal = new tticol127();

        public InterfazDAL_tticol127()
        {
        }

        public int enviaDatos(string nit, string NombreCliente, string ApellidoCliente, string Nit)
        {
            return 0;
        }

        public void guardaErrores(ref string strError, string Aplicacion)
        {

        }

        public DataTable listaRegistro_ObtieneAlmacen(ref Ent_tticol127 Parametros, ref string strError)
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

        public DataTable listaRegistrosOrden_Param(ref Ent_tticol127 parametros, ref string strError, ref string strOrden, bool print = false)
        {
            //int retorno = -1;
            DataTable retorno;
            try
            {
                retorno = dal.listaRegistrosOrden_Param(ref parametros, ref strError, ref strOrden, print);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }

        }
        public DataTable listaRegistrosOrden_ParamMRB(ref Ent_tticol127 parametros, ref string strError, ref string strOrden, bool print = false)
        {
            //int retorno = -1;
            DataTable retorno;
            try
            {
                retorno = dal.listaRegistrosOrden_ParamMRB(ref parametros, ref strError, ref strOrden, print);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }

        }

        public DataTable Lista_Items()
        {
            try
            {
                return dal.Lista_Items();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }

        public  DataTable Lista_Lotes( string clot)
        {
            try
            {
                return dal.Lista_Lote(clot);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }
    }
}
