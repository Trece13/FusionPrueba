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
using whusa.Entidades;
using whusa.Interfases;
using System.Configuration;
using System.Globalization;
using System.Threading;
using whusa.Utilidades;

namespace whusap.WebPages.InvMaterial
{
    public partial class whInvMaterialTakePrintLabel : System.Web.UI.Page
    {
        #region Propiedades

            protected static InterfazDAL_ttwhcol016 idal = new InterfazDAL_ttwhcol016();
            Ent_ttwhcol016 obj = new Ent_ttwhcol016();
            DataTable resultado = new DataTable();
            string DefaultZone = string.Empty;
            string strError = string.Empty;
            int ReqWareHouse = 0;

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
                txtCwar.Focus();
                this.SetFocus(Page.Form.UniqueID);

                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-CO");
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("es-CO");
                base.InitializeCulture();

                try { DefaultZone = ConfigurationManager.AppSettings["DefaultZone"].ToString(); }
                catch { DefaultZone = string.Empty; }
                try { ReqWareHouse = Convert.ToInt32(ConfigurationManager.AppSettings["WarehouseReq"].ToString()); }
                catch
                {
                    ReqWareHouse = 0;
                    lblCwar.Visible = true;
                    txtCwar.Visible = true;
                    txtCwar.Enabled = true;
                    trWareHouse.Visible = true;
                }


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

                    lblCwar.Visible = true;
                    txtCwar.Visible = true;

                    if (!String.IsNullOrEmpty(DefaultZone))
                    {
                        txtZone.Text = DefaultZone;
                        txtZone.Enabled = false;
                        ClientScript.RegisterStartupScript(this.GetType(), "validaInfo",
                                                                      "if(this.value != ''){ validaInfo(" + DefaultZone + ", '2', 'Contenido_" + LblZone.ID + "', this);}",
                                                                      true);

                    }
                    else { txtZone.Text = string.Empty; txtZone.Enabled = true; }

                    if (ReqWareHouse == 1) { trWareHouse.Visible = true; lblCwar.Visible = true; txtCwar.Visible = true; }
                    if (ReqWareHouse == 2) { trWareHouse.Visible = false; lblCwar.Visible = false; txtCwar.Visible = false; }

                    txtCwar.Attributes.Add("onblur", "if(this.value != ''){ validaInfo(this.value, '1', 'Contenido_" + lblCwar.ID + "', this);}");
                    txtZone.Attributes.Add("onblur", "if(this.value != ''){ validaInfo(this.value, '2', 'Contenido_" + LblZone.ID + "', this);}");
                    txtItem.Attributes.Add("onblur", "if(this.value != ''){ validaInfo(this.value, '3', 'Contenido_" + lblItem.ID + "', this);}");
                    txtLote.Attributes.Add("onblur", "if(this.value != ''){ validaInfo(this.value, '4', 'Contenido_" + lblLotCode.ID + "', this);}");

                    if (Session["IsPreviousPage"] == null) { Session.Clear(); }

