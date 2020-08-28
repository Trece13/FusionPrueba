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

namespace whusap.WebPages.InvReceipts
{
    public partial class PickMaterial : System.Web.UI.Page
    {
        public static InterfazDAL_twhcol130 twhcol130DAL = new InterfazDAL_twhcol130();
        private static InterfazDAL_ttccol301 _idalttccol301 = new InterfazDAL_ttccol301();
        string formName = string.Empty;
        public static string _operator = string.Empty;
        string _idioma = string.Empty;
        private static Mensajes _mensajesForm = new Mensajes();
        private static string globalMessages = "GlobalMessages";


        public static string TheIDpalledPendingToLocateByUser     = string.Empty;
        public static string YouHaveTheIDPalletPendingToLocate    = string.Empty;
        public static string TheIDPalletAlreadyBeenLocated        = string.Empty;
        public static string ThePalletIDStatusisNotReceived      = string.Empty;
        public static string TheIDPalletDoesNotExist              = string.Empty;


        protected void Page_Load(object sender, EventArgs e)
        {
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
                CargarIdioma();

            }
        }

        public class Error {
                public string MyError { get; set; }
            };

        [WebMethod]
        public static string Clic_Pick(string PAID)
        {
            

            try
            {
                string retorno = string.Empty;  
                string USRR = _operator;
                DataTable DTtccol307PalletID = twhcol130DAL.Consultarttccol307(PAID, string.Empty);


                if (DTtccol307PalletID.Rows.Count > 0)
                {
                    Error ReturnError = new Error{
                        MyError = string.Format(TheIDpalledPendingToLocateByUser,DTtccol307PalletID.Rows[0]["PAID"].ToString().Trim(),DTtccol307PalletID.Rows[0]["USRR"].ToString().Trim())
                    };
                    retorno =  JsonConvert.SerializeObject(ReturnError);
                }
                else if (DTtccol307PalletID.Rows.Count == 0)
                {
                    DataTable DTtccol307User = twhcol130DAL.Consultarttccol307(string.Empty, USRR);
                    if (DTtccol307User.Rows.Count > 0)
                    {
                        Error ReturnError = new Error
                        {
                            MyError = string.Format(YouHaveTheIDPalletPendingToLocate,DTtccol307User.Rows[0]["PAID"].ToString().Trim())
                        };
                        retorno = JsonConvert.SerializeObject(ReturnError);

                    }
                    else
                    {

                        List<Ent_twhcol130131> LstPallet = twhcol130DAL.ConsultarPorPalletIDReimpresion(PAID.Trim().ToUpper(), USRR, "");

                        if (LstPallet.Count > 0)
                        {
                            if (LstPallet[0].STAT == "1")
                            {


                                Ent_ttccol307 tccol307 = new Ent_ttccol307
                                {
                                    PAID = PAID,
                                    REFCNTD = 0,
                                    REFCNTU = 0,
                                    STAT = "2",
                                    USRR = USRR,
                                    PROC = "3"
                                };

                                if (LstPallet[0].LOGT.Trim() == string.Empty)
                                {
                                    bool InsertSucces = twhcol130DAL.Insertarttccol307(tccol307);

                                    string PICK = "1";
                                    string DATK = DateTime.Now.ToString("dd/MM/yyyy").ToString();
                                    string LOGP = _operator;
                                    string STAT = "2";

                                    twhcol130DAL.ActualizacionPickMaterialWhcol130(PAID, PICK, DATK, LOGP, STAT);
                                  

                                    retorno = JsonConvert.SerializeObject(LstPallet[0]);
                                }
                                else
                                {
                                    Error ReturnError = new Error
                                    {
                                        MyError = TheIDPalletAlreadyBeenLocated
                                    };

                                    retorno = JsonConvert.SerializeObject(ReturnError);
                                }

                            }
                        else
                            {
                                Error ReturnError = new Error
                                {
                                    MyError = ThePalletIDStatusisNotReceived
                                };

                                retorno = JsonConvert.SerializeObject(ReturnError);
                            }
                        }
                        else
                        {
                            Error ReturnError = new Error
                            {
                                MyError = TheIDPalletDoesNotExist
                            };

                            retorno = JsonConvert.SerializeObject(ReturnError);
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

        protected void CargarIdioma()
        {
            TheIDpalledPendingToLocateByUser = mensajes("TheIDpalledPendingToLocateByUser");
            YouHaveTheIDPalletPendingToLocate = mensajes("YouHaveTheIDPalletPendingToLocate");
            TheIDPalletAlreadyBeenLocated = mensajes("TheIDPalletAlreadyBeenLocated");
            ThePalletIDStatusisNotReceived = mensajes("ThePalletIDStatusisNotReceived");
            TheIDPalletDoesNotExist = mensajes("TheIDPalletDoesNotExist");
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