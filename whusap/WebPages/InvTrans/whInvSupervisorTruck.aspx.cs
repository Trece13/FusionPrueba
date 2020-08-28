using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Threading;
using System.Globalization;
using whusa.Interfases;
using System.Data;
using System.Configuration;
using whusa.Entidades;
using whusa.Utilidades;

namespace whusap.WebPages.InvTrans
{
    public partial class whInvSupervisorTruck : System.Web.UI.Page
    {
        #region Propiedades
            string strError = string.Empty;
            private static InterfazDAL_twhcol120 _idaltwhcol120 = new InterfazDAL_twhcol120();
            private static InterfazDAL_twhcol122 _idaltwhcol122 = new InterfazDAL_twhcol122();
            private static InterfazDAL_twhcol123 _idaltwhcol123 = new InterfazDAL_twhcol123();
            private static InterfazDAL_twhcol124 _idaltwhcol124 = new InterfazDAL_twhcol124();
            private static InterfazDAL_ttccol301 _idalttccol301 = new InterfazDAL_ttccol301();
            private static Mensajes _mensajesForm = new Mensajes();
            private static LabelsText _textoLabels = new LabelsText();
            private static string tipoFormulario;
            private static string _idioma;
            private static string _operator;
            public static string _numberOfPallets = "0";
            public static string _numberOfPalletsenviados = "0";
            public static string _numberOfPalletsenviados123 = "0";
            public static int _resultado = 0;
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

                    tipoFormulario = Request.QueryString["tipoFormulario"];
                    tipoFormulario = tipoFormulario.ToUpper();
                    string strTitulo = "";
                    lblError.Text = String.Empty;
                    lblConfirm.Text = String.Empty;
                    dvDataTruck.Visible = false;

                    if (Session["user"] == null)
                    {
                        if (Request.QueryString["Valor1"] == null || Request.QueryString["Valor1"] == "")
                        {
                            Response.Redirect(ConfigurationManager.AppSettings["UrlBase"] + "/WebPages/Login/whLogIni.aspx");
                        }
                        else
                        {
                            _operator = Request.QueryString["Valor1"];
                            Session["user"] = _operator;
                            Session["logok"] = "OKYes";
                        }
                    }
                    else
                    {
                        _operator = Session["user"].ToString();
                    }
                    //Session["user"] = "JCASTRO";
                    //Session["username"] = "JAVIER CASTRO";

                    //_operator = Session["user"].ToString();

                    if (Session["ddlIdioma"] == null)
                    {
                        Session["ddlIdioma"] = "INGLES";
                    }

                    _idioma = Session["ddlIdioma"].ToString();

                    CargarIdioma();

                    //if (_idioma == "ESPAÑOL")
                    //{
                    //    IdiomaEspañol();
                    //}
                    //else
                    //{
                    //    IdiomaIngles();
                    //}


                    if (tipoFormulario != null)
                    {
                        switch (tipoFormulario)
                        {
                            case "PICKING":
                                btnEndPicking.Visible = true;
                                btnEndReceipt.Visible = false;
                                break;
                            case "RECEIVE":
                                btnEndPicking.Visible = false;
                                btnEndReceipt.Visible = true;
                                break;
                            default:
                                btnEndPicking.Visible = true;
                                btnEndReceipt.Visible = false;
                                tipoFormulario = "PICKING";
                                break;
                        }
                    }

                    Label control = (Label)Page.Controls[0].FindControl("lblPageTitle");
                    strTitulo = tipoFormulario == "PICKING" ? mensajes("encabezadopicking") : mensajes("encabezadoreceive");
                    control.Text = strTitulo;

                    Ent_ttccol301 data = new Ent_ttccol301()
                    {
                        user = _operator,
                        come = mensajes("encabezado"),
                        refcntd = 0,
                        refcntu = 0
                    };

                    List<Ent_ttccol301> datalog = new List<Ent_ttccol301>();
                    datalog.Add(data);

