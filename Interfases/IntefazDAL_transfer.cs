using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using whusa.Entidades;

namespace whusa.Interfases
{
    public class IntefazDAL_transfer
    {
        transfer dal = new transfer();

        public IntefazDAL_transfer()
        {
            //Constructor
        }

        public DataTable ConsultarRegistroTransferir(string PAID)
        {
            DataTable retorno = new DataTable();

            try
            {
                retorno = dal.ConsultarRegistroTransferir(PAID);
            }
            catch (Exception ex)
            {
                //throw new Exception(strError += "\nPila: " + ex.Message);
            }

            return retorno;
        }

        public bool InsertarTransferencia(Ent_twhcol020 objWhcol020)
        {

               return dal.InsertarTransferencia(objWhcol020);

        }

        public bool ActualizarTransferencia(Ent_twhcol020 objWhcol020)
        {
            return dal.ActualizarTransferencia(objWhcol020);
        }

        public DataTable ListWarehouses()
        {
            return dal.ListWarehouses();
        }

        public DataTable ConsultarLocation(string CWAR, string LOCA)
        {
            return dal.ConsultarLocation(CWAR, LOCA);
        }

        public string DescripcionItem(string ITEM)
        {
            return dal.DescripcionItem(ITEM);
        }

        public DataTable ConsultaTransferencia(string PAID)
        {
            return dal.ConsultaTransferencia(PAID);
        }

        public bool ActualizarTransferencia(string PAID, string CurrentWarehouse, string CurrentLocation, string TargetWarehouse, string TargetLocation,string USER)
        {
            return dal.ActualizarTransferencia(PAID, CurrentWarehouse, CurrentLocation, TargetWarehouse, TargetLocation, USER);
        }

        public DataTable ConsultarWarehouse(string LOCA)
        {
            return dal.ConsultarWarehouse(LOCA);
        }

    }
}
