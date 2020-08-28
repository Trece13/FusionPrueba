using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using whusa.Interfases;
using whusa.Entidades;
using System.Data;
using System.Globalization;
using System.Web.Configuration;
using System.Threading;
using Newtonsoft.Json;
using System.Configuration;
using whusa.Utilidades;

namespace whusap.WebPages.InvReceipts
{

    public partial class InformationPalletIDRegrind : System.Web.UI.Page
    {

        public static string PalletIdDoesntExist        = string.Empty;
        public static string ItemDodeIsNotPurchaseType  = string.Empty;
        public static string ItemCodeDoesntExist        = string.Empty;
        public static string LotCodeCoesntExist         = string.Empty;
        public static string WarehouseCodeDoesntExist   = string.Empty;
        public static string LocationCodeDoesntExist    = string.Empty;
        public static string codeDoesntExist            = string.Empty;

        public static string RegisteredQuantityNotAvilableOnBaanInventory = string.Empty;
        private static string globalMessages = "GlobalMessages";
        private static Mensajes _mensajesForm = new Mensajes();
        public static string RequestUrlAuthority = string.Empty;
        string formName = string.Empty;
        public static string _operator = string.Empty;
        string _idioma = string.Empty;
        public static string UrlBaseBarcode = WebConfigurationManager.AppSettings["UrlBaseBarcode"].ToString();
        private static InterfazDAL_twhcol122 _idalttwhcol122 = new InterfazDAL_twhcol122();
        private static InterfazDAL_tticol042 _idaltticol042 = new InterfazDAL_tticol042();
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
               CargarIdioma();

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
        public static string VerificarPalletID(string PAID)
        {
            string strError = string.Empty;
            Ent_tticol042 ObjTticol042 = new Ent_tticol042();
            ObjTticol042.sqnb = PAID.Trim();
            DataTable DTticol042 = _idaltticol042.listaRegistroXSQNB_LocatedRegrind(ref ObjTticol042, ref strError);
            

            if (DTticol042.Rows.Count > 0)
            {


                ObjTticol042.pdno = DTticol042.Rows[0]["T$PDNO"].ToString();
                ObjTticol042.sqnb = DTticol042.Rows[0]["T$SQNB"].ToString();
                ObjTticol042.mitm = DTticol042.Rows[0]["T$MITM"].ToString();
                ObjTticol042.dsca = DTticol042.Rows[0]["T$DSCA"].ToString();
                ObjTticol042.cuni = DTticol042.Rows[0]["T$CUNI"].ToString();
                ObjTticol042.kltc = DTticol042.Rows[0]["T$KLTC"].ToString();
                ObjTticol042.Error = false;

                if (DTticol042.Rows[0]["T$DELE"].ToString() != "7")
                {
                    ObjTticol042.Error = true;
                    ObjTticol042.TypeMsgJs = "label";
                    
                    ObjTticol042.ErrorMsg = PalletIdDoesntExist;
                }
            }
            else
            {
                ObjTticol042.Error = true;
                ObjTticol042.TypeMsgJs = "label";
                
                ObjTticol042.ErrorMsg = PalletIdDoesntExist;
            }

            return JsonConvert.SerializeObject(ObjTticol042);
        }

