using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Script.Serialization;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Configuration;
using System.Globalization;
using System.Threading;
using whusa.Entidades;
using whusa.Interfases;
using whusa.Utilidades;

namespace whusap.WebPages.Inventarios
{
    public partial class whInvInventoryfp : System.Web.UI.Page
    {
        #region Propiedades
            protected static InterfazDAL_ttccol301 idal301 = new InterfazDAL_ttccol301();
            protected static InterfazDAL_twhwmd300 idal300 = new InterfazDAL_twhwmd300();
            protected static InterfazDAL_twhcol002 idal002 = new InterfazDAL_twhcol002();
            protected static InterfazDAL_twhltc100 idal100 = new InterfazDAL_twhltc100();
            protected static InterfazDAL_twhinr140 idal140 = new InterfazDAL_twhinr140();
            protected static InterfazDAL_ttcibd001 idal001 = new InterfazDAL_ttcibd001();
            protected static InterfazDAL_twhcol015 idal015 = new InterfazDAL_twhcol015();
            Ent_twhcol015 obj015 = new Ent_twhcol015();
            Ent_ttcibd001 obj001 = new Ent_ttcibd001();
            Ent_twhinr140 obj140 = new Ent_twhinr140();
            Ent_twhltc100 obj100 = new Ent_twhltc100();
            Ent_twhcol002 obj002 = new Ent_twhcol002();
            Ent_twhwmd300 obj300 = new Ent_twhwmd300();
            Ent_ttccol301 obj301 = new Ent_ttccol301();
            DataTable dtt_twhwmd300 = new DataTable();
            DataTable dtt_twhcol002 = new DataTable();
            DataTable dtt_twhltc100 = new DataTable();
            DataTable dtt_twhinr140 = new DataTable();
            DataTable dtt_ttcibd001 = new DataTable();
            DataTable dtt_twhcol015 = new DataTable();
            string strError = string.Empty;

            //Manejo idioma
            private static Mensajes _mensajesForm = new Mensajes();
            private static LabelsText _textoLabels = new LabelsText();
            private static string formName;
            private static string globalMessages = "GlobalMessages";
            public static string _idioma;
        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
            {
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

                    if (Session["ddlIdioma"] != null)
                    {
                        _idioma = Session["ddlIdioma"].ToString();
                    }
                    else
                    {
                        _idioma = "INGLES";
                    }

                    CargarIdioma();

                    lblError.Visible = false;
                    lblResult.Visible = false;
                    divWarehouse.Visible = false;
                    divItem.Visible = false;
                    divCantidad.Visible = false;
                    txtLot.Enabled = false;

                    if (Session["IsPreviousPage"] == null) { Session.Clear(); }

                    string strTitulo = mensajes("encabezado");
                    Label control = (Label)Page.Controls[0].FindControl("lblPageTitle");
                    control.Text = strTitulo;
                    //Page.Form.DefaultButton = btnQuery.UniqueID;

                    if (lblIngreso.Text == "")
                    {
                        lblIngreso.Text = "1";
                    }

                }
            }

        protected void txtlocation_TextChanged(object sender, EventArgs e)
            {
                InsertarIngresoPagina();

                lblError.Text = string.Empty;
                lblError.Visible = false;
                lblResult.Text = string.Empty;
                lblResult.Visible = false;

                divWarehouse.Visible = false;
                lblWarehouse.Text = string.Empty;
                txtLot.Enabled = false;


                //BUSCAR ALMACEN DE LA UBICACION
                obj300.loca = txtlocation.Text.Trim().ToUpper();
                dtt_twhwmd300 = idal300.listaRegistro_ObtieneAlmacen(ref obj300, ref strError);

                if (dtt_twhwmd300.Rows.Count < 1)
                {
                    lblError.Visible = true;
                    lblError.Text = mensajes("incorrectlocation");
                    return;
                }
                else
                {
                    lblWarehouse.Text = dtt_twhwmd300.Rows[0]["ALM"].ToString();

                    ////Buscar Secuencia del Conteo
                    obj002.cwar = lblWarehouse.Text.Trim().ToUpper();
                    dtt_twhcol002 = idal002.listaRegistro_ObtieneConteo(ref obj002, ref strError);

                    if (dtt_twhcol002.Rows.Count < 1)
                    {
                        lblError.Visible = true;
                        lblError.Text = String.Format(mensajes("inactivecount"), obj002.cwar);
                        return;
                    }
                    else
                    {
                        lblstrconteo.Text = dtt_twhcol002.Rows[0]["CONT"].ToString();
                    }

                    if (lblstrconteo.Text == "0")
                    {
                        lblError.Visible = true;
                        lblError.Text = mensajes("countzero");
                        return;
                    }

                    txtLot.Enabled = true;
                    txtLot.Focus();
                }
            }

        protected void txtLot_TextChanged(object sender, EventArgs e)
            {
                ddlItem.Items.Clear();
                lblError.Text = string.Empty;
                lblError.Visible = false;

                if (txtLot.Text.Trim().Length == 0)
                {
                    lblError.Visible = true;
                    lblError.Text = mensajes("lotrequiered");
                    return;
                }

                obj100.clot = txtLot.Text.Trim().ToUpper();
                dtt_twhltc100 = idal100.listaRegistro_SiExiste(ref obj100, ref strError);
                if (dtt_twhltc100.Rows.Count < 1)
                {
                    lblError.Visible = true;
                    lblError.Text = mensajes("lotnotasociated");
                    return;
                }
                else
                {
                    obj140.loca = txtlocation.Text.Trim().ToUpper();
                    obj140.clot = txtLot.Text.Trim().ToUpper();

                    dtt_twhinr140 = idal140.listaRegistros_ObtieneItem(ref obj140, ref strError);
                    if (dtt_twhinr140.Rows.Count < 1)
                    {
                        //VALIDAR EL OBJ100 QUE YA LLEVE EL LOT
                        dtt_twhltc100 = idal100.listaRegistros_ObtieneItem(ref obj100, ref strError);
                        if (dtt_twhltc100.Rows.Count < 1)
                        {
                            lblError.Visible = true;
                            lblError.Text = mensajes("lotnotasociated");
                            return;
                        }
                        else
                        {
                            DataRow filatwhltc100 = dtt_twhltc100.NewRow();

                            filatwhltc100[0] = " ";
                            filatwhltc100[1] = "-- Select Item --";
                            dtt_twhltc100.Rows.InsertAt(filatwhltc100, 0);

                            dtt_twhltc100.DefaultView.Sort = "ITEM ASC";
                            ViewState["resultado"] = dtt_twhltc100;

                            ddlItem.DataSource = ViewState["resultado"] as DataTable;
                            ddlItem.DataTextField = "ITEM";
                            ddlItem.DataValueField = "ID";
                            ddlItem.DataBind();

                            if (dtt_twhltc100.Rows.Count == 2)
                            {
                                ddlItem.SelectedIndex = 1;
                                BuscarDescripcionItem();
                            }
                            else
                            {
                                ddlItem.SelectedIndex = 0;
                            }
                        }
                    }
                    else
                    {
                        DataRow filatwhinr140 = dtt_twhinr140.NewRow();

                        filatwhinr140[0] = " ";
                        filatwhinr140[1] = "-- Select Item --";
                        dtt_twhinr140.Rows.InsertAt(filatwhinr140, 0);

                        dtt_twhinr140.DefaultView.Sort = "ITEM ASC";
                        ViewState["resultado"] = dtt_twhinr140;

                        ddlItem.DataSource = ViewState["resultado"] as DataTable;
                        ddlItem.DataTextField = "ITEM";
                        ddlItem.DataValueField = "ID";
                        ddlItem.DataBind();

                        if (dtt_twhinr140.Rows.Count == 2)
                        {
                            ddlItem.SelectedIndex = 1;
                            BuscarDescripcionItem();
                        }
                        else
                        {
                            ddlItem.SelectedIndex = 0;
                        }
                    }

                    divItem.Visible = true;
                }
            }

        protected void ddlItem_SelectedIndexChanged(object sender, EventArgs e)
            {
                lblError.Text = string.Empty;
                lblError.Visible = false;

                if (ddlItem.SelectedIndex > 0)
                {
                    BuscarDescripcionItem();
                }
                else
                {

                    lblItem.Text = string.Empty;
                    lblUnity.Text = string.Empty;

                    divCantidad.Visible = false;
                    btnSave.Visible = false;

                    lblError.Visible = true;
                    lblError.Text = "Select item.";
                    return;
                }
            }

        protected void btnSave_Click(object sender, EventArgs e)
            {
                if (ddlItem.SelectedIndex > 0)
                {
                    NumberStyles style = NumberStyles.AllowDecimalPoint;
                    decimal cantidad = 0;
                    bool valido = false;
                    lblError.Text = "";
                    lblError.Visible = false;

                    List<Ent_twhcol015> parameterCollection015 = new List<Ent_twhcol015>();

                    valido = decimal.TryParse(txtCantidad.Text.Trim(), style, System.Globalization.CultureInfo.InvariantCulture, out cantidad);


                    if (!valido || cantidad < 0)
                    {
                        lblError.Text = mensajes("quantityzero");
                        return;
                    }

                    if (lblstrclot.Text == "1")
                    {
                        obj015.cwar = lblWarehouse.Text.Trim().ToUpper();
                        obj015.loca = txtlocation.Text.Trim().ToUpper();
                        obj015.pdno = txtLot.Text.Trim().ToUpper();

                        dtt_twhcol015 = idal015.listaRegistro_ObtieneSecuencia(ref obj015, ref strError);
                        if (dtt_twhcol015.Rows.Count < 1)
                        {
                            lblstrseq.Text = "1";
                        }
                        else
                        {
                            lblstrseq.Text = Convert.ToString(Convert.ToInt32(dtt_twhcol015.Rows[0]["SEQ"].ToString()) + 1);
                        }
                    }
                    else
                    {
                        obj015.cwar = lblWarehouse.Text.Trim().ToUpper();
                        obj015.loca = txtlocation.Text.Trim().ToUpper();

                        dtt_twhcol015 = idal015.listaRegistro_ObtieneSecuenciaNolote(ref obj015, ref strError);
                        if (dtt_twhcol015.Rows.Count < 1)
                        {
                            lblstrseq.Text = "1";
                        }
                        else
                        {
                            lblstrseq.Text = Convert.ToString(Convert.ToInt32(dtt_twhcol015.Rows[0]["SEQ"].ToString()) + 1);
                        }

                        obj015.pdno = " ";
                    }

                    obj015.sqnb = Convert.ToInt32(lblstrseq.Text.ToUpper());
                    obj015.mitm = "         " + ddlItem.SelectedValue.Trim().ToUpper();
                    obj015.dsca = lblItem.Text.Trim().ToUpper();
                    obj015.qtdl = cantidad;
                    obj015.cuni = lblUnity.Text.Trim().ToUpper();
                    obj015.mess = " ";
                    obj015.proc = 2;
                    obj015.coun = Convert.ToInt32(lblstrconteo.Text.ToUpper());
                    obj015.refcntd = 0;
                    obj015.refcntu = 0;
                    parameterCollection015.Add(obj015);

                    int retorno = idal015.insertarRegistro(ref parameterCollection015, ref strError);

                    if (!string.IsNullOrEmpty(strError))
                    {
                        lblError.Text = mensajes("errorsave");
                        lblError.Visible = true;
                        return;
                    }

                    divItem.Visible = false;
                    lblResult.Text = mensajes("msjsave");
                    lblResult.Visible = true;
                    Habilitar(false);
                    limpiar();
                }
            }


        #endregion

        #region Metodos

        void InsertarIngresoPagina()
        {
            if (lblIngreso.Text == "1")
            {
                lblIngreso.Text = "0";

                List<Ent_ttccol301> parameterCollection301 = new List<Ent_ttccol301>();

                obj301.user = Session["user"].ToString();
                obj301.come = mensajes("encabezado");
                obj301.refcntd = 0;
                obj301.refcntu = 0;
                parameterCollection301.Add(obj301);

                int retorno = idal301.insertarRegistro(ref parameterCollection301, ref strError);

                if (!string.IsNullOrEmpty(strError))
                {
                    lblError.Visible = true;
                    lblError.Text = strError;
                    return;
                }
            }
        }

        void BuscarDescripcionItem()
        {
            obj001.item = ddlItem.SelectedValue.Trim().ToUpper();
            dtt_ttcibd001 = idal001.listaRegistro_ObtieneClotUnitCOnv(ref obj001, ref strError);
            if (dtt_ttcibd001.Rows.Count < 1)
            {
                lblError.Visible = true;
                lblError.Text = mensajes("itemincorrect");
                return;
            }
            else
            {
                lblstrclot.Text = dtt_ttcibd001.Rows[0]["CLOT"].ToString();
            }

            dtt_ttcibd001 = idal001.listaRegistro_ObtieneDescripcionUnidad(ref obj001, ref strError);

            if (dtt_ttcibd001.Rows.Count < 1)
            {
                lblError.Visible = true;
                lblError.Text = String.Format(mensajes("notfound"), obj001.item);
                return;
            }
            else
            {
                lblItem.Text = dtt_ttcibd001.Rows[0]["DESCRIPCION"].ToString();
                lblUnity.Text = dtt_ttcibd001.Rows[0]["UNID"].ToString();
            }

            Habilitar(true);
        }

        void Habilitar(Boolean estado)
        {
            divWarehouse.Visible = estado;
            divCantidad.Visible = estado;
            txtlocation.ReadOnly = estado;
            txtLot.ReadOnly = estado;
            btnSave.Visible = estado;
        }

        void limpiar()
        {
            lblWarehouse.Text = string.Empty;
            txtlocation.Text = string.Empty;
            txtLot.Text = string.Empty;
            txtCantidad.Text = string.Empty;
            lblItem.Text = string.Empty;
            lblError.Text = string.Empty;
            lblstrconteo.Text = string.Empty;
            lblstrclot.Text = string.Empty;
            lblstrseq.Text = string.Empty;
            ddlItem.Items.Clear();
        }

        protected void CargarIdioma()
        {
            lblDescWarehouse.Text = _textoLabels.readStatement(formName, _idioma, "lblDescWarehouse");
            lblLocation.Text = _textoLabels.readStatement(formName, _idioma, "lblLocation");
            lblDescItem.Text = _textoLabels.readStatement(formName, _idioma, "lblDescItem");
            lblLot.Text = _textoLabels.readStatement(formName, _idioma, "lblLot");
            lblQty.Text = _textoLabels.readStatement(formName, _idioma, "lblQty");
            lblMaximumDigits.Text = _textoLabels.readStatement(formName, _idioma, "lblMaximumDigits");
            lblUnit.Text = _textoLabels.readStatement(formName, _idioma, "lblUnit");
            btnSave.Text = _textoLabels.readStatement(formName, _idioma, "btnSave");
            validateReturn.ErrorMessage = _textoLabels.readStatement(formName, _idioma, "regularQuantity");
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