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

namespace whusap.WebPages.InvMaterial
{
    public partial class whInvSpareDelivery : System.Web.UI.Page
    {
        string strError = string.Empty;
        DataTable resultado = new DataTable();

        protected static InterfazDAL_ttscol100 idal = new InterfazDAL_ttscol100();
        Ent_ttscol100 obj = new Ent_ttscol100();
        private static string globalMessages = "GlobalMessages";

        public static string Pleasereviewallitemsbeforesave = mensajes("Pleasereviewallitemsbeforesave");
        public static string ViewStateisnullPleasecontacttosupport = mensajes("ViewStateisnullPleasecontacttosupport");
        public static string SpareDeliverywassuccesfullyregistered = mensajes("SpareDeliverywassuccesfullyregistered");
        public static string ItemcantbeemptyCannotContinue = mensajes("ItemcantbeemptyCannotContinue");
        public static string LocationcantbeemptyCannotContinue = mensajes("LocationcantbeemptyCannotContinue");
        public static string LocationdonthavewarehouseassociatedCannotContinue = mensajes("LocationdonthavewarehouseassociatedCannotContinue");
        public static string QuantitymustbegreatherthanzeroCannotContinue = mensajes("QuantitymustbegreatherthanzeroCannotContinue");

        StringBuilder script;

        protected void Page_PreRender(object sender, EventArgs e)
        {
            registerScript();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Cambiar cultura para manejo de separador decimal
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-CO");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("es-CO");
            base.InitializeCulture();

            if (!IsPostBack)
            {
                if (Session["IsPreviousPage"] == null) { Session.Clear(); }

                string strTitulo = "Spare Delivery";
                Label control = (Label)Page.Controls[0].FindControl("lblPageTitle");
                control.Text = strTitulo;              
                Page.Form.DefaultButton = btnSend.UniqueID;
                divOptButtons.Visible = false;
            }
            this.txtWorkOrder.Attributes.Add("onkeypress", "button_click(this," + this.txtWorkOrder.ClientID + ")");
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            strError = string.Empty;

            if (string.IsNullOrEmpty(txtWorkOrder.Text.Trim()))
            {
                RequiredField.Enabled = true;
                RequiredField.IsValid = false;
                RequiredField.Validate();
                txtWorkOrder.Focus();
                return;
            }

            lblResult.Text = string.Empty;

            txtWorkOrder.Text = txtWorkOrder.Text.ToUpperInvariant();

            if (string.IsNullOrEmpty(txtWorkOrder.Text.Trim()))
                return;

            obj.orno = txtWorkOrder.Text.ToUpperInvariant();
            resultado = idal.spareDelivery_verificaOrdenes_Param(ref obj, ref strError);


            if (resultado.Rows.Count < 1 || !string.IsNullOrEmpty(strError))
            {
                lblResult.Text = string.Format("Order Work {0} : {1}", obj.orno.Trim(), strError);
                return;
            }

            createObjectTable();
            divOptButtons.Visible = true;

        }

        protected void btnAddRow_Click(object sender, EventArgs e)
        {
            btnContinue.Visible = true;

            if (verifyRow())
            {
                addTableRow();
                ValuesItem = "";
                ValuesLocation = "";
            }
        }

        protected void grdRecords_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                ((TextBox)e.Row.Cells[2].FindControl("txtItem")).
                                         Attributes.Add("onblur", "if(this.value != '') { validaInfo(this.value, '1', " + e.Row.RowIndex + ", this);}");


                ((TextBox)e.Row.Cells[2].FindControl("txtLocation")).
                                         Attributes.Add("onblur", "if(this.value != '') { validaInfo(this.value, '2'," + e.Row.RowIndex + ", this);}");


                TextBox toReturn = (TextBox)e.Row.Cells[7].FindControl("toReturn");

