using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using whusa.Interfases;
using whusa.Utilidades;
using whusa.Entidades;
using System.Configuration;
using System.Threading;
using System.Globalization;
using System.Data;

namespace whusap.WebPages.Migration
{
    public partial class whInvTimeAbscent : System.Web.UI.Page
    {
        #region Propiedades
            private static InterfazDAL_ttccol301 _idalttccol301 = new InterfazDAL_ttccol301();
            private static InterfazDAL_tticol132 _idaltticol132 = new InterfazDAL_tticol132();
            private static InterfazDAL_ttirou003 _idalttirou003 = new InterfazDAL_ttirou003();
            private static InterfazDAL_tticol077 _idaltticol077 = new InterfazDAL_tticol077();
            private static InterfazDAL_ttihra130 _idalttihra130 = new InterfazDAL_ttihra130();
            private static InterfazDAL_tticol111 _idaltticol111 = new InterfazDAL_tticol111();
            private static InterfazDAL_tticol085 _idaltticol085 = new InterfazDAL_tticol085();
            private static Mensajes _mensajesForm = new Mensajes();
            private static LabelsText _textoLabels = new LabelsText();
            private static string _operator;
            public static string _idioma;
            private static string strError;
            private static string formName;
            private static string globalMessages = "GlobalMessages";
            private static DataTable _recordsTask;
            private static DataTable _recordsLaborType;
            private static DataTable _recordsComments;
            private static DataTable _ordersTurn;
            private static string _dscaMachine;
            private static string _machine;
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
                _machine = txtMachine.Text.Trim().ToUpper();
                var ckot = slType.SelectedValue.Trim();

                var validarMaquina = _idaltticol132.ValidarMaquina(_machine, ref strError);

