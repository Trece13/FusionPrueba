using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using whusa.Interfases;
using whusa.Utilidades;
using whusa.Entidades;
using System.Data;
using System.Globalization;
using System.Threading;
using System.Configuration;
using System.EnterpriseServices;
using System.Runtime.InteropServices;
using System.Web.Configuration;

namespace whusap.WebPages.Migration
{
    public partial class whInvMaterialRejectedPlan : System.Web.UI.Page
    {
        #region Propiedades
            private static InterfazDAL_tticol022 _idaltticol022 = new InterfazDAL_tticol022();
            private static InterfazDAL_ttcmcs005 _idalttcmcs005 = new InterfazDAL_ttcmcs005();
            private static InterfazDAL_tticol011 _idaltticol011 = new InterfazDAL_tticol011();
            private static InterfazDAL_tticol100 _idaltticol100 = new InterfazDAL_tticol100();
            private static InterfazDAL_ttccol301 _idalttccol301 = new InterfazDAL_ttccol301();
            private static Mensajes _mensajesForm = new Mensajes();
            private static LabelsText _textoLabels = new LabelsText();
            private static string _operator;
            public static string _idioma;
            private static string strError;
            private static string formName;
            private static string globalMessages = "GlobalMessages";
            public static DataTable _validarOrden = new DataTable();
            public DataTable listaReasons = new DataTable();
            public string UrlBaseBarcode = WebConfigurationManager.AppSettings["UrlBaseBarcode"].ToString();

            private string order = string.Empty;
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
            divBotones.Visible = false;
            divLabel.Visible = false;

            var order = txtWorkOrder.Text.Trim().ToUpper();

            if(order == String.Empty)
            {
                lblError.Text = mensajes("formempty");
                return;
            }

            _validarOrden = _idaltticol022.findRecordBySqnbRejectedPlant(ref order, ref strError);

