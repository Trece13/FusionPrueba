using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using whusa.Interfases;
using whusap.Entidades;
using System.Data;
using whusa.Entidades;

namespace whusap.WebPages.InvMaterial
{
    public partial class whInvMaterialPlasticReturnBoxesTracker : System.Web.UI.Page
    {
        InterfazDAL_ttdcol524 ttdcol524 = new InterfazDAL_ttdcol524();

        protected void Page_Load(object sender, EventArgs e)
        {
            lblMensaje.ForeColor = System.Drawing.Color.Black;
            if (!IsPostBack)
            {
                InicializarControles();
            }
        }

        protected void btnContinue_Click(object sender, EventArgs e)
        {
            string strError = string.Empty;
            Ent_ttdcol524 ParametrosIn = new Ent_ttdcol524();
            List<Ent_ttdcol524> parametros = new List<Ent_ttdcol524>();
            InterfazDAL_tticol132 tticol132 = new InterfazDAL_tticol132();
            List<Ent_tticol132> param = new List<Ent_tticol132>();
            DataTable DSReturnBox = new DataTable();
            try
            {
                if (ValidarDatos(ref strError))
                {
                    string item = string.Empty;
                    string quantity = string.Empty;

                    for (int i = 0; i < 5; i++)
                    {
                        string barcode = string.Empty;
                        int type = 0;
                        switch (i)
                        {
                            case 0: 
                                item = txtCover.Text.Trim().ToUpperInvariant();
                                quantity = txtQCover.Text;
                                barcode = txtCoverBarCode.Text.Trim().ToUpperInvariant();
                                type = 3;
                                break;
                            case 1:
                                item = txtMaster1.Text.Trim().ToUpperInvariant();
                                quantity = txtQMaster1.Text;
                                barcode = txtMasterBarCode1.Text.Trim().ToUpperInvariant();
                                type = 2;
                                break;
                            case 2:
                                item = txtMaster2.Text.Trim().ToUpperInvariant();
                                quantity = txtQMaster2.Text;
                                barcode = txtMasterBarCode2.Text.Trim().ToUpperInvariant();
                                type = 2;
                                break;
                            case 3:
                                item = txtMaster3.Text.Trim().ToUpperInvariant();
                                quantity = txtQMaster3.Text;
                                barcode = txtMasterBarCode3.Text.Trim().ToUpperInvariant();
                                type = 2;
                                break;
                            case 4:
                                item = txtBox.Text.Trim().ToUpperInvariant();
                                quantity = txtQBox.Text;
                                barcode = string.Empty;
                                type = 1;
                                break;
                        }

                        ParametrosIn.item = item;
                        ParametrosIn.adjustmentQuantity = quantity;

                        long lOrderNumber;
                        //Obtener datos
                        DSReturnBox = ttdcol524.GetData(ref ParametrosIn, ref strError);

                        if (!string.IsNullOrEmpty(barcode))
                        {
                            param.Add(new Ent_tticol132()
                            {
                                barcode = barcode,
                                date = DateTime.Now,
                                type = type,
                                item = string.Format("         {0}", item.Trim().ToUpperInvariant()),
                                user = Session["user"].ToString(),
                                status = 2,
                                machine = "  ",
                                refcntd = 0,
                                refcntu = 0
                            });
                        }

                        if (DSReturnBox.Rows.Count > 0)
                        {
                            if (DSReturnBox.Rows.Count == 1)
                            {
                                if (long.TryParse(DSReturnBox.Rows[0]["orderNumber"].ToString(), out lOrderNumber))
                                {
                                    Ent_ttdcol524 entidad = new Ent_ttdcol524();
                                    entidad.orderNumber = (lOrderNumber + i).ToString().PadLeft(9, '0');
                                    entidad.employee = DSReturnBox.Rows[0]["Employee"].ToString();
                                    entidad.warehouse = DSReturnBox.Rows[0]["Warehouse"].ToString();
                                    entidad.item = DSReturnBox.Rows[0]["Item"].ToString();
                                    entidad.adjustmentQuantity = quantity;
                                    entidad.unit = DSReturnBox.Rows[0]["Unit"].ToString();
                                    entidad.adjustmentReason = DSReturnBox.Rows[0]["AdjustmentReason"].ToString();
                                    entidad.status = DSReturnBox.Rows[0]["Status"].ToString();
                                    entidad.adjustmentBaanOrderLine = DSReturnBox.Rows[0]["AdjustmentBaanOrderLine"].ToString();
                                    entidad.adjustmentBaanOrder = DSReturnBox.Rows[0]["AdjustmentBaanOrder"].ToString();
                                    entidad.refcntd = 0;
                                    entidad.refcntu = 0;

                                    parametros.Add(entidad);
                                }
                            }
                        }
                    }
                    //Guardar en tdcol524
                    if (parametros.Count > 0 && param.Count > 0)
                    {
                        if (ttdcol524.insertarDatos(ref parametros, ref strError) == 1)
                        {
                            if (tticol132.insertarRegistro(ref param, ref strError) == 1)
                            {
                                if (ttdcol524.actualizarContadores(ref strError) < 1)
                                    strError = "Save successfully but the counters were not updated";
                                else
                                    strError = "Save successfully";

                                LimpiarControles();
                            }
                            else
                                strError = "An error occurred while saving the information";
                        }
                    }
                    else
                    {
                        strError = "Save unsuccessfully";
                    }
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

        /// <summary>
        /// Método que valida si algún dato ingresado es inválido o faltan por ingresar 
        /// </summary>
        /// <param name="strError"></param>
        /// <returns>true: si los datos están correctos, false: muestra mensaje de error</returns>
        protected bool ValidarDatos(ref string strError)
        {
            bool siRetorno = false;
            try
            {
                if (string.IsNullOrEmpty(txtCoverBarCode.Text))   strError = "Cover - Barcode doesn't exist";
                if (string.IsNullOrEmpty(txtMasterBarCode1.Text)) strError = "Master 1 - Barcode doesn't exist";
                if (string.IsNullOrEmpty(txtMasterBarCode2.Text)) strError = "Master 2 - Barcode doesn't exist";
                if (string.IsNullOrEmpty(txtMasterBarCode3.Text)) strError = "Master 3 - Barcode doesn't exist";


                if (string.IsNullOrEmpty(txtCover.Text)) strError = "Cover - Item isn't valid";
                if (string.IsNullOrEmpty(txtMaster1.Text)) strError = "Master 1 - Item isn't valid";
                if (string.IsNullOrEmpty(txtMaster2.Text)) strError = "Master 2 - Item isn't valid";
                if (string.IsNullOrEmpty(txtMaster3.Text)) strError = "Master 3 - Item isn't valid";
                if (string.IsNullOrEmpty(txtBox.Text)) strError = "Box - Item doesn't exist";

                int value;
                if (string.IsNullOrEmpty(txtQCover.Text) && int.TryParse(txtQCover.Text, out value)) strError = "Invalid number of cover";
                if (string.IsNullOrEmpty(txtQMaster1.Text) && int.TryParse(txtQMaster1.Text, out value)) strError = "Invalid number of Master 1";
                if (string.IsNullOrEmpty(txtQMaster2.Text) && int.TryParse(txtQMaster2.Text, out value)) strError = "Invalid number of Master 2";
                if (string.IsNullOrEmpty(txtQMaster3.Text) && int.TryParse(txtQMaster3.Text, out value)) strError = "Invalid number of Master 3";
                if (string.IsNullOrEmpty(txtQBox.Text) && int.TryParse(txtQBox.Text, out value)) strError = "Invalid number of Boxes";

                if (txtCover.Text.Contains("doesn't exist")) strError = "Invalid cover value";
                if (txtMaster1.Text.Contains("doesn't exist")) strError = "Invalid Master 1 value";
                if (txtMaster2.Text.Contains("doesn't exist")) strError = "Invalid Master 2 value";
                if (txtMaster3.Text.Contains("doesn't exist")) strError = "Invalid Master 3 value";
                if (txtBox.Text.Contains("doesn't exist")) strError = "Invalid Box value";

                if (string.IsNullOrEmpty(strError))
                    siRetorno = true;

            }
            catch (Exception ex)
            {
                strError = ex.InnerException != null ?
                           ex.Message + " (" + ex.InnerException + ")" :
                           ex.Message;
            }
            return siRetorno;
        }

        /// <summary>
        /// Método que limpia todos los controles excepto los mensajes
        /// </summary>
        protected void LimpiarControles()
        {
            txtCoverBarCode.Text = string.Empty;
            txtMasterBarCode1.Text = string.Empty;
            txtMasterBarCode2.Text = string.Empty;
            txtMasterBarCode3.Text = string.Empty;
            txtBoxBarCode.Text = string.Empty;

            txtCover.Text = string.Empty;
            txtMaster1.Text = string.Empty;
            txtMaster2.Text = string.Empty;
            txtMaster3.Text = string.Empty;
            txtBox.Text = string.Empty;

            lblCover.Text = string.Empty;
            lblMaster1.Text = string.Empty;
            lblMaster2.Text = string.Empty;
            lblMaster3.Text = string.Empty;
            lblBox.Text = string.Empty;

            txtQCover.Text = "1";
            txtQMaster1.Text = "1";
            txtQMaster2.Text = "1";
            txtQMaster3.Text = "1";
            txtQBox.Text = "72";
        }

        protected void InicializarControles()
        {

            //txtCoverBarCode.Attributes.Add("readonly", "readonly");
            //txtMasterBarCode1.Attributes.Add("readonly", "readonly");
            //txtMasterBarCode2.Attributes.Add("readonly", "readonly");
            //txtMasterBarCode3.Attributes.Add("readonly", "readonly");
            txtBoxBarCode.Attributes.Add("readonly", "readonly");

            this.txtCoverBarCode.Attributes.Add("onkeypress", "_toUpper(this)");
            this.txtMasterBarCode1.Attributes.Add("onkeypress", "_toUpper(this)");
            this.txtMasterBarCode2.Attributes.Add("onkeypress", "_toUpper(this)");
            this.txtMasterBarCode3.Attributes.Add("onkeypress", "_toUpper(this)");
            this.txtCover.Attributes.Add("onkeypress", "_toUpper(this)");
            this.txtBox.Attributes.Add("onkeypress", "_toUpper(this)");

            txtCover.Attributes.Add("readonly", "readonly");
            txtMaster1.Attributes.Add("readonly", "readonly");
            txtMaster2.Attributes.Add("readonly", "readonly");
            txtMaster3.Attributes.Add("readonly", "readonly");
            //txtBox.Attributes.Add("readonly", "readonly");

            txtQCover.Attributes.Add("readonly", "readonly");
            txtQMaster1.Attributes.Add("readonly", "readonly");
            txtQMaster2.Attributes.Add("readonly", "readonly");
            txtQMaster3.Attributes.Add("readonly", "readonly");
            //txtQBox.Attributes.Add("readonly", "readonly");


            txtCoverBarCode.Attributes.Add("onchange", "validarItemByBarCode(this, 3, 0);");
            txtMasterBarCode1.Attributes.Add("onchange", "validarItemByBarCode(this, 2, 1);");
            txtMasterBarCode2.Attributes.Add("onchange", "validarItemByBarCode(this, 2, 2);");
            txtMasterBarCode3.Attributes.Add("onchange", "validarItemByBarCode(this, 2, 3);");

            //Event check only numbers
            txtQBox.Attributes.Add("onkeypress", "return checkNumber(event);");
            txtBox.Attributes.Add("onchange", "validarItem(this, 'txtBox', 'lblDescBox', 'Box');");

        }
    }

}