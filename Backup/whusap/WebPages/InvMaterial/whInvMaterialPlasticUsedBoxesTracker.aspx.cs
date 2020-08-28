using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using whusa.Interfases;
using whusa.Entidades;
using System.Data;
using System.Web.Services;

namespace whusap.WebPages.InvMaterial
{
    public partial class whInvMaterialPlasticUsedBoxesTracker : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Form.DefaultButton = this.btnContinue.UniqueID;

            if (!IsPostBack)
            {
                this.txtMachine.Attributes.Add("onchange", "validarMaquina(this)");
                this.txtIdentifier.Attributes.Add("onchange", "validarIdentifier(this)");

                this.txtMachine.Attributes.Add("onkeypress", "_toUpper(this)");
                this.txtIdentifier.Attributes.Add("onkeypress", "_toUpper(this)");
            }
        }

        protected void btnContinue_Click(object sender, EventArgs e)
        {
            InterfazDAL_tticol132 tticol132 = new InterfazDAL_tticol132();
            Ent_tticol132 ent_tticol132 = new Ent_tticol132();
            string strError = string.Empty;
            int retorno;
            List<Ent_tticol132> param = new List<Ent_tticol132>();
            DataTable DTBox;
            try
            {
                if (String.IsNullOrEmpty(txtIdentifier.Text))
                {
                    strError = "Box Identifier doesn't exist";
                    return;
                }

                ent_tticol132.barcode = txtIdentifier.Text;
                DTBox = tticol132.ObtenerBox(ref ent_tticol132, ref strError);
                if (DTBox.Rows.Count > 0)
                {
                    ent_tticol132.item = string.Format("         {0}", DTBox.Rows[0]["t$item"].ToString().Trim());
                    ent_tticol132.type = int.Parse(DTBox.Rows[0]["t$type"].ToString());
                    ent_tticol132.user = Session["user"].ToString();
                    //Modificado por petición de DMontoya
                    //Al digitar un registro de uso el registro que se está guardando en la tabla el registro con:
                    //status “Nuevo” y este campo debe quedar con valor “En Uso” (2) Pendiente
                    //ent_tticol132.status = int.Parse(DTBox.Rows[0]["t$stat"].ToString());
                    ent_tticol132.status = 2;
                    ent_tticol132.machine = txtMachine.Text.Trim().ToUpperInvariant();
                    ent_tticol132.refcntd = 0;
                    ent_tticol132.refcntu = 0;

                    param.Add(ent_tticol132);
                }

                retorno = tticol132.insertarRegistro(ref param, ref strError);

                if (retorno == 0 && !string.IsNullOrEmpty(strError))
                    strError = "Save unsuccessfully (" + strError + ")";
                else
                {
                    strError = "Save successfully";
                    txtIdentifier.Text = "";
                    //txtMachine.Text = "";
                }
            }
            catch (Exception ex)
            {
                strError = ex.InnerException != null ?
                                ex.Message + " (" + ex.InnerException + ")" :
                                ex.Message;
            }
            lblMensaje.Text = strError;
        }

        [WebMethod]
        public static string ValidarMaquina(string sMachine)
        {
            string retorno = "0";
            DataTable DTRetorno = null;
            string strError = string.Empty;
            InterfazDAL_tticol132 tticol132 = new InterfazDAL_tticol132();
            if (string.IsNullOrEmpty(sMachine))
                retorno = "0";
            else
            {
                try
                {
                    DTRetorno = tticol132.ValidarMaquina(sMachine, ref strError);
                    if (DTRetorno.Rows.Count > 0)
                        retorno = string.Empty;
                }
                catch (Exception ex)
                {
                    retorno = ex.InnerException != null ?
                                ex.Message + " (" + ex.InnerException + ")" :
                                ex.Message;
                }
            }
            return retorno;
        }

        [WebMethod]
        public static string ValidarIdentifier(string sIdentifier)
        {
            string retorno = "0";
            DataTable DTRetorno = null;
            string strError = string.Empty;
            InterfazDAL_tticol132 tticol132 = new InterfazDAL_tticol132();
            Ent_tticol132 ent_tticol132 = new Ent_tticol132();
            if (string.IsNullOrEmpty(sIdentifier))
                retorno = "0";
            else
            {
                Ent_tticol132 param = new Ent_tticol132() { barcode = sIdentifier };
                try
                {
                    DTRetorno = tticol132.ObtenerBox(ref param, ref strError);
                    if (DTRetorno.Rows.Count > 0)
                        retorno = string.Empty;
                }
                catch (Exception ex)
                {
                    retorno = ex.InnerException != null ?
                                ex.Message + " (" + ex.InnerException + ")" :
                                ex.Message;
                }
            }
            return retorno;
            
        }
    
    }
}