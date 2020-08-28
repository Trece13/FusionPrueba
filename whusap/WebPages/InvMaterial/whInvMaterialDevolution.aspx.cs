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
using whusa.Utilidades;
using whusa;
using System.Configuration;

namespace whusap.WebPages.InvMaterial
{
    public partial class whInvMaterialDevolution : System.Web.UI.Page
    {
        #region Propiedades
        private static string _operator;
        public static InterfazDAL_twhcol130 Itwhcol130 = new InterfazDAL_twhcol130();
        public static InterfazDAL_tticol022 Itticol022 = new InterfazDAL_tticol022();
        ClientScriptManager scriptBlock;
        Type csType;
        string strError = string.Empty;
        string Aplicacion = "WEBAPP";
        string ktlc = string.Empty;
        string Item = string.Empty;
        string Uni = string.Empty;
        //Manejo idioma
        public static string PalletIDdoesntexists = string.Empty;
        public static string RecordswithoutIDpallet = string.Empty;
        public static string QuantitytoreturnmustbeequalorlessthanPalletIDquantityforpalletId = string.Empty;
        public static string PalletIDstatusdoesntallowreturn = string.Empty;
        public static string Theamounttoreturnisinvalid = string.Empty;
        public static string Returnedquantityhigherthanpalletsize = string.Empty;

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
            Page.Form.DefaultButton = btnSend.UniqueID;

            var ctrlName = Request.Params[Page.postEventSourceID];
            var args = Request.Params[Page.postEventArgumentID];

            //HandleCustomPostbackEvent(ctrlName, args);
            //lblOrder.Visible = false;

            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
            base.InitializeCulture();

            if (!IsPostBack)
            {
                formName = Request.Url.AbsoluteUri.Split('/').Last();
                if (formName.Contains('?'))
                {
                    formName = formName.Split('?')[0];
                }

                if (Session["user"] == null)
                {
                    Response.Redirect(ConfigurationManager.AppSettings["UrlBase"] + "/WebPages/Login/whLogIni.aspx");
                }

                _operator = Session["user"].ToString();

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

            //lblOrder.Visible = true;
            InterfazDAL_tticol125 idal = new InterfazDAL_tticol125();
            Ent_tticol125 obj = new Ent_tticol125();
            string strError = string.Empty;

            txtWorkOrder.Text = txtWorkOrder.Text.ToUpperInvariant();
            obj.pdno = txtWorkOrder.Text.ToUpperInvariant();

            lblResult.Text = string.Empty;
            DataTable resultado = idal.listaRegistrosOrden_Param(ref obj, ref strError);

            if (resultado.Rows.Count > 0)
            {
                foreach (DataRow item in resultado.Rows)
                {
                    Ent_tticol125 MyObj = new Ent_tticol125();
                    MyObj.pdno = item["Orden"].ToString();
                    MyObj.pono = Convert.ToInt32(item["Pos"].ToString());

                    DataTable ResQtdl = idal.ConsultarQtdl(ref MyObj, ref strError);

                    int Qtdl = 0;

                    if (ResQtdl.Rows.Count > 0)
                    {
                        Qtdl = Convert.ToInt32(ResQtdl.Rows[0]["REQT"].ToString());
                    }

                    item["CANT"] = Convert.ToString(Convert.ToInt32(item["CANT"].ToString()) - Qtdl);
                }
            }

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
            //lblOrder.Text = _idioma == "INGLES" ? "Order: " : "Orden: " + obj.pdno;
            grdRecords.DataSource = resultado;
            grdRecords.DataBind();
            printLabel.Visible = false;
            this.HeaderGrid.Visible = true;
            btnSave.Visible = true;
            lblResult.Text = "";
        }

