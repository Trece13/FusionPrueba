﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using whusa.Interfases;
using System.Threading;
using System.Web.Services;
using whusa.Entidades;
using whusa;
using System.Data;
using System.Globalization;
using System.Web.Configuration;
using Newtonsoft.Json;
using System.Configuration;
using whusa.Utilidades;

namespace whusap.WebPages.InvReceipts
{
    public partial class InventoryTakingByPalletID : System.Web.UI.Page
    {
        public static string RequestUrlAuthority = string.Empty;
        //private static LabelsText _textoLabels = new LabelsText();
        string formName = string.Empty;
        public static string _operator = string.Empty;
        string _idioma = string.Empty;
        public static string PCLOT = string.Empty;
        private static string globalMessages = "GlobalMessages";
        public static string PalletIDdoesntexist = mensajes("PalletIDdoesntexist");
        public static string ItemcodeisnotPurchaseType = mensajes("ItemcodeisnotPurchaseType");
        public static string Lotcodedoesntexist = mensajes("Lotcodedoesntexist");
        public static string Warehousecodedoesntexist = mensajes("Warehousecodedoesntexist");
        public static string Locationblockedinbound = mensajes("Locationblockedinbound");
        public static string Locationcodedoesntexist = mensajes("Locationcodedoesntexist");
        public static string RegisteredquantitynotavilableonBaaninventory = mensajes("RegisteredquantitynotavilableonBaaninventory");

        public static string UrlBaseBarcode = WebConfigurationManager.AppSettings["UrlBaseBarcode"].ToString();
        private static InterfazDAL_tticol022 _idaltticol022 = new InterfazDAL_tticol022();
        private static InterfazDAL_twhcol019 _idaltwhcol019 = new InterfazDAL_twhcol019();
        private static InterfazDAL_ttccol301 _idalttccol301 = new InterfazDAL_ttccol301();
        public static InterfazDAL_twhcol130 twhcol130DAL = new InterfazDAL_twhcol130();
        public static InterfazDAL_ttcibd001 ITtcibd001 = new InterfazDAL_ttcibd001();
        public static InterfazDAL_tticol125 ITticol125 = new InterfazDAL_tticol125();
        public static InterfazDAL_ttwhcol016 ITtwhcol016 = new InterfazDAL_ttwhcol016();
        public static InterfazDAL_twhwmd200 ITwhwmd200 = new InterfazDAL_twhwmd200();
        public static IntefazDAL_transfer Itransfer = new IntefazDAL_transfer();
        public static InterfazDAL_twhinr140 ITtwhinr140 = new InterfazDAL_twhinr140();

        protected void Page_Load(object sender, EventArgs e)
        {
            RequestUrlAuthority = (string)Request.Url.Authority;


            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-CO");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("es-CO");
            base.InitializeCulture();

            if (!IsPostBack)
            {
                formName = Request.Url.AbsoluteUri.Split('/').Last();

                if (formName.Contains('?'))
                {
                    formName = formName.Split('?')[0];
                }
                Label control = (Label)Page.Controls[0].FindControl("lblPageTitle");
                if (Session["user"] == null)
                {
                    Response.Redirect(ConfigurationManager.AppSettings["UrlBase"] + "/WebPages/Login/whLogIni.aspx");
                }

                _operator = Session["user"].ToString();

                try
                {
                    _idioma = Session["ddlIdioma"].ToString();
                }
                catch (Exception)
                {
                    _idioma = "INGLES";
                }

                string strError = string.Empty;

                Ent_ttccol301 data = new Ent_ttccol301()
                {
                    user = _operator,
                    come = "",
                    refcntd = 0,
                    refcntu = 0
                };

                List<Ent_ttccol301> datalog = new List<Ent_ttccol301>();
                datalog.Add(data);

                _idalttccol301.insertarRegistro(ref datalog, ref strError);
            }
        }

        [WebMethod]
        public static string VerificarPalletID(string PAID)
        {
            string strError = string.Empty;
            DataTable DTPalletID = twhcol130DAL.VerificarPalletID(ref PAID);
            EntidadPicking ObjPicking = new EntidadPicking();

            if (DTPalletID.Rows.Count > 0)
            {
                ObjPicking.PALLETID = DTPalletID.Rows[0]["PAID"].ToString();
                ObjPicking.ITEM = DTPalletID.Rows[0]["ITEM"].ToString();
                ObjPicking.DESCRIPTION = DTPalletID.Rows[0]["DSCA"].ToString();
                ObjPicking.LOT = DTPalletID.Rows[0]["DSCA"].ToString();
                ObjPicking.WRH = DTPalletID.Rows[0]["CWAT"].ToString();
                ObjPicking.DESCWRH = DTPalletID.Rows[0]["DESCAW"].ToString();
                ObjPicking.LOCA = DTPalletID.Rows[0]["ACLO"].ToString();
                ObjPicking.QTY = DTPalletID.Rows[0]["QTYT"].ToString();
                ObjPicking.UN = DTPalletID.Rows[0]["UNIT"].ToString();
                ObjPicking.STAT = DTPalletID.Rows[0]["STAT"].ToString();
                //PDNO, SQNB, MITM, DSCA, CUNI, QTDL, DELE, PRO1, PROC
                ObjPicking.error = false;

                //if (DTPalletID.Rows[0]["STAT"].ToString() != "7")
                //{
                //    ObjPicking.error = true;
                //    ObjPicking.typeMsgJs = "label";
                //    ObjPicking.errorMsg = "Pallet ID doesn´t exist";
                //}
            }
            else
            {
                ObjPicking.error = true;
                ObjPicking.typeMsgJs = "label";
                
                ObjPicking.errorMsg = PalletIDdoesntexist;
            }


            return JsonConvert.SerializeObject(ObjPicking);
        }

        public static string VerificarItem(string ITEM,string CLOT)
        {

            Ent_ttcibd001 ObjTtcibd001 = new Ent_ttcibd001();

            DataTable dtTtcibd001 = ITtcibd001.findItem(ITEM);
            if (dtTtcibd001.Rows.Count > 0)
            {
                ObjTtcibd001.item = dtTtcibd001.Rows[0]["ITEM"].ToString();
                ObjTtcibd001.dsca = dtTtcibd001.Rows[0]["DSCA"].ToString();
                ObjTtcibd001.cuni = dtTtcibd001.Rows[0]["CUNI"].ToString();
                ObjTtcibd001.kltc = dtTtcibd001.Rows[0]["KLTC"].ToString();
                ObjTtcibd001.kitm = dtTtcibd001.Rows[0]["KITM"].ToString();

                if (ObjTtcibd001.kltc.Trim() == "1")
                {
                    PCLOT = CLOT;
                }

                if (ObjTtcibd001.kitm.Trim() == "1")
                {
                    ObjTtcibd001.Error = true;
                    ObjTtcibd001.TypeMsgJs = "label";
                    
                    ObjTtcibd001.ErrorMsg = ItemcodeisnotPurchaseType;
                }
                else
                {
                    ObjTtcibd001.Error = false;
                    ObjTtcibd001.TypeMsgJs = "console";
                    ObjTtcibd001.SuccessMsg = "Item Encontrado";
                }
            }
            else
            {
                ObjTtcibd001.Error = true;
                ObjTtcibd001.TypeMsgJs = "label";
                
                ObjTtcibd001.ErrorMsg = ItemcodeisnotPurchaseType;
            }



            return JsonConvert.SerializeObject(ObjTtcibd001);
        }


        [WebMethod]
        public static string VerificarLote(string ITEM, string CLOT)
        {
            string strError = string.Empty;

            Ent_tticol125 Obj_tticol125 = new Ent_tticol125();
            Obj_tticol125.item = ITEM;
            Obj_tticol125.clot = CLOT;

            DataTable DtTticol125 = ITticol125.listaRegistrosLoteItem_Param(ref Obj_tticol125);

            if (DtTticol125.Rows.Count > 0)
            {
                Obj_tticol125.Error = false;
                Obj_tticol125.TypeMsgJs = "console";
                Obj_tticol125.SuccessMsg = "Lote Encontrado";
            }
            else
            {
                Obj_tticol125.Error = true;
                Obj_tticol125.TypeMsgJs = "label";
                
                Obj_tticol125.ErrorMsg = Lotcodedoesntexist;

            }

            return JsonConvert.SerializeObject(Obj_tticol125);


        }
        [WebMethod]
        public static string VerificarWarehouse(string CWAR)
        {

            string strError = string.Empty;

            Ent_ttwhcol016 Obj_twhcol016 = new Ent_ttwhcol016();
            Obj_twhcol016.cwar = CWAR;

            Ent_twhwmd200 Obj_twhwmd200 = new Ent_twhwmd200();
            Obj_twhwmd200.cwar = CWAR;

            DataTable DtTtwhcol016 = ITtwhcol016.TakeMaterialInv_verificaBodega_Param(ref Obj_twhcol016, ref strError);
            DataTable DtTwhwmd200 = ITwhwmd200.listaRegistro_ObtieneAlmacenLocation(ref Obj_twhwmd200, ref strError);

            if (DtTtwhcol016.Rows.Count > 0)
            {
                Obj_twhcol016.Error = false;
                Obj_twhcol016.TypeMsgJs = "console";
                Obj_twhcol016.SuccessMsg = "Warehouse Encontrado";

                if (DtTwhwmd200.Rows.Count > 0)
                {
                    Obj_twhcol016.sloc = DtTwhwmd200.Rows[0]["LOC"].ToString();
                }
                else
                {
                    Obj_twhcol016.sloc = string.Empty;
                }
            }
            else
            {
                Obj_twhcol016.Error = true;
                Obj_twhcol016.TypeMsgJs = "label";
                
                Obj_twhcol016.ErrorMsg = Warehousecodedoesntexist;
            }

            return JsonConvert.SerializeObject(Obj_twhcol016);
        }

        [WebMethod]
        public static string VerificarLocation(string CWAR, string LOCA)
        {


            string strError = string.Empty;
            Ent_twhwmd200 Obj_twhwmd200 = new Ent_twhwmd200();
            Obj_twhwmd200.cwar = CWAR;

            DataTable DtTransfer = Itransfer.ConsultarLocation(Obj_twhwmd200.cwar, LOCA);

            if (DtTransfer.Rows.Count > 0)
            {
                if (DtTransfer.Rows[0]["LOCT"].ToString() == "5")
                {
                    if (DtTransfer.Rows[0]["BINB"].ToString() == "2")
                    {
                        Obj_twhwmd200.Error = false;
                        Obj_twhwmd200.TypeMsgJs = "console";
                        Obj_twhwmd200.SuccessMsg = "Location Encontrado";
                    }
                    else
                    {
                        Obj_twhwmd200.Error = true;
                        Obj_twhwmd200.TypeMsgJs = "label";
                        
                        Obj_twhwmd200.ErrorMsg = Locationblockedinbound;
                    }
                }
                else
                {
                    Obj_twhwmd200.Error = true;
                    Obj_twhwmd200.TypeMsgJs = "label";
                    
                    Obj_twhwmd200.ErrorMsg = Locationcodedoesntexist;
                }

            }
            else
            {
                Obj_twhwmd200.Error = true;
                Obj_twhwmd200.TypeMsgJs = "label";
                
                Obj_twhwmd200.ErrorMsg = Locationcodedoesntexist;
            }

            return JsonConvert.SerializeObject(Obj_twhwmd200);


        }
        [WebMethod]
        public static string VerificarQuantity(string CWAR, string ITEM,string LOCA = " ", string CLOT = " ")
        {
            string strError = string.Empty;
            DataTable DtTtwhinr140 = ITtwhinr140.consultaPorAlmacenItemUbicacionLote(ref CWAR, ref ITEM, ref LOCA, ref PCLOT, ref strError);
            Ent_twhinr140 ObjTtwhinr140 = new Ent_twhinr140();

            if (DtTtwhinr140.Rows.Count > 0)
            {
                if (Convert.ToInt32(DtTtwhinr140.Rows[0]["STKS"].ToString()) < 1)
                {
                    ObjTtwhinr140.Error = true;
                    ObjTtwhinr140.TypeMsgJs = "label";
                    
                    ObjTtwhinr140.ErrorMsg = RegisteredquantitynotavilableonBaaninventory;
                }
                else
                {
                    ObjTtwhinr140.stks = Convert.ToInt32(DtTtwhinr140.Rows[0]["STKS"].ToString());
                    ObjTtwhinr140.Error = false;
                    ObjTtwhinr140.TypeMsgJs = "label";
                    
                    ObjTtwhinr140.SuccessMsg = RegisteredquantitynotavilableonBaaninventory;
                }
                
            }
            else
            {
                ObjTtwhinr140.Error = true;
                ObjTtwhinr140.TypeMsgJs = "label";
                
                ObjTtwhinr140.ErrorMsg = RegisteredquantitynotavilableonBaaninventory;
            }

            return JsonConvert.SerializeObject(ObjTtwhinr140);

        }

        [WebMethod]
        public static string Click_Save(string PAID, string ITEM, string CWAR, string LOCA, string UNIT, string QTYS)
        {
            string ZONE = " ";
            string CLOT = " ";
            string strError = string.Empty;
            Ent_twhcol019 ObjTwhcol019 = new Ent_twhcol019();
            ObjTwhcol019.PAID   = PAID;   
            ObjTwhcol019.SQNB   = Convert.ToInt32(PAID.Substring(10,3));   
            ObjTwhcol019.ZONE   = ZONE;   
            ObjTwhcol019.CWAR   = CWAR;   
            ObjTwhcol019.LOCA   = LOCA;   
            ObjTwhcol019.ITEM   = ITEM;   
            ObjTwhcol019.CLOT   = CLOT;   
            ObjTwhcol019.QTDL   = Convert.ToDecimal(QTYS.ToString().Trim());   
            ObjTwhcol019.CUNI   = UNIT;   
            ObjTwhcol019.LOGN   = _operator;   
            ObjTwhcol019.DATE   = new DateTime();   
            ObjTwhcol019.COUN   = 0;   
            ObjTwhcol019.PROC   = 0;   
            ObjTwhcol019.REFCNTD= 0;        
            ObjTwhcol019.REFCNTU= 0;

            _idaltwhcol019.insertRegistertwhcol019(ref ObjTwhcol019, ref strError);
            return JsonConvert.SerializeObject(ObjTwhcol019);

        }

        protected static string mensajes(string tipoMensaje)
        {
            Mensajes mensajesForm = new Mensajes();
            string idioma = "INGLES";
            var retorno = mensajesForm.readStatement("InventoryTakingByPalletID.aspx", idioma, ref tipoMensaje);

            if (retorno.Trim() == String.Empty)
            {
                retorno = mensajesForm.readStatement(globalMessages, idioma, ref tipoMensaje);
            }

            return retorno;
        }
    }

}