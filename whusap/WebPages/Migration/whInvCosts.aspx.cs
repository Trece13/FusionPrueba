using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using whusa.Interfases;
using whusa.Utilidades;
using whusa.Entidades;
using System.Configuration;
using System.Threading;
using System.Globalization;
using System.Data;

namespace whusap.WebPages.Migration
{
    public partial class whInvCosts : System.Web.UI.Page
    {
        #region Propiedades
        public string WorkorderhasnotbeeninitiatedPOP = string.Empty;
        private static InterfazDAL_tticol090 _idaltticol090 = new InterfazDAL_tticol090();
        private static InterfazDAL_tticol111 _idaltticol111 = new InterfazDAL_tticol111();
        private static InterfazDAL_ttccol301 _idalttccol301 = new InterfazDAL_ttccol301();
        private static InterfazDAL_tticst001 _idaltticst001 = new InterfazDAL_tticst001();
        private static InterfazDAL_tticol080 _idaltticol080 = new InterfazDAL_tticol080();
        private static InterfazDAL_tticol110 _idaltticol110 = new InterfazDAL_tticol110();
        private static Mensajes _mensajesForm = new Mensajes();
        private static LabelsText _textoLabels = new LabelsText();
        private static string _operator;
        public static string _idioma;
        private static string strError;
        private static string formName;
        private static string globalMessages = "GlobalMessages";
        private static DataTable _consultarTurno;
        public static DataTable _consultaMateriales = new DataTable();
        public static List<MyLioEntidad> LstTable = new List<MyLioEntidad>();
        public bool valstatwo = Convert.ToBoolean(ConfigurationManager.AppSettings["valstatwo"]);
        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            // Cambiar cultura para manejo de separador decimal
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-CO");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("es-CO");
            base.InitializeCulture();

            if (!IsPostBack)
            {
                formName = Request.Url.AbsoluteUri.Split('/').Last();
                if (formName.Contains('?'))
                {
                    formName = formName.Split('?')[0];
                }
                Label control = (Label)Page.Controls[0].FindControl("lblPageTitle");
                lblError.Text = "";
                lblConfirm.Text = "";

                if (Session["user"] == null)
                {
                    Response.Redirect(ConfigurationManager.AppSettings["UrlBase"] + "/WebPages/Login/whLogIni.aspx");

                }

                _operator = Session["user"].ToString();

                try
                {
                    _idioma = Session["ddlIdioma"].ToString();
                }
                catch (Exception)
                {
                    _idioma = "INGLES";
                }

                CargarIdioma();

                string strTitulo = mensajes("encabezado");
                control.Text = strTitulo;

                Ent_ttccol301 data = new Ent_ttccol301()
                {
                    user = _operator,
                    come = strTitulo,
                    refcntd = 0,
                    refcntu = 0
                };

                List<Ent_ttccol301> datalog = new List<Ent_ttccol301>();
                datalog.Add(data);

                _idalttccol301.insertarRegistro(ref datalog, ref strError);
            }
        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            Session["orno"] = txtOrder.Text.Trim().ToUpper();
            lblError.Text = String.Empty;
            lblConfirm.Text = String.Empty;

            var order = txtOrder.Text.Trim().ToUpper();

