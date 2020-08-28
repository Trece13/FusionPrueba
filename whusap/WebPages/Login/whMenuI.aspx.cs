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
using System.Web.Configuration;

namespace whusap.WebPages.Login
{
    

    public partial class whMenuI : System.Web.UI.Page
    {
        List<Ent_ttccol303> lstMenu = new List<Ent_ttccol303>();

        protected static InterfazDAL_ttccol303 idal = new InterfazDAL_ttccol303();
        protected static SlideMenuItems items2;
        protected static DataTable resultado = new DataTable();
        Ent_ttccol300 obj = new Ent_ttccol300();
        private static string _idioma = String.Empty;
        string strError = string.Empty;
        public string UrlBaseBarcode = WebConfigurationManager.AppSettings["UrlBaseBarcode"].ToString();


        protected void Page_Load(object sender, EventArgs e)
        {

            // Cambiar cultura para manejo de separador decimal
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-CO");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("es-CO");
            base.InitializeCulture();

            if (!IsPostBack)
            {
                string strTitulo = "Home";
                Label control = (Label)Page.Controls[0].FindControl("lblPageTitle");
                control.Text = strTitulo;

                if (Session["user"] == null) { Response.Redirect("/WebPages/Login/whLogIni.aspx"); }
                obj.user = Session["user"].ToString();
                resultado = idal.listaRegistrosMenu_Param(ref obj, ref strError);

                Session["IsPreviousPage"] = "";
                //StringBuilder htmlTabla = crearMenu();

                //ltrTabla.Text = Server.HtmlDecode(Server.Html htmlTabla.ToString());
                //divTabla.InnerHtml = htmlTabla.ToString();

                if (Session["ddlIdioma"] == null)
                {
                    Session["ddlIdioma"] = "INGLES";
                }

                _idioma = Session["ddlIdioma"].ToString();


                tblMenu.InnerHtml = "<div>"+crearMenuCategorizado()+Menuflujo()+"</div>";
                
                //Session["Arbol"] = Menuflujo();
                //tblMenu.InnerHtml = crearMenu();
            }
        }

