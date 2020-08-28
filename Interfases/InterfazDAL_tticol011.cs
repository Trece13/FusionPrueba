using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using whusa.Entidades;
using whusa.DAL;


namespace whusa.Interfases
{
    public class InterfazDAL_tticol011
    {
        tticol011 dal = new tticol011();

        public InterfazDAL_tticol011()
        {
        }

        public int countByMachineStatusAndDiferentPdno(ref string pdno, ref string stat, ref string mcno, ref string strError)
        {
            int retorno;
            try
            {
                retorno = dal.countByMachineStatusAndDiferentPdno(ref pdno, ref stat, ref mcno, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public bool updateStatusInitiated(ref Ent_tticol011 data, ref string strError)
        {
            bool retorno = false;
            try
            {
                retorno = dal.updateStatusInitiated(ref data, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }

        public bool updateStatusOnHold(ref Ent_tticol011 data, ref string strError)
        {
            bool retorno = false;
            try
            {
                retorno = dal.updateStatusOnHold(ref data, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }

        public bool updateStatusFinish(ref Ent_tticol011 data, ref string strError)
        {
            bool retorno = false;
            try
            {
                retorno = dal.updateStatusFinish(ref data, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }

        public DataTable findRecordByPdno(ref string pdno, ref string strError)
        {
            DataTable retorno;
            try
            {
                retorno = dal.findRecordByPdno(ref pdno, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public DataTable invLabel_listaRegistrosOrdenMaquina_Param(ref Ent_tticol011 Parametros, ref string strError)
        {
            //int retorno = -1;
            DataTable retorno;
            try
            {
                retorno = dal.invLabel_listaRegistrosOrdenMaquina_Param(ref Parametros, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public DataTable invLabel_listaRegistrosOrdenMaquina_Workorder(ref Ent_tticol011 Parametros, ref string strError)
        {
            //int retorno = -1;
            DataTable retorno;
            try
            {
                retorno = dal.invLabel_listaRegistrosOrdenMaquina_Workorder(ref Parametros, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public DataTable invLabel_listaRegistrosOrdenParam(ref Ent_tticol011 Parametros, ref string strError)
        {
            //int retorno = -1;
            DataTable retorno;
            try
            {
                retorno = dal.invLabel_listaRegistrosOrdenParam(ref Parametros, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public DataTable invLabelRegrind_listaRegistrosOrdenMaquina_Param(ref Ent_tticol011 Parametros, ref string strError)
        {
            //int retorno = -1;
            DataTable retorno;
            try
            {
                retorno = dal.invLabelRegrind_listaRegistrosOrdenMaquina_Param(ref Parametros, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public DataTable invLabelRegrind_listaRegistrosOrdenParam(ref Ent_tticol011 Parametros, ref string strError)
        {
            //int retorno = -1;
            DataTable retorno;
            try
            {
                retorno = dal.invLabelRegrind_listaRegistrosOrdenParam(ref Parametros, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public DataTable listaRegistrosMaquina_Param(ref Ent_tticol011 Parametros, ref string strError)
        {
            //int retorno = -1;
            DataTable retorno;
            try
            {
                retorno = dal.listaRegistrosMaquina_Param(ref Parametros, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public DataTable findByPdnoAndInStatus(ref string pdno, ref string status, ref string strError)
        {
            //int retorno = -1;
            DataTable retorno;
            try
            {
                retorno = dal.findByPdnoAndInStatus(ref pdno, ref status, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }
    }
}
