using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Xml;
using System.Configuration;

namespace whusap.WebPages.WorkOrders
{
    public partial class EditParamPaginador : System.Web.UI.Page
    {
        public static string CicloPaginacion = string.Empty;
        public static string CicloActualizacion = string.Empty;

        public static string numberRetryOnSave = string.Empty;
        public static string bodyColor = string.Empty;
        public static string owner = string.Empty;
        public static string env = string.Empty;
        public static string InitialVector = string.Empty;
        public static string KeyAlgorithm = string.Empty;
        public static string envCol = string.Empty;
        public static string envt = string.Empty;
        public static string cia = string.Empty;
        public static string DefaultZone = string.Empty;
        public static string WarehouseReq = string.Empty;
        public static string BalanceMachines = string.Empty;
        public static string BalanceMachinesRetail = string.Empty;
        public static string Disposition = string.Empty;
        public static string timeOutRollSave = string.Empty;
        public static string initialConsecTagId = string.Empty;
        public static string usershoplogix = string.Empty;
        public static string passshoplogix = string.Empty;
        public static string userImpersonation = string.Empty;
        public static string passImpersonation = string.Empty;
        public static string domaImpersonation = string.Empty;

        //Ruta donde se guardan documentos de calidad-->
        //public static string SaveDocQuality = string.Empty;
        //Url base para armar menu-->
        //<add key=UrlBase value=http://scolbogweb01dc:82/whusap//>-->
        //public static string UrlBase = string.Empty;
        //Bandera de anuncio automatico para formularios whInvAnucioOrd-->
        public static string anuncioAutomatico = string.Empty;
        //Tiempo en que se valida el cambio de contraseña-->
        public static string tiempoCambioContrasena = string.Empty;
        //Sitio que va a retornar a web antigua, convivencia de ambas versiones-->
        public static string sitioConRetorno = string.Empty;
        //Porcentaje utilizado en calculo para formulario /Migration/InvLabelsPalletTags y InvLabelsPalletTagsPartial-->
        public static string calcLabelPalletTag = string.Empty;
        //Porcentaje utilizado en calculo para formulario /Migration/InvAnuncioOrd-->
        public static string calcAnuncioOrd = string.Empty;
        //Calculo utilizado en formulario /Balance/whInvLabel-->
        public static string calcInvLabel = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            XmlDocument xmlD = new XmlDocument();
            xmlD.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
            bool verificacionCicloPaginacion = false;
            bool verificacionCicloActualizacion = false;
            foreach (XmlElement element in xmlD.DocumentElement)
            {
                if (element.Name.Equals("appSettings"))
                {
                    foreach (XmlNode node in element.ChildNodes)
                    {
                        if (node.Attributes != null)
                        {
                            switch (node.Attributes[0].Value)
                            {
                                case "CicloPaginacion":
                                    CicloPaginacion = node.Attributes[1].Value;
                                    break;
                                case "CicloActualizacion":
                                    CicloActualizacion = node.Attributes[1].Value;
                                    break;
                                case "numberRetryOnSave":
                                    numberRetryOnSave = node.Attributes[1].Value;
                                    break;
                                case "bodyColor":
                                    bodyColor = node.Attributes[1].Value;
                                    break;
                                case "owner":
                                    owner = node.Attributes[1].Value;
                                    break;
                                case "env":
                                    env = node.Attributes[1].Value;
                                    break;
                                case "InitialVector":
                                    InitialVector = node.Attributes[1].Value;
                                    break;
                                case "KeyAlgorithm":
                                    KeyAlgorithm = node.Attributes[1].Value;
                                    break;
                                case "envCol":
                                    envCol = node.Attributes[1].Value;
                                    break;
                                case "envt":
                                    envt = node.Attributes[1].Value;
                                    break;
                                case "cia":
                                    cia = node.Attributes[1].Value;
                                    break;
                                case "DefaultZone":
                                    DefaultZone = node.Attributes[1].Value;
                                    break;
                                case "WarehouseReq":
                                    WarehouseReq = node.Attributes[1].Value;
                                    break;
                                case "BalanceMachines":
                                    BalanceMachines = node.Attributes[1].Value;
                                    break;
                                case "BalanceMachinesRetail":
                                    BalanceMachinesRetail = node.Attributes[1].Value;
                                    break;
                                case "Disposition":
                                    Disposition = node.Attributes[1].Value;
                                    break;
                                case "timeOutRollSave":
                                    timeOutRollSave = node.Attributes[1].Value;
                                    break;
                                case "initialConsecTagId":
                                    initialConsecTagId = node.Attributes[1].Value;
                                    break;
                                case "usershoplogix":
                                    usershoplogix = node.Attributes[1].Value;
                                    break;
                                case "passshoplogix":
                                    passshoplogix = node.Attributes[1].Value;
                                    break;
                                case "userImpersonation":
                                    userImpersonation = node.Attributes[1].Value;
                                    break;
                                case "passImpersonation":
                                    passImpersonation = node.Attributes[1].Value;
                                    break;
                                case "domaImpersonation":
                                    domaImpersonation = node.Attributes[1].Value;
                                    break;
                                case "anuncioAutomatico":
                                    anuncioAutomatico = node.Attributes[1].Value;
                                    break;
                                case "tiempoCambioContrasena":
                                    tiempoCambioContrasena = node.Attributes[1].Value;
                                    break;
                                case "sitioConRetorno":
                                    sitioConRetorno = node.Attributes[1].Value;
                                    break;
                                case "calcLabelPalletTag":
                                    calcLabelPalletTag = node.Attributes[1].Value;
                                    break;
                                case "calcAnuncioOrd":
                                    calcAnuncioOrd = node.Attributes[1].Value;
                                    break;
                                case "calcInvLabel":
                                    calcInvLabel = node.Attributes[1].Value;
                                    break;

                            }
                        }

                    }
                }
            }
        }

        [WebMethod]
        public static string ClickSave(string PCicloPaginacion,
                                        string PCicloActualizacion,
                                        string PnumberRetryOnSave,
                                        string PbodyColor,
                                        string Powner ,
                                        string Penv ,
                                        string PInitialVector ,
                                        string PKeyAlgorithm ,
                                        string PenvCol ,
                                        string Penvt ,
                                        string Pcia ,
                                        string PDefaultZone ,
                                        string PWarehouseReq ,
                                        string PBalanceMachines ,
                                        string PBalanceMachinesRetail ,
                                        string PDisposition ,
                                        string PtimeOutRollSave ,
                                        string PinitialConsecTagId ,
                                        string Pusershoplogix ,
                                        string Ppassshoplogix ,
                                        string PuserImpersonation ,
                                        string PpassImpersonation ,
                                        string PdomaImpersonation ,
                                        string PanuncioAutomatico ,
                                        string PtiempoCambioContrasena ,
                                        string PsitioConRetorno ,
                                        string PcalcLabelPalletTag ,
                                        string PcalcAnuncioOrd ,
                                        string PcalcInvLabel 

            )
        {


            XmlDocument xmlD = new XmlDocument();
            xmlD.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
            bool verificacionCicloPaginacion = false;
            bool verificacionCicloActualizacion = false;
            foreach (XmlElement element in xmlD.DocumentElement)
            {
                if (element.Name.Equals("appSettings"))
                {
                    foreach (XmlNode node in element.ChildNodes)
                    {
                        if (node.Attributes != null)
                        {
                            switch (node.Attributes[0].Value)
                            {
                                case "CicloPaginacion":
                                    node.Attributes[1].Value = PCicloPaginacion;
                                    break;
                                case "CicloActualizacion":
                                    node.Attributes[1].Value = PCicloActualizacion;
                                    break;
                                case "numberRetryOnSave":
                                    node.Attributes[1].Value = PnumberRetryOnSave;
                                    break;
                                case "bodyColor":
                                    node.Attributes[1].Value = PbodyColor;
                                    break;
                                case "owner":
                                    node.Attributes[1].Value = Powner;
                                    break;
                                case "env":
                                    node.Attributes[1].Value = Penv;
                                    break;
                                case "InitialVector":
                                    node.Attributes[1].Value = PInitialVector;
                                    break;
                                case "KeyAlgorithm":
                                    node.Attributes[1].Value = PKeyAlgorithm;
                                    break;
                                case "envCol":
                                    node.Attributes[1].Value = PenvCol;
                                    break;
                                case "envt":
                                    node.Attributes[1].Value = Penvt;
                                    break;
                                case "cia":
                                    node.Attributes[1].Value = Pcia;
                                    break;
                                case "DefaultZone":
                                    node.Attributes[1].Value = PDefaultZone;
                                    break;
                                case "WarehouseReq":
                                    node.Attributes[1].Value = PWarehouseReq;
                                    break;
                                case "BalanceMachines":
                                    node.Attributes[1].Value = PBalanceMachines;
                                    break;
                                case "BalanceMachinesRetail":
                                    node.Attributes[1].Value = PBalanceMachinesRetail;
                                    break;
                                case "Disposition":
                                    node.Attributes[1].Value = PDisposition;
                                    break;
                                case "timeOutRollSave":
                                    node.Attributes[1].Value = PtimeOutRollSave;
                                    break;
                                case "initialConsecTagId":
                                    node.Attributes[1].Value = PinitialConsecTagId;
                                    break;
                                case "usershoplogix":
                                    node.Attributes[1].Value = Pusershoplogix;
                                    break;
                                case "passshoplogix":
                                    node.Attributes[1].Value = Ppassshoplogix;
                                    break;
                                case "userImpersonation":
                                    node.Attributes[1].Value = PuserImpersonation;
                                    break;
                                case "passImpersonation":
                                    node.Attributes[1].Value = PpassImpersonation;
                                    break;
                                case "domaImpersonation":
                                    node.Attributes[1].Value = PdomaImpersonation;
                                    break;
                                case "anuncioAutomatico":
                                    node.Attributes[1].Value = PanuncioAutomatico;
                                    break;
                                case "tiempoCambioContrasena":
                                    node.Attributes[1].Value = PtiempoCambioContrasena;
                                    break;
                                case "sitioConRetorno":
                                    node.Attributes[1].Value = PsitioConRetorno;
                                    break;
                                case "calcLabelPalletTag":
                                    node.Attributes[1].Value = PcalcLabelPalletTag;
                                    break;
                                case "calcAnuncioOrd":
                                    node.Attributes[1].Value = PcalcAnuncioOrd;
                                    break;
                                case "calcInvLabel":
                                    node.Attributes[1].Value = PcalcInvLabel;
                                    break;
                            }
                        }
                    }
                    xmlD.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
                    ConfigurationManager.RefreshSection("appSettings");
                }
            }
            return "";
        }
    }
}