        protected string crearMenu()
        {
            var urlBase = ConfigurationManager.AppSettings["UrlBase"].ToString();
            string htmlTabla = String.Empty;
            string[] valores = new string[3];
            valores[0] = Session["user"].ToString();
            valores[1] = Session["logok"].ToString();
            valores[2] = Session["username"].ToString();
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
            string strDescriptionButtonESP = String.Empty;
            string strDescriptionButtonENG = String.Empty;

            
            htmlTabla += "<table>";

            string siteName = ConfigurationManager.AppSettings["enlaceRet"].ToString();
            string rutaServ = UrlBaseBarcode + "/" + siteName + "/";
            string idLnk = string.Empty;
            int intFila = 0;

            foreach(DataRow reg in resultado.Rows)
            {
                strOrden = reg["orden"].ToString().Trim();
                if (reg["programa"].ToString().Trim().StartsWith("http"))
                {
                    strSess = String.Concat(reg["programa"].ToString().Trim());
                }
                else
                {
                    strSess = String.Concat(urlBase, reg["programa"].ToString().Trim().StartsWith("/") ? "" : "/", reg["programa"].ToString().Trim());
                }
                strBoton = urlBase + "/" + reg["boton"].ToString().Trim();
                strDescriptionButtonESP = reg["MESP"].ToString();
                strDescriptionButtonENG = reg["MENG"].ToString();

                idLnk = "lnk_" + strSess.Replace(".aspx?flag=Y","");

                var a = strSess.Split('?').Last();
                strSess += strSess.Split('?').Last() != strSess ? "&" + urlParametro : "?" + urlParametro;
                //idLnk = idLnk.Replace("/fusionp/WebPages/", "");
                idLnk = idLnk.Replace(urlBase + "/WebPages/", "");
                idLnk = idLnk.Remove(0, idLnk.IndexOf("/") + 1);
                if (idLnk.IndexOf('?') > 0)
                {
                    int cntCh = idLnk.Length - idLnk.IndexOf('?');
                    idLnk = idLnk.Remove(idLnk.IndexOf('?'), cntCh);
                }

                idLnk = "lnk_" + idLnk.Replace(".aspx", "");                
                intFila = Convert.ToInt32(strOrden) % 4;

                switch (intFila)
                {
                    case 1:
                        htmlTabla += "<tr>";
                        htmlTabla += "<td align='center' style='min-width:204px; padding:3px;'>" +
                            //string.Format("<asp:HyperLink ID='{0}' runat='server' CssClass='ButtonsSendSave' " +
                            //"ImageUrl='{1}' NavigateUrl='{2}'></asp:HyperLink>", idLnk, strBoton, strSess) + "</td>";
                            String.Format("<input type='button' onclick='console.log(this)' class='buttonMenu' value='{1}' data-url='{0}'>", strSess, Session["ddlIdioma"].ToString() == "ESPAÑOL" ? strDescriptionButtonESP.Trim() : strDescriptionButtonENG.Trim());
                        break;
                    case 2:
                        htmlTabla += "<td align='center' style='min-width:204px; padding:3px;'>" +
                                          //string.Format("<asp:HyperLink ID='lnk_{0}' runat='server' CssClass='ButtonsSendSave' " +
                                          //"ImageUrl='{1}' NavigateUrl='{2}'></asp:HyperLink>", idLnk, strBoton, strSess) + "</td>";
                         String.Format("<input type='button' onclick='console.log(this)' class='buttonMenu' value='{1}' data-url='{0}'>", strSess, Session["ddlIdioma"].ToString() == "ESPAÑOL" ? strDescriptionButtonESP.Trim() : strDescriptionButtonENG.Trim());

                       // htmlTabla.Append("<td align='center' class='style3'><a href=" + strSess + "><img src=" + strBoton + "></a></td>");
                        break;
                    case 3:
                        htmlTabla += "<td align='center ' style='min-width:204px; padding:3px;'>" +
                                          //string.Format("<asp:HyperLink ID='lnk_{0}' runat='server' CssClass='ButtonsSendSave' " +
                                          //"ImageUrl='{1}' NavigateUrl='{2}'></asp:HyperLink>", idLnk, strBoton, rutaServ + strSess) + "</td>";
                    //htmlTabla.Append("<td align='center' class='style3'><a href=" + strSess + "><img src=" + strBoton + "></a></td>");
                         String.Format("<input type='button' onclick='console.log(this)' class='buttonMenu' value='{1}' data-url='{0}'>", strSess, Session["ddlIdioma"].ToString() == "ESPAÑOL" ? strDescriptionButtonESP.Trim() : strDescriptionButtonENG.Trim());
                        break;
                    case 0:
                        htmlTabla += "<td align='center' style='min-width:204px; padding:3px;'>" +
                                          //string.Format("<asp:HyperLink ID='lnk_{0}' runat='server' CssClass='ButtonsSendSave' " +
                                          //"ImageUrl='{1}' NavigateUrl='{2}'></asp:HyperLink>", idLnk, strBoton, rutaServ + strSess) + "</td>";
                         String.Format("<input type='button' onclick='console.log(this)' class='buttonMenu' value='{1}' data-url='{0}'>", strSess, Session["ddlIdioma"].ToString() == "ESPAÑOL" ? strDescriptionButtonESP.Trim() : strDescriptionButtonENG.Trim());

                    //htmlTabla.Append("<td align='center' class='style3'><a href=" + strSess + "><img src=" + strBoton + "></a></td>");
                        htmlTabla += "</tr>";
                        break;
                }
            }
            htmlTabla += "</table>";

            
            return htmlTabla;

        }


        protected string crearMenuCategorizado()
        {
            var urlBase = ConfigurationManager.AppSettings["UrlBase"].ToString();
            string htmlTabla = String.Empty;
            string[] valores = new string[3];
            valores[0] = Session["user"].ToString();
            valores[1] = Session["logok"].ToString();
            valores[2] = Session["username"].ToString();
            int valor = 1;
            string urlParametro = string.Empty;

            foreach (string param in valores)
            {
                urlParametro += "Valor" + valor.ToString() + "=" + param + "&";
                valor++;
            }

            urlParametro = urlParametro.Substring(0, urlParametro.Length - 1);

            string strOrden = string.Empty;
            string strSess = string.Empty;
            string strBoton = string.Empty;
            string strDescriptionButtonESP = String.Empty;
            string strDescriptionButtonENG = String.Empty;


            htmlTabla += "<table>";

            string siteName = ConfigurationManager.AppSettings["enlaceRet"].ToString();
            string rutaServ = UrlBaseBarcode + "/" + siteName + "/";
            string idLnk = string.Empty;
            int intFila = 0;

            //List<Ent_ttccol303> lstMenu = new List<Ent_ttccol303>();

            foreach (DataRow item in resultado.Rows){

                strOrden = item["orden"].ToString().Trim();
                if (item["programa"].ToString().Trim().StartsWith("http"))
                {
                    strSess = String.Concat(item["programa"].ToString().Trim());
                }
                else
                {
                    strSess = String.Concat(urlBase, item["programa"].ToString().Trim().StartsWith("/") ? "" : "/", item["programa"].ToString().Trim());
                }
                strBoton = urlBase + "/" + item["boton"].ToString().Trim();
                strDescriptionButtonESP = item["MESP"].ToString();
                strDescriptionButtonENG = item["MENG"].ToString();

                idLnk = "lnk_" + strSess.Replace(".aspx?flag=Y", "");

                var a = strSess.Split('?').Last();
                strSess += strSess.Split('?').Last() != strSess ? "&" + urlParametro : "?" + urlParametro;
                //idLnk = idLnk.Replace("/fusionp/WebPages/", "");
                idLnk = idLnk.Replace(urlBase + "/WebPages/", "");
                idLnk = idLnk.Remove(0, idLnk.IndexOf("/") + 1);
                if (idLnk.IndexOf('?') > 0)
                {
                    int cntCh = idLnk.Length - idLnk.IndexOf('?');
                    idLnk = idLnk.Remove(idLnk.IndexOf('?'), cntCh);
                }

                idLnk = "lnk_" + idLnk.Replace(".aspx", "");  


                Ent_ttccol303 MyObj = new Ent_ttccol303();

                MyObj.boton = item["boton"].ToString().Trim();
                MyObj.programa = item["programa"].ToString().Trim();
                MyObj.categoria = item["categoria"].ToString().Trim();
                MyObj.MENG = item["MENG"].ToString().Trim();
                MyObj.MESP = item["MESP"].ToString().Trim();
                MyObj.orden = item["orden"].ToString().Trim();
                MyObj.strSess = strSess;
                MyObj.MENUPADRE = item["MENUPADRE"].ToString().Trim();
                MyObj.MENUID = item["MENUID"].ToString().Trim();

                lstMenu.Add(MyObj);
                
            }

            

            var Categorias = from a in lstMenu select a.categoria;
            //lblCategory.Text = Categorias.FirstOrDefault();
            Categorias = Categorias.Distinct().OrderBy(x => x);
            
            Dictionary<string,List<Ent_ttccol303>> Lstdividida = new Dictionary<string,List<Ent_ttccol303>>();
            for (int CategoriaActual = 0; CategoriaActual < Categorias.Count(); CategoriaActual++)
            {
                List<Ent_ttccol303> ListaFiltroCategoria = lstMenu.Where(x => x.categoria == Categorias.ElementAt(CategoriaActual)).ToList();
                Lstdividida.Add(Categorias.ElementAt(CategoriaActual), ListaFiltroCategoria);
            }

            int CountCategorias = Lstdividida.Count();
            int CategoriasPagina = 3;
            int TotalpaginasSlider = Convert.ToInt32(Math.Ceiling(CountCategorias / Convert.ToDouble(CategoriasPagina)));

            string htmlSliderHead = string.Empty;
            string htmlSliderFooter = string.Empty;
            string htmlSliderBody = string.Empty;
            string htmlSliderItemActiveHead = string.Empty;
            string htmlSliderItemHead = string.Empty;
            string htmlSliderItemEnd = string.Empty;
            string htmlReturn = string.Empty;

            htmlSliderHead +=
                "<div id='ModeCategoryMenu' style='padding-bottom: 60px;'><div id='carouselExampleIndicators' class='carousel slide' data-ride='carousel'>" +
                    "<div class='carousel-inner' role='listbox'>";

            htmlSliderFooter +=
                "</div></div></div>" +
                    "<a class='carousel-control-prev' href='#carouselExampleIndicators' role='button' data-slide='prev'>" +
                        "<span class='carousel-control-prev-icon' aria-hidden='true'></span>" +
                        "<span class='sr-only'>Previous</span>" +
                    "</a>" +
                    "<a class='carousel-control-next' href='#carouselExampleIndicators' role='button' data-slide='next'>" +
                        "<span class='carousel-control-next-icon' aria-hidden='true'></span>" +
                        "<span class='sr-only'>Next</span>" +
                    "</a>" +
                "</div></div></div></div>";

            htmlSliderItemActiveHead +=
                "<div class='carousel-item active'>" +
                            "<div class='d-flex justify-content-around' alt='First slide'>";
            htmlSliderItemHead +=
                "<div class='carousel-item'>" +
                            "<div class='d-flex justify-content-around' alt='First slide'>";

            htmlSliderItemEnd +=
                    "</div>" +
                "</div>";

            int conuntCategory = 0;//
            bool flagCategory = false;

            htmlReturn += htmlSliderHead;
            
            foreach (var MenuCategorizado in Lstdividida)
            {

                if (conuntCategory == 0 && flagCategory == false) 
                {
                    htmlReturn += htmlSliderItemActiveHead; 
                    flagCategory = true;
                }
                else if (conuntCategory == 0 && flagCategory == true)
                {
                    htmlReturn += htmlSliderItemHead; 
                }

                string NomCategoria = MenuCategorizado.Key;

                htmlSliderBody +=
                    "<div class='card shadow align-self-start' >"+
                    "<div class='card-header'>" +
                            "<span class='glyphicon glyphicon-th-list' style='display: inline-block; color:212529'></span><h4 class='card-title' style='display: inline-block'>&nbsp" + NomCategoria + "</h4>" +
                        "</div>"+
                        "<div class='card-body " +NomCategoria+"Class'>";
                    

                List<Ent_ttccol303> lstCat = MenuCategorizado.Value;
                int Submenu = 1;
                foreach (Ent_ttccol303 item in lstCat)
                {
                    htmlSliderBody +=
                        "<a onclick = saveNamePage('" + item.MENG.ToString().Replace(" ","_") + "') href='" + item.strSess + "'class='list-group-item list-group-item-action list-sm'" + (Submenu > 10 ? "style = 'Display:none; border-left-color:transparent; border-right-color:transparent'" : "style = 'border-left-color:transparent; border-right-color:transparent'") + ">" + (Session["ddlIdioma"] == "INGLES" ? item.MENG : item.MESP) + "</a>";
                    Submenu++;
                }
                string xe = NomCategoria;
                htmlSliderBody += 
                        "</div>"+
                        string.Format((lstCat.Count > 10 ? "<input type='button' name='Hola'id='btn" + NomCategoria + "' class='btx' value='"+(Session["ddlIdioma"]=="INGLES"?"View More":"Ver Mas")+"' onclick='MyFnc(this)'>" : ""), xe) +
                    "</div>";

                htmlReturn += htmlSliderBody;
                htmlSliderBody = string.Empty;

                conuntCategory++;

                if (conuntCategory == CategoriasPagina)
                {
                    conuntCategory = 0;
                    htmlReturn += htmlSliderItemEnd;
                }
            }


            htmlReturn += htmlSliderFooter;

            return htmlReturn;

        }

        public string Menuflujo()
        {
            const int MenuPorPAgina = 5;
            int contFlow = 0;
            string ReturHtml = "<div id='MenuFlow' style='display:none; line-height: 15px;'><div id='carouselFlow' class='carousel slide' data-ride='carousel'>" +
                "<div class='carousel-inner' role='listbox'>";
            foreach (Ent_ttccol303  obj in lstMenu) {
                

                if (obj.MENUPADRE == "") 
                {
                    if (contFlow == 0)
                    {
                        ReturHtml += "<div class='carousel-item active'>" +
                                    "<div class='d-flex justify-content-around' alt='First slide'>";
                    }
                    else if ((contFlow % MenuPorPAgina) == 0)
                    {
                        ReturHtml += "<div class='carousel-item'>" +
                                "<div class='d-flex justify-content-around' alt='First slide'>";
                    }
                    ReturHtml += "<div style = 'float:left'><label id='lblCategoryTitle' class = 'bd-title'><strong>" + obj.categoria + "</strong></label><!--<ul>-->";
                    ReturHtml += "<li><a href='" + obj.strSess + "' class=' btn " + (obj.categoria == "Reports" ? "btn-primary" : "btn-primary") + "'>" + (Session["ddlIdioma"] == "INGLES" ? obj.MENG : obj.MESP) + "</a></li>";
                    // ReturHtml += " <ul>";
                    foreach (Ent_ttccol303 subObj in lstMenu) {
                        if (subObj.MENUPADRE == obj.MENUID) {
                            ReturHtml += "<li><a href='" + subObj.strSess + "' class=' btn  " + (subObj.categoria == "Reports" ? "btn-primary" : "btn-primary") + "'>" + (Session["ddlIdioma"] == "INGLES" ? subObj.MENG : subObj.MESP) + "</a></li>";
                            //ReturHtml += "<ul>";
                            foreach (Ent_ttccol303 subObj1 in lstMenu) {
                                if (subObj1.MENUPADRE == subObj.MENUID) {
                                    ReturHtml += "<li><a href='" + subObj1.strSess + "' class=' btn " + (subObj1.categoria == "Reports" ? "btn-primary" : "btn-warning") + "'>" + (Session["ddlIdioma"] == "INGLES" ? subObj1.MENG : subObj1.MESP) + "</a></li>";
                                    //ReturHtml += "<ul>";
                                    foreach (Ent_ttccol303 subObj2 in lstMenu) {
                                        if (subObj2.MENUPADRE == subObj1.MENUID) {
                                            ReturHtml += "<li><a href='" + subObj2.strSess + "' class=' btn  " + (subObj2.categoria == "Reports" ? "btn-primary" : "btn-info") + "'>" + (Session["ddlIdioma"] == "INGLES" ? subObj2.MENG : subObj2.MESP) + "</a></li>";
                                            //ReturHtml += "<ul>";
                                        }
                                    }
                                    //ReturHtml += "</ul>";
                                }
                            }
                            //ReturHtml += "</ul>";
                        }
                    }
                    //ReturHtml += "</ul>";
                }
                
                if (obj.MENUPADRE == "")
                {
                    ReturHtml += "</div>";
                    contFlow++;


                    if (((contFlow % MenuPorPAgina) == 0) && (contFlow != 0))
                    {
                        ReturHtml += "</div>" +
                        "</div>";
                    }

                    if (contFlow == 0)
                    {
                        ReturHtml += "</div>" +
                        "</div>";
                    }

                    if (lstMenu.Count() == contFlow)
                    {
                        ReturHtml += "</div>" +
                        "</div>";
                    }
                }
            }
            ReturHtml += "<div><div><a class='carousel-control-prev' href='#carouselFlow' role='button' data-slide='prev'>" +
            "<span class='carousel-control-prev-icon' aria-hidden='true'></span>" +
            "<span class='sr-only'>Previous</span>"+
          "</a>"+
          "<a class='carousel-control-next' href='#carouselFlow'  role='button' data-slide='next'>" +
            "<span class='carousel-control-next-icon' aria-hidden='true'></span>"+
            "<span class='sr-only'>Next</span>"+
          "</a>"+
      "</div>"+
    "</div></div></div></div>";
            var x = ReturHtml;
            return ReturHtml;
            
            /*JavaScriptSerializer js = new JavaScriptSerializer();
            string json = js.Serialize(lstRetorno);*/


        }




    }

    public enum Categoria { 
        Logistica = 1,
        Manofactura = 2,
        Reporte = 3
    }
}