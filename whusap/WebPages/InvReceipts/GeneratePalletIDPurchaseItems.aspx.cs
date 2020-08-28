using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using whusa.Interfases;
using whusa.Entidades;
using System.Data;
using System.Web.Services;
using Newtonsoft.Json;
using System.Web.Configuration;
using whusa;
using System.Threading;
using System.Configuration;
using System.Globalization;
using whusa.Utilidades;

namespace whusap.WebPages.InvReceipts
{
    public partial class GeneratePalletIDPurchaseItems : System.Web.UI.Page
    {
        public static int kltc = 0;
        public static string RequestUrlAuthority = string.Empty;
        string formName = string.Empty;
        public static string _operator = string.Empty;
        string _idioma = string.Empty;
        private static string globalMessages = "GlobalMessages";

        public static string ItemcodeisnotPurchaseType = mensajes("ItemcodeisnotPurchaseType");
        public static string Itemcodedoesntexist = mensajes("Itemcodedoesn´texist");
        public static string Lotcodedoesntexist = mensajes("Lotcodedoesntexist");
        public static string Warehousecodedoesntexist = mensajes("Warehousecodedoesntexist");
        public static string Locationblockedinbound = mensajes("Locationblockedinbound");
        public static string Locationcodedoesntexist = mensajes("Locationcodedoesntexist");
        public static string codedoesntexist = mensajes("codedoesntexist");
        public static string RegisteredquantitynotavilableonBaaninventory = mensajes("RegisteredquantitynotavilableonBaaninventory");

        public static string UrlBaseBarcode = WebConfigurationManager.AppSettings["UrlBaseBarcode"].ToString();
        private static InterfazDAL_ttccol301 _idalttccol301 = new InterfazDAL_ttccol301();
        public static InterfazDAL_twhcol130 twhcol130DAL = new InterfazDAL_twhcol130();
        public static InterfazDAL_ttcibd001 ITtcibd001 = new InterfazDAL_ttcibd001();
        public static InterfazDAL_tticol125 ITticol125 = new InterfazDAL_tticol125();
        public static InterfazDAL_ttwhcol016 ITtwhcol016 = new InterfazDAL_ttwhcol016();
        public static InterfazDAL_twhwmd200 ITwhwmd200 = new InterfazDAL_twhwmd200();
        public static IntefazDAL_transfer Itransfer = new IntefazDAL_transfer();
        public static InterfazDAL_twhinr140 ITtwhinr140 = new InterfazDAL_twhinr140();

        protected void Page_Load(object sender, EventArgs e)
        {
            RequestUrlAuthority = (string)Request.Url.Authority;


            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-CO");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("es-CO");
            base.InitializeCulture();

            if (!IsPostBack)
            {
                formName = Request.Url.AbsoluteUri.Split('/').Last();

                if (formName.Contains('?'))
                {
                    formName = formName.Split('?')[0];
                }
                Label control = (Label)Page.Controls[0].FindControl("lblPageTitle");
                if (Session["user"] == null)
                {
                    Response.Redirect(ConfigurationManager.AppSettings["UrlBase"] + "/WebPages/Login/whLogIni.aspx");
                }

                _operator = Session["user"].ToString();

                try
                {
                    _idioma = Session["ddlIdioma"].ToString();
                }
                catch (Exception)
                {
                    _idioma = "INGLES";
                }

                string strError = string.Empty;

                Ent_ttccol301 data = new Ent_ttccol301()
                {
                    user = _operator,
                    come = "",
                    refcntd = 0,
                    refcntu = 0
                };

                List<Ent_ttccol301> datalog = new List<Ent_ttccol301>();
                datalog.Add(data);

                _idalttccol301.insertarRegistro(ref datalog, ref strError);
            }
        }
        [WebMethod]
        public static string VerificarItem(string ITEM)
        {

            Ent_ttcibd001 ObjTtcibd001 = new Ent_ttcibd001();

            DataTable dtTtcibd001 = ITtcibd001.findItem(ITEM);
            if (dtTtcibd001.Rows.Count > 0)
            {
                kltc = Convert.ToInt32(dtTtcibd001.Rows[0]["KLTC"].ToString());
                ObjTtcibd001.item = dtTtcibd001.Rows[0]["ITEM"].ToString();
                ObjTtcibd001.dsca = dtTtcibd001.Rows[0]["DSCA"].ToString();
                ObjTtcibd001.cuni = dtTtcibd001.Rows[0]["CUNI"].ToString();
                ObjTtcibd001.kltc = kltc.ToString();
                ObjTtcibd001.kitm = dtTtcibd001.Rows[0]["KITM"].ToString();

                if (ObjTtcibd001.kitm.Trim() == "1")
                {
                    ObjTtcibd001.Error = false;
                    ObjTtcibd001.TypeMsgJs = "console";
                    ObjTtcibd001.SuccessMsg = "Item Encontrado";
                }
                else
                {
                    ObjTtcibd001.Error = true;
                    ObjTtcibd001.TypeMsgJs = "label";
                    
                    ObjTtcibd001.ErrorMsg = ItemcodeisnotPurchaseType;

                }
            }
            else
            {
                ObjTtcibd001.Error = true;
                ObjTtcibd001.TypeMsgJs = "label";
                
                ObjTtcibd001.ErrorMsg = Itemcodedoesntexist;
            }



            return JsonConvert.SerializeObject(ObjTtcibd001);
        }


