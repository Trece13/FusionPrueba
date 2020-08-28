using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using whusa.Utilidades;
using whusa.Interfases;
using System.Configuration;
using whusa;
using System.Globalization;
using System.Xml;
using System.Diagnostics;

namespace whusa.LoadInfoShopLogix
{
    public partial class frmLoadShopLogix : Form
    {
        string strError = string.Empty;
        Recursos recursos = new Recursos();
        InterfazDAL_XMLGenerate idal = new InterfazDAL_XMLGenerate();
        List<string> lstMensajesError = new List<string>();
        string path = ConfigurationManager.AppSettings["webFolderPath"].ToString();
        string file = ConfigurationManager.AppSettings["webFolderPath"].ToString() + ConfigurationManager.AppSettings["pathResourcesSQL"].ToString() + "MachinesId.xml";
        string urlShopLogix = ConfigurationManager.AppSettings["urlshoplogixxml"].ToString();
        string extension = ".xml";
        public Usuario usuario { get; set; }
        frmLogin form;
        
        public frmLoadShopLogix()
        {
            InitializeComponent();

            CargarDatos();
        }

        public frmLoadShopLogix(Usuario usuario, frmLogin frmLogin)
        {
            try
            {
                InitializeComponent();
                this.usuario = usuario;
                CargarDatos();
                form = frmLogin;
            }
            catch (Exception ex)
            {
                this.Close();
                frmLogin.Show();
            }
        }

        protected void CargarDatos()
        {
            try
            {
                if (string.IsNullOrEmpty(usuario.UserName) || string.IsNullOrEmpty(usuario.UserName))
                {
                    this.Close();
                    (new frmLogin()).Show();
                    return;
                }
                System.Xml.XmlTextReader reader = new System.Xml.XmlTextReader(file);
                DataTable machineSel = new DataTable();

                DataGridViewCheckBoxColumn checkColumn = new DataGridViewCheckBoxColumn();
                checkColumn.Name = "chkRow";
                checkColumn.HeaderText = "Select";
                checkColumn.Width = 50;
                checkColumn.ReadOnly = false;
                checkColumn.FillWeight = 10;
                gvMachines.Columns.Add(checkColumn);
                

                machineSel.ReadXml(reader);
                
                //Cargar GridVew
                gvMachines.DataSource = machineSel.Select("Id = 'USA'").CopyToDataTable();
                gvMachines.AutoGenerateColumns = false;
                gvMachines.Visible = true;
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    strError = ex.Message + " (" + ex.InnerException + ")";
                else
                    strError = ex.Message;
            }
        }
        
        private void btnExit_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
                form.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            InterfazDAL_tticol074 idal074 = new InterfazDAL_tticol074();
            InterfazDAL_tticol075 idal075 = new InterfazDAL_tticol075();
            List<Ent_tticol074> parametrosIn074 = new List<Ent_tticol074>();
            List<Ent_tticol075> parametrosIn075 = new List<Ent_tticol075>();
            strError = string.Empty;
            Dictionary<string, bool> dcArchivos = new Dictionary<string, bool>();

            DateTime dtStart;
            DateTime dtEnd;


            if (!DateTime.TryParseExact(dtpStartDate.Text, "MM/dd/yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out dtStart))
            {
                MessageBox.Show("Start date is invalid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!DateTime.TryParseExact(dtpEndDate.Text, "MM/dd/yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out dtEnd))
            {
                MessageBox.Show("Start end is invalid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            if (dtStart >= dtEnd)
            {
                MessageBox.Show("Start date cannot be greater than or equal to end date", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (dtStart > DateTime.Now)
            {
                MessageBox.Show("Start date can not be greater than today's date", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!System.IO.File.Exists(file))
                recursos.createMachines(path);

            string userImpersonation = string.Empty, passImpersonation = string.Empty, domaImpersonation = string.Empty;

            userImpersonation = ConfigurationManager.AppSettings["userImpersonation"].ToString();
            passImpersonation = ConfigurationManager.AppSettings["passImpersonation"].ToString(); ;
            domaImpersonation = ConfigurationManager.AppSettings["domaImpersonation"].ToString(); ;
            try
            {
                System.Xml.XmlTextReader reader = new System.Xml.XmlTextReader(file);
                DataTable machineSel = new DataTable();

                machineSel.ReadXml(reader);

                string sRows = GetSelectedRecords(ref strError);
                if (string.IsNullOrEmpty(strError) && !string.IsNullOrEmpty(sRows))
                {
                    whusa.Utilidades.ImpersonateManager.ImpersonateUser(domaImpersonation, userImpersonation, passImpersonation);
                    foreach (DataRow row in machineSel.Select("Id = 'USA' AND MachineId IN (" + sRows + ")"))
                    {
                        string machineID = row["MachineID"].ToString();
                        string sURL = string.Format(urlShopLogix, machineID, dtStart.ToString("yyyyMMdd"), dtEnd.AddDays(1).ToString("yyyyMMdd"));
                        recursos.DownloadFile(sURL, machineID, extension, ref dcArchivos, ref strError);
                    }
                    whusa.Utilidades.ImpersonateManager.StopImpersonation();
                }
                else
                {
                    MessageBox.Show("No machine selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                DataTable dtTticol075 = new DataTable();
                dtTticol075.Columns.Add("orden", typeof(string));
                dtTticol075.Columns.Add("numOperacion", typeof(int));
                dtTticol075.Columns.Add("tarifaHoraria", typeof(string));
                dtTticol075.Columns.Add("horas", typeof(decimal));
                dtTticol075.Columns.Add("comentario", typeof(string));
                dtTticol075.Columns.Add("usuario", typeof(string));
                dtTticol075.Columns.Add("fecha", typeof(DateTime));
                dtTticol075.Columns.Add("chequeado", typeof(int));
                dtTticol075.Columns.Add("procesado", typeof(int));
                dtTticol075.Columns.Add("mensaje", typeof(string));
                dtTticol075.Columns.Add("refcntd", typeof(int));
                dtTticol075.Columns.Add("refcntu", typeof(int));


                foreach (var dc in dcArchivos)
                {
                    if (dc.Value)
                        ReadXML(dc.Key, ref parametrosIn074, ref strError);
                    else
                        lstMensajesError.Add("Error processing file: " + dc.Value + ".");

                    if (!string.IsNullOrEmpty(strError))
                    {
                        MessageBox.Show(strError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                
                List<Ent_tticol074> filterParametrosIn074 = parametrosIn074.Where(p => p.fecha >= dtStart.AddHours(5) && p.fecha <= dtEnd.AddHours(5)).ToList();
                var lstOrdenes = from u in filterParametrosIn074
                                 group u by u.orden into g
                                 orderby g.Count() descending
                                 select "'" + g.Key + "'";
                string nOrdenes = string.Join(",", lstOrdenes.ToList());
                if (!string.IsNullOrEmpty(nOrdenes))
                {
                    idal074.eliminarRegistro(nOrdenes, dtStart.AddHours(5), dtEnd.AddHours(5), ref strError);
                    idal075.eliminarRegistro(nOrdenes, dtStart.AddHours(5), dtEnd.AddHours(5), ref strError);

                    if (!validarRetorno(idal074.insertarRegistro(ref filterParametrosIn074, ref strError)))
                    {
                        MessageBox.Show(strError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    dtTticol075 = idal074.ObtenerConsolidado(nOrdenes, dtStart.AddHours(5), dtEnd.AddHours(5), ref strError);
                    foreach (DataRow row in dtTticol075.Rows)
                    {
                        parametrosIn075.Add(new Ent_tticol075()
                        {
                            orden = row[0].ToString(),
                            numOperacion = Convert.ToInt32(row[1].ToString()),
                            tarifaHoraria = row[2].ToString(),
                            horas = Convert.ToDecimal(row[3].ToString()),
                            comentario = row[4].ToString(),
                            usuario = row[5].ToString(),
                            fecha = Convert.ToDateTime(row[6].ToString()),
                            chequeado = Convert.ToInt32(row[7].ToString()),
                            procesado = Convert.ToInt32(row[8].ToString()),
                            mensaje = row[9].ToString(),
                            refcntd = Convert.ToInt32(row[10].ToString()),
                            refcntu = Convert.ToInt32(row[11].ToString())
                        });

                    }

                    if (!validarRetorno(idal075.insertarRegistro(ref parametrosIn075, ref strError)))
                    {
                        MessageBox.Show(strError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    strError = "Upload successfully";
                    MessageBox.Show(strError, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    strError = "Upload unsuccessfully";
                    MessageBox.Show(strError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                strError = ex.InnerException != null ?
                    ex.Message + " (" + ex.InnerException.Message + ")" :
                    ex.Message;
                MessageBox.Show(strError, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected void ReadXML(string file, ref List<Ent_tticol074> parametrosIn, ref string strError)
        {
            XmlDocument doc = new XmlDocument();
            try
            {
                if (System.IO.File.Exists(file))
                    doc.Load(file);
                else
                {
                    strError = "File not found";
                    return;
                }
                string sHost = string.Empty;
                string sOrden = string.Empty;
                string tipoTarifa = string.Empty;
                DateTime start = new DateTime(), end = new DateTime(), fecha = new DateTime();
                XmlReader rdr = XmlReader.Create(new System.IO.StringReader(doc.InnerXml));
                while (rdr.Read())
                {
                    if (rdr.NodeType == XmlNodeType.Element)
                    {
                        switch (rdr.LocalName)
                        {
                            case "Machine":
                                sOrden = rdr.GetAttribute("Name").ToString();
                                break;
                            case "Status":
                                sHost = rdr.GetAttribute("Job") != null ? rdr.GetAttribute("Job").ToString() : "NOJOB";
                                if (rdr.GetAttribute("Name").ToString() == "Running")
                                    tipoTarifa = "TR";
                                else if (rdr.GetAttribute("Name").ToString() == "Idle" || rdr.GetAttribute("Name").ToString() == "Setup")
                                {
                                    if (string.IsNullOrEmpty(rdr.GetAttribute("Reason")))
                                        tipoTarifa = "NA";
                                    else
                                        tipoTarifa = rdr.GetAttribute("Reason").Substring(0, 2);
                                }
                                start = DateTime.Parse(rdr.GetAttribute("Start"), new System.Globalization.CultureInfo("en-us"));
                                end = DateTime.Parse(rdr.GetAttribute("End"), new System.Globalization.CultureInfo("en-us"));
                                fecha = DateTime.Parse(rdr.GetAttribute("Start"), new System.Globalization.CultureInfo("en-us"));
                                if (tipoTarifa != "NA" && sHost != "NOJOB")
                                {
                                    parametrosIn.Add(new Ent_tticol074()
                                    {
                                        orden = sHost,
                                        numOperacion = 10,
                                        tarifaHoraria = tipoTarifa,
                                        horas = Math.Round(Convert.ToDecimal(Utils.DateDiffHours(start, end, ref strError)), 4),
                                        comentario = "C000".PadLeft(9, ' '),
                                        usuario = "Shoplogix",
                                        fecha = start.AddHours(5),
                                        chequeado = 2,
                                        procesado = 2,
                                        mensaje = sOrden,
                                        refcntd = 0,
                                        refcntu = 0
                                    });

                                    //if (dtTticol075.Select(string.Format("Orden = '{0}'", sHost)).Count() == 0)
                                    //{
                                    //    dtTticol075.Rows.Add(new object[] {sHost,
                                    //    10,
                                    //    tipoTarifa,
                                    //    Math.Round(Convert.ToDecimal(Utils.DateDiffHours(start, end, ref strError)), 2),
                                    //    "C000".PadLeft(9, ' '),
                                    //    "Shoplogix",
                                    //    fecha = start,
                                    //    2,
                                    //    2,
                                    //    sOrden,
                                    //    0,
                                    //    0});
                                    //}
                                }
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                strError = ex.InnerException != null ?
                    ex.Message + " (" + ex.InnerException.Message + ")" :
                    ex.Message;
            }
        }

        protected string GetSelectedRecords(ref string strError)
        {
            string machineRecords = string.Empty;
            List<string> lstMachineRecords = new List<string>();
            try
            {
                foreach (DataGridViewRow row in gvMachines.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    if (Convert.ToBoolean(chk.Value))
                        lstMachineRecords.Add("'" + row.Cells[2].Value.ToString() + "'");
                }
                if (lstMachineRecords.Count > 0)
                    machineRecords = string.Join(",", lstMachineRecords);

                return machineRecords;
            }
            catch (Exception ex)
            {
                strError = ex.InnerException != null ?
                    ex.Message + " (" + ex.InnerException.Message + ")" :
                    ex.Message;

                return machineRecords;
            }
        }

        protected bool validarRetorno(int retorno)
        {
            if (retorno <= 0)
                return false;
            else
                return true;
        }
    
    }
}