                if (validarMaquina.Rows.Count > 0)
                {
                    _dscaMachine = validarMaquina.Rows[0]["DSCA"].ToString().Trim().ToUpper();

                    _recordsTask = _idalttirou003.listRecordsByCkot(ref ckot, ref strError);

                    if (_recordsTask.Rows.Count < 0)
                    {
                        lblError.Text = mensajes("notexiststask");
                        lblConfirm.Text = string.Empty;
                        return;
                    }

                    _recordsLaborType = _idalttihra130.listRecords(ref strError);
                    _recordsComments = _idaltticol077.listRecords(ref strError);

                    _ordersTurn = _idaltticol111.findRecordsByMcnoTimeRegistration(ref _machine, ref strError);

                    divBtnGuardar.Visible = true;
                    makeTable();
                    lblError.Text = string.Empty;
                    lblConfirm.Text = string.Empty;
                }
                else 
                {
                    divBtnGuardar.Visible = false;
                    divTable.InnerHtml = String.Empty;
                    lblError.Text = mensajes("notexistsmachine");
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

        protected void btnGuardar_Click(object sender, EventArgs e) 
        {
            var minutes = Math.Round(Convert.ToDecimal(Request.Form["slMinutes"].ToString()) / 60, 2);
            var hours = Convert.ToInt32(Request.Form["slHours"].ToString()) + minutes;
            Ent_tticol085 data085 = new Ent_tticol085() 
            {
                mcno = _machine,
                type = Convert.ToInt32(slType.SelectedValue.Trim()),
                tano = Convert.ToInt32(Request.Form["slTask"].ToString()),
                cwtt = Request.Form["slHoursLaborType"].ToString(),
                hrea = hours,
                comm = Request.Form["slComments"].ToString(),
                logn = _operator
            };

            var validInsert = _idaltticol085.insertarRegistro(ref data085, ref strError);

            if (validInsert > 0)
            {
                divTable.InnerHtml = String.Empty;
                divBtnGuardar.Visible = false;
                txtMachine.Text = String.Empty;
                lblConfirm.Text = mensajes("msjsave");
                lblError.Text = string.Empty;
                return;
            }
            else 
            {
                lblError.Text = mensajes("errorsave");
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
            //Fila machine
            table += String.Format("<hr /><table class='table table-bordered' style='width:800px; font-size:13px; border:3px solid; border-style:outset; text-align:center;'>");

            table += String.Format("<tr style='font-weight:bold;background-color: lightgray;'><td>{0}</td><td colspan='6'>{1}</td></tr>"
                                , _idioma == "INGLES" ? "Machine:" : "Maquina:"
                                , String.Concat(machine, " - ", _dscaMachine));
            //Fila task
            table += String.Format("<tr style='font-weight:bold; background-color: white;'><td>{0}</td><td>{1}</td><td colspan='2'>{2}</td><td colspan='2'>{3}</td><td>{4}</td></tr>"
                                ,_idioma == "INGLES" ? "Task" : "Tarea"
                                , _idioma == "INGLES" ? "Hourly Labor Type" : "Tipo de trabajo por hora"
                                , _idioma == "INGLES" ? "Hours" : "Horas"
                                , _idioma == "INGLES" ? "Minutes" : "Minutos"
                                , _idioma == "INGLES" ? "Comments" : "Comentarios");

            //Fila Selects
            table += "<tr>";
            //--Record Task
            table += String.Format("<td><select id='slTask' name='slTask' class='TextboxBig'><option value=''>{0}</option>"
                ,_idioma == "INGLES" ? "-- Select an option --" : "-- Seleccione una opción --");

            foreach (DataRow itemTaks in _recordsTask.Rows)
            {
                table += String.Format("<option value='{0}'>{1}</option>"
                                    , itemTaks["TANO"].ToString().Trim().ToUpper()
                                    , String.Concat(itemTaks["TANO"].ToString().Trim().ToUpper(), " - ", itemTaks["DSCA"].ToString().Trim().ToUpper()));
            }

            table += "</select></td>";

            //--Record Labor Type
            table += String.Format("<td><select id='slHoursLaborType' name='slHoursLaborType' class='TextboxBig'><option value=''>{0}</option>"
                , _idioma == "INGLES" ? "-- Select an option --" : "-- Seleccione una opción --");

            foreach (DataRow itemType in _recordsLaborType.Rows)
            {
                table += String.Format("<option value='{0}'>{1}</option>"
                                    , itemType["CKOW"].ToString().Trim().ToUpper()
                                    , String.Concat(itemType["CKOW"].ToString().Trim().ToUpper(), " - ", itemType["DSCA"].ToString().Trim().ToUpper()));
            }

            table += "</select></td>";

            //--Record Hours
            table += String.Format("<td colspan='2'><select id='slHours' name='slHours' class='TextboxBig'><option value=''>{0}</option>"
                , _idioma == "INGLES" ? "-- Select an option --" : "-- Seleccione una opción --");

            for (int cont = 0; cont <= 12; cont++ )
            {
                table += String.Format("<option value='{0}'>{1}</option>"
                                    , cont
                                    , cont);
            }

            table += "</select></td>";

            //--Record Minutes
            table += String.Format("<td colspan='2'><select id='slMinutes' name='slMinutes' class='TextboxBig'><option value=''>{0}</option>"
                , _idioma == "INGLES" ? "-- Select an option --" : "-- Seleccione una opción --");

            for (int cont = 0; cont <= 55; cont+=5)
            {
                table += String.Format("<option value='{0}'>{1}</option>"
                                    , cont
                                    , cont);
            }

            table += "</select></td>";


            //--Record Comments
            table += String.Format("<td><select id='slComments' name='slComments' class='TextboxBig'><option value=''>{0}</option>"
                , _idioma == "INGLES" ? "-- Select an option --" : "-- Seleccione una opción --");

            foreach (DataRow itemType in _recordsComments.Rows)
            {
                table += String.Format("<option value='{0}'>{1}</option>"
                                    //, itemType["CODE"].ToString().Trim().ToUpper()
                                    , itemType["CODE"].ToString().ToUpper()
                                    , itemType["COMM"].ToString().Trim().ToUpper());
            }

            table += "</select></td>";

            table += "</tr>";

            //Parte de ordenes
            table += String.Format("<tr style='font-weight:bold;background-color: lightgray;'><td colspan='7'>{0}</td></tr>"
                                    , String.Concat(_idioma == "INGLES" ? "Orders for this turn: " : "Ordenes para este turno: ", _ordersTurn.Rows.Count));

            if (_ordersTurn.Rows.Count > 0)
            {
                //Encabezado
                table += String.Format("<tr style='font-weight:bold;background-color: white;'><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td><td>{6}</td></tr>"
                                        , _idioma == "INGLES" ? "User" : "Usuario"
                                        , _idioma == "INGLES" ? "Order" : "Orden"
                                        , _idioma == "INGLES" ? "Labor Type" : "Tipo de labor"
                                        , _idioma == "INGLES" ? "Description" : "Descripción"
                                        , _idioma == "INGLES" ? "Time" : "Tiempo"
                                        , _idioma == "INGLES" ? "Date" : "Fecha"
                                        , _idioma == "INGLES" ? "Comments" : "Comentarios");

                var horasTotales = 0;
                foreach (DataRow itemOrder in _ordersTurn.Rows)
                {
                    table += String.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td><td>{6}</td></tr>"
                        , itemOrder["LOGN"].ToString().Trim().ToUpper()
                        , itemOrder["PDNO"].ToString().Trim().ToUpper()
                        , itemOrder["CWTT"].ToString().Trim().ToUpper()
                        , itemOrder["DSCA"].ToString().Trim().ToUpper()
                        , itemOrder["HREA"].ToString().Trim().ToUpper()
                        , itemOrder["FECHA"].ToString().Trim().ToUpper()
                        , itemOrder["COMM"].ToString().Trim().ToUpper());

                    horasTotales += Convert.ToInt32(itemOrder["HREA"]);
                    //horasTotales += Convert.ToInt32(itemOrder["HREA"].ToString());
                }

                table += String.Format("<tr style='font-weight:bold;background-color: lightgray;'><td colspan='7'>{0}</td></tr>"
                                    , String.Concat(_idioma == "INGLES" ? "Total Hours: " : "Horas totales ", horasTotales));
            }

            table += "</table>";

            divTable.InnerHtml = table;
        }

        protected void CargarIdioma()
        {
            btnConsultar.Text = _textoLabels.readStatement(formName, _idioma, "btnConsultar");
            lblMachine.Text = _textoLabels.readStatement(formName, _idioma, "lblMachine");
            lblType.Text = _textoLabels.readStatement(formName, _idioma, "lblType");
            btnGuardar.Text = _textoLabels.readStatement(formName, _idioma, "btnGuardar");
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