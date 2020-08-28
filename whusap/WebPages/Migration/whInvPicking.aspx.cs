using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.Globalization;
using whusa.Interfases;
using System.Configuration;
using whusa.Entidades;
using System.Data;
using whusa.Utilidades;

namespace whusap.WebPages.Migration
{
    public partial class whInvPicking : System.Web.UI.Page
    {
        #region Propiedades
            private static InterfazDAL_ttccol301 _idalttccol301 = new InterfazDAL_ttccol301();
            private static InterfazDAL_twhcol080 _idaltwhcol080 = new InterfazDAL_twhcol080();
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
                divTable.Visible = false;
                btnGuardarRecepcion.Visible = false;
            }
        }

        protected void btnConsultarRecepcion_Click(object sender, EventArgs e) 
        {
            if (txtNumeroRecepcion.Text.Trim() != "")
            {
                var orderNumber = txtNumeroRecepcion.Text.Trim();

                var findRecord = _idaltwhcol080.findRecordByProc(ref orderNumber, ref strError);

                if (findRecord.Rows.Count > 0)
                {
                    divTable.InnerHtml = makeTableReceipt(findRecord);
                    divTable.Visible = true;
                    btnGuardarRecepcion.Visible = true;
                    return;
                }
                else 
                {
                    divTable.Visible = false;
                    btnGuardarRecepcion.Visible = false;
                    lblError.Text = mensajes("orderempty");
                    return;
                }
            }
            else 
            {
                lblError.Text = mensajes("formempty");
                return;
            }
        }

        protected void btnGuardarRecepcion_Click(object sender, EventArgs e) 
        {
            var quant = Request.Form["cant-0"].ToString();
            var quantConvert = double.Parse(quant, CultureInfo.InvariantCulture.NumberFormat);

            Ent_twhcol080 data080 = new Ent_twhcol080() 
            {
                sour = Convert.ToInt32(hdfFuente.Value),
                conj = 0,
                pono = 0,
                sqnb = 0,
                sern = 0,
                orig = 4,
                rcno = "0",
                cwar = "0",
                loca = " ",
                item = "0",
                qana = 0,
                cuni = " ",
                clot = "0",
                habl = 0,
                dife = 2,
                proc = 2,
                conf = 2,
                refcntd = 0,
                refcntu = 0,
                orno = txtNumeroRecepcion.Text.Trim().ToUpper(),
                logn = _operator,
                esti = Convert.ToInt32(quantConvert)
            };

            var validaRegistro = _idaltwhcol080.findRecord(ref data080, ref strError);

            if (validaRegistro.Rows.Count > 0)
            {
                var updateRecord = _idaltwhcol080.updateRecordPicking(ref data080, ref strError);

                if (updateRecord)
                {
                    data080.orig = 3;
                    
                    var updateConf = _idaltwhcol080.updateRecordConfPicking(ref data080, ref strError);

                    lblConfirm.Text = mensajes("msjsave");
                    lblError.Text = String.Empty;
                    divTable.Visible = false;
                    btnGuardarRecepcion.Visible = false;
                    txtNumeroRecepcion.Text = String.Empty;
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
                var validateInsert = _idaltwhcol080.insertRecord(ref data080, ref strError);

                if (validateInsert > 0)
                {
                    data080.orig = 3;

                    var updateConf = _idaltwhcol080.updateRecordConfPicking(ref data080, ref strError);

                    lblConfirm.Text = mensajes("msjsave");
                    lblError.Text = String.Empty;
                    divTable.Visible = false;
                    btnGuardarRecepcion.Visible = false;
                    txtNumeroRecepcion.Text = String.Empty;
                    return;
                }
                else 
                {
                    lblError.Text = mensajes("errorsave");
                    return;
                }
            }
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

            table += "<hr /><table class='table table-bordered' style='font-size:13px;'>";

            for (int i = 0; i < receipts.Rows.Count; i++)
            {
                table += String.Format("<tr style='background-color: darkblue; color: white; font-weight:bold;'><td>{0}</td><td colspan='3'>{1}</td></tr>"
                    , _idioma == "ESPAÑOL" ? "Orden: " : "Order: "
                    , receipts.Rows[i]["ORNO"].ToString());

                //tr Cantidad
                table += String.Format("<tr style='background-color: lightgray;'><td style='font-weight: bold;'>{0}</td><td colspan='3'>{1}</td></tr>"
                    , _idioma == "ESPAÑOL" ? "Cantidad: " : "Quantity: "
                    , String.Format("<input type='number' step='any' runat='server' name='{0}' id='{0}' onblur='validateNumber(this);' class='Textbox' />", "cant-" + i));

                hdfFuente.Value = receipts.Rows[i]["SOUR"].ToString();
            }

            table += "</table>";

            return table;
        }

        #endregion
    }
}