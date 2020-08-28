using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using whusa.Interfases;
using whusa;
using Newtonsoft.Json;
using System.Data;
using whusa.Entidades;
using System.Globalization;
using System.Threading;
using System.Configuration;
using whusa.Utilidades;

namespace whusap.WebPages.InvReceipts
{
    public partial class LocateRawMaterial : System.Web.UI.Page
    {
        public static string ThePalletIDdoesnotexist = string.Empty;
        public static string TheLocationIsNotBulkType = string.Empty;
        public static string TheLocationIsLockedForInbound = string.Empty;
        public static string DeleteTccol307Error = string.Empty;
        public static string ThePalletHasAlreadyBeLocated = string.Empty;
        public static string UpdateTwhcol130Error = string.Empty;
        public static string TheCurrentLocate = string.Empty;
        public static string TheCurrentLocationCannotBeTheSameAsTheTargetLocation = string.Empty;
        public static string TheLocationDoesNotExist = string.Empty;
        public static string ThePalletHasAlreadyLocate = string.Empty;
        public static string ThePalletIDDoesDotExistAssociated = string.Empty;



        public static InterfazDAL_twhcol130 twhcol130DAL = new InterfazDAL_twhcol130();
        private static InterfazDAL_ttccol301 _idalttccol301 = new InterfazDAL_ttccol301();
        public static InterfazDAL_twhcol020 twhcol020DAL = new InterfazDAL_twhcol020();
        string formName = string.Empty;
        public static string _operator = string.Empty;
        string _idioma = string.Empty;
        private static Mensajes _mensajesForm = new Mensajes();
        private static string globalMessages = "GlobalMessages";

        protected void Page_Load(object sender, EventArgs e)
        {
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


                string strTitulo = "Locate Raw Material";
                control.Text = strTitulo;
                string strError = string.Empty;

                Ent_ttccol301 data = new Ent_ttccol301()
                {
                    user = _operator,
                    come = strTitulo,
                    refcntd = 0,
                    refcntu = 0
                };

                List<Ent_ttccol301> datalog = new List<Ent_ttccol301>();
                datalog.Add(data);

                _idalttccol301.insertarRegistro(ref datalog, ref strError);
                CargarIdioma();

            }
        }

        [WebMethod]
        public static string Click_Locate(string PAID, string CWAR, string LOCA)
        {
            HttpContext context = HttpContext.Current;

            try
            {
                Ent_twhcol130131 MyObj = new Ent_twhcol130131();
                DataTable LocationExist = twhcol130DAL.ConsultarLocation(PAID, CWAR, LOCA);

                if (LocationExist.Rows.Count > 0)
                {
                    if (LocationExist.Rows[0]["LOCT"].ToString() != "5")
                    {
                        MyObj.errorMsg = TheLocationIsNotBulkType;
                        MyObj.error = true;
                    }
                    else if (LocationExist.Rows[0]["BINB"].ToString() != "2")
                    {
                        MyObj.errorMsg = TheLocationIsLockedForInbound;
                        MyObj.error = true;
                    }
                    else if (LocationExist.Rows[0]["LOCT"].ToString() == "5" && LocationExist.Rows[0]["BINB"].ToString() == "2")
                    {
                        List<Ent_twhcol130131> LstPallet = twhcol130DAL.ConsultarPorPalletID(PAID, "", string.Empty);
                        if (LstPallet.Count > 0)
                        {
                            if (LstPallet[0].LOCA.Trim() != LOCA)
                            {
                                bool UpdateSucces = twhcol130DAL.ActualizacionLocaWhcol130(PAID, LOCA, CWAR, context.Session["user"].ToString(), "3");
                                if (UpdateSucces == true)
                                {
                                    LstPallet = twhcol130DAL.ConsultarPorPalletID(PAID, "", string.Empty);

                                    string errorTwhcol020 = string.Empty;

                                    string CLOT = LstPallet[0].ORNO;

                                    DataTable PrioridadNativa = twhcol130DAL.ConsultarPrioridadNativa(LstPallet[0].CWAR.Trim());
                                    DataTable LocationNativa = twhcol130DAL.ConsultarLocationNativa(LstPallet[0].CWAR.Trim(), PrioridadNativa.Rows[0]["T$PRIO"].ToString());


                                    Ent_twhcol020 MyObj_twhcol020 = new Ent_twhcol020
                                    {
                                        clot = CLOT.Trim(),
                                        sqnb = PAID,//ConsecutivoTwhcol020 + 1,
                                        mitm = LstPallet[0].ITEM.Trim(),
                                        dsca = LstPallet[0].DSCA,//Dato Quemado Por favor Cambiar
                                        cwor = LstPallet[0].CWAR.Trim(),
                                        loor = LocationNativa.Rows[0]["T$LOCA"].ToString(),//Dato Quemado Por favor Cambiar
                                        cwde = CWAR,//LstPallet[0].CWAR.Trim(),
                                        lode = LOCA,//LstPallet[0].LOCA.Trim(),
                                        qtdl = Convert.ToDouble(LstPallet[0].QTYC.ToString()),
                                        cuni = LstPallet[0].UNIC.Trim(),
                                        date = DateTime.Now.ToString("dd / MM / aaaa"),
                                        mess = string.Empty,
                                        user = context.Session["user"].ToString(),
                                        refcntd = 0,
                                        refcntu = 0
                                    };



                                    string errorInsertTwhcol020 = string.Empty;

                                    int InsertSuccsesTwhcol020 = twhcol020DAL.insertarRegistro(ref MyObj_twhcol020, ref errorInsertTwhcol020);

                                    if (errorInsertTwhcol020 == string.Empty)
                                    {
                                        bool DeleteSucces = twhcol130DAL.Eliminartccol307(LstPallet[0].PAID, _operator);
                                        if (DeleteSucces == true)
                                        {
                                            MyObj = LstPallet[0];
                                        }
                                        else
                                        {
                                            MyObj.errorMsg = DeleteTccol307Error;
                                            MyObj.error = true;

                                        }
                                    }
                                    else
                                    {
                                        MyObj.errorMsg = ThePalletHasAlreadyBeLocated;
                                        MyObj.error = true;

                                    }

                                }
                                else
                                {
                                    MyObj.errorMsg = UpdateTwhcol130Error;
                                    MyObj.error = true;
                                }
                            }
                            else
                            {
                                MyObj.errorMsg = TheCurrentLocate;
                                MyObj.error = true;
                            }
                        }
                        else
                        {
                            MyObj.errorMsg = TheCurrentLocationCannotBeTheSameAsTheTargetLocation;
                            MyObj.error = true;
                        }
                    }
                }
                else
                {
                    List<Ent_twhcol130131> LstPallet = twhcol130DAL.ConsultarPorPalletID(PAID, string.Empty, _operator);

                    if (LstPallet.Count > 0)
                    {
                        MyObj = LstPallet[0];
                    };

                    if (MyObj.SLOC != "1")
                    {

                        bool UpdateSucces = twhcol130DAL.ActualizacionLocaWhcol130(PAID, LOCA = " ", CWAR, context.Session["user"].ToString(), "3");
                        if (UpdateSucces == true)
                        {
                            LstPallet = twhcol130DAL.ConsultarPorPalletID(PAID, "", string.Empty);
                            if (LstPallet.Count > 0)
                            {
                                MyObj = LstPallet[0];
                            }

                            bool Insert131 = twhcol130DAL.InsertarReseiptRawMaterial131(MyObj);

                            if (Insert131)
                            {
                                bool Delete130 = twhcol130DAL.Eliminartccol130(MyObj);
                            }

                            bool DeleteSucces = twhcol130DAL.Eliminartccol307(MyObj.PAID, _operator);
                            if (DeleteSucces == true)
                            {
                                MyObj = LstPallet[0];
                            }
                            else
                            {
                                MyObj.errorMsg = DeleteTccol307Error;
                                MyObj.error = true;
                            }

                        }
                        else
                        {
                            MyObj.errorMsg = UpdateTwhcol130Error;
                            MyObj.error = true;
                        }
                    }
                    else
                    {
                        MyObj.errorMsg = TheLocationDoesNotExist;
                        MyObj.error = true;
                    }
                }
                return JsonConvert.SerializeObject(MyObj);
            }
            catch (Exception e)
            {
                return ThePalletIDdoesnotexist;
            }
        }

        [WebMethod]
        public static string Click_Query(string PAID)
        {
            try
            {
                Ent_twhcol130131 MyObj = new Ent_twhcol130131();

                List<Ent_twhcol130131> LstPallet = twhcol130DAL.ConsultarPorPalletID(PAID, "", _operator);
                if (LstPallet.Count > 0)
                {
                    MyObj = LstPallet[0];

                    if (MyObj.LOGT.Trim() != string.Empty && MyObj.LOCA.Trim() != string.Empty)
                    {
                        MyObj.error = true;
                        MyObj.errorMsg = ThePalletHasAlreadyLocate;
                    }
                }
                else
                {
                    MyObj.error = true;
                    MyObj.errorMsg = ThePalletIDDoesDotExistAssociated;
                }

                return JsonConvert.SerializeObject(MyObj);
            }
            catch (Exception e)
            {
                return ThePalletIDdoesnotexist;
            }

        }



        protected void CargarIdioma()
        {
            ThePalletIDdoesnotexist = mensajes("ThePalletIDdoesnotexist");
            TheLocationIsNotBulkType = mensajes("TheLocationIsNotBulkType");
            TheLocationIsLockedForInbound = mensajes("TheLocationIsLockedForInbound");
            DeleteTccol307Error = mensajes("DeleteTccol307Error");
            ThePalletHasAlreadyBeLocated = mensajes("ThePalletHasAlreadyBeLocated");
            UpdateTwhcol130Error = mensajes("UpdateTwhcol130Error");
            TheCurrentLocate = mensajes("TheCurrentLocate");
            TheCurrentLocationCannotBeTheSameAsTheTargetLocation = mensajes("TheCurrentLocationCannotBeTheSameAsTheTargetLocation");
            DeleteTccol307Error = mensajes("DeleteTccol307Error");
            UpdateTwhcol130Error = mensajes("UpdateTwhcol130Error");
            TheLocationDoesNotExist = mensajes("TheLocationDoesNotExist");
            ThePalletHasAlreadyLocate = mensajes("ThePalletHasAlreadyLocate");
            ThePalletIDDoesDotExistAssociated = mensajes("ThePalletIDDoesDotExistAssociated");
        }

        protected string mensajes(string tipoMensaje)
        {
            var retorno = _mensajesForm.readStatement(formName, _idioma, ref tipoMensaje);

            if (retorno.Trim() == String.Empty)
            {
                retorno = _mensajesForm.readStatement(globalMessages, _idioma, ref tipoMensaje);
            }

            return retorno;
        }

    }

}


