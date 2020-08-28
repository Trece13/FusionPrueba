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

namespace whusap.WebPages.InvMaterial
{
    public partial class whInvMaterialDevolutionConfirm : System.Web.UI.Page
    {
        string strError = string.Empty;
        string Aplicacion = "WEBAPP";


        protected void Page_Load(object sender, EventArgs e)
        {
            txtWorkOrder.Focus();
            Page.Form.DefaultButton = btnSend.UniqueID;

            var ctrlName = Request.Params[Page.postEventSourceID];
            var args = Request.Params[Page.postEventArgumentID];
            Page.Form.Unload += new EventHandler(Form_Unload);

            //HandleCustomPostbackEvent(ctrlName, args);
            String strTitulo = "Confirm Material Devolution";
            if (!IsPostBack)
            {
                Label control = (Label)Page.Controls[0].FindControl("lblPageTitle");
                if (control != null) { control.Text = strTitulo; }
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
            obj.conf = 2;   // CONFIRMED = NO
            lblResult.Text = string.Empty;
            DataTable resultado = idal.listaRegistrosporConfirmar_Param(ref obj, ref strError);


            // Validar si el numero de orden trae registros
            if (strError != string.Empty)
            {
                OrderError.IsValid = false;
                txtWorkOrder.Focus();
                btnSave.Visible = false;
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

            foreach (GridViewRow fila in grdRecords.Rows)
            {
                DropDownList lista = ((DropDownList)fila.Cells[7].Controls[1]);
                if (lista != null)
                {
                    if (Convert.ToInt32(lista.SelectedValue) == 1)
                    {
                        obj = new Ent_tticol125();
                        obj.pdno = txtWorkOrder.Text.Trim();
                        obj.pono = Convert.ToInt32(fila.Cells[0].Text);
                        obj.item = fila.Cells[1].Text;
                        obj.cwar = fila.Cells[3].Text.Trim();
                        obj.clot = string.IsNullOrEmpty(fila.Cells[4].Text) ? " " : fila.Cells[4].Text;
                        obj.reqt = Convert.ToDecimal(fila.Cells[5].Text);
                        obj.refcntd = "0";
                        obj.refcntu = "0";
                        obj.mess = " ";
                        obj.prin = Convert.ToInt32(((Label)fila.Cells[8].FindControl("prin")).Text);
                        obj.conf = Convert.ToInt32(lista.SelectedValue); ;
                        obj.idrecord = grdRecords.DataKeys[fila.RowIndex].Value.ToString();
                        //Requerimiento No. 46122.
                        //Insertar en la tabla ticol080
                        //CChaverra 28/07/2017
                        obj.user = Session["user"].ToString();
                        parameterCollection.Add(obj);
                    }
                }
            }
            InterfazDAL_tticol125 idal = new InterfazDAL_tticol125();
            idal.actualizarRegistro_Param(ref parameterCollection, ref strError, Aplicacion);
            printResult.Visible = true;
            lblResult.Text = "Records was saved succesfully.";
            this.HeaderGrid.Visible = false;

            grdRecords.DataSource = "";
            grdRecords.DataBind();

            if (strError != string.Empty)
            {
                lblResult.Text = strError;
                Label control = ((Label)Page.Controls[0].FindControl("lblPageTitle"));
                control.Text = strError;
                return;
                //throw new System.InvalidOperationException(strError);
            }

        }

        protected void grdRecords_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string prin = ((DataRowView)e.Row.DataItem).DataView.Table.Rows[e.Row.RowIndex]["T$PRIN"].ToString();
                // ((Button)e.Row.Cells[7].FindControl("btnPrint")).OnClientClick = "printTag(" + FilaSerializada.Trim() + ")";
                ((Label)e.Row.Cells[8].FindControl("prin")).Text = prin;
            }
        }

        protected void Form_Unload(object sender, EventArgs e)
        {
            Session["FilaImprimir"] = null;
            Session["resultado"] = null;
            Session["WorkOrder"] = null;
        }
    }
}