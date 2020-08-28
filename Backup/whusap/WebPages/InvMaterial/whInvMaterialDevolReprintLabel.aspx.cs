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
using System.IO;

namespace whusap.WebPages.InvMaterial
{
    public partial class whInvMaterialDevolReprintLabel : System.Web.UI.Page
    {
        string strError = string.Empty;
        //string Aplicacion = "WEBAPP";
        DataTable resultado = new DataTable();
        //Type csType;
        //ClientScriptManager scriptBlock;
//        DataTable TABLA;
        protected void Page_Load(object sender, EventArgs e)
        {
            txtWorkOrder.Focus();
            this.SetFocus(Page.Form.UniqueID);
//            TABLA = (new Tabla()).crearTabla();

//            Page.Form.Unload += new EventHandler(Form_Unload);
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "printTag", "", true);
            string strTitulo = "Printer Label's Material Devolution";
            if (IsPostBack)
            {
                //if (Session["FilaImprimir"] != null)
                //{
                //    Literal executeScript = new Literal();
                //    executeScript.Mode = LiteralMode.PassThrough;
                //    executeScript.Text = @"<script>javascript:printTag()</;" + @"script>";
                //    this.Controls.Add(executeScript);
                //}
            }

            if (!IsPostBack)
            {
                // Si la llamada no proviene de otro formulario
                if ( Session["IsPreviousPage"] == null) { Session.Clear(); }

                Label control = (Label)Page.Controls[0].FindControl("lblPageTitle");
                if (control != null) { control.Text = strTitulo; }
                Page.Form.DefaultButton = btnSend.UniqueID;

                if (Session["resultado"] != null)
                {
                    resultado = (DataTable)Session["resultado"];
                    grdRecords.DataSource = resultado;
                    grdRecords.DataBind();
                    if (PreviousPage != null)
                    {
                        if (PreviousPage.IsValid)
                        {
                            txtWorkOrder.Text = ((TextBox)Page.PreviousPage.FindControl(txtWorkOrder.UniqueID)).Text;
                        }
                    }
                    else if (Session["WorkOrder"] != null)
                    {
                        txtWorkOrder.Text = Session["WorkOrder"].ToString();
                    }
                    txtWorkOrder.ReadOnly = true;
                    lblOrder.Text = "Order: " + txtWorkOrder.Text;
                    btnSend.Visible = false;
                }

            }

        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtWorkOrder.Text.Trim()))
            {
                minlenght.Enabled = true;
                minlenght.ErrorMessage = "Work Order is required";
                minlenght.IsValid = false;
                
                return;
            }

            InterfazDAL_tticol125 idal = new InterfazDAL_tticol125();
            Ent_tticol125 obj = new Ent_tticol125();
            string strError = string.Empty;

            txtWorkOrder.Text = txtWorkOrder.Text.ToUpperInvariant();
            obj.pdno = txtWorkOrder.Text.ToUpperInvariant();
            resultado = idal.listaRegistrosOrden_Param(ref obj, ref strError, true); //DataTable resultado = TABLA; // 

            string findIn = string.Empty;
            // Validar si el numero de orden trae registros
            if (strError != string.Empty || resultado.Rows.Count < 1)
            {
                strError = string.Empty;
                // Si no encuentra registros en la principal busca en historico
                resultado = idal.listaRegistrosOrden_ParamHis(ref obj, ref strError);
                if (strError != string.Empty || resultado.Rows.Count < 1)
                {
                    OrderError.IsValid = false;
                    txtWorkOrder.Focus();
                    grdRecords.DataSource = "";
                    grdRecords.DataBind();
                    return;
                }
                findIn = " [ Find in  History ]";
                Session["update"] = 1;
            }

            lblOrder.Text = "Order: " + obj.pdno + findIn;
            grdRecords.DataSource = resultado;
            grdRecords.DataBind();

            if (Session["resultado"] == null) { Session["resultado"] = resultado; }

        }

        protected void grdRecords_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string prin = ((DataRowView)e.Row.DataItem).DataView.Table.Rows[e.Row.RowIndex]["T$PRIN"].ToString();
                // ((Button)e.Row.Cells[7].FindControl("btnPrint")).OnClientClick = "printTag(" + FilaSerializada.Trim() + ")";
                ((Button)e.Row.Cells[7].FindControl("btnPrint")).Text = (prin.Trim().Equals("2") ? "Print" : "Reprint");
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
                        resultado = (DataTable)Session["resultado"];
                        grdRecords.DataSource = resultado;
                        grdRecords.DataBind();
                        reg = resultado.Rows[index];
                        Session["FilaImprimir"] = reg;

                        StringBuilder script = new StringBuilder();
                        script.Append("ventanaImp = window.open('../Labels/whInvPrintLabel.aspx', ");
                        script.Append("'ventanaImp', 'menubar=0,resizable=0,width=580,height=450');");
                        script.Append("ventanaImp.moveTo(30, 0);");
                        //script.Append("setTimeout (ventanaImp.close(), 20000);");
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "printTag", script.ToString(), true);



                    }
                }
            }
        }
    }
}