                    _idalttccol301.insertarRegistro(ref datalog, ref strError);
                }
            }

            protected void txtUniqueID_OnTextChanged(object sender, EventArgs e)
            {
                lblError.Text = String.Empty;
                lblConfirm.Text = String.Empty;

                if (txtUniqueID.Text != "")
                {
                    var UNIQUEID = txtUniqueID.Text.Trim().ToUpper();

                    //Validate that exists in twhcol120
                    var validationUI = _idaltwhcol120.validarRegistroByUniqueId(ref UNIQUEID, ref strError);

                    if (validationUI.Rows.Count > 0)
                    {
                        var tipoFormulario = Request.QueryString["tipoFormulario"];
                        tipoFormulario = tipoFormulario.ToUpper();

                        if (tipoFormulario == "RECEIVE")
                        {
                            var validationEndReceive = validationUI.Rows[0]["ENDRECEIVE"].ToString() == "2" ? true : false;

                            if (!validationEndReceive)
                            {
                                dvDataTruck.Visible = false;
                                lblError.Text = mensajes("received");
                                return;
                            }

                            dvDataTruck.Visible = true;
                            txtUniqueID.Enabled = false;
                            bMessage.Visible = false;

                            var listPallets = new DataTable();
                            var listPalletsenviados = new DataTable();

                            //Enviar una advertencia cuando no se reciba la misma cantidad que se cargo al trailer
                            //var validatepalletesreceived = new DataTable();
                            //validatepalletesreceived = _idaltwhcol124.validarPalletsRecibidosByUniqueId(ref UNIQUEID, ref strError);
                            listPallets = _idaltwhcol124.validarRegistroByUniqueId(ref UNIQUEID, ref strError);
                            _numberOfPallets = listPallets.Rows.Count.ToString();

                            listPalletsenviados = _idaltwhcol122.validarRegistroByUniqueId(ref UNIQUEID, ref strError);
                            _numberOfPalletsenviados = listPalletsenviados.Rows.Count.ToString();
                            listPalletsenviados = _idaltwhcol123.validarRegistroByUniqueId(ref UNIQUEID, ref strError);
                            _numberOfPalletsenviados123 = listPalletsenviados.Rows.Count.ToString();

                            _resultado = Convert.ToInt32(_numberOfPalletsenviados123) + Convert.ToInt32(_numberOfPalletsenviados);

                            grdPalletsUID.DataSource = listPallets;
                            grdPalletsUID.DataBind();

                            grdPalletsUID.Columns[4].Visible = false;

                            if (Convert.ToInt32(_resultado) != Convert.ToInt32(_numberOfPallets))
                            {
                                lblError.Text = String.Format(mensajes("difference"), _resultado, _numberOfPallets);
                                return;
                            }
                            lblError.Text = "";
                        }
                        else
                        {
                            var validationEndPicking = validationUI.Rows[0]["ENDPICKING"].ToString() == "2" ? true : false;

                            if (!validationEndPicking)
                            {
                                dvDataTruck.Visible = false;
                                lblError.Text = mensajes("confirmed");
                                return;
                            }

                            var listPallets = new DataTable();
                            listPallets = _idaltwhcol122.validarRegistroByUniqueId(ref UNIQUEID, ref strError);

                            _numberOfPallets = listPallets.Rows.Count.ToString();
                            grdPalletsUID.DataSource = listPallets;
                            grdPalletsUID.DataBind();
                            grdPalletsUID.Columns[4].Visible = true;

                            dvDataTruck.Visible = true;
                            txtUniqueID.Enabled = false;
                            bMessage.Visible = true;

                            lblError.Text = "";
                        }
                    }
                    else
                    {
                        dvDataTruck.Visible = false;
                        lblError.Text = mensajes("notexists");
                        return;
                    }
                }
                else
                {
                    lblError.Text = mensajes("formempty");
                    return;
                }
            }

            protected void btnEndPicking_Click(object sender, EventArgs e)
            {
                lblError.Text = String.Empty;
                lblConfirm.Text = String.Empty;

                var uniqueId = txtUniqueID.Text.Trim().ToUpper();

                var validateRegistry = _idaltwhcol122.validarRegistroByUniqueId(ref uniqueId, ref strError);

                if (validateRegistry.Rows.Count > 0)
                {
                    var responseUpdate = _idaltwhcol120.updateEndPicking(ref uniqueId, ref strError);

                    if (responseUpdate)
                    {
                        lblConfirm.Text = String.Format(mensajes("loaded"), _numberOfPallets);
                    }
                    else
                    {
                        lblError.Text = mensajes("errorupdt");
                    }
                }
                else
                {
                    lblError.Text = mensajes("IDBlank");
                    return;
                }
            }

            protected void btnEndReceive_Click(object sender, EventArgs e)
            {
                lblError.Text = String.Empty;
                lblConfirm.Text = String.Empty;

                var uniqueId = txtUniqueID.Text.Trim().ToUpper();

                var validateRegistry = _idaltwhcol120.validateEndPicking(ref uniqueId, ref strError);

                if (validateRegistry.Rows.Count > 0)
                {
                    var responseUpdate = _idaltwhcol120.updateEndReceive(ref uniqueId, ref strError);

                    if (responseUpdate)
                    {
                        lblConfirm.Text = String.Format(mensajes("receivednumber"), _numberOfPallets);
                    }
                    else
                    {
                        lblError.Text = mensajes("errorupdt");
                    }
                }
                else
                {
                    lblError.Text = mensajes("notpicking");
                    return;
                }
            }

        #endregion

        #region Metodos

        protected void CargarIdioma()
            {
                lblNameForm.Text = _textoLabels.readStatement(formName, _idioma, "lblNameForm");
                lblScan.Text = _textoLabels.readStatement(formName, _idioma, "lblScan");
                btnEndPicking.Text = _textoLabels.readStatement(formName, _idioma, "btnEndPicking");
                btnEndReceipt.Text = _textoLabels.readStatement(formName, _idioma, "btnEndReceipt");
                grdPalletsUID.Columns[0].HeaderText = _textoLabels.readStatement(formName, _idioma, "grdPallet");
                grdPalletsUID.Columns[1].HeaderText = _textoLabels.readStatement(formName, _idioma, "grdItem");
                grdPalletsUID.Columns[2].HeaderText = _textoLabels.readStatement(formName, _idioma, "grdLot");
                grdPalletsUID.Columns[3].HeaderText = _textoLabels.readStatement(formName, _idioma, "grdQuantity");
                grdPalletsUID.Columns[4].HeaderText = _textoLabels.readStatement(formName, _idioma, "grdLocation");
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
