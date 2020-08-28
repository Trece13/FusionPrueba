using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Serialization;
using whusa.Interfases;
using System.Data;
using System.Web.Script.Services;
using whusa.Entidades;

//namespace whusap.WebPages
//{
    /// <summary>
    /// Summary description for WebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class WebService : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod()]
        public string GetItems(string sItem)
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

        [WebMethod()]
        public string GetItemByBarCode(string sBarCode, string sTipo)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            InterfazDAL_tticol132 idal = new InterfazDAL_tticol132();
            string retorno = string.Empty;
            string strError = string.Empty;
            DataTable dtItems = null;
            sBarCode = sBarCode.ToString().Replace(" ", "").Replace("\"", "");
            sTipo = sTipo.ToString().Replace(" ", "").Replace("\"", "");
            try
            {
                dtItems = idal.listarItemByBarCode(sBarCode, sTipo, ref strError);
                retorno = js.Serialize(dtItems.AsEnumerable().Select(x => x.ItemArray.ToList()).ToList());
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

        [WebMethod()]
        public string ValidarMaquina(string sMachine)
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

        [WebMethod()]
        public string ValidarIdentifier(string sIdentifier)
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
//}
