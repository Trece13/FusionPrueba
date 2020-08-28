using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.Globalization;
using System.Configuration;
using whusa.Entidades;
using whusa.Interfases;
using System.Data;
using whusa.Utilidades;

namespace whusap.WebPages.Migration
{
    public partial class whInvTransfers : System.Web.UI.Page
    {
        #region Propiedades
            private static InterfazDAL_ttccol301 _idalttccol301 = new InterfazDAL_ttccol301();
            private static InterfazDAL_twhltc100 _idaltwhltc100 = new InterfazDAL_twhltc100();
            private static InterfazDAL_ttcibd001 _idalttcibd001 = new InterfazDAL_ttcibd001();
            private static InterfazDAL_twhwmd200 _idaltwhwmd200 = new InterfazDAL_twhwmd200();
            private static InterfazDAL_tticol127 _idaltticol127 = new InterfazDAL_tticol127();
            private static InterfazDAL_twhwmd300 _idaltwhwmd300 = new InterfazDAL_twhwmd300();
            private static InterfazDAL_twhcol010 _idaltwhcol010 = new InterfazDAL_twhcol010();
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
                    come = mensajes("encabezado"),
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
            lblConfirm.Text = String.Empty;
            lblError.Text = String.Empty;
            if (txtItem.Text.Trim() != String.Empty)
            {
                var withLot = true;
                var consultaInformacion = new DataTable();

                var ITEM = txtItem.Text.Trim().ToUpper();
                var CLOT = txtNumeroLote.Text.Trim().ToUpper();

                Ent_twhltc100 data = new Ent_twhltc100()
                {
                    item = ITEM,
                    clot = CLOT
                };

                Ent_tticol127 dataticol127 = new Ent_tticol127() { user = _operator };

                var validaAlmacenUsuario = _idaltticol127.listaRegistro_ObtieneAlmacen(ref dataticol127, ref strError);

                if (validaAlmacenUsuario.Rows.Count < 1)
                {
                    lblError.Text = mensajes("Usuario no tiene definido el almacen, por favor comuniquese con el administrador.");
                    return;
                }

                if (txtNumeroLote.Text.Trim() != String.Empty)
                {
                    var validaItem = _idaltwhltc100.listaRegistro_Clot(ref data, ref strError);

                    if (validaItem.Rows.Count > 0)
                    {
                        consultaInformacion = _idalttcibd001.findRecordTransfers(ref ITEM, ref withLot, ref strError);
                    }
                }
                else 
                {
                    withLot = false;
                    consultaInformacion = _idalttcibd001.findRecordTransfers(ref ITEM, ref withLot, ref strError);   
                }

                if (consultaInformacion.Rows.Count > 0)
                {
                    hdfUnidad.Value = consultaInformacion.Rows[0]["CUNI"].ToString();
                    hdfFactor.Value = consultaInformacion.Rows[0]["CONV"].ToString();
                    txtSourceLocation.Text = validaAlmacenUsuario.Rows[0]["BODEGA"].ToString();
                    txtDescription.Text = consultaInformacion.Rows[0]["DSCA"].ToString();
                    txtNumeroLote.Text = txtNumeroLote.Text.Trim().ToUpper();
                    txtNumeroLote.Enabled = false;
                    txtItem.Text = txtItem.Text.Trim().ToUpper();
                    txtItem.Enabled = false;
                    trDescription.Visible = true;
                    trSource.Visible = true;
                    trUbicacion.Visible = true;
                    btnConsultar.Visible = false;
                    btnConsultarSource.Visible = true;
                }
                else
                {
                    txtNumeroLote.Text = txtNumeroLote.Text.Trim().ToUpper();
                    txtNumeroLote.Enabled = true;
                    txtItem.Text = txtItem.Text.Trim().ToUpper();
                    txtItem.Enabled = true;
                    trDescription.Visible = false;
                    trSource.Visible = false;
                    trUbicacion.Visible = false;
                    btnConsultar.Visible = true;
                    btnConsultarSource.Visible = false;
                    lblError.Text = withLot ? mensajes("lotitemincorrect") : mensajes("itemincorrect");
                    return;
                }
            }
            else 
            {
                lblError.Text = mensajes("itemblank");
                return;
            }
        }

