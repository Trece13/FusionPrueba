using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using whusa.Interfases;
using whusa.Utilidades;
using System.Threading;
using System.Globalization;
using System.Configuration;
using whusa.Entidades;
using System.Data;

namespace whusap.WebPages.Migration
{
    public partial class whInvTimeRegistration : System.Web.UI.Page
    {
        #region Propiedades
            private static InterfazDAL_ttccol301 _idalttccol301 = new InterfazDAL_ttccol301();
            private static InterfazDAL_tticol111 _idaltticol111 = new InterfazDAL_tticol111();
            private static Mensajes _mensajesForm = new Mensajes();
            private static LabelsText _textoLabels = new LabelsText();
            private static string _operator;
            public static string _idioma;
            private static string strError;
            private static string formName;
            private static string globalMessages = "GlobalMessages";
            private static DataTable _consultaOrdenesMaquina;
            private static DataTable _consultaMaquina;
        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            // Cambiar cultura para manejo de separador decimal
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
                lblError.Text = "";
                lblConfirm.Text = "";

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

                string strTitulo = mensajes("encabezado");
                control.Text = strTitulo;

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

        protected void btnConsultar_Click(object sender, EventArgs e) 
        {
            if (txtMachine.Text.Trim() != String.Empty)
            {
                var machine = txtMachine.Text.Trim().ToUpper();

                _consultaMaquina = _idaltticol111.findRecordByMcno(ref machine, ref strError);

                if (_consultaMaquina.Rows.Count > 0)
                {
                    _consultaOrdenesMaquina = _idaltticol111.findRecordsByMcnoTimeRegistration(ref machine, ref strError);

                    if (_consultaOrdenesMaquina.Rows.Count > 0)
                    {
                        makeTable();
                        lblConfirm.Text = string.Empty;
                        lblError.Text = string.Empty;
                    }
                    else 
                    {
                        lblError.Text = mensajes("machineorders");
                        lblConfirm.Text = string.Empty;
                        return;
                    }
                }
                else 
                {
                    lblError.Text = mensajes("machinescheduled");
                    lblConfirm.Text = string.Empty;
                    return;
                }
            }
            else 
            {
                lblError.Text = mensajes("formempty");
                lblConfirm.Text = string.Empty;
                return;
            }
        }

        #endregion

        #region Metodos

        protected void makeTable() 
        {
            var table = String.Empty;

            var machine = txtMachine.Text.Trim().ToUpper();
            var dscamachine = _consultaMaquina.Rows[0]["DSCAMCNO"].ToString();

            //Fila machine
            table += String.Format("<hr /><table class='table table-bordered' style='width:800px; font-size:13px; border:3px solid; border-style:outset; text-align:center;'><tr style='font-weight:bold;background-color: lightgray; margin-bottom:300px;'><td>{0}</td><td colspan='6'>{1}</td></tr>"
                                , _idioma == "INGLES" ? "Machine:" : "Maquina:"
                                , String.Concat(machine, " - ", dscamachine));

            //Fila encabezados
            table += String.Format("<tr style='font-weight:bold; background-color: white;'><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td><td>{6}</td></tr>"
                                , _idioma == "INGLES" ? "User" : "Usuario"
                                , _idioma == "INGLES" ? "Order" : "Orden"
                                , _idioma == "INGLES" ? "Labor Type" : "Tipo de trabajo"
                                , _idioma == "INGLES" ? "Description" : "Descripción"
                                , _idioma == "INGLES" ? "Time" : "Tiempo"
                                , _idioma == "INGLES" ? "Date" : "Fecha"
                                , _idioma == "INGLES" ? "Comments" : "Comentarios");

            //Fila valores
            foreach (DataRow item in _consultaOrdenesMaquina.Rows)
            {
                table += String.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td><td>{6}</td></tr>"
                                    , item["LOGN"].ToString().Trim().ToUpper()
                                    , item["PDNO"].ToString().Trim().ToUpper()
                                    , item["CWTT"].ToString().Trim().ToUpper()
                                    , item["DSCA"].ToString().Trim().ToUpper()
                                    , item["HREA"].ToString().Trim().ToUpper()
                                    , item["FECHA"].ToString().Trim().ToUpper()
                                    , item["COMM"].ToString().Trim().ToUpper());
            }

            table += "</table>";

            divTable.InnerHtml = table;
        }

        protected void CargarIdioma()
        {
            btnConsultar.Text = _textoLabels.readStatement(formName, _idioma, "btnConsultar");
            lblMachine.Text = _textoLabels.readStatement(formName, _idioma, "lblMachine");
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

        #endregion
    }
}