            if (_validarOrden.Rows.Count > 0)
            {
                lblError.Text = string.Empty;
                //var sqnb = _validarOrden.Rows[0]["SQNB"].ToString().Trim();
                //var mitm = _validarOrden.Rows[0]["MITM"].ToString().Trim();
                //var dsca = _validarOrden.Rows[0]["DSCA"].ToString().Trim();
                //var qtdl = _validarOrden.Rows[0]["QTDL"].ToString().Trim();
                //var pdno = _validarOrden.Rows[0]["PDNO"].ToString().Trim();

                var dele = Convert.ToInt32(_validarOrden.Rows[0]["DELE"].ToString());
                var pro1 = Convert.ToInt32(_validarOrden.Rows[0]["PRO1"].ToString());
                var proc = Convert.ToInt32(_validarOrden.Rows[0]["PROC"].ToString());

                /////////////////////////////////////SQNB
                //lblOrdPonoSeqn.Text = sqnb;

                //var rutaServOrdPonoSeqn = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + lblOrdPonoSeqn.Text.Trim().ToUpper() + "&code=Code128&dpi=96";
                //imgOrdPonoSeqn.Src = !string.IsNullOrEmpty(lblOrdPonoSeqn.Text) ? rutaServOrdPonoSeqn : String.Empty;
                ///////////////////////////////////////MITM
                //var rutaServItem = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + mitm.ToUpper() + "&code=Code128&dpi=96";
                //imgItem.Src = !string.IsNullOrEmpty(mitm) ? rutaServItem : String.Empty;
                ///////////////////////////////////////////dsca
                //lblValueDescripcion.Text = dsca;
                /////////////////qtdl
                //lblValueUnit.Text = qtdl;

                //var rutaQtdl = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + qtdl.ToUpper() + "&code=Code128&dpi=96";
                //imgQuantity.Src = !string.IsNullOrEmpty(qtdl) ? rutaQtdl : String.Empty;
                /////////////////pdno
                //lblValueWorkOrder.Text = pdno;
                //var rutaPdno = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + pdno.ToUpper() + "&code=Code128&dpi=96";
                //imgWorkOrder.Src = !string.IsNullOrEmpty(qtdl) ? rutaServItem : String.Empty;

                if (proc == 2)
                {
                    lblError.Text = mensajes("palletnotannounced");
                    return;
                }

                if (dele != 2 && dele != 4)
                {
                    lblError.Text = mensajes("palletdeleted");
                    return;
                }

                if (pro1 == 1)
                {
                    lblError.Text = mensajes("palletconfirmed");
                    return;
                }

                makeTable();
                divBtnGuardar.Visible = true;
                divTable.Visible = true;
            }
            else 
            {
                lblError.Text = mensajes("palletnotexists");
                return;
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e) 
        {
            var validInsert = 0;
            var validUpdate = 0;
            Ent_tticol100 Objdata100 = new Ent_tticol100();
            for (int i = 0; i < _validarOrden.Rows.Count; i++)
            {
                var shift = Request.Form["txtShift-" + i].ToString().Trim();
                var reason = Request.Form["slReasons-" + i].ToString().Trim();
                var rejectType = Request.Form["txtRejectType-" + i].ToString().Trim();
                var exactReasons = Request.Form["txtExactReason-" + i].ToString().Trim();
                var pdno = txtWorkOrder.Text.Trim().ToUpper().Substring(0,9);
                var machine = " ";
                var consecutivo = 1;


                if (shift != String.Empty && exactReasons != String.Empty)
                {
                    var findMachine = _idaltticol011.findRecordByPdno(ref pdno, ref strError);

                    if (findMachine.Rows.Count > 0)
                    {
                        machine = findMachine.Rows[0]["MCNO"].ToString();
                    }

                    var consultaConsecutivo = _idaltticol100.findMaxSeqnByPdno(ref pdno, ref strError);

                    if (consultaConsecutivo.Rows.Count > 0)
                    {
                        consecutivo = Convert.ToInt32(consultaConsecutivo.Rows[0]["SEQN"]) + 1;
                    }

                    Ent_tticol100 data100 = new Ent_tticol100() 
                    { 
                        pdno = pdno,
                        seqn = consecutivo,
                        seqnR = _validarOrden.Rows[0][1].ToString().Trim().ToUpper(),
                        mcno = machine,
                        shif = shift,
                        item = _validarOrden.Rows[i]["MITM"].ToString().Trim().ToUpper(),
                        qtyr = double.Parse(_validarOrden.Rows[i]["QTDL"].ToString().Trim(), CultureInfo.InvariantCulture.NumberFormat),
                        cdis = reason,
                        rejt = Convert.ToInt32(rejectType),
                        clot = pdno,
                        obse = exactReasons,
                        logr = _operator,
                        dsca = _validarOrden.Rows[i]["DSCA"].ToString().Trim().ToUpper(),
                        cwar = _validarOrden.Rows[i]["CWAR"].ToString().Trim().ToUpper(),
                        paid = txtWorkOrder.Text.Trim().ToUpper(),
                    };
                    Objdata100 = data100;

                    validInsert += _idaltticol100.insertRecord(ref data100, ref strError);

                    if (validInsert > 0 )
                    {
                        
                        validUpdate = _idaltticol100.ActualizaRegistro_ticol022(ref data100, ref strError);
                    }


                }
            }

            if (validInsert > 0)
            {
                lblError.Text = "";
                lblConfirm.Text = mensajes("msjsave");
                divTable.InnerHtml = String.Empty;
                divBtnGuardar.Visible = false;
                txtWorkOrder.Text = String.Empty;
                MakeLabel(Objdata100);
                divLabel.Visible = true;
                divBotones.Visible = true;
            }
            else
            {
                lblError.Text = mensajes("errorsave");
                return;
            }
        }

        #endregion

        #region Metodos

