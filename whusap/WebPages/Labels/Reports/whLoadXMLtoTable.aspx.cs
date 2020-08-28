using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using whusa.Utilidades;
using whusa.Interfases;
using System.Data;
using System.Configuration;
using System.Net;
using System.IO;
using whusa;
using System.Xml;
using System.Globalization;
using whusa.Entidades;

namespace whusap.WebPages.Reports
{
    public partial class whLoadXMLtoTable : System.Web.UI.Page
    {
        string strError = string.Empty;
        Recursos recursos = new Recursos();
        InterfazDAL_XMLGenerate idal = new InterfazDAL_XMLGenerate();
        string strTitulo = "Load Info. ShopLogix";
        List<string> lstMensajesError = new List<string>();
        string path = ConfigurationManager.AppSettings["webFolderPath"].ToString();
        string file = ConfigurationManager.AppSettings["webFolderPath"].ToString() + ConfigurationManager.AppSettings["pathResourcesSQL"].ToString() + "MachinesId.xml";
        string urlShopLogix = ConfigurationManager.AppSettings["urlshoplogixxml"].ToString();
        string extension = ".xml";

        DateTime dtStart;
        DateTime dtEnd;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Label control = (Label)Page.Controls[0].FindControl("lblPageTitle");
                if (control != null)
                {
                    control.Text = strTitulo;
                    control.Attributes.Add("style", "font-weight: bold;");
                }
                //GridView gridView = (GridView)Page.Controls[0].FindControl("gvMachines");
                //gridView.DataSource = null;
                //gridView.DataBind();
                CargarDatos();
            }
        }

        protected void CargarDatos()
        {
            try
            {
                System.Xml.XmlTextReader reader = new System.Xml.XmlTextReader(file);
                DataTable machineSel = new DataTable();
                machineSel.ReadXml(reader);

                //Cargar GridVew
                gvMachines.DataSource = machineSel.Select("Id = 'USA'").CopyToDataTable();
                gvMachines.AllowPaging = true;
                gvMachines.AllowSorting = true;
                gvMachines.AutoGenerateColumns = false;
                gvMachines.Visible = true;
                gvMachines.DataBind();

            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    strError = ex.Message + " (" + ex.InnerException + ")";
                else
                    strError = ex.Message;
            }
        }

        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            InterfazDAL_tticol074 idal074 = new InterfazDAL_tticol074();
            InterfazDAL_tticol075 idal075 = new InterfazDAL_tticol075();
            List<Ent_tticol074> parametrosIn074 = new List<Ent_tticol074>();
            List<Ent_tticol075> parametrosIn075 = new List<Ent_tticol075>();

            Dictionary<string, bool> dcArchivos = new Dictionary<string, bool>();

            if (!DateTime.TryParseExact(Request.Form[txtStartDate.UniqueID], "MM/dd/yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out dtStart))
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "Start date is invalid";
                return;
            }
            if (!DateTime.TryParseExact(Request.Form[txtEndDate.UniqueID], "MM/dd/yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out dtEnd))
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "Start end is invalid";
                return;
            }

            
            if (dtStart >= dtEnd)
            {
                lblMessage.Text = ("Start date cannot be greater than or equal to end date");
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }
            else if (dtStart > DateTime.Now)
            {
                lblMessage.Text = ("Start date can not be greater than today's date");
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }

            if (!System.IO.File.Exists(Page.ResolveUrl(file)))
                recursos.createMachines(path);

            string userImpersonation = string.Empty, passImpersonation = string.Empty, domaImpersonation = string.Empty;
            
            try
            {
                userImpersonation = ConfigurationManager.AppSettings["userImpersonation"].ToString();
                passImpersonation = ConfigurationManager.AppSettings["passImpersonation"].ToString();
                domaImpersonation = ConfigurationManager.AppSettings["domaImpersonation"].ToString();

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
                    lblMessage.Text = ("No machine selected");
                    lblMessage.ForeColor = System.Drawing.Color.Red;
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
                        lblMessage.Text = strError;
                        lblMessage.ForeColor = System.Drawing.Color.Red;
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
                            horas = Math.Round(Convert.ToDecimal(row[3].ToString()), 1),
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
                        lblMessage.Text = strError;
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        return;
                    }
                    strError = "Upload successfully";
                    lblMessage.Text = strError;
                    lblMessage.ForeColor = System.Drawing.Color.Black;
                }
                else
                {
                    strError = "Upload unsuccessfully";
                    lblMessage.Text = strError;
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.InnerException != null ?
                    ex.Message + " (" + ex.InnerException.Message + ")" :
                    ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
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
                                        horas = Math.Round(Convert.ToDecimal(Utils.DateDiffHours(start, end > dtEnd ? dtEnd : end, ref strError)), 4),
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
                foreach (GridViewRow row in gvMachines.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);
                        if (chkRow.Checked)
                            lstMachineRecords.Add("'" + row.Cells[2].Text + "'");
                    }
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

        protected void gvMachines_PageIndexChanged(object sender, GridViewPageEventArgs e)
        {
            this.CargarDatos();

            gvMachines.PageIndex = e.NewPageIndex;
            gvMachines.DataBind();
        }
    }
}