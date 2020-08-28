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

namespace whusap.WebPages.InvMaterial
{
    public partial class whInvMaterialRejectedD : System.Web.UI.Page
    {
       
        string strError = string.Empty;
        string strOrden = string.Empty;
        // string Aplicacion = "WEBAPP";

        //Type csType;
        //ClientScriptManager scriptBlock;


        protected void Page_Load(object sender, EventArgs e)
        {
            txtItem.Focus();
            Page.Form.DefaultButton = btnSend.UniqueID;

            var ctrlName = Request.Params[Page.postEventSourceID];
            var args = Request.Params[Page.postEventArgumentID];
            
            //HandleCustomPostbackEvent(ctrlName, args);
            String strTitulo = "Material Diposition Inside MRB Warehouse";
            if (!IsPostBack)
            {
                Label control = (Label)Page.Controls[0].FindControl("lblPageTitle");
                if (control != null) { control.Text = strTitulo; }
            }

            Type csType = this.GetType();
            ClientScriptManager scriptBlock = Page.ClientScript;

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
            this.txtItem.Attributes.Add("onkeypress", "button_click(this," + this.btnSend.ClientID + ")");
            this.txtLot.Attributes.Add("onkeypress", "button_click(this," + this.btnSend.ClientID + ")");

        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtItem.Text.Trim()))
            {
                RequiredField.Enabled = true;
                RequiredField.IsValid = false;
                txtItem.Focus();
                btnSave.Visible = false;
                grdRecords.DataSource = "";
                grdRecords.DataBind();
                return;
            }

            if (string.IsNullOrEmpty(txtLot.Text.Trim()))
            {
                RequiredFieldLot.Enabled = true;
                RequiredFieldLot.IsValid = false;
                txtLot.Focus();
                btnSave.Visible = false;
                grdRecords.DataSource = "";
                grdRecords.DataBind();
                return;
            }

            // Validar si el usuario tiene almacen MRB asociado
            //Validar si el Item y Lote digitado son correctos
            InterfazDAL_tticol127 idal = new InterfazDAL_tticol127();
            Ent_tticol127 obj = new Ent_tticol127();
            string strError = string.Empty;

            obj.user = Session["user"].ToString().ToUpper();
            obj.item = txtItem.Text.ToUpper();
            obj.lote = txtLot.Text.ToUpper();
            lblResult.Text = string.Empty;
            DataTable resultado = idal.listaRegistrosOrden_Param(ref obj, ref strError, ref strOrden);

            if (strError != string.Empty)
            {
                txtItem.Text = "";
                txtLot.Text = "";
                txtItem.Focus();
                btnSave.Visible = false;
                grdRecords.DataSource = "";
                grdRecords.DataBind();
                lblResult.Text = strError;
                return;
            }
            else
            {
                grdRecords.DataSource = resultado;
                grdRecords.DataBind();
                Session["Orden"] = strOrden;
                Session["Lote"] = txtLot.Text.ToUpperInvariant();
                this.HeaderGrid.Visible = true;
                btnSave.Visible = true;
                lblResult.Text = "";
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
             
            List<Ent_tticol118> parameterCollection = new List<Ent_tticol118>();
            Ent_tticol118 obj = new Ent_tticol118();
            //Recorrer filas con valores en los textos
            string disposition = String.Empty;
            string reason = String.Empty;
            string stockw = String.Empty;
            string orden = String.Empty;

            foreach (GridViewRow row in grdRecords.Rows)
            {
                string toreturn = ((TextBox)row.Cells[7].FindControl("toReturn")).Text;
                string lote = ((TextBox)row.Cells[7].FindControl("toReturn")).Text;
                reason = ((DropDownList)row.Cells[10].FindControl("Reasonid")).SelectedValue;
                disposition = ((DropDownList)row.Cells[11].FindControl("Dispoid")).SelectedValue;
                stockw = ((DropDownList)row.Cells[12].FindControl("Stockwareh")).SelectedValue;
                if (stockw == String.Empty)
                {
                    stockw = "NA";
                }   
                string regrind = ((DropDownList)row.Cells[13].FindControl("Regrind")).SelectedValue;
                if (regrind == String.Empty)
                {
                    regrind = "NA";
                }   
                string obse = ((TextBox)row.Cells[14].FindControl("Comments")).Text;
                if (obse.Length > 255)
                {
                    obse = ((TextBox)row.Cells[14].FindControl("Comments")).Text.Substring(1, 255);
                }
                string supplier = ((DropDownList)row.Cells[11].FindControl("Supplier")).SelectedValue;
                if (supplier == string.Empty)
                {
                    supplier = " ";
                }
                if (Session["Lote"].ToString() == " ")
                {
                    Session["Lote"] = " ";
                }
                if (!toreturn.Equals(string.Empty))
                {
                    obj = new Ent_tticol118();
                    obj.item = "         " + txtItem.Text.Trim().ToUpperInvariant();
                    obj.cwar = row.Cells[2].Text.ToUpperInvariant(); 
                    //obj.clot = row.Cells[3].Text.ToUpperInvariant();
                    obj.clot = Session["Lote"].ToString();
                    obj.qtyr = Convert.ToInt32(toreturn); 
                    obj.cdis = reason;
                    obj.obse = obse;
                    obj.logr = Session["user"].ToString();
                    obj.disp = Convert.ToInt32(disposition);
                    obj.stoc = stockw;
                    obj.ritm = regrind;
                    obj.proc = 2;
                    obj.mess = " ";
                    obj.suno = supplier;
                    obj.refcntd = 0;
                    obj.refcntu = 0;
                    parameterCollection.Add(obj);
                }

            }



            InterfazDAL_tticol118 idal = new InterfazDAL_tticol118();
            int actualizar = idal.actualizarRegistro_Param(ref parameterCollection, ref strError);
            if (actualizar <1)
                idal.insertarRegistro(ref parameterCollection, ref strError);

            if (strError != string.Empty)
            {
                lblResult.Text = strError;
            }   
            else
            {
                printResult.Visible = true;
                if (Convert.ToInt32(disposition) == 4)
                {

                    DataTable resultado = idal.invLabel_registroImprimir_Param(ref obj, ref strError);

                    DataRow reg = resultado.Rows[0];
                    Session["FilaImprimir"] = reg;
                    printLabel.Visible = true;
                }
                lblResult.Text = strError;
                lblResult.Text = "Material Rejection Inside Warehouse was updated succesfully.";
                this.HeaderGrid.Visible = false;   
                
            }   

            grdRecords.DataSource = "";
            grdRecords.DataBind();

            if (strError != string.Empty)
            {
                lblResult.Text = strError;
                throw new System.InvalidOperationException(strError);
            }
        }

        protected void printLabel_Click(object sender, EventArgs e)
        {

            InterfazDAL_tticol118 idal = new InterfazDAL_tticol118();
            Ent_tticol118 obj = new Ent_tticol118();
            string strError = string.Empty;
            obj.item = txtItem.Text;
            obj.clot = txtLot.Text;
            lblResult.Text = string.Empty;
            DataTable resultado = idal.listaRegistros_Param(ref obj, ref strError);

            Session["resultado"] = resultado;
            StringBuilder paramurl = new StringBuilder();
            paramurl.Append("?");
            paramurl.Append("valor1=" + Request.QueryString[0].ToString() + "&");
            paramurl.Append("valor2=" + Request.QueryString[1].ToString() + "&");
            paramurl.Append("valor3=" + Request.QueryString[2].ToString());
            Session["IsPreviousPage"] = "";

            StringBuilder script = new StringBuilder();
            script.Append("ventanaImp = window.open('../Labels/whInvLabelMaterialRejectedD.aspx', ");
            script.Append("'ventanaImp', 'menubar=0,resizable=0,width=580,height=450');");
            script.Append("ventanaImp.moveTo(30, 0);");
            //script.Append("setTimeout (ventanaImp.close(), 20000);");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "printTag", script.ToString(), true);           
        }        


        protected void grdRecords_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Verificar que si la cantidad es igual a 0, el control "toReturn" se deshabilite
                string stock = ((HiddenField)e.Row.Cells[4].FindControl("actualqty")).Value.Trim();
                string stritem = ((DataRowView)e.Row.DataItem).DataView.Table.Rows[e.Row.RowIndex]["Item"].ToString();

                if (Convert.ToInt32(stock) == 0)
                {
                    ((TextBox)e.Row.Cells[6].FindControl("toReturn")).Enabled = false;
                    ((TextBox)e.Row.Cells[6].FindControl("toReturn")).Attributes.Add("onfocus", "limpiar(this);");
                
                }
                // Verificar que si el lote es nulo o vacio
                string strLote = ((DataRowView)e.Row.DataItem).DataView.Table.Rows[e.Row.RowIndex]["LOT"].ToString();

                DataRow drv = ((DataRowView)e.Row.DataItem).Row;    
                var fila = drv.ItemArray.ToArray();
                    
                var serializador = new JavaScriptSerializer();
                var FilaSerializada = serializador.Serialize(fila);

                ((RangeValidator)e.Row.Cells[6].FindControl("validateQuantity")).MinimumValue = "0";
                ((RangeValidator)e.Row.Cells[6].FindControl("validateQuantity")).MaximumValue = ((DataRowView)e.Row.DataItem).DataView.Table.Rows[e.Row.RowIndex]["stock"].ToString();

