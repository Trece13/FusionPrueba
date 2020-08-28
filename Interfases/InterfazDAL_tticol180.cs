using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using whusa.DAL;
using whusa.Entidades;
using System.Data;

namespace whusa.Interfases
{
    public class InterfazDAL_tticol180
    {
        tticol180 dal = new tticol180();

        public InterfazDAL_tticol180()
        {
            //Constructor
        }

        public int insertRecord(ref Ent_tticol180 parametrosIn, ref string strError)
        {
            int retorno = -1;
            try
            {
                retorno = dal.insertRecord(ref parametrosIn, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }

        public bool updateRecord(ref Ent_tticol180 data, ref string strError)
        {
            bool retorno = false;
            try
            {
                retorno = dal.updateRecord(ref data, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }

        public List<Ent_tticol180> listRecordSendEmail(ref string strError)
        {
            List<Ent_tticol180> retorno = new List<Ent_tticol180>();
            DataTable dt = new DataTable();
            try
            {
                dt = dal.listRecordSendEmail(ref strError);

                foreach (DataRow item in dt.Rows)
                {
                    Ent_tticol180 registro = new Ent_tticol180()
                    {
                        docn = item["DOCN"].ToString(),
                        user = item["CUSER"].ToString(),
                        path = item["CPATH"].ToString(),
                        tgbrg835_name = item["CNAME"].ToString(),
                        tgbrg835_mail = item["CMAIL"].ToString()
                    };

                    retorno.Add(registro);
                }

                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }
    }
}
