using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using whusa.Interfases;
using System.Data;
using Newtonsoft.Json;
using whusa.Entidades;
using whusa.Utilidades;

namespace whusap.WebPages.WorkOrders
{
    public partial class DropPickedMaterialMFG : System.Web.UI.Page
    {
        public static Ent_tticol082 MyObj = new Ent_tticol082();
        public static IntefazDAL_tticol082 Itticol082 = new IntefazDAL_tticol082();

        private static string globalMessages = "GlobalMessages";

        public static string Thepickedissuccess = mensajes("Thepickedissuccess");
        public static string Thepickedisnotsuccess = mensajes("Thepickedisnotsuccess");
        public static string ThePalletIDDoesntexist = mensajes("ThePalletIDDoesntexist");
        public static string PalletIDnotvalidfortaketoMFG = mensajes("PalletIDnotvalidfortaketoMFG");


        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static string ClickDropTagPick(string PalletID)
        {
            DataTable TableItticol082 = Itticol082.ConsultarPalletIDTticol083(PalletID);
            string ObjRetorno = string.Empty;

            if (ExistenciaData(TableItticol082))
            {
                DataRow myObjDt = TableItticol082.Rows[0];
                MyObj.TBL = myObjDt["TBL"].ToString();
                MyObj.PAID = myObjDt["PAID"].ToString();
                MyObj.QTYT = myObjDt["QTYT"].ToString();
                MyObj.UNIT = myObjDt["UNIT"].ToString();
                MyObj.ITEM = myObjDt["ITEM"].ToString();
                MyObj.DSCA = myObjDt["DSCA"].ToString();

                bool ActalizacionExitosa = false;
                switch (MyObj.TBL)
                {
                    case "ticol022":
                        ActalizacionExitosa = Itticol082.Actualizartticol022MFG(MyObj);
                        Itticol082.Actualizartticol083MFG(MyObj);                        
                        break;
                    case "ticol222":
                        ActalizacionExitosa = Itticol082.Actualizartticol222MFG(MyObj);
                        Itticol082.Actualizartticol083MFG(MyObj);                        
                        break;
                    case "ticol242":
                        ActalizacionExitosa = Itticol082.Actualizartticol242MFG(MyObj);
                        Itticol082.Actualizartticol083MFG(MyObj);                        
                        break;
                    case "ticol082":
                        ActalizacionExitosa = Itticol082.Actualizartticol082MFG(MyObj);
                        Itticol082.Actualizartticol083MFG(MyObj);                        
                        break;
                    case "whcol130":
                        ActalizacionExitosa = Itticol082.Actualizartwhcol130MFG(MyObj);
                        Itticol082.Actualizartticol083MFG(MyObj);                        
                        break;
                    case "whcol131":
                        ActalizacionExitosa = Itticol082.Actualizartwhcol131MFG(MyObj);
                        Itticol082.Actualizartticol083MFG(MyObj);                        

                        break;
                    case "ticol042":
                        ActalizacionExitosa = Itticol082.Actualizartticol042MFG(MyObj);
                        Itticol082.Actualizartticol083MFG(MyObj);                        
                        break;
                }
                if (ActalizacionExitosa)
                {
                    MyObj.Error = false;
                    MyObj.TipeMsgJs = "alert";
                    MyObj.SuccessMsg = Thepickedissuccess;
                    ObjRetorno = JsonConvert.SerializeObject(MyObj);
                }
                else
                {
                    MyObj.Error = true;
                    MyObj.TipeMsgJs = "alert";
                    MyObj.ErrorMsg = Thepickedisnotsuccess;
                    ObjRetorno = JsonConvert.SerializeObject(MyObj);
                }
            }
            else
            {
                MyObj.Error = true;
                MyObj.TipeMsgJs = "alert";
                MyObj.ErrorMsg = ThePalletIDDoesntexist;

                ObjRetorno = JsonConvert.SerializeObject(MyObj);
            }
            return ObjRetorno;
        }

        [WebMethod]
        public static string SearchPalletID(string PalletID)
        {

            DataTable TableItticol082 = Itticol082.ConsultarPalletIDTticol082MFG(PalletID);
            string ObjRetorno = string.Empty;

            if (ExistenciaData(TableItticol082))
            {
                DataRow myObjDt = TableItticol082.Rows[0];

                MyObj.TBL = myObjDt["TBL"].ToString();
                MyObj.PAID = myObjDt["PAID"].ToString();
                MyObj.QTYT = myObjDt["QTYT"].ToString();
                MyObj.UNIT = myObjDt["UNIT"].ToString();
                MyObj.ITEM = myObjDt["ITEM"].ToString();
                MyObj.DSCA = myObjDt["DSCA"].ToString();
                MyObj.MCNO = myObjDt["MCNO"].ToString();
                MyObj.ORNO = myObjDt["ORNO"].ToString();
                MyObj.STAT = myObjDt["STAT"].ToString();
               
                if (VerificarStatPortabla(MyObj.TBL, MyObj.STAT))
                {
                    MyObj.Error = false;

                    ObjRetorno = JsonConvert.SerializeObject(MyObj);
                }
                else
                {
                    MyObj.Error = true;
                    MyObj.TipeMsgJs = "lbl";
                    MyObj.ErrorMsg = PalletIDnotvalidfortaketoMFG;
                    ObjRetorno = JsonConvert.SerializeObject(MyObj);
                }

            }
            else
            {
                MyObj.Error = true;
                MyObj.TipeMsgJs = "alert";
                MyObj.ErrorMsg = ThePalletIDDoesntexist;
                ObjRetorno = JsonConvert.SerializeObject(MyObj);
            }
            return ObjRetorno;
        }

        public static bool ExistenciaData(DataTable Data)
        {
            bool ContieneDatos = false;
            if (Data.Rows.Count > 0)
            {
                ContieneDatos = true;
            }
            return ContieneDatos;
        }

        public static bool VerificarStatPortabla(string tabla, string estado)
        {
            bool retorno = false;
            switch (tabla)
            {
                case "ticol022":
                    if (estado == "11") { retorno = true; } else { retorno = false; };
                    break;
                case "ticol042":
                    if (estado == "11") { retorno = true; } else { retorno = false; };

                    break;
                case "whcol130":
                    if (estado == "9") { retorno = true; } else { retorno = false; };
                    break;
                case "whcol131":
                    if (estado == "9") { retorno = true; } else { retorno = false; };

                    break;
            }
            return retorno;
        }

        protected static string mensajes(string tipoMensaje)
        {
            Mensajes mensajesForm = new Mensajes();
            string idioma = "INGLES";
            var retorno = mensajesForm.readStatement("DropPickedMaterialMFG.aspx", idioma, ref tipoMensaje);

            if (retorno.Trim() == String.Empty)
            {
                retorno = mensajesForm.readStatement(globalMessages, idioma, ref tipoMensaje);
            }

            return retorno;
        }


    }
}