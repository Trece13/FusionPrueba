using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using whusa.Entidades;
using whusa.DAL;

namespace whusa.Interfases
{
    public class InterfazDAL_tticol030
    {
        tticol030 dal = new tticol030();

        static InterfazDAL_tticol030()
        {
        }

        public int insertarRegistro(ref List<Ent_tticol030> parametros, ref string strError)
        {
            int retorno = -1;
            try
            {
                retorno = dal.insertarRegistro(ref parametros, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }

        public int consultarConsecutivoRegistro(ref string pdno, ref string strError) 
        {
            int retorno = -1;
            try
            {
                retorno = dal.consultarConsecutivoRegistro(ref pdno, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }
    }
}
