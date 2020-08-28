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
using System.Drawing;
using System.EnterpriseServices;
using System.Web.Services;
using iTextSharp.text.pdf.interfaces;
using Newtonsoft.Json;
using whusa.Utilidades;

namespace whusap.WebPages.InvMaterial
{
    public partial class whInvMaterialRejectedD : System.Web.UI.Page
    {
        #region Propiedades

        InterfazDAL_tticol127 idal127 = new InterfazDAL_tticol127();
        public List<Ent_tticol127> ListaItems = new List<Ent_tticol127>();
        public List<Ent_tticol127> ListaLotes = new List<Ent_tticol127>();
        public string ListaItemsJSON = string.Empty;
        public string ListaLotesJSON = string.Empty;

        DataTable DTregrind = new DataTable();

        string strError = string.Empty;
        string strOrden = string.Empty;
        string stritem = String.Empty;

        //Manejo idioma
        private static Mensajes _mensajesForm = new Mensajes();
        private static LabelsText _textoLabels = new LabelsText();
        private static string formName;
        private static string globalMessages = "GlobalMessages";
        public static string _idioma;

        public string FactorKG = string.Empty;

        public bool corregible = false;

        string MyRegrind = string.Empty;
        string MyRegrindDesc = string.Empty;


        //Etiquetas Errores

        public string lblitemError    = string.Empty;
        public string lblLotError = string.Empty;

        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            

            corregible = false;
            //Obtencion de listas Asociadas a Lotes e items
            //ListaLotes = TraerListaLotes();
            ListaItems = TraerListaItems();
            //
            txtItem.Focus();
            Page.Form.DefaultButton = btnSend.UniqueID;

            var ctrlName = Request.Params[Page.postEventSourceID];
            var args = Request.Params[Page.postEventArgumentID];

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
            //this.txtItem.Attributes.Add("onkeypress", "button_click(this," + this.btnSend.ClientID + ")");
            //this.txtLot.Attributes.Add("onkeypress", "button_click(this," + this.btnSend.ClientID + ")");

        }

        protected void btnSend_Click(object sender, EventArgs e)
        {

            Session["txtItem"] = txtItem.Text.ToUpper();
            Session["txtLot"] = string.IsNullOrEmpty(txtLot.Text.ToUpper()) || string.IsNullOrWhiteSpace(txtLot.Text.ToUpper()) ? " " : txtLot.Text.ToUpper();

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

            //if (string.IsNullOrEmpty(txtLot.Text.Trim()))
            //{
            //    RequiredFieldLot.Enabled = true;
            //    RequiredFieldLot.IsValid = false;
            //    txtLot.Focus();
            //    btnSave.Visible = false;
            //    grdRecords.DataSource = "";
            //    grdRecords.DataBind();
            //    return;
            //}

            // Validar si el usuario tiene almacen MRB asociado
            //Validar si el Item y Lote digitado son correctos
            InterfazDAL_tticol127 idal = new InterfazDAL_tticol127();
            Ent_tticol127 obj = new Ent_tticol127();
            string strError = string.Empty;

            obj.user = Session["user"].ToString().ToUpper();


            obj.item = Session["txtItem"].ToString();
            obj.lote = Session["txtLot"].ToString();


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
                lblResult.Text = strError.StartsWith("User") ? mensajes("notwarehouseuser")
                                : strError.StartsWith("Lot") ? mensajes("lotnotexists")
                                : strError.StartsWith("Item") && strError.Contains("MRB") ? mensajes("notstockitem")
                                : mensajes("itemnotexists");


                return;
            }
            else
            {
 
                grdRecords.DataSource = resultado;
                grdRecords.DataBind();
                grdRecords.DataSource = resultado;
                grdRecords.DataBind();
                Session["Orden"] = strOrden;
                Session["Lote"] = txtLot.Text.ToUpperInvariant();
                this.HeaderGrid.Visible = true;
                //btnSave.Visible = true;
                lblResult.Text = "";
            }
            txtItem.Text = "";
            txtLot.Text = "";
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
            //var Regrind;

