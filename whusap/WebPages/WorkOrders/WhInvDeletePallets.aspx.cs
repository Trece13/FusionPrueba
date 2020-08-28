using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using whusa;
using Newtonsoft.Json;
using System.Data;
using System.Web.Services;
using whusa.Interfases;
using System.Globalization;
using System.Threading;
using System.Configuration;
using whusa.Entidades;
using whusa.Utilidades;

namespace whusap.WebPages.WorkOrders
{
    public partial class WhInvDeletePallets : System.Web.UI.Page
    {
        public static InterfazDAL_tticol022 tticol022DAL = new InterfazDAL_tticol022();
        public static InterfazDAL_tticol042 tticol042DAL = new InterfazDAL_tticol042();
        public static InterfazDAL_twhcol130 twhcol130DAL = new InterfazDAL_twhcol130();
        public static InterfazDAL_tticol080 tticol080DAL = new InterfazDAL_tticol080();
        private static InterfazDAL_ttccol301 _idalttccol301 = new InterfazDAL_ttccol301();

        public static string PalletidDoesnexist = mensajes("PalletidDoesnexist");
        public static string RegrindPalletIDStatusdoesntallowdeleteit = mensajes("RegrindPalletIDStatusdoesntallowdeleteit");
        public static string TheIDpalled = mensajes("TheIDpalled");
        public static string pendingtolocatebyuser = mensajes("pendingtolocatebyuser");
        public static string TheIDPalletisConfirmedcantdelete = mensajes("TheIDPalletisConfirmedcantdelete");
        public static string TheIDPalletisalreadymarktoDeleteSuccessfully = mensajes("TheIDPalletisalreadymarktoDeleteSuccessfully");
        public static string TheIDPalletismarktoDeleteSuccessfully = mensajes("TheIDPalletismarktoDeleteSuccessfully");
        public static string Transactionsavedsuccesfully = mensajes("Transactionsavedsuccesfully");

        string formName = string.Empty;
        public static string _operator = string.Empty;
        string _idioma = string.Empty;
        private static Mensajes _mensajesForm = new Mensajes();
        private static string globalMessages = "GlobalMessages";
        public static string strError;
        public static bool active022 = true;
        protected void Page_Load(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-CO");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("es-CO");
            base.InitializeCulture();
            string tipoFormulario = Request.QueryString["tipoFormulario"];
            if (tipoFormulario.Trim() == "pallet")
            {
                active022 = true;
            }
            else
            {
                active022 = false;
            }

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

                string strTitulo = mensajes("encabezado");
                control.Text = strTitulo;

                string strError = string.Empty;
                Ent_ttccol301 data = new Ent_ttccol301()
                {
                    user = _operator,
                    come = strTitulo,
                    refcntd = 0,
                    refcntu = 0
                };

                List<Ent_ttccol301> datalog = new List<Ent_ttccol301>();
                datalog.Add(data);

                _idalttccol301.insertarRegistro(ref datalog, ref strError);
            }
        }

        public class Error
        {
            public string MyError { get; set; }
        };

