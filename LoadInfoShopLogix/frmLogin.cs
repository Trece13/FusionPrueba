using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using whusa.Interfases;
using whusa.Entidades;

namespace whusa.LoadInfoShopLogix
{
    public partial class frmLogin : Form
    {
        string strError = string.Empty;
        DataTable resultado = new DataTable();

        protected static InterfazDAL_ttccol300 idal = new InterfazDAL_ttccol300();
        protected static InterfazDAL_ttccol303 ttccol303 = new InterfazDAL_ttccol303();
        Ent_ttccol300 obj = new Ent_ttccol300();
        Usuario usuario = new Usuario();

        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            obj = new Ent_ttccol300();
            List<Ent_ttccol300> parameterCollection = new List<Ent_ttccol300>();
            strError = string.Empty;
            
            if (validarDatos())
            {
                obj.user = txtUserName.Text.ToUpperInvariant().Trim();
                obj.pass = txtPassword.Text.Trim();
                try
                {
                    resultado = idal.listaRegistro_Param(ref obj, ref strError);
                }
                catch
                {
                    MessageBox.Show(strError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                MessageBox.Show(strError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (resultado.Rows.Count < 1)
            {
                MessageBox.Show("Usuario no valido. Intente de nuevo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (resultado.Rows[0]["pswd"].ToString() != obj.pass)
            {
                MessageBox.Show("Contraseña no válida, intente de nuevo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            obj.nama = resultado.Rows[0]["Nombre"].ToString();
            obj.refcntd = 0;
            obj.refcntu = 0;
            parameterCollection.Add(obj);
            
            //int retorno = idal.actualizarRegistro(ref parameterCollection, ref strError);

            //if (!string.IsNullOrEmpty(strError))
            //{
            //    MessageBox.Show(strError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}

            usuario.User = obj.user.Trim();
            usuario.UserName = resultado.Rows[0]["Nombre"].ToString();
            usuario.logok = "OKYes";
            try
            {
                frmLoadShopLogix form = new frmLoadShopLogix(usuario, this);
                this.Visible = false;
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtUserName.Text = string.Empty;
            txtPassword.Text = string.Empty;

            usuario = new Usuario();
        }

        private bool validarDatos()
        {
            bool siValido = false;

            if (string.IsNullOrEmpty(txtUserName.Text) || string.IsNullOrEmpty(txtPassword.Text))
                strError = "User or Password invalid";
            else
                siValido = true;
            return siValido;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void keypressed(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnOk_Click(sender, e);
            }
        }
    }
}
