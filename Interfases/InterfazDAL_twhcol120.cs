using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using whusa.DAL;
using whusa.Entidades;
using System.Data;

namespace whusa.Interfases
{
    public class InterfazDAL_twhcol120
    {
        twhcol120 dal = new twhcol120();

        public InterfazDAL_twhcol120()
        {
            //Constructor
        }

        public int insertarRegistro(ref Ent_twhcol120 parametrosIn, ref string strError)
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

        public bool updateEndPicking(ref string uniqueId, ref string strError) 
        {
            bool retorno = false;
            try
            {
                retorno = dal.updateEndPicking(ref uniqueId, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }

        public bool updateEndReceive(ref string uniqueId, ref string strError)
        {
            bool retorno = false;
            try
            {
                retorno = dal.updateEndReceive(ref uniqueId, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }

        public bool updateFieldPrint(ref string uniqueId, ref string strError)
        {
            bool retorno = false;
            try
            {
                retorno = dal.updateFieldPrint(ref uniqueId, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }

        public DataTable consultaUINotPrinted(ref string strError)
        {
            DataTable retorno = new DataTable();
            try
            {
                retorno = dal.consultaUINotPrinted(ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }

        public DataTable validateEndPicking(ref string uniqueId, ref string strError)
        {
            DataTable retorno = new DataTable();
            try
            {
                retorno = dal.validateEndPicking(ref uniqueId, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }

        public DataTable listFalseEndPickingRecords(ref string strError)
        {
            DataTable retorno = new DataTable();
            try
            {
                retorno = dal.listFalseEndPickingRecords(ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }

        public DataTable listFalseEndReceiveRecords(ref string strError)
        {
            DataTable retorno = new DataTable();
            try
            {
                retorno = dal.listFalseEndReceiveRecords(ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }

        public DataTable validarRegistroByUniqueId(ref string uniqueId, ref string strError)
        {
            DataTable retorno = new DataTable();
            try
            {
                retorno = dal.validarRegistroByUniqueId(ref uniqueId, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }
    }
}
