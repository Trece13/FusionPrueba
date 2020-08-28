using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using whusa.DAL;
using whusa.Entidades;
using System.Data;

namespace whusa.Interfases
{
    public class InterfazDAL_twhcol122
    {
        twhcol122 dal = new twhcol122();

        public InterfazDAL_twhcol122()
        {
            //Constructor
        }

        public bool insertarRegistro(ref Ent_twhcol122 parametrosIn, ref string strError)
        {
            //int retorno = -1;
            //try
            //{
                return dal.insertarRegistro(ref parametrosIn, ref strError);
                
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception(strError += "\nPila: " + ex.Message);
            //}
        }

        public DataTable validarRegistroByPalledId(ref string palletId, ref string uniqueId, ref string bodegaori, ref string bodegades, string strError) 
        {
            DataTable retorno = new DataTable();
            try
            {
                retorno = dal.validarRegistroByPalletId(ref palletId, ref uniqueId, ref bodegaori, ref bodegades, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
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

        public DataTable buscarbodegasuid(ref string uniqueId, ref string strError)
        {
            DataTable retorno = new DataTable();
            try
            {
                retorno = dal.buscarbodegasuid(ref uniqueId, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nPila: " + ex.Message);
            }
        }

        //inicio

        public List<EntidadPicking> ConsultarPalletPicking22(string PAID, string UrlBaseBarcode, string USER)
        {
            List<EntidadPicking> Lstwhcol122 = new List<EntidadPicking>();
            DataTable DTwhcol122 = dal.ConsultarPalletPicking22(PAID, USER);
            if (DTwhcol122.Rows.Count > 0)
            {
                foreach (DataRow MyRow in DTwhcol122.Rows)
                {

                    EntidadPicking MyObjet = new EntidadPicking
                    {

                        PALLETID = MyRow["PALLETID"].ToString(),
                        ITEM = MyRow["ITEM"].ToString(),
                        DESCRIPTION = MyRow["DESCRIPTION"].ToString(),
                        LOT = MyRow["LOT"].ToString(),
                        WRH = MyRow["WRH"].ToString(),
                        DESCWRH = MyRow["DESCWRH"].ToString(),
                        QTY = MyRow["QTY"].ToString(),
                        UN = MyRow["UN"].ToString(),
                        PRIO = MyRow["PRIO"].ToString(),
                        LOCA = MyRow["LOCA"].ToString(),
                        ROWN = MyRow["ROWN"].ToString(),
                        OORG = MyRow["OORG"].ToString(),
                        ORNO = MyRow["ORNO"].ToString(),
                        OSET = MyRow["OSET"].ToString(),
                        PONO = MyRow["PONO"].ToString(),
                        SQNB = MyRow["SQNB"].ToString(),
                        ADVS = MyRow["ADVS"].ToString(),
                        QTYT = MyRow["QTYT"].ToString(),

                    };

                    Lstwhcol122.Add(MyObjet);
                }

            }
            return Lstwhcol122;
        }

        public List<EntidadPicking> ConsultarPalletPicking042(string PAID, string UrlBaseBarcode, string USER)
        {
            List<EntidadPicking> Lstwhcol042 = new List<EntidadPicking>();
            DataTable DTwhcolo42 = dal.ConsultarPalletPicking042(PAID, USER);
            if (DTwhcolo42.Rows.Count > 0)
            {
                foreach (DataRow MyRow in DTwhcolo42.Rows)
                {

                    EntidadPicking MyObjet = new EntidadPicking
                    {


                        PALLETID = MyRow["PALLETID"].ToString(),
                        ITEM = MyRow["ITEM"].ToString(),
                        DESCRIPTION = MyRow["DESCRIPTION"].ToString(),
                        LOT = MyRow["LOT"].ToString(),
                        WRH = MyRow["WRH"].ToString(),
                        DESCWRH = MyRow["DESCWRH"].ToString(),
                        QTY = MyRow["QTY"].ToString(),
                        UN = MyRow["UN"].ToString(),
                        PRIO = MyRow["PRIO"].ToString(),
                        LOCA = MyRow["LOCA"].ToString(),
                        ROWN = MyRow["ROWN"].ToString(),
                        OORG = MyRow["OORG"].ToString(),
                        ORNO = MyRow["ORNO"].ToString(),
                        OSET = MyRow["OSET"].ToString(),
                        PONO = MyRow["PONO"].ToString(),
                        SQNB = MyRow["SQNB"].ToString(),
                        ADVS = MyRow["ADVS"].ToString(),

                    };

                    Lstwhcol042.Add(MyObjet);
                }

            }
            return Lstwhcol042;
        }

        
        public List<EntidadPicking> ConsultarPalletPicking131(string PAID, string UrlBaseBarcode, string USER)
        {
            List<EntidadPicking> Lstwhcol131 = new List<EntidadPicking>();
            DataTable DTwhcolo131 = dal.ConsultarPalletPicking131(PAID, USER);
            if (DTwhcolo131.Rows.Count > 0)
            {
                foreach (DataRow MyRow in DTwhcolo131.Rows)
                {

                    EntidadPicking MyObjet = new EntidadPicking
                    {


                        PALLETID = MyRow["PALLETID"].ToString(),
                        ITEM = MyRow["ITEM"].ToString(),
                        DESCRIPTION = MyRow["DESCRIPTION"].ToString(),
                        LOT = MyRow["LOT"].ToString(),
                        WRH = MyRow["WRH"].ToString(),
                        DESCWRH = MyRow["DESCWRH"].ToString(),
                        QTY = MyRow["QTY"].ToString(),
                        UN = MyRow["UN"].ToString(),
                        PRIO = MyRow["PRIO"].ToString(),
                        LOCA = MyRow["LOCA"].ToString(),
                        ROWN = MyRow["ROWN"].ToString(),
                        OORG = MyRow["OORG"].ToString(),
                        ORNO = MyRow["ORNO"].ToString(),
                        OSET = MyRow["OSET"].ToString(),
                        PONO = MyRow["PONO"].ToString(),
                        SQNB = MyRow["SQNB"].ToString(),
                        ADVS = MyRow["ADVS"].ToString(),

                    };

                    Lstwhcol131.Add(MyObjet);
                }

            }
            return Lstwhcol131;
        }
        //registros textbox ok

        public int IngRegistrott307140(string USER, int SQNB, string PDNO, int REFCNTD, int REFCNTU)
        {
           
            string strError = string.Empty;
            try
            {
                int DTwhcolo131 = dal.IngRegistrott307140(USER, SQNB, PDNO, REFCNTD, REFCNTU);
                return DTwhcolo131;
               
            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nError: " + ex.Message);
            }
        }

        public DataTable ConsultarTt307140(Ent_ttccol307 Objttccol307 )
        {

                return dal.ConsultarTt307140(Objttccol307);
        }
        //actRegtticol022140
        public int actRegtticol022140(string PDNO)
        {

            string strError = string.Empty;
            try
            {
                int DTwhcolo131 = dal.actRegtticol022140(PDNO);
                return DTwhcolo131;

            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nError: " + ex.Message);
            }
        }

        
        public int actRegtticol042140(string PDNO)
        {

            string strError = string.Empty;
            try
            {
                int DTwhcolo131 = dal.actRegtticol042140(PDNO);
                return DTwhcolo131;

            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nError: " + ex.Message);
            }
        }


        public int actRegtticol131140(string PDNO)
        {

            string strError = string.Empty;
            try
            {
                int DTwhcolo131 = dal.actRegtticol131140(PDNO);
                return DTwhcolo131;

            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nError: " + ex.Message);
            }
        }

       

        public int actRegtticol082140(string user ,string pallet, string Location, int stat,string t,string OORG,string ORNO,string OSET,string PONO,string SQNB,string ADVS)
        {

            string strError = string.Empty;
            try
            {
                int DTwhcolo131 = dal.actRegtticol082140(user, pallet, Location, stat, t, OORG, ORNO, OSET, PONO, SQNB, ADVS);
                return DTwhcolo131;

            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nError: " + ex.Message);
            }
        }

      

       
         public int InsertRegCausalCOL084(string pallet,string user, int statCausal)
        {

            string strError = string.Empty;
            try
            {
                int DTwhcolo131 = dal.InsertRegCausalCOL084(pallet, user, statCausal);
                return DTwhcolo131;

            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nError: " + ex.Message);
            }
        }

      

         public int ingRegTticol092140(string maximo, string pallet, string txtpallet, int causal, string _operator)
         {

             string strError = string.Empty;
             try
             {
                 int DTwhcolo131 = dal.ingRegTticol092140(maximo, pallet, txtpallet,  causal,  _operator);
                 return DTwhcolo131;

             }
             catch (Exception ex)
             {
                 throw new Exception(strError += "\nError: " + ex.Message);
             }
         }
      
         public int ActCausalTICOL022(string pallet,int  stat )
        {

            string strError = string.Empty;
            try
            {
                int DTwhcolo131 = dal.ActCausalTICOL022(pallet, stat);
                return DTwhcolo131;

            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nError: " + ex.Message);
            }
        }

         public int ActCausalTICOL042(string pallet, int stat)
         {

             string strError = string.Empty;
             try
             {
                 int DTwhcolo131 = dal.ActCausalTICOL042(pallet, stat);
                 return DTwhcolo131;

             }
             catch (Exception ex)
             {
                 throw new Exception(strError += "\nError: " + ex.Message);
             }
         }

       
        
         public int ActCausalcol131140(string pallet, int stat)
         {

             string strError = string.Empty;
             try
             {
                 int DTwhcolo131 = dal.ActCausalcol131140(pallet, stat);
                 return DTwhcolo131;

             }
             catch (Exception ex)
             {
                 throw new Exception(strError += "\nError: " + ex.Message);
             }
         }


        //public DataTable invLabelRegrind_listaRegistrosOrdenParam(ref Ent_tticol011 Parametros, ref string strError)
        public DataTable invLabelRegrind_listaRegistrosOrdenParam()
        {
            
            DataTable retorno;
            try
            {
                retorno = dal.invLabelRegrind_listaRegistrosOrdenParam();
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }

        public DataTable VerificarLocate(string CWAR, string LOCA)
        {
            return dal.VerificarLocate(CWAR, LOCA);
        }

        public DataTable VerificarPalletID(string PAID)
        {
            return dal.VerificarPalletID(PAID);
        }
        //fin

        //public void IngRegistrott307140(string _operator, int p, string pallet, int p_2, int p_3)
        //{
        //    throw new NotImplementedException();
        //}

        public bool InsertarTccol307140(string USER, string STAT, string PAID, string PROC, string REFCNTD, string REFCNTU)
        {
            return dal.InsertarTccol307140(USER, STAT, PAID, PROC, REFCNTD, REFCNTU);

        }

        public bool EliminarTccol307140(string PAID)
        {
            return dal.EliminarTccol307140(PAID);

        }

        public List<EntidadPicking> ConsultarPalletPicking22PAID(string PAID, string UrlBaseBarcode, string USER)
        {
            List<EntidadPicking> Lstwhcol122 = new List<EntidadPicking>();
            DataTable DTwhcol122 = dal.ConsultarPalletPicking22PAID(PAID, USER);
            if (DTwhcol122.Rows.Count > 0)
            {
                foreach (DataRow MyRow in DTwhcol122.Rows)
                {

                    EntidadPicking MyObjet = new EntidadPicking
                    {

                        PALLETID = MyRow["PALLETID"].ToString(),
                        ITEM = MyRow["ITEM"].ToString(),
                        DESCRIPTION = MyRow["DESCRIPTION"].ToString(),
                        LOT = MyRow["LOT"].ToString(),
                        WRH = MyRow["WRH"].ToString(),
                        DESCWRH = MyRow["DESCWRH"].ToString(),
                        QTY = MyRow["QTY"].ToString(),
                        UN = MyRow["UN"].ToString(),
                        PRIO = MyRow["PRIO"].ToString(),
                        LOCA = MyRow["LOCA"].ToString(),
                        ROWN = MyRow["ROWN"].ToString(),
                        OORG = MyRow["OORG"].ToString(),
                        ORNO = MyRow["ORNO"].ToString(),
                        OSET = MyRow["OSET"].ToString(),
                        PONO = MyRow["PONO"].ToString(),
                        SQNB = MyRow["SQNB"].ToString(),
                        ADVS = MyRow["ADVS"].ToString(),
                        QTYT = MyRow["QTYT"].ToString(),
                    };

                    Lstwhcol122.Add(MyObjet);
                }

            }
            return Lstwhcol122;
        }

        public List<EntidadPicking> ConsultarPalletPicking042PAID(string PAID, string UrlBaseBarcode, string USER)
        {
            List<EntidadPicking> Lstwhcol042 = new List<EntidadPicking>();
            DataTable DTwhcolo42 = dal.ConsultarPalletPicking042PAID(PAID, USER);
            if (DTwhcolo42.Rows.Count > 0)
            {
                foreach (DataRow MyRow in DTwhcolo42.Rows)
                {

                    EntidadPicking MyObjet = new EntidadPicking
                    {


                        PALLETID = MyRow["PALLETID"].ToString(),
                        ITEM = MyRow["ITEM"].ToString(),
                        DESCRIPTION = MyRow["DESCRIPTION"].ToString(),
                        LOT = MyRow["LOT"].ToString(),
                        WRH = MyRow["WRH"].ToString(),
                        DESCWRH = MyRow["DESCWRH"].ToString(),
                        QTY = MyRow["QTY"].ToString(),
                        UN = MyRow["UN"].ToString(),
                        PRIO = MyRow["PRIO"].ToString(),
                        LOCA = MyRow["LOCA"].ToString(),
                        ROWN = MyRow["ROWN"].ToString(),
                        OORG = MyRow["OORG"].ToString(),
                        ORNO = MyRow["ORNO"].ToString(),
                        OSET = MyRow["OSET"].ToString(),
                        PONO = MyRow["PONO"].ToString(),
                        SQNB = MyRow["SQNB"].ToString(),
                        ADVS = MyRow["ADVS"].ToString(),
                        QTYT = MyRow["QTYT"].ToString(),


                    };

                    Lstwhcol042.Add(MyObjet);
                }

            }
            return Lstwhcol042;
        }


        public List<EntidadPicking> ConsultarPalletPicking131PAID(string PAID, string UrlBaseBarcode, string USER)
        {
            List<EntidadPicking> Lstwhcol131 = new List<EntidadPicking>();
            DataTable DTwhcolo131 = dal.ConsultarPalletPicking131PAID(PAID, USER);
            if (DTwhcolo131.Rows.Count > 0)
            {
                foreach (DataRow MyRow in DTwhcolo131.Rows)
                {

                    EntidadPicking MyObjet = new EntidadPicking
                    {


                        PALLETID = MyRow["PALLETID"].ToString(),
                        ITEM = MyRow["ITEM"].ToString(),
                        DESCRIPTION = MyRow["DESCRIPTION"].ToString(),
                        LOT = MyRow["LOT"].ToString(),
                        WRH = MyRow["WRH"].ToString(),
                        DESCWRH = MyRow["DESCWRH"].ToString(),
                        QTY = MyRow["QTY"].ToString(),
                        UN = MyRow["UN"].ToString(),
                        PRIO = MyRow["PRIO"].ToString(),
                        LOCA = MyRow["LOCA"].ToString(),
                        ROWN = MyRow["ROWN"].ToString(),
                        OORG = MyRow["OORG"].ToString(),
                        ORNO = MyRow["ORNO"].ToString(),
                        OSET = MyRow["OSET"].ToString(),
                        PONO = MyRow["PONO"].ToString(),
                        SQNB = MyRow["SQNB"].ToString(),
                        ADVS = MyRow["ADVS"].ToString(),
                        QTYT = MyRow["QTYT"].ToString(),


                    };

                    Lstwhcol131.Add(MyObjet);
                }

            }
            return Lstwhcol131;
        }

        public bool Actualizar307(string PAID_NEW, string PAID_OLD)
        {
             return dal.Actualizar307(PAID_NEW, PAID_OLD);
        }

        public int ActCausalcol130140(string pallet, int stat)
        {
            string strError = string.Empty;
            try
            {
                int DTwhcolo131 = dal.ActCausalcol130140(pallet, stat);
                return DTwhcolo131;

            }
            catch (Exception ex)
            {
                throw new Exception(strError += "\nError: " + ex.Message);
            }
        }
    }
}