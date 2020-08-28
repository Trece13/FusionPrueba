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
    public partial class whInvTransfersPlant : System.Web.UI.Page
    {
        #region Propiedades
            private static InterfazDAL_twhwmd300 _idaltwhwmd300 = new InterfazDAL_twhwmd300();
            private static InterfazDAL_twhcol010 _idaltwhcol010 = new InterfazDAL_twhcol010();
            private static InterfazDAL_ttcibd001 _idalttcibd001 = new InterfazDAL_ttcibd001();
            private static InterfazDAL_ttccol301 _idalttccol301 = new InterfazDAL_ttccol301();
            private static Mensajes _mensajesForm = new Mensajes();
            private static LabelsText _textoLabels = new LabelsText();
            private static string _operator;
            public static string _idioma;
            private static string strError;
            private static string formName;
            private static string globalMessages = "GlobalMessages";
            private static string _tipoFormulario = String.Empty;
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

                string strTitulo = _tipoFormulario == "TO" ? mensajes("encabezadoto") : mensajes("encabezado");
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

                if (Request.QueryString["tipoFormulario"] != null)
                {
                    _tipoFormulario = Request.QueryString["tipoFormulario"].ToString().Trim().ToUpper();
                }
                else 
                {
                    _tipoFormulario = "IN";
                }

                if (_tipoFormulario == "TO")
                {
                    trPlant.Visible = true;
                    cargarPlantas();
                }
                else 
                {
                    trPlant.Visible = false;
                }
            }
        }

        protected void btnTransfer_Click(object sender, EventArgs e) 
        {
            if (txtItem.Text.Trim() == String.Empty ||txtTargetLocation.Text.Trim() == String.Empty || txtTargetQty.Text.Trim() == String.Empty)
            {
                lblError.Text = mensajes("formempty");
                return;
            }

            if (_tipoFormulario == "TO" && slPlant.SelectedValue.Trim() == String.Empty)
            {
                lblError.Text = mensajes("formempty");
                return;
            }

            var item = txtItem.Text.Trim().ToUpper();
            var loca = txtTargetLocation.Text.Trim().ToUpper();
            var qtdl = double.Parse(txtTargetQty.Text.Trim(), CultureInfo.InvariantCulture.NumberFormat);

            //Buscar datos del articulo
            Ent_ttcibd001 data001 = new Ent_ttcibd001() { item = item };
            var datosArticulo = _idalttcibd001.listaRegistro_ObtieneDescripcionUnidad(ref data001, ref strError);

            if (datosArticulo.Rows.Count <= 0)
            {
                lblError.Text = mensajes("itemnotexists");
                return;
            }

            var dscaitem = datosArticulo.Rows[0]["DESCRIPCION"].ToString();
            var cuni = datosArticulo.Rows[0]["UNID"].ToString();

            //Buscar almacen de la ubicación+
            var loct = "5";
            var btrr = "2";
            var datosUbicacion = _idaltwhwmd300.findLocationByLoctAndBtrr(ref loct, ref btrr, ref loca, ref strError);

            if (datosUbicacion.Rows.Count <= 0)
            {
                lblError.Text = String.Format(mensajes("targetlocation"), loca);
                return;
            }

            var cwar = datosUbicacion.Rows[0]["CWAR"].ToString();

            //Valida si ya existe registro
            var consecutivo = _tipoFormulario == "TO" ? _idaltwhcol010.consultarConsecutivoMaximo(ref strError)
                : _idaltwhcol010.consultarConsecutivoMaximo(ref strError); //_idaltwhcol010.consultarConsecutivoRegistroPorItem(ref item, ref loca, ref strError);

            consecutivo = consecutivo + 1;

            Ent_twhcol010 data010 = new Ent_twhcol010() 
            {
                clot = " ",
                sqnb = consecutivo,
                mitm = item,
                dsca = dscaitem,
                cwor = _tipoFormulario == "TO" ? slPlant.SelectedValue.Trim().ToUpper() : cwar,
                loor = " ",
                cwde = cwar,
                lode = loca,
                qtdl = qtdl,
                cuni = cuni,
                user = _operator,
                refcntd = 0,
                refcntu = 0
            };

            var validInsert = _idaltwhcol010.insertarRegistro(ref data010, ref strError);

            if (validInsert > 0)
            {
                lblError.Text = String.Empty;
                lblConfirm.Text = mensajes("msjsave");
                txtItem.Text = String.Empty;
                txtTargetLocation.Text = String.Empty;
                txtTargetQty.Text = String.Empty;
                return;
            }
            else 
            {
                lblError.Text = mensajes("errorsave");
                return;
            }
        }

        #endregion

        #region Metodos

        protected void cargarPlantas() 
        {
            var listaPlantas = _idaltwhwmd300.consultaPlantasTransfer(ref strError);

            ListItem itemSelect = new ListItem() { Text = _idioma == "INGLES" ? "--Select an option--" : "--Seleccione una opción", Value = "" };

            slPlant.Items.Insert(0, itemSelect);

            if (listaPlantas.Rows.Count > 0)
            {
                foreach (DataRow item in listaPlantas.Rows)
                {
                    ListItem registro = new ListItem() 
                    {
                        Text = item["PLAN"].ToString() + " - " + item["CWAR"].ToString(),
                        Value = item["CWAR"].ToString()
                    };
                }
            }
        }

        protected void CargarIdioma()
        {
            lblPlant.Text = _textoLabels.readStatement(formName, _idioma, "lblPlant");
            lblItem.Text = _textoLabels.readStatement(formName, _idioma, "lblItem");
            lblTargetLocation.Text = _textoLabels.readStatement(formName, _idioma, "lblTargetLocation");
            lblTargetQty.Text = _textoLabels.readStatement(formName, _idioma, "lblTargetQty");
            btnTransfer.Text = _textoLabels.readStatement(formName, _idioma, "btnTransfer");
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