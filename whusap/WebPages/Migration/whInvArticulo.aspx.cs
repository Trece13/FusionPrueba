using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using whusa.Utilidades;
using whusa.Interfases;
using System.Globalization;
using whusa.Entidades;
using System.Configuration;
using System.Data;

namespace whusap.WebPages.Migration
{
    public partial class whInvArticulo : System.Web.UI.Page
    {
        #region Propiedades
            private static InterfazDAL_ttccol301 _idalttccol301 = new InterfazDAL_ttccol301();
            private static InterfazDAL_tticol127 _idaltticol127 = new InterfazDAL_tticol127();
            private static InterfazDAL_ttisfc001 _idalttisfc001 = new InterfazDAL_ttisfc001();
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
            private DataTable _consultaCantidadLote = new DataTable();
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
                if (txtLote.Text.Trim().ToUpper() != String.Empty)
                {
                    Ent_tticol127 dataticol127 = new Ent_tticol127() { user = _operator };
                    _consultaLoteUsuario = _idaltticol127.listaRegistro_ObtieneAlmacen(ref dataticol127, ref strError);

                    if (_consultaLoteUsuario.Rows.Count > 0)
                    {
                        var lote = txtLote.Text.Trim().ToUpper();
                        _consultaItem = _idalttisfc001.findByPdnoArticulo(ref lote, ref strError);

                        if (_consultaItem.Rows.Count > 0)
                        {
                            var cwar = _consultaLoteUsuario.Rows[0]["BODEGA"].ToString();
                            var item = _consultaItem.Rows[0]["MITM"].ToString();

                            _consultaInformacion = _idaltwhinr140.consultaPorAlmacenItem(ref cwar, ref item, ref strError);

                            if (_consultaInformacion.Rows.Count > 0)
                            {
                                _consultaCantidadLote = _idaltwhinr140.consultaCantidadItemLote(ref cwar, ref item, ref strError);
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
                lblLote.Text = _textoLabels.readStatement(formName, _idioma, "lblLote");
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
                var item = _consultaItem.Rows[0]["MITM"].ToString();
                var itemdesc = _consultaItem.Rows[0]["DSCA"].ToString();
                var cantidadlote = _consultaCantidadLote.Rows[0]["STKS"].ToString();

                var table = String.Empty;

                table += String.Format("<hr/><table class='table table-bordered' style='font-size:13px; border:3px solid; border-style:outset;'><tr style='background-color: darkblue; color: white; font-weight:bold;'><td>{0}</td>"
                    + "<td colspan='2'>{1}</td></tr>", _idioma == "ESPAÑOL" ? "Almacen: " : "Warehouse: ", cwar + " - " + cwardesc);

                table += String.Format("<tr style='background-color: lightgray;'><td style='font-weight: bold;'>{0}</td><td colspan='2'>{1}</td></tr>"
                        , _idioma == "ESPAÑOL" ? "Articulo: " : "Item: ", item + " - " + itemdesc);

                table += String.Format("<tr style='background-color: white;'><td style='font-weight: bold;'>{0}</td><td colspan='2'>{1}</td></tr>"
                        , _idioma == "ESPAÑOL" ? "Inventario total: " : "Total inventory: ", cantidadlote);

                table += String.Format("<tr style='background-color: lightgray; font-weight:bold;'><b><td>{0}</td><td>{1}</td><td>{2}</td></b></tr>",
                        _idioma == "ESPAÑOL" ? "Ubicación " : "Location "
                        , _idioma == "ESPAÑOL" ? "Lote " : "Lot "
                        , _idioma == "ESPAÑOL" ? "Cantidad " : "Quantity");

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