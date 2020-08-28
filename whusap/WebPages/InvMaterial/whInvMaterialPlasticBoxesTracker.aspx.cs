using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using whusa.Interfases;
using System.Data;
using System.Web.Script.Serialization;
using whusa.Entidades;
using whusa.Utilidades;

namespace whusap.WebPages.InvMaterial
{
    public partial class whInvMaterialPlasticBoxesTracker : System.Web.UI.Page
    {
        #region Propiedades

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
                this.Form.DefaultButton = this.btnContinue.UniqueID;
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

                    txtItem.Attributes.Add("onchange", "validarItem(this);");

                    //btnContinue.Attributes.Add("onclick", "guardar(this);");
                }
            }

        protected void btnContinue_Click(object sender, EventArgs e)
            {//
                InterfazDAL_tticol132 tticol132 = new InterfazDAL_tticol132();
                string strError = string.Empty;
                int retorno;
                List<Ent_tticol132> param = new List<Ent_tticol132>();
                try
                {
                    if (validarDatos(ref strError))
                    {
                        param.Add(new Ent_tticol132()
                        {
                            barcode = txtBarCodeID.Text.Trim().ToUpperInvariant(),
                            date = DateTime.Now,
                            type = ddlType.SelectedIndex,
                            item = string.Format("         {0}", txtItem.Text.Trim().ToUpperInvariant()),
                            user = Session["user"].ToString(),
                            status = 1,
                            machine = "  ",
                            refcntd = 0,
                            refcntu = 0
                        });

                        retorno = tticol132.insertarRegistro(ref param, ref strError);

                        if (retorno == 0 && string.IsNullOrEmpty(strError))
                            strError = mensajes("errorsave");
                        else
                        {
                            txtBarCodeID.Text = "";
                            strError = mensajes("msjsave");
                        }
                    }
                }
                catch (Exception ex)
                {
                    strError = ex.InnerException != null ?
                                    ex.Message + " (" + ex.InnerException + ")" :
                                    ex.Message;
                }
                lblMessage.Text = strError;
            }

        #endregion

        #region Metodos

        [System.Web.Services.WebMethod()]
        public static string GetItems(string sItem)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            InterfazDAL_tticol130 idal = new InterfazDAL_tticol130();
            string retorno = string.Empty;
            string strError = string.Empty;
            DataTable dtItems = null;
            sItem = sItem.ToString().Replace(" ", "").Replace("\"", "");

            try
            {
                dtItems = idal.listarItems(sItem, ref strError);
                if (dtItems.Rows.Count == 1)
                    retorno = dtItems.Rows[0]["descripcion"].ToString();
                else if (dtItems.Rows.Count > 1)
                    retorno = "Search returned more than one item";
                else
                    retorno = "Item doesn't exist";

                return retorno;
            }
            catch (Exception ex)
            {
                strError = ex.InnerException != null ?
                                ex.Message + " (" + ex.InnerException + ")" :
                                ex.Message;
            }
            return string.Empty;
        }

        protected bool validarDatos(ref string strError)
        {
            bool siValido = false;
            if (string.IsNullOrEmpty(txtItem.Text))
            {
                strError += _idioma == "INGLES" ? "- Item is not valid\n" : "- Articulo no valido\n";
            }

            if (string.IsNullOrEmpty(txtBarCodeID.Text))
            {
                strError += _idioma == "INGLES" ? "- Barcode is not valid\n" : "- Codigo de barras no valido\n";
            }

            if (string.IsNullOrEmpty(ddlType.SelectedIndex.ToString()))
            {
                strError += _idioma == "INGLES" ? "- Type is not valid\n" : "- Tipo no valido\n";
            }

            if (string.IsNullOrEmpty(strError))
                siValido = true;
            else
                siValido = false;

            return siValido;
        }

        protected void CargarIdioma()
        {
            lblItem.Text = _textoLabels.readStatement(formName, _idioma, "lblItem");
            lblType.Text = _textoLabels.readStatement(formName, _idioma, "lblType");
            lblBarCodeID.Text = _textoLabels.readStatement(formName, _idioma, "lblBarCodeID");
            btnContinue.Text = _textoLabels.readStatement(formName, _idioma, "btnContinue");

            ListItem item0 = new ListItem() { Text = _idioma == "INGLES" ? "Select..." : "Seleccione...", Value = "0" };
            ListItem item1 = new ListItem() { Text = _idioma == "INGLES" ? "Box" : "Caja", Value = "1" };
            ListItem item2 = new ListItem() { Text = _idioma == "INGLES" ? "Master" : "Principal", Value = "2" };
            ListItem item3 = new ListItem() { Text = _idioma == "INGLES" ? "Cover" : "Tapa", Value = "3" };

            ddlType.Items.Insert(ddlType.Items.Count, item1);
            ddlType.Items.Insert(ddlType.Items.Count, item2);
            ddlType.Items.Insert(ddlType.Items.Count, item3);
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