        protected void makeTable()
        {
            var rstp = "10";
            listaReasons = _idalttcmcs005.findRecords(ref rstp, ref strError);
            
            var selectReasons = "<select id='slReasons' name='slReasons' class='TextboxBig'>";

            foreach (DataRow item in listaReasons.Rows)
            {
                selectReasons += String.Format("<option value='{0}'>{1}</option>", item["CDIS"].ToString().Trim(), item["CDIS"].ToString().Trim() + " - " + item["DSCA"].ToString().Trim());
            }

            selectReasons += "</select>";

            var table = String.Empty;

            //Fila cwar
            table += String.Format("<hr /><table class='table table-bordered' style='width:1200px; font-size:13px; border:3px solid; border-style:outset; text-align:center;'>");

            table += String.Format("<tr style='font-weight:bold; background-color:lightgray;'><td>{0}</td><td colspan='8'>{1}</td></tr>"
                , _idioma == "INGLES" ? "Order:" : "Orden:"
                , _validarOrden.Rows[0]["PDNO"].ToString().Trim().ToUpper());

            table += String.Format("<tr style='font-weight:bold; background-color:white;'><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td><td>{6}</td><td>{7}</td><td>{8}</td></tr>"
                , _idioma == "INGLES" ? "Pallet Id" : "Id Pallet"
                , _idioma == "INGLES" ? "Item" : "Articulo"
                , _idioma == "INGLES" ? "Description" : "Descripción"
                , _idioma == "INGLES" ? "Qty" : "Cant"
                , _idioma == "INGLES" ? "Unit" : "Unidad"
                , _idioma == "INGLES" ? "Shift" : "Cambio"
                , _idioma == "INGLES" ? "Reason" : "Razón"
                , _idioma == "INGLES" ? "Reject type" : "Tipo de devolución"
                , _idioma == "INGLES" ? "Exact Reason" : "Razón exacta");

            for (int i = 0; i <  _validarOrden.Rows.Count; i++)
            {
                table += String.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td><td>{6}</td><td>{7}</td><td>{8}</td></tr>"
                    , _validarOrden.Rows[i]["SQNB"].ToString().Trim().ToUpper()
                    , _validarOrden.Rows[i]["MITM"].ToString().Trim().ToUpper()
                    , _validarOrden.Rows[i]["DSCA"].ToString().Trim().ToUpper()
                    , _validarOrden.Rows[i]["QTDL"].ToString().Trim().ToUpper()
                    , _validarOrden.Rows[i]["CUNI"].ToString().Trim().ToUpper()
                    , String.Format("<input type='text' id='{0}' name='{0}' class='TextBoxBig' onchange='validarShift(this);' />", "txtShift-" + i)
                    , selectReasons.Replace("id='slReasons'","id='slReasons-" + i + "'").Replace("name='slReasons'","name='slReasons-" + i + "'")
                    , String.Format("<select id='{0}' name='{0}' class='TextBoxBig'><option value='1'>Supplied</option><option value='2'>Internal</option><option value='3'>Return</option></select>", "txtRejectType-" + i)
                    , String.Format("<textarea id='{0}' name='{0}' rows='2'></textarea>","txtExactReason-"+i));
            }

            //foreach (DataRow itemOrder in _validarOrden.Rows)
            //{
            //    table += String.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td><td>{6}</td><td>{7}</td><td>{8}</td></tr>"
            //        , itemOrder["SQNB"].ToString().Trim().ToUpper()
            //        , itemOrder["MITM"].ToString().Trim().ToUpper()
            //        , itemOrder["DSCA"].ToString().Trim().ToUpper()
            //        , itemOrder["QTDL"].ToString().Trim().ToUpper()
            //        , itemOrder["CUNI"].ToString().Trim().ToUpper()
            //        ,"<input type='text' id='txtShift' name='txtShift' class='TextBoxBig' onchange='validarShift(this);' />"
            //        , selectReasons
            //        ,"<select id='txtRejectType' name='txtRejectType' class='TextBoxBig'><option value='1'>Supplied</option><option value='2'>Internal</option><option value='3'>Return</option></select>"
            //        , "<textarea id='txtExactReason' name='txtExactReason' rows='2'></textarea>");
            //}

