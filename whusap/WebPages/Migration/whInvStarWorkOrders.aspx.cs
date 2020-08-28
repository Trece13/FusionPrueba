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

namespace whusap.WebPages.Migration
{
    public enum enumStatus
    {
        Scheduled = 1,
        Initiated = 2,
        Finished = 3,
        OnHold = 4
    }

    public partial class whInvStarWorkOrders : System.Web.UI.Page
    {
        #region Propiedades
        private static InterfazDAL_tticol011 _idaltticol011 = new InterfazDAL_tticol011();
        private static InterfazDAL_ttccol301 _idalttccol301 = new InterfazDAL_ttccol301();
        private static Mensajes _mensajesForm = new Mensajes();
        private static LabelsText _textoLabels = new LabelsText();
        private static string _operator;
        public static string _idioma;
        private static string strError;
        private static string formName;
        private static string globalMessages = "GlobalMessages";
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
            lblError.Text = String.Empty;
            lblConfirm.Text = String.Empty;
            if (txtWorkOrder.Text.Trim().Length == 9)
            {
                if (txtWorkOrder.Text.Trim() != String.Empty)
                {
                    var pdno = txtWorkOrder.Text.Trim().ToUpper();
                    var status = "1,2,4";
                    var validaOrden = _idaltticol011.findByPdnoAndInStatus(ref pdno, ref status, ref strError);

                    if (validaOrden.Rows.Count > 0)
                    {
                        var machine = validaOrden.Rows[0]["MCNO"].ToString().Trim();
                        status = "2";

                        var consultaMaquina = _idaltticol011.countByMachineStatusAndDiferentPdno(ref pdno, ref status, ref machine, ref strError);

                        if (consultaMaquina > 0)
                        {
                            lblError.Text = String.Format(mensajes("orderinitiated"), machine);
                            return;
                        }
                        else
                        {
                            slAction.Items.Clear();
                            //ListItem itemSelect = new ListItem() { Text = _idioma == "INGLES" ? "--Select an action--" : "--Seleccione una acción--", Value = "" };
                            //ListItem itemInitiated = new ListItem() { Text = _idioma == "INGLES" ? "Initiated" : "Iniciado", Value = "2" };
                            //ListItem itemFinish = new ListItem() { Text = _idioma == "INGLES" ? "Finish" : "Finalizado", Value = "3" };
                            //ListItem itemHold = new ListItem() { Text = _idioma == "INGLES" ? "On Hold" : "En espera", Value = "4" };

                            ListItem itemSelect = new ListItem() { Text = _idioma == "INGLES" ? "--Select an action--" : "--Seleccione una acción--", Value = "" };
                            ListItem itemInitiated = new ListItem() { Text = _idioma == "INGLES" ? "Initiated" : "Iniciado", Value = "2" };
                            //ListItem itemFinish = new ListItem() { Text = _idioma == "INGLES" ? "Finish" : "Finalizado", Value = "3" };
                            ListItem itemHold = new ListItem() { Text = _idioma == "INGLES" ? "On Hold" : "En espera", Value = "4" };

                            var stat = (enumStatus)Convert.ToInt32(validaOrden.Rows[0]["STAT"].ToString().Trim());

                            slAction.Items.Insert(0, itemSelect);

                            switch (stat)//1 - Scheduled, 2 - Initiated, 4 - On hold
                            {
                                case enumStatus.Initiated:
                                    //slAction.Items.Insert(slAction.Items.Count, itemFinish);
                                    slAction.Items.Insert(slAction.Items.Count, itemHold);
                                    break;
                                case enumStatus.Scheduled:
                                    slAction.Items.Insert(slAction.Items.Count, itemInitiated);
                                    break;
                                case enumStatus.OnHold:
                                    slAction.Items.Insert(slAction.Items.Count, itemInitiated);
                                    break;
                            }

                            lblValueOrder.Text = txtWorkOrder.Text.Trim().ToUpper();
                            lblValueMachine.Text = machine;
                            lblValueStatus.Text = stat == enumStatus.Initiated
                                ? _idioma == "INGLES" ? "Initiated" : "Iniciado"
                                : stat == enumStatus.OnHold
                                ? _idioma == "INGLES" ? "On Hold" : "En espera"
                                : stat == enumStatus.Finished
                                ? _idioma == "INGLES" ? "Finish" : "Finalizado"
                                : stat == enumStatus.Scheduled
                                ? _idioma == "INGLES" ? "Scheduled" : "Programado" : "";

                            divTable.Visible = true;
                        }
                    }
                    else
                    {
                        divTable.Visible = false;
                        lblError.Text = mensajes("ordernotexists");
                        return;
                    }
                }
                else
                {
                    lblError.Text = mensajes("formempty");
                    return;
                }
            }
            else
            {
                divTable.Visible = false;
                lblError.Text = mensajes("OrderLength");
                return;
            }
        }

        protected void btnSaveOrder_Click(object sender, EventArgs e)
        {
            if (slAction.SelectedValue != String.Empty)
            {
                var stat = (enumStatus)Convert.ToInt32(slAction.SelectedValue);

                var updateStatus = false;

                Ent_tticol011 data011 = new Ent_tticol011()
                {
                    stat = Convert.ToInt32(slAction.SelectedValue),
                    inib = _operator,
                    pdno = lblValueOrder.Text.Trim().ToUpper(),
                    mcno = lblValueMachine.Text.Trim().ToUpper(),
                    comb = _operator
                };

                switch (stat)
                {
                    case enumStatus.Initiated:
                        updateStatus = _idaltticol011.updateStatusInitiated(ref data011, ref strError);
                        break;
                    case enumStatus.Finished:
                        updateStatus = _idaltticol011.updateStatusFinish(ref data011, ref strError);
                        break;
                    case enumStatus.OnHold:
                        updateStatus = _idaltticol011.updateStatusOnHold(ref data011, ref strError);
                        break;
                }

                if (updateStatus)
                {
                    lblError.Text = String.Empty;
                    lblConfirm.Text = mensajes("msjupdate");
                    txtWorkOrder.Text = String.Empty;
                    divTable.Visible = false;
                }
                else
                {
                    lblError.Text = mensajes("errorupdt");
                    return;
                }
            }
            else
            {
                lblError.Text = mensajes("formempty");
                return;
            }
        }

        #endregion

        #region Metodos

        protected void CargarIdioma()
        {
            lblWorkOrder.Text = _textoLabels.readStatement(formName, _idioma, "lblWorkOrder");
            btnConsultar.Text = _textoLabels.readStatement(formName, _idioma, "btnConsultar");
            lblOrder.Text = _textoLabels.readStatement(formName, _idioma, "lblOrder");
            lblMachine.Text = _textoLabels.readStatement(formName, _idioma, "lblMachine");
            lblStatus.Text = _textoLabels.readStatement(formName, _idioma, "lblStatus");
            lblAction.Text = _textoLabels.readStatement(formName, _idioma, "lblAction");
            btnSaveOrder.Text = _textoLabels.readStatement(formName, _idioma, "btnSaveOrder");
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