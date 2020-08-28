using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using whusap.Entidades;

namespace whusa.Interfases
{
    public class InterfazDAL_twhcol130
    {
        twhcol130 dal = new twhcol130();

        public DataTable ValidarOrderID(Ent_twhcol130 whcol130)
        {
            DataTable retorno = new DataTable();
            retorno = dal.ValidarOrderID(whcol130); 
            return retorno;

        }

        public DataTable ValidarItem(Ent_twhcol130 whcol130)
        {
            DataTable retorno = new DataTable();
            retorno = dal.ValidarItem(whcol130);
            return retorno;

        }

        public DataTable ValidarLote(Ent_twhcol130 whcol130)
        {
            DataTable retorno = new DataTable();
            retorno = dal.ValidarLote(whcol130);
            return retorno;

        }

        public List<DataTable> ListasOrderType()
        {
            List<DataTable> ListasOrderType = new List<DataTable>();

            DataTable DtSalesOrderReturn = new DataTable();
            DataTable DtListaTransferOrder = new DataTable();
            DataTable DtListaPurchaseOrders = new DataTable();


            DtSalesOrderReturn      = dal.ListaSalesOrderReturn();
            DtListaTransferOrder    = dal.ListaTransferOrder();
            DtListaPurchaseOrders   = dal.ListaPurchaseOrders();

            ListasOrderType.Add(DtSalesOrderReturn);
            ListasOrderType.Add(DtListaTransferOrder);
            ListasOrderType.Add(DtListaPurchaseOrders);
            return ListasOrderType;

        }

        public DataTable FactorConvercionDiv(string ITEM, string STUN, string CUNI)
        {
             return dal.FactorConvercionDiv(ITEM, STUN,CUNI);
        }

        public DataTable FactorConvercionMul(string ITEM, string STUN, string CUNI)
        {
            return dal.FactorConvercionMul(ITEM, STUN, CUNI);
        }

        public DataTable ConsultaNOOrdencompra(string ORNO, string PONO, string OORG, decimal CANT, string ITEM, string CLOT)
        {
            return dal.ConsultaNOOrdencompra( ORNO,  PONO,  OORG,  CANT,  ITEM,  CLOT);
        }

        public DataTable ConsultaOrdencompra(string ORNO, string PONO, decimal CANT, string ITEM, string CLOT)
        {
            return dal.ConsultaOrdencompra( ORNO,  PONO,  CANT,  ITEM,  CLOT);
        }

        public bool InsertarReseiptRawMaterial(Ent_twhcol130131 myObj)
        {
            return dal.InsertarReseiptRawMaterial(myObj);
        }

        public bool InsertarReseiptRawMaterial131(Ent_twhcol130131 myObj)
        {
            return dal.InsertarReseiptRawMaterial131(myObj);
        }

