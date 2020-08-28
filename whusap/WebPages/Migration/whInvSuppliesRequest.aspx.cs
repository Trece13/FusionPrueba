using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using whusa.Interfases;
using whusa.Utilidades;
using System.Threading;
using System.Configuration;
using whusa.Entidades;
using System.Globalization;
using System.Data;

namespace whusap.WebPages.Migration
{
    public partial class whInvSuppliesRequest : System.Web.UI.Page
    {
        #region Propiedades
        private static InterfazDAL_ttccol301 _idalttccol301 = new InterfazDAL_ttccol301();
        private static InterfazDAL_tticol132 _idaltticol132 = new InterfazDAL_tticol132();
        private static InterfazDAL_ttcibd001 _idalttcibd001 = new InterfazDAL_ttcibd001();
        private static InterfazDAL_tticol095 _idaltticol095 = new InterfazDAL_tticol095();
        private static Mensajes _mensajesForm = new Mensajes();
        private static LabelsText _textoLabels = new LabelsText();
        private static string _operator;
        public static string _idioma;
        private static string strError;
        private static string formName;
        private static string globalMessages = "GlobalMessages";
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
            slItem1.Items.Clear();
            slItem2.Items.Clear();
            slItem3.Items.Clear();
            slItem4.Items.Clear();
            slItem5.Items.Clear();

            lblError.Text = String.Empty;
            var machine = txtMachine.Text.Trim().ToUpper();

            if (machine == String.Empty)
            {
                lblError.Text = mensajes("formempty");
                return;
            }

            //Consulta machine
            var consultaMaquina = _idaltticol132.ValidarMaquina(machine, ref strError);

            if (consultaMaquina.Rows.Count > 0)
            {
                //consulta items
                var consultaItem = _idalttcibd001.findRecordsSupplies(ref strError);

                if (consultaItem.Rows.Count > 0)
                {
                    List<ListItem> _listaRegistros = new List<ListItem>();

                    ListItem itemSelect = new ListItem();
                    itemSelect.Text = _idioma == "INGLES" ? "--Select an option--" : "--Seleccione--";
                    itemSelect.Value = "";

                    _listaRegistros.Add(itemSelect);

                    foreach (DataRow item in consultaItem.Rows)
                    {
                        ListItem row = new ListItem();
                        row.Text = String.Concat(item["ITEM"].ToString().Trim().ToUpper(), " - "
                                                , item["DSCA"].ToString().Trim().ToUpper(), " -. "
                                                , item["CWAR"].ToString().Trim().ToUpper(), " - "
                                                , item["CUNI"].ToString().Trim().ToUpper());
                        row.Value = item["ITEM"].ToString().Trim().ToUpper();

                        _listaRegistros.Add(row);
                    }

                    for (int i = 0; i < _listaRegistros.Count; i++)
                    {

                        slItem1.Items.Insert(i, _listaRegistros[i]);
                        slItem2.Items.Insert(i, _listaRegistros[i]);
                        slItem3.Items.Insert(i, _listaRegistros[i]);
                        slItem4.Items.Insert(i, _listaRegistros[i]);
                        slItem5.Items.Insert(i, _listaRegistros[i]);
                    }

                    txtMachine1.Text = txtMachine.Text.Trim().ToUpper();
                    txtMachine2.Text = txtMachine.Text.Trim().ToUpper();
                    txtMachine3.Text = txtMachine.Text.Trim().ToUpper();
                    txtMachine4.Text = txtMachine.Text.Trim().ToUpper();
                    txtMachine5.Text = txtMachine.Text.Trim().ToUpper();

                    txtQuantity1.Text = "0";
                    txtQuantity2.Text = "0";
                    txtQuantity3.Text = "0";
                    txtQuantity4.Text = "0";
                    txtQuantity5.Text = "0";

                    lblValueMachine.Text = txtMachine.Text.Trim().ToUpper();

                    divTable.Visible = true;
                }
                else
                {
                    divTable.Visible = false;
                    lblError.Text = mensajes("queryblank");
                    return;
                }
            }
            else
            {
                lblError.Text = mensajes("machinenotexists");
                return;
            }
        }