            if (order != String.Empty)
            {
                Ent_tticol090 data090 = new Ent_tticol090() { fpdn = order };
                var consultaOrden = _idaltticol090.lineClearance_verificaOrdenes_Param(ref data090, ref strError);

                if (consultaOrden.Rows.Count > 0)
                {
                    if (valstatwo)
                    {
                        if (consultaOrden.Rows[0]["STAT"].ToString() != "2")
                        {
                            lblError.Text = WorkorderhasnotbeeninitiatedPOP;
                            return;
                        }
                    }
                    _consultarTurno = _idaltticol111.findRecordByPdno(ref order, ref strError);

                    if (_consultarTurno.Rows.Count > 0)
                    {
                        string shift = _consultarTurno.Rows[0]["shif"].ToString().Trim();

                        _consultaMateriales = _idaltticst001.findByPdnoCosts(ref order, ref shift, ref strError);

                        if (_consultaMateriales.Rows.Count > 0)
                        {
                            LstTable.Clear();
                            foreach (DataRow item in _consultaMateriales.Rows)
                            {


                                MyLioEntidad MyLioEntidadObj = new MyLioEntidad();

                                MyLioEntidadObj.PDNO = item["PDNO"].ToString();
                                MyLioEntidadObj.PONO = item["PONO"].ToString();
                                MyLioEntidadObj.CWAR = item["CWAR"].ToString();
                                MyLioEntidadObj.CLOT = item["CLOT"].ToString();
                                MyLioEntidadObj.SITM = item["SITM"].ToString();
                                MyLioEntidadObj.DSCA = item["DSCA"].ToString();
                                MyLioEntidadObj.CUNI = item["CUNI"].ToString();
                                MyLioEntidadObj.CANT_EST = item["CANT_EST"].ToString();
                                MyLioEntidadObj.ACT_CANT = item["ACT_CANT"].ToString();
                                MyLioEntidadObj.ISWH = item["ISWH"].ToString().Trim() == "" ? "0" : item["ISWH"].ToString().Trim();
                                MyLioEntidadObj.OQMF = item["OQMF"].ToString();
                                MyLioEntidadObj.CANTD = item["CANTD"].ToString();
                                MyLioEntidadObj.MCNO = item["MCNO"].ToString();
                                //MyLioEntidadObj.cant_hidden = "";
                                DataTable dt215 = _idaltticol090.ConsultarCantidad215(MyLioEntidadObj, ref strError);
                                DataTable dt022044131 = _idaltticol090.ConsultarCantidadPoritem022042131(MyLioEntidadObj, ref strError);

                                if (dt022044131.Rows.Count > 0)
                                {
                                    MyLioEntidadObj.STOCK = (Convert.ToDecimal(string.IsNullOrEmpty(MyLioEntidadObj.ISWH.Trim()) ? Convert.ToDecimal(0) : Convert.ToDecimal(MyLioEntidadObj.ISWH.Trim())) + Convert.ToDecimal(dt022044131.Rows[0]["QTYC"].ToString())).ToString();
                                }

                                if (dt215.Rows.Count > 0)
                                {
                                    MyLioEntidadObj.STOCK = (Convert.ToDecimal(dt215.Rows[0]["T$STOC"].ToString()) - Convert.ToDecimal(string.IsNullOrEmpty(MyLioEntidadObj.ACT_CANT.Trim()) ? Convert.ToDecimal(0) : Convert.ToDecimal(MyLioEntidadObj.ISWH.Trim()))).ToString();
                                }

                                LstTable.Add(MyLioEntidadObj);
                            }

                            foreach (MyLioEntidad item in LstTable)
                            {
                                item.cant_reg = quantity_reg_order_machine140(shift, item.MCNO, item.SITM, item.PDNO, "cant_reg") == string.Empty ? Convert.ToString(0) : quantity_reg_order_machine140(shift, item.MCNO, item.SITM, item.PDNO, "cant_reg");
                                item.cant_max = maxquantity_per_shift140(shift, item.MCNO, item.SITM, item.PDNO, "cant_max") == string.Empty ? Convert.ToString(Int32.MaxValue) : maxquantity_per_shift140(shift, item.MCNO, item.SITM, item.PDNO, "cant_max");
                                item.cant_proc = maxquantity_per_shift140(shift, item.MCNO, item.SITM, item.PDNO, "cant_proc") == string.Empty ? Convert.ToString(0) : maxquantity_per_shift140(shift, item.MCNO, item.SITM, item.PDNO, "cant_proc");
                            }
                            makeTable();
                            divBtnGuardar.Visible = true;
                            lblError.Text = String.Empty;
                        }
                        else
                        {
                            lblError.Text = mensajes("nomaterials");
                            return;
                        }

                    }
                    else
                    {
                        lblError.Text = mensajes("machinenotexists");
                        return;
                    }
                }
                else
                {
                    lblError.Text = mensajes("ordernotexists");
                    return;
                }
            }
            else
            {
                lblError.Text = mensajes("formempty");
                return;
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            lblError.Text = String.Empty;
            lblConfirm.Text = String.Empty;

            for (int i = 0; i < LstTable.Count; i++)
            {
                bool tticol088 = false;
                var txtQuantity = Request.Form["txtQuantity-" + i].ToString().Trim();
                var txtQuantityHidden = Request.Form["txtQuantityHidden-" + i].ToString().Trim();

                if ((txtQuantity.Trim() != string.Empty && txtQuantity.Trim() != "0") || (txtQuantityHidden.Trim() != string.Empty && txtQuantityHidden.Trim() != "0"))
                {
                    var orno = Convert.ToString(Session["orno"]);
                    var qune = double.Parse((txtQuantity.Trim() == "" ? "0" : txtQuantity.Trim()),CultureInfo.InvariantCulture.NumberFormat) == 0 ? double.Parse(txtQuantityHidden, CultureInfo.InvariantCulture.NumberFormat) : double.Parse(txtQuantity, CultureInfo.InvariantCulture.NumberFormat);
                    var pono = LstTable[i].PONO.ToString().Trim();
                    var item = LstTable[i].SITM.ToString().Trim();
                    var cwar = LstTable[i].CWAR.ToString().Trim();
                    var iswh = LstTable[i].ISWH.ToString().Trim();
                    var actcant = LstTable[i].ACT_CANT.ToString().Trim();
                    var stock = LstTable[i].STOCK.ToString().Trim();
                    var consultaRegistro = _idaltticol080.findRecordByOrnoPono(ref orno, ref pono, ref strError);





                    Ent_tticol080 data080 = new Ent_tticol080()
                    {
                        orno = orno,
                        pono = Convert.ToInt32(pono),
                        item = "         "+item,
                        cwar = cwar,
                        qune = Convert.ToDecimal(qune),
                        logn = _operator,
                        proc = 2,
                        refcntd = 0,
                        refcntu = 0,
                        clot = " ",
                        oorg = "4",
                        pick = 0
                    };

                    Ent_tticol088 data088 = new Ent_tticol088()
                    {
                        orno = orno,
                        pono = Convert.ToInt32(pono),
                        item = "         " + item,
                        cwar = cwar,
                        qune = Convert.ToDecimal(qune),
                        logn = _operator,
                        proc = 1,
                        refcntd = 0,
                        refcntu = 0,
                        oorg = "4"

                    };

                    if ((Convert.ToDecimal(stock.Trim()) - Convert.ToDecimal(iswh.Trim())) < Convert.ToDecimal(txtQuantityHidden.Trim()))
                    {
                        _idaltticol090.InsertTticol088(data088, ref strError);
                        lblError.Text += "[" + item + "] Available quantity not enough for your request <br/>";
                        tticol088 = true;
                        
                    }
                    if (tticol088 == false)
                    {
                        if (consultaRegistro.Rows.Count > 0)
                        {
                            var validaUptade = _idaltticol080.updateRecordCosts(ref data080, ref strError);

                            if (validaUptade)
                            {
                                //lblError.Text = String.Empty;
                                lblConfirm.Text += "[" + item + "]" + mensajes("msjupdate") + "<br/>";
                                //divTable.InnerHtml = String.Empty;
                                divBtnGuardar.Visible = false;
                                //txtOrder.Text = String.Empty;
                            }
                            else
                            {
                                lblError.Text += "[" + item + "]" + mensajes("errorupdt") + "<br/>";
                                return;
                            }
                        }
                        else
                        {
                            List<Ent_tticol080> lista = new List<Ent_tticol080>();
                            lista.Add(data080);
                            var tag = String.Empty;
                            var validInsert = _idaltticol080.insertarRegistro(ref lista, ref strError, ref tag);

                            if (validInsert > 0)
                            {
                                //lblError.Text = String.Empty;
                                lblConfirm.Text += "[" + item + "]" + mensajes("msjsave") + "<br/>";
                                //divTable.InnerHtml = String.Empty;
                                divBtnGuardar.Visible = false;
                                //txtOrder.Text = String.Empty;
                            }
                            else
                            {
                                lblError.Text += "[" + item + "]" + mensajes("errorsave") + "<br/>";
                               
                            }
                        }
                    }
                }
            }
            txtOrder.Text = String.Empty;
            divTable.InnerHtml = String.Empty;
        }

