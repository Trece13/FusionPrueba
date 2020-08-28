using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using whusa.Entidades;
using whusa.DAL;
using System.Data;

namespace whusa.Interfases
{
    public class IntefazDAL_tticol082
    {
        tticol082 dal = new tticol082();

        public DataTable ConsultarTticol082()
        {
            return dal.ConsultarTticol082();
        }

        public bool ActualizarPrioridadTticol082(Ent_tticol082 myObj)
        {
            return dal.ActualizarPrioridadTticol082(myObj);
        }

        public bool InsertarregistroItticol093(Ent_tticol082 myObj)
        {
            return dal.InsertarregistroItticol093(myObj);
        }

        public bool InsertarregistroItticol082(Ent_tticol082 myObj)
        {
            return dal.InsertarregistroItticol082(myObj);
        }

        public int PrioridadMaxima()
        {
            return dal.PrioridadMaxima();
        }

        public DataTable ConsultaPlantTticol082()
        {
            return dal.ConsultaPlantTticol082();
        }
        public DataTable ConsultaWarehouseTticol082( string plant )
        {
            return dal.ConsultaWarehouseTticol082( plant );
        }
        
        public object ConsultaMachineTticol082(string plant, string warehouse)
        {
            return dal.ConsultaMachineTticol082(plant,warehouse);
        }

        public DataTable ConsultarTticol082PorPlant(string plant,string warehouse,string machine)
        {
            return dal.ConsultarTticol082PorPlant(plant,warehouse,machine);
        }

        public DataTable ConsultarPalletIDTticol082(string PalletID)
        {
            return dal.ConsultarPalletIDTticol082(PalletID);
        }

        public DataTable ConsultarPalletIDTticol083(string PalletID)
        {
            return dal.ConsultarPalletIDTticol083(PalletID);
        }

        public bool Actualizartticol022(Ent_tticol082 myObj)
        {
            return dal.Actualizartticol022(myObj);
        }

        public bool Actualizartticol042(Ent_tticol082 myObj)
        {
            return dal.Actualizartticol042(myObj);
        }

        public bool Actualizartticol082(Ent_tticol082 myObj)
        {
            return dal.Actualizartticol082(myObj);
        }

        public bool Actualizartticol222(Ent_tticol082 myObj)
        {
            return dal.Actualizartticol222(myObj);
        }

        public bool Actualizartticol242(Ent_tticol082 myObj)
        {
            return dal.Actualizartticol242(myObj);
        }

        public bool Actualizartwhcol131(Ent_tticol082 myObj)
        {
            return dal.Actualizartwhcol131(myObj);
        }

        public DataTable ConsultarPalletIDTticol082MFG(string PalletID)
        {
            return dal.ConsultarPalletIDTticol082MFG(PalletID);
        }

        public bool Actualizartticol022MFG(Ent_tticol082 myObj)
        {
            return dal.Actualizartticol022MFG(myObj);
        }

        public bool Actualizartticol042MFG(Ent_tticol082 myObj)
        {
            return dal.Actualizartticol042MFG(myObj);
        }

        public bool Actualizartticol082MFG(Ent_tticol082 myObj)
        {
            return dal.Actualizartticol082MFG(myObj);
        }

        public bool Actualizartticol222MFG(Ent_tticol082 myObj)
        {
            return dal.Actualizartticol222MFG(myObj);
        }

        public bool Actualizartwhcol131MFG(Ent_tticol082 myObj)
        {
            return dal.Actualizartwhcol131MFG(myObj);
        }

        public bool Actualizartticol083MFG(Ent_tticol082 myObj)
        {
            return dal.Actualizartticol083MFG(myObj);
        }

        public bool Actualizartwhcol130MFG(Ent_tticol082 myObj)
        {
            return dal.Actualizartwhcol130MFG(myObj);
        }

        public bool Actualizartwhcol130(Ent_tticol082 MyObj)
        {
            return dal.Actualizartwhcol130(MyObj);
        }

        public bool Actualizartticol242MFG(Ent_tticol082 MyObj)
        {
            return dal.Actualizartticol222MFG(MyObj);
        }

        public DataTable ConsultarRegistrosBloquedos()
        {
            return dal.ConsultarRegistrosBloquedos();
        }

        public bool Actualizartwhcol131STAT(Ent_tticol082 myObj)
        {
            return dal.Actualizartwhcol131STAT(myObj);
        }

        public bool Actualizartwhcol130STAT(Ent_tticol082 myObj)
        {
            return dal.Actualizartwhcol130STAT(myObj);
        }
        public bool Actualizartticol022STAT(Ent_tticol082 myObj)
        {
            return dal.Actualizartticol022STAT(myObj);
        }

        public bool Actualizartticol042STAT(Ent_tticol082 myObj)
        {
            return dal.Actualizartticol042STAT(myObj);
        }

        public bool Actualizartwhcol131Cant(Ent_tticol082 myObj)
        {
            return dal.Actualizartwhcol131Cant(myObj);
        }

        public bool Actualizartwhcol130Cant(Ent_tticol082 myObj)
        {
            return dal.Actualizartwhcol130Cant(myObj);
        }
        public bool Actualizartticol222Cant(Ent_tticol082 myObj)
        {
            return dal.Actualizartticol222Cant(myObj);
        }

        public bool Actualizartticol242Cant(Ent_tticol082 myObj)
        {
            return dal.Actualizartticol242Cant(myObj);
        }

        public DataTable ConsultarOtrosRegistros()
        {
            return dal.ConsultarOtrosRegistros();
        }

        public DataTable ConsultarTticol082PorPlantPono(string plant,int prio,string advs)
        {
            return dal.ConsultarTticol082PorPlantPono(plant,prio,advs);
        }
    }
}
