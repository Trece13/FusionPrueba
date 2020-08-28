using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using whusa.Entidades;
using whusa.Interfases;
using System.Data;

namespace ServicioWCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    public class ServicioPrueba : InterfazServicioWhusa
    {
        private string Aplicacion;
        private string strError;

        public ServicioPrueba()
        {
            strError = System.String.Empty;
            Aplicacion = "WS";
        }

        //public int actualizarInfoCliente(Ent_tticol125 parametros)
        //{
        //    int i2;

        //    int i1 = 0;
        //    InterfazDAL_tticol125 interfazDAL = new InterfazDAL_tticol125();
        //    try
        //    {
        //        i1 = interfazDAL.actualizarInfoCliente(parametros, ref strError, Aplicacion);
        //        strError = "Actualizar Informacion del cliente";
        //        interfazDAL.guardaErrores(ref strError, Aplicacion);
        //        strError = System.String.Empty;
        //        i2 = i1;
        //    }
        //    catch (System.TimeoutException e1)
        //    {
        //        throw new System.Exception(e1.StackTrace);
        //    }
        //    catch (System.Exception e2)
        //    {
        //        strError = strError + "| - |" + e2.Message.ToString();
        //        interfazDAL.guardaErrores(ref strError, Aplicacion);
        //        throw new System.Exception(strError);
        //    }
        //    return i2;
        //}

        public System.Collections.Generic.List<Ent_tticol125> enviaDatos(List<Ent_tticol125> parametros)
        {
            List<Ent_tticol125> list;

            int i = 1;
            List<Ent_tticol125> list1 = new System.Collections.Generic.List<Ent_tticol125>();
            InterfazDAL_tticol125 interfazDAL = new InterfazDAL_tticol125();
            try
            {
                strError = "CREAR tticol125";
                i = interfazDAL.insertarRegistro(ref parametros, ref strError, Aplicacion);
                strError = "Recuperar CalenDario";
                interfazDAL.guardaErrores(ref strError, Aplicacion);
                strError = System.String.Empty;
                if ((i > 0) && System.String.IsNullOrEmpty(strError))
                {
                    strError = "Recuperar tticol125 Sentencia";
                    interfazDAL.guardaErrores(ref strError, Aplicacion);
                    strError = System.String.Empty;
                   // list1 = recuperarClientes(ref parametros);
                }
                else if (strError != System.String.Empty)
                {
                    interfazDAL.guardaErrores(ref strError, Aplicacion);
                    throw new System.Exception(strError);
                }
                list = list1;
            }
            catch (System.TimeoutException e1)
            {
                throw new System.Exception(e1.StackTrace);
            }
            catch (System.Exception e2)
            {
                strError += e2.Message.ToString();
                interfazDAL.guardaErrores(ref strError, Aplicacion);
                throw new System.Exception(strError);
            }
            return list;
        }

        public System.Collections.Generic.List<Ent_tticol125> CambiaEstado(Ent_tticol125 parametros)
        {
            List<Ent_tticol125> lista = new List<Ent_tticol125>();
            return lista;
        }

        public List<Ent_tticol125> recuperarCliente(Ent_tticol125 parametros)
        {
            List<Ent_tticol125> lista = new List<Ent_tticol125>();
            return lista;
        }

        public DataTable listaRegistrosOrden_Param(ref Ent_tticol125 parametros)
        {
            //System.Collections.Generic.List<EntidadRecuperaClientes> lista = new System.Collections.Generic.List<EntidadRecuperaClientes>();
            DataTable lista ; //= new System.Collections.Generic.List<EntidadRecuperaClientes>();
            InterfazDAL_tticol125 interfazDAL = new InterfazDAL_tticol125();
            string strError = string.Empty;
            try
            {
                lista = interfazDAL.listaRegistrosOrden_Param(ref parametros, ref strError);
        //        int i = list.get_Count();

            }
            catch (System.TimeoutException e1)
            {
                throw new System.Exception(e1.StackTrace);
            }
            catch (System.ServiceModel.FaultException e2)
            {
                throw new System.Exception(e2.Reason.ToString());
            }
            catch (System.Exception e3)
            {
                throw new System.Exception(strError);
            }
            return lista;
        }

        //public System.Collections.Generic.List<masMobile.mipyme.ENTIDADES.EntidadListaRangosVentas> recuperaRangosVentasOpcion()
        //{
        //    // trial
        //    return null;
        //}

        //public System.Collections.Generic.List<masMobile.mipyme.ENTIDADES.EntidadCalendarioImpuestos> recuperarCalendario(ref masMobile.mipyme.ENTIDADES.EntidadParamRecuperaImpuestos parametros)
        //{
        //    // trial
        //    return null;
        //}
    }
}