        [WebMethod]
        public static string Click_Query(string PAID)
        {

            if (active022 == true)
            {
                DataTable consultaOrden = tticol022DAL.ConsultaPorPalletID(ref PAID, ref strError);
                Ent_tticol022 ObjTticol022 = new Ent_tticol022();

                if (consultaOrden.Rows.Count > 0)
                {

                    DataRow MyRow = consultaOrden.Rows[0];
                    ObjTticol022.proc = Convert.ToInt32(MyRow["PROC"].ToString());
                    ObjTticol022.mitm = MyRow["ITEM"].ToString();
                    ObjTticol022.dsca = MyRow["DSCA"].ToString();
                    ObjTticol022.qtdl = Convert.ToDecimal(MyRow["ACQT"].ToString() == String.Empty ? "0" : MyRow["ACQT"].ToString());
                    ObjTticol022.cuni = MyRow["CUNI"].ToString();
                    ObjTticol022.dele = Convert.ToInt32(MyRow["STAT"].ToString());
                    ObjTticol022.Error = false;
                }
                else
                {
                    ObjTticol022.Error = true;
                    ObjTticol022.ErrorMsg = PalletidDoesnexist;
                    ObjTticol022.TypeMsgJs = "alert";
                }
                return JsonConvert.SerializeObject(ObjTticol022);
            }
            else
            {
                DataTable consultaOrden = tticol042DAL.ConsultaPorPalletID(ref PAID, ref strError);
                Ent_tticol042 ObjTticol042 = new Ent_tticol042();

                if (consultaOrden.Rows.Count > 0)
                {

                    DataRow MyRow = consultaOrden.Rows[0];
                    ObjTticol042.proc = Convert.ToInt32(MyRow["PROC"].ToString());
                    ObjTticol042.date = MyRow["DATEA"].ToString();
                    ObjTticol042.mitm = MyRow["ITEM"].ToString();
                    ObjTticol042.dsca = MyRow["DSCA"].ToString();
                    ObjTticol042.qtdl = Convert.ToDecimal(MyRow["ACQT"].ToString() == String.Empty ? "0" : MyRow["ACQT"].ToString());
                    ObjTticol042.cuni = MyRow["CUNI"].ToString();
                    ObjTticol042.dele = Convert.ToInt32(MyRow["STAT"].ToString());
                    ObjTticol042.Error = false;

                    if (ObjTticol042.dele != 2)
                    {
                        ObjTticol042.Error = true;
                        ObjTticol042.ErrorMsg = RegrindPalletIDStatusdoesntallowdeleteit;
                        ObjTticol042.TypeMsgJs = "alert";
                    }
                }
                else
                {
                    ObjTticol042.Error = true;
                    ObjTticol042.ErrorMsg = PalletidDoesnexist;
                    ObjTticol042.TypeMsgJs = "alert";
                }
                return JsonConvert.SerializeObject(ObjTticol042);
            }

        }

        [WebMethod]
        public static string Click_Pick(string PAID)
        {

            try
            {
                var ITEM = string.Empty;
                var DSCA = string.Empty;
                var ACQT = string.Empty;
                var CUNI = string.Empty;
                var STAT = string.Empty;
                var SQNB = string.Empty;
                var PONO = string.Empty;
                var CWAT = string.Empty;
                var PROC = string.Empty;
                var DATE = string.Empty;

                string retorno = string.Empty;
                string USRR = _operator;
                DataTable DTtccol307PalletID = twhcol130DAL.Consultarttccol307(PAID, string.Empty);


                if (DTtccol307PalletID.Rows.Count > 0)
                {
                    Error ReturnError = new Error
                    {
                        MyError = TheIDpalled +" "+ DTtccol307PalletID.Rows[0]["PAID"].ToString().Trim() +" "+ pendingtolocatebyuser+" "+ DTtccol307PalletID.Rows[0]["USRR"].ToString().Trim()
                    };
                    retorno = JsonConvert.SerializeObject(ReturnError);
                }
                else if (DTtccol307PalletID.Rows.Count == 0)
                {
                    //DataTable DTtccol307User = twhcol130DAL.Consultarttccol307(PAID, USRR);
                    //if (DTtccol307User.Rows.Count > 0)
                    //{
                    //    Error ReturnError = new Error
                    //    {
                    //        MyError = "You have the ID pallet: " + DTtccol307User.Rows[0]["PAID"].ToString().Trim() + " Pending to locate"
                    //    };
                    //    retorno = JsonConvert.SerializeObject(ReturnError);

                    //}
                    //else
                    if (true)
                    {
                        if (active022 == true)
                        {
                            DataTable consultaOrden = tticol022DAL.ConsultaPorPalletID(ref PAID, ref strError);
                            PROC = consultaOrden.Rows[0]["PROC"].ToString();
                            ITEM = consultaOrden.Rows[0]["ITEM"].ToString();
                            DSCA = consultaOrden.Rows[0]["DSCA"].ToString();
                            ACQT = consultaOrden.Rows[0]["ACQT"].ToString();
                            CUNI = consultaOrden.Rows[0]["CUNI"].ToString();
                            STAT = consultaOrden.Rows[0]["STAT"].ToString();
                        }
                        else
                        {
                            DataTable consultaOrden = tticol042DAL.ConsultaPorPalletID(ref PAID, ref strError);
                            SQNB = consultaOrden.Rows[0]["SQNB"].ToString();
                            PONO = consultaOrden.Rows[0]["PONO"].ToString();
                            PDNO = consultaOrden.Rows[0]["PDNO"].ToString();
                            CWAT = consultaOrden.Rows[0]["CWAT"].ToString();
                            PROC = consultaOrden.Rows[0]["PROC"].ToString();
                            ITEM = consultaOrden.Rows[0]["ITEM"].ToString();
                            DSCA = consultaOrden.Rows[0]["DSCA"].ToString();
                            ACQT = consultaOrden.Rows[0]["ACQT"].ToString();
                            CUNI = consultaOrden.Rows[0]["CUNI"].ToString();
                            STAT = consultaOrden.Rows[0]["STAT"].ToString();
                            DATE = consultaOrden.Rows[0]["DATEA"].ToString();
                        }
                        if (active022 == true && STAT != "2")
                        {
                            Error ReturnError = new Error
                            {
                                MyError = TheIDPalletisConfirmedcantdelete
                            };
                            retorno = JsonConvert.SerializeObject(ReturnError);
                        }
                        else
                        {
                            if (STAT == "13")
                            {
                                Error ReturnError = new Error
                                {
                                    MyError = TheIDPalletisalreadymarktoDeleteSuccessfully
                                };
                                retorno = JsonConvert.SerializeObject(ReturnError);
                            }
                            else
                            {
                                if (active022 == true)
                                {
                                    tticol022DAL.ActualizacionPalletId(PAID, "13", strError);
                                    Error ReturnError = new Error
                                    {
                                        MyError = TheIDPalletismarktoDeleteSuccessfully
                                    };
                                    retorno = JsonConvert.SerializeObject(ReturnError);
                                }
                                else
                                {

                                    bool stateDelete = tticol042DAL.ActualizacionPalletId(PAID, "13", strError);
                                    if (stateDelete)
                                    {
                                        Ent_tticol080 obj080 = new Ent_tticol080();




                                        obj080.orno = PDNO.Trim();
                                        obj080.pono = Convert.ToInt16(PONO);
                                        obj080.item = ITEM;
                                        obj080.cwar = CWAT;
                                        obj080.qune = Convert.ToDecimal(ACQT);
                                        obj080.logn = _operator;
                                        obj080.date = DATE;
                                        obj080.proc = Convert.ToInt16(PROC);
                                        obj080.refcntd = 0;
                                        obj080.refcntu = 0;
                                        obj080.clot = " ";
                                        obj080.idrecord = "2";
                                        obj080.oorg = "4";
                                        obj080.pick = 0;
                                        
                                        string strTagID = string.Empty;
                                        List<Ent_tticol080> list080 = new List<Ent_tticol080>();
                                        list080.Add(obj080);
                                        tticol080DAL.insertarRegistro(ref list080, ref strError, ref strTagID);
                                    }
                                    Error ReturnError = new Error
                                    {
                                        MyError = Transactionsavedsuccesfully
                                    };
                                    retorno = JsonConvert.SerializeObject(ReturnError);
                                }

                            }
                        }
                    }
                }
                return retorno;

            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }

        [WebMethod]
        public static bool changeState(string state)
        {
            active022 = Convert.ToBoolean(state);
            return active022;
        }
        protected static string mensajes(string tipoMensaje)
        {
            Mensajes mensajesForm = new Mensajes();
            string idioma = "INGLES";
            var retorno = mensajesForm.readStatement("WhInvDeletePallets.aspx", idioma, ref tipoMensaje);

            if (retorno.Trim() == String.Empty)
            {
                retorno = _mensajesForm.readStatement(globalMessages, idioma, ref tipoMensaje);
            }

            return retorno;
        }

        public static string SQNB { get; set; }

        public static string CWAT { get; set; }

        public static string PDNO { get; set; }
    }
}