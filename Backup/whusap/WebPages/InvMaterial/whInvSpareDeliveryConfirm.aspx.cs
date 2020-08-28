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
namespace whusap.WebPages.InvMaterial
{
    public partial class whInvSpareDeliveryConfirm : System.Web.UI.Page
    {
        string strError = string.Empty;
        DataTable resultado = new DataTable();

        protected static InterfazDAL_ttscol100 idal = new InterfazDAL_ttscol100();
        Ent_ttscol100 obj = new Ent_ttscol100();

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

                string strTitulo = "Spare Delivery Confirm";
                Label control = (Label)Page.Controls[0].FindControl("lblPageTitle");
                control.Text = strTitulo;
                Page.Form.DefaultButton = btnSend.UniqueID;
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
            resultado = idal.spareDelivery_listaRegistrosOrdenParam(ref obj, ref strError);


            if (resultado.Rows.Count < 1 || !string.IsNullOrEmpty(strError))
            {
                lblResult.Text = string.Format("Order Work {0} : {1}", obj.orno.Trim(), strError);
                return;
            }

            grdRecords.DataSource = resultado;
            grdRecords.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            List<Ent_ttscol100> parameterCollection = new List<Ent_ttscol100>();
            Ent_ttscol100 obj = new Ent_ttscol100();

            foreach (GridViewRow fila in grdRecords.Rows)
            {
                DropDownList lista = ((DropDownList)fila.Cells[6].Controls[1]);
                if (lista != null)
                {
                    if (Convert.ToInt32(lista.SelectedValue) == 1)
                    {
                        obj = new Ent_ttscol100();
                        obj.orno = txtWorkOrder.Text.Trim();
                        obj.item = fila.Cells[0].Text;
                        obj.loca = fila.Cells[2].Text;
                        obj.cwar = fila.Cells[3].Text.Trim();
                        string toreturn = fila.Cells[4].Text;
                        obj.qdel = Decimal.Parse(toreturn, System.Globalization.CultureInfo.InvariantCulture);
                        obj.refcntd = 0;
                        obj.refcntu = 0;
                        obj.mess = " ";
                        obj.conf = Convert.ToInt32(lista.SelectedValue); ;
                        obj.cusr = Session["user"].ToString();

                        obj.idrecord = grdRecords.DataKeys[fila.RowIndex].Value.ToString();
                        parameterCollection.Add(obj);
                    }
                }
            }
            idal.actualizarRegistro_Param(ref parameterCollection, ref strError);
            if (strError != string.Empty)
            {
                lblResult.Text = strError;
                throw new System.InvalidOperationException(strError);
            } 
            printResult.Visible = true;
            lblResult.Text = "Records was saved succesfully.";

            grdRecords.DataSource = "";
            grdRecords.DataBind();
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
        }




    }
}