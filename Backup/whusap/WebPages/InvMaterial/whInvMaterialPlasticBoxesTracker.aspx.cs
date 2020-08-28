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

namespace whusap.WebPages.InvMaterial
{
    public partial class whInvMaterialPlasticBoxesTracker : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Form.DefaultButton = this.btnContinue.UniqueID;
            if (!IsPostBack)
            {
                txtItem.Attributes.Add("onchange", "validarItem(this);");
                
                //btnContinue.Attributes.Add("onclick", "guardar(this);");
            }
        }

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

        //[System.Web.Services.WebMethod()]
        //public static string GuardarDatos(string sItem, string sType, string sBarCode, string sUser)
        //{
        //    InterfazDAL_tticol132 tticol132 = new InterfazDAL_tticol132();
        //    string strError = string.Empty;
        //    int retorno;
        //    List<Ent_tticol132> param = new List<Ent_tticol132>();
        //    try
        //    {
        //        param.Add(new Ent_tticol132() 
        //        { 
        //            barcode = sBarCode,
        //            date = DateTime.Now,
        //            type = int.Parse(sType),
        //            item = sItem,
        //            user = sUser,
        //            status = 1,
        //            machine = "  ",
        //            refcntd = 0,
        //            refcntu = 0
        //        });

        //        retorno = tticol132.insertarRegistro(ref param, ref strError);

        //        if (retorno == 0 && string.IsNullOrEmpty(strError))
        //        {
        //            strError = "Save unsuccessfully";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        strError = ex.InnerException != null ?
        //                        ex.Message + " (" + ex.InnerException + ")" :
        //                        ex.Message;
        //    }
        //    return strError;
        //}

        protected void btnContinue_Click(object sender, EventArgs e)
        {
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
                        strError = "Save unsuccessfully";
                    else
                    {
                        txtBarCodeID.Text = "";
                        //ddlType.SelectedIndex = 0;
                        //txtItem.Text = "";
                        strError = "Save successfully";
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

        protected bool validarDatos(ref string strError)
        {
            bool siValido = false;
            if (string.IsNullOrEmpty(txtItem.Text))
            {
                strError += "- Item is not valid\n";
            }
            
            if (string.IsNullOrEmpty(txtBarCodeID.Text))
            {
                strError += "- Barcode is not valid\n";
            }

            if (string.IsNullOrEmpty(ddlType.SelectedIndex.ToString()))
            {
                strError += "- Type is not valid\n";
            }

            if (string.IsNullOrEmpty(strError))
                siValido = true;
            else
                siValido = false;

            return siValido;
        }
    
    }
}