        public void validaBackend(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            String str = txt.Text;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            List<Ent_tticol125> parameterCollection = new List<Ent_tticol125>();
            Ent_tticol125 obj = new Ent_tticol125();
            //Recorrer filas con valores en los textos
            Session["WorkOrder"] = txtWorkOrder.Text.Trim();

            //string vpaid = validaPalletID("OPI009584-001", 1);
            String a = string.Empty;
            foreach (GridViewRow row in grdRecords.Rows)
            {
                string paid = string.Empty;
                string sec  = string.Empty;
                int sec022 = 0;
                int sec131 = 0;
                string toreturn = ((TextBox)row.Cells[6].FindControl("toReturn")).Text;
                string toLot = ((DropDownList)row.Cells[8].FindControl("toLot")).SelectedValue;
                string palletId = ((TextBox)row.Cells[9].FindControl("palletId")).Text;
                string condLote = ((HiddenField)row.Cells[6].FindControl("LOTE")).Value.Trim();
                ktlc = Convert.ToString(row.Cells[11].Text);
                Item = row.Cells[1].Text.ToUpperInvariant();
                Uni = row.Cells[5].Text.ToLower().Trim(); 
                bool reqLote = condLote == "1" ? true : false;
                
                //if (myObj.CUNI.Trim().ToUpper() != "PLT")
                //{
                //    MyConvertionFactor = FactorConversion(myObj.ITEM, myObj.CUNI, "PLT");
                //    QUANTITYPLT = (MyConvertionFactor.Tipo == "Div") ? Convert.ToDecimal((QUANTITYCUNI * MyConvertionFactor.FactorB) / MyConvertionFactor.FactorD) : Convert.ToDecimal((QUANTITYCUNI * MyConvertionFactor.FactorD) / MyConvertionFactor.FactorB);
                //    ciclosADVS = Convert.ToInt32(Math.Ceiling(QUANTITYPLT));
                //}

                //if (palletId.Trim() == string.Empty)
                //{
                //    lblResult.Text = RecordswithoutIDpallet;
                //    lblResult.ForeColor = System.Drawing.Color.Red;
                //    return;
                //}
                if ((reqLote && String.IsNullOrEmpty(toLot.Trim())) && !string.IsNullOrEmpty(toreturn))
                {
                    lblResult.Text = mensajes("lotcode");
                    lblResult.ForeColor = System.Drawing.Color.Green;
                    return;
                }

                //string toreturn = ((TextBox)row.Cells[6].FindControl("toReturn")).Text;
                //string toLot = ((TextBox)row.Cells[8].FindControl("toLot")).Text;

                if (!toreturn.Equals(string.Empty))
                {
                    

                    if (ktlc == "1")
                    {

                        Ent_twhcol130131 obj131 = new Ent_twhcol130131();
                        Ent_twhcol130131 MyObj = new Ent_twhcol130131();
                        DataTable dt022 = Itticol022.SecuenciaMayorRT(txtWorkOrder.Text.Trim().ToUpperInvariant());
                        DataTable dt131 = Itwhcol130.PaidMayorwhcol131(txtWorkOrder.Text.Trim().ToUpperInvariant() + "-RT");

                        if (dt022.Rows.Count > 0)
                        {
                            paid = dt022.Rows[0]["T$SQNB"].ToString().Trim();
                            sec = paid.Substring(12, 2);
                            sec022 = Convert.ToInt32(sec);
                        }

                        if (dt131.Rows.Count > 0)
                        {
                            paid = dt131.Rows[0]["T$PAID"].ToString().Trim();
                            sec = paid.Substring(12, 2);
                            sec131 = Convert.ToInt32(sec);
                        }

                        if (sec022 > sec131)
                        {
                            sec = addZero(sec022 + 1);
                        }
                        else if (sec022 < sec131)
                        {
                            sec = addZero(sec131 + 1);
                        }
                        else if (sec022 == sec131)
                        {
                            sec = addZero(sec022 + 1);
                        }

                        MyObj.OORG = "4";
                        MyObj.ORNO = txtWorkOrder.Text.Trim().ToUpperInvariant();
                        MyObj.ITEM = row.Cells[1].Text.ToUpperInvariant();
                        MyObj.PAID = txtWorkOrder.Text.Trim().ToUpperInvariant() + "-RT" + sec;
                        MyObj.PONO = Convert.ToInt32(row.Cells[0].Text).ToString();
                        MyObj.SEQN = "0";
                        MyObj.CLOT = string.IsNullOrEmpty(toLot) ? " " : toLot.ToUpperInvariant();
                        MyObj.CWAR = row.Cells[3].Text.ToUpperInvariant();
                        MyObj.QTYS = toreturn;// cantidad escaneada view 
                        MyObj.UNIT = row.Cells[5].Text.Trim();
                        MyObj.QTYC = toreturn;//cantidad escaneada view aplicando factor
                        MyObj.UNIC = row.Cells[5].Text.Trim();
                        MyObj.DATE = DateTime.Now.ToString("dd/MM/yyyy").ToString();//fecha de confirmacion 
                        MyObj.CONF = "1";
                        MyObj.RCNO = " ";//llena baan
                        MyObj.DATR = "01/01/70";//llena baan
                        MyObj.LOCA = " ";// enviamos vacio
                        MyObj.DATL = DateTime.Now.ToString("dd/MM/yyyy").ToString();//llenar con fecha de hoy
                        MyObj.PRNT = "1";// llenar en 1
                        MyObj.DATP = DateTime.Now.ToString("dd/MM/yyyy").ToString();//llena baan
                        MyObj.NPRT = "1";//conteo de reimpresiones 
                        MyObj.LOGN = _operator;// nombre de ususario de la session
                        MyObj.LOGT = " ";//llena baan
                        MyObj.STAT = "9";// LLENAR EN 1  
                        MyObj.DSCA = row.Cells[2].Text.ToUpperInvariant();
                        MyObj.COTP = "0";
                        MyObj.FIRE = "1";
                        MyObj.PSLIP = " ";
                        MyObj.ALLO = "0";

                        Itwhcol130.Insertartwhcol131(MyObj);

                    }
                    else if (ktlc == "2")
                    {

                        Ent_tticol022 obj022 = new Ent_tticol022();
                        List<Ent_tticol022> list022 = new List<Ent_tticol022>();
                        
                        DataTable dt022 = Itticol022.SecuenciaMayorRT(txtWorkOrder.Text.Trim().ToUpperInvariant());
                        DataTable dt131 = Itwhcol130.PaidMayorwhcol131(txtWorkOrder.Text.Trim().ToUpperInvariant() + "-RT");

                        if (dt022.Rows.Count > 0)
                        {
                            paid = dt022.Rows[0]["T$SQNB"].ToString().Trim();
                            sec = paid.Substring(12, 2);
                            sec022 = Convert.ToInt32(sec);
                        }

                        if (dt131.Rows.Count > 0)
                        {
                            paid = dt131.Rows[0]["T$PAID"].ToString().Trim();
                            sec = paid.Substring(12, 2);
                            sec131 = Convert.ToInt32(sec);
                        }

                        if( sec022 > sec131 ){
                            sec = addZero(sec022 + 1);
                        }
                        else if ( sec022 < sec131 )
                        {
                            sec = addZero( sec131 + 1 );
                        }
                        else if ( sec022 == sec131 )
                        {
                            sec = addZero(sec022 + 1);
                        }

                        obj022.pdno = txtWorkOrder.Text.Trim().ToUpperInvariant();
                        obj022.sqnb = txtWorkOrder.Text.Trim().ToUpperInvariant() + "-RT" + sec;
                        obj022.proc = 1;
                        obj022.logn = _operator;
                        obj022.mitm = row.Cells[1].Text.ToUpperInvariant().Trim();
                        obj022.qtdl = Convert.ToDecimal(toreturn);
                        obj022.cuni = row.Cells[5].Text.Trim();
                        obj022.log1 = "NONE";
                        obj022.qtd1 = Convert.ToInt32(toreturn);
                        obj022.pro1 = 1;
                        obj022.log2 = "NONE";
                        obj022.qtd2 = Convert.ToInt32(toreturn);
                        obj022.pro2 = 2;
                        obj022.loca = " ";
                        obj022.norp = 1;
                        obj022.dele = 11;
                        obj022.logd = "NONE";
                        obj022.refcntd = 0;
                        obj022.refcntu = 0;
                        obj022.drpt = DateTime.Now;
                        obj022.urpt = _operator;
                        obj022.acqt = Convert.ToDecimal(toreturn);
                        obj022.cwaf = row.Cells[3].Text.ToUpperInvariant();
                        obj022.cwat = row.Cells[3].Text.ToUpperInvariant();
                        obj022.aclo = " ";
                        obj022.allo = 0;


                        list022.Add(obj022);
                        Itticol022.insertarRegistroSimple(ref obj022, ref strError);
                        Itticol022.InsertarRegistroTicol222(ref obj022, ref strError);

                    }

                    obj = new Ent_tticol125();
                    obj.pdno = txtWorkOrder.Text.Trim().ToUpperInvariant();
                    obj.pono = Convert.ToInt32(row.Cells[0].Text);
                    obj.item = row.Cells[1].Text.ToUpperInvariant(); //.Trim();
                    obj.cwar = row.Cells[3].Text.ToUpperInvariant(); //.Trim();
                    obj.paid = txtWorkOrder.Text.Trim().ToUpperInvariant() + "-RT" + sec;
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
            lblResult.Text = mensajes("msjsave");
            lblResult.ForeColor = System.Drawing.Color.Green;
            this.HeaderGrid.Visible = false;

            grdRecords.DataSource = "";
            grdRecords.DataBind();
            btnSave.Visible = false;

            if (strError != string.Empty)
            {
                btnSave.Visible = false;

                lblResult.Text = mensajes("errorsave");
                lblResult.ForeColor = System.Drawing.Color.Red;
                throw new System.InvalidOperationException(strError);
            }
        }

        private string addZero(int secint)
        {
            string secRet = string.Empty;
            if (secint < 10)
            {
                secRet = "0" + ( secint ).ToString();
            }
            else
            {
                secRet = ( secint ).ToString();
            }
            return secRet;
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
                OrderError.ErrorMessage = mensajes("worktag");
                OrderError.ForeColor = System.Drawing.Color.Red;
                OrderError.IsValid = false;
                OrderError.ErrorMessage = MsgError;
                return;
            }
            //lblOrder.Text = _idioma == "INGLES" ? "Order: " : "Orden: " + obj.pdno;

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
                var rowIndex = e.Row.RowIndex;
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
                    filaIni[1] = _idioma == "INGLES" ? " - Select Lot - " : "- Seleccione un lote -";
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
                string quantityCheck = control.Text;
                control.Attributes.Add("onblur", "validaLot(this, " + FilaSerializada + ", '" + txtWorkOrder.Text.Trim() + "');");
                control.Attributes.Add("onchange", "validarCantidadMaxima(this, " + FilaSerializada + ", 0);validateQty('" + rowIndex + "')");
                lista.Attributes.Add("onchange", "validarCantidadMaxima(this, " + FilaSerializada + ", 1);");

                //TextBox palletIdTextBox = (TextBox)e.Row.Cells[10].FindControl("palletId");
                //Console.WriteLine("This is palletIDBox value : ", palletIdTextBox);
                //changing following line.
                //palletIdTextBox.Attributes.Add("onchange", "validaPaid(this,'" + rowIndex + "');");
                //palletIdTextBox.Attributes.Add("onchange", "validaPaid(this,drv,FilaSerializada, '" + quantityCheck + "');");

            }
        }

