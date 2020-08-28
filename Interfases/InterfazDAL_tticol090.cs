using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using whusa.Entidades;
using whusa.DAL;


namespace whusa.Interfases
{
    public class InterfazDAL_tticol090
    {
        tticol090 dal = new tticol090();

        public InterfazDAL_tticol090()
        {
        }

        public int insertarRegistro(ref List<Ent_tticol090> parametrosIn, ref string strError)
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

        public int actualizarRegistro_Param(ref List<Ent_tticol090> parametrosIn, ref string strError, string Aplicacion)
        {
            int retorno = -1;
            try
            {
                retorno = dal.actualizarRegistro_Param(ref parametrosIn, ref strError, Aplicacion);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }

        public DataTable lineClearance_verificaOrdenes_Param(ref Ent_tticol090 Parametros, ref string strError)
        {
            //int retorno = -1;
            DataTable retorno;
            try
            {
                retorno = dal.lineClearance_verificaOrdenes_Param(ref Parametros, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public DataTable lineClearance_listaRegistrosOrden_Param(ref Ent_tticol090 Parametros, ref string strError)
        {
            DataTable retorno;
            try
            {
                retorno = dal.lineClearance_listaRegistrosOrden_Param(ref Parametros, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public DataTable lineClearance_verificaRegistrosOrden_Param(ref Ent_tticol090 Parametros, ref string strError)
        {
            DataTable retorno;
            try
            {
                retorno = dal.lineClearance_verificaRegistrosOrden_Param(ref Parametros, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public DataTable lineClearance_verificaRegistrosOrden_Param(ref Ent_tticol090 Parametros, ref Ent_ttwhcol016 Parametros016, ref string strError)
        {
            DataTable retorno;
            try
            {
                retorno = dal.lineClearance_verificaRegistrosOrden_Param(ref Parametros, ref Parametros016, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public DataTable lineClearance_verificaLote_Param(ref Ent_tticol090 Parametros, ref string strError)
        {
            DataTable retorno;
            try
            {
                retorno = dal.lineClearance_verificaLote_Param(ref Parametros, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public DataTable ConsultarCantidadPoritem022042131(MyLioEntidad objEnt, ref string strError)
        {
            DataTable retorno;
            try
            {
                retorno = dal.ConsultarCantidadPoritem022042131( objEnt, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public DataTable ConsultarCantidad215(MyLioEntidad objEnt, ref string strError)
        {
            DataTable retorno;
            try
            {
                retorno = dal.ConsultarCantidad215( objEnt, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public DataTable InsertTticol088( Ent_tticol088 obj088, ref string strError)
        {
            DataTable retorno;
            try
            {
                retorno = dal.InsertTticol088( obj088, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }
    }
}
