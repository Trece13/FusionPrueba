using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using whusa.Interfases;
using whusa.Utilidades;
using System.Globalization;
using System.Configuration;
using whusa.Entidades;
using System.Data;

namespace whusap.WebPages.Migration
{
    public partial class whInvAlmArticulo : System.Web.UI.Page
    {
        #region Propiedades
            private static InterfazDAL_ttccol301 _idalttccol301 = new InterfazDAL_ttccol301();
            private static InterfazDAL_tticol127 _idaltticol127 = new InterfazDAL_tticol127();
            private static InterfazDAL_ttcibd001 _idalttcibd001 = new InterfazDAL_ttcibd001();
            private static InterfazDAL_twhinr140 _idaltwhinr140 = new InterfazDAL_twhinr140();
            private static Mensajes _mensajesForm = new Mensajes();
            private static LabelsText _textoLabels = new LabelsText();
            private static string _operator;
            public static string _idioma;
            private static string strError;
            private static string formName;
            private static string globalMessages = "GlobalMessages";
            private DataTable _consultaLoteUsuario = new DataTable();
            private DataTable _consultaItem = new DataTable();
            private DataTable _consultaInformacion = new DataTable();
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
            divTable.InnerHtml = String.Empty;
            if (txtArticulo.Text.Trim().ToUpper() != String.Empty)
            {
                Ent_tticol127 dataticol127 = new Ent_tticol127() { user = _operator };
                _consultaLoteUsuario = _idaltticol127.listaRegistro_ObtieneAlmacen(ref dataticol127, ref strError);

                if (_consultaLoteUsuario.Rows.Count > 0)
                {
                    Ent_ttcibd001 data001 = new Ent_ttcibd001() { item = txtArticulo.Text.Trim().ToUpper() };
                    _consultaItem = _idalttcibd001.listaRegistro_ObtieneDescripcionUnidad(ref data001, ref strError);

                    if (_consultaItem.Rows.Count > 0)
                    {
                        var cwar = _consultaLoteUsuario.Rows[0]["BODEGA"].ToString();
                        var cwardesc = _consultaLoteUsuario.Rows[0]["DSCA"].ToString();
                        var item = txtArticulo.Text.Trim().ToUpper();
                        var itemdesc = _consultaItem.Rows[0]["DESCRIPCION"].ToString();
                        var unidad = _consultaItem.Rows[0]["UNID"].ToString();

                        _consultaInformacion = _idaltwhinr140.consultaPorAlmacenItem(ref cwar, ref item, ref strError);

                        if (_consultaInformacion.Rows.Count > 0)
                        {
                            divTable.InnerHtml = makeTableReceipt();
                        }
                        else
                        {
                            lblError.Text = String.Format(mensajes("nodata"), cwar, item);
                            return;
                        }
                    }
                    else
                    {
                        lblError.Text = mensajes("itemnotexists");
                        return;
                    }
                }
                else
                {
                    lblError.Text = mensajes("usernotwarehouse");
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
            lblArticulo.Text = _textoLabels.readStatement(formName, _idioma, "lblArticulo");
            btnConsultar.Text = _textoLabels.readStatement(formName, _idioma, "btnConsultar");
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

        protected string makeTableReceipt()
        {
            var cwar = _consultaLoteUsuario.Rows[0]["BODEGA"].ToString();
            var cwardesc = _consultaLoteUsuario.Rows[0]["DSCA"].ToString();
            var item = txtArticulo.Text.Trim().ToUpper();
            var itemdesc = _consultaItem.Rows[0]["DESCRIPCION"].ToString();
            var unidad = _consultaItem.Rows[0]["UNID"].ToString();

            var table = String.Empty;

            table += String.Format("<hr/><table class='table table-bordered' style='font-size:13px; border:3px solid; border-style:outset;'><tr style='background-color: darkblue; color: white; font-weight:bold;'><td>{0}</td>"
                + "<td colspan='2'>{1}</td></tr>", _idioma == "ESPAÑOL" ? "Almacen: " : "Warehouse: ", cwar + " - " + cwardesc);

            table += String.Format("<tr style='background-color: lightgray;'><td style='font-weight: bold;'>{0}</td><td colspan='2'>{1}</td></tr>"
                    , _idioma == "ESPAÑOL" ? "Articulo: " : "Item: ", item + " - " + itemdesc);

            table += String.Format("<tr style='background-color: white;'><td style='font-weight: bold;'>{0}</td><td colspan='2'>{1}</td></tr>"
                    , _idioma == "ESPAÑOL" ? "Unidad: " : "Unit: ", unidad);

            table += String.Format("<tr style='background-color: lightgray;'><b><td>{0}</td><td>{1}</td><td>{2}</td></b></tr>",
                    _idioma == "ESPAÑOL" ? "Ubicación " : "Location "
                    , _idioma == "ESPAÑOL" ? "Lote " : "Lot "
                    , _idioma == "ESPAÑOL" ? "Unidad " : "Unit");

            for (int i = 0; i < _consultaInformacion.Rows.Count; i++)
            {
                //tr Articulo
                table += String.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td></tr>",
                    _consultaInformacion.Rows[i]["LOCA"].ToString()
                    , _consultaInformacion.Rows[i]["CLOT"].ToString()
                    , _consultaInformacion.Rows[i]["STKS"].ToString());
            }

            table += "</table>";

            return table;
        }

        #endregion
    }
}