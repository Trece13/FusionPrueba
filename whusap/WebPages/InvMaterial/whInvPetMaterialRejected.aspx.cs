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


namespace whusap.WebPages.InvMaterial
{
    public partial class whInvPetMaterialRejected : System.Web.UI.Page
    {
        public static string PONO = string.Empty;
        public static string CWAR = string.Empty;
        public static int kltc = 0;
        public static string RequestUrlAuthority = string.Empty;
        //private static LabelsText _textoLabels = new LabelsText();
        string formName = string.Empty;
        public static string _operator = string.Empty;
        string _idioma = string.Empty;
        private static InterfazDAL_tticol080 Tticol080 = new InterfazDAL_tticol080();
        private static InterfazDAL_tticol011 Tticol011 = new InterfazDAL_tticol011();
        private static InterfazDAL_tticst001 ITticst001 = new InterfazDAL_tticst001();
        private static InterfazDAL_ttccol301 _idalttccol301 = new InterfazDAL_ttccol301();
        private static InterfazDAL_twhcol122 Itwhcol122 = new InterfazDAL_twhcol122();
        private static IntefazDAL_tticol082 Itticol082 = new IntefazDAL_tticol082();
        public static InterfazDAL_twhcol130 twhcol130DAL = new InterfazDAL_twhcol130();
        private static Mensajes _mensajesForm = new Mensajes();
        private static string globalMessages = "GlobalMessages";

        public static string WorkOrdernotActive = mensajes("WorkOrdernotActive");
        public static string WorkOrderdoesntexist = mensajes("WorkOrderdoesntexist");
        public static string PalletIDnotavailable = mensajes("PalletIDnotavailable");
        public static string doesnotexistaregisterwithItemassociatedtopalletIDandWorkOrder = mensajes("doesnotexistaregisterwithItemassociatedtopalletIDandWorkOrder");
        public static string PalletIddoesntexist = mensajes("PalletIddoesntexist");
        public static string Itemcodedoesntexist = mensajes("Itemcodedoesntexist");
        public static string Lotcodedoesntexist = mensajes("Lotcodedoesntexist");
        public static string QuantitymustbelowerthanPalletIDquantity = mensajes("QuantitymustbelowerthanPalletIDquantity");
        public static string Updatesuccess = mensajes("Updatesuccess");
        public static string Updatefail = mensajes("Updatefail");
        public static string Insertsuccess = mensajes("Insertsuccess");
        public static string Insertfail = mensajes("Insertfail");


        public static string UrlBaseBarcode = WebConfigurationManager.AppSettings["UrlBaseBarcode"].ToString();
        //item
        //findItem SQL
        public static InterfazDAL_ttcibd001 ITtcibd001 = new InterfazDAL_ttcibd001();

        //lot
        //listaRegistrosLoteItem_Param
        public static InterfazDAL_tticol125 ITticol125 = new InterfazDAL_tticol125();


        //warehouse
        //TakeMaterialInv_verificaBodega_Param
        public static InterfazDAL_ttwhcol016 ITtwhcol016 = new InterfazDAL_ttwhcol016();

        //location
        //listaRegistro_ObtieneAlmacenLocation (si maneja o no localizaciones )
        //whwmd200.sloc = 1
        //transfer
        //ConsultarLocation()
        public static InterfazDAL_twhwmd200 ITwhwmd200 = new InterfazDAL_twhwmd200();
        public static IntefazDAL_transfer Itransfer = new IntefazDAL_transfer();

        //quantity
        //consultaPorAlmacenItemLote
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
        public static string VerificarWorkorder(string PDNO)
        {
            string strError = string.Empty;
            Ent_tticol011 ObjTicol011 = new Ent_tticol011();
            ObjTicol011.pdno = PDNO.ToUpper().Trim();
            ObjTicol011.Acceso = false;


            DataTable dtTicol011 = Tticol011.invLabelRegrind_listaRegistrosOrdenParam(ref ObjTicol011, ref strError);

            if (dtTicol011.Rows.Count > 0)
            {
                DataTable dtTicol011N = Tticol011.invLabel_listaRegistrosOrdenMaquina_Workorder(ref ObjTicol011, ref strError);

                if (dtTicol011N.Rows.Count > 0)
                {
                    HttpContext.Current.Session["PDNO"] = PDNO;
                }
                else
                {
                    ObjTicol011.Error = true;
                    ObjTicol011.TypeMsgJs = "label";
                    
                    ObjTicol011.ErrorMsg = WorkOrdernotActive;
                }
            }
            else
            {
                ObjTicol011.Error = true;
                ObjTicol011.TypeMsgJs = "label";
                
                ObjTicol011.ErrorMsg = WorkOrderdoesntexist;
            }

            return JsonConvert.SerializeObject(ObjTicol011);
        }