        [WebMethod]
        public static string VerificarLote(string ITEM, string CLOT)
        {
            string strError = string.Empty;

            Ent_tticol125 Obj_tticol125 = new Ent_tticol125();
            Obj_tticol125.item = ITEM;
            Obj_tticol125.clot = CLOT;

            DataTable DtTticol125 = ITticol125.listaRegistrosLoteItem_Param(ref Obj_tticol125);

            if (DtTticol125.Rows.Count > 0)
            {
                Obj_tticol125.Error = false;
                Obj_tticol125.TypeMsgJs = "console";
                Obj_tticol125.SuccessMsg = "Lote Encontrado";
            }
            else
            {
                Obj_tticol125.Error = true;
                Obj_tticol125.TypeMsgJs = "label";
                
                Obj_tticol125.ErrorMsg = Lotcodedoesntexist;

            }

            return JsonConvert.SerializeObject(Obj_tticol125);


        }
        [WebMethod]
        public static string VerificarWarehouse(string CWAR)
        {

            string strError = string.Empty;

            Ent_ttwhcol016 Obj_twhcol016 = new Ent_ttwhcol016();
            Obj_twhcol016.cwar = CWAR;

            Ent_twhwmd200 Obj_twhwmd200 = new Ent_twhwmd200();
            Obj_twhwmd200.cwar = CWAR.ToUpper();

            DataTable DtTtwhcol016 = ITtwhcol016.TakeMaterialInv_verificaBodega_Param(ref Obj_twhcol016, ref strError);
            DataTable DtTwhwmd200 = ITwhwmd200.listaRegistro_ObtieneAlmacenLocation(ref Obj_twhwmd200, ref strError);

            if (DtTtwhcol016.Rows.Count > 0)
            {
                Obj_twhcol016.Error = false;
                Obj_twhcol016.TypeMsgJs = "console";
                Obj_twhcol016.SuccessMsg = "Warehouse Encontrado";
                Obj_twhcol016.dsca = DtTtwhcol016.Rows[0]["DESCRIPCION"].ToString();

                if (DtTwhwmd200.Rows.Count > 0)
                {
                    Obj_twhcol016.sloc = DtTwhwmd200.Rows[0]["LOC"].ToString();
                }
                else
                {
                    Obj_twhcol016.sloc = string.Empty;
                }
            }
            else
            {
                Obj_twhcol016.Error = true;
                Obj_twhcol016.TypeMsgJs = "label";
                
                Obj_twhcol016.ErrorMsg = Warehousecodedoesntexist;
            }

            return JsonConvert.SerializeObject(Obj_twhcol016);
        }

        [WebMethod]
        public static string VerificarLocation(string CWAR, string LOCA)
        {


            string strError = string.Empty;
            Ent_twhwmd200 Obj_twhwmd200 = new Ent_twhwmd200();
            Obj_twhwmd200.cwar = CWAR;

            DataTable DtTransfer = Itransfer.ConsultarLocation(Obj_twhwmd200.cwar, LOCA);

            if (DtTransfer.Rows.Count > 0)
            {
                if (DtTransfer.Rows[0]["LOCT"].ToString() == "5")
                {
                    if (DtTransfer.Rows[0]["BINB"].ToString() == "2")
                    {
                        Obj_twhwmd200.Error = false;
                        Obj_twhwmd200.TypeMsgJs = "console";
                        Obj_twhwmd200.SuccessMsg = "Location Encontrado";
                    }
                    else
                    {
                        Obj_twhwmd200.Error = true;
                        Obj_twhwmd200.TypeMsgJs = "label";
                        
                        Obj_twhwmd200.ErrorMsg = Locationblockedinbound;
                    }

                }
                else
                {
                    Obj_twhwmd200.Error = true;
                    Obj_twhwmd200.TypeMsgJs = "label";
                    
                    Obj_twhwmd200.ErrorMsg = Locationcodedoesntexist;
                }

            }
            else
            {
                Obj_twhwmd200.Error = true;
                Obj_twhwmd200.TypeMsgJs = "label";
                
                Obj_twhwmd200.ErrorMsg = codedoesntexist;
            }

            return JsonConvert.SerializeObject(Obj_twhwmd200);


        }
        [WebMethod]
        public static string VerificarQuantity(string CWAR, string ITEM, string CLOT, string LOCA)
        {
            string strError = string.Empty;
            DataTable DtTtwhinr140 = new DataTable();

            CWAR = CWAR.ToUpper();
            ITEM = ITEM.ToUpper();
            CLOT = CLOT.ToUpper();
            LOCA = LOCA.ToUpper();

            DtTtwhinr140 = ITtwhinr140.consultaStks(ref CWAR, ref ITEM, ref CLOT, ref LOCA, ref strError);

            Ent_twhinr140 ObjTtwhinr140 = new Ent_twhinr140();

            if (DtTtwhinr140.Rows.Count > 0)
            {
                decimal stks = Convert.ToDecimal(DtTtwhinr140.Rows[0]["STKS"].ToString());
                if (stks > 0)
                {
                    ObjTtwhinr140.stks = stks;
                    ObjTtwhinr140.Error = false;
                    ObjTtwhinr140.TypeMsgJs = "label";
                    
                    ObjTtwhinr140.SuccessMsg = RegisteredquantitynotavilableonBaaninventory;
                }
                else
                {
                    ObjTtwhinr140.Error = true;
                    ObjTtwhinr140.TypeMsgJs = "label";
                    
                    ObjTtwhinr140.ErrorMsg = RegisteredquantitynotavilableonBaaninventory;
                }
            }
            else
            {
                ObjTtwhinr140.Error = true;
                ObjTtwhinr140.TypeMsgJs = "label";
                
                ObjTtwhinr140.ErrorMsg = RegisteredquantitynotavilableonBaaninventory;
            }

            return JsonConvert.SerializeObject(ObjTtwhinr140);

        }