                ((TextBox)e.Row.Cells[7].FindControl("toReturn")).Attributes.Add("onfocus", "limpiar(this);");
            }
        }


        protected void btnContinue_Click(object sender, EventArgs e)
        {
            btnSave.Visible = true;
            btnAddRow.Enabled = false;
            SetRowData();
            
            lblResult.Text = Pleasereviewallitemsbeforesave;
        }

        protected void grdRecords_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            SetRowData();

            if (ViewState["tablaTemp"] != null)
            {
                DataTable tablaTemp = (DataTable)ViewState["tablaTemp"];
                DataRow fila = null;
                int rowIndex = Convert.ToInt32(e.RowIndex);
                if (tablaTemp.Rows.Count >= 1)
                {
                    tablaTemp.Rows.Remove(tablaTemp.Rows[rowIndex]);
                    fila = tablaTemp.NewRow();
                    if (tablaTemp.Rows.Count < 1) { createObjectTable();  }

                    ViewState["tablaTemp"] = tablaTemp;
                    grdRecords.DataSource = tablaTemp;
                    grdRecords.DataBind();

                    for (int i = 0; i < grdRecords.Rows.Count - 1; i++)
                    {
                        grdRecords.Rows[i].Cells[0].Text = Convert.ToString(i + 1);
                    }
                    SetTablePreviousData();
                }
            }
            else
            {
                
                lblResult.Text = ViewStateisnullPleasecontacttosupport;
            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (ViewState["tablaTemp"] != null)
            {
                DataTable tablaTemp = ViewState["tablaTemp"] as DataTable;

                if (tablaTemp.Rows.Count >= 1)
                {
                    List<Ent_ttscol100> parameterCollection = new List<Ent_ttscol100>(); 
                    
                    for (int i = 1; i <= tablaTemp.Rows.Count; i++)
                    {

                        obj = new Ent_ttscol100();
                        obj.orno = txtWorkOrder.Text.Trim().ToUpperInvariant();
                        obj.item = tablaTemp.Rows[i -1]["T$ITEM"].ToString();
                        obj.loca = tablaTemp.Rows[i -1]["T$LOCA"].ToString();
                        obj.cwar = tablaTemp.Rows[i -1]["T$CWAR"].ToString();
                        string toreturn = tablaTemp.Rows[i -1]["T$QDEL"].ToString();
                        obj.qdel = Decimal.Parse(toreturn, System.Globalization.CultureInfo.InvariantCulture);
                        obj.logn = Session["user"].ToString();
                        obj.mess = " ";
                        obj.refcntd = 0;
                        obj.refcntu = 0;
                        obj.cusr = " ";
                        obj.conf = 2;
                        parameterCollection.Add(obj);
                    }

                    idal.insertarRegistro(ref parameterCollection, ref strError);
                    printResult.Visible = true;

                    if (strError != string.Empty)
                    {
                        lblResult.Text = strError;
                        throw new System.InvalidOperationException(strError);
                    }
                    
                    lblResult.Text = SpareDeliverywassuccesfullyregistered;

                    createObjectTable();
                    btnAddRow.Enabled = true;
               }
            }
            else
            {
                
                lblResult.Text = ViewStateisnullPleasecontacttosupport
;
            }
        }

        protected void createObjectTable()
        {
            DataTable tablaTemp = new DataTable();
            DataRow fila = null;
            tablaTemp.Columns.Add(new DataColumn("ID", typeof(string)));
            tablaTemp.Columns.Add(new DataColumn("T$ITEM", typeof(string)));
            tablaTemp.Columns.Add(new DataColumn("DESCRIPCION", typeof(string)));
            tablaTemp.Columns.Add(new DataColumn("T$LOCA", typeof(string)));
            tablaTemp.Columns.Add(new DataColumn("T$CWAR", typeof(string)));
            tablaTemp.Columns.Add(new DataColumn("UNIDAD", typeof(string)));
            tablaTemp.Columns.Add(new DataColumn("T$QDEL", typeof(decimal)));
            fila = tablaTemp.NewRow();

            fila["ID"] = 1;
            fila["T$ITEM"] = string.Empty;
            fila["DESCRIPCION"] = string.Empty;
            fila["T$LOCA"] = string.Empty;
            fila["T$CWAR"] = string.Empty;
            fila["UNIDAD"] = string.Empty;
            fila["T$QDEL"] = 0;
            tablaTemp.Rows.Add(fila);

            ViewState["tablaTemp"] = tablaTemp;

            grdRecords.DataSource = tablaTemp;
            grdRecords.DataBind();

        }

        protected void addTableRow()
        {
            int rowIndex = 0;

            if (ViewState["tablaTemp"] != null)
            {
                DataTable tablaTemp = (DataTable)ViewState["tablaTemp"];
                DataRow fila = null;
                if (tablaTemp.Rows.Count > 0)
                {
                    string txtUni = string.Empty;
                    string txtDesc = string.Empty;
                    for (int i = 1; i <= tablaTemp.Rows.Count; i++)
                    {

                        String[] itemArr = hiddenIDesc.Value.ToString().Split('|');

                        TextBox txtItem = (TextBox)grdRecords.Rows[rowIndex].Cells[2].FindControl("txtItem");
                        TextBox txtLocation = (TextBox)grdRecords.Rows[rowIndex].Cells[4].FindControl("txtLocation");
                        string txtcwar = hiddenLDesc.Value;
                        TextBox txttoReturn = (TextBox)grdRecords.Rows[rowIndex].Cells[7].FindControl("toReturn");


                        if (!string.IsNullOrEmpty(tablaTemp.Rows[i - 1]["DESCRIPCION"].ToString()))
                            txtDesc = tablaTemp.Rows[i - 1]["DESCRIPCION"].ToString();
                        else
                            txtDesc = itemArr[0];

                        if (!string.IsNullOrEmpty(tablaTemp.Rows[i - 1]["UNIDAD"].ToString()))
                            txtUni = tablaTemp.Rows[i - 1]["UNIDAD"].ToString();
                        else
                            txtUni = itemArr[1];

                        fila = tablaTemp.NewRow();
                        fila["ID"] = i + 1;
                        fila["T$QDEL"] = 0;

                        tablaTemp.Rows[i - 1]["T$ITEM"] = txtItem.Text;
                        tablaTemp.Rows[i - 1]["DESCRIPCION"] = txtDesc;
                        tablaTemp.Rows[i - 1]["T$LOCA"] = txtLocation.Text;
                        tablaTemp.Rows[i - 1]["T$CWAR"] = txtcwar;
                        tablaTemp.Rows[i - 1]["UNIDAD"] = txtUni;
                        tablaTemp.Rows[i - 1]["T$QDEL"] = txttoReturn.Text;
                        rowIndex++;
                    }

                    tablaTemp.Rows.Add(fila);
                    ViewState["tablaTemp"] = tablaTemp;

                    grdRecords.DataSource = tablaTemp;
                    grdRecords.DataBind();
                }
            }
            else
            {
                
                lblResult.Text = ViewStateisnullPleasecontacttosupport;
            }
            SetTablePreviousData();
        }

        protected bool verifyRow()
        {

            int lastRow = (grdRecords.Rows.Count - 1);
            String[] itemArr = hiddenIDesc.Value.ToString().Split('|');
            TextBox txtItem = (TextBox)grdRecords.Rows[lastRow].Cells[2].FindControl("txtItem");
            TextBox txtLocation = (TextBox)grdRecords.Rows[lastRow].Cells[4].FindControl("txtLocation");
            string txtcwar = hiddenLDesc.Value;
            TextBox txttoReturn = (TextBox)grdRecords.Rows[lastRow].Cells[7].FindControl("toReturn");

            if (string.IsNullOrEmpty(Server.HtmlDecode(txtItem.Text.Trim())))
            {
                
                lblResult.Text = ItemcantbeemptyCannotContinue;
                txtItem.Text = string.Empty;
                txtItem.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(Server.HtmlDecode(txtLocation.Text.Trim())))
            {
                
                lblResult.Text = LocationcantbeemptyCannotContinue;
                txtLocation.Text = string.Empty;
                txtLocation.Focus();

                return false;
            }
            if (string.IsNullOrEmpty(txtcwar.Trim()))
            {
                
                lblResult.Text = LocationdonthavewarehouseassociatedCannotContinue;
                return false;
            }
            if (Convert.ToDecimal(txttoReturn.Text) < 1)
            {
                
                lblResult.Text = QuantitymustbegreatherthanzeroCannotContinue;
                txttoReturn.Text = "0";
                txttoReturn.Focus();

                return false;
            }

            return true;

        }

        private void SetRowData()
        {
            int rowIndex = 0;

            if (ViewState["tablaTemp"] != null)
            {
                DataTable tablaTemp = (DataTable)ViewState["tablaTemp"];
                //DataRow fila = null;
                if (tablaTemp.Rows.Count > 0)
                {
                    for (int i = 1; i <= tablaTemp.Rows.Count; i++)
                    {
                        string txtUni = string.Empty;
                        string txtDesc = string.Empty;
                        String[] itemArr = hiddenIDesc.Value.ToString().Split('|');

                        TextBox txtItem = (TextBox)grdRecords.Rows[rowIndex].Cells[2].FindControl("txtItem");

                        if (!string.IsNullOrEmpty(tablaTemp.Rows[i - 1]["DESCRIPCION"].ToString()))
                            txtDesc = tablaTemp.Rows[i - 1]["DESCRIPCION"].ToString();
                        else
                          txtDesc = itemArr[0];

                        if (!string.IsNullOrEmpty(tablaTemp.Rows[i - 1]["UNIDAD"].ToString()))
                            txtUni = tablaTemp.Rows[i - 1]["UNIDAD"].ToString();
                        else
                            txtUni = itemArr[1];

                        TextBox txtLocation = (TextBox)grdRecords.Rows[rowIndex].Cells[4].FindControl("txtLocation");
                        string txtcwar = hiddenLDesc.Value;
                        TextBox txttoReturn = (TextBox)grdRecords.Rows[rowIndex].Cells[7].FindControl("toReturn");

                        //fila = tablaTemp.NewRow();
                        tablaTemp.Rows[i - 1]["T$ITEM"] = txtItem.Text;
                        tablaTemp.Rows[i - 1]["DESCRIPCION"] = txtDesc;
                        tablaTemp.Rows[i - 1]["T$LOCA"] = txtLocation.Text;
                        tablaTemp.Rows[i - 1]["T$CWAR"] = txtcwar;
                        tablaTemp.Rows[i - 1]["UNIDAD"] = txtUni;
                        tablaTemp.Rows[i - 1]["T$QDEL"] = txttoReturn.Text;
                        rowIndex++;
                    }

                    ViewState["CurrentTable"] = tablaTemp;
                }
            }
            else
            {
                
                lblResult.Text = ViewStateisnullPleasecontacttosupport;
            }
        }

        private void SetTablePreviousData()
        {
            int rowIndex = 0;
            if (ViewState["tablaTemp"] != null)
            {
                DataTable tablaTemp = (DataTable)ViewState["tablaTemp"];
                if (tablaTemp.Rows.Count > 0)
                {
                    for (int i = 0; i < tablaTemp.Rows.Count; i++)
                    {

                        TextBox txtItem = (TextBox)grdRecords.Rows[rowIndex].Cells[2].FindControl("txtItem");
                        Label lblDescription = (Label)grdRecords.Rows[rowIndex].Cells[3].FindControl("lblDescription");
                        TextBox txtLocation = (TextBox)grdRecords.Rows[rowIndex].Cells[4].FindControl("txtLocation");
                        Label lblWareHouse = (Label)grdRecords.Rows[rowIndex].Cells[5].FindControl("lblWareHouse");
                        Label lblUnit = (Label)grdRecords.Rows[rowIndex].Cells[6].FindControl("lblUnit");
                        TextBox txttoReturn = (TextBox)grdRecords.Rows[rowIndex].Cells[7].FindControl("toReturn");

                        grdRecords.Rows[i].Cells[0].Text = Convert.ToString(i + 1);
                        txtItem.Text = tablaTemp.Rows[i]["T$ITEM"].ToString();
                        lblDescription.Text = tablaTemp.Rows[i]["DESCRIPCION"].ToString();
                        txtLocation.Text = tablaTemp.Rows[i]["T$LOCA"].ToString();
                        lblWareHouse.Text = tablaTemp.Rows[i]["T$CWAR"].ToString();
                        lblUnit.Text = tablaTemp.Rows[i]["UNIDAD"].ToString();
                        txttoReturn.Text =  tablaTemp.Rows[i]["T$QDEL"].ToString();

                        rowIndex++;
                    }
                }
            }
        }


        protected void registerScript()
        {
            // Crear el script para la ejecucion de la forma
            script = new StringBuilder();
            script.Append("<script type=\"text/javascript\">function button_click(objTextBox, objTextID) {");
            script.Append("if(window.event.keyCode==13)");
            script.Append("{");
            script.Append("document.getElementById(objTextID).focus();");
            script.Append("}}");
            script.Append("</script>");

            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "button_clickFrom", script.ToString(), false);

            script = new StringBuilder();
            script.Append("<script type=\"text/javascript\">");
            script.Append("function validateMov(valorCant, valorClear, controlCompare, objFocus) {");
            script.Append("var cantAct = parseFloat(valorCant).toFixed(6);   ");
            script.Append("var cantMov = parseFloat(valorClear).toFixed(6);  ");
            script.Append("var obj = document.getElementById(controlCompare);");
            script.Append("if (Page_ClientValidate()) {");
            script.Append("if (parseFloat(cantMov) > parseFloat(cantAct)) {");
            script.Append("obj.isvalid = false;");
            script.Append("objFocus.focus();   ");
            script.Append("Page_IsValid = false;");
            script.Append("Page_BlockSubmit = false;");
            script.Append("alert('Quantity to return cannot be greater than Actual Quantity');");
            script.Append("return false;");
            script.Append("}");
            script.Append("}");
            script.Append("Page_IsValid = true; ");
            script.Append("Page_BlockSubmit = true;");
            script.Append("return true;");
            script.Append("}");
            script.Append("</script>");

            ScriptManager.RegisterClientScriptBlock(Page, GetType(), "validateMov", script.ToString(), false);

            script = new StringBuilder();
            script.Append("<script type=\"text/javascript\">");
            script.Append("function limpiar(obj) {");
            script.Append("var objControl = document.getElementById(obj.id);");
            script.Append("objControl.value = '0';");
            script.Append("objControl.focus();");
            script.Append("}");
            script.Append("</script>");

            ScriptManager.RegisterClientScriptBlock(Page, GetType(), "limpiar", script.ToString(), false);
        }


        protected String ValuesItem
        {
            get { return hiddenIDesc.Value; }
            set { hiddenIDesc.Value = value; }
        }

        protected String ValuesLocation
        {
            get { return hiddenLDesc.Value; }
            set { hiddenLDesc.Value = value; }
        }


        [System.Web.Services.WebMethod()]
        public static string validaInfo(string valor, string tipo, string orden = null)
        {

            string strSecondVal = string.Empty;
            string strError = string.Empty;
            string retorno = string.Empty;
            DataTable resultado = new DataTable();

            InterfazDAL_ttwhcol016 idal016 = new InterfazDAL_ttwhcol016();
            Ent_ttwhcol016 obj016 = new Ent_ttwhcol016();
                       
            InterfazDAL_ttscol100 idal = new InterfazDAL_ttscol100();
            Ent_ttscol100 obj = new Ent_ttscol100();

            if (tipo == "1") // Items
            {
                obj.item = valor;
                resultado = idal.spareDelivery_listaRegistroItem_Param(ref obj, ref strError);
                if (resultado.Rows.Count > 0)
                {
                    retorno = resultado.Rows[0]["DESCRIPCION"].ToString().Trim() + "|" +
                                  resultado.Rows[0]["UNIDAD"].ToString().Trim() + "|" +
                                  resultado.Rows[0]["ARTICULO"].ToString().Trim();
                }
            }

            if (tipo == "2") // Lote
            {
                obj.loca = valor;
                resultado = idal.spareDelivery_listaRegistroUbicacion_Param(ref obj, ref strError);
                if (resultado.Rows.Count > 0)
                {
                    retorno = resultado.Rows[0]["LOCATION"].ToString().Trim() + "|" +
                                  resultado.Rows[0]["WAREHOUSE"].ToString().Trim();
                }

            }

            // Validar si el numero de orden trae registros
            if (strError != string.Empty) { return "BAAN: " + strError; }
            // if (resultado.Rows.Count > 0) { retorno = resultado.Rows[0]["DESCRIPCION"].ToString(); }
            
            return retorno;
        }

        protected static string mensajes(string tipoMensaje)
        {
            Mensajes mensajesForm = new Mensajes();
            string idioma = "INGLES";
            var retorno = mensajesForm.readStatement("whInvSpareDelivery.aspx", idioma, ref tipoMensaje);

            if (retorno.Trim() == String.Empty)
            {
                retorno = mensajesForm.readStatement(globalMessages, idioma, ref tipoMensaje);
            }

            return retorno;
        }
            
    }
}