        [WebMethod]
        public static string VerificarItem(string ITEM)
        {

            Ent_ttcibd001 ObjTtcibd001 = new Ent_ttcibd001();

            DataTable dtTtcibd001 = ITtcibd001.findItem(ITEM);
            if (dtTtcibd001.Rows.Count > 0)
            {
                ObjTtcibd001.item = dtTtcibd001.Rows[0]["ITEM"].ToString();
                ObjTtcibd001.dsca = dtTtcibd001.Rows[0]["DSCA"].ToString();
                ObjTtcibd001.cuni = dtTtcibd001.Rows[0]["CUNI"].ToString();
                ObjTtcibd001.kltc = dtTtcibd001.Rows[0]["KLTC"].ToString();
                ObjTtcibd001.kitm = dtTtcibd001.Rows[0]["KITM"].ToString();

                if (ObjTtcibd001.kitm.Trim() == "1")
                {
                    ObjTtcibd001.Error = true;
                    ObjTtcibd001.TypeMsgJs = "label";
                    
                    ObjTtcibd001.ErrorMsg = ItemDodeIsNotPurchaseType;
                }
                else
                {
                    ObjTtcibd001.Error = false;
                    ObjTtcibd001.TypeMsgJs = "console";
                    
                    ObjTtcibd001.SuccessMsg = "Item Encontrado";
                }
            }
            else
            {
                ObjTtcibd001.Error = true;
                ObjTtcibd001.TypeMsgJs = "label";

                ObjTtcibd001.ErrorMsg = ItemCodeDoesntExist;
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
                Obj_tticol125.ErrorMsg = LotCodeCoesntExist;

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
            Obj_twhwmd200.cwar = CWAR;

            DataTable DtTtwhcol016 = ITtwhcol016.TakeMaterialInv_verificaBodega_Param(ref Obj_twhcol016, ref strError);
            DataTable DtTwhwmd200 = ITwhwmd200.listaRegistro_ObtieneAlmacenLocation(ref Obj_twhwmd200, ref strError);

            if (DtTtwhcol016.Rows.Count > 0)
            {
                Obj_twhcol016.Error = false;
                Obj_twhcol016.TypeMsgJs = "console";
                Obj_twhcol016.SuccessMsg = "Warehouse Encontrado";

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
                Obj_twhcol016.ErrorMsg = WarehouseCodeDoesntExist;

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
                    Obj_twhwmd200.Error = false;
                    Obj_twhwmd200.TypeMsgJs = "console";
                    Obj_twhwmd200.SuccessMsg = "Location Encontrado";
                }
                else
                {
                    Obj_twhwmd200.Error = true;
                    Obj_twhwmd200.TypeMsgJs = "label";
                    
                    Obj_twhwmd200.ErrorMsg = LocationCodeDoesntExist;

                }

            }
            else
            {
                Obj_twhwmd200.Error = true;
                Obj_twhwmd200.TypeMsgJs = "label";
                
                Obj_twhwmd200.ErrorMsg = codeDoesntExist;

            }

            return JsonConvert.SerializeObject(Obj_twhwmd200);


        }
        [WebMethod]
        public static string VerificarQuantity(string CWAR, string ITEM, string LOCA = " ", string CLOT = " ")
        {
            string strError = string.Empty;
            DataTable DtTtwhinr140 = ITtwhinr140.consultaPorAlmacenItemUbicacionLote(ref CWAR, ref ITEM, ref LOCA, ref CLOT, ref strError);
            Ent_twhinr140 ObjTtwhinr140 = new Ent_twhinr140();

            if (DtTtwhinr140.Rows.Count > 0)
            {
                if (Convert.ToDecimal(DtTtwhinr140.Rows[0]["STKS"].ToString()) < 1)
                {
                    ObjTtwhinr140.Error = true;
                    ObjTtwhinr140.TypeMsgJs = "label";
                    
                    ObjTtwhinr140.ErrorMsg = RegisteredQuantityNotAvilableOnBaanInventory;
                }
                else
                {
                    ObjTtwhinr140.stks = Convert.ToDecimal(DtTtwhinr140.Rows[0]["STKS"].ToString());
                    ObjTtwhinr140.Error = false;
                    ObjTtwhinr140.TypeMsgJs = "label";
                    
                    ObjTtwhinr140.SuccessMsg = RegisteredQuantityNotAvilableOnBaanInventory;
                }

            }
            else
            {
                ObjTtwhinr140.Error = true;
                ObjTtwhinr140.TypeMsgJs = "label";
                
                ObjTtwhinr140.ErrorMsg = RegisteredQuantityNotAvilableOnBaanInventory;
            }

            return JsonConvert.SerializeObject(ObjTtwhinr140);

        }

        [WebMethod]
        public static string Click_Save(string PAID, string ORNO, string ITEM, string CWAR, string LOCA, string UNIT, string QTYS)
        {

            string strError = string.Empty;
            _idalttwhcol122.ActCausalTICOL042(PAID, 7);

            Ent_tticol042 ObjTticol042 = new Ent_tticol042();
            ObjTticol042.pdno = ORNO;
            ObjTticol042.sqnb = PAID;
            ObjTticol042.urpt = _operator;
            ObjTticol042.acqt = Convert.ToDouble(QTYS);
            ObjTticol042.cwaf = CWAR;
            ObjTticol042.cwat = CWAR;
            ObjTticol042.aclo = LOCA;
            ObjTticol042.allo = 0;
            ObjTticol042.Error = false;
            List<Ent_tticol042> Listtticol042 = new List<Ent_tticol042>();
            Listtticol042.Add(ObjTticol042);

            bool res = _idaltticol042.insertarRegistroTticon242(ref Listtticol042, ref strError);
            if (res == false)
            {
                _idaltticol042.ActualizarCantidadAlmacenRegistroTicol242(_operator, ObjTticol042.acqt, ObjTticol042.aclo, ObjTticol042.cwaf, ObjTticol042.sqnb);
            }

            return JsonConvert.SerializeObject(ObjTticol042);

        }

        protected void CargarIdioma()
        {

            PalletIdDoesntExist = mensajes("PalletIdDoesntExist");
            ItemDodeIsNotPurchaseType = mensajes("ItemDodeIsNotPurchaseType");
            ItemCodeDoesntExist = mensajes("ItemCodeDoesntExist");
            LotCodeCoesntExist = mensajes("LotCodeCoesntExist");
            WarehouseCodeDoesntExist = mensajes("WarehouseCodeDoesntExist");
            LocationCodeDoesntExist = mensajes("LocationCodeDoesntExist");
            codeDoesntExist = mensajes("CodeDoesntExist");
            RegisteredQuantityNotAvilableOnBaanInventory = mensajes("RegisteredQuantityNotAvilableOnBaanInventory");
        }

        protected string mensajes(string tipoMensaje)
        {
            var retorno = _mensajesForm.readStatement(formName, _idioma, ref tipoMensaje);

            if (retorno.Trim() == String.Empty)
            {
                retorno = _mensajesForm.readStatement(globalMessages, _idioma, ref tipoMensaje);
            }

            return retorno;
        }
    }
}