        public static Factor FactorConversion(string ITEM, string STUN, string CUNI)
        {
            Factor MyFactor = new Factor
            {
                MsgError = "No Tiene Factor",//mensajes("ItHasNoFactor"),
                FactorD = null,
                Tipo = string.Empty
            };

            DataTable DtFactor = new DataTable();
            DataTable ConvercionDiv = Itwhcol130.FactorConvercionMul(ITEM, CUNI, STUN);
            DataTable ConvercionMul = Itwhcol130.FactorConvercionDiv(ITEM, CUNI, STUN);

            if (ConvercionDiv.Rows.Count > 0)
            {
                MyFactor.MsgError = string.Empty;
                MyFactor.Tipo = "Div";
                MyFactor.FactorD = (decimal?)ConvercionDiv.Rows[0]["FACTOR"];
                MyFactor.FactorB = (decimal?)ConvercionDiv.Rows[0]["POTENCIA"];
            }
            else if (ConvercionMul.Rows.Count > 0)
            {
                MyFactor.MsgError = string.Empty;
                MyFactor.Tipo = "Mul";
                MyFactor.FactorD = (decimal?)ConvercionMul.Rows[0]["FACTOR"];
                MyFactor.FactorB = (decimal?)ConvercionMul.Rows[0]["POTENCIA"];
            }
            else if (ConvercionDiv.Rows.Count == 0 && ConvercionMul.Rows.Count == 0)
            {
                MyFactor = FactorConversion(string.Empty, STUN, CUNI);
                return MyFactor;
            }

            return MyFactor;

        }