        #endregion

        #region Metodos

        public string quantity_reg_order_machine140(string SHIFT, string MCNO, string SITM, string PDNO, string PARA)
        {
            //var RegProcesados = _idaltticst001.quantity_reg_order_machine140(shift, MyLioEntidadObj.MCNO, MyLioEntidadObj.SITM);//Procesado   
            string Retorno = string.Empty;
            var RegProcesados = _idaltticst001.quantity_reg_order_machine140(SHIFT, MCNO, SITM, PDNO);//Procesado     
            if (RegProcesados.Rows.Count > 0)
            {
                Retorno = RegProcesados.Rows[0][PARA].ToString();
            }
            return Retorno;

        }
        public string maxquantity_per_shift140(string SHIFT, string MCNO, string SITM, string PDNO, string PARA)
        {
            //var RegPendientesPorProcesar = _idaltticst001.maxquantity_per_shift140(shift, MyLioEntidadObj.MCNO, MyLioEntidadObj.SITM);//Pendiente por procesar
            string Retorno = string.Empty;
            var RegPendientesPorProcesar = _idaltticst001.maxquantity_per_shift140(SHIFT, MCNO, SITM, PDNO);//Pendiente por procesar
            if (RegPendientesPorProcesar.Rows.Count > 0)
            {
                Retorno = RegPendientesPorProcesar.Rows[0][PARA].ToString();
            }
            return Retorno;
        }

