using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using whusa.Interfases;
using System.Web.Services;
using whusa.Entidades;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using whusap.WebPages.InvReceipts;
using System.Data;
using System.Threading;
using System.Globalization;
using whusa.Utilidades;
using System.Configuration;
using whusa;


namespace whusap.WebPages.SalesOrders
{
    public partial class SalesOrders : System.Web.UI.Page
    {
        

        public static IntefazDAL_ttccom110 Ittccom110 = new IntefazDAL_ttccom110();
        public static IntefazDAL_twhinh220 Itwhinh220 = new IntefazDAL_twhinh220();
        public static IntefazDAL_tticol082 Itticol082 = new IntefazDAL_tticol082();
        public static InterfazDAL_twhcol130 Itwhcol130 = new InterfazDAL_twhcol130();
        private static InterfazDAL_ttccol301 _idalttccol301 = new InterfazDAL_ttccol301();

        public static string InvalidCustomerCode = string.Empty;

        string formName = string.Empty;
        public static string _operator = string.Empty;
        string  _idioma = string.Empty;
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


                string strTitulo = string.Empty;//mensajes("encabezado");
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
        public static string ClickQuery(string Customer, string ToDate)
        {
            Ent_twhinh220 Objtwhinh220 = new Ent_twhinh220
            {
                STAD = Customer,
                PDDT = ToDate
            };
            string strError = string.Empty;
            DataTable ListaRegistroCustomer = Itwhinh220.TraerOrdenesCustomer(Objtwhinh220,ref strError);
            if (strError == string.Empty)
            {
                return JsonConvert.SerializeObject(ListaRegistroCustomer);
            }
            else
            {
                return strError;
            }

           
            
        }

        

        private static int CantidadPorCiclo(decimal QCUNI, int QFAC, int CICLOS, int CICLOACTUAL)
        {
            if (QCUNI % QFAC == 0)
            {

                return QFAC;

            }
            else
            {
                if (CICLOS == CICLOACTUAL)
                {

                    return Convert.ToInt32(Math.Ceiling(Math.Round(QCUNI, 1)) % QFAC);


                }
                else
                {
                    return QFAC;
                }

            }
        }

        [WebMethod]
        public static string KeyPressCustomer(string Customer)
        {


            Ent_ttccom110 ObjTtccom110 = new Ent_ttccom110
            {
                BPID = Customer.ToUpper().Trim()
            };

            DataTable ExistenciaCustomer = Ittccom110.VerificarExistenciaCustomer(ObjTtccom110);

            if (ExistenciaCustomer.Rows.Count > 0)
            {
                ObjTtccom110.BPID = ExistenciaCustomer.Rows[0]["BPID"].ToString().Trim();
                ObjTtccom110.NAMA = ExistenciaCustomer.Rows[0]["NAMA"].ToString().Trim();
            }
            else
            {
                ObjTtccom110.Error = true;
                ObjTtccom110.ErrorMsg = "Invalid customer code";//mensajes("InvalidCustomerCode");
                ObjTtccom110.TipeMsgJs = "lbl";

            }

            return JsonConvert.SerializeObject(ObjTtccom110);
        }

        public static Factor FactorConversion(string ITEM, string STUN, string CUNI)
        {
            Factor MyFactor = new Factor
            {
                MsgError = "No Tiene Factor",//mensajes("ItHasNoFactor"),
                FactorD = null,
                Tipo = string.Empty
            };

            DataTable DtFactor = new DataTable();
            DataTable ConvercionDiv = Itwhcol130.FactorConvercionMul(ITEM, CUNI, STUN);
            DataTable ConvercionMul = Itwhcol130.FactorConvercionDiv(ITEM, CUNI, STUN);

            if (ConvercionDiv.Rows.Count > 0)
            {
                MyFactor.MsgError = string.Empty;
                MyFactor.Tipo = "Div";
                MyFactor.FactorD = (decimal?)ConvercionDiv.Rows[0]["FACTOR"];
                MyFactor.FactorB = (decimal?)ConvercionDiv.Rows[0]["POTENCIA"];
            }
            else if (ConvercionMul.Rows.Count > 0)
            {
                MyFactor.MsgError = string.Empty;
                MyFactor.Tipo = "Mul";
                MyFactor.FactorD = (decimal?)ConvercionMul.Rows[0]["FACTOR"];
                MyFactor.FactorB = (decimal?)ConvercionMul.Rows[0]["POTENCIA"];
            }
            else if (ConvercionDiv.Rows.Count == 0 && ConvercionMul.Rows.Count == 0)
            {
                MyFactor = FactorConversion(string.Empty, STUN, CUNI);
                return MyFactor;
            }

            return MyFactor;

        }