            foreach (GridViewRow row in grdRecords.Rows)
            {
                string toreturn = ((TextBox)row.Cells[7].FindControl("toReturn")).Text;
                string lote = ((TextBox)row.Cells[7].FindControl("toReturn")).Text;
                reason = ((DropDownList)row.Cells[9].FindControl("Reasonid")).SelectedValue;
                disposition = ((DropDownList)row.Cells[8].FindControl("Dispoid")).SelectedValue;
                stockw = ((DropDownList)grdRecords.Rows[0].Cells[1].FindControl("Stockwareh")).SelectedValue;
                DataTable DTMyRegrind = new DataTable();
                Session["MyRegrind"] = ((DropDownList)grdRecords.Rows[0].Cells[1].FindControl("Regrind")).SelectedValue.ToString().Trim();
                if (stockw == String.Empty)
                {
                    stockw = "NA";
                }
                string regrind = ((DropDownList)grdRecords.Rows[0].Cells[2].FindControl("Regrind")).SelectedValue;
                if (regrind == String.Empty)
                {
                    regrind = "NA";
                }
                string obse = ((TextBox)grdRecords.Rows[0].Cells[3].FindControl("Comments")).Text;
                if (obse.Length > 255)
                {
                    obse = ((TextBox)grdRecords.Rows[0].Cells[3].FindControl("Comments")).Text.Substring(1, 255);
                }
                string supplier = ((DropDownList)grdRecords.Rows[0].Cells[0].FindControl("Supplier")).SelectedValue;
                if (supplier == string.Empty)
                {
                    supplier = " ";
                }
                if (Session["Lote"].ToString() == "" || Session["Lote"].ToString() == " ")
                {
                    Session["Lote"] = " ";
                }

                if (!toreturn.Equals(string.Empty))
                {
                    obj = new Ent_tticol118();
                    obj.item = "         " + Session["txtItem"].ToString().Trim().ToUpperInvariant();
                    obj.cwar = row.Cells[2].Text.ToUpperInvariant();
                    //obj.clot = row.Cells[3].Text.ToUpperInvariant();
                    obj.clot = Session["Lote"].ToString();
                    obj.qtyr = Double.Parse(toreturn, System.Globalization.CultureInfo.InvariantCulture);//Convert.ToDecimal(toreturn);
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
            //int actualizar = idal.actualizarRegistro_Param(ref parameterCollection, ref strError);
            //if (actualizar < 1)

            if (string.IsNullOrEmpty(reason) || string.IsNullOrWhiteSpace(reason))
            {
                corregible = true;
                strError = _textoLabels.readStatement(formName, _idioma, "lblReasoNull");
            }
            else
            {
                idal.insertarRegistro(ref parameterCollection, ref strError);
            }



            if (strError != string.Empty)
            {
                lblResult.Text = mensajes("errorsave");
            }
            else
            {
                printResult.Visible = true;
                if (Convert.ToInt32(disposition) == 4 || Convert.ToInt32(disposition) == 5)
                {

                    DataTable resultado = idal.invLabel_registroImprimir_Param(ref obj, ref strError);
                    resultado.Columns.Add("USER", typeof(String));
                    DataRow reg = resultado.Rows[0];
                    resultado.Rows[0]["USER"] = Session["user"].ToString().ToUpper();
                    Session["FilaImprimir"] = reg;
                    //printLabel.Visible = true;
                }
                lblResult.Text = strError;
                lblResult.Text = mensajes("msjupdt");
                this.HeaderGrid.Visible = false;

            }

            if (corregible == false)
            {
                grdRecords.DataSource = "";
                grdRecords.DataBind();
                grdRecords.DataSource = "";
                grdRecords.DataBind();
            }

            if (strError != string.Empty)
            {
                lblResult.Text = strError;
                //throw new System.InvalidOperationException(strError);
            }
            else
            {


                //Print Label Inmmediatly

                if (Convert.ToInt32(disposition) == 4 || Convert.ToInt32(disposition) == 5)
                {

                    //obj.item = txtItem.Text;

                    //obj.clot = txtLot.Text;

                    lblResult.Text = string.Empty;
                    
                    DataTable resultadop = idal.listaRegistros_Param(ref obj, ref strError);

                    resultadop.Columns.Add("FactorKG", typeof(string));

                    resultadop.Rows[0]["FactorKG"] = FactorKG.Trim();

                    Session["resultadop"] = resultadop;

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
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "alert('Information Saved Successfully.');", true);
                    lblResult.Text = mensajes("msjupdt");
                    lblResult.Visible = true;
                }



            }

        }

