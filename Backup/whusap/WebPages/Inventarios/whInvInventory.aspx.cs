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
using System.Configuration;
using System.Globalization;
using System.Threading;
using whusa.Entidades;
using whusa.Interfases;

namespace whusap.WebPages.Inventarios
{
    public partial class whInvInventory : System.Web.UI.Page
    {
        string strError = string.Empty;
        string strclot = string.Empty;
        
        DataTable dtt_twhwmd300 = new DataTable();
        DataTable dtt_twhcol002 = new DataTable();
        DataTable dtt_ttcibd001 = new DataTable();
        DataTable dtt_twhltc100 = new DataTable();
        DataTable dtt_twhcol015 = new DataTable();

        protected static InterfazDAL_ttccol301 idal301 = new InterfazDAL_ttccol301();
        Ent_ttccol301 obj301 = new Ent_ttccol301();
        protected static InterfazDAL_twhwmd300 idal300 = new InterfazDAL_twhwmd300();
        Ent_twhwmd300 obj300 = new Ent_twhwmd300();
        protected static InterfazDAL_twhcol002 idal002 = new InterfazDAL_twhcol002();
        Ent_twhcol002 obj002 = new Ent_twhcol002();
        protected static InterfazDAL_ttcibd001 idal001 = new InterfazDAL_ttcibd001();
        Ent_ttcibd001 obj001 = new Ent_ttcibd001();
        protected static InterfazDAL_twhltc100 idal100 = new InterfazDAL_twhltc100();
        Ent_twhltc100 obj100 = new Ent_twhltc100();
        protected static InterfazDAL_twhcol015 idal015 = new InterfazDAL_twhcol015();
        Ent_twhcol015 obj015 = new Ent_twhcol015();

        protected void Page_Load(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-CO");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("es-CO");
            base.InitializeCulture();

            if (!IsPostBack)
            {
                lblError.Visible = false;
                lblResult.Visible = false;

                divWarehouse.Visible = false;
                divCantidad.Visible = false;
                divLote.Visible = false;
                //btnContinue.Enabled = false;
                btnSave.Visible = false;
                txtItem.Enabled = false;

                if (Session["IsPreviousPage"] == null) { Session.Clear(); }

                string strTitulo = "Inventory Raw Material";
                Label control = (Label)Page.Controls[0].FindControl("lblPageTitle");
                control.Text = strTitulo;
                //Page.Form.DefaultButton = btnContinue.UniqueID;
                Page.Form.DefaultButton = btnSave.UniqueID;

                if (lblIngreso.Text == "")
                {
                    lblIngreso.Text = "1";
                }
            }
        }

        protected void txtlocation_TextChanged(object sender, EventArgs e)
        {
            InsertarIngresoPagina();

            lblError.Text = "";
            lblResult.Text = "";
            lblError.Visible = false;
            lblResult.Visible = false;

            List<Ent_twhwmd300> parameterCollection300 = new List<Ent_twhwmd300>();
            List<Ent_twhcol002> parameterCollection002 = new List<Ent_twhcol002>();


            //BUSCAR ALMACEN DE LA UBICACION
            obj300.loca = txtlocation.Text.Trim().ToUpper();
            dtt_twhwmd300 = idal300.listaRegistro_ObtieneAlmacen(ref obj300, ref strError);

            if (dtt_twhwmd300.Rows.Count < 1)
            {
                lblError.Visible = true;
                lblError.Text = strError;
                txtItem.Text = string.Empty;
                txtItem.Enabled = false;
                txtLot.Text = string.Empty;
                divLote.Visible = false;

                return;
            }
            else
            {
                lblWarehouse.Text = dtt_twhwmd300.Rows[0]["ALM"].ToString();

                ////Buscar Secuencia del Conteo
                obj002.cwar = lblWarehouse.Text.Trim().ToUpper();
                dtt_twhcol002 = idal002.listaRegistro_ObtieneConteo(ref obj002, ref strError);

                if (dtt_twhcol002.Rows.Count < 1)
                {
                    lblError.Visible = true;
                    lblError.Text = strError;
                    return;
                }
                else
                {
                    lblstrconteo.Text = dtt_twhcol002.Rows[0]["CONT"].ToString();
                }

                if (lblstrconteo.Text == "0")
                {
                    lblError.Visible = true;
                    lblError.Text = "The count number must be greather than Zero";
                    return;
                }
            }

            // VALIDAR QUE EXISTA EL ALMACEN Y LA UBICACION
            obj300.cwar = lblWarehouse.Text.Trim().ToUpper();
            dtt_twhwmd300 = idal300.listaRegistro_ObtieneAlmacen(ref obj300, ref strError);
            if (dtt_twhwmd300.Rows.Count < 1)
            {
                lblError.Visible = true;
                lblError.Text = strError;
                return;
            }

            txtItem.Enabled = true;
            txtItem.Focus();
        }