        [WebMethod]
        public static string VerificarPalletId(string PAID)
        {
            

            string strError = string.Empty; 
            Ent_tticol125 Obj_tticol125 = new Ent_tticol125();
            Obj_tticol125.paid = PAID;

            DataTable DtTticol125 = ITticol125.vallidatePalletID(ref Obj_tticol125, ref strError);
            if (DtTticol125.Rows.Count > 0)
            {
                
                var myObj = DtTticol125.Rows[0];
                
                Obj_tticol125.pdno = myObj["T$ORNO"].ToString();
                Obj_tticol125.paid = myObj["T$PAID"].ToString();
                Obj_tticol125.cwar = myObj["T$CWAR"].ToString();
                Obj_tticol125.item = myObj["T$ITEM"].ToString();
                Obj_tticol125.clot = myObj["T$CLOT"].ToString();
                Obj_tticol125.dsca = myObj["T$DSCA"].ToString();
                Obj_tticol125.cuni = myObj["T$CUNI"].ToString();
                Obj_tticol125.stat = myObj["T$STAT"].ToString();
                Obj_tticol125.tbl  =  myObj["TBL"].ToString();
                Obj_tticol125.qtyc = myObj["T$QTYC"].ToString();
                Obj_tticol125.Error = false;
                Obj_tticol125.TypeMsgJs = "console";
                Obj_tticol125.SuccessMsg = "Pallet Encontrado";


                string tableName = myObj["TBL"].ToString();
                int status = Convert.ToInt32(myObj["T$STAT"].ToString().ToString());

                if (((tableName == "whcol130") && (status != 3)) || ((tableName == "whcol131") && (status != 3))||((tableName == "ticol022") && (status != 7)) || ((tableName == "ticol042") && (status != 7)))
                {

                    Obj_tticol125.Error = true;
                    Obj_tticol125.TypeMsgJs = "label";
                    
                    Obj_tticol125.ErrorMsg = PalletIDnotavailable;

                    return JsonConvert.SerializeObject(Obj_tticol125);

                }

                HttpContext.Current.Session["PAID"] = PAID.ToString();
                string PDNO = HttpContext.Current.Session["PDNO"].ToString();
                string ITEM = Obj_tticol125.item.ToString();

                HttpContext.Current.Session["QTYC"] = myObj["T$QTYC"].ToString();
                HttpContext.Current.Session["TBL"] = myObj["TBL"].ToString();

                DataTable dtTticst001 = ITticst001.findByItemAndPdno(ref PDNO, ref ITEM, ref strError);
                if (dtTticst001.Rows.Count < 1)
                {
                    Obj_tticol125.Error = true;
                    Obj_tticol125.TypeMsgJs = "label";
                    
                    Obj_tticol125.ErrorMsg = doesnotexistaregisterwithItemassociatedtopalletIDandWorkOrder;
                }

            }
            else
            {
                Obj_tticol125.Error = true;
                Obj_tticol125.TypeMsgJs = "label";
                
                Obj_tticol125.ErrorMsg = PalletIddoesntexist
;

            }

            return JsonConvert.SerializeObject(Obj_tticol125);
        }

