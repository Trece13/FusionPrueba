using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using whusa.Interfases;
using System.Data;
using Newtonsoft.Json;
using whusa.Entidades;
using System.Threading;
using System.Globalization;
using System.Configuration;
using whusa.Utilidades;

namespace whusap.WebPages.WorkOrders
{
    public partial class ConsultaBlockedPalletEdit : System.Web.UI.Page
    {
            public static IntefazDAL_tticol082 Itticol082 = new IntefazDAL_tticol082();
            public static IntefazDAL_ttccom110 Ittccom110 = new IntefazDAL_ttccom110();
            public static IntefazDAL_twhinh220 Itwhinh220 = new IntefazDAL_twhinh220();
            public static InterfazDAL_twhcol130 Itwhcol130 = new InterfazDAL_twhcol130();
            private static InterfazDAL_ttccol301 _idalttccol301 = new InterfazDAL_ttccol301();
            string formName = string.Empty;
            public static string _operator = string.Empty;
            string _idioma = string.Empty;
            private static Mensajes _mensajesForm = new Mensajes();
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
                    //CargarIdioma();

                }
            }

            [WebMethod]
            public static string SelectPlant()
            {
                return JsonConvert.SerializeObject(Itticol082.ConsultaPlantTticol082());
            }

            [WebMethod]
            public static string ClickQuery()
            {
                Console.WriteLine("Entro en ClickQuery...");
                string strError = string.Empty;
                DataTable ListaRegistroCustomer = Itticol082.ConsultarRegistrosBloquedos();
                if (strError == string.Empty)
                {
                    return JsonConvert.SerializeObject(ListaRegistroCustomer);
                }
                else
                {
                    return strError;
                }

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

            [WebMethod]
            public static string ClickSave(string LstJson)
            {

                List<Ent_tticol082> lstGuardar = JsonConvert.DeserializeObject<List<Ent_tticol082>>(LstJson);

                foreach (Ent_tticol082 item in lstGuardar)
                {
                    item.LOGN = _operator;
                    actualizarTablas(item);
                }
                

                return "{www:'1221'}";
            }


        public static void actualizarTablas(Ent_tticol082 MyObj){
            bool ActalizacionExitosa = false;
            switch (MyObj.TBL)
                {
                    case "ticol022":
                        ActalizacionExitosa = Itticol082.Actualizartticol022STAT(MyObj);
                        break;
                    case "whcol130":
                        ActalizacionExitosa = Itticol082.Actualizartwhcol130STAT(MyObj);
                        break;
                    case "whcol131":
                        ActalizacionExitosa = Itticol082.Actualizartwhcol131STAT(MyObj);
                        break;
                    case "ticol042":
                        ActalizacionExitosa = Itticol082.Actualizartticol042STAT(MyObj);
                        break;
                }
        }
    }
}