        protected void txtItem_TextChanged(object sender, EventArgs e)
        {
            lblError.Text = "";
            lblResult.Text = "";
            lblError.Visible = false;
            lblResult.Visible = false;
            lblDescItem.Text = string.Empty;
            lblUnity.Text = string.Empty;


            //List<Ent_ttcibd001> parameterCollection001 = new List<Ent_ttcibd001>();
            //List<Ent_twhltc100> parameterCollection100 = new List<Ent_twhltc100>();

            obj001.item = txtItem.Text.Trim().ToUpper();
            dtt_ttcibd001 = idal001.listaRegistro_ObtieneClotUnitCOnv(ref obj001, ref strError);
            if (dtt_ttcibd001.Rows.Count < 1)
            {
                lblError.Visible = true;
                lblError.Text = strError;
                txtLot.Text = string.Empty;
                divLote.Visible = false;
                return;
            }
            else
            {
                lblstrclot.Text = dtt_ttcibd001.Rows[0]["CLOT"].ToString();
            }

            txtLot.Text = string.Empty;

            if (lblstrclot.Text == "1")
            {
                divLote.Visible = true;
                txtLot.Focus();
                //btnContinue.Enabled = false;
            }
            else
            {
                divLote.Visible = false;

                // ARTICULO QUE NO MANEJA LOTE
                obj001.item = txtItem.Text.Trim().ToUpper();
                dtt_ttcibd001 = idal001.listaRegistro_ObtieneDescUnidNOLote(ref obj001, ref strError);

                if (dtt_ttcibd001.Rows.Count < 1)
                {
                    lblError.Visible = true;
                    lblError.Text = strError;
                    return;
                }
                else
                {
                    lblDescItem.Text = dtt_ttcibd001.Rows[0]["DESCRIPCION"].ToString();
                    lblUnity.Text = dtt_ttcibd001.Rows[0]["UNID"].ToString();
                }

                Habilitar(true);
                txtCantidad.Focus();
                //btnContinue.Enabled = true;
            }
        }

        protected void txtLot_TextChanged(object sender, EventArgs e)
        {
            lblError.Text = "";
            lblResult.Text = "";
            lblError.Visible = false;
            lblResult.Visible = false;

            List<Ent_ttcibd001> parameterCollection001 = new List<Ent_ttcibd001>();
            List<Ent_twhltc100> parameterCollection100 = new List<Ent_twhltc100>();

            if (lblstrclot.Text == "1")
            {
                // VALIDAR QUE SE HAYA DIGITADO ALGO EN EL ITEM
                if (txtLot.Text.Trim().Length == 0)
                {
                    lblError.Visible = true;
                    lblError.Text = "lot Requiered for Item.";
                    return;
                }

                //VALIDAR EL LOTE
                strclot = lblstrclot.Text;
                strclot = strclot.Replace(",", "");
                lblstrclot.Text = strclot;

                obj100.clot = txtLot.Text.Trim().ToUpper();
                obj100.item = txtItem.Text.Trim().ToUpper();
                dtt_twhltc100 = idal100.listaRegistro_Clot(ref obj100, ref strError);
                if (dtt_twhltc100.Rows.Count < 1)
                {
                    lblError.Visible = true;
                    lblError.Text = strError;
                    return;
                }
                else
                {
                    obj001.item = txtItem.Text.Trim().ToUpper();
                    dtt_ttcibd001 = idal001.listaRegistro_ObtieneDescripcionUnidad(ref obj001, ref strError);
                    if (dtt_ttcibd001.Rows.Count < 1)
                    {
                        lblError.Visible = true;
                        lblError.Text = strError;
                        return;
                    }
                    else
                    {
                        lblDescItem.Text = dtt_ttcibd001.Rows[0]["DESCRIPCION"].ToString();
                        lblUnity.Text = dtt_ttcibd001.Rows[0]["UNID"].ToString();
                    }
                }
            }

            Habilitar(true);
            txtCantidad.Focus();
            //btnContinue.Enabled = true;
        }