        protected void makeTable()
        {
            var table = String.Empty;

            table += String.Format("<hr /><table class='table table-bordered' style='width:1000px; font-size:13px; border-style:outset; text-align:center; margin-bottom: 200px; border: none;'>");

            table += String.Format("<tr style='font-weight:bold; background-color:lightgray;'><td>{0}</td><td colspan='6'>{1}</td></tr>"
                , _idioma == "INGLES" ? "Order:" : "Orden:"
                , txtOrder.Text.Trim().ToUpper());

            table += String.Format("<tr style='font-weight:bold; background-color:white;'><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td><td>{6}</td><td style='display:none'>{7}</td></tr>"
                , _idioma == "INGLES" ? "Item" : "Articulo"
                , _idioma == "INGLES" ? "Description" : "Descripción"
                , _idioma == "INGLES" ? "Actual Quantity" : "Cantidad Actual"
                , _idioma == "INGLES" ? "To Issue by Warehousing" : "Para emitir por almacenaje"
                , _idioma == "INGLES" ? "Stock" : "Stock"
                , _idioma == "INGLES" ? "To Issue" : "Para emitir"
                , _idioma == "INGLES" ? "Unit" : "Unidad"
                , _idioma == "Hidden");



            for (int i = 0; i < LstTable.Count; i++)
            {
                //var cant_max = _consultaMateriales.Rows[i]["CANT_MAX"].ToString().Trim().ToUpper();
                //var cant_reg = _consultaMateriales.Rows[i]["CANT_REG"].ToString().Trim().ToUpper();

                table += String.Format("<tr><td>{0}</td><td>{1}</td><td style='text-align:left'>{2}</td><td style='text-align:left'>{3}</td><td>{4}</td><td>{5}</td><td>{6}</td><td style='display:none'>{7}</td><td style='border-top: none;border-bottom: none;border-right: none;'>{8}</td></tr>"
                    , LstTable[i].SITM.ToString().Trim().ToUpper()
                    , LstTable[i].DSCA.ToString().Trim().ToUpper()
                    , LstTable[i].ACT_CANT.ToString().Trim().ToUpper()
                    , LstTable[i].ISWH.ToString().Trim().ToUpper()
                    , LstTable[i].STOCK.ToString().Trim().ToUpper()

                    , String.Format("<input type='number' step='any' id='{0}' name='{0}' class='TextBox' onchange='validarCantidadLimiteArticuloMaquina(this,{1},{2},{3})' />"
                                    , "txtQuantity-" + i,
                                    LstTable[i].STOCK.ToString().Trim().ToUpper().Replace(",", "."),
                                    LstTable[i].ISWH.ToString().Trim().ToUpper().Replace(",", "."),
                                    i)
                    , LstTable[i].CUNI.ToString().Trim().ToUpper()
                    , String.Format("<input type='number' step='any' id='{0}' name='{0}' class='TextBox'  readonly/>"
                                    , "txtQuantityHidden-" + i)
                    , String.Format("<i style='display:none; color:red' class='fa fa-exclamation-triangle' aria-hidden='true' id={0} onclick='clickAlert({1})'></i>"
                                    , "btnAlert-" + i
                                    , i)
                    );
            }

            //foreach (DataRow item in _consultaMateriales.Rows)
            //{
            //    table += String.Format("<tr><td>{0}</td><td>{1]</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td></tr>"
            //        , item["SITM"].ToString().Trim().ToUpper()
            //        , item["DSCA"].ToString().Trim().ToUpper()
            //        , item["ACT_CANT"].ToString().Trim().ToUpper()
            //        , item["ISWH"].ToString().Trim().ToUpper()
            //        , String.Format("<input type='number' step='any' id='{0}' name='{0}' class='TextBox' onchange='validarCantidad(this,{1},{2})' />"
            //        ,"txtQuantity-")
            //        , item[""].ToString().Trim().ToUpper());
            //}

            table += "</table>";

            divTable.InnerHtml = table;
        }

        protected void CargarIdioma()
        {
            WorkorderhasnotbeeninitiatedPOP = _textoLabels.readStatement(formName, _idioma, "WorkorderhasnotbeeninitiatedPOP");
            lblOrder.Text = _textoLabels.readStatement(formName, _idioma, "lblOrder");
            btnConsultar.Text = _textoLabels.readStatement(formName, _idioma, "btnConsultar");
            btnGuardar.Text = _textoLabels.readStatement(formName, _idioma, "btnGuardar");
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