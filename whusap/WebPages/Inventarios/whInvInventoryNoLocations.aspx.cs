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
    public partial class whInvInventoryNoLocations : System.Web.UI.Page
    {
        #region Propiedades
            protected static InterfazDAL_ttccol301 idal301 = new InterfazDAL_ttccol301();
            protected static InterfazDAL_twhwmd200 idal200 = new InterfazDAL_twhwmd200();
            protected static InterfazDAL_twhcol002 idal002 = new InterfazDAL_twhcol002();
            protected static InterfazDAL_ttcibd001 idal001 = new InterfazDAL_ttcibd001();
            protected static InterfazDAL_twhltc100 idal100 = new InterfazDAL_twhltc100();
            protected static InterfazDAL_twhcol015 idal015 = new InterfazDAL_twhcol015();
            Ent_twhcol015 obj015 = new Ent_twhcol015();
            Ent_twhltc100 obj100 = new Ent_twhltc100();
            Ent_ttcibd001 obj001 = new Ent_ttcibd001();
            Ent_twhcol002 obj002 = new Ent_twhcol002();
            Ent_twhwmd200 obj200 = new Ent_twhwmd200();
            Ent_ttccol301 obj301 = new Ent_ttccol301();
            DataTable dtt_twhwmd200 = new DataTable();
            DataTable dtt_twhcol002 = new DataTable();
            DataTable dtt_ttcibd001 = new DataTable();
            DataTable dtt_twhltc100 = new DataTable();
            DataTable dtt_twhcol015 = new DataTable();
            string strError = string.Empty;
            string strclot = string.Empty;

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
                    divCantidad.Visible = false;
                    divLote.Visible = false;
                    btnSave.Visible = false;

                    txtLot.ReadOnly = true;
                    txtItem.ReadOnly = true;

                    if (Session["IsPreviousPage"] == null) { Session.Clear(); }

                    string strTitulo = mensajes("encabezado");
                    Label control = (Label)Page.Controls[0].FindControl("lblPageTitle");
                    control.Text = strTitulo;
                    Page.Form.DefaultButton = btnSave.UniqueID;
                }
            }

        protected void txtWarehouse_TextChanged(object sender, EventArgs e)
            {
                InsertarIngresoPagina();

                lblError.Text = "";
                lblResult.Text = "";
                lblError.Visible = false;
                lblResult.Visible = false;

                txtItem.Text = string.Empty;
                txtLot.Text = string.Empty;
                txtItem.ReadOnly = true;
                txtLot.ReadOnly = true;

                obj200.cwar = txtWarehouse.Text.Trim().ToUpper();
                dtt_twhwmd200 = idal200.listaRegistro_ObtieneAlmacenLocation(ref obj200, ref strError);

                if (dtt_twhwmd200.Rows.Count < 1)
                {
                    lblError.Visible = true;
                    lblError.Text = mensajes("incorrectwarehouse");
                    return;
                }
                else
                {
                    lblStrUbicacion.Text = dtt_twhwmd200.Rows[0]["LOC"].ToString();

                    if (lblStrUbicacion.Text == "2")
                    {
                        obj002.cwar = txtWarehouse.Text.Trim().ToUpper();
                        dtt_twhcol002 = idal002.listaRegistro_ObtieneConteo(ref obj002, ref strError);

                        if (dtt_twhcol002.Rows.Count < 1)
                        {
                            lblError.Visible = true;
                            lblError.Text = String.Format(mensajes("countwarehouse"),obj002.cwar);
                            return;
                        }
                        else
                        {
                            lblstrconteo.Text = dtt_twhcol002.Rows[0]["CONT"].ToString();
                        }
                    }
                    else
                    {
                        lblError.Visible = true;
                        lblError.Text = mensajes("warehouselocations");
                        return;
                    }
                }

                txtItem.ReadOnly = false;
            }

        protected void txtItem_TextChanged(object sender, EventArgs e)
            {
                lblError.Text = "";
                lblResult.Text = "";
                lblError.Visible = false;
                lblResult.Visible = false;

                txtLot.Text = string.Empty;
                txtLot.ReadOnly = true;

                if (lblStrUbicacion.Text == "2")
                {
                    obj001.item = txtItem.Text.Trim().ToUpper();
                    dtt_ttcibd001 = idal001.listaRegistro_ObtieneClotUnitCOnv(ref obj001, ref strError);
                    if (dtt_ttcibd001.Rows.Count < 1)
                    {
                        lblError.Visible = true;
                        lblError.Text = mensajes("itemincorrect");
                        txtLot.Text = string.Empty;
                        //inHabilitar campo LOT
                        return;
                    }
                    else
                    {
                        lblstrclot.Text = dtt_ttcibd001.Rows[0]["CLOT"].ToString();
                        //strqty = objrs.Fields("strqty").Value
                    }

                    if (lblstrclot.Text == "1")
                    {
                        txtLot.ReadOnly = false;
                        divLote.Visible = true;
                        txtLot.Focus();
                    }
                    else
                    {
                        divLote.Visible = false;

                        obj001.item = txtItem.Text.Trim().ToUpper();
                        dtt_ttcibd001 = idal001.listaRegistro_ObtieneDescUnidNOLote(ref obj001, ref strError);

                        if (dtt_ttcibd001.Rows.Count < 1)
                        {
                            lblError.Visible = true;
                            lblError.Text = mensajes("itemincorrect");
                            return;
                        }
                        else
                        {
                            lblDescItem.Text = dtt_ttcibd001.Rows[0]["DESCRIPCION"].ToString();
                            lblUnity.Text = dtt_ttcibd001.Rows[0]["UNID"].ToString();
                        }

                        Habilitar(true);
                        txtCantidad.Focus();
                    }

                }
                else
                {
                    lblError.Visible = true;
                    lblError.Text = mensajes("warehouselocations");
                    return;
                }
            }

        protected void txtLot_TextChanged(object sender, EventArgs e)
            {
                lblError.Text = "";
                lblResult.Text = "";
                lblError.Visible = false;
                lblResult.Visible = false;

                if (lblstrclot.Text == "1")
                {
                    // VALIDAR QUE SE HAYA DIGITADO ALGO EN EL ITEM
                    if (txtLot.Text.Trim().Length == 0)
                    {
                        lblError.Visible = true;
                        lblError.Text = mensajes("incorrectlot");
                        return;
                    }

                    //VALIDAR EL LOTE
                    strclot = lblstrclot.Text;
                    strclot = strclot.Replace(",", "");
                    lblstrclot.Text = strclot;

                    obj100.clot = txtLot.Text.Trim().ToUpper();
                    obj100.item = txtItem.Text.Trim().ToUpper();
                    dtt_twhltc100 = idal100.listaRegistro_Clot(ref obj100, ref strError);
                    if (dtt_twhltc100.Rows.Count < 1)
                    {
                        lblError.Visible = true;
                        lblError.Text = mensajes("asociatedlot");
                        return;
                    }
                    else
                    {
                        obj001.item = txtItem.Text.Trim().ToUpper();
                        dtt_ttcibd001 = idal001.listaRegistro_ObtieneDescripcionUnidad(ref obj001, ref strError);
                        if (dtt_ttcibd001.Rows.Count < 1)
                        {
                            lblError.Visible = true;
                            lblError.Text = String.Format(mensajes("notfound"), obj001.item);
                            return;
                        }
                        else
                        {
                            lblDescItem.Text = dtt_ttcibd001.Rows[0]["DESCRIPCION"].ToString();
                            lblUnity.Text = dtt_ttcibd001.Rows[0]["UNID"].ToString();
                        }
                    }
                }
                Habilitar(true);
                txtCantidad.Focus();
            }

        protected void btnSave_Click(object sender, EventArgs e)
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
                    obj015.cwar = txtWarehouse.Text.Trim().ToUpper();
                    obj015.pdno = txtLot.Text.Trim().ToUpper();

                    dtt_twhcol015 = idal015.listaRegistro_ObtieneSecuenciaLOCATION(ref obj015, ref strError);
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
                    obj015.cwar = txtWarehouse.Text.Trim().ToUpper();
                    obj015.pdno = txtLot.Text.Trim().ToUpper();
                    obj015.mitm = txtItem.Text.Trim().ToUpper();

                    dtt_twhcol015 = idal015.listaRegistro_ObtieneSecuenciaNoloteLOCATION(ref obj015, ref strError);
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

                obj015.loca = " ";
                obj015.sqnb = Convert.ToInt32(lblstrseq.Text.ToUpper());
                obj015.mitm = "         " + txtItem.Text.Trim().ToUpper();
                obj015.dsca = lblDescItem.Text.Trim().ToUpper();
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


                divLote.Visible = false;
                lblResult.Text = mensajes("msjsave");
                lblResult.Visible = true;
                Habilitar(false);
                limpiar();

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

        void Habilitar(Boolean estado)
        {
            divCantidad.Visible = estado;
            txtWarehouse.ReadOnly = estado;
            txtItem.ReadOnly = estado;
            txtLot.ReadOnly = estado;
            btnSave.Visible = estado;
        }

        void limpiar()
        {
            txtWarehouse.Text = string.Empty;
            txtItem.Text = string.Empty;
            txtLot.Text = string.Empty;
            txtCantidad.Text = string.Empty;
            lblDescItem.Text = string.Empty;
            lblError.Text = string.Empty;
            lblstrconteo.Text = string.Empty;
            lblstrclot.Text = string.Empty;
            lblstrseq.Text = string.Empty;
            lblStrUbicacion.Text = string.Empty;
        }

        protected void CargarIdioma()
        {
            lblDescWarehouse.Text = _textoLabels.readStatement(formName, _idioma, "lblDescWarehouse");
            lblItem.Text = _textoLabels.readStatement(formName, _idioma, "lblItem");
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