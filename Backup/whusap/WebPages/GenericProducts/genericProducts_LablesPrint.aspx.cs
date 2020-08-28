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

namespace whusap.WebPages.GenericProducts
{
    public partial class genericProducts_LablesPrint : System.Web.UI.Page
    {
        string strError = string.Empty;
        string Aplicacion = "WEBAPP";
        DataTable resultado = new DataTable();

        Type csType;
        ClientScriptManager scriptBlock;

        protected void Page_Load(object sender, EventArgs e)
        {
                txtWorkOrder.Focus();
                Page.Form.DefaultButton = btnSend.UniqueID;

                var ctrlName = Request.Params[Page.postEventSourceID];
                var args = Request.Params[Page.postEventArgumentID];

                lblOrder.Visible = false;
                String strTitulo = "Imprimir Etiquetas Genericos";

                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-CO");
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("es-CO");
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
            InterfazDAL_ttisfc001 idal = new InterfazDAL_ttisfc001();
            //Ent_tticol125 obj = new Ent_tticol125();
            string strError = string.Empty;

            txtWorkOrder.Text = txtWorkOrder.Text.ToUpperInvariant();
            string pdno = txtWorkOrder.Text.ToUpperInvariant();
            lblResult.Text = string.Empty;
            DataTable resultado = idal.GenericProducts_listaRegistroOrden_Param(ref pdno, ref strError);

            
            // Validar si el numero de orden trae registros
            if (strError != string.Empty)
            {
                OrderError.IsValid = false;
                txtWorkOrder.Focus();
                grdRecords.DataSource = "";
                grdRecords.DataBind();
                return;
            }
            if (resultado.Rows.Count < 1)
            {
                lblError.Text = "El número de orden no existe";
                return;
            }
            Session["resultado"] = resultado;
            lblOrder.Text = "Order: " + pdno;
            grdRecords.DataSource = resultado;
            grdRecords.DataBind();

            lblResult.Text = "";
        }

        protected void grdRecords_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Verificar que si la cantidad es igual a 0, el control "toReturn" se deshabilite
                //if (Convert.ToInt32(e.Row.Cells[3].Text) == 0)
                //{
                //    ((TextBox)e.Row.Cells[6].FindControl("toReturn")).Enabled = false;
                //    ((TextBox)e.Row.Cells[6].FindControl("toReturn")).Attributes.Add("onfocus", "limpiar(this);");

                //}
                DropDownList lista = ((DropDownList)e.Row.Cells[6].FindControl("ddltypeLabel"));


                DataRow drv = ((DataRowView)e.Row.DataItem).Row;
                var fila = drv.ItemArray.ToArray();

                var serializador = new JavaScriptSerializer();
                var FilaSerializada = serializador.Serialize(fila);

     

                    if (lista != null)
                    {
                        //((TextBox)e.Row.Cells[8].FindControl("toLot")).Attributes.Add("onblur", "validaLot(" + FilaSerializada.Trim() + ", this.value, this);");
                        ((TextBox)e.Row.Cells[6].FindControl("toPrint")).Attributes.Add("onfocus", "limpiar(this);");
                    }

                    //DataRow filaIni = lotesItem.NewRow();
                    //filaIni[0] = "  ";
                    //filaIni[1] = " - Select Lot - ";
                    //lotesItem.Rows.InsertAt(filaIni, 0);

                    //lista.Enabled = true;
                    //lista.DataSource = lotesItem;
                    //lista.DataTextField = "DESCRIPCION";
                    //lista.DataValueField = "LOTE";
                    //lista.DataBind();

                TextBox control = (TextBox)e.Row.Cells[5].FindControl("toPrint");
                control.Attributes.Add("onblur", "validaLot(this, " + FilaSerializada + ", '" + txtWorkOrder.Text.Trim() + "');");
            }
        }

        protected void grdRecords_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "btnPrint_Click")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = grdRecords.Rows[index];
                DataRow reg;
                if (row.DataItem == null)
                {
                    if (Session["resultado"] != null)
                    {
                        DropDownList lista = ((DropDownList)row.Cells[6].FindControl("ddltypeLabel"));
                        string labelToImp = lista.SelectedValue;

                        TextBox txtCnt = ((TextBox)row.Cells[5].FindControl("toPrint"));
                        int cntImp = Convert.ToInt32(txtCnt.Text);

                        TextBox txtNSerie = ((TextBox)row.Cells[5].FindControl("nSerie"));
                        int nSerie = Convert.ToInt32(txtNSerie.Text);

                        resultado = (DataTable)Session["resultado"];
                        grdRecords.DataSource = resultado;
                        grdRecords.DataBind();
                        reg = resultado.Rows[index];
                        Session["FilaImprimir"] = reg;
                        string nSerieImp = string.Empty;
                        for (int i = 1; i <= cntImp; i++)
                        {
                           nSerieImp = nSerie.ToString().PadLeft(4, '0');
                           Session["Consec"] = nSerieImp; 
                           StringBuilder script = new StringBuilder();
                           script.Append("ventanaImp = window.open('../Labels/GenericProducts/" + labelToImp.Trim() + ".aspx', ");
                           script.Append("'ventanaImp', 'menubar=0,resizable=0,width=580,height=450');");
                           script.Append("ventanaImp.moveTo(30, 0);");
                           //script.Append("setTimeout (ventanaImp.close(), 20000);");
                           ScriptManager.RegisterStartupScript(this, this.GetType(), "printTag", script.ToString(), true);
                           nSerie++;
                        }

                    }
                }
            }
        }
    }
}