        protected void btnSaveOrder_Click(object sender, EventArgs e)
        {
            var contadorGuardados = guardarRegistros();

            if (contadorGuardados > 0)
            {
                divTable.Visible = false;
                lblError.Text = String.Empty;
                lblConfirm.Text = mensajes("msjsave");
                return;
            }
        }

        #endregion

        #region Metodos

        protected int guardarRegistros()
        {
            List<Ent_tticol095> listaDatos = new List<Ent_tticol095>();

            var mcno = txtMachine.Text.Trim().ToUpper();

            if (slItem1.SelectedValue.Trim() != "")
            {
                var item = slItem1.SelectedItem.Text.Split('-')[0] + "-" + slItem1.SelectedItem.Text.Split('-')[1];
                var cwar = slItem1.SelectedItem.Text.Split(new string[] { "-." }, StringSplitOptions.None).Last().Split('-')[0];
                var unit = slItem1.SelectedItem.Text.Split(new string[] { "-." }, StringSplitOptions.None).Last().Split('-').Last();

                var seqn = _idaltticol095.consultaSeqn(ref mcno, ref cwar, ref item, ref strError);
                seqn = seqn + 1;

                Ent_tticol095 data1 = new Ent_tticol095();
                data1.mcno = mcno;
                data1.seqn = seqn;
                data1.item = item.Trim();
                data1.cwar = cwar.Trim() == String.Empty ? " " : cwar.Trim();
                data1.unit = unit.Trim();
                data1.qana = double.Parse(txtQuantity1.Text.Trim(), CultureInfo.InvariantCulture.NumberFormat);
                data1.logn = _operator;
                data1.proc = 2;

                listaDatos.Add(data1);
            }

            if (slItem2.SelectedValue.Trim() != "")
            {
                var item = slItem2.SelectedItem.Text.Split('-')[0] + "-" + slItem2.SelectedItem.Text.Split('-')[1];
                var cwar = slItem2.SelectedItem.Text.Split(new string[] { "-." }, StringSplitOptions.None).Last().Split('-')[0];
                var unit = slItem2.SelectedItem.Text.Split(new string[] { "-." }, StringSplitOptions.None).Last().Split('-').Last();

                var seqn = _idaltticol095.consultaSeqn(ref mcno, ref cwar, ref item, ref strError);
                seqn = seqn + 1;

                Ent_tticol095 data2 = new Ent_tticol095();
                data2.mcno = mcno;
                data2.seqn = seqn;
                data2.item = item.Trim();
                data2.cwar = cwar.Trim() == String.Empty ? " " : cwar.Trim();
                data2.unit = unit.Trim();
                data2.qana = double.Parse(txtQuantity2.Text.Trim(), CultureInfo.InvariantCulture.NumberFormat);
                data2.logn = _operator;
                data2.proc = 2;

                listaDatos.Add(data2);
            }

            if (slItem3.SelectedValue.Trim() != "")
            {
                var item = slItem3.SelectedItem.Text.Split('-')[0] + "-" + slItem3.SelectedItem.Text.Split('-')[1];
                var cwar = slItem3.SelectedItem.Text.Split(new string[] { "-." }, StringSplitOptions.None).Last().Split('-')[0];
                var unit = slItem3.SelectedItem.Text.Split(new string[] { "-." }, StringSplitOptions.None).Last().Split('-').Last();

                var seqn = _idaltticol095.consultaSeqn(ref mcno, ref cwar, ref item, ref strError);
                seqn = seqn + 1;

                Ent_tticol095 data3 = new Ent_tticol095();
                data3.mcno = mcno;
                data3.seqn = seqn;
                data3.item = item.Trim();
                data3.cwar = cwar.Trim() == String.Empty ? " " : cwar.Trim();
                data3.unit = unit.Trim();
                data3.qana = double.Parse(txtQuantity3.Text.Trim(), CultureInfo.InvariantCulture.NumberFormat);
                data3.logn = _operator;
                data3.proc = 2;

                listaDatos.Add(data3);

            }

            if (slItem4.SelectedValue.Trim() != "")
            {
                var item = slItem4.SelectedItem.Text.Split('-')[0] + "-" + slItem4.SelectedItem.Text.Split('-')[1];
                var cwar = slItem4.SelectedItem.Text.Split(new string[] { "-." }, StringSplitOptions.None).Last().Split('-')[0];
                var unit = slItem4.SelectedItem.Text.Split(new string[] { "-." }, StringSplitOptions.None).Last().Split('-').Last();

                var seqn = _idaltticol095.consultaSeqn(ref mcno, ref cwar, ref item, ref strError);
                seqn = seqn + 1;

                Ent_tticol095 data4 = new Ent_tticol095();
                data4.mcno = mcno;
                data4.seqn = seqn;
                data4.item = item.Trim();
                data4.cwar = cwar.Trim() == String.Empty ? " " : cwar.Trim();
                data4.unit = unit.Trim();
                data4.qana = double.Parse(txtQuantity4.Text.Trim(), CultureInfo.InvariantCulture.NumberFormat);
                data4.logn = _operator;
                data4.proc = 2;

                listaDatos.Add(data4);
            }

            if (slItem5.SelectedValue.Trim() != "")
            {
                var item = slItem5.SelectedItem.Text.Split('-')[0] + "-" + slItem5.SelectedItem.Text.Split('-')[1];
                var cwar = slItem5.SelectedItem.Text.Split(new string[] { "-." }, StringSplitOptions.None).Last().Split('-')[0];
                var unit = slItem5.SelectedItem.Text.Split(new string[] { "-." }, StringSplitOptions.None).Last().Split('-').Last();

                var seqn = _idaltticol095.consultaSeqn(ref mcno, ref cwar, ref item, ref strError);
                seqn = seqn + 1;

                Ent_tticol095 data5 = new Ent_tticol095();
                data5.mcno = mcno;
                data5.seqn = seqn;
                data5.item = item.Trim();
                data5.cwar = cwar.Trim() == String.Empty ? " " : cwar.Trim();
                data5.unit = unit.Trim();
                data5.qana = double.Parse(txtQuantity5.Text.Trim(), CultureInfo.InvariantCulture.NumberFormat);
                data5.logn = _operator;
                data5.proc = 2;
                listaDatos.Add(data5);

            }


            foreach (var item in listaDatos)
            {
                var registro = item;
                var validateSave = _idaltticol095.insertarRegistro(ref registro, ref strError);

                if (validateSave < 1)
                {
                    lblError.Text = String.Format(mensajes("errorsave"), registro.item);
                    return 0;
                }
            }

            return listaDatos.Count;
        }

        protected void CargarIdioma()
        {
            lblMachine.Text = _textoLabels.readStatement(formName, _idioma, "lblMachine");
            btnConsultar.Text = _textoLabels.readStatement(formName, _idioma, "btnConsultar");
            lblMachineTable.Text = _textoLabels.readStatement(formName, _idioma, "lblMachine");
            lblSequence.Text = _textoLabels.readStatement(formName, _idioma, "lblSequence");
            lblItem.Text = _textoLabels.readStatement(formName, _idioma, "lblItem");
            lblQuantity.Text = _textoLabels.readStatement(formName, _idioma, "lblQuantity");
            lblMachine1.Text = _textoLabels.readStatement(formName, _idioma, "lblMachine");
            lblValueSelect.Text = _textoLabels.readStatement(formName, _idioma, "lblValueSelect");
            btnSaveOrder.Text = _textoLabels.readStatement(formName, _idioma, "btnSaveOrder");
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