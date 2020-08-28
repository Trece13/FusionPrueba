using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Threading;
using System.Configuration;
using whusa.Entidades;
using System.Globalization;
using whusa.Interfases;
using whusa.Utilidades;

namespace whusap.WebPages.Migration
{
    public partial class whInvLocations : System.Web.UI.Page
    {
        #region Propiedades
            private static InterfazDAL_ttccol301 _idalttccol301 = new InterfazDAL_ttccol301();
            private static InterfazDAL_twhinh210 _idaltwhinh210 = new InterfazDAL_twhinh210();
            private static InterfazDAL_twhwmd300 _idaltwhwmd300 = new InterfazDAL_twhwmd300();
            private static InterfazDAL_twhcol080 _idaltwhcol080 = new InterfazDAL_twhcol080();
            private static DataTable _consultaRecepcion = new DataTable();
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

                //if (_idioma == "ESPAÑOL")
                //{
                //    IdiomaEspañol();
                //}
                //else
                //{
                //    IdiomaIngles();
                //}

                string strTitulo = mensajes("encabezado");
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

                btnGuardarRecepcion.Visible = false;
            }
        }

        protected void btnConsultarRecepcion_Click(object sender, EventArgs e) 
        {
            var origenMovimiento = ddlOrigenMovimiento.SelectedValue;
            var numeroRecepcion = txtNumeroRecepcion.Text.Trim().ToUpper();

            _consultaRecepcion = _idaltwhinh210.findByNumberReceiptAndOrigin(ref numeroRecepcion, ref origenMovimiento, ref strError);

            if (_consultaRecepcion.Rows.Count > 0)
            {
                lblError.Text = String.Empty;
                lblConfirm.Text = String.Empty;
                tblReceipts.InnerHtml = makeTableReceipt(_consultaRecepcion);
                btnGuardarRecepcion.Visible = true;
                return;
            }
            else 
            {
                lblError.Text = mensajes("recepblank");
                return;
            }
        }

        protected void btnGuardarRecepcion_Click(object sender, EventArgs e) 
        {
            var cantidad = Request.Form["cant-0"];
            var LOCA = Request.Form[String.Concat("ubic-0")];

            var CWAR = _consultaRecepcion.Rows[0]["CWAR"].ToString().ToUpper();

            var validateLocation = _idaltwhwmd300.validateExistsLocation(ref LOCA, ref CWAR, ref strError);

            if (validateLocation.Rows.Count > 0)
            {
                Ent_twhcol080 data = new Ent_twhcol080()
                {
                    sour = Convert.ToInt32(_consultaRecepcion.Rows[0]["OORG"].ToString()),
                    orno = _consultaRecepcion.Rows[0]["ORNO"].ToString(),
                    conj = Convert.ToInt32(_consultaRecepcion.Rows[0]["OSET"].ToString()),
                    pono = Convert.ToInt32(_consultaRecepcion.Rows[0]["PONO"].ToString()),
                    sqnb = Convert.ToInt32(_consultaRecepcion.Rows[0]["SEQN"].ToString()),
                    rcno = " ",
                    cwar = _consultaRecepcion.Rows[0]["CWAR"].ToString(),
                    loca = LOCA,
                    item = _consultaRecepcion.Rows[0]["ITEM"].ToString(),
                    qana =  double.Parse(cantidad, CultureInfo.InvariantCulture.NumberFormat),
                    cuni = _consultaRecepcion.Rows[0]["CUNI"].ToString(),
                    clot = _consultaRecepcion.Rows[0]["CLOT"].ToString(),
                    logn = _operator,
                    orig = 2
                };

                var validateRecord = _idaltwhcol080.findRecord(ref data, ref strError);

                if (validateRecord.Rows.Count > 0)
                {
                    var validateUpdate = _idaltwhcol080.updateRecord(ref data, ref strError);

                    if (validateUpdate)
                    {
                        lblConfirm.Text = mensajes("msjupdate");
                        tblReceipts.InnerHtml = String.Empty;
                        btnGuardarRecepcion.Visible = false;
                        lblError.Text = String.Empty;
                        return;
                    }
                    else 
                    {
                        lblError.Text = mensajes("errorupdt");
                        return;
                    }
                }
                else 
                {
                    var validateInsert = _idaltwhcol080.insertRecord(ref data, ref strError);

                    if (validateInsert > 0)
                    {
                        lblConfirm.Text = mensajes("msjsave");
                        tblReceipts.InnerHtml = String.Empty;
                        btnGuardarRecepcion.Visible = false;
                        lblError.Text = String.Empty;
                        return;
                    }
                    else
                    {
                        lblError.Text = mensajes("errorsave");
                        return;
                    }
                }
            }
            else 
            {
                lblError.Text = mensajes("locinv");
                return;
            }
        }

        #endregion

        #region Metodos

        protected void CargarIdioma()
        {
            lblOrigenMovimiento.Text = _textoLabels.readStatement(formName, _idioma, "lblOrigenMovimiento");
            lblNumeroRecepcion.Text = _textoLabels.readStatement(formName, _idioma, "lblNumeroRecepcion");
            btnConsultarRecepcion.Text = _textoLabels.readStatement(formName, _idioma, "btnConsultarRecepcion");
            btnGuardarRecepcion.Text = _textoLabels.readStatement(formName, _idioma, "btnGuardarRecepcion");

            ddlOrigenMovimiento.Items.Clear();
            ddlOrigenMovimiento.Items.Insert(0, new ListItem("--Seleccione--", ""));
            ddlOrigenMovimiento.Items.Insert(1, new ListItem(_textoLabels.readStatement(formName, _idioma, "ddlPurchases"), "2"));
            ddlOrigenMovimiento.Items.Insert(2, new ListItem(_textoLabels.readStatement(formName, _idioma, "ddlSales"), "1"));
            ddlOrigenMovimiento.Items.Insert(3, new ListItem(_textoLabels.readStatement(formName, _idioma, "ddlManufacture"), "4"));
            ddlOrigenMovimiento.Items.Insert(4, new ListItem(_textoLabels.readStatement(formName, _idioma, "ddlTransfers"), "22"));
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

        protected string makeTableReceipt(DataTable receipts)
        {
            var table = String.Empty;

            table += String.Format("<table class='table table-bordered' style='font-size:13px; border:3px solid; border-style:outset;'><tr style='background-color: darkblue; color: white; font-weight:bold;'><td>{0}</td>"
                + "<td colspan='3'>{1}</td></tr>", _idioma == "ESPAÑOL" ? "Orden: " : "Order: ", receipts.Rows[0]["ORNO"].ToString());

            for (int i = 0; i < receipts.Rows.Count; i++)
            {
                //tr Articulo
                table += String.Format("<tr style='background-color: lightgray;'><td style='font-weight: bold;'>{0}</td><td colspan='3'>{1}</td></tr>"
                    , _idioma == "ESPAÑOL" ? "Articulo: " : "Item: ", receipts.Rows[i]["DSCA"].ToString());

                //tr Encabezados
                table += String.Format("<tr style='font-weight: bold;'><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td></tr>"
                    , _idioma == "ESPAÑOL" ? "Cantidad " : "Quantity "
                    , _idioma == "ESPAÑOL" ? "Unidad " : "Unit "
                    , _idioma == "ESPAÑOL" ? "Ubicación " : "Location"
                    , _idioma == "ESPAÑOL" ? "Lote " : "Warehouse");

                //tr información
                table += String.Format("<tr><td style='width:80px;'>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td></tr>"
                    , String.Format("<input type='number' runat='server' name='{0}' id='{0}' class='Textbox validateReceipt' onchange='validateQuantity(this,{1},{2},{3})' />"
                    , "cant-" + i, receipts.Rows[i]["ASTK"].ToString(), receipts.Rows[i]["QADV"].ToString(),i)
                    , receipts.Rows[i]["CUNI"]
                    , String.Format("<input type='text' runat='server' name='{0}' id='{0}' class='Textbox validateReceipt' />", "ubic-" + i)
                    ,  receipts.Rows[i]["CLOT"]);
            }

            table += "</table>";

            return table;
        }

        #endregion
    }
}