            table += "</table>";

            divTable.InnerHtml = table;
        }

        protected void MakeLabel(Ent_tticol100 Objtticol100)
        {
            var rstp = "10";
            listaReasons = _idalttcmcs005.findRecords(ref rstp, ref strError);
            IList<Reason> lstReasons = new List<Reason>();
            foreach (DataRow item in listaReasons.Rows)
            {
                Reason reason = new Reason
                {
                    cdis = item["CDIS"].ToString(),
                    dsca = item["DSCA"].ToString()
                };
                lstReasons.Add(reason);
            }

            var ObjReason = lstReasons.Where(x => x.cdis.Trim().ToUpper() == Objtticol100.cdis.Trim().ToUpper()).Single();

            var sqnb = _validarOrden.Rows[0]["SQNB"].ToString().Trim().ToUpper();
                var mitm = _validarOrden.Rows[0]["MITM"].ToString().Trim().ToUpper();
                var dsca = _validarOrden.Rows[0]["DSCA"].ToString().Trim().ToUpper();
                var qtyr = _validarOrden.Rows[0]["QTDL"].ToString().Trim().ToUpper();
                var pdno = _validarOrden.Rows[0]["PDNO"].ToString().Trim().ToUpper();
            //T$SQNB
            lblDmtNumber.Text = "DMT-NUMBER";
            lblDescription.Text = "Description";
            lblRejected.Text = "Rejected Qty";
            Label1.Text = "WorkOrder";
            lblTitleMachine.Text = "Machine";
            Label2.Text = "Disposition - Review";
            lblPrintedBy.Text = "Printed By";
            lblReason.Text = "Reason";
            lblFecha.Text = "Date:";
            lblValueReason.Text = "Adhesion";
            lblComments.Text = "Comments";
            
            lblDescMachine.Text = Objtticol100.mcno;

            lblValueComments.Text = Objtticol100.obse;
            lblValuePrintedBy.Text = Objtticol100.logr;
            lblValueReason.Text = ObjReason.dsca;
            LblSqnb.Text = sqnb;
            var rutaSqnb = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + sqnb.ToUpper() + "&code=Code128&dpi=96";
            imgCBSqnb.Src = !string.IsNullOrEmpty(sqnb) ? rutaSqnb : String.Empty;
            //T$MITM
            lblDsca.Text = mitm;
            var rutaMitm = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + mitm.ToUpper() + "&code=Code128&dpi=96";
            imgCBMitm.Src = !string.IsNullOrEmpty(mitm) ? rutaMitm : String.Empty;
            //T$DSCA
            lblDsca.Text = dsca;
            //T$QTDL
            lblValueQuantity.Text = Convert.ToString(qtyr);
            lblQtdl.Text = Objtticol100.Unit;
            var rutaQtdl = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + Convert.ToString(qtyr).ToUpper() + "&code=Code128&dpi=96";
            imgBCQtdl.Src = !string.IsNullOrEmpty(Convert.ToString(qtyr)) ? rutaQtdl : String.Empty;
            //T$PDNO
            lblPdno.Text = pdno;
            var rutaPdno = UrlBaseBarcode + "/Barcode/BarcodeHandler.ashx?data=" + pdno.ToUpper() + "&code=Code128&dpi=96";
            imgBCPdno.Src = !string.IsNullOrEmpty(pdno) ? rutaPdno : String.Empty;
        }

        protected void CargarIdioma()
        {
            lblWorkOrder.Text = _textoLabels.readStatement(formName, _idioma, "lblWorkOrder");
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
        public class Reason
        {
            public string cdis { get; set; }
            public string dsca { get; set; }
        };


        #endregion
    }
}