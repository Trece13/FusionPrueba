using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.Globalization;
using whusa.Interfases;
using System.Text;
using System.Configuration;
using whusa.Entidades;
using whusa.Utilidades;

namespace whusap.WebPages.InvTrans
{
    public partial class whInvPrintUI : System.Web.UI.Page
    {
        #region Propiedades
            protected static InterfazDAL_twhcol120 _idaltwhcol120 = new InterfazDAL_twhcol120();
            private static InterfazDAL_ttccol301 _idalttccol301 = new InterfazDAL_ttccol301();
            private static Mensajes _mensajesForm = new Mensajes();
            private static LabelsText _textoLabels = new LabelsText();
            private static string _idioma;
            private static string _operator;
            private static string tipoFormulario;
            string strError = string.Empty;
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

                    tipoFormulario = Request.QueryString["tipoFormulario"];
                    tipoFormulario = tipoFormulario.ToUpper();
                    lblError.Text = String.Empty;

                    if (Session["user"] == null)
                    {
                        if (Request.QueryString["Valor1"] == null || Request.QueryString["Valor1"] == "")
                        {
                            Response.Redirect(ConfigurationManager.AppSettings["UrlBase"] + "/WebPages/Login/whLogIni.aspx");
                        }
                        else
                        {
                            _operator = Request.QueryString["Valor1"];
                            Session["user"] = _operator;
                            Session["logok"] = "OKYes";
                        }
                    }
                    else
                    {
                        _operator = Session["user"].ToString();
                    }


                    if (Session["ddlIdioma"] == null)
                    {
                        Session["ddlIdioma"] = "INGLES";
                    }

                    _idioma = Session["ddlIdioma"].ToString();

                    CargarIdioma();

                    //if (_idioma == "ESPAÑOL")
                    //{
                    //    IdiomaEspañol();
                    //}
                    //else
                    //{
                    //    IdiomaIngles();
                    //}

                    if (tipoFormulario != null)
                    {
                        switch (tipoFormulario)
                        {
                            case "PRINT":
                                //strTitulo = "Print Unique Id";
                                //txtUniqueID.Visible = false;
                                if (Request.QueryString["UID"] != null)
                                {
                                    txtUniqueID.Text = Request.QueryString["UID"];
                                    btnPrint_Click(btnPrint, EventArgs.Empty);
                                }

                                cargarUniquesID();
                                break;
                            case "REPRINT":
                                //strTitulo = "Reprint Unique Id";
                                txtUniqueID.Visible = true;
                                break;
                            default:
                                tipoFormulario = "PRINT";
                                //strTitulo = "Print Unique Id";
                                txtUniqueID.Visible = false;
                                break;
                        }
                    }

                    Label control = (Label)Page.Controls[0].FindControl("lblPageTitle");
                    string strTitulo = tipoFormulario == "PRINT" ? mensajes("encabezadoprint") : mensajes("encabezadoreprint");
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

        protected void btnPrint_Click(object sender, EventArgs e)
            {
                lblError.Text = String.Empty;

                var tipoFormulario = Request.QueryString["tipoFormulario"];
                tipoFormulario = tipoFormulario.ToUpper();

                if (txtUniqueID.Text != "")
                {
                    var uniqueID = String.Empty;

                    if (tipoFormulario == "REPRINT")
                    {
                        uniqueID = txtUniqueID.Text.Trim().ToUpper();
                    }
                    else
                    {
                        uniqueID = txtUniqueID.Text.ToUpper();
                    }

                    var dataID = _idaltwhcol120.validarRegistroByUniqueId(ref uniqueID, ref strError);

                    if (dataID.Rows.Count > 0)
                    {
                        Session["resultado"] = dataID.Rows[0];
                        Session["tipoFormulario"] = tipoFormulario;

                        var responseUpdate = false;

                        if (tipoFormulario == "PRINT")
                        {
                            responseUpdate = _idaltwhcol120.updateFieldPrint(ref uniqueID, ref strError);
                        }
                        else
                        {
                            responseUpdate = true;
                        }


                        if (responseUpdate)
                        {
                            StringBuilder script = new StringBuilder();
                            script.Append("ventanaImp = window.open('../Labels/whInvLabelUI.aspx', ");
                            script.Append("'ventanaImp', 'menubar=0,resizable=0,width=580,height=450');");
                            script.Append("ventanaImp.moveTo(30, 0);");
                            //script.Append("setTimeout (ventanaImp.close(), 20000);");
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "printTag", script.ToString(), true);
                        }
                        else
                        {
                            lblError.Text = mensajes("error");
                            return;
                        }
                    }
                    else
                    {
                        lblError.Text = mensajes("blank");
                        return;
                    }
                }
                else
                {
                    lblError.Text = mensajes("palletblank");
                    return;
                }
            }

        #endregion

        #region Metodos

        protected void cargarUniquesID()
        {
            //var responseQuery = _idaltwhcol120.consultaUINotPrinted(ref strError);

            //if (responseQuery.Rows.Count > 0)
            //{
            //    ddlUniqueID.DataSource = responseQuery;
            //    ddlUniqueID.DataTextField = "ID";
            //    ddlUniqueID.DataValueField = "ID";
            //    ddlUniqueID.DataBind();
            //    ddlUniqueID.Items.Insert(0,new ListItem("-- Seleccione --",""));
            //}
            //else
            //{
            //    lblError.Text = "No records were found without print";
            //    return;
            //}
        }

        protected void CargarIdioma()
        {
            lblScan.Text = _textoLabels.readStatement(formName, _idioma, "lblScan");
            btnPrint.Text = _textoLabels.readStatement(formName, _idioma, "btnPrint");
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