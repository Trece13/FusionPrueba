using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using whusa.DAL;
using System.Data;
using whusa.Entidades;

namespace whusa.Interfases
{
    public class IntefazDAL_ttccom110
    {
        ttccom110 dal = new ttccom110();

        public DataTable VerificarExistenciaCustomer(Ent_ttccom110 ObjTtccom110)
        {
            return dal.VerificarExistenciaCustomer(ObjTtccom110);
        }
    }


}