        protected void btnContinue_Click(object sender, EventArgs e)
        {
            Habilitar(true);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            NumberStyles style = NumberStyles.AllowDecimalPoint;
            decimal cantidad = 0;
            bool valido = false;
            lblError.Text = "";
            lblError.Visible = false;

            List<Ent_twhcol015> parameterCollection015 = new List<Ent_twhcol015>();

            valido = decimal.TryParse(txtCantidad.Text.Trim(), style, System.Globalization.CultureInfo.InvariantCulture, out cantidad);


            if (!valido || cantidad < 0)
            {
                lblError.Text = "Quantity Enter doesn't be less zero, please check.";
                return;
            }

            if (lblstrclot.Text == "1")
            {
                obj015.cwar = lblWarehouse.Text.Trim().ToUpper();
                obj015.loca = txtlocation.Text.Trim().ToUpper();
                obj015.pdno = txtLot.Text.Trim().ToUpper();

                dtt_twhcol015 = idal015.listaRegistro_ObtieneSecuencia(ref obj015, ref strError);
                if (dtt_twhcol015.Rows.Count < 1)
                {
                    lblstrseq.Text = "1";
                }
                else
                {
                    lblstrseq.Text = Convert.ToString(Convert.ToInt32(dtt_twhcol015.Rows[0]["SEQ"].ToString()) + 1);
                }
            }
            else
            {
                obj015.cwar = lblWarehouse.Text.Trim().ToUpper();
                obj015.loca = txtlocation.Text.Trim().ToUpper();

                dtt_twhcol015 = idal015.listaRegistro_ObtieneSecuenciaNolote(ref obj015, ref strError);
                if (dtt_twhcol015.Rows.Count < 1)
                {
                    lblstrseq.Text = "1";
                }
                else
                {
                    lblstrseq.Text = Convert.ToString(Convert.ToInt32(dtt_twhcol015.Rows[0]["SEQ"].ToString()) + 1);
                }

                obj015.pdno = " ";
            }

            obj015.sqnb = Convert.ToInt32(lblstrseq.Text.ToUpper());
            obj015.mitm = "         " + txtItem.Text.Trim().ToUpper();
            obj015.dsca = lblDescItem.Text.Trim().ToUpper();
            obj015.qtdl = cantidad;
            obj015.cuni = lblUnity.Text.Trim().ToUpper();
            obj015.mess = " ";
            obj015.proc = 2;
            obj015.coun = Convert.ToInt32(lblstrconteo.Text.ToUpper());
            obj015.refcntd = 0;
            obj015.refcntu = 0;
            parameterCollection015.Add(obj015);

            int retorno = idal015.insertarRegistro(ref parameterCollection015, ref strError);

            if (!string.IsNullOrEmpty(strError))
            {
                lblError.Text = strError;
                lblError.Visible = true;
                return;
            }


            divLote.Visible = false;
            lblResult.Text = "Count saved sucessfully.";
            lblResult.Visible = true;
            Habilitar(false);
            limpiar();
        }

        void Habilitar(Boolean estado)
        {
            divWarehouse.Visible = estado;
            divCantidad.Visible = estado;
            txtlocation.ReadOnly = estado;
            txtItem.ReadOnly = estado;
            txtLot.ReadOnly = estado;
            //btnContinue.Visible = !estado;
            btnSave.Visible = estado;
        }

        void limpiar()
        {
            lblWarehouse.Text = string.Empty;
            txtlocation.Text = string.Empty;
            txtLot.Text = string.Empty;
            txtItem.Text = string.Empty;
            txtItem.Enabled = false;
            txtCantidad.Text = string.Empty;
            lblDescItem.Text = string.Empty;
            lblError.Text = string.Empty;
            //btnContinue.Enabled = false;
            lblstrconteo.Text = string.Empty;
            lblstrclot.Text = string.Empty;
            lblstrseq.Text = string.Empty;
        }

        void InsertarIngresoPagina()
        {
            if (lblIngreso.Text == "1")
            {
                lblIngreso.Text = "0";

                List<Ent_ttccol301> parameterCollection301 = new List<Ent_ttccol301>();

                obj301.user = Session["user"].ToString();
                obj301.come = "Inventory Raw Material";
                obj301.refcntd = 0;
                obj301.refcntu = 0;
                parameterCollection301.Add(obj301);

                int retorno = idal301.insertarRegistro(ref parameterCollection301, ref strError);

                if (!string.IsNullOrEmpty(strError))
                {
                    lblError.Visible = true;
                    lblError.Text = strError;
                    return;
                }
            }
        }
    }
}