        [WebMethod]
        public static string Click_Save(string CWAR, string ITEM, string CLOT, string LOCA, string QTYS, string UNIT)
        {
            int consecutivoPalletID = 0;
            DataTable DTPalletContinue = twhcol130DAL.PaidMayorwhcol130("INITIAPOP");
            string SecuenciaPallet = "001";
            if (DTPalletContinue.Rows.Count > 0)
            {
                foreach (DataRow item in DTPalletContinue.Rows)
                {
                    consecutivoPalletID = Convert.ToInt32(item["T$PAID"].ToString().Trim().Substring(10, 3)) + 1;
                    if (consecutivoPalletID.ToString().Length == 1)
                    {
                        SecuenciaPallet = "00" + consecutivoPalletID;
                    }
                    if (consecutivoPalletID.ToString().Length == 2)
                    {
                        SecuenciaPallet = "0" + consecutivoPalletID;
                    }
                    if (consecutivoPalletID.ToString().Length == 3)
                    {
                        SecuenciaPallet = consecutivoPalletID.ToString();
                    }

                }

            }

            Ent_twhcol130131 MyObj = new Ent_twhcol130131
            {
                OORG = "2",// Order type escaneada view 
                ORNO = "INITIAPOP",
                ITEM = "         " + ITEM.ToUpper(),
                PAID = "INITIAPOP" + "-" + SecuenciaPallet,
                PONO = "1",
                SEQN = "1",
                CLOT = CLOT.ToUpper(),// lote VIEW
                CWAR = CWAR.ToUpper(),
                QTYS = QTYS,// cantidad escaneada view 
                UNIT = UNIT,//unit escaneada view
                QTYC = QTYS,//cantidad escaneada view aplicando factor
                UNIC = UNIT,//unidad view stock
                DATE = DateTime.Now.ToString("dd/MM/yyyy").ToString(),//fecha de confirmacion 
                CONF = "1",
                RCNO = " ",//llena baan
                DATR = DateTime.Now.ToString("dd/MM/yyyy").ToString(),//llena baan
                LOCA = LOCA.ToUpper(),// enviamos vacio
                DATL = DateTime.Now.ToString("dd/MM/yyyy").ToString(),//llenar con fecha de hoy
                PRNT = "1",// llenar en 1
                DATP = DateTime.Now.ToString("dd/MM/yyyy").ToString(),//llena baan
                NPRT = "1",//conteo de reimpresiones 
                LOGN = _operator,// nombre de ususario de la session
                LOGT = " ",//llena baan
                STAT = "3",// LLENAR EN 3 
                DSCA = " ",
                COTP = " ",
                FIRE = "2",
                PSLIP = " ",
                ALLO = "0",
                PAID_URL = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + "INITIAPOP" + "-" + SecuenciaPallet + "&code=Code128&dpi=96",
                ORNO_URL = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + "DTOrdencompra.Rows[0][].ToString()" + "&code=Code128&dpi=96",
                ITEM_URL = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + ITEM + "&code=Code128&dpi=96",
                CLOT_URL = CLOT.ToString().Trim() != "" ? UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + CLOT + "&code=Code128&dpi=96" : "",
                QTYC_URL = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + QTYS + "&code=Code128&dpi=96",
                UNIC_URL = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + UNIT + "&code=Code128&dpi=96"
            };

            bool Insertsucces = twhcol130DAL.Insertartwhcol131(MyObj);

            return JsonConvert.SerializeObject(MyObj);

        }

        protected static string mensajes(string tipoMensaje)
        {
            string idioma = "INGLES";
            Mensajes _mensajesForm = new Mensajes();
            var retorno = _mensajesForm.readStatement("GeneratePalletIDPurchaseItems.aspx", idioma, ref tipoMensaje);

            if (retorno.Trim() == String.Empty)
            {
                retorno = _mensajesForm.readStatement(globalMessages, idioma, ref tipoMensaje);
            }

            return retorno;
        }
    }
}