        protected void btnConsultarSource_Click(object sender, EventArgs e) 
        {
            lblConfirm.Text = String.Empty;
            lblError.Text = String.Empty;
            if (txtUbicacion.Text.Trim() != String.Empty)
            {
                var CWAR = txtSourceLocation.Text.Trim().ToUpper();

                Ent_twhwmd200 datamd200 = new Ent_twhwmd200() { cwar = CWAR };

                var consultaAlmacen = _idaltwhwmd200.listaRegistro_ObtieneAlmacenLocation(ref datamd200, ref strError);

                if (consultaAlmacen.Rows.Count > 0)
                {
                    var clot = txtNumeroLote.Text.Trim().ToUpper();
                    var item = txtItem.Text.Trim().ToUpper();
                    var cwar = txtSourceLocation.Text.Trim().ToUpper();
                    var loca = txtUbicacion.Text.Trim().ToUpper();

                    var consultaInfoUbicacion = _idaltwhwmd300.consultaPorAlmacenUbicacion(ref clot, ref item, ref cwar, ref loca, ref strError).Rows;

                    if (consultaInfoUbicacion.Count > 0)
                    {
                        var loct = Convert.ToInt32(consultaInfoUbicacion[0]["LOCT"]);
                        var btri = Convert.ToInt32(consultaInfoUbicacion[0]["BTRI"]);
                        var qtydisp = consultaInfoUbicacion[0]["QTYDISP"].ToString();
                        var factor = hdfFactor.Value;
                        var unidad = hdfUnidad.Value;

                        if (loct != 5)
                        {
                            lblError.Text = String.Format(mensajes("locationnotload"), loca);
                            return;
                        }

                        if (btri != 2)
                        {
                            lblError.Text = String.Format(mensajes("locationblock"), loca);
                            return;
                        }

                        if ((float.Parse(qtydisp, CultureInfo.InvariantCulture.NumberFormat) == 0))
                        {
                            lblError.Text = mensajes("itemunavailable");
                            return;
                        }

                        var cantplt = 0.0;

                        if (factor.Trim() != String.Empty)
                        {
                            cantplt = Math.Round((float.Parse(qtydisp, CultureInfo.InvariantCulture.NumberFormat) / float.Parse(factor, CultureInfo.InvariantCulture.NumberFormat)), 2); 
                        }
                        else 
                        {
                            cantplt = Math.Round((float.Parse(qtydisp, CultureInfo.InvariantCulture.NumberFormat) / 1), 2);
                        }

                        txtCantidadUnidad.Text = qtydisp + " - " + unidad;
                        txtCantidad.Text = cantplt.ToString();
                        txtUbicacion.Enabled = false;
                        trCantidadUnidad.Visible = true;
                        trCantidad.Visible = true;
                        trDestination.Visible = true;
                        trCantidadTransferir.Visible = true;
                        btnConsultarSource.Visible = false;
                        btnActualizar.Visible = true;
                    }
                    else 
                    {
                        txtCantidadUnidad.Text = String.Empty;
                        txtCantidad.Text = String.Empty;
                        txtUbicacion.Enabled = true;
                        trCantidadUnidad.Visible = false;
                        trCantidad.Visible = false;
                        trDestination.Visible = false;
                        trCantidadTransferir.Visible = false;
                        btnConsultarSource.Visible = true;
                        btnActualizar.Visible = false;
                        lblError.Text = mensajes("locationnotexists");
                        return;
                    }
                }
                else 
                {
                    lblError.Text = mensajes("warehousenotexists");
                    return;
                }
            }
            else 
            {
                lblError.Text = mensajes("formempty");
                return;
            }
        }