        [WebMethod]
        public static string ClickSave(string LstJson)
        {

            List<Ent_twhinh220> lstGuardar = JsonConvert.DeserializeObject<List<Ent_twhinh220>>(LstJson);
            List<bool> ListInsertResult = new List<bool>(); 

            if (lstGuardar.Count > 0)
            {

                
                foreach (Ent_twhinh220 myObj in lstGuardar)
                {
                    Factor MyConvertionFactor = new Factor { };
                    decimal QUANTITYPLT = 0;
                    decimal QUANTITYCUNI = 0;
                    int ciclosADVS = 0;

                    if (myObj.CUNI != myObj.STUN)
                    {
                        MyConvertionFactor = FactorConversion(myObj.ITEM, myObj.STUN, myObj.CUNI);
                        QUANTITYCUNI = (MyConvertionFactor.Tipo == "Div") ? Convert.ToDecimal((Convert.ToDecimal(myObj.QSTR) * MyConvertionFactor.FactorB) / MyConvertionFactor.FactorD) : Convert.ToDecimal((Convert.ToDecimal(myObj.QSTR) * MyConvertionFactor.FactorD) / MyConvertionFactor.FactorB);
                        QUANTITYCUNI = Math.Ceiling(Math.Round(QUANTITYCUNI, 1));
                        if (myObj.CUNI.Trim().ToUpper() != "PLT")
                        {
                            MyConvertionFactor = FactorConversion(myObj.ITEM, myObj.CUNI, "PLT");
                            QUANTITYPLT = (MyConvertionFactor.Tipo == "Div") ? Convert.ToDecimal((QUANTITYCUNI * MyConvertionFactor.FactorB) / MyConvertionFactor.FactorD) : Convert.ToDecimal((QUANTITYCUNI * MyConvertionFactor.FactorD) / MyConvertionFactor.FactorB);
                            ciclosADVS = Convert.ToInt32(Math.Ceiling(QUANTITYPLT));
                        }
                    }
                    else
                    {
                        QUANTITYCUNI = Convert.ToDecimal(myObj.QSTR);
                        MyConvertionFactor = FactorConversion(myObj.ITEM, myObj.CUNI, "PLT");
                        QUANTITYPLT = (MyConvertionFactor.Tipo == "Div") ? Convert.ToDecimal((Convert.ToDecimal(myObj.QSTR) * MyConvertionFactor.FactorB) / MyConvertionFactor.FactorD) : Convert.ToDecimal((Convert.ToDecimal(myObj.QSTR) * MyConvertionFactor.FactorD) / MyConvertionFactor.FactorB);
                        ciclosADVS = Convert.ToInt32(Math.Ceiling(QUANTITYPLT));
                    }

                    if (ciclosADVS > 0)
                    {
                        int PRIO = Itticol082.PrioridadMaxima();

                        for (int i = 1; i <= ciclosADVS; i++)
                        {
                            Ent_tticol082 Objtticol082 = new Ent_tticol082
                            {
                                PAID = " ",
                                LOGN = _operator,
                                OORG = myObj.OORG,
                                ORNO = myObj.ORNO,
                                OSET = myObj.OSET,
                                PONO = myObj.PONO,
                                SQNB = myObj.SEQN,
                                ADVS = i.ToString(),
                                ITEM = myObj.ITEM,
                                STAT = "1",
                                QTYT = CantidadPorCiclo(QUANTITYCUNI, Convert.ToInt32(MyConvertionFactor.FactorD), ciclosADVS, i).ToString(),
                                CWAR = myObj.SFCO.Trim(),
                                UNIT = myObj.CUNI,
                                PRIO = Convert.ToString(PRIO+1)
                            };
                            bool InsertSuccess = Itticol082.InsertarregistroItticol082(Objtticol082);
                            ListInsertResult.Add(InsertSuccess);
                            PRIO++;
                        }
                    }
                }
            }
            return JsonConvert.SerializeObject(ListInsertResult);
        }

        protected void CargarIdioma()
        {
            InvalidCustomerCode = mensajes("InvalidCustomerCode");
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