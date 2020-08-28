using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using whusa;
using DAL;
using System.Data;

namespace Interfases
{
    public class InterfazDAL
    {

        public InterfazDAL()
        {
        }



        public int actualizarInfoCliente(whusa.EntidadRecuperaClientes parametrosIn, ref string strError, string Aplicacion)
        {
            int i2 = 0;

            int i1 = -1;
            try
            {
                //masMobile.mipyme.DAL.clientesApp clientesApp = new masMobile.mipyme.DAL.clientesApp();
                //i1 = clientesApp.actualizarInfoCliente(ref strError, ref parametrosIn, Aplicacion);
                i2 = i1;
            }
            catch (System.Exception e)
            {
                string s = strError + "Pila: " + e.InnerException.ToString();
                strError = strError + "Pila: " + e.InnerException.ToString();
                throw new System.Exception(s);
            }
            return i2;
        }

        public int actualizarInfoCliente(string idMovil, int ultDig, string nit, string razonSocial, int idCiudad, int rangoVentas, string valorVentas, int regimenComun, string impuestos, int idTipoPersoneria, ref string strError, string Aplicacion)
        {
            // trial
            return 0;
        }

        //public System.Collections.Generic.List<Entidades.EntidadRecuperaClientes> recuperarTodosRegs(ref string strError, string Aplicacion)
        //{
        //    System.Collections.Generic.List<masMobile.mipyme.ENTIDADES.EntidadListaImpuestos> list1;

        //    System.Collections.Generic.List<masMobile.mipyme.ENTIDADES.EntidadListaImpuestos> list = new System.Collections.Generic.List<masMobile.mipyme.ENTIDADES.EntidadListaImpuestos>();
        //    try
        //    {
        //        masMobile.mipyme.DAL.listaImpuestos listaImpuestos = new masMobile.mipyme.DAL.listaImpuestos();
        //        list = listaImpuestos.consultarListaImpuestos(ref strError, Aplicacion);
        //        list1 = list;
        //    }
        //    catch (System.Exception e)
        //    {
        //        string s = strError + "Pila: " + e.InnerException.ToString();
        //        strError = strError + "Pila: " + e.InnerException.ToString();
        //        throw new System.Exception(s);
        //    }
        //    return list1;
        //}



        public int enviaDatos(whusa.EntidadRecuperaClientes parametrosIn, ref string strError, string Aplicacion)
        {
            int retorno = -1;
            try
            {
                DAL.clientesApp dal = new DAL.clientesApp();

                retorno = dal.enviarDatos(ref parametrosIn);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "Pila: " + ex.InnerException.ToString());
            }
        }

        public DataTable listaClientes()
        {
            //int retorno = -1;
            DataTable retorno;
            try
            {
                DAL.clientesApp dal = new DAL.clientesApp();

                retorno = dal.listaClientes();
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        
        }
        public int enviaDatos(string nit, string NombreCliente, string ApellidoCliente, string Nit )
        {
            return 0;
        }

        public void guardaErrores(ref string strError, string Aplicacion)
        {

        }


    } // class InterfazDAL
}