        #endregion

        #region Metodos

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
        public static string vallidatePalletID(string palletID, string quantity)
        {
            //TextBox palletIdTextBox = (TextBox)e.Row.Cells[10].FindControl("palletId");
            //
            //quantityToReturn = "2";

            InterfazDAL_tticol125 idal = new InterfazDAL_tticol125();
            Ent_tticol125 obj = new Ent_tticol125();
            string strError = string.Empty;
            string retorno = string.Empty;
            //TextBox quantity = (TextBox)e.Row.Cells[10].FindControl("palletId");

            obj.paid = palletID.Trim().ToUpperInvariant();
            //obj.reqt = Convert.ToInt32(fila.GetValue(8).ToString().Trim());
            obj.reqt = Convert.ToDecimal(quantity);

            DataTable resultado = idal.vallidatePalletID(ref obj, ref strError);

            decimal palletQuantity = 0;
            decimal status;
            string tableName = string.Empty;
            decimal givenQuantity = Convert.ToDecimal(quantity);

            if (resultado.Rows.Count < 1) { return PalletIDdoesntexists; }
            else
            {
                foreach (DataRow dr in resultado.Rows)
                {
                    status = Convert.ToDecimal(dr.ItemArray[8].ToString());
                    palletQuantity = Convert.ToDecimal(dr.ItemArray[3].ToString());
                    tableName = dr.ItemArray[0].ToString();
                    if (palletQuantity < givenQuantity)
                    {
                        retorno = QuantitytoreturnmustbeequalorlessthanPalletIDquantityforpalletId + " " + palletID.Trim().ToUpperInvariant();
                        break;
                    }
                    if (((tableName == "whcol131") || (tableName == "whcol130")) && (status != 9))
                    {
                        
                        retorno = PalletIDstatusdoesntallowreturn
;
                        break;
                    }
                    else if (((tableName == "ticol022") || (tableName == "ticol042")) && (status != 11))
                    {
                        
                        retorno = PalletIDstatusdoesntallowreturn;
                        break;
                    }
                }
            }

            return retorno;
        }

