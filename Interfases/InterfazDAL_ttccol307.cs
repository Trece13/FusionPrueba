using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using whusa.Entidades;
using whusa.DAL;
using System.Data;

namespace whusa.Interfases
{
    public class IntefazDAL_ttccol307
    {
        ttccol307 dal = new ttccol307();

        public DataTable ConsultarRegistrotccol307(Ent_ttccol307 ObjTtccol307)
        {
            return dal.ConsultarRegistrotccol307(ObjTtccol307);
        }

        public bool ActualizarUsuariotccol307(Ent_ttccol307 ObjTtccol307)
        {
            return dal.ActualizarUsuariotccol307(ObjTtccol307);
        }


    }
}