        protected void btnActualizar_Click(object sender, EventArgs e) 
        {
            lblConfirm.Text = String.Empty;
            lblError.Text = String.Empty;
            var txtCantidadTransferir = txtCantidadTrasnferir.Text.Trim();
            var ubicacionDestino = txtUbicacionDestino.Text.Trim().ToUpper();
            var cwar = txtSourceLocation.Text.Trim().ToUpper();
            var clot = txtNumeroLote.Text.Trim().ToUpper();

            if (txtCantidadTransferir == String.Empty || ubicacionDestino == String.Empty)
            {
                lblError.Text = mensajes("formempty");
                return;
            }

            var cantidadTransferir = double.Parse(txtCantidadTransferir, CultureInfo.InvariantCulture.NumberFormat);

            if (cantidadTransferir == 0)
            {
                lblError.Text = mensajes("quantityzero");
                return;
            }

            if (ubicacionDestino == txtSourceLocation.Text.Trim().ToUpper())
            {
                lblError.Text = mensajes("differentlocation");
                return;
            }

            var validaUbicacion = _idaltwhwmd300.validateExistsLocation(ref ubicacionDestino, ref cwar, ref strError).Rows;

            if (validaUbicacion.Count > 0)
            {
                var loct = Convert.ToInt32(validaUbicacion[0]["LOCT"].ToString());
                var btrr = Convert.ToInt32(validaUbicacion[0]["BTRR"].ToString());
                var cantdisponible = double.Parse(txtCantidad.Text.Trim(), CultureInfo.InvariantCulture.NumberFormat);

                if (loct != 5)
                {
                    lblError.Text = String.Format(mensajes("locationdesnotload"), ubicacionDestino);
                    return;
                }

                if (btrr != 2)
                {
                    lblError.Text = String.Format(mensajes("locationdesblocked"), ubicacionDestino); ;
                    return;
                }

                if (cantidadTransferir > cantdisponible)
                {
                    lblError.Text = String.Format(mensajes("quantitytransfer"), cantidadTransferir, cantdisponible);
                    return;
                }

                var consecutivo = _idaltwhcol010.consultarConsecutivoRegistro(ref clot, ref strError);

                consecutivo = consecutivo + 1;

                Ent_twhcol010 data010 = new Ent_twhcol010() 
                {
                    clot = clot,
                    sqnb = consecutivo,
                    mitm = txtItem.Text.Trim().ToUpper(),
                    dsca = txtDescription.Text.Trim().ToUpper(),
                    cwor = txtSourceLocation.Text.Trim().ToUpper(),
                    loor = txtUbicacion.Text.Trim().ToUpper(),
                    cwde = txtSourceLocation.Text.Trim().ToUpper(),
                    lode = txtUbicacionDestino.Text.Trim().ToUpper(),
                    qtdl = double.Parse(txtCantidadTrasnferir.Text.Trim(), CultureInfo.InvariantCulture.NumberFormat),
                    cuni = txtCantidadUnidad.Text.Split('-').Last().Trim(),
                    user = _operator,
                    refcntd = 0,
                    refcntu = 0
                };

                var validSave = _idaltwhcol010.insertarRegistro(ref data010, ref strError);

                if (validSave > 0)
                {
                    lblConfirm.Text = mensajes("msjsave");

                    trDescription.Visible = false;
                    trSource.Visible = false;
                    trUbicacion.Visible = false;
                    trCantidadUnidad.Visible = false;
                    trCantidad.Visible = false;
                    trDestination.Visible = false;
                    trCantidadTransferir.Visible = false;
                    txtNumeroLote.Enabled = true;
                    txtItem.Enabled = true;

                    txtNumeroLote.Text = String.Empty;
                    txtItem.Text = String.Empty;

                    btnConsultar.Visible = true;
                    btnConsultarSource.Visible = false;
                    btnActualizar.Visible = false;

                    return;
                }
                else 
                {
                    lblError.Text = mensajes("errorsave");
                    return;
                }
            }
            else 
            {
                lblError.Text = mensajes("locationnotexists");
                return;
            }
        }

        #endregion

        #region Metodos

        protected void CargarIdioma() 
        {
            lblNumeroLote.Text = _textoLabels.readStatement(formName, _idioma, "lblNumeroLote");
            lblItem.Text = _textoLabels.readStatement(formName, _idioma, "lblItem");
            btnConsultar.Text = _textoLabels.readStatement(formName, _idioma, "btnConsultar");
            lblDescription.Text = _textoLabels.readStatement(formName, _idioma, "lblDescription");
            lblSourceLocation.Text = _textoLabels.readStatement(formName, _idioma, "lblSourceLocation");
            lblUbicacion.Text = _textoLabels.readStatement(formName, _idioma, "lblUbicacion"); ;
            btnConsultarSource.Text = _textoLabels.readStatement(formName, _idioma, "btnConsultarSource"); ;
            lblCantidadUnidad.Text = _textoLabels.readStatement(formName, _idioma, "lblCantidadUnidad"); ;
            lblCantidad.Text = _textoLabels.readStatement(formName, _idioma, "lblCantidad"); ;
            lblUbicacionDestino.Text = _textoLabels.readStatement(formName, _idioma, "lblUbicacionDestino"); ;
            lblCantidadTrasnferir.Text = _textoLabels.readStatement(formName, _idioma, "lblCantidadTrasnferir"); ;
            btnActualizar.Text = _textoLabels.readStatement(formName, _idioma, "btnActualizar"); ;
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