        [System.Web.Services.WebMethod()]
        public static string validarCantidades(object fila, string valor, string lote, string qty)
        {
            InterfazDAL_tticol125 idal = new InterfazDAL_tticol125();
            Ent_tticol125 obj = new Ent_tticol125();
            string strError = string.Empty;
            string retorno = string.Empty;
            Factor MyConvertionFactor = new Factor { };
            Array row = fila.ToString().Replace(" ", "").Replace("\"", "").Split(',');

            try
            {
                valor = valor.Trim();
                lote = lote.Trim().ToUpperInvariant();
                if (!string.IsNullOrEmpty(valor))
                {
                    MyConvertionFactor = FactorConversion(row.GetValue(4).ToString().Trim(), row.GetValue(6).ToString().Trim(), "plt");
                    decimal QUANTITYCUNI = (MyConvertionFactor.Tipo == "Div") ? Convert.ToDecimal((Convert.ToDecimal(qty) * MyConvertionFactor.FactorB) / MyConvertionFactor.FactorD) : Convert.ToDecimal((Convert.ToDecimal(qty) * MyConvertionFactor.FactorD) / MyConvertionFactor.FactorB);
                    if (QUANTITYCUNI > 1)
                    {
                        strError =  Returnedquantityhigherthanpalletsize;
                        return strError;
                    }
                }
                if (!string.IsNullOrEmpty(valor) && !string.IsNullOrEmpty(lote))
                {
                    
                    double iCantMax = 0;

                    obj.pdno = row.GetValue(1).ToString().Trim();
                    obj.pono = Convert.ToInt32(row.GetValue(2).ToString().Trim());
                    obj.item = row.GetValue(4).ToString().Trim();
                    obj.clot = lote.ToString().Trim();

                    iCantMax = idal.cantidadMaximaPorLote(ref obj, ref strError);

                    if (double.Parse(valor) <= iCantMax)
                    {
                        return retorno;
                    }
                    else if (string.IsNullOrEmpty(strError))
                    {
                        
                        strError = Theamounttoreturnisinvalid;  
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

        protected void CargarIdioma()
        {
            PalletIDdoesntexists = _textoLabels.readStatement(formName, _idioma, "PalletIDdoesntexists");
            Returnedquantityhigherthanpalletsize = _textoLabels.readStatement(formName, _idioma, "Returnedquantityhigherthanpalletsize");
            RecordswithoutIDpallet = _textoLabels.readStatement(formName, _idioma, "RecordswithoutIDpallet");
            QuantitytoreturnmustbeequalorlessthanPalletIDquantityforpalletId = _textoLabels.readStatement(formName, _idioma, "QuantitytoreturnmustbeequalorlessthanPalletIDquantityforpalletId");
            PalletIDstatusdoesntallowreturn = _textoLabels.readStatement(formName, _idioma, "PalletIDstatusdoesntallowreturn");
            Theamounttoreturnisinvalid = _textoLabels.readStatement(formName, _idioma, "Theamounttoreturnisinvalid");
            lblWorkOrder.Text = _textoLabels.readStatement(formName, _idioma, "lblWorkOrder");
            btnSend.Text = _textoLabels.readStatement(formName, _idioma, "btnSend");
            printLabel.Text = _textoLabels.readStatement(formName, _idioma, "printLabel");
            btnSave.Text = _textoLabels.readStatement(formName, _idioma, "btnSave");
            minlenght.ErrorMessage = _textoLabels.readStatement(formName, _idioma, "regularWorkOrder");
            RequiredField.ErrorMessage = _textoLabels.readStatement(formName, _idioma, "requiredWorkOrder");
            OrderError.ErrorMessage = _textoLabels.readStatement(formName, _idioma, "customWorkOrder");
            //validateReturn.ErrorMessage = _textoLabels.readStatement(formName, _idioma, "btnSave");
            //validateQuantity.ErrorMessage = _textoLabels.readStatement(formName, _idioma, "btnSave");
            grdRecords.Columns[0].HeaderText = _textoLabels.readStatement(formName, _idioma, "headPosition");
            grdRecords.Columns[1].HeaderText = _textoLabels.readStatement(formName, _idioma, "headItem");
            grdRecords.Columns[2].HeaderText = _textoLabels.readStatement(formName, _idioma, "headDescription");
            grdRecords.Columns[3].HeaderText = _textoLabels.readStatement(formName, _idioma, "headWarehouse");
            grdRecords.Columns[4].HeaderText = _textoLabels.readStatement(formName, _idioma, "headActualQty");
            grdRecords.Columns[5].HeaderText = _textoLabels.readStatement(formName, _idioma, "headUnit");
            grdRecords.Columns[6].HeaderText = _textoLabels.readStatement(formName, _idioma, "headToReturn");
            grdRecords.Columns[7].HeaderText = _textoLabels.readStatement(formName, _idioma, "headLot");
            grdRecords.Columns[8].HeaderText = _textoLabels.readStatement(formName, _idioma, "headLot");
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