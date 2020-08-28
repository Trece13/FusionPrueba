using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using whusa.Interfases;
using System.Threading;
using System.Configuration;
using whusa.Entidades;
using System.Globalization;
using System.Data;
using whusa.Utilidades;

namespace whusap.WebPages.Migration
{
    public partial class whInvAdvices : System.Web.UI.Page
    {
        #region Propiedades

            private static InterfazDAL_ttccol301 _idalttccol301 = new InterfazDAL_ttccol301();
            private static IntefazDAL_twhinh225 _idaltwhinh225 = new IntefazDAL_twhinh225();
            private static InterfazDAL_twhcol080 _idaltwhcol080 = new InterfazDAL_twhcol080();
            private static Mensajes _mensajesForm = new Mensajes();
            private static LabelsText _textoLabels = new LabelsText();
            private static string _operator;
            public static string _idioma;
            private static string strError;
            private static string formName;
            private static string globalMessages = "GlobalMessages";
            private static DataTable _validateRecord = new DataTable();

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
            }
        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            lblConfirm.Text = String.Empty;
            lblError.Text = String.Empty;

            if (txtNumero.Text != "")
            {
                var numeroSugerencia = txtNumero.Text.ToUpper();
                var cwar = numeroSugerencia.Substring(0, 5);
                var orno = numeroSugerencia.Substring(0, 9);
                var oset = numeroSugerencia.Substring(9, 2);
                var pono = numeroSugerencia.Substring(11, 3);
                var seqn = numeroSugerencia.Substring(14, 2);
                var sern = numeroSugerencia.Substring(16, 2);


                Ent_twhinh225 data = new Ent_twhinh225()
                {
                    orno = orno,
                    oset = oset,
                    pono = pono,
                    sern = sern,
                    seqn = seqn
                };

                _validateRecord = _idaltwhinh225.findRecordByOrderNumber(ref data, ref strError);

                if (_validateRecord.Rows.Count > 0)
                {
                    if (_validateRecord.Rows[0]["SLOC"].ToString() == "2")
                    {
                        lblError.Text = mensajes("almnolocation");
                        return;
                    }

                    divTable.Visible = true;
                    lblValueOrden.Text = String.Concat(_validateRecord.Rows[0]["ORNO"].ToString()
                                        , _validateRecord.Rows[0]["OSET"].ToString()
                                        , _validateRecord.Rows[0]["PONO"].ToString()
                                        , _validateRecord.Rows[0]["SEQN"].ToString());
                    lblValueArticulo.Text = String.Concat(_validateRecord.Rows[0]["DSCA"].ToString(), " ", _validateRecord.Rows[0]["ITEM"].ToString());
                    lblValueCantSugerida.Text = _validateRecord.Rows[0]["ASTR"].ToString();
                    lblValueUnidad.Text = _validateRecord.Rows[0]["ATUN"].ToString();
                    lblValueAlmacen.Text = _validateRecord.Rows[0]["CWAR"].ToString();
                    lblValueUbicacion.Text = _validateRecord.Rows[0]["LOCA"].ToString();
                    lblValueLote.Text = _validateRecord.Rows[0]["CLOT"].ToString();
                    lblValueUnidadTwo.Text = _validateRecord.Rows[0]["ATUN"].ToString();
                    lblValueAlmacenTwo.Text = _validateRecord.Rows[0]["CWAR"].ToString();

                    hdfCantidadPedida.Value = _validateRecord.Rows[0]["ASTK"].ToString(); ;
                    hdfCantidadSugerida.Value = _validateRecord.Rows[0]["ASTR"].ToString(); ;
                    hdfUbicacion.Value = _validateRecord.Rows[0]["LOCA"].ToString(); ;
                    hdfLote.Value = _validateRecord.Rows[0]["CLOT"].ToString(); ;
                }
                else
                {
                    lblError.Text = mensajes("notorder");
                    return;
                }
            }
            else
            {
                lblError.Text = mensajes("blanknumber");
                return;
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            lblConfirm.Text = String.Empty;
            lblError.Text = String.Empty;
            var cantidad = txtCantSugerida.Text;
            var LOCA = txtUbicacion.Text.Trim().ToUpper();
            var CLOT = txtLote.Text.Trim().ToUpper();

            Ent_twhcol080 data = new Ent_twhcol080()
            {
                sour = Convert.ToInt32(_validateRecord.Rows[0]["OORG"].ToString()),
                orno = _validateRecord.Rows[0]["ORNO"].ToString(),
                conj = Convert.ToInt32(_validateRecord.Rows[0]["OSET"].ToString()),
                pono = Convert.ToInt32(_validateRecord.Rows[0]["PONO"].ToString()),
                sqnb = Convert.ToInt32(_validateRecord.Rows[0]["SEQN"].ToString()),
                sern = Convert.ToInt32(_validateRecord.Rows[0]["SERN"].ToString()),
                rcno = " ",
                cwar = _validateRecord.Rows[0]["CWAR"].ToString(),
                loca = LOCA == String.Empty ? " " : LOCA,
                item = _validateRecord.Rows[0]["ITEM"].ToString(),
                qana = double.Parse(cantidad, CultureInfo.InvariantCulture.NumberFormat),
                cuni = _validateRecord.Rows[0]["ATUN"].ToString(),
                clot = CLOT == String.Empty ? " " : CLOT,
                logn = _operator,
                orig = 3
            };

            var validateRecord = _idaltwhcol080.findRecord(ref data, ref strError);

            if (validateRecord.Rows.Count > 0)
            {
                var validateUpdate = _idaltwhcol080.updateRecord(ref data, ref strError);

                if (validateUpdate)
                {
                    txtNumero.Text = String.Empty;
                    txtCantSugerida.Text = String.Empty;
                    txtUbicacion.Text = String.Empty;
                    txtLote.Text = String.Empty;
                    lblConfirm.Text = mensajes("msjupdate");
                    divTable.Visible = false;
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
                    txtNumero.Text = String.Empty;
                    txtCantSugerida.Text = String.Empty;
                    txtUbicacion.Text = String.Empty;
                    txtLote.Text = String.Empty;
                    lblConfirm.Text = mensajes("msjsave");
                    divTable.Visible = false;
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

        #endregion

        #region Metodos

        protected void CargarIdioma()
        {
            lblSugNumero.Text = _textoLabels.readStatement(formName, _idioma, "lblSugNumero");
            btnConsultar.Text = _textoLabels.readStatement(formName, _idioma, "btnConsultar");
            lblOrden.Text = _textoLabels.readStatement(formName, _idioma, "lblOrden");
            lblArticulo.Text = _textoLabels.readStatement(formName, _idioma, "lblArticulo");
            lblCantSugerida.Text = _textoLabels.readStatement(formName, _idioma, "lblCantSugerida");
            lblUnidad.Text = _textoLabels.readStatement(formName, _idioma, "lblUnidad");
            lblAlmacen.Text = _textoLabels.readStatement(formName, _idioma, "lblAlmacen");
            lblUbicacion.Text = _textoLabels.readStatement(formName, _idioma, "lblUbicacion");
            lblLote.Text = _textoLabels.readStatement(formName, _idioma, "lblLote");
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

        //protected void IdiomaIngles()
        //{
        //    Session["ddlIdioma"] = "INGLES";
        //    _idioma = "INGLES";
        //    lblSugNumero.Text = "Advice number: ";
        //    btnConsultar.Text = "Query";
        //    lblOrden.Text = "Order";
        //    lblArticulo.Text = "Item";
        //    lblCantSugerida.Text = "Quantity";
        //    lblUnidad.Text = "UOM";
        //    lblAlmacen.Text = "Warehouse";
        //    lblUbicacion.Text = "Location";
        //    lblLote.Text = "Lot";
        //    btnGuardar.Text = "Save Picking";
        //}

        //protected void IdiomaEspañol()
        //{
        //    Session["ddlIdioma"] = "ESPAÑOL";
        //    _idioma = "ESPAÑOL";
        //    lblSugNumero.Text = "Sugerencia Número: ";
        //    btnConsultar.Text = "Consultar";
        //    lblOrden.Text = "Orden";
        //    lblArticulo.Text = "Articulo";
        //    lblCantSugerida.Text = "Cant. Sugerida";
        //    lblUnidad.Text = "Un";
        //    lblAlmacen.Text = "Almacen";
        //    lblUbicacion.Text = "Ubicación";
        //    lblLote.Text = "Lote";
        //    btnGuardar.Text = "Guardar Recolección";
        //}

        

        //protected string mensajes(string tipoMensaje)
        //{
        //    var retorno = String.Empty;

        //    switch (tipoMensaje)
        //    {
        //        case "encabezado":
        //            retorno = _idioma == "ESPAÑOL" ? "Recolección Articulo" : "Item Picking";
        //            break;
        //        case "almnolocation":
        //            retorno = _idioma == "ESPAÑOL" ? "Almacen no utiliza ubicaciones" : "Warehouse don't use locations";
        //            break;
        //        case "notorder":
        //            retorno = _idioma == "ESPAÑOL" ? " Orden no existe." : "Order not exists";
        //            break;
        //        case "blanknumber":
        //            retorno = _idioma == "ESPAÑOL" ? "Por favor ingrese un número" : "Please, enter a number";
        //            break;
        //        case "msjupdate":
        //            retorno = _idioma == "ESPAÑOL" ? "Se ha actualizado la información correctamente." : "Information updated correctly.";
        //            break;
        //        case "msjsave":
        //            retorno = _idioma == "ESPAÑOL" ? "Se ha guardado la información correctamente." : "Information saved correctly";
        //            break;
        //        case "errorupdt":
        //            retorno = _idioma == "ESPAÑOL" ? "Ha ocurrido un error al actualizar la información." : "An error ocurred when update information";
        //            break;
        //        case "errorsave":
        //            retorno = _idioma == "ESPAÑOL" ? "Ha ocurrido un error al guardar la información." : "An error ocurred when save information";
        //            break;
        //    }

        //    return retorno;
        //}
    }
}