        protected void printLabel_Click(object sender, EventArgs e)
        {

            InterfazDAL_tticol118 idal = new InterfazDAL_tticol118();
            Ent_tticol118 obj = new Ent_tticol118();
            string strError = string.Empty;
            obj.item = Session["txtItem"].ToString();
            obj.clot = Session["txtLot"].ToString();
            lblResult.Text = string.Empty;
            DataTable resultado = idal.listaRegistros_Param(ref obj, ref strError);
            resultado.Columns.Add("FactorKG", typeof(string));
            resultado.Rows[0]["FactorKG"] = FactorKG.Trim();
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
                stritem = ((DataRowView)e.Row.DataItem).DataView.Table.Rows[e.Row.RowIndex]["Item"].ToString();

                if (Convert.ToDouble(stock) == 0)
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

                //Llenar Reason Dropdownlist
                InterfazDAL_tticol118 idal = new InterfazDAL_tticol118();
                DataTable reason = idal.listaReason_Param(ref strError);
                DropDownList listreason = (DropDownList)e.Row.Cells[9].FindControl("Reasonid");
                listreason.DataSource = reason;
                listreason.DataTextField = "descr";
                listreason.DataValueField = "reason";
                listreason.DataBind();
                listreason.Items.Insert(0, new ListItem(_idioma == "INGLES" ? "Select Reason here..." : "Seleccione una razón...", string.Empty));

                //DataTable proveedor = idalp.listaProveedores_Param(ref obj, ref strError);
                //DropDownList listproveedor = (DropDownList)e.Row.Cells[9].FindControl("Supplier");
                //listproveedor.DataSource = proveedor;
                //listproveedor.DataTextField = "nombre";
                //listproveedor.DataValueField = "proveedor";
                //listproveedor.DataBind();
                //listproveedor.Items.Insert(0, new ListItem("Select Supplier here...", string.Empty));


                ////Llenar Reason Dropdownlist
                //InterfazDAL_tticol118 idal = new InterfazDAL_tticol118();
                //DataTable reason = idal.listaReason_Param(ref strError);
                //DropDownList listreason = (DropDownList) e.Row.Cells[10].FindControl("Reasonid");
                //listreason.DataSource = reason;
                //listreason.DataTextField = "descr";
                //listreason.DataValueField = "reason";
                //listreason.DataBind();
                //listreason.Items.Insert(0, new ListItem("Select Reason here...", string.Empty));               

                ////Llenar Disposition Dropdownlist desde el webconfig
                //////if (ConfigurationManager.AppSettings.AllKeys.Contains("Disposition"))
                //////{
                //////    string listdisposition = ConfigurationManager.AppSettings["Disposition"];

                //////    var dispositionarray = listdisposition.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries).ToList<string>();
                //////    dispositionarray.Insert(0, "-- Select Disposition --");

                //////    DropDownList dispoid = (DropDownList)e.Row.Cells[11].FindControl("Dispositionid");
                //////    dispoid.DataSource = dispositionarray;
                //////    dispoid.DataBind();
                //////}
                //////else { lblResult.Visible = true; lblResult.Text = "Disposition List not found"; }

                ////Llenar Stock Warehouse Dropdownlist
                //InterfazDAL_tticol118 idalsw = new InterfazDAL_tticol118();
                //DataTable stockw = idalsw.listaStockw_Param(ref strError);
                //DropDownList liststockw = (DropDownList) e.Row.Cells[11].FindControl("Stockwareh");
                //liststockw.DataSource = stockw;
                //liststockw.DataTextField = "descrw";
                //liststockw.DataValueField = "warehouse";
                //liststockw.DataBind();
                //liststockw.Items.Insert(0, new ListItem("Select Stock Warehouse here...", string.Empty));

                ////Llenar Stock Warehouse Dropdownlist
                //InterfazDAL_tticol118 idalr = new InterfazDAL_tticol118();
                //DataTable regrind = idalr.listaRegrind_Param(ref strError);
                //DropDownList listregrind = (DropDownList)e.Row.Cells[12].FindControl("Regrind");
                //listregrind.DataSource = regrind;
                //listregrind.DataTextField = "descrc";
                //listregrind.DataValueField = "item";
                //listregrind.DataBind();
                //listregrind.Items.Insert(0, new ListItem("Select Item here...", string.Empty));
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Llenar Stock Warehouse Dropdownlist
                InterfazDAL_tticol118 idalp = new InterfazDAL_tticol118();
                Ent_tticol118 obj = new Ent_tticol118();
                obj.item = stritem;
                DataTable proveedor = idalp.listaProveedores_Param(ref obj, ref strError);
                DropDownList listproveedor = (DropDownList)e.Row.Cells[0].FindControl("Supplier");
                listproveedor.DataSource = proveedor;
                listproveedor.DataTextField = "nombre";
                listproveedor.DataValueField = "proveedor";
                listproveedor.DataBind();
                listproveedor.Items.Insert(0, new ListItem(_idioma == "INGLES" ? "Select Supplier here..." : "Seleccione un proveedor...", string.Empty));

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
                DropDownList liststockw = (DropDownList)e.Row.Cells[1].FindControl("Stockwareh");
                liststockw.DataSource = stockw;
                liststockw.DataTextField = "warehouseFullName";
                liststockw.DataValueField = "warehouse";
                liststockw.DataBind();
                liststockw.Items.Insert(0, new ListItem(_idioma == "INGLES" ? "Select Stock Warehouse here..." : "Seleccione inventario almacen...", string.Empty));

                //Llenar Stock Warehouse Dropdownlist
                InterfazDAL_tticol118 idalr = new InterfazDAL_tticol118();
                DTregrind = idalr.listaRegrind_ParamV2(ref stritem, ref strError);
                Session["DTregrind"] = DTregrind;
                //DataTable regrind = idalr.listaRegrind_Param(ref strError);
                DropDownList listregrind = (DropDownList)e.Row.Cells[2].FindControl("Regrind");
                listregrind.DataSource = DTregrind;
                listregrind.DataTextField = "descrc";
                listregrind.DataValueField = "item";
                listregrind.DataBind();
                listregrind.Items.Insert(0, new ListItem(_idioma == "INGLES" ? "Select Item here..." : "Seleccione un articulo...", string.Empty));

                
            }
        }

        //protected void grdRecords_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        //Llenar Stock Warehouse Dropdownlist
        //        InterfazDAL_tticol118 idalp = new InterfazDAL_tticol118();
        //        Ent_tticol118 obj = new Ent_tticol118();
        //        obj.item = stritem;
        //        DataTable proveedor = idalp.listaProveedores_Param(ref obj, ref strError);
        //        DropDownList listproveedor = (DropDownList)e.Row.Cells[0].FindControl("Supplier");
        //        listproveedor.DataSource = proveedor;
        //        listproveedor.DataTextField = "nombre";
        //        listproveedor.DataValueField = "proveedor";
        //        listproveedor.DataBind();
        //        listproveedor.Items.Insert(0, new ListItem(_idioma == "INGLES" ? "Select Supplier here..." : "Seleccione un proveedor...", string.Empty));

        //        //Llenar Disposition Dropdownlist desde el webconfig
        //        ////if (ConfigurationManager.AppSettings.AllKeys.Contains("Disposition"))
        //        ////{
        //        ////    string listdisposition = ConfigurationManager.AppSettings["Disposition"];

        //        ////    var dispositionarray = listdisposition.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries).ToList<string>();
        //        ////    dispositionarray.Insert(0, "-- Select Disposition --");

        //        ////    DropDownList dispoid = (DropDownList)e.Row.Cells[11].FindControl("Dispositionid");
        //        ////    dispoid.DataSource = dispositionarray;
        //        ////    dispoid.DataBind();
        //        ////}
        //        ////else { lblResult.Visible = true; lblResult.Text = "Disposition List not found"; }

        //        //Llenar Stock Warehouse Dropdownlist
        //        InterfazDAL_tticol118 idalsw = new InterfazDAL_tticol118();
        //        DataTable stockw = idalsw.listaStockw_Param(ref strError);
        //        DropDownList liststockw = (DropDownList)e.Row.Cells[1].FindControl("Stockwareh");
        //        liststockw.DataSource = stockw;
        //        liststockw.DataTextField = "warehouseFullName";
        //        liststockw.DataValueField = "warehouse";
        //        liststockw.DataBind();
        //        liststockw.Items.Insert(0, new ListItem(_idioma == "INGLES" ? "Select Stock Warehouse here..." : "Seleccione inventario almacen...", string.Empty));

        //        //Llenar Stock Warehouse Dropdownlist
        //        InterfazDAL_tticol118 idalr = new InterfazDAL_tticol118();
        //        DataTable regrind = idalr.listaRegrind_ParamV2(ref stritem, ref strError);
        //        //DataTable regrind = idalr.listaRegrind_Param(ref strError);
        //        DropDownList listregrind = (DropDownList)e.Row.Cells[2].FindControl("Regrind");
        //        listregrind.DataSource = regrind;
        //        listregrind.DataTextField = "descrc";
        //        listregrind.DataValueField = "item";
        //        listregrind.DataBind();
        //        listregrind.Items.Insert(0, new ListItem(_idioma == "INGLES" ? "Select Item here..." : "Seleccione un articulo...", string.Empty));
        //    }
        //}

        #endregion

        #region Metodos

        //Falta mensajes de este metodo
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
            DropDownList liststock = (DropDownList)grdRecords.Rows[0].Cells[0].FindControl("Stockwareh");
            DropDownList listregrind = (DropDownList)grdRecords.Rows[0].Cells[1].FindControl("Regrind");
            DropDownList listproveedor = (DropDownList)grdRecords.Rows[0].Cells[2].FindControl("Supplier");

            btnSave.Visible = true;
            btnSave.Enabled = true;

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


                if (listregrind.Items.Count > 1)
                {
                    btnSave.Visible = true;
                    btnSave.Enabled = true;
                }
                else
                {
                    btnSave.Visible = false;
                    btnSave.Enabled = false;
                }
            }
            if (listdispo.SelectedValue == "5")
            {
                liststock.Enabled = false;
                listproveedor.Enabled = false;
                listregrind.Enabled = false;
            }

            if (listdispo.SelectedItem.Text == "Return to Vendor")
            {
                InterfazDAL_tticol118 idalp = new InterfazDAL_tticol118();
                Ent_tticol118 obj = new Ent_tticol118();
                obj.item = Session["txtItem"].ToString().Trim();
                DataTable proveedor = idalp.ListaProveedoresProducto(ref obj, ref strError);
                listproveedor.DataSource = proveedor;
                listproveedor.DataBind();
                listproveedor.Items.Insert(0, new ListItem(_idioma == "INGLES" ? "Select Supplier here..." : "Seleccione un proveedor...", string.Empty));
            }
            else
            {
                InterfazDAL_tticol118 idalp = new InterfazDAL_tticol118();
                Ent_tticol118 obj = new Ent_tticol118();
                obj.item = Session["txtItem"].ToString().Trim();
                listproveedor.DataSource = idalp.listaProveedores_Param(ref obj, ref strError);
                listproveedor.DataBind();
                listproveedor.Items.Insert(0, new ListItem(_idioma == "INGLES" ? "Select Supplier here..." : "Seleccione un proveedor...", string.Empty));
            }
        }

        protected void CargarIdioma()
        {

            grdRecords.Font.Size = FontUnit.Medium;
            lblDescItem.Text = _textoLabels.readStatement(formName, _idioma, "lblDescItem");
            lblDescLot.Text = _textoLabels.readStatement(formName, _idioma, "lblDescLot");
            btnSend.Text = _textoLabels.readStatement(formName, _idioma, "btnSend");
            //printLabel.Text = _textoLabels.readStatement(formName, _idioma, "printLabel");
            btnSave.Text = _textoLabels.readStatement(formName, _idioma, "btnSave");
            grdRecords.Columns[0].HeaderText = _textoLabels.readStatement(formName, _idioma, "headItem");
            grdRecords.Columns[1].HeaderText = _textoLabels.readStatement(formName, _idioma, "headDescription");
            grdRecords.Columns[2].HeaderText = _textoLabels.readStatement(formName, _idioma, "headWarehouse");
            grdRecords.Columns[5].HeaderText = _textoLabels.readStatement(formName, _idioma, "headLot");
            //grdRecords.Columns[4].HeaderText = _textoLabels.readStatement(formName, _idioma, ".");
            grdRecords.Columns[6].HeaderText = _textoLabels.readStatement(formName, _idioma, "headUnit");
            grdRecords.Columns[7].HeaderText = _textoLabels.readStatement(formName, _idioma, "headToReturn");
            grdRecords.Columns[8].HeaderText = _textoLabels.readStatement(formName, _idioma, "headDisposition");
            grdRecords.Columns[9].HeaderText = _textoLabels.readStatement(formName, _idioma, "headReason");
            grdRecords.Columns[10].HeaderText = _textoLabels.readStatement(formName, _idioma, "headSupplier");
            //grdRecords.Columns[1].HeaderText = _textoLabels.readStatement(formName, _idioma, "headStock");
            grdRecords.Columns[12].HeaderText = _textoLabels.readStatement(formName, _idioma, "headStock");
            grdRecords.Columns[13].HeaderText = _textoLabels.readStatement(formName, _idioma, "headRegrind");
            grdRecords.Columns[14].HeaderText = _textoLabels.readStatement(formName, _idioma, "headComments");

            //grdRecords.Columns[0].HeaderText = _textoLabels.readStatement(formName, _idioma, "headSupplier");
            ////grdRecords.Columns[1].HeaderText = _textoLabels.readStatement(formName, _idioma, "headStock");
            //grdRecords.Columns[2].HeaderText = _textoLabels.readStatement(formName, _idioma, "headStock");
            //grdRecords.Columns[3].HeaderText = _textoLabels.readStatement(formName, _idioma, "headRegrind");
            //grdRecords.Columns[4].HeaderText = _textoLabels.readStatement(formName, _idioma, "headComments");

            lblitemError = _textoLabels.readStatement(formName, _idioma, "lblItemError");
            lblLotError     = _textoLabels.readStatement(formName, _idioma, "lblLotError");
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

        protected void txtItem_TextChanged(object sender, EventArgs e)
        {
            if (ListaItems.Where(x => x.item.ToString().ToUpper().Trim() == Session["txtLot"].ToString().ToUpper().Trim())
                    .Count() > 0)
            {

            }
            else
            {
                RequiredField.Text = "item is not valid";
                RequiredField.Visible = true;
            }
        }
        protected void txtLot_TextChanged(object sender, EventArgs e)
        {
            if (ListaLotes.Where(x => x.lote.ToString().ToUpper().Trim() == txtLot.Text.ToUpper().Trim())
                    .Count() > 0)
            {

            }
            else
            {
                RequiredFieldLot.Text = "Lot is not valid";
                RequiredFieldLot.Visible = true;
            }
        }

        [WebMethod()]
        public static bool TraerListaLotes(string clot)
        {
            InterfazDAL_tticol127 idal = new InterfazDAL_tticol127();
            DataTable dtListaLotes = idal.Lista_Lotes(clot);
            bool existencia = dtListaLotes.Rows.Count > 0;

            return existencia;
        }


        [WebMethod]
        public static string Hello()
        {
            return "Hi";
        }

        [WebMethod]
        public static string Hello(string x)
        {
            return "Hi " + x;
        }


        public List<Ent_tticol127> TraerListaItems()
        {
            List<Ent_tticol127> lstItems = new List<Ent_tticol127>();

            DataTable DtListaItems = idal127.Lista_Items();
            foreach (DataRow row in DtListaItems.Rows)
            {
                Ent_tticol127 objTticol127 = new Ent_tticol127();
                objTticol127.item = row["item"].ToString().Trim();
                objTticol127.kltc = row["kltc"].ToString().Trim();
                lstItems.Add(objTticol127);
            }
            ListaItemsJSON = JsonConvert.SerializeObject(lstItems);
            return lstItems;
        }
    }
}
//
//