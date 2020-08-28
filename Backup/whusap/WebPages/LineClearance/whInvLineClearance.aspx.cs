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
using System.Threading;
using System.Globalization;


namespace whusap.WebPages.LineClearance
{
    public partial class whInvLineClearance : System.Web.UI.Page
    {

        string strError = string.Empty;
        //string Aplicacion = "WEBAPP";
        StringBuilder script;

        InterfazDAL_tticol090 idal = new InterfazDAL_tticol090();
        Ent_tticol090 obj = new Ent_tticol090();
        DataTable resultado;

        protected void Page_PreRender(object sender, EventArgs e)
        {
            registerScript();
        }


        protected void Page_Load(object sender, EventArgs e)
        {

            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-CO");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("es-CO");
            base.InitializeCulture();

            txtWorkOrderFrom.Focus();
            Page.Form.DefaultButton = btnSend.UniqueID;
            String strTitulo = "Line Clearance";

            if (!IsPostBack)
            {
                Label control = (Label)Page.Controls[0].FindControl("lblPageTitle");
                if (control != null) { control.Text = strTitulo; }

                this.txtWorkOrderFrom.Attributes.Add("onkeypress", "button_click(this," + this.txtWorkOrderTo.ClientID + ")");
                this.txtWorkOrderTo.Attributes.Add("onkeypress", "button_click(this," + this.btnSend.ClientID + ")");

            }
        }

        protected void grdRecords_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Verificar que si el lote es nulo o vacio
                string strLote = ((DataRowView)e.Row.DataItem).DataView.Table.Rows[e.Row.RowIndex]["IND_LOTE"].ToString();
                string strItem = ((DataRowView)e.Row.DataItem).DataView.Table.Rows[e.Row.RowIndex]["ARTICULO"].ToString();
                if (string.IsNullOrEmpty(strLote))
                {
                    ((DropDownList)e.Row.Cells[5].FindControl("toLot")).Enabled = false;
                    // ((TextBox)e.Row.Cells[5].FindControl("toLot")).Enabled = false;
                }
                else
                {
                    DataRow drv = ((DataRowView)e.Row.DataItem).Row;
                    var fila = drv.ItemArray.ToArray();

                    var serializador = new JavaScriptSerializer();
                    var FilaSerializada = serializador.Serialize(fila);

                    //((RangeValidator)e.Row.Cells[4].FindControl("validateQuantity")).MinimumValue = "0";
                    TextBox toReturn = (TextBox)e.Row.Cells[4].FindControl("toReturn");
//                    TextBox toLot = (TextBox)e.Row.Cells[5].FindControl("toLot");

                    strLote = ((DataRowView)e.Row.DataItem).DataView.Table.Rows[e.Row.RowIndex]["IND_LOTE"].ToString();

                    obj = new Ent_tticol090();
                    obj.tpdn = txtWorkOrderFrom.Text.Trim().ToUpperInvariant();
                    obj.item = strItem;
                    DataTable lotesItem = idal.lineClearance_verificaLote_Param(ref obj, ref strError);

                    if (toReturn != null)
                    {
                        string dataCant = ((DataRowView)e.Row.DataItem).DataView.Table.Rows[e.Row.RowIndex]["ACT_CANT"].ToString();
                        CompareValidator objVal = (CompareValidator)e.Row.Cells[4].FindControl("validateQuantity");

                        objVal.ValueToCompare = dataCant;
                        objVal.ControlToValidate = toReturn.ID;
                        //objVal.Enabled = true;

                        //((RangeValidator)e.Row.Cells[4].FindControl("validateQuantity")).MaximumValue = ((DataRowView)e.Row.DataItem).DataView.Table.Rows[e.Row.RowIndex]["ACT_CANT"].ToString();


                        //((TextBox)e.Row.Cells[4].FindControl("toReturn")).Attributes.Add("onblur", "if(this.value != '') " +
                        //                                                                           "{ validaInfo('" + strItem + "' , '1',this); }");
                        ((TextBox)e.Row.Cells[4].FindControl("toReturn")).Attributes.Add("onblur", "if(this.value != '') " +
                                                                                                   "{ validaInfo('" + strItem + "' , '1',this); " +
                                                                                                     "validateMov('" + dataCant + "',this.value, '" + objVal.ClientID + "', this);}");
                        ((TextBox)e.Row.Cells[4].FindControl("toReturn")).Attributes.Add("onfocus", "limpiar(this);");
                    }

                    if (strLote != "1")
                    {
                        ((DropDownList)e.Row.Cells[5].FindControl("toLot")).Enabled = false;
                        //((TextBox)e.Row.Cells[5].FindControl("toLot")).Enabled = false;
                    }
                   else 
                    {
                        DataRow filaIni = lotesItem.NewRow();
                        filaIni[0] = "  ";
                        filaIni[1] = " - Select Lot - ";
                        lotesItem.Rows.InsertAt(filaIni, 0);

                        DropDownList lista = ((DropDownList)e.Row.Cells[5].FindControl("toLot"));
                        lista.DataSource = lotesItem;
                        lista.DataTextField = "DESCRIPCION";
                        lista.DataValueField = "LOTE";
                        lista.DataBind();
                        
                        //((TextBox)e.Row.Cells[5].FindControl("toLot")).Attributes.Add("onblur", "if(this.value != ''){ validaInfo(this.value +'|" + strItem.Trim() + "', '2', this);}");
                        //((TextBox)e.Row.Cells[5].FindControl("toLot")).Attributes.Add("onfocus", "limpiar(this);");
                    }
                }
            }
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtWorkOrderFrom.Text.Trim()))
            {
                RequiredField.Enabled = true;
                RequiredField.IsValid = false;
                RequiredField.Validate(); 
                txtWorkOrderFrom.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtWorkOrderTo.Text.Trim()))
            {
                RequiredFieldValidator1.Enabled = true;
                RequiredFieldValidator1.IsValid = false;
                RequiredFieldValidator1.Validate();
                txtWorkOrderTo.Focus();
                return;
            }

            lblResult.Text = string.Empty;

            txtWorkOrderFrom.Text = txtWorkOrderFrom.Text.ToUpperInvariant();
            txtWorkOrderTo.Text = txtWorkOrderTo.Text.ToUpperInvariant();


            if (string.IsNullOrEmpty(txtWorkOrderFrom.Text.Trim()))
                return;

            if (string.IsNullOrEmpty(txtWorkOrderTo.Text.Trim()))
                return;

            if (txtWorkOrderFrom.Text.Trim().ToUpperInvariant() == txtWorkOrderTo.Text.Trim().ToUpperInvariant())
            { lblResult.Text = "Work Order's are equals. Cannot continue"; return;}

            string ErrorOrden = "FROM ";

            
            obj.fpdn = txtWorkOrderFrom.Text.ToUpperInvariant();
            resultado = idal.lineClearance_verificaOrdenes_Param(ref obj, ref strError);

            ErrorOrden = "TO ";
            obj.fpdn = txtWorkOrderTo.Text.ToUpperInvariant();
            resultado = idal.lineClearance_verificaOrdenes_Param(ref obj, ref strError);


            obj.fpdn = txtWorkOrderFrom.Text.ToUpperInvariant();
            resultado = idal.lineClearance_listaRegistrosOrden_Param(ref obj, ref strError);

            if (resultado.Rows.Count < 1)
            {
                lblResult.Text = string.Format("Order Work {0}: {1} doesn't exist or the status is not active, release or completed.", ErrorOrden, obj.fpdn.Trim());
                return;
            }

            hidden.Value = txtWorkOrderTo.Text.Trim();
            grdRecords.DataSource = resultado;
            grdRecords.DataBind();

            HeaderGrid.Visible = true;
            btnSave.Visible = true;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            List<Ent_tticol090> parameterCollection = new List<Ent_tticol090>();
            Ent_tticol090 obj = new Ent_tticol090();


            //Recorrer filas con valores en los textos
            Session["WorkOrder"] = txtWorkOrderFrom.Text.Trim();

            foreach (GridViewRow row in grdRecords.Rows)
            {
                string toreturn = ((TextBox)row.Cells[4].FindControl("toReturn")).Text;

                //string toLot = ((TextBox)row.Cells[5].FindControl("toLot")).Text;
                string toLot = ((DropDownList)row.Cells[5].FindControl("toLot")).SelectedValue;


                string condLote = ((HiddenField)row.Cells[6].FindControl("LOTE")).Value.Trim();
                bool reqLote = condLote == "1" ? true : false;
                if ((reqLote && String.IsNullOrEmpty(toLot.Trim())) && !string.IsNullOrEmpty(toreturn))
                {
                    lblResult.Text = "Lot code is required. Cannot continue";
                    return;
                }
                
                if (!toreturn.Equals(string.Empty))
                {
                    obj = new Ent_tticol090();
                    obj.fpdn = txtWorkOrderFrom.Text.ToUpperInvariant();
                    obj.tpdn = txtWorkOrderTo.Text.ToUpperInvariant();
                    obj.clot = string.IsNullOrEmpty(toLot) ? " " : toLot.ToUpperInvariant();
                    obj.srno = 0;
                    obj.item = row.Cells[0].Text.ToUpperInvariant(); 
                    obj.qana = Decimal.Parse(toreturn, System.Globalization.CultureInfo.InvariantCulture); 
                    obj.logn = Session["user"].ToString();
                    obj.proc = 2; 
                    obj.refcntd = 0 ;
                    obj.refcntu = 0;
                    obj.idrecord = grdRecords.DataKeys[row.RowIndex].Value.ToString();
                    parameterCollection.Add(obj);
                }

            }
            InterfazDAL_tticol090 idal = new InterfazDAL_tticol090();
            idal.insertarRegistro(ref parameterCollection, ref strError);
            printResult.Visible = true;

            lblResult.Text = "Line Clearance was succesfully registered.";
            this.HeaderGrid.Visible = false;

            grdRecords.DataSource = "";
            grdRecords.DataBind();

            if (strError != string.Empty)
            {
                lblResult.Text = strError;
                throw new System.InvalidOperationException(strError);
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
        
        }

        protected String OrderTo
        {
            get { return hidden.Value; }
            set { hidden.Value = value; }
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
                       
            InterfazDAL_tticol090 idal = new InterfazDAL_tticol090();
            Ent_tticol090 obj = new Ent_tticol090();

            if (tipo == "1") // Items
            {
                obj.item = valor;
                obj.fpdn = orden;

                resultado = idal.lineClearance_verificaRegistrosOrden_Param(ref obj, ref strError);
            }

            if (tipo == "2") // Lote
            {
                String[] valores = valor.Split('|');

                obj016.clot = valores[0].Trim().ToUpperInvariant();
                obj016.item = valores[1].Trim().ToUpperInvariant();
                obj.fpdn = orden;

                resultado = idal016.TakeMaterialInv_verificaLote_Param(ref obj016, ref strError);

                if (string.IsNullOrEmpty(strError))
                    resultado = idal.lineClearance_verificaRegistrosOrden_Param(ref obj, ref obj016, ref strError);
            }

            // Validar si el numero de orden trae registros
            if (strError != string.Empty) { return "BAAN: " + strError; }
            if (resultado.Rows.Count > 0) { retorno = resultado.Rows[0]["DESCRIPCION"].ToString(); }

            return retorno;
        }

            
    }
}


