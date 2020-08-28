using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.Globalization;
using whusa.Entidades;
using whusa.Interfases;
using System.Data;
using System.Configuration;
using whusa.Utilidades;

namespace whusap.WebPages.Migration
{
    public partial class whInvReceipts : System.Web.UI.Page
    {
        #region Propiedades
            private static InterfazDAL_ttccol301 _idalttccol301 = new InterfazDAL_ttccol301();
            private static InterfazDAL_twhinh210 _idaltwhinh210 = new InterfazDAL_twhinh210();
            private static InterfazDAL_twhcol080 _idaltwhcol080 = new InterfazDAL_twhcol080();
            private static DataTable findReceipt;
            private static Mensajes _mensajesForm = new Mensajes();
            private static LabelsText _textoLabels = new LabelsText();
            public static int _countReceipts;
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
                lblError.Text = String.Empty;
                lblConfirm.Text = String.Empty;

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
            if (txtNumeroRecepcion.Text != "")
            {
                var numberReceipt = txtNumeroRecepcion.Text.Trim();

                findReceipt = _idaltwhinh210.findByNumberReceipt(ref numberReceipt, ref strError);
                _countReceipts = findReceipt.Rows.Count;

                if (findReceipt.Rows.Count > 0)
                {
                    lblError.Text = String.Empty;
                    lblConfirm.Text = String.Empty;
                    tblReceipts.InnerHtml = makeTableReceipt(findReceipt);
                    btnGuardarRecepcion.Visible = true;

                    return;
                }
                else 
                {
                    lblError.Text = mensajes("findreceiptblank");
                    return;
                }
            }
            else 
            {
                lblError.Text = mensajes("txtrecepcionblank");
                return;
            }
        }

        protected void btnGuardarRecepcion_Click(object sender, EventArgs e) 
        {
            lblError.Text = "";

            for (int i = 0; i < findReceipt.Rows.Count; i++)
            {
                var cantidad = Request.Form[String.Concat("cant-", i)].Trim().ToUpper();
                var cantident = Request.Form[String.Concat("cantident-", i)].Trim().ToUpper();
                var clot = Request.Form[String.Concat("bodega-", i)].Trim().ToUpper();

                Ent_twhcol080 data = new Ent_twhcol080()
                {
                    sour = Convert.ToInt32(findReceipt.Rows[i]["OORG"].ToString()),
                    orno = findReceipt.Rows[i]["ORNO"].ToString(),
                    conj = Convert.ToInt32(findReceipt.Rows[i]["OSET"].ToString()),
                    pono = Convert.ToInt32(findReceipt.Rows[i]["PONO"].ToString()),
                    sqnb = Convert.ToInt32(findReceipt.Rows[i]["SEQN"].ToString()),
                    rcno = findReceipt.Rows[i]["RCNO"].ToString(),
                    item = findReceipt.Rows[i]["ITEM"].ToString(),
                    qana = double.Parse(cantidad, CultureInfo.InvariantCulture.NumberFormat),
                    cuni = findReceipt.Rows[i]["RCUN"].ToString(),
                    clot = clot,
                    logn = _operator,
                    habl = double.Parse(cantident, CultureInfo.InvariantCulture.NumberFormat)
                };

                var findRecord = _idaltwhcol080.findRecord(ref data, ref strError);

                if (findRecord.Rows.Count > 0)
                {
                    var updateRecord = _idaltwhcol080.updateRecord(ref data, ref strError);

                    if (!updateRecord)
                    {
                        lblConfirm.Text = String.Empty;
                        lblError.Text = mensajes("errorupdt");
                        return;
                    }
                }
                else 
                {
                    var insertRecord = _idaltwhcol080.insertRecord(ref data, ref strError);

                    if (insertRecord < 1)
                    {
                        lblConfirm.Text = String.Empty;
                        lblError.Text = mensajes("errorsave");
                        return;
                    }
                }
            }

            lblConfirm.Text = mensajes("msjsave");
            tblReceipts.InnerHtml = String.Empty;
            btnGuardarRecepcion.Visible = false;
            txtNumeroRecepcion.Text = String.Empty;
            return;
        }

        #endregion

        #region Metodos

        protected void CargarIdioma()
        {
            lblNumeroRecepcion.Text = _textoLabels.readStatement(formName, _idioma, "lblNumeroRecepcion");
            btnConsultarRecepcion.Text = _textoLabels.readStatement(formName, _idioma, "btnConsultarRecepcion");
            btnGuardarRecepcion.Text = _textoLabels.readStatement(formName, _idioma, "btnGuardarRecepcion");
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

            table += String.Format("<table class='table table-bordered' style='font-size:13px;'><tr style='background-color: darkblue; color: white; font-weight:bold;'><td>{0}</td>"
                + "<td colspan='3'>{1}</td></tr>", _idioma == "ESPAÑOL" ? "Recepción: " : "Receipt: ", receipts.Rows[0]["RCNO"].ToString() );

            for (int i = 0; i < receipts.Rows.Count; i++)
            {
                //tr Orden
                table += String.Format("<tr style='background-color: lightgray;'><td style='font-weight: bold;'>{0}</td><td colspan='3'>{1}</td></tr>"
                    , _idioma == "ESPAÑOL" ? "Orden: " : "Order: ", receipts.Rows[i]["ORNO"].ToString());

                //tr Articulo
                table += String.Format("<tr style='background-color: white;'><td style='font-weight: bold;'>{0}</td><td colspan='3'>{1}</td></tr>"
                    , _idioma == "ESPAÑOL" ? "Articulo: " : "Item: ", String.Concat(receipts.Rows[i]["ITEM"].ToString()," - ",receipts.Rows[i]["DSCA"].ToString()));

                //tr Encabezados
                table += String.Format("<tr style='font-weight: bold;'><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td></tr>"
                    , _idioma == "ESPAÑOL" ? "Cantidad " : "Quantity "
                    , _idioma == "ESPAÑOL" ? "Unidad " : "Unit "
                    , _idioma == "ESPAÑOL" ? "Lote " : "Lot "
                    , _idioma == "ESPAÑOL" ? "Cant. Identificadores " : "Quant. identifiers ");

                //tr información
                table += String.Format("<tr><td style='width:80px;'>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td></tr>"
                    , String.Format("<input type='number' runat='server' name='{0}' id='{0}' class='Textbox validateReceipt' step='any' onchange='checklot({1},{2}, this)' />", "cant-" + i,receipts.Rows[i]["KLTC"],i)
                    , receipts.Rows[i]["RCUN"]
                    , String.Format("<input type='text' runat='server' name='{0}' id='{0}' value='{1}' class='Textbox' disabled />", "bodega-" + i , receipts.Rows[i]["CLOT"])
                    , String.Format("<input type='number' runat='server' name='{0}' id='{0}' step='any' class='Textbox validateReceipt' />", "cantident-" + i));
            }

            table += "</table>";

            return table;
        }

        #endregion
    }
}
