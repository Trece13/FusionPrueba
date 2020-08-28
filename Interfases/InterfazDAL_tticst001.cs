using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using whusa.DAL;
using System.Data;

namespace whusa.Interfases
{
    public class InterfazDAL_tticst001
    {
        tticst001 dal = new tticst001();
        
        public InterfazDAL_tticst001()
        {
 
        }

        public DataTable findByItemAndPdno(ref string pdno, ref string item, ref string strError)
        {
            //int retorno = -1;
            DataTable retorno = new DataTable();
            try
            {
                retorno = dal.findByItemAndPdno(ref pdno, ref item, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        //public DataTable findByPdno(ref string pdno,  ref string strError)
        //{
        //    //int retorno = -1;
        //    DataTable retorno = new DataTable();
        //    try
        //    {
        //        retorno = dal.findByPdno(ref pdno, ref strError);
        //        return retorno;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.InnerException.ToString());
        //    }
        //}

        public DataTable findByPdno(ref string pdno, ref string strError)
        {
            //int retorno = -1;
            DataTable retorno = new DataTable();
            try
            {
                retorno = dal.findByPdnoMRB(ref pdno, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public DataTable findByPdnoMRB(ref string pdno, ref string strError)
        {
            //int retorno = -1;
            DataTable retorno = new DataTable();
            try
            {
                retorno = dal.findByPdnoMRB(ref pdno, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public DataTable findByPdnoCosts(ref string pdno, ref string  shift, ref string strError)
        {
            //int retorno = -1;
            DataTable retorno = new DataTable();
            try
            {
                retorno = dal.findByPdnoCosts(ref pdno,ref shift, ref strError);
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
            DataTable retorno = new DataTable();
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

        public DataTable maxquantity_per_shift140(string SHIFT, string MCNO, string SITM, string PDNO)
        {
            DataTable retorno = new DataTable();
            try
            {
                retorno = dal.maxquantity_per_shift140(SHIFT,MCNO,SITM,PDNO);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public DataTable quantity_reg_order_machine140(string SHIFT, string MCNO, string SITM, string PDNO)
        {
            DataTable retorno = new DataTable();
            try
            {
                retorno = dal.quantity_reg_order_machine140(SHIFT,MCNO,SITM, PDNO);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }
    }
}