        [WebMethod]
        public static string VerificarItem(string PDNO, string ITEM)
        {
            string strError = string.Empty;
            Ent_ttcibd001 ObjTtcibd001 = new Ent_ttcibd001();
            string myPdno = PDNO.ToUpper().Trim();
            string myItem = "         " + ITEM.ToUpper().Trim();

            DataTable dtTticst001 = ITticst001.findByItemAndPdno(ref myPdno, ref myItem, ref strError);
            if (dtTticst001.Rows.Count > 0)
            {
                HttpContext.Current.Session["PONO"] = dtTticst001.Rows[0]["PONO"].ToString();
                HttpContext.Current.Session["CWAR"] = dtTticst001.Rows[0]["CWAR"].ToString();

                DataTable dtTtcibd001 = ITtcibd001.findItem(ITEM);
                if (dtTtcibd001.Rows.Count > 0)
                {
                    kltc = Convert.ToInt32(dtTtcibd001.Rows[0]["KLTC"].ToString());
                    ObjTtcibd001.item = dtTtcibd001.Rows[0]["ITEM"].ToString();
                    ObjTtcibd001.dsca = dtTtcibd001.Rows[0]["DSCA"].ToString();
                    ObjTtcibd001.cuni = dtTtcibd001.Rows[0]["CUNI"].ToString();
                    ObjTtcibd001.kltc = kltc.ToString();
                    ObjTtcibd001.kitm = dtTtcibd001.Rows[0]["KITM"].ToString();
                }
                else
                {
                    ObjTtcibd001.Error = true;
                    ObjTtcibd001.TypeMsgJs = "label";
                    
                    ObjTtcibd001.ErrorMsg = Itemcodedoesntexist
;
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

        public static void actualizarTablas(Ent_tticol080 MyObj)
        {
            bool ActalizacionExitosa = false;
            switch (HttpContext.Current.Session["TBL"].ToString().Trim())
            {
                case "ticol022":
                    Itwhcol122.ActCausalTICOL022(HttpContext.Current.Session["PAID"].ToString().Trim().ToUpper(),11);
                    break;
                case "whcol130":
                    Itwhcol122.ActCausalcol130140(HttpContext.Current.Session["PAID"].ToString().Trim().ToUpper(), 9);

                    break;
                case "whcol131":
                    Itwhcol122.ActCausalcol131140(HttpContext.Current.Session["PAID"].ToString().Trim().ToUpper(), 9);
                    break;
                case "ticol042":
                    Itwhcol122.ActCausalTICOL042(HttpContext.Current.Session["PAID"].ToString().Trim().ToUpper(), 11);
                    break;
            }
        }

        public static void actualizarTablasCant(Ent_tticol082 MyObj)
        {
            bool ActalizacionExitosa = false;
            switch (HttpContext.Current.Session["TBL"].ToString().Trim())
            {
                case "ticol022":
                    Itticol082.Actualizartticol222Cant(MyObj);
                    break;
                case "whcol130":
                    Itticol082.Actualizartwhcol130Cant(MyObj);

                    break;
                case "whcol131":
                    Itticol082.Actualizartwhcol131Cant(MyObj);
                    break;
                case "ticol042":
                    Itticol082.Actualizartticol242Cant(MyObj);
                    break;
            }
        }

        [WebMethod]
        public static string Click_Save(string PDNO, string ITEM, string CLOT, string QTYS, string UNIT)
        {
            
            Ent_tticol080 MyObj = new Ent_tticol080();
            string strError = string.Empty;
            string myPdno = PDNO.ToUpper().Trim();
            string myItem = "         " + ITEM.ToUpper().Trim();

            if (Convert.ToDecimal(HttpContext.Current.Session["QTYC"].ToString().Trim()) < Convert.ToDecimal(QTYS.Trim()))
            {
                MyObj.Error = true;
                MyObj.TypeMsgJs = "alert";
                
                MyObj.ErrorMsg = QuantitymustbelowerthanPalletIDquantity;
                return JsonConvert.SerializeObject(MyObj);
            }

            DataTable dtTticst001 = ITticst001.findByItemAndPdno(ref myPdno, ref myItem, ref strError);
            if (dtTticst001.Rows.Count > 0)
            {
                HttpContext.Current.Session["PONO"] = dtTticst001.Rows[0]["PONO"].ToString();
                HttpContext.Current.Session["CWAR"] = dtTticst001.Rows[0]["CWAR"].ToString();
            }
            else
            {
                MyObj.Error = true;
                MyObj.TypeMsgJs = "alert";
                
                MyObj.ErrorMsg = doesnotexistaregisterwithItemassociatedtopalletIDandWorkOrder;
                return JsonConvert.SerializeObject(MyObj);
            }

            string strTagId = string.Empty;
            List<Ent_tticol080> lstTticol080 = new List<Ent_tticol080>();

            MyObj.orno = PDNO.ToUpper().Trim();
            MyObj.pono = Convert.ToInt32(HttpContext.Current.Session["PONO"].ToString().Trim());// VARIABLE GLOBAL
            MyObj.item = ITEM.ToUpper().Trim();
            MyObj.cwar = HttpContext.Current.Session["CWAR"].ToString().ToUpper();//VARIABLE GLOBAL
            MyObj.qune = Convert.ToDecimal(QTYS);


            MyObj.logn = _operator;
            MyObj.date = "";
            MyObj.proc = 2;
            MyObj.refcntu = 0;
            MyObj.refcntd = 0;
            MyObj.clot = CLOT.ToUpper().Trim() == string.Empty ? " " : CLOT.ToUpper().Trim();
            MyObj.oorg = "4";
            MyObj.pick = 0;

            lstTticol080.Add(MyObj);
            
            string ORNO = MyObj.orno.ToString();
            string PONO = MyObj.pono.ToString();
            string ITEMN = MyObj.item.ToString();

            DataTable dtTticol080 = Tticol080.findRecordByOrnoPonoItem(ref ORNO, ref PONO, ref ITEMN, ref strError);

            if (dtTticol080.Rows.Count > 0)
            {
                int res = Tticol080.actualizarRegistro_Param(ref lstTticol080, ref strError, ref strTagId);
                if (res == 1) {
                    MyObj.Error = false;
                    MyObj.TypeMsgJs = "alert";
                    
                    MyObj.SuccessMsg = Updatesuccess;


                    if ((Convert.ToDecimal(HttpContext.Current.Session["QTYC"].ToString()) - Convert.ToDecimal(QTYS.Trim())) == 0)
                    {
                        actualizarTablas(MyObj);
                        Ent_tticol082 ObjTticol082 = new Ent_tticol082();
                        ObjTticol082.PAID = HttpContext.Current.Session["PAID"].ToString().Trim().ToUpper();
                        ObjTticol082.QTYC = (Convert.ToDecimal(HttpContext.Current.Session["QTYC"].ToString()) - Convert.ToDecimal(QTYS.Trim())).ToString();
                        actualizarTablasCant(ObjTticol082);
                    }
                    else
                    {
                        Ent_tticol082 ObjTticol082 = new Ent_tticol082();
                        ObjTticol082.PAID = HttpContext.Current.Session["PAID"].ToString().Trim().ToUpper();
                        ObjTticol082.QTYC = (Convert.ToDecimal(HttpContext.Current.Session["QTYC"].ToString()) - Convert.ToDecimal(QTYS.Trim())).ToString();
                        actualizarTablasCant(ObjTticol082);
                    }
                    
                }
                else
                {
                    MyObj.Error = true;
                    MyObj.TypeMsgJs = "alert";
                    
                    MyObj.ErrorMsg = Updatefail;
                }
            }
            else
            {
                int res = Tticol080.insertarRegistro(ref lstTticol080, ref strError, ref strTagId);
                if (res == 1)
                {
                    MyObj.Error = false;
                    MyObj.TypeMsgJs = "alert";
                    
                    MyObj.SuccessMsg = Insertsuccess;

                    if ((Convert.ToDecimal(HttpContext.Current.Session["QTYC"].ToString()) - Convert.ToDecimal(QTYS.Trim())) == 0)
                    {
                        actualizarTablas(MyObj);
                        Ent_tticol082 ObjTticol082 = new Ent_tticol082();
                        ObjTticol082.PAID = HttpContext.Current.Session["PAID"].ToString().Trim().ToUpper();
                        ObjTticol082.QTYC = (Convert.ToDecimal(HttpContext.Current.Session["QTYC"].ToString()) - Convert.ToDecimal(QTYS.Trim())).ToString();
                        actualizarTablasCant(ObjTticol082);
                    }
                    else
                    {
                        Ent_tticol082 ObjTticol082 = new Ent_tticol082();
                        ObjTticol082.PAID = HttpContext.Current.Session["PAID"].ToString().Trim().ToUpper();
                        ObjTticol082.QTYC = (Convert.ToDecimal(HttpContext.Current.Session["QTYC"].ToString()) - Convert.ToDecimal(QTYS.Trim())).ToString();
                        actualizarTablasCant(ObjTticol082);
                    }
                }
                else
                {
                    MyObj.Error = true;
                    MyObj.TypeMsgJs = "alert";
                    
                    MyObj.ErrorMsg = Insertfail;
                }
            }
            

            return JsonConvert.SerializeObject(MyObj);

        }

        protected static string mensajes(string tipoMensaje)
        {
            Mensajes mensajesForm = new Mensajes();
            string idioma = "INGLES";
            var retorno = mensajesForm.readStatement("whInvPetMaterialRejected.aspx", idioma, ref tipoMensaje);

            if (retorno.Trim() == String.Empty)
            {
                retorno = mensajesForm.readStatement(globalMessages, idioma, ref tipoMensaje);
            }

            return retorno;
        }
    }
}//
//
//
//