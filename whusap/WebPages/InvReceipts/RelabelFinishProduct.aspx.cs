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
using System.Text;
using whusa.Utilidades;

namespace whusap.WebPages.InvReceipts
{
    public partial class RelabelFinishProduct : System.Web.UI.Page
    {


        public static int kltc = 0;
        public static string RequestUrlAuthority = string.Empty;
        string formName = string.Empty;
        public static string _operator = string.Empty;
        string _idioma = string.Empty;
        private static string globalMessages = "GlobalMessages";
        public static string UrlBaseBarcode = WebConfigurationManager.AppSettings["UrlBaseBarcode"].ToString();
        public static string cyclecountLabel = WebConfigurationManager.AppSettings["cyclecountLabel"].ToString();

        public static string ItemcodeisnotManufacturingType = mensajes("ItemcodeisnotManufacturingType");
        public static string Itemcodedoesntexist = mensajes("Itemcodedoesntexist");
        public static string Lotcodedoesntexist = mensajes("Lotcodedoesntexist");
        public static string Warehousecodedoesntexist = mensajes("Warehousecodedoesntexist");
        public static string Locationblockedinbound = mensajes("Locationblockedinbound");
        public static string Locationcodedoesntexist = mensajes("Locationcodedoesntexist");
        public static string codedoesntexist = mensajes("codedoesntexist");
        public static string RegisteredquantitynotavilableonBaaninventory = mensajes("RegisteredquantitynotavilableonBaaninventory");
        public static string FactordontexistforthisItem = mensajes("FactordontexistforthisItem");

        private static InterfazDAL_ttccol301 _idalttccol301 = new InterfazDAL_ttccol301();
        public static InterfazDAL_twhcol130 twhcol130DAL = new InterfazDAL_twhcol130();
        private static InterfazDAL_tticol022 _idaltticol022 = new InterfazDAL_tticol022();
        private static InterfazDAL_tticol042 _idaltticol042 = new InterfazDAL_tticol042();
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

                HttpContext.Current.Session["ITEM"] = "";
                HttpContext.Current.Session["CWAR"] = "";
                HttpContext.Current.Session["CLOT"] = "";
                HttpContext.Current.Session["LOCA"] = "";
                HttpContext.Current.Session["QTYS"] = "";
                HttpContext.Current.Session["UNIT"] = "";
                HttpContext.Current.Session["CUNI"] = "";

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
            HttpContext.Current.Session["myItemType"] = string.Empty;
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
                ObjTtcibd001.cpcl = dtTtcibd001.Rows[0]["CPCL"].ToString();

                if (ObjTtcibd001.kitm.Trim() == "2")
                {
                    ObjTtcibd001.Error = false;
                    ObjTtcibd001.TypeMsgJs = "console";
                    ObjTtcibd001.SuccessMsg = "Item Encontrado";
                    HttpContext.Current.Session["myItemType"] = ObjTtcibd001.cpcl;
                    HttpContext.Current.Session["ITEM"] = ObjTtcibd001.item;
                    HttpContext.Current.Session["CUNI"] = ObjTtcibd001.cuni;

                }
                else
                {
                    ObjTtcibd001.Error = true;
                    ObjTtcibd001.TypeMsgJs = "label";
                    
                    ObjTtcibd001.ErrorMsg = ItemcodeisnotManufacturingType;

                }
            }
            else
            {
                ObjTtcibd001.Error = true;
                ObjTtcibd001.TypeMsgJs = "label";
                
                ObjTtcibd001.ErrorMsg = Itemcodedoesntexist
;
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
                HttpContext.Current.Session["ITEM"] = Obj_tticol125.item;
                HttpContext.Current.Session["CLOT"] = Obj_tticol125.clot;
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

                HttpContext.Current.Session["CWAR"] = Obj_twhcol016.cwar;
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
                        HttpContext.Current.Session["LOCA"] = LOCA.Trim();
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
        public static string VerificarQuantity(string CWAR, string ITEM, string CLOT, string LOCA, string QTYS)
        {
            QTYS = QTYS.Replace(".", ",");
            decimal factor = 0;
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
                    HttpContext.Current.Session["QTYS"] = QTYS;
                    if (HttpContext.Current.Session["myItemType"].ToString().Trim() != "RET")
                    {
                        DataTable dtFactor = twhcol130DAL.ConsultafactoresporItem(ITEM.Trim());
                        if (dtFactor.Rows.Count > 0)
                        {
                            foreach (DataRow row in dtFactor.Rows)
                            {
                                if (row["UNIT"].ToString().Trim() == "PLT" && row["BASU"].ToString().Trim() == HttpContext.Current.Session["CUNI"].ToString().Trim())
                                {
                                    factor = Convert.ToDecimal(row["FACTOR"].ToString().Trim());
                                }
                            }

                            if (factor > 0)
                            {
                                if (Convert.ToDecimal(QTYS) > factor)
                                {
                                    ObjTtwhinr140.Error = true;
                                    ObjTtwhinr140.ErrorMsg = RegisteredquantitynotavilableonBaaninventory;
                                }
                            }
                            else
                            {
                                ObjTtwhinr140.Error = true;
                                ObjTtwhinr140.ErrorMsg = FactordontexistforthisItem;
                            }
                        }
                        else
                        {
                            ObjTtwhinr140.Error = true;
                            ObjTtwhinr140.ErrorMsg = FactordontexistforthisItem;
                        }
                    }
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

        //[WebMethod]
        //public static string Click_Save(string CWAR, string ITEM, string CLOT, string LOCA, string QTYS, string UNIT)
        //{
        //    RelabelFinishProduct RFP = new RelabelFinishProduct();
        //    string strOrden = "";
        //    string strSecuen = "";
        //    string itemId = "";
        //    string cantidad = "";
        //    string bodega = "";
        //    string usuario = "";
        //    string fecha = "";
        //    string descItem = "";
        //    string unidad = "";
        //    string strMachine = "";
        //    string strTagId = "";

        //    RelabelFinishProduct rfP = new RelabelFinishProduct();
        //    Ent_tticol022 data022;
        //    Ent_tticol042 data042;
        //    string strError = string.Empty;
        //    int consecutivoPalletID = 0;
        //    DataTable DTPalletContinue = twhcol130DAL.PaidMayorwhcol130(CLOT == "" ? ITEM.Trim() : CLOT.Trim());
        //    string SecuenciaPallet = "C001";
        //    if (DTPalletContinue.Rows.Count > 0)
        //    {
        //        foreach (DataRow item in DTPalletContinue.Rows)
        //        {
        //            consecutivoPalletID = Convert.ToInt32(item["T$PAID"].ToString().Trim().Substring(11, 3)) + 1;
        //            if (consecutivoPalletID.ToString().Length == 1)
        //            {
        //                SecuenciaPallet = "C00" + consecutivoPalletID;
        //            }
        //            if (consecutivoPalletID.ToString().Length == 2)
        //            {
        //                SecuenciaPallet = "C0" + consecutivoPalletID;
        //            }
        //            if (consecutivoPalletID.ToString().Length == 3)
        //            {
        //                SecuenciaPallet = consecutivoPalletID.ToString();
        //            }

        //        }

        //    }

        //    if (HttpContext.Current.Session["myItemType"].ToString() == "RET")
        //    {

        //        data022 = new Ent_tticol022()
        //        {
        //            pdno = CLOT == "" ? ITEM : CLOT,
        //            sqnb = (CLOT == "" ? ITEM : CLOT) + "-" + SecuenciaPallet,
        //            proc = 2,
        //            logn = _operator,
        //            mitm = ITEM,
        //            qtdl = 0,
        //            cuni = "CJ",
        //            log1 = "NONE",
        //            qtd1 = 0,
        //            pro1 = 2,
        //            log2 = "NONE",
        //            qtd2 = 0,
        //            pro2 = 2,
        //            loca = " ",
        //            norp = 1,
        //            dele = 2,
        //            logd = "NONE",
        //            refcntd = 0,
        //            refcntu = 0,
        //            drpt = DateTime.Now,
        //            urpt = _operator,
        //            acqt = Convert.ToDecimal(QTYS),
        //            cwaf = CWAR,
        //            cwat = CWAR,
        //            aclo = LOCA
        //        };

        //        HttpContext.Current.Session["strOrden"] = data022.pdno;
        //        HttpContext.Current.Session["strSecuen"] = data022.sqnb;
        //        HttpContext.Current.Session["itemId"] = data022.mitm;
        //        HttpContext.Current.Session["cantidad"] = data022.acqt;
        //        HttpContext.Current.Session["bodega"] = data022.cwaf;
        //        HttpContext.Current.Session["usuario"] = data022.urpt;
        //        HttpContext.Current.Session["fecha"] = data022.drpt;
        //        HttpContext.Current.Session["descItem"] = data022.mitm;
        //        HttpContext.Current.Session["unidad"] = data022.cuni;
        //        HttpContext.Current.Session["strTagId"] = "";

        //        var validateSave = _idaltticol022.insertarRegistroSimple(ref data022, ref strError);
        //        //if (validateSave < 1)
        //        //{
        //        //    lblError.Text = mensajes("errorsave");
        //        //    lblConfirm.Text = string.Empty;

        //        //    return;
        //        //}
        //        //else
        //        //{
        //        var validateSaveTicol222 = _idaltticol022.InsertarRegistroTicol222(ref data022, ref strError);
        //        //if (validateSaveTicol222 < 1)
        //        //{
        //        //    lblError.Text = mensajes("errorsave Ticol222");
        //        //    lblConfirm.Text = string.Empty;

        //        //    return;
        //        //}
        //        //}
        //        //rfP.printTag();
        //        //StringBuilder script = new StringBuilder();
        //        //script.Append("ventanaImp = window.open('../Labels/whInvLabelFinishProduct.aspx', ");
        //        //script.Append("'ventanaImp', 'menubar=0,resizable=0,width=580,height=450');");
        //        //script.Append("ventanaImp.moveTo(30, 0);");
        //        ////script.Append("setTimeout (ventanaImp.close(), 20000);");
        //        //ScriptManager.RegisterStartupScript(this, this.GetType(), "printTag", script.ToString(), true);
        //        strOrden = HttpContext.Current.Session["strOrden"].ToString();
        //        strSecuen = HttpContext.Current.Session["strSecuen"].ToString();
        //        itemId = HttpContext.Current.Session["itemId"].ToString();
        //        cantidad = HttpContext.Current.Session["cantidad"].ToString();
        //        bodega = HttpContext.Current.Session["bodega"].ToString();
        //        usuario = HttpContext.Current.Session["usuario"].ToString();
        //        fecha = HttpContext.Current.Session["fecha"].ToString();
        //        descItem = HttpContext.Current.Session["descItem"].ToString();
        //        unidad = HttpContext.Current.Session["unidad"].ToString();
        //        strMachine = "";
        //        strTagId = HttpContext.Current.Session["strTagId"].ToString();

        //        RFP.cargarEtiqueta();
        //        return JsonConvert.SerializeObject(data022);
        //    }
        //    else
        //    {

        //        data042 = new Ent_tticol042()
        //        {
        //            pdno = CLOT == "" ? ITEM : CLOT,
        //            sqnb = (CLOT == "" ? ITEM : CLOT) + "-" + SecuenciaPallet,
        //            proc = 2,
        //            logn = _operator,
        //            mitm = ITEM,
        //            qtdl = 0,
        //            cuni = "CJ",
        //            log1 = "NONE",
        //            qtd1 = 0,
        //            pro1 = 2,
        //            log2 = "NONE",
        //            qtd2 = 0,
        //            pro2 = 2,
        //            loca = " ",
        //            norp = 1,
        //            dele = 2,
        //            logd = "NONE",
        //            refcntd = 0,
        //            refcntu = 0,
        //            drpt = DateTime.Now,
        //            urpt = _operator,
        //            acqt = Convert.ToDouble(QTYS),
        //            cwaf = CWAR,
        //            cwat = CWAR,
        //            aclo = LOCA
        //        };

        //        HttpContext.Current.Session["strOrden"] = data042.pdno;
        //        HttpContext.Current.Session["strSecuen"] = data042.sqnb;
        //        HttpContext.Current.Session["itemId"] = data042.mitm;
        //        HttpContext.Current.Session["cantidad"] = data042.acqt;
        //        HttpContext.Current.Session["bodega"] = data042.cwaf;
        //        HttpContext.Current.Session["usuario"] = data042.urpt;
        //        HttpContext.Current.Session["fecha"] = data042.drpt;
        //        HttpContext.Current.Session["descItem"] = data042.mitm;
        //        HttpContext.Current.Session["unidad"] = data042.cuni;
        //        HttpContext.Current.Session["strTagId"] = "";

        //        var validateSave = _idaltticol042.insertarRegistroSimple(ref data042, ref strError);

        //        var validateSaveTicol242 = _idaltticol042.InsertarRegistroTicol242(ref data042, ref strError);

        //        strOrden = HttpContext.Current.Session["strOrden"].ToString();
        //        strSecuen = HttpContext.Current.Session["strSecuen"].ToString();
        //        itemId = HttpContext.Current.Session["itemId"].ToString();
        //        cantidad = HttpContext.Current.Session["cantidad"].ToString();
        //        bodega = HttpContext.Current.Session["bodega"].ToString();
        //        usuario = HttpContext.Current.Session["usuario"].ToString();
        //        fecha = HttpContext.Current.Session["fecha"].ToString();
        //        descItem = HttpContext.Current.Session["descItem"].ToString();
        //        unidad = HttpContext.Current.Session["unidad"].ToString();
        //        strMachine = "";
        //        strTagId = HttpContext.Current.Session["strTagId"].ToString();

        //        RFP.cargarEtiqueta();
        //        return JsonConvert.SerializeObject(data042);

        //    }
        //}


        protected void save_Click(object sender, EventArgs e)
        {
            string ITEM = HttpContext.Current.Session["ITEM"].ToString();
            string CWAR = HttpContext.Current.Session["CWAR"].ToString();
            string CLOT = HttpContext.Current.Session["CLOT"].ToString();
            string LOCA = HttpContext.Current.Session["LOCA"].ToString();
            string QTYS = HttpContext.Current.Session["QTYS"].ToString();
            string UNIT = HttpContext.Current.Session["UNIT"].ToString();
            string CUNI = HttpContext.Current.Session["CUNI"].ToString();
            Ent_tticol022 data022;
            Ent_tticol042 data042;
            string strError = string.Empty;
            string SecuenciaPallet = "C001";
            int consecutivo = 0;

            if (HttpContext.Current.Session["myItemType"].ToString().Trim() != "RET")
            {
                string id = CLOT.Trim() == "" ? cyclecountLabel : CLOT.Trim();
                DataTable Dtticol022 = _idaltticol022.SecuenciaMayor(id);
                if (Dtticol022.Rows.Count > 0)
                {
                    if (CLOT.Trim() != string.Empty)
                    {
                        consecutivo = Convert.ToInt32(Dtticol022.Rows[0]["T$SQNB"].ToString().Trim().Substring(11, 3)) + 1;
                    }
                    else
                    {
                        consecutivo = Convert.ToInt32(Dtticol022.Rows[0]["T$SQNB"].ToString().Trim().Substring(11, 3)) + 1;
                    }
                }
                else
                {
                    consecutivo = 0;
                }
            }
            else
            {
                string id = CLOT.Trim() == "" ? ITEM.Trim() : CLOT.Trim();
                DataTable Dtticol042 = _idaltticol042.SecuenciaMayor(id);
                if (Dtticol042.Rows.Count > 0)
                {
                    if (CLOT.Trim() != string.Empty)
                    {
                        consecutivo = Convert.ToInt32(Dtticol042.Rows[0]["T$SQNB"].ToString().Trim().Substring(11, 3)) + 1;
                    }
                    else
                    {
                        consecutivo = Convert.ToInt32(Dtticol042.Rows[0]["T$SQNB"].ToString().Trim().Substring(11, 3)) + 1;
                    }
                }
                else
                {
                    consecutivo = 0;
                }
            }


            if (consecutivo.ToString().Length == 1)
            {
                SecuenciaPallet = "C00" + consecutivo.ToString();
            }
            if (consecutivo.ToString().Length == 2)
            {
                SecuenciaPallet = "C0" + consecutivo.ToString();
            }
            if (consecutivo.ToString().Length == 3)
            {
                SecuenciaPallet = "C0" + consecutivo.ToString();
            }


            if (HttpContext.Current.Session["myItemType"].ToString().Trim() != "RET")
            {

                data022 = new Ent_tticol022()
                {
                    pdno = CLOT == "" ? cyclecountLabel : CLOT,
                    sqnb = (CLOT == "" ? cyclecountLabel : CLOT) + "-" + SecuenciaPallet,
                    proc = 2,
                    logn = _operator,
                    mitm = ITEM.Trim(),
                    qtdl = Convert.ToDecimal(QTYS),
                    cuni = CUNI,
                    log1 = "NONE",
                    qtd1 = Convert.ToInt32(QTYS),
                    pro1 = 2,
                    log2 = "NONE",
                    qtd2 = Convert.ToInt32(QTYS),
                    pro2 = 2,
                    loca = " ",
                    norp = 1,
                    dele = 2,
                    logd = "NONE",
                    refcntd = 0,
                    refcntu = 0,
                    drpt = DateTime.Now,
                    urpt = _operator,
                    acqt = Convert.ToDecimal(QTYS),
                    cwaf = CWAR,
                    cwat = CWAR,
                    aclo = LOCA,
                    allo = 0
                };

                HttpContext.Current.Session["strOrden"] = data022.pdno;
                HttpContext.Current.Session["strSecuen"] = data022.sqnb;
                HttpContext.Current.Session["itemId"] = data022.mitm;
                HttpContext.Current.Session["cantidad"] = data022.acqt;
                HttpContext.Current.Session["bodega"] = data022.cwaf;
                HttpContext.Current.Session["usuario"] = data022.urpt;
                HttpContext.Current.Session["fecha"] = data022.drpt;
                HttpContext.Current.Session["descItem"] = data022.mitm;
                HttpContext.Current.Session["unidad"] = data022.cuni;
                HttpContext.Current.Session["strTagId"] = "";

                var validateSave = _idaltticol022.insertarRegistroSimple(ref data022, ref strError);
                var validateSaveTicol222 = _idaltticol022.InsertarRegistroTicol222(ref data022, ref strError);
                
                //return JsonConvert.SerializeObject(data022);
                if (Convert.ToBoolean(validateSave) && Convert.ToBoolean(validateSaveTicol222))
                {
                    StringBuilder script = new StringBuilder();
                    script.Append("ventanaImp = window.open('../Labels/whInvLabelFinishProduct.aspx', ");
                    script.Append("'ventanaImp', 'menubar=0,resizable=0,width=580,height=450');");
                    script.Append("ventanaImp.moveTo(30, 0);");
                    //script.Append("setTimeout (ventanaImp.close(), 20000);");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "printTag", script.ToString(), true);
                }
            }
            else
            {

                data042 = new Ent_tticol042()
                {
                    pdno = CLOT == "" ? cyclecountLabel : CLOT,
                    sqnb = (CLOT == "" ? cyclecountLabel : CLOT) + "-" + SecuenciaPallet,
                    proc = 2,
                    logn = _operator,
                    mitm = ITEM.Trim(),
                    qtdl = Convert.ToDecimal(QTYS),
                    cuni = CUNI,
                    log1 = "NONE",
                    qtd1 = Convert.ToDecimal(QTYS),
                    pro1 = 2,
                    log2 = "NONE",
                    qtd2 = Convert.ToDecimal(QTYS),
                    pro2 = 2,
                    loca = " ",
                    norp = 1,
                    dele = 2,
                    logd = "NONE",
                    refcntd = 0,
                    refcntu = 0,
                    drpt = DateTime.Now,
                    urpt = _operator,
                    acqt = Convert.ToDouble(QTYS),
                    cwaf = CWAR,
                    cwat = CWAR,
                    aclo = LOCA,
                    allo = 0
                };

                HttpContext.Current.Session["strOrden"] = data042.pdno;
                HttpContext.Current.Session["strSecuen"] = data042.sqnb;
                HttpContext.Current.Session["itemId"] = data042.mitm;
                HttpContext.Current.Session["cantidad"] = data042.acqt;
                HttpContext.Current.Session["bodega"] = data042.cwaf;
                HttpContext.Current.Session["usuario"] = data042.urpt;
                HttpContext.Current.Session["fecha"] = data042.drpt;
                HttpContext.Current.Session["descItem"] = data042.mitm;
                HttpContext.Current.Session["unidad"] = data042.cuni;
                HttpContext.Current.Session["strTagId"] = "";

                var validateSave = _idaltticol042.insertarRegistroSimpleD(ref data042, ref strError);
                var validateSaveTicol242 = _idaltticol042.InsertarRegistroTicol242(ref data042, ref strError);

                //return JsonConvert.SerializeObject(data042);
                if (Convert.ToBoolean(validateSave) && Convert.ToBoolean(validateSaveTicol242))
                {
                    StringBuilder script = new StringBuilder();
                    script.Append("ventanaImp = window.open('../Labels/whInvLabelFinishProduct.aspx', ");
                    script.Append("'ventanaImp', 'menubar=0,resizable=0,width=580,height=450');");
                    script.Append("ventanaImp.moveTo(30, 0);");
                    //script.Append("setTimeout (ventanaImp.close(), 20000);");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "printTag", script.ToString(), true);
                }
            }

        }

        protected static string mensajes(string tipoMensaje)
        {
            string idioma = "INGLES";
            Mensajes _mensajesForm = new Mensajes();
            var retorno = _mensajesForm.readStatement("RelabelFinishProduct.aspx", idioma, ref tipoMensaje);

            if (retorno.Trim() == String.Empty)
            {
                retorno = _mensajesForm.readStatement(globalMessages, idioma, ref tipoMensaje);
            }

            return retorno;
        }

    }
}