 public bool UpdateStatusPicked(Ent_twhcol130131 myObj)
        {
            return dal.UpdateStatusPicked(myObj);
        }
        public DataTable vallidatePurchaseOrder(ref string returnorder, ref string strError)
        {
            //int retorno = -1;
            DataTable retorno;
            try
            {
                retorno = dal.vallidatePurchaseOrder(ref returnorder,  ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }
        public DataTable vallidatePurchaseOrderWithPosition(ref string returnorder, ref string position, ref string strError)
        {
            //int retorno = -1;
            DataTable retorno;
            try
            {
                retorno = dal.vallidatePurchaseOrderWithPosition(ref returnorder, ref  position, ref strError);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString());
            }
        }
		
        public DataTable PaidMayorwhcol130(string ORNO)
        {
            return dal.PaidMayorwhcol130(ORNO);
        }


        public DataTable ConsultaUnidadesMedida()
        {
            return dal.ConsultaUnidadesMedida();
        }

        public DataTable ConsultafactoresporItem(string ITEM)
        {
            return dal.ConsultafactoresporItem(ITEM);
        }

        public List<Ent_twhcol130131> ConsultarPorPalletIDReimpresion(string PAID,string LOGR, string UrlBaseBarcode)
        {
            List<Ent_twhcol130131> Lstwhcol130 = new List<Ent_twhcol130131>();
            DataTable DTwhcol130 = dal.ConsultarPorPalletIDReimpresion(PAID);
            if (DTwhcol130.Rows.Count > 0)
            {

                dal.ActualizarConteoReimpresion(PAID, LOGR);
                foreach(DataRow MyRow in DTwhcol130.Rows){

                     Ent_twhcol130131 MyObjet  = new Ent_twhcol130131{

                        
                         OORG = MyRow["OORG"].ToString(),
                         ORNO = MyRow["ORNO"].ToString(),
                         ITEM = MyRow["ITEM"].ToString(),
                         PAID = MyRow["PAID"].ToString(),
                         PONO = MyRow["PONO"].ToString(),
                         SEQN = MyRow["SEQN"].ToString(),
                         CLOT = MyRow["CLOT"].ToString(),
                         CWAR = MyRow["CWAR"].ToString(),
                         QTYS = MyRow["QTYS"].ToString(),
                         UNIT = MyRow["UNIT"].ToString(),
                         QTYC = MyRow["QTYC"].ToString(),
                         UNIC = MyRow["UNIC"].ToString(),
                         DATE = MyRow["T$DATE"].ToString(),
                         CONF = MyRow["CONF"].ToString(),
                         RCNO = MyRow["RCNO"].ToString(),
                         DATR = MyRow["DATR"].ToString(),
                         LOCA = MyRow["LOCA"].ToString(),
                         DATL = MyRow["DATL"].ToString(),
                         PRNT = MyRow["PRNT"].ToString(),
                         DATP = MyRow["DATP"].ToString(),
                         NPRT = (Convert.ToInt32(MyRow["NPRT"])).ToString(),
                         LOGN = MyRow["LOGN"].ToString(),
                         LOGT = MyRow["LOGT"].ToString(),
                         STAT = MyRow["T$STAT"].ToString(),
                         DSCA = MyRow["DSCA"].ToString(),

                         PAID_URL = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + PAID+ "&code=Code128&dpi=96",
                         ORNO_URL = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + MyRow["OORG"].ToString() + "&code=Code128&dpi=96",
                         ITEM_URL = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + MyRow["ITEM"].ToString() + "&code=Code128&dpi=96",
                         CLOT_URL = MyRow["CLOT"].ToString() == "" ? "" : (UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + MyRow["CLOT"].ToString() + "&code=Code128&dpi=96"),
                         QTYC_URL = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + MyRow["QTYC"].ToString() + "&code=Code128&dpi=96",
                         UNIC_URL = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + MyRow["QTYC"].ToString() + "&code=Code128&dpi=96"
                     };

                    Lstwhcol130.Add(MyObjet);
                }
            }
            return Lstwhcol130;
        }

        public List<Ent_twhcol130131> ConsultarPorPalletIDReimpresion131(string PAID, string LOGR, string UrlBaseBarcode)
        {
            List<Ent_twhcol130131> Lstwhcol130 = new List<Ent_twhcol130131>();
            DataTable DTwhcol130 = dal.ConsultarPorPalletIDReimpresion131(PAID);
            if (DTwhcol130.Rows.Count > 0)
            {

                dal.ActualizarConteoReimpresion131(PAID, LOGR);
                foreach (DataRow MyRow in DTwhcol130.Rows)
                {

                    Ent_twhcol130131 MyObjet = new Ent_twhcol130131
                    {


                        OORG = MyRow["OORG"].ToString(),
                        ORNO = MyRow["ORNO"].ToString(),
                        ITEM = MyRow["ITEM"].ToString(),
                        PAID = MyRow["PAID"].ToString(),
                        PONO = MyRow["PONO"].ToString(),
                        SEQN = MyRow["SEQN"].ToString(),
                        CLOT = MyRow["CLOT"].ToString(),
                        CWAR = MyRow["CWAR"].ToString(),
                        QTYS = MyRow["QTYS"].ToString(),
                        UNIT = MyRow["UNIT"].ToString(),
                        QTYC = MyRow["QTYC"].ToString(),
                        UNIC = MyRow["UNIC"].ToString(),
                        DATE = MyRow["T$DATE"].ToString(),
                        CONF = MyRow["CONF"].ToString(),
                        RCNO = MyRow["RCNO"].ToString(),
                        DATR = MyRow["DATR"].ToString(),
                        LOCA = MyRow["LOCA"].ToString(),
                        DATL = MyRow["DATL"].ToString(),
                        PRNT = MyRow["PRNT"].ToString(),
                        DATP = MyRow["DATP"].ToString(),
                        NPRT = (Convert.ToInt32(MyRow["NPRT"])).ToString(),
                        LOGN = MyRow["LOGN"].ToString(),
                        LOGT = MyRow["LOGT"].ToString(),
                        STAT = MyRow["T$STAT"].ToString(),
                        DSCA = MyRow["DSCA"].ToString(),

                        PAID_URL = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + PAID + "&code=Code128&dpi=96",
                        ORNO_URL = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + MyRow["OORG"].ToString() + "&code=Code128&dpi=96",
                        ITEM_URL = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + MyRow["ITEM"].ToString() + "&code=Code128&dpi=96",
                        CLOT_URL = MyRow["CLOT"].ToString() == "" ? "" : (UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + MyRow["CLOT"].ToString() + "&code=Code128&dpi=96"),
                        QTYC_URL = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + MyRow["QTYC"].ToString() + "&code=Code128&dpi=96",
                        UNIC_URL = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + MyRow["QTYC"].ToString() + "&code=Code128&dpi=96"
                    };

                    Lstwhcol130.Add(MyObjet);
                }
            }
            return Lstwhcol130;
        }

        public DataTable ActualizarConteoReimpresion(string PAID, string LOGR)
        {
            return dal.ActualizarConteoReimpresion(PAID, LOGR);
        }

        public DataTable Consultarttccol307(string PAID,string USRR)
        {
            return dal.Consultarttccol307(PAID, USRR);
        }
        
        public bool Insertarttccol307(Ent_ttccol307 tccol307)
        {
            return dal.Insertarttccol307(tccol307);
        }


        public string ConsultarSumatoriaCantidades130(string ORNO, string PONO ,string SEQNR)
        {

                return dal.ConsultarSumatoriaCantidades130(ORNO,PONO,SEQNR);
            
        }

        public string ConsultarSumatoriaCantidades130(string ORNO, string PONO)
        {

            return dal.ConsultarSumatoriaCantidades130NOOC(ORNO, PONO);

        }


        public List<Ent_twhcol130131> ConsultarPorPalletID(string PAID, string UrlBaseBarcode,string USER)
        {
            List<Ent_twhcol130131> Lstwhcol130 = new List<Ent_twhcol130131>();
            DataTable DTwhcol130 = dal.ConsultarPorPalletIDReimpresionLogp(PAID,USER);
            if (DTwhcol130.Rows.Count > 0)
            {
                foreach (DataRow MyRow in DTwhcol130.Rows)
                {

                    Ent_twhcol130131 MyObjet = new Ent_twhcol130131
                    {


                        OORG = MyRow["OORG"].ToString(),
                        ORNO = MyRow["ORNO"].ToString(),
                        ITEM = MyRow["ITEM"].ToString(),
                        DSCA = MyRow["DSCA"].ToString(),
                        PAID = MyRow["PAID"].ToString(),
                        PONO = MyRow["PONO"].ToString(),
                        SEQN = MyRow["SEQN"].ToString(),
                        CLOT = MyRow["CLOT"].ToString(),
                        CWAR = MyRow["CWAR"].ToString(),
                        QTYS = MyRow["QTYS"].ToString(),
                        UNIT = MyRow["UNIT"].ToString(),
                        QTYC = MyRow["QTYC"].ToString(),
                        UNIC = MyRow["UNIC"].ToString(),
                        DATE = MyRow["T$DATE"].ToString(),
                        CONF = MyRow["CONF"].ToString(),
                        RCNO = MyRow["RCNO"].ToString(),
                        DATR = MyRow["DATR"].ToString(),
                        LOCA = MyRow["LOCA"].ToString(),
                        DATL = MyRow["DATL"].ToString(),
                        PRNT = MyRow["PRNT"].ToString(),
                        DATP = MyRow["DATP"].ToString(),
                        NPRT = (Convert.ToInt32(MyRow["NPRT"])).ToString(),
                        LOGN = MyRow["LOGN"].ToString(),
                        LOGT = MyRow["LOGT"].ToString(),
                        STAT = MyRow["T$STAT"].ToString(),
                        SLOC = MyRow["SLOC"].ToString(),
                        ALLO = MyRow["ALLO"].ToString(),
                        ORNO_URL = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + MyRow["OORG"].ToString() + "&code=Code128&dpi=96",
                        ITEM_URL = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + MyRow["ITEM"].ToString() + "&code=Code128&dpi=96",
                        CLOT_URL = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + MyRow["CLOT"].ToString() + "&code=Code128&dpi=96",
                        QTYC_URL = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + MyRow["QTYC"].ToString() + "&code=Code128&dpi=96",
                        UNIC_URL = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + MyRow["UNIC"].ToString() + "&code=Code128&dpi=96"
                    };

                    Lstwhcol130.Add(MyObjet);
                }
            }
            return Lstwhcol130;
        }

        public DataTable ConsultarLocation(string PAID, string CWAR, string LOCA)
        {

            return dal.ConsultarLocation(CWAR, LOCA);

        }


        public bool ActualizacionPickMaterialWhcol130(string PAID,string PICK, string DATK, string LOGP, string STAT)
        {
            DataTable DT = dal.ActualizacionPickMaterialWhcol130(PAID,PICK, DATK, LOGP, STAT);
            return true;
        }

        public bool ActualizacionLocaWhcol130(string PAID, string LOCA, string CWAA, string LOGT, string STAT)
        {
            return dal.ActualizacionLocaWhcol130(PAID, LOCA, CWAA, LOGT, STAT);
        }

        public bool Eliminartccol307(string PAID, string USER)
        {
            return dal.EliminarRegistrotccol307(PAID, USER);
        }

        public DataTable ConsultarPrioridadNativa(string CWAR)
        {
            return dal.ConsultarPrioridadNativa(CWAR);
        }

        public DataTable ConsultarLocationNativa(string CWAR,string PRIO = "1")
        {
            return dal.ConsultarLocationNativa(CWAR, PRIO);
        }

        public DataTable ConsultaOrdenImportacion(string COTP)
        {
            return dal.ConsultaOrdenImportacion(COTP);
        }

        public DataTable ConsultaPresupuestoImportacion(string ORNO)
        {
            return dal.ConsultaPresupuestoImportacion(ORNO);
        }

        public bool Eliminartccol130(Ent_twhcol130131 MyObj)
        {
            return dal.Eliminartccol130(MyObj);
        }

        public bool Insertartwhcol131(Ent_twhcol130131 MyObj)
        {
            return dal.Insertartwhcol131(MyObj);
        }

        public List<DataTable> ListasOrderType(string ORNO, string TYPE_ORNO)
        {
            List<DataTable> ListasOrderType = new List<DataTable>();

            DataTable DtSalesOrderReturn = new DataTable();
            DataTable DtListaTransferOrder = new DataTable();
            DataTable DtListaPurchaseOrders = new DataTable();

            if(TYPE_ORNO.Trim() =="1" ){
            DtSalesOrderReturn = dal.ListaSalesOrdersReturn(ORNO);
            }
            else if( TYPE_ORNO.Trim() =="22" ){
            DtListaTransferOrder = dal.ListaTransferOrders(ORNO);
            }
            else if (TYPE_ORNO.Trim() == "2" )
            {
            DtListaPurchaseOrders = dal.ListaPurchaseOrder(ORNO);
            }

            ListasOrderType.Add(DtSalesOrderReturn);
            ListasOrderType.Add(DtListaTransferOrder);
            ListasOrderType.Add(DtListaPurchaseOrders);
            return ListasOrderType;

        }

        public DataTable VerificarPalletID(ref string PAID)
        {
            return dal.VerificarPalletID(PAID);
        }

        public DataTable VerificarPalletIDz(ref string PAID)
        {
            return dal.VerificarPalletIDz(PAID);
        }

        public DataTable VerificarZoneCode(ref string ZONE)
        {
            return dal.VerificarZoneCode(ZONE);
        }

        public DataTable PaidMayorwhcol131(string ORNO)
        {
            return dal.maximaSecuencia(ORNO);
        }
    }
}
