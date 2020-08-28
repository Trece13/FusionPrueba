using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.Globalization;
using System.Drawing;
using whusa.Interfases;
using whusa.Entidades;
using System.Configuration;
using whusa.Utilidades;

namespace whusap.WebPages.InvTrans
{
    public partial class whInvGenerateUI : System.Web.UI.Page
    {
        #region Propiedades
            protected static InterfazDAL_twhwmd300 _idaltwhwmd300 = new InterfazDAL_twhwmd300();
            protected static InterfazDAL_twhcol120 _idaltwhcol120 = new InterfazDAL_twhcol120();
            private static InterfazDAL_ttccol301 _idalttccol301 = new InterfazDAL_ttccol301();
            private static Mensajes _mensajesForm = new Mensajes();
            private static LabelsText _textoLabels = new LabelsText();
            public static string _idioma;
            protected static string _operator;
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

                    Label control = (Label)Page.Controls[0].FindControl("lblPageTitle");
                    lblError.Text = String.Empty;
                    lblConfirm.Text = String.Empty;

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

                    //Session["user"] = "JCASTRO";
                    //Session["username"] = "JAVIER CASTRO";



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

                    var username = Session["username"] == null ? Request.QueryString["Valor3"].ToString() : Session["username"].ToString();

                    txtOperator.Text = _operator + " - " + username;
                    divUniqueIdentifier.Visible = false;
                    lblHora.Text = DateTime.Now.ToString();
                }
            }

        protected void btnGenerateUI_Click(object sender, EventArgs e)
            {
                if (validarCampos())
                {
                    var CWARSOURCE = txtSourceWareHouse.Text.ToUpper();
                    var CWARDESTINATION = txtDestinationWareHouse.Text.ToUpper();
                    var respuesta = _idaltwhwmd300.consultaSourceAndDestinationWarehouse(ref CWARSOURCE, ref CWARDESTINATION, ref strError);

                    var validationSource = false;
                    var validationDestination = false;

                    //Validate Source and Destination Warehouse
                    if (respuesta.Rows.Count > 0)
                    {
                        for (int i = 0; i < respuesta.Rows.Count; i++)
                        {
                            if (respuesta.Rows[i]["FILTRO"].ToString().ToUpper() == "SOURCE")
                            {
                                validationSource = true;
                            }
                            else if (respuesta.Rows[i]["FILTRO"].ToString().ToUpper() == "DESTINATION")
                            {
                                validationDestination = true;
                            }
                        }

                        if (!validationSource)
                        {
                            lblError.Text = mensajes("sourceblank");
                            return;
                        }
                        else if (!validationDestination)
                        {
                            lblError.Text = mensajes("destinationblank");
                            return;
                        }

                        var dateNow = DateTime.Now;
                        var ui = String.Concat(txtTruckID.Text.ToUpper(), dateNow.ToString("MMddyyyy"), dateNow.Hour.ToString().Length == 1 ? "0" + dateNow.Hour.ToString() : dateNow.Hour.ToString()
                            , dateNow.Minute.ToString().Length == 1 ? "0" + dateNow.Minute.ToString() : dateNow.Minute.ToString());

                        //Save data in table whcol120
                        Ent_twhcol120 datatwhcol120 = new Ent_twhcol120()
                        {
                            unid = ui,
                            date = dateNow.ToString(),
                            logn = _operator,
                            idtr = txtTruckID.Text.Trim().ToUpper(),
                            whso = txtSourceWareHouse.Text.Trim().ToUpper(),
                            whta = txtDestinationWareHouse.Text.Trim().ToUpper(),
                            shpo = " ",
                            enpi = 2,
                            enre = 2,
                            rcno = " ",
                            prnt = 2,
                            proo = 3,
                            pror = 3,
                            mes1 = " ",
                            mes2 = " ",
                            refcntd = 0,
                            refcntu = 0
                        };

                        var resptwhcol120 = _idaltwhcol120.insertarRegistro(ref datatwhcol120, ref strError);

                        if (resptwhcol120 > 0)
                        {
                            txtUI.Text = ui;
                            divUniqueIdentifier.Visible = true;
                            lblConfirm.Text = mensajes("msjsave");
                            Response.Redirect(String.Format("whInvPrintUI.aspx?tipoFormulario=print&UID={0}", ui));
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
                        lblError.Text = mensajes("warehouseblank");
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

        private bool validarCampos()
        {
            lblError.Text = String.Empty;
            lblConfirm.Text = String.Empty;

            var validate = true;
            if (txtSourceWareHouse.Text == String.Empty)
            {
                txtSourceWareHouse.BorderColor = Color.Red;
                validate = false;
            }
            else { txtSourceWareHouse.BorderColor = Color.Gray; }

            if (txtDestinationWareHouse.Text == String.Empty)
            {
                txtDestinationWareHouse.BorderColor = Color.Red;
                validate = false;
            }
            else { txtDestinationWareHouse.BorderColor = Color.Gray; }

            if (txtTruckID.Text == String.Empty)
            {
                txtTruckID.BorderColor = Color.Red;
                validate = false;
            }
            else { txtTruckID.BorderColor = Color.Gray; }

            return validate;
        }

        protected void CargarIdioma()
        {
            lblPagina.Text = _textoLabels.readStatement(formName, _idioma, "lblPagina");
            lblOperator.Text = _textoLabels.readStatement(formName, _idioma, "lblOperator");
            lblTruck.Text = _textoLabels.readStatement(formName, _idioma, "lblTruck");
            lblSource.Text = _textoLabels.readStatement(formName, _idioma, "lblSource");
            lblDestination.Text = _textoLabels.readStatement(formName, _idioma, "lblDestination");
            btnGenerateUI.Text = _textoLabels.readStatement(formName, _idioma, "btnGenerateUI");
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