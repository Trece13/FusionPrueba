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

namespace whusap.WebPages.InvMaterial
{
    public partial class whInvMaterialDevolution : System.Web.UI.Page
    {
       
        string strError = string.Empty;
        string Aplicacion = "WEBAPP";

        Type csType;
        ClientScriptManager scriptBlock;

        protected void Page_Load(object sender, EventArgs e)
        {
                txtWorkOrder.Focus();
                Page.Form.DefaultButton = btnSend.UniqueID;

                var ctrlName = Request.Params[Page.postEventSourceID];
                var args = Request.Params[Page.postEventArgumentID];

                //HandleCustomPostbackEvent(ctrlName, args);
                lblOrder.Visible = false;
                String strTitulo = "Material Returns";

                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
                base.InitializeCulture();

                if (!IsPostBack)
                {
                    Label control = (Label)Page.Controls[0].FindControl("lblPageTitle");
                    if (control != null) { control.Text = strTitulo; }
                }

                csType = this.GetType();
                scriptBlock = Page.ClientScript;

                StringBuilder script = new StringBuilder();
                // Crear el script para la ejecucion de la forma
                script.Append("<script type=\"text/javascript\">function button_click(objTextBox,objBtnID) {");
                script.Append("if(window.event.keyCode==13)");
                script.Append("{");
                script.Append("document.getElementById(objBtnID).focus();");
                script.Append("document.getElementById(objBtnID).click();");
                script.Append("}}");
                script.Append("</script>");

                scriptBlock.RegisterClientScriptBlock(csType, "button_click", script.ToString(), false);
                this.txtWorkOrder.Attributes.Add("onkeypress", "button_click(this," + this.btnSend.ClientID + ")");
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            btnSave.Visible = false;
            if (string.IsNullOrEmpty(txtWorkOrder.Text.Trim()))
            {
                RequiredField.Enabled = true;
                RequiredField.IsValid = false;
                txtWorkOrder.Focus();
                grdRecords.DataSource = "";
                grdRecords.DataBind();
                return;
            }

            lblOrder.Visible = true;
            InterfazDAL_tticol125 idal = new InterfazDAL_tticol125();
            Ent_tticol125 obj = new Ent_tticol125();
            string strError = string.Empty;

            txtWorkOrder.Text = txtWorkOrder.Text.ToUpperInvariant();
            obj.pdno = txtWorkOrder.Text.ToUpperInvariant();
            lblResult.Text = string.Empty;
            DataTable resultado = idal.listaRegistrosOrden_Param(ref obj, ref strError);


            // Validar si el numero de orden trae registros
            if (strError != string.Empty)
            {
                OrderError.IsValid = false;
                txtWorkOrder.Focus();
                btnSave.Visible = false;
                grdRecords.DataSource = "";
                grdRecords.DataBind();
                return;
            }
            lblOrder.Text = "Order: " + obj.pdno;
            grdRecords.DataSource = resultado;
            grdRecords.DataBind();

            this.HeaderGrid.Visible = true;
            btnSave.Visible = true;
            lblResult.Text = "";
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            
            List<Ent_tticol125> parameterCollection = new List<Ent_tticol125>();
            Ent_tticol125 obj = new Ent_tticol125();
            //Recorrer filas con valores en los textos
            Session["WorkOrder"] = txtWorkOrder.Text.Trim();

            foreach (GridViewRow row in grdRecords.Rows)
            {
                string toreturn = ((TextBox)row.Cells[6].FindControl("toReturn")).Text;
                string toLot = ((DropDownList)row.Cells[8].FindControl("toLot")).SelectedValue; 

                string condLote = ((HiddenField)row.Cells[6].FindControl("LOTE")).Value.Trim();
                bool reqLote = condLote == "1" ? true : false;
                if ((reqLote && String.IsNullOrEmpty(toLot.Trim())) && !string.IsNullOrEmpty(toreturn))
                {
                    lblResult.Text = "Lot code is required. Cannot continue";
                    return;
                }

                //string toreturn = ((TextBox)row.Cells[6].FindControl("toReturn")).Text;
                //string toLot = ((TextBox)row.Cells[8].FindControl("toLot")).Text;

                if (!toreturn.Equals(string.Empty))
                {
                    obj = new Ent_tticol125();
                    obj.pdno = txtWorkOrder.Text.Trim().ToUpperInvariant();
                    obj.pono = Convert.ToInt32(row.Cells[0].Text);
                    obj.item = row.Cells[1].Text.ToUpperInvariant(); //.Trim();
                    obj.cwar = row.Cells[3].Text.ToUpperInvariant(); //.Trim();
                    obj.clot = string.IsNullOrEmpty(toLot) ? " " : toLot.ToUpperInvariant();
                    obj.reqt = Decimal.Parse(toreturn, System.Globalization.CultureInfo.InvariantCulture);  //Convert.ToInt32(toreturn);
                    obj.refcntd = "0";
                    obj.refcntu = "0";
                    obj.mess = " ";
                    obj.prin = 2;
                    obj.conf = 2;
                    obj.user = Session["user"].ToString();
                    obj.idrecord = grdRecords.DataKeys[row.RowIndex].Value.ToString();
                    parameterCollection.Add(obj);
                }

            }
            InterfazDAL_tticol125 idal = new InterfazDAL_tticol125();
            idal.insertarRegistro(ref parameterCollection, ref strError, Aplicacion);
            printResult.Visible = true;
            printLabel.Visible = true;
            lblResult.Text = "Material Return was saved succesfully.";
            this.HeaderGrid.Visible = false;

            grdRecords.DataSource = "";
            grdRecords.DataBind();

            btnSave.Visible = false; 
            if (strError != string.Empty)
            {
                btnSave.Visible = false; 
                lblResult.Text = strError;
                throw new System.InvalidOperationException(strError);
            }
        }

        protected void printLabel_Click(object sender, EventArgs e)
        {

            InterfazDAL_tticol125 idal = new InterfazDAL_tticol125();
            Ent_tticol125 obj = new Ent_tticol125();
            string strError = string.Empty;
            obj.pdno = txtWorkOrder.Text;
            lblResult.Text = string.Empty;
            DataTable resultado = idal.listaRegistrosOrden_Param(ref obj, ref strError, true);

            // Validar si el numero de orden trae registros
            if (strError != string.Empty)
            {
                string MsgError = OrderError.ErrorMessage;
                OrderError.ErrorMessage = "Work Order don't have tag's to print";
                OrderError.IsValid = false;
                OrderError.ErrorMessage = MsgError;
                return;
            }
            lblOrder.Text = "Order: " + obj.pdno;

            Session["resultado"] = resultado;
            StringBuilder paramurl = new StringBuilder();
            paramurl.Append("?");
            paramurl.Append("valor1=" + Request.QueryString[0].ToString() + "&");
            paramurl.Append("valor2=" + Request.QueryString[1].ToString() + "&");
            paramurl.Append("valor3=" + Request.QueryString[2].ToString());
            Session["IsPreviousPage"] = "";
            //Server.Transfer("whInvMaterialDevolReprintLabel.aspx", true);
            Response.Redirect("whInvMaterialDevolReprintLabel.aspx" + paramurl.ToString());
            
            
//            //grdRecords.DataSource = resultado;
//            //grdRecords.DataBind();
//            printResult.Visible = false;


//            string printerHtml = printerDiv.InnerHtml;
//            printerDiv.Style.Add(HtmlTextWriterStyle.BackgroundColor, "White");
//            printerDiv.Style.Add(HtmlTextWriterStyle.Height, "400px");
//            printerDiv.Style.Add(HtmlTextWriterStyle.Width, "500px");
////            printerDiv.Style.Add(HtmlTextWriterStyle., BorderStyle.Solid.ToString());

//            foreach (DataRow reg in resultado.Rows)
//            {

//                if (scriptBlock.IsClientScriptBlockRegistered("printTag"))
//                {
//                    scriptBlock.RegisterClientScriptBlock(csType, "printTag", string.Empty, false);
//                }

//                DataRow drv = reg;
//                Session["FilaImprimir"] = reg;
//                printerDiv.InnerHtml = printerHtml; //

//                // Validar que la division exista en el documento
//                Control div = FindControl("printer");
//                if (div == null)
//                {
//                   // this.Controls.Add(createDiv);
//                }

//                printerDiv.InnerHtml = "<iframe src='../Labels/whInvPrintLabel.aspx' width='100%'; height='100%'; onload='this.contentWindow.print()></iframe>"; //
//                printerDiv.Focus();

//                //csType = this.GetType();
//                //scriptBlock = Page.ClientScript;

//                //scriptBlock.RegisterClientScriptBlock(csType, "printTag", this.crearScriptImp(pagina), false);

//                //String cstext1 = "printTag();";
//                //scriptBlock.RegisterStartupScript(csType, "ButtonClickScript", cstext1, true);
//          }
        }        


        protected void grdRecords_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Verificar que si la cantidad es igual a 0, el control "toReturn" se deshabilite
                if (Convert.ToDouble(e.Row.Cells[4].Text) == 0)
                {
                    ((TextBox)e.Row.Cells[6].FindControl("toReturn")).Enabled = false;
                    ((TextBox)e.Row.Cells[6].FindControl("toReturn")).Attributes.Add("onfocus", "limpiar(this);");

                }
                DropDownList lista = ((DropDownList)e.Row.Cells[5].FindControl("toLot"));
                // Verificar que si el lote es nulo o vacio
                string strLote = ((DataRowView)e.Row.DataItem).DataView.Table.Rows[e.Row.RowIndex]["LOTE"].ToString();
                string strItem = ((DataRowView)e.Row.DataItem).DataView.Table.Rows[e.Row.RowIndex]["ARTICULO"].ToString();
                InterfazDAL_tticol090 idal090 = new InterfazDAL_tticol090();
                Ent_tticol090 obj090 = new Ent_tticol090();
                obj090.tpdn = txtWorkOrder.Text.Trim().ToUpperInvariant();
                obj090.item = strItem;
                DataTable lotesItem = idal090.lineClearance_verificaLote_Param(ref obj090, ref strError);

                DataRow drv = ((DataRowView)e.Row.DataItem).Row;
                var fila = drv.ItemArray.ToArray();

                var serializador = new JavaScriptSerializer();
                var FilaSerializada = serializador.Serialize(fila);
                
                if (strLote != "1")
                {
                    //((TextBox)e.Row.Cells[8].FindControl("toLot")).Enabled = false;
                    lista.Enabled = false;
                }
                else
                {

                    if (lista != null)
                    {
                        //((TextBox)e.Row.Cells[8].FindControl("toLot")).Attributes.Add("onblur", "validaLot(" + FilaSerializada.Trim() + ", this.value, this);");
                        ((TextBox)e.Row.Cells[6].FindControl("toReturn")).Attributes.Add("onfocus", "limpiar(this);");                
                    }

                    DataRow filaIni = lotesItem.NewRow();
                    filaIni[0] = "  ";
                    filaIni[1] = " - Select Lot - ";
                    lotesItem.Rows.InsertAt(filaIni, 0);

                    lista.Enabled = true;
                    lista.DataSource = lotesItem;
                    lista.DataTextField = "DESCRIPCION";
                    lista.DataValueField = "LOTE";
                    lista.DataBind();

                }
                ((RangeValidator)e.Row.Cells[6].FindControl("validateQuantity")).MinimumValue = "0";
                ((RangeValidator)e.Row.Cells[6].FindControl("validateQuantity")).MaximumValue = ((DataRowView)e.Row.DataItem).DataView.Table.Rows[e.Row.RowIndex]["CANT"].ToString(); ;
                TextBox control = (TextBox)e.Row.Cells[6].FindControl("toReturn");
                control.Attributes.Add("onblur", "validaLot(this, " + FilaSerializada + ", '" + txtWorkOrder.Text.Trim() + "');");
                control.Attributes.Add("onchange", "validarCantidadMaxima(this, " + FilaSerializada + ", 0);");
                lista.Attributes.Add("onchange", "validarCantidadMaxima(this, " + FilaSerializada + ", 1);");
            }  
        }

        protected string crearScriptImp(string pagina)
        {
            StringBuilder script = new StringBuilder();
            // Crear el script para la ejecucion de la forma
            script.Append("<script type=\"text/javascript\">function printTag() {");
            script.Append("var div = document.getElementById('printer');");
            script.Append("div.innerHTML = ");
            script.Append('"');
            script.Append("<iframe src='../Labels/whInvPrintLabel.aspx' ");
            script.Append("onload='this.contentWindow.print();'></iframe>");
            script.Append('"');
            script.Append(";}</");
            script.Append("script>");

            return script.ToString();        
        }

        [System.Web.Services.WebMethod()]
        public static string validaExistLot(object Fila, string valor)
        {
            InterfazDAL_tticol125 idal = new InterfazDAL_tticol125();
            Ent_tticol125 obj = new Ent_tticol125();
            string strError = string.Empty;
            Array fila = Fila.ToString().Replace(" ", "").Replace("\"", "").Split(',');
            string retorno = string.Empty;

            obj.pdno = valor.Trim().ToUpperInvariant();
            obj.pono = Convert.ToInt32(fila.GetValue(2).ToString().Trim());
           // obj.clot = valor.Trim().ToUpperInvariant();

            //lblResult.Text = string.Empty;
            int resultado = idal.listaRegistrosPendConfItem_Param(ref obj, ref strError);

            // Validar si el numero de orden trae registros
            if (strError != string.Empty)
            {
                return strError;
            }

            return retorno;
        }



        [System.Web.Services.WebMethod()]
        public static string validarCantidades(object fila, string valor, string lote)
        {
            InterfazDAL_tticol125 idal = new InterfazDAL_tticol125();
            Ent_tticol125 obj = new Ent_tticol125();
            string strError = string.Empty;
            string retorno = string.Empty;

            try
            {
                valor = valor.Trim();
                lote = lote.Trim().ToUpperInvariant();
                if (!string.IsNullOrEmpty(valor) && !string.IsNullOrEmpty(lote))
                {
                    Array row = fila.ToString().Replace(" ", "").Replace("\"", "").Split(',');
                    double iCantMax = 0;

                    obj.pdno = row.GetValue(1).ToString().Trim();
                    obj.pono = Convert.ToInt32(row.GetValue(2).ToString().Trim());
                    obj.item = row.GetValue(4).ToString().Trim();
                    obj.clot = lote.ToString().Trim();

                    iCantMax =idal.cantidadMaximaPorLote(ref obj, ref strError);

                    if (double.Parse(valor) <= iCantMax)
                    {
                        return retorno;
                    }
                    else if (string.IsNullOrEmpty(strError))
                    {
                        strError = "The amount to return is invalid";
                    }
                }
            }
            catch (Exception ex)
            {
                strError = ex.InnerException != null ? 
                                ex.Message + " (" + ex.InnerException + ")" : 
                                ex.Message;
            }
            return strError;
        }
    }
}