                //Llenar Stock Warehouse Dropdownlist
                InterfazDAL_tticol118 idalp = new InterfazDAL_tticol118();
                Ent_tticol118 obj = new Ent_tticol118();
                obj.item = stritem;
                DataTable proveedor = idalp.listaProveedores_Param(ref obj, ref strError);
                DropDownList listproveedor = (DropDownList)e.Row.Cells[9].FindControl("Supplier");
                listproveedor.DataSource = proveedor;
                listproveedor.DataTextField = "nombre";
                listproveedor.DataValueField = "proveedor";
                listproveedor.DataBind();
                listproveedor.Items.Insert(0, new ListItem("Select Supplier here...", string.Empty));


                //Llenar Reason Dropdownlist
                InterfazDAL_tticol118 idal = new InterfazDAL_tticol118();
                DataTable reason = idal.listaReason_Param(ref strError);
                DropDownList listreason = (DropDownList) e.Row.Cells[10].FindControl("Reasonid");
                listreason.DataSource = reason;
                listreason.DataTextField = "descr";
                listreason.DataValueField = "reason";
                listreason.DataBind();
                listreason.Items.Insert(0, new ListItem("Select Reason here...", string.Empty));               

                //Llenar Disposition Dropdownlist desde el webconfig
                ////if (ConfigurationManager.AppSettings.AllKeys.Contains("Disposition"))
                ////{
                ////    string listdisposition = ConfigurationManager.AppSettings["Disposition"];

                ////    var dispositionarray = listdisposition.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries).ToList<string>();
                ////    dispositionarray.Insert(0, "-- Select Disposition --");

                ////    DropDownList dispoid = (DropDownList)e.Row.Cells[11].FindControl("Dispositionid");
                ////    dispoid.DataSource = dispositionarray;
                ////    dispoid.DataBind();
                ////}
                ////else { lblResult.Visible = true; lblResult.Text = "Disposition List not found"; }

                //Llenar Stock Warehouse Dropdownlist
                InterfazDAL_tticol118 idalsw = new InterfazDAL_tticol118();
                DataTable stockw = idalsw.listaStockw_Param(ref strError);
                DropDownList liststockw = (DropDownList) e.Row.Cells[11].FindControl("Stockwareh");
                liststockw.DataSource = stockw;
                liststockw.DataTextField = "descrw";
                liststockw.DataValueField = "warehouse";
                liststockw.DataBind();
                liststockw.Items.Insert(0, new ListItem("Select Stock Warehouse here...", string.Empty));

                //Llenar Stock Warehouse Dropdownlist
                InterfazDAL_tticol118 idalr = new InterfazDAL_tticol118();
                DataTable regrind = idalr.listaRegrind_Param(ref strError);
                DropDownList listregrind = (DropDownList)e.Row.Cells[12].FindControl("Regrind");
                listregrind.DataSource = regrind;
                listregrind.DataTextField = "descrc";
                listregrind.DataValueField = "item";
                listregrind.DataBind();
                listregrind.Items.Insert(0, new ListItem("Select Item here...", string.Empty));
            }  
        }


        [System.Web.Services.WebMethod()]
        public static string validaExistLot(object Fila, string valor)
        {
            InterfazDAL_tticol125 idal = new InterfazDAL_tticol125();
            Ent_tticol125 obj = new Ent_tticol125();
            string strError = string.Empty;
            Array fila = Fila.ToString().Replace(" ", "").Replace("\"", "").Split(',');
            string retorno = string.Empty;

            obj.item = fila.GetValue(4).ToString().Trim().ToUpperInvariant();
            obj.pdno = fila.GetValue(1).ToString().Trim().ToUpperInvariant();
            obj.clot = valor.Trim().ToUpperInvariant();


            //lblResult.Text = string.Empty;
            DataTable resultado = idal.listaRegistrosLoteItem_Param(ref obj, ref strError);

            // Validar si el numero de orden trae registros
            if (strError != string.Empty)
            {
                return strError;
            }
            return retorno;
        }

        protected void Dispoid_SelectedIndexChanged(object sender, EventArgs e)
        {
            // dispoc = (GridViewRow)sender;
            DropDownList listdispo = (DropDownList)sender;
            GridViewRow contenedor = (GridViewRow)listdispo.Parent.DataItemContainer;
            DropDownList liststock = (DropDownList)contenedor.Cells[10].FindControl("Stockwareh");
            DropDownList listregrind = (DropDownList)contenedor.Cells[11].FindControl("Regrind");
            DropDownList listproveedor = (DropDownList)contenedor.Cells[8].FindControl("Supplier");

            if (listdispo.SelectedValue == "2")
            {
                listregrind.Enabled = false;
                liststock.Enabled = false;
                listproveedor.Enabled = true;
            }
            if (listdispo.SelectedValue == "3")
            {
                listregrind.Enabled = false;
                listproveedor.Enabled = false;
                liststock.Enabled = true;
            }
            if (listdispo.SelectedValue == "4")
            {
                liststock.Enabled = false;
                listproveedor.Enabled = false;
                listregrind.Enabled = true;
            }
        }
    }
}
