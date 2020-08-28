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
using whusa.Utilidades;
using System.Collections;
using OboutInc.SlideMenu;

namespace whusap.WebPages.Login
{
    public partial class whMenuI : System.Web.UI.Page
    {
        string strError = string.Empty;
        //string Aplicacion = "WEBAPP";
        DataTable resultado = new DataTable();

        protected static InterfazDAL_ttccol303 idal = new InterfazDAL_ttccol303();
        protected static SlideMenuItems items2;
        Ent_ttccol300 obj = new Ent_ttccol300();

        protected void Page_Load(object sender, EventArgs e)
        {
            // Cambiar cultura para manejo de separador decimal
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-CO");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("es-CO");
            base.InitializeCulture();

            if (!IsPostBack)
            {
                //if (Session["IsPreviousPage"] == null) { Session.Clear(); }
                OboutInc.SlideMenu.SlideMenu mainMenu = (OboutInc.SlideMenu.SlideMenu) Page.Controls[0].FindControl("mainMenu");

                if (Session["itemsMenu"] == null)
                {
                    mainMenu.AddParent("Inventory", "Inventory");
                    
                    mainMenu.AddChild("whInvMaterialTakePrintLabel", "Label Generator", "../InvMaterial/whInvMaterialTakePrintLabel.aspx");
                    mainMenu.AddChild("lnk_whInvMaterialTakeRegister", "Counting", "../InvMaterial/whInvMaterialTakeRegister.aspx");
                    mainMenu.AddChild("whInvMaterialDevolution", "Devolution", "../InvMaterial/whInvMaterialDevolution.aspx");
                    mainMenu.AddChild("whInvMaterialDevolutionConfirm", "Confirm return", "../InvMaterial/whInvMaterialDevolutionConfirm.aspx");
                    mainMenu.AddChild("whInvMaterialPlasticBoxesTracker", "Plastic Boxes Tracker", "../InvMaterial/whInvMaterialPlasticBoxesTracker.aspx");
                    mainMenu.AddChild("whInvMaterialPlasticUsedBoxesTracker", "Plastic Used Boxes Tracker", "../InvMaterial/whInvMaterialPlasticUsedBoxesTracker.aspx");
                    mainMenu.AddChild("whInvMaterialPlasticReturnBoxesTracker", "Plastic Return Boxes Tracker", "../InvMaterial/whInvMaterialPlasticReturnBoxesTracker.aspx");
                    mainMenu.AddChild("whLoadXMLtoTable.aspx", "Load table from ShopLogix", "../Reports/whLoadXMLtoTable.aspx");
                    mainMenu.AddChild("whInvMaterialRejectedD.aspx", "Rejected MBR", "../InvMaterial/whInvMaterialRejectedD.aspx");
                    
                    Recursos recursos = new Recursos();
                    //List<object> items = recursos.saveMenuItems(mainMenu.MenuItems);
                    //Session["itemsMenu"] = items;
                }

                
                string strTitulo = "BAAN FUSION [ Home ]";
                Label control = (Label)Page.Controls[0].FindControl("lblPageTitle");
                control.Text = strTitulo;

                if (Session["user"] == null) { Server.Transfer("whLogIni.aspx"); }
                obj.user = Session["user"].ToString();
                resultado = idal.listaRegistrosMenu_Param(ref obj, ref strError);

                StringBuilder htmlTabla = crearMenu();

                //ltrTabla.Text = Server.HtmlDecode(Server.Html htmlTabla.ToString());
                //divTabla.InnerHtml = htmlTabla.ToString();
            }

            
        }
        protected StringBuilder crearMenu()
        {
            StringBuilder htmlTabla = new StringBuilder();
            string[] valores = new string[3];
            valores[0] = (string)AspSession.Get("user");
            valores[1] = (string)AspSession.Get("logok");
            valores[2] = (string)AspSession.Get("username");
            int valor = 1;
            string urlParametro = string.Empty;

            foreach(string param in valores)
            {
                urlParametro += "Valor" + valor.ToString() + "=" + param + "&";
                valor++;
            }
            
            urlParametro = urlParametro.Substring( 0, urlParametro.Length - 1);

            string strOrden = string.Empty;
            string strSess = string.Empty;
            string strBoton = string.Empty;

            
            htmlTabla.Append("<table style='width: 80%; height: 100%;'>");

            string siteName = ConfigurationManager.AppSettings["enlaceRet"].ToString();
            string rutaServ = "http://" + Request.Url.Authority + "/" + siteName + "/";
            string idLnk = string.Empty;
            int intFila = 0;

            foreach(DataRow reg in resultado.Rows)
            {
                strOrden = reg["orden"].ToString().Trim();
                strSess = reg["programa"].ToString().Trim();
                strBoton = "~/" + reg["boton"].ToString().Trim();

                idLnk = "lnk_" + strSess.Replace(".aspx?flag=Y","");
                if (idLnk.Contains("fusionp/"))
                {
                    strSess += "?" + urlParametro;
                    idLnk = idLnk.Replace("/fusionp/WebPages/", "");
                    idLnk = idLnk.Remove(0, idLnk.IndexOf("/") + 1);
                    if (idLnk.IndexOf('?') > 0)
                    {
                        int cntCh = idLnk.Length - idLnk.IndexOf('?');
                        idLnk = idLnk.Remove(idLnk.IndexOf('?'), cntCh);
                    }
                }

                idLnk = "lnk_" + idLnk.Replace(".aspx", "");                
                intFila = Convert.ToInt32(strOrden) % 4;

                switch (intFila)
                {
                    case 1:
                        htmlTabla.Append("<tr>");
                        htmlTabla.Append("<td align='center'>" +
                                          string.Format("<asp:HyperLink ID='{0}' runat='server' CssClass='ButtonsSendSave' " +
                                          "ImageUrl='{1}' NavigateUrl='{2}'></asp:HyperLink>", idLnk, strBoton, strSess) + "</td>");
                        break;
                    case 2:
                        htmlTabla.Append("<td align='center'>" +
                                          string.Format("<asp:HyperLink ID='lnk_{0}' runat='server' CssClass='ButtonsSendSave' " +
                                          "ImageUrl='{1}' NavigateUrl='{2}'></asp:HyperLink>", idLnk, strBoton, strSess) + "</td>");

                       // htmlTabla.Append("<td align='center' class='style3'><a href=" + strSess + "><img src=" + strBoton + "></a></td>");
                        break;
                    case 3:
                        htmlTabla.Append("<td align='center'>" +
                                          string.Format("<asp:HyperLink ID='lnk_{0}' runat='server' CssClass='ButtonsSendSave' " +
                                          "ImageUrl='{1}' NavigateUrl='{2}'></asp:HyperLink>", idLnk, strBoton, rutaServ + strSess) + "</td>");
                    //htmlTabla.Append("<td align='center' class='style3'><a href=" + strSess + "><img src=" + strBoton + "></a></td>");
                        break;
                    case 0:
                        htmlTabla.Append("<td align='center'>" +
                                          string.Format("<asp:HyperLink ID='lnk_{0}' runat='server' CssClass='ButtonsSendSave' " +
                                          "ImageUrl='{1}' NavigateUrl='{2}'></asp:HyperLink>", idLnk, strBoton, rutaServ + strSess) + "</td>");

                    //htmlTabla.Append("<td align='center' class='style3'><a href=" + strSess + "><img src=" + strBoton + "></a></td>");
                        htmlTabla.Append("</tr>");
                        break;
                }
            }
            htmlTabla.Append("</table>");
            return htmlTabla;

        }
    }
}