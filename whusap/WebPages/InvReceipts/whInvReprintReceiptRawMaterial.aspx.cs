using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using whusa.Interfases;
using System.Web.Services;
using Newtonsoft.Json;
using whusap.Entidades;
using whusa;
using System.Globalization;
using System.Threading;
using System.Configuration;
using System.Web.Configuration;
using whusa.Entidades;
using whusa.Utilidades;
namespace whusap.WebPages.InvReceipts
{
    public partial class whInvReprintReceiptRawMaterial : System.Web.UI.Page
    {
        public static InterfazDAL_twhcol130 twhcol130DAL = new InterfazDAL_twhcol130();
        private static InterfazDAL_ttccol301 _idalttccol301 = new InterfazDAL_ttccol301();
        public static string RequestUrlAuthority = string.Empty;
        private static Mensajes _mensajesForm = new Mensajes();

        string formName = string.Empty;
        public static string _operator = string.Empty;
        string _idioma = string.Empty;
        public string UrlBaseBarcode = WebConfigurationManager.AppSettings["UrlBaseBarcode"].ToString();
        private static string globalMessages = "GlobalMessages";

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

            }
        }

        [WebMethod]
        public static string ReprintLabel(string PAID)
        {
            try
            {
                List<Ent_twhcol130131> LstPallet = twhcol130DAL.ConsultarPorPalletIDReimpresion(PAID, _operator, RequestUrlAuthority);
                if (LstPallet.Count == 0)
                {
                    LstPallet = twhcol130DAL.ConsultarPorPalletIDReimpresion131(PAID, _operator, RequestUrlAuthority);
                }
                return JsonConvert.SerializeObject(LstPallet[0]);
            }
            catch (Exception e)
            {
                return mensajesStatic("ThePalletIDdoesnotexist");
            }
        }

        protected static string mensajesStatic(string tipoMensaje)
        {
            Mensajes mensajesForm = new Mensajes();
            string idioma = "INGLES";
            var retorno = mensajesForm.readStatement("whInvReprintReceiptRawMaterial.aspx", idioma, ref tipoMensaje);

            if (retorno.Trim() == String.Empty)
            {
                retorno = mensajesForm.readStatement(globalMessages, idioma, ref tipoMensaje);
            }

            return retorno;
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