using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using whusa.Interfases;
using System.Web.Services;
using whusa.Entidades;
using System.Data;
using whusa;
using Newtonsoft.Json;
using whusa.Utilidades;

namespace whusap.WebPages.WorkOrders
{
    public partial class EditUser : System.Web.UI.Page
    {
        public static IntefazDAL_ttccol307 Ittccol307 = new IntefazDAL_ttccol307();
        public static InterfazDAL_ttccol300 Ittccol300 = new InterfazDAL_ttccol300();
        public static Ent_ttccol307 ObjReturn = new Ent_ttccol307();

        private static string globalMessages = "GlobalMessages";
        public static string ThepalletIDdoestexist = mensajes("ThepalletIDdoestexist");
        public static string Thecurrentusercannotbethesameastheprevioususer = mensajes("Thecurrentusercannotbethesameastheprevioususer");
        public static string Updtatesuccessfull = mensajes("Updtatesuccessfull");
        public static string Updtatenotsuccessfull = mensajes("Updtatenotsuccessfull");
        public static string Theusertoupdatenotexist = mensajes("Theusertoupdatenotexist");

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static string ConsultarTtccol307(string PAID)
        {

            DataTable Lsttccol307 = Ittccol307.ConsultarRegistrotccol307(new Ent_ttccol307 { PAID = PAID.Trim().ToUpper() });
            if (Lsttccol307.Rows.Count > 0)
            {
                DataRow ItemRow = Lsttccol307.Rows[0];
                ObjReturn.PAID = ItemRow["PAID"].ToString();
                ObjReturn.USRR = ItemRow["USSR"].ToString();
                ObjReturn.STAT = ItemRow["STAT"].ToString();
                ObjReturn.Error = false;
            }
            else
            {
                ObjReturn.Error = true;
                ObjReturn.ErrorMsg = ThepalletIDdoestexist;
                ObjReturn.TypeMsgJs = "lbl";
            }

            return JsonConvert.SerializeObject(ObjReturn);
        }

        [WebMethod]
        public static string ActualizarUsuarioTtccol307(string PAID, string USER, string USERO)
        {

            if (USER.Trim().ToUpper() == USERO.Trim().ToUpper())
            {
                ObjReturn.Error = true;
                ObjReturn.ErrorMsg = Thecurrentusercannotbethesameastheprevioususer;
                ObjReturn.TypeMsgJs = "lbl";
                return JsonConvert.SerializeObject(ObjReturn);
            }

            bool ExisteUsusario = ConsulrarExistenciaUsuario(new Ent_ttccol300 { user = USER.Trim().ToUpper() });
            if (ExisteUsusario)
            {
                bool ActualizacionExitosa = Ittccol307.ActualizarUsuariotccol307(new Ent_ttccol307 { PAID = PAID.Trim().ToUpper(), USRR = USER.Trim().ToUpper() });
                if (ActualizacionExitosa)
                {
                    ObjReturn.Error = false;
                    ObjReturn.SuccessMsg = Updtatesuccessfull;
                    ObjReturn.TypeMsgJs = "lbl";
                }
                else if (!ActualizacionExitosa)
                {
                    ObjReturn.Error = true;
                    ObjReturn.ErrorMsg = Updtatenotsuccessfull;
                    ObjReturn.TypeMsgJs = "lbl";
                }
            }
            else if (!ExisteUsusario)
            {
                ObjReturn.Error = true;
                ObjReturn.ErrorMsg = Theusertoupdatenotexist;
                ObjReturn.TypeMsgJs = "lbl";
            }

            return JsonConvert.SerializeObject(ObjReturn);
        }

        private static bool ConsulrarExistenciaUsuario(Ent_ttccol300 ttccol300)
        {
            bool ExisteUsuario = false;
            string StrError = string.Empty;
            DataTable ListaUsuarios = Ittccol300.listaRegistro_Param(ref ttccol300, ref StrError);
            if (ListaUsuarios.Rows.Count > 0) { ExisteUsuario = true; } else { ExisteUsuario = false; };
            return ExisteUsuario;
        }

        protected static string mensajes(string tipoMensaje)
        {
            Mensajes mensajesForm = new Mensajes();
            string idioma = "INGLES";
            var retorno = mensajesForm.readStatement("EditUser.aspx", idioma, ref tipoMensaje);

            if (retorno.Trim() == String.Empty)
            {
                retorno = mensajesForm.readStatement(globalMessages, idioma, ref tipoMensaje);
            }

            return retorno;
        }

    }
}