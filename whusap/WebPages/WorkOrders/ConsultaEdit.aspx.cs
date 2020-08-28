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
    public partial class ConsultaEdit : System.Web.UI.Page
    {
        public static string plantGlobal = string.Empty;
        public static DataTable ListaRegistroCustomer = new DataTable();
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
        public static string SelectWarehouse(string plant)
        {
            return JsonConvert.SerializeObject(Itticol082.ConsultaWarehouseTticol082( plant ));
        }

        [WebMethod]
        public static string SelectMachine(string plant,string warehouse)
        {
            return JsonConvert.SerializeObject(Itticol082.ConsultaMachineTticol082( plant, warehouse));
        }

        [WebMethod]
        public static string ClickQuery(string plant,string warehouse,string machine)
        {
            plantGlobal = plant;
            Console.WriteLine("Entro en ClickQuery...");
            string strError = string.Empty;
            DataTable ListaRegistroCustomer = Itticol082.ConsultarTticol082PorPlant(plant,warehouse,machine);
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
                ListaRegistroCustomer = Itticol082.ConsultarTticol082PorPlantPono(plantGlobal, Convert.ToInt32(item.PRIT), item.ADVS);
                
                if (ListaRegistroCustomer.Rows.Count > 0)
                {
                    foreach (DataRow itemDt in ListaRegistroCustomer.Rows)
                    {
                        Ent_tticol082 obj82 = new Ent_tticol082();
                        obj82.OORG = itemDt["OORG"].ToString();
                        obj82.ORNO = itemDt["ORNO"].ToString();
                        obj82.OSET = itemDt["OSET"].ToString();
                        obj82.PONO = itemDt["PONO"].ToString();
                        obj82.SQNB = itemDt["SQNB"].ToString();
                        obj82.ADVS = itemDt["ADVS"].ToString();
                        obj82.ITEM = itemDt["ITEM"].ToString();
                        obj82.STAT = itemDt["STAT"].ToString();
                        obj82.QTYT = itemDt["QTYT"].ToString();
                        obj82.CWAR = itemDt["CWAR"].ToString();
                        obj82.UNIT = itemDt["UNIT"].ToString();
                        obj82.PRIT = (Convert.ToInt32(itemDt["PRIO"]) + 1).ToString();
                        obj82.TIME = itemDt["TIME"].ToString();
                        Itticol082.ActualizarPrioridadTticol082(obj82);
                        Itticol082.InsertarregistroItticol093(obj82);

                    }
                }

                bool Upt082 = Itticol082.ActualizarPrioridadTticol082(item);
                Itticol082.InsertarregistroItticol093(item);

            }



            return "{www:'1221'}";
        }
    }
}