                    string strTitulo = mensajes("encabezado");
                    Label control = (Label)Page.Controls[0].FindControl("lblPageTitle");
                    control.Text = strTitulo;
                    Page.Form.DefaultButton = btnSend.UniqueID;
                }
            }

        protected void btnSend_Click(object sender, EventArgs e)
            {
                //if ((string.IsNullOrEmpty(txtCwar.Text)) ||
                if ((string.IsNullOrEmpty(txtItem.Text))
                || (string.IsNullOrEmpty(txtQuantity.Text))
                || (string.IsNullOrEmpty(txtZone.Text)))
                {
                    lblError.Text = mensajes("formempty");
                    txtItem.Focus();
                    return;
                }

                if (hi_indLote.Value == "1" && string.IsNullOrEmpty(txtLote.Text.Trim()))
                {
                    lblError.Text = mensajes("lotempty");
                    txtLote.Focus();
                    return;
                }

                // Debe refrescar el consecutivo de la etiqueta

                obj.zone = txtZone.Text.Trim().ToUpperInvariant();
                int consecutivo = idal.TakeMaterialInv_verificaConsLabel_Param(ref obj, ref strError);
                hidden.Value = consecutivo.ToString(); ;

                obj.cwar = string.IsNullOrEmpty(txtCwar.Text.Trim()) ? " " : txtCwar.Text.ToUpperInvariant();
                obj.zone = txtZone.Text.ToUpperInvariant();
                obj.item = txtItem.Text.ToUpperInvariant();
                obj.clot = obj.clot = string.IsNullOrEmpty(txtLote.Text.Trim()) ? " " : txtLote.Text.ToUpperInvariant();
                obj.qtyr = Decimal.Parse(txtQuantity.Text, System.Globalization.CultureInfo.InvariantCulture);  //Convert.ToDecimal(txtQuantity.Text);
                obj.refcntd = 0;
                obj.refcntu = 0;
                obj.logn = Session["user"].ToString();
                obj.labl = obj.zone.Trim().ToUpperInvariant() + "-" + hidden.Value.PadLeft(5, '0');
                hidden.Value = (Convert.ToInt32(hidden.Value) + 1).ToString();

                List<Ent_ttwhcol016> parameterCollection = new List<Ent_ttwhcol016>();
                parameterCollection.Add(obj);
                //            CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
                //            lblError.Text = currentCulture.NumberFormat.CurrencyDecimalSeparator.ToString();

                int retorno = idal.insertarRegistro(ref parameterCollection, ref strError);

                // Buscar en web config cantidad de reintentos para guardar
                int contador;
                bool convert = int.TryParse(ConfigurationManager.AppSettings["numberRetryOnSave"].ToString(), out contador);

                if (!convert)
                    contador = 1;

                int cantidad = 0;

                // reintar tantas veces como se haya especificado
                while (retorno < 0 && cantidad < contador)
                {
                    retorno = idal.insertarRegistro(ref parameterCollection, ref strError);
                    cantidad++;
                }


                if (retorno < 1)
                {
                    lblError.Text = mensajes("errorsave");
                    return;
                }

                txtCwar.Text = string.Empty;
                lblCwar.Text = string.Empty;
                txtItem.Text = string.Empty;
                lblItem.Text = string.Empty;
                txtLote.Text = string.Empty;
                lblLotCode.Text = string.Empty;
                txtQuantity.Text = string.Empty;
                lblUnity.Text = string.Empty;
                //txtZone.Text = string.Empty;               


                DataTable resultado = idal.TakeMaterialInv_listaConsLabel_Param(ref obj, ref strError);
                DataRow reg = resultado.Rows[0];
                Session["FilaImprimir"] = reg;
                Session["descItem"] = hi_descItem.Value;
                Session["unidad"] = hi_unityItem.Value;

                StringBuilder script = new StringBuilder();
                script.Append("ventanaImp = window.open('../Labels/whInvPrintLabelPhysical.aspx', ");
                script.Append("'ventanaImp', 'menubar=0,resizable=0,width=580,height=450');");
                script.Append("ventanaImp.moveTo(30, 0);");
                //script.Append("setTimeout (ventanaImp.close(), 20000);");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "printTag", script.ToString(), true);

            }

        #endregion

        #region Metodos

        protected String ConsecLabel
        {
            get { return hidden.Value; }
            set { hidden.Value = value; }
        }

        protected String indLote
        {
            get { return hi_indLote.Value; }
            set { hi_indLote.Value = value; }
        }

        protected String descItem
        {
            get { return hi_descItem.Value; }
            set { hi_descItem.Value = value; }
        }

        protected String unityItem
        {
            get { return hi_unityItem.Value; }
            set { hi_unityItem.Value = value; }
        }

        [System.Web.Services.WebMethod()]
        public static string validaInfo(string valor, string tipo)
        {
            InterfazDAL_ttwhcol016 idal = new InterfazDAL_ttwhcol016();
            Ent_ttwhcol016 obj = new Ent_ttwhcol016();
            string strError = string.Empty;
            string retorno = string.Empty;
            DataTable resultado = new DataTable();
            string strSecondVal = string.Empty;
            if (tipo == "1") // Bodega
            {
                obj.cwar = valor.Trim().ToUpperInvariant();
                resultado = idal.TakeMaterialInv_verificaBodega_Param(ref obj, ref strError);
            }
            if (tipo == "2") // Zona
            {
                obj.zone = valor.Trim().ToUpperInvariant();
                resultado = idal.TakeMaterialInv_verificaZona_Param(ref obj, ref strError);
                if (resultado.Rows.Count > 0)
                {
                    int consecutivo = 0;
                    consecutivo = idal.TakeMaterialInv_verificaConsLabel_Param(ref obj, ref strError);
                    strSecondVal = "|" + consecutivo.ToString().Trim();

                    if (consecutivo < 1)
                    {
                        strError = _idioma == "INGLES" ? "Sequence Zone " + valor + " doesn't exist. Cannot Continue"
                            : "Secunecia de zona " + valor + " no existe, no se puede continuar.";
                    }
                }
            }
            if (tipo == "3") // Item
            {
                obj.item = valor.Trim().ToUpperInvariant();
                resultado = idal.TakeMaterialInv_verificaItem_Param(ref obj, ref strError);
                strError = _idioma == "INGLES" ? "Item code doesn´t exist. Cannot continue" : "Codigo de articulo no existe. No se puede continuar";
            }
            if (tipo == "4") // Lote
            {
                obj.clot = valor.Trim().ToUpperInvariant();
                resultado = idal.TakeMaterialInv_verificaLote_Param(ref obj, ref strError);
                strError = _idioma == "INGLES" ? "Lot Code doesn´t exist. Cannot Continue" : "Codigo de lote no existe. No se puede continuar";
            }



            // Validar si el numero de orden trae registros
            if (strError != string.Empty)
            {
                return "BAAN: " + strError;
            }

            if (resultado.Rows.Count > 0)
            {
                retorno = resultado.Rows[0]["DESCRIPCION"].ToString();

                if (tipo == "3")
                    retorno = resultado.Rows[0]["DESCRIPCION"].ToString().Trim() + "|" +
                              resultado.Rows[0]["UNIDAD"].ToString().Trim() + "|" +
                              resultado.Rows[0]["LOTE"].ToString().Trim();
            }

            return retorno + strSecondVal;
        }

        protected void CargarIdioma()
        {
            lblDescWarehouse.Text = _textoLabels.readStatement(formName, _idioma, "lblDescWarehouse");
            lblDescZone.Text = _textoLabels.readStatement(formName, _idioma, "lblDescZone");
            lblDescItem.Text = _textoLabels.readStatement(formName, _idioma, "lblDescItem");
            lblDescLotCode.Text = _textoLabels.readStatement(formName, _idioma, "lblDescLotCode");
            lblDescQuantity.Text = _textoLabels.readStatement(formName, _idioma, "lblDescQuantity");
            btnSend.Text = _textoLabels.readStatement(formName, _idioma, "btnSend");
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
}//
//

