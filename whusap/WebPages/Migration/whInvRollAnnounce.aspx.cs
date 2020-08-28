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
    public partial class whInvRollAnnounce : System.Web.UI.Page
    {
        #region Propiedades
            private static InterfazDAL_tticol022 _idaltticol022 = new InterfazDAL_tticol022();
            private static InterfazDAL_tticol080 _idaltticol080 = new InterfazDAL_tticol080();
            private static InterfazDAL_tticst001 _idaltticst001 = new InterfazDAL_tticst001();
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

            if (txtRollNumber.Text.Trim() != String.Empty && txtWorkOrder.Text.Trim() != String.Empty)
            {
                var sqnb = txtRollNumber.Text.Trim().ToUpper();
                var pdno = txtWorkOrder.Text.Trim().ToUpper();
                var bodo = String.Empty;
                var bodd = String.Empty;

                var consultaSqnb = _idaltticol022.validarRegistroByPalletId(ref sqnb, ref bodo, ref bodd, ref pdno);

                if (consultaSqnb.Rows.Count > 0)
                {
                    var item = consultaSqnb.Rows[0]["ITEM"].ToString();
                    var qtdl = consultaSqnb.Rows[0]["QUANTITY"].ToString();

                    var consultaItem = _idaltticst001.findByItemAndPdno(ref pdno, ref item, ref strError);

                    if (consultaItem.Rows.Count > 0)
                    {
                        txtItem.Text = item.Trim().ToUpper();
                        txtQuantity.Text = qtdl.Trim();
                        hdfCWAR.Value = consultaItem.Rows[0]["CWAR"].ToString();
                        hdfPONO.Value = consultaItem.Rows[0]["PONO"].ToString();
                        trItem.Visible = true;
                        trQuantity.Visible = true;
                        txtWorkOrder.ReadOnly = true;
                        txtRollNumber.ReadOnly = true;
                        btnConsultar.Visible = false;
                        btnRegister.Visible = true;
                    }
                    else 
                    {
                        trItem.Visible = false;
                        trQuantity.Visible = false;
                        txtWorkOrder.ReadOnly = false;
                        txtRollNumber.ReadOnly = false;
                        btnConsultar.Visible = true;
                        btnRegister.Visible = false;
                        lblError.Text = mensajes("orderitemnotexists");
                        return;
                    }
                }
                else 
                {
                    lblError.Text = mensajes("rollnotexists");
                    return;
                }
            }
            else 
            {
                lblError.Text = mensajes("formempty");
                return;
            }
        }

        protected void btnRegister_Click(object sender, EventArgs e) 
        {
            lblError.Text = String.Empty;
            lblConfirm.Text = String.Empty;
            var qtdl = double.Parse(txtQuantity.Text.Trim(), CultureInfo.InvariantCulture.NumberFormat);
            var orno = txtWorkOrder.Text.Trim().ToUpper();
            var pono = hdfPONO.Value.Trim().ToUpper();
            var item = txtItem.Text.Trim().ToUpper();
            var cwar = hdfCWAR.Value.Trim().ToUpper();

            if (qtdl <= 0)
            {
                lblError.Text = mensajes("quantityhigher");
                return;
            }

            var consultaRegistro = _idaltticol080.findRecordByOrnoPonoItem(ref orno, ref pono, ref item, ref strError).Rows;

            Ent_tticol080 data080 = new Ent_tticol080()
            {
                orno = orno,
                pono = Convert.ToInt32(pono),
                item = item,
                cwar = cwar,
                qune = Convert.ToDecimal(qtdl),
                logn = _operator,
                proc = 2,
                refcntd = 0,
                refcntu = 0,
                clot = " ",
                oorg = "4",
                pick = 0
            };

            if (consultaRegistro.Count > 0)
            {
                var validateUpdate = _idaltticol080.updateRecordRollAnnounce(ref data080, ref strError);

                if (validateUpdate)
                {
                    _idaltticol022.ActualizacionPalletId(txtRollNumber.Text.Trim(),"11", strError);
                    lblError.Text = String.Empty;
                    lblConfirm.Text = mensajes("msjupdate");
                    trItem.Visible = false;
                    trQuantity.Visible = false;
                    txtWorkOrder.ReadOnly = false;
                    txtRollNumber.ReadOnly = false;
                    txtRollNumber.Text = String.Empty;
                    txtWorkOrder.Text = String.Empty;
                    btnConsultar.Visible = true;
                    btnRegister.Visible = false;
                }
                else 
                {
                    lblError.Text = mensajes("errorupdt");
                    return;
                }
            }
            else 
            {
                List<Ent_tticol080> lista = new List<Ent_tticol080>();
                lista.Add(data080);
                var isTag = String.Empty;

                var validaInsert = _idaltticol080.insertarRegistro(ref lista, ref strError, ref isTag);
                _idaltticol022.ActualizacionPalletId(txtRollNumber.Text.Trim(), "11", strError);
                if (validaInsert > 0)
                {
                    lblError.Text = String.Empty;
                    lblConfirm.Text = mensajes("msjsave");
                    trItem.Visible = false;
                    trQuantity.Visible = false;
                    txtWorkOrder.ReadOnly = false;
                    txtRollNumber.ReadOnly = false;
                    txtRollNumber.Text = String.Empty;
                    txtWorkOrder.Text = String.Empty;
                    btnConsultar.Visible = true;
                    btnRegister.Visible = false;
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
            lblRollNumber.Text = _textoLabels.readStatement(formName, _idioma, "lblRollNumber");
            lblWorkOrder.Text = _textoLabels.readStatement(formName, _idioma, "lblWorkOrder");
            btnConsultar.Text = _textoLabels.readStatement(formName, _idioma, "btnConsultar");
            lblItem.Text = _textoLabels.readStatement(formName, _idioma, "lblItem");
            lblQuantity.Text = _textoLabels.readStatement(formName, _idioma, "lblQuantity");
            btnRegister.Text = _textoLabels.readStatement(formName, _idioma, "btnRegister");
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