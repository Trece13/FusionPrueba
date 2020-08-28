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
using whusa.Utilidades;

namespace whusap.WebPages.InvMaterial
{
    public partial class whInvMaterialDevolReprintLabel : System.Web.UI.Page
    {
        #region Propiedades
            string strError = string.Empty;
            DataTable resultado = new DataTable();

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
                txtWorkOrder.Focus();
                this.SetFocus(Page.Form.UniqueID);



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

                    string strTitulo = mensajes("encabezado");

                    // Si la llamada no proviene de otro formulario
                    if (Session["IsPreviousPage"] == null) { Session.Clear(); }

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
                        lblOrder.Text = _idioma == "INGLES" ? "Order:" : "Orden:" + txtWorkOrder.Text;
                        btnSend.Visible = false;
                    }

                }

            }

        protected void btnSend_Click(object sender, EventArgs e)
            {
                if (string.IsNullOrEmpty(txtWorkOrder.Text.Trim()))
                {
                    minlenght.Enabled = true;
                    minlenght.ErrorMessage = mensajes("workrequired");
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
                    findIn = _idioma == "INGLES" ? " [ Find in  History ]" : " [ Buscar en Historial ]";
                    Session["update"] = 1;
                }

                lblOrder.Text = _idioma == "INGLES" ? "Order: " : "Orden: " + obj.pdno + findIn;
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
                    ((Button)e.Row.Cells[7].FindControl("btnPrint")).Text = (prin.Trim().Equals("2")
                        ? _idioma == "INGLES" ? "Print" : "Imprimir" : _idioma == "INGLES" ? "Reprint" : "Reimprimir");
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

        #endregion

        #region Metodos

        protected void CargarIdioma()
        {
            lblWorkOrder.Text = _textoLabels.readStatement(formName, _idioma, "lblWorkOrder");
            btnSend.Text = _textoLabels.readStatement(formName, _idioma, "btnSend");
            minlenght.ErrorMessage = _textoLabels.readStatement(formName, _idioma, "regularWorkOrder");
            RequiredField.ErrorMessage = _textoLabels.readStatement(formName, _idioma, "requiredWorkOrder");
            OrderError.ErrorMessage = _textoLabels.readStatement(formName, _idioma, "customWorkOrder");
            grdRecords.Columns[0].HeaderText = _textoLabels.readStatement(formName, _idioma, "headPosition");
            grdRecords.Columns[1].HeaderText = _textoLabels.readStatement(formName, _idioma, "headItem");
            grdRecords.Columns[2].HeaderText = _textoLabels.readStatement(formName, _idioma, "headDescription");
            grdRecords.Columns[3].HeaderText = _textoLabels.readStatement(formName, _idioma, "headWarehouse");
            grdRecords.Columns[4].HeaderText = _textoLabels.readStatement(formName, _idioma, "headUnit");
            grdRecords.Columns[5].HeaderText = _textoLabels.readStatement(formName, _idioma, "headReturnQty");
            grdRecords.Columns[6].HeaderText = _textoLabels.readStatement(formName, _idioma, "headLot");
            grdRecords.Columns[7].HeaderText = _textoLabels.readStatement(formName, _idioma, "headPallet");
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