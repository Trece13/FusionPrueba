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
using System.Web.Configuration;

namespace whusap.WebPages.Migration
{
    public partial class whInvReprintLabel : System.Web.UI.Page
    {
        #region Propiedades
            private static InterfazDAL_ttccol301 _idalttccol301 = new InterfazDAL_ttccol301();
            private static InterfazDAL_tticol022 _idaltticol022 = new InterfazDAL_tticol022();
            private static Mensajes _mensajesForm = new Mensajes();
            private static LabelsText _textoLabels = new LabelsText();
            private static string _operator;
            public static string _idioma;
            private static string strError;
            private static string formName;
            private static string globalMessages = "GlobalMessages";
            public string UrlBaseBarcode = WebConfigurationManager.AppSettings["UrlBaseBarcode"].ToString();
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
            var pdno = txtOrder.Text.Trim().ToUpper();
            var sec = txtSecuence.Text.Trim().ToUpper();
            var roll = txtRollWinder.Text.Trim().ToUpper();
            var sqnb = String.Concat(pdno,'-',sec);

            if (pdno == String.Empty || sec == String.Empty  || roll == String.Empty )
            {
                lblError.Text = mensajes("formempty");
                return;
            }

            var consultaInformacion = _idaltticol022.findBySqnbPdnoLabelPallet(ref pdno, ref sqnb, ref strError).Rows;

            if (consultaInformacion.Count > 0)
            {
                var dele = consultaInformacion[0]["DELE"].ToString().Trim();

                if (dele == "1")
                {
                    lblError.Text = mensajes("tagdeleted");
                    return;
                }

                var item = consultaInformacion[0]["MITM"].ToString().Trim();
                var descitem = consultaInformacion[0]["DSCA"].ToString().Trim();
                var quantity = consultaInformacion[0]["QTDL"].ToString().Trim();
                var unit = consultaInformacion[0]["CUNI"].ToString().Trim();
                var user = consultaInformacion[0]["LOGN"].ToString().Trim();
                var fecha = consultaInformacion[0]["FECHA"].ToString().Trim();
                var machine = consultaInformacion[0]["MCNO"].ToString().Trim();
                var norp = Convert.ToInt32(consultaInformacion[0]["NORP"].ToString().Trim());

                //imgItem
                var rutaServItem = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + item.ToUpper() + "&code=Code128&dpi=96";
                imgItem.Src = !string.IsNullOrEmpty(item) ? rutaServItem : "";

                lblItem.Text = descitem;

                //imgSqnb
                var rutaServSqnb = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + sqnb.ToUpper() + "&code=Code128&dpi=96";
                imgSqnb.Src = !string.IsNullOrEmpty(sqnb) ? rutaServSqnb : "";

                lblValueWorkOrder.Text = pdno;
                lblValueWeight.Text = quantity;
                lblValueRollNumber.Text = sec;
                lblValueDate.Text = fecha;
                lblValueShift.Text = "A,B,C,D";
                lblValueOperator.Text = user;
                lblValueRollWinder.Text = " " + roll;
                lblValueShift2.Text = "R - L";
                lblValueReprintedBy.Text = " " + _operator;
                lblValueMachine.Text = machine;

                divTable.Visible = true;
                divBotones.Visible = true;
                //modificaciones jc
                _idaltticol022.ActualizarRegistroTicol222(Session["user"].ToString(), pdno,sqnb);
                Ent_tticol022 Obj_tticol022 = new Ent_tticol022
                {
                    sqnb = sqnb,
                    norp = ++norp
                };
                bool ActualizacionTticol022 = _idaltticol022.ActualizarNorpTicol022(Obj_tticol022);
            }
            else 
            {
                divTable.Visible = false;
                divBotones.Visible = false;

                lblError.Text = mensajes("ordenocreated");
                return;
            }
        }

        #endregion

        #region Metodos

        protected void CargarIdioma()
        {
            lblOrder.Text = _textoLabels.readStatement(formName, _idioma, "lblOrder");
            lblSecuence.Text = _textoLabels.readStatement(formName, _idioma, "lblSecuence");
            lblRollWinder.Text = _textoLabels.readStatement(formName, _idioma, "lblRollWinder");
            btnConsultar.Text = _textoLabels.readStatement(formName, _idioma, "btnConsultar");
            btnPrint.InnerText = _textoLabels.readStatement(formName, _idioma, "linkPrint");

            lblWorkOrder.Text = _textoLabels.readStatement(formName, _idioma, "lblWorkOrder");
            lblWeight.Text = _textoLabels.readStatement(formName, _idioma, "lblWeight");
            lblRollNumber.Text = _textoLabels.readStatement(formName, _idioma, "lblRollNumber");
            lblDate.Text = _textoLabels.readStatement(formName, _idioma, "lblDate");
            lblShift.Text = _textoLabels.readStatement(formName, _idioma, "lblShift");
            lblOperator.Text = _textoLabels.readStatement(formName, _idioma, "lblOperator");
            //lblRollWinder.Text = _textoLabels.readStatement(formName, _idioma, "lblRollWinder");
            lblTRollWinder.Text = _textoLabels.readStatement(formName, _idioma, "lblTRollWinder");
            lblMachine.Text = _textoLabels.readStatement(formName, _idioma, "lblMachine");
            lblReprintedBy.Text = _textoLabels.readStatement(formName, _idioma, "lblReprintedBy");
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