using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using whusa.Interfases;
using System.Threading;
using System.Web.Services;
using whusa.Entidades;
using whusa;
using System.Data;
using System.Globalization;
using System.Web.Configuration;
using Newtonsoft.Json;
using System.Configuration;
using whusa.Utilidades;

namespace whusap.WebPages.InvReceipts
{
    public partial class InventoryTakingByPalletIDWarehousesNotControlledByLocation : System.Web.UI.Page
    {
        public static string RequestUrlAuthority = string.Empty;
        private static string globalMessages = "GlobalMessages";
        public static string UrlBaseBarcode = WebConfigurationManager.AppSettings["UrlBaseBarcode"].ToString();        
        string formName = string.Empty;
        public static string _operator = string.Empty;
        string _idioma = string.Empty;
        public static string PCLOT = string.Empty;

        private static InterfazDAL_tticol022 _idaltticol022 = new InterfazDAL_tticol022();
        private static InterfazDAL_twhcol019 _idaltwhcol019 = new InterfazDAL_twhcol019();
        private static InterfazDAL_ttccol301 _idalttccol301 = new InterfazDAL_ttccol301();
        public static InterfazDAL_twhcol130 twhcol130DAL = new InterfazDAL_twhcol130();
        public static InterfazDAL_ttcibd001 ITtcibd001 = new InterfazDAL_ttcibd001();
        public static InterfazDAL_tticol125 ITticol125 = new InterfazDAL_tticol125();
        public static InterfazDAL_ttwhcol016 ITtwhcol016 = new InterfazDAL_ttwhcol016();
        public static InterfazDAL_twhwmd200 ITwhwmd200 = new InterfazDAL_twhwmd200();
        public static IntefazDAL_transfer Itransfer = new IntefazDAL_transfer();
        public static InterfazDAL_twhinr140 ITtwhinr140 = new InterfazDAL_twhinr140();


        public static string Zonecodedoesntexist; 
        public static string ThepaletteIDdoesnotexistorisnotlocated; 
        public static string Lotcodedoesntexist; 
        public static string Locationblockedinbound; 
        public static string Locationcodedoesntexist; 
        public static string Registeredquantitycannotbelessthanorequaltozero; 
        public static string Warehousecodedoesntexist; 
                                                        
               
        public static string ItemcodeisnotPurchaseType                          = mensajes("ItemcodeisnotPurchaseType");
        public static string Itemcodedoesntexist                                = mensajes("Itemcodedoesntexist");
        public static string Therecordwassuccessfullyinserted                   = mensajes("Therecordwassuccessfullyinserted");
        public static string Therecordwasnotinserted                            = mensajes("Therecordwasnotinserted");


        protected void Page_Load(object sender, EventArgs e)
        {
            CargarIdioma();
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
        public static string VerificarZoneCode(string ZONE)
        {
            string strError = string.Empty;
            DataTable DTZoneCode = twhcol130DAL.VerificarZoneCode(ref ZONE);
            EntidadZones ObjZone = new EntidadZones();

            if (DTZoneCode.Rows.Count > 0)
            {
                ObjZone.CWAR = DTZoneCode.Rows[0]["T$CWAR"].ToString();
                ObjZone.ZONE = DTZoneCode.Rows[0]["T$ZONE"].ToString();
                ObjZone.DSCA = DTZoneCode.Rows[0]["T$DSCA"].ToString();
                ObjZone.BALL = DTZoneCode.Rows[0]["T$BALL"].ToString();
                ObjZone.BINB = DTZoneCode.Rows[0]["T$BINB"].ToString();
                ObjZone.BOUT = DTZoneCode.Rows[0]["T$BOUT"].ToString();
                ObjZone.BTRR = DTZoneCode.Rows[0]["T$BTRR"].ToString();
                ObjZone.BTRI = DTZoneCode.Rows[0]["T$BTRI"].ToString();
                ObjZone.BASS = DTZoneCode.Rows[0]["T$BASS"].ToString();
                ObjZone.EMNO = DTZoneCode.Rows[0]["T$EMNO"].ToString();
                ObjZone.PRTR = DTZoneCode.Rows[0]["T$PRTR"].ToString();
                //PDNO, SQNB, MITM, DSCA, CUNI, QTDL, DELE, PRO1, PROC
                ObjZone.error = false;

            }
            else
            {
                ObjZone.error = true;
                ObjZone.typeMsgJs = "label";
                ObjZone.errorMsg = Zonecodedoesntexist;
            }


            return JsonConvert.SerializeObject(ObjZone);
        }

        [WebMethod]
        public static string VerificarPalletID(string PAID)
        {
            string strError = string.Empty;
            PAID = PAID.ToUpper();
            DataTable DTPalletID = twhcol130DAL.VerificarPalletIDz(ref PAID);
            EntidadPicking ObjPicking = new EntidadPicking();

            if (DTPalletID.Rows.Count > 0)
            {
                ObjPicking.PALLETID = DTPalletID.Rows[0]["PAID"].ToString();
                ObjPicking.ITEM = DTPalletID.Rows[0]["ITEM"].ToString();
                ObjPicking.DESCRIPTION = DTPalletID.Rows[0]["DSCA"].ToString();
                ObjPicking.LOT = DTPalletID.Rows[0]["DSCA"].ToString().Trim();
                ObjPicking.WRH = DTPalletID.Rows[0]["CWAT"].ToString().Trim();
                ObjPicking.DESCWRH = DTPalletID.Rows[0]["DESCAW"].ToString();
                ObjPicking.LOCA = DTPalletID.Rows[0]["ACLO"].ToString().Trim();
                ObjPicking.QTY = DTPalletID.Rows[0]["QTYT"].ToString();
                ObjPicking.UN = DTPalletID.Rows[0]["UNIT"].ToString();
                ObjPicking.STAT = DTPalletID.Rows[0]["STAT"].ToString();
                ObjPicking.SLOC = DTPalletID.Rows[0]["SLOC"].ToString();
                //PDNO, SQNB, MITM, DSCA, CUNI, QTDL, DELE, PRO1, PROC
                ObjPicking.error = false;

                //if (DTPalletID.Rows[0]["STAT"].ToString() != "7")
                //{
                //    ObjPicking.error = true;
                //    ObjPicking.typeMsgJs = "label";
                //    ObjPicking.errorMsg = "Pallet ID doesn´t exist";
                //}
            }
            else
            {
                ObjPicking.error = true;
                ObjPicking.typeMsgJs = "label";
                
                ObjPicking.errorMsg = ThepaletteIDdoesnotexistorisnotlocated;
            }


            return JsonConvert.SerializeObject(ObjPicking);
        }

        public static string VerificarItem(string ITEM,string CLOT)
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

                if (ObjTtcibd001.kltc.Trim() == "1")
                {
                    PCLOT = CLOT;
                }

                if (ObjTtcibd001.kitm.Trim() == "1")
                {
                    ObjTtcibd001.Error = true;
                    ObjTtcibd001.TypeMsgJs = "label";
                    
                    ObjTtcibd001.ErrorMsg = ItemcodeisnotPurchaseType;
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
                ObjTtcibd001.error = true;
                ObjTtcibd001.typeMsgJs = "label";
                ObjTtcibd001.errorMsg = Itemcodedoesntexist;
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
                Obj_tticol125.error = false;
                Obj_tticol125.typeMsgJs = "console";
                Obj_tticol125.SuccessMsg = "Lote Encontrado";
            }
            else
            {

                Obj_tticol125.error = true;
                Obj_tticol125.typeMsgJs = "label";
                Obj_tticol125.errorMsg = Lotcodedoesntexist;

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
                Obj_twhcol016.error = false;
                Obj_twhcol016.typeMsgJs = "console";
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
              
                Obj_twhcol016.error = true;
                Obj_twhcol016.typeMsgJs = "label";
                Obj_twhcol016.errorMsg = Warehousecodedoesntexist;
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
                        Obj_twhwmd200.error = true;
                        Obj_twhwmd200.typeMsgJs = "label";
                        Obj_twhwmd200.errorMsg = Locationblockedinbound
;
                    }
                }
                else
                {
                    Obj_twhwmd200.error = true;
                    Obj_twhwmd200.typeMsgJs = "label";
                    Obj_twhwmd200.errorMsg = Locationcodedoesntexist;
                }

            }
            else
            {
                Obj_twhwmd200.error = true;
                Obj_twhwmd200.typeMsgJs = "label";
                Obj_twhwmd200.errorMsg = Locationcodedoesntexist;
            }

