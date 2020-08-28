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
using whusa.Utilidades;

namespace whusap.WebPages.InvMaterial
{
    public partial class whInvMaterialDevolutionConfirm : System.Web.UI.Page
    {
        #region Propiedades
            string strError = string.Empty;
            string Aplicacion = "WEBAPP";

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
                txtPalletId.Focus();
                Page.Form.DefaultButton = btnSend.UniqueID;

                var ctrlName = Request.Params[Page.postEventSourceID];
                var args = Request.Params[Page.postEventArgumentID];
                Page.Form.Unload += new EventHandler(Form_Unload);

                //HandleCustomPostbackEvent(ctrlName, args);
                
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

                    String strTitulo = mensajes("encabezado");


                    Label control = (Label)Page.Controls[0].FindControl("lblPageTitle");
                    if (control != null) { control.Text = strTitulo; }
                }
            }

        protected void btnSend_Click(object sender, EventArgs e)
            {

                //if (string.IsNullOrEmpty(txtPalletId.Text.Trim()))
                //{
                //    minlenght.Enabled = true;
                //    minlenght.ErrorMessage = mensajes("Please Fill all the Required  Fields.");
                //    minlenght.IsValid = false;

                //    return;
                //}

                InterfazDAL_tticol125 idal = new InterfazDAL_tticol125();
                Ent_tticol125 obj = new Ent_tticol125();
                string strError = string.Empty;
    
                obj.pdno = txtWorkOrder.Text.ToUpperInvariant();
                obj.conf = 2;   // CONFIRMED = NO
                obj.paid = txtPalletId.Text.ToUpperInvariant();
                lblResult.Text = string.Empty;
            //
                
                DataTable resultado = idal.listaRegistrosporConfirmar_Param(ref obj, ref strError);


                // Validar si el numero de orden trae registros
                if (strError != string.Empty)
                {
                    OrderError.IsValid = false;
                    OrderError.ForeColor = System.Drawing.Color.Red;
                    OrderError.ErrorMessage = strError;

                    txtWorkOrder.Focus();
                    btnSave.Visible = false;
                    return;
                }
                lblOrder.Text = _idioma == "INGLES" ? "Order: " : "Orden: " + obj.pdno;
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
                    DropDownList lista = ((DropDownList)fila.Cells[8].Controls[1]);
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
                            obj.reqt = Convert.ToDecimal(fila.Cells[6].Text);
                            obj.refcntd = "0";
                            obj.refcntu = "0";
                            obj.mess = " ";
                            
                            obj.paid = txtPalletId.Text.Trim();
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

                //Update status updataPalletStatus on table (whcol131, ticol022, ticol042) to delivered.   paramList.Add("p2", 9);// , whcol131 -Delieverd status=9
               // paramList.Add("p3", 11); // , ticol022, ticol042 -Delieverd status=11

                bool resultado = idal.updataPalletStatus(ref obj, ref strError);


                printResult.Visible = true;
                lblResult.Text = mensajes("msjsave");
                lblResult.ForeColor = System.Drawing.Color.Green;
                this.HeaderGrid.Visible = false;

                grdRecords.DataSource = "";
                grdRecords.DataBind();

                if (strError != string.Empty)
                {
                    lblResult.Text = strError;
                    lblResult.ForeColor = System.Drawing.Color.Red;
                    Label control = ((Label)Page.Controls[0].FindControl("lblPageTitle"));
                    control.Text = strError;
                    return;
                    //throw new System.InvalidOperationException(strError);
                }

            }

            [System.Web.Services.WebMethod()]
            ////public static string updataPalletStatus(string confirmstatus, string palletId)
            ////{
            ////    InterfazDAL_tticol125 idal = new InterfazDAL_tticol125();
            ////    Ent_tticol125 obj = new Ent_tticol125();
            ////    string strError = string.Empty;
            ////    obj.paid = palletId.ToUpperInvariant();
            ////    obj.conf = int.Parse(confirmstatus);
            ////    bool resultado = idal.updataPalletStatus(ref obj, ref strError);

            ////    // Validar si el numero de orden trae registros
            ////    if (resultado)
            ////    {
            ////        return _idioma == "INGLES" ? "Status updated successfully. " : "Status updated successfully. ";
            ////    }

            ////    return _idioma == "INGLES" ? "Failed to update statuses. " : "Failed to update statuses. ";
            ////}
        

        protected void OptionList_value(object sender, EventArgs e)
        {
            DropDownList ddl = (DropDownList)sender;
            if (ddl.SelectedIndex == 0)
            {
                return;
            }
            else
            {
                return;
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

        #endregion

        #region Metodos

        protected void CargarIdioma()
        {
            lblWorkOrder.Text = _textoLabels.readStatement(formName, _idioma, "lblWorkOrder");
            btnSend.Text = _textoLabels.readStatement(formName, _idioma, "btnSend");
            btnSave.Text = _textoLabels.readStatement(formName, _idioma, "btnSave");
            minlenght.ErrorMessage = _textoLabels.readStatement(formName, _idioma, "regularWorkOrder");
            RequiredField.ErrorMessage = _textoLabels.readStatement(formName, _idioma, "requiredWorkOrder");
            OrderError.ErrorMessage = _textoLabels.readStatement(formName, _idioma, "customWorkOrder");
            grdRecords.Columns[0].HeaderText = _textoLabels.readStatement(formName, _idioma, "headPosition");
            grdRecords.Columns[1].HeaderText = _textoLabels.readStatement(formName, _idioma, "headItem");
            grdRecords.Columns[2].HeaderText = _textoLabels.readStatement(formName, _idioma, "headDescription");
            grdRecords.Columns[3].HeaderText = _textoLabels.readStatement(formName, _idioma, "headWarehouse");
            grdRecords.Columns[4].HeaderText = _textoLabels.readStatement(formName, _idioma, "headLot");
            grdRecords.Columns[5].HeaderText = _textoLabels.readStatement(formName, _idioma, "headPalletID");
            grdRecords.Columns[6].HeaderText = _textoLabels.readStatement(formName, _idioma, "headReturnQty");
            grdRecords.Columns[7].HeaderText = _textoLabels.readStatement(formName, _idioma, "headUnit");
            grdRecords.Columns[8].HeaderText = _textoLabels.readStatement(formName, _idioma, "headConfirmed");
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