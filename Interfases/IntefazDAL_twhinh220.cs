using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using whusa.DAL;
using System.Data;
using whusa.Entidades;

namespace whusa.Interfases
{
    public class IntefazDAL_twhinh220
    {
        twhinh220 dal = new twhinh220();

        public DataTable TraerOrdenesCustomer(Ent_twhinh220 Objtwhinh220,ref string strError)
        {
            return dal.TraerOrdenesCustomer(Objtwhinh220,ref strError);
        }
    }
}