            return JsonConvert.SerializeObject(Obj_twhwmd200);


        }

        [WebMethod]
        public static string VerificarQuantity(string QTY)
        {
            //LOCA = LOCA.Trim() == "" ? " " : LOCA.Trim();
            //CLOT = CLOT.Trim() == "" ? " " : CLOT.Trim();
            //string strError = string.Empty;
            //DataTable DtTtwhinr140 = ITtwhinr140.consultaPorAlmacenItemUbicacionLote(ref CWAR, ref ITEM, ref LOCA, ref PCLOT, ref strError);
            Ent_twhinr140 ObjTtwhinr140 = new Ent_twhinr140();
            QTY = QTY.Trim() == "" ? "0" : QTY.Trim();
            QTY = QTY.Contains(".") == true ? QTY.Replace(".",","): QTY.Replace(",",".");
            if (Convert.ToDecimal(QTY) <= 0)
            {
                    ObjTtwhinr140.error = true;
                    ObjTtwhinr140.typeMsgJs = "label";
                    
                    ObjTtwhinr140.errorMsg = Registeredquantitycannotbelessthanorequaltozero;
            }
            else
            {
                ObjTtwhinr140.error = false;
                ObjTtwhinr140.typeMsgJs = "label";
                
                ObjTtwhinr140.errorMsg = Registeredquantitycannotbelessthanorequaltozero;
            }

            return JsonConvert.SerializeObject(ObjTtwhinr140);

        }

        [WebMethod]
        public static string Click_Save(string PAID, string ITEM, string CWAR, string LOCA, string UNIT, string QTYS,string ZONE)
        {
            string strError = string.Empty;

            DataTable DTPalletContinue = _idaltwhcol019.Consetwhcol019(PAID, ref strError);
            int SecuenciaPallet = 0;
            if (DTPalletContinue.Rows.Count > 0)
            {

                SecuenciaPallet = DTPalletContinue.Rows.Count+1;
            }
            else
            {
                SecuenciaPallet = 1;
            }

            Ent_twhcol019 ObjTwhcol019 = new Ent_twhcol019();

            QTYS = QTYS.Trim() == "" ? "0" : QTYS.Trim();
            //QTYS = QTYS.Contains(".") == true ? QTYS.Replace(".", ",") : QTYS.Replace(",", ".");
            decimal QTYD = Convert.ToDecimal(QTYS);

            if (QTYD <= Convert.ToDecimal(0))
            {
                ObjTwhcol019.error = false;
                ObjTwhcol019.typeMsgJs = "label";               
                ObjTwhcol019.SuccessMsg = Registeredquantitycannotbelessthanorequaltozero;
                return JsonConvert.SerializeObject(ObjTwhcol019);
            }
            string CLOT = " ";
            
            ObjTwhcol019.PAID   = PAID;
            ObjTwhcol019.SQNB   = SecuenciaPallet;//Convert.ToInt32(PAID.Substring(10,3));   
            ObjTwhcol019.ZONE   = ZONE;   
            ObjTwhcol019.CWAR   = CWAR;
            ObjTwhcol019.LOCA   = LOCA.Trim() == "" ? " " : LOCA.Trim();   
            ObjTwhcol019.ITEM   = ITEM;   
            ObjTwhcol019.CLOT   = CLOT;
            ObjTwhcol019.QTDL = QTYD;   
            ObjTwhcol019.CUNI   = UNIT;   
            ObjTwhcol019.LOGN   = _operator;   
            ObjTwhcol019.DATE   = new DateTime();   
            ObjTwhcol019.COUN   = 0;   
            ObjTwhcol019.PROC   = 0;   
            ObjTwhcol019.REFCNTD= 0;        
            ObjTwhcol019.REFCNTU= 0;

            bool res = _idaltwhcol019.insertRegistertwhcol019(ref ObjTwhcol019, ref strError);
            if (res)
            {
                ObjTwhcol019.error = false;
                ObjTwhcol019.typeMsgJs = "alert";
                
                ObjTwhcol019.SuccessMsg = Therecordwassuccessfullyinserted;
            }
            else{
                ObjTwhcol019.error = true;
                ObjTwhcol019.typeMsgJs = "alert";
                
                ObjTwhcol019.SuccessMsg = Therecordwasnotinserted+ " " + strError;
            }
            return JsonConvert.SerializeObject(ObjTwhcol019);

        }

        protected static string mensajes(string tipoMensaje)
        {
            Mensajes mensajesForm = new Mensajes();
            string idioma = "INGLES";
            var retorno = mensajesForm.readStatement("InventoryTakingByPalletIDWarehousesNotControlledByLocation.aspx", idioma, ref tipoMensaje);

            if (retorno.Trim() == String.Empty)
            {
                retorno = mensajesForm.readStatement(globalMessages, idioma, ref tipoMensaje);
            }

            return retorno;
        }


        protected static bool CargarIdioma()
        {
            LabelsText _textoLabels = new LabelsText();
            string idioma = "INGLES";
            string formName = "InventoryTakingByPalletIDWarehousesNotControlledByLocation.aspx";
            Zonecodedoesntexist                             =_textoLabels.readStatement(formName, idioma, "Zonecodedoesntexist");
            ThepaletteIDdoesnotexistorisnotlocated          =_textoLabels.readStatement(formName, idioma, "ThepaletteIDdoesnotexistorisnotlocated");
            Lotcodedoesntexist                              =_textoLabels.readStatement(formName, idioma, "Lotcodedoesntexist");
            Locationblockedinbound                          =_textoLabels.readStatement(formName, idioma, "Locationblockedinbound");
            Locationcodedoesntexist                         =_textoLabels.readStatement(formName, idioma, "Locationcodedoesntexist");
            Registeredquantitycannotbelessthanorequaltozero =_textoLabels.readStatement(formName, idioma, "Registeredquantitycannotbelessthanorequaltozero");
            Warehousecodedoesntexist                        =_textoLabels.readStatement(formName, idioma, "Warehousecodedoesntexist");
            return true;
        }

    }

}