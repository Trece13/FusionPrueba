using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI; 
using System.Web.UI.WebControls;
using whusa;
using whusa.Interfases;
using Newtonsoft.Json;
using System.Data;
using System.Configuration;
using whusa.Entidades;
using whusa.Interfases;
using System.Globalization;
using System.Threading;
using whusa.Utilidades;

namespace whusap.WebPages.InvFloor
{
    public partial class whInvTransfers : System.Web.UI.Page
    {
        public static IntefazDAL_transfer Transfers = new IntefazDAL_transfer();
        private static InterfazDAL_ttccol301 _idalttccol301 = new InterfazDAL_ttccol301();

        public string lstWarehouses = string.Empty;
        public List<string> lstLocates = new List<string>();
        public static string _operator;
        public static string _idioma;
        public static string strError;
        public static string formName = "NewWhInvTransfers.aspx";


        public static string Thetransferwassuccessful = string.Empty;
        public static string PalletNotLocate = string.Empty;
        public static string PalletNotExist = string.Empty;
        public static string LocationTransfeCannotEqual = string.Empty;
        public static string LocationTypeMustBulK = string.Empty;
        public static string LocationBlockedTransfers = string.Empty;
        public static string TransferNotUpdated = string.Empty;
        public static string NotInserted = string.Empty;
        public static string TargetLocationNotExist = string.Empty;
        public static string CurrentNotExist = string.Empty;

        private static Mensajes _mensajesForm = new Mensajes();
        private static string globalMessages = "GlobalMessages";

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (Session["user"] == null)
                {
                    Response.Redirect(ConfigurationManager.AppSettings["UrlBase"] + "/WebPages/Login/whLogIni.aspx");
                }
                Label control = (Label)Page.Controls[0].FindControl("lblPageTitle");
                _operator = Session["user"].ToString();
                try
                {
                    _idioma = Session["ddlIdioma"].ToString();
                }
                catch (Exception)
                {
                    _idioma = "INGLES";
                }


                CargarIdioma();

                string strTitulo = mensajes("encabezado");
                control.Text = strTitulo;

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

            }
            lstWarehouses = ListWarehouses();



        }

        [WebMethod]
        public static string VerifyWarehouse(string LOCA)
        {
            Warehouse MyWarehouse = new Warehouse();
            DataTable CurrentWareHouse = Transfers.ConsultarWarehouse(LOCA);
            if (CurrentWareHouse.Rows.Count > 0)
            {
                MyWarehouse.CWAR = CurrentWareHouse.Rows[0]["CWAR"].ToString();
            }
            return JsonConvert.SerializeObject(MyWarehouse);
        }

        [WebMethod]
        public static string clickQuery(string PAID)
        {

            DataTable Transferencias = Transfers.ConsultarRegistroTransferir(PAID);


            Ent_twhcol020 objWhcol020 = new Ent_twhcol020();

            if (Transferencias.Rows.Count > 0)
            {
                //if (Transferencias.Rows[0]["SLOC"].ToString() == "1")
                //{
                if (Transferencias.Rows[0]["T$LOCA"].ToString().Trim() == string.Empty && Transferencias.Rows[0]["T$CWAR"].ToString().Trim() == string.Empty)
                {
                    objWhcol020.Error = true;
                    objWhcol020.ErrorMsg = PalletNotLocate;
                    objWhcol020.TipeMsgJs = "lbl";
                }
                else
                {
                    objWhcol020.tbl = Transferencias.Rows[0]["TBL"].ToString().Trim();
                    objWhcol020.sloc = Transferencias.Rows[0]["SLOC"].ToString();
                    objWhcol020.clot = Transferencias.Rows[0]["T$ORNO"].ToString().Trim();
                    objWhcol020.sqnb = Transferencias.Rows[0]["T$PAID"].ToString().Trim();
                    objWhcol020.mitm = Transferencias.Rows[0]["T$ITEM"].ToString().Trim();
                    objWhcol020.dsca = Transfers.DescripcionItem(objWhcol020.mitm);
                    objWhcol020.cwor = Transferencias.Rows[0]["T$CWAR"].ToString().Trim();
                    objWhcol020.loor = Transferencias.Rows[0]["T$LOCA"].ToString().Trim();
                    objWhcol020.qtdl = Convert.ToDouble(Transferencias.Rows[0]["T$QTYC"].ToString().Trim());
                    objWhcol020.cuni = Transferencias.Rows[0]["T$CUNI"].ToString().Trim();
                    objWhcol020.user = _operator;
                }
                //}
            }
            else
            {
                objWhcol020.Error = true;
                objWhcol020.ErrorMsg = PalletNotExist;
                objWhcol020.TipeMsgJs = "lbl";

            }

            return JsonConvert.SerializeObject(objWhcol020);

        }

        [WebMethod]
        public static string clickTransfer(string PAID, string CurrentWarehouse, string CurrentSloc, string CurrentLocation, string TargetWarehouse, string TargetSloc, string TargetLocation, bool StartCurrentWarehouse = true, bool StartCurrentLocation = true)
        {
            Ent_twhcol020 objWhcol020 = new Ent_twhcol020();

            DataTable Transferencias = Transfers.ConsultarRegistroTransferir(PAID);

            bool LocationCurrent = false;
            bool LocationTarget = false;
            if (CurrentWarehouse != TargetWarehouse && CurrentLocation != TargetLocation || CurrentWarehouse == TargetWarehouse && CurrentLocation != TargetLocation || CurrentWarehouse != TargetWarehouse && CurrentLocation == TargetLocation || (CurrentWarehouse == TargetWarehouse && CurrentLocation == TargetLocation && CurrentSloc.Trim() != "1" && TargetSloc != "1"))
            {
                if (CurrentSloc.Trim() != "1")
                {
                    LocationCurrent = true;
                }
                else
                {
                    if (StartCurrentWarehouse == true && StartCurrentWarehouse == true)
                    {
                        LocationCurrent = true;
                    }
                    else if (StartCurrentWarehouse == true && StartCurrentWarehouse == false)
                    {
                        LocationCurrent = (Transfers.ConsultarLocation(CurrentWarehouse, CurrentLocation).Rows.Count > 0) ? true : false;
                    }
                    else if (StartCurrentWarehouse == false && StartCurrentWarehouse == true)
                    {
                        LocationCurrent = (Transfers.ConsultarLocation(CurrentWarehouse, CurrentLocation).Rows.Count > 0) ? true : false;
                    }
                    else if (StartCurrentWarehouse == false && StartCurrentWarehouse == false)
                    {
                        LocationCurrent = (Transfers.ConsultarLocation(CurrentWarehouse, CurrentLocation).Rows.Count > 0) ? true : false;
                    }
                }


                if (TargetSloc.Trim() != "1")
                {
                    LocationTarget = true;
                }
                else
                {
                    var DtCurrentLocation = Transfers.ConsultarLocation(TargetWarehouse, TargetLocation);
                    if (DtCurrentLocation.Rows.Count > 0)
                    {
                        string LOCT = DtCurrentLocation.Rows[0]["LOCT"].ToString().Trim();
                        string BINB = DtCurrentLocation.Rows[0]["BINB"].ToString().Trim();
                        if (LOCT != "5")
                        {
                            objWhcol020.Error = true;
                            objWhcol020.ErrorMsg = LocationTypeMustBulK;
                            objWhcol020.TipeMsgJs = "lbl";
                            return JsonConvert.SerializeObject(objWhcol020);
                        }
                        else if (BINB != "2")
                        {
                            objWhcol020.Error = true;
                            objWhcol020.ErrorMsg = LocationBlockedTransfers;
                            objWhcol020.TipeMsgJs = "lbl";
                            return JsonConvert.SerializeObject(objWhcol020);
                        }
                        else
                        {
                            LocationTarget = true;
                        }
                    }
                }


                if (LocationCurrent == true)
                {
                    if (LocationTarget == true)
                    {

                        if (Transferencias.Rows.Count > 0)
                        {
                            objWhcol020.tbl = Transferencias.Rows[0]["TBL"].ToString().Trim();
                            objWhcol020.clot = Transferencias.Rows[0]["T$ORNO"].ToString().Trim();
                            objWhcol020.sqnb = Transferencias.Rows[0]["T$PAID"].ToString().Trim();
                            objWhcol020.mitm = Transferencias.Rows[0]["T$ITEM"].ToString();
                            objWhcol020.dsca = Transfers.DescripcionItem(objWhcol020.mitm);
                            objWhcol020.cwor = CurrentWarehouse;
                            objWhcol020.loor = CurrentLocation;
                            objWhcol020.cwde = TargetWarehouse;
                            objWhcol020.lode = TargetLocation;

                            objWhcol020.qtdl = Convert.ToDouble(Transferencias.Rows[0]["T$QTYC"].ToString().Trim());
                            objWhcol020.cuni = Transferencias.Rows[0]["T$CUNI"].ToString().Trim();
                            objWhcol020.user = _operator;




                            DataTable ExistenciaTransfer = Transfers.ConsultaTransferencia(PAID);

                            if (ExistenciaTransfer.Rows.Count > 0)
                            {
                                bool TransferenciasU = Transfers.ActualizarTransferencia(PAID, CurrentWarehouse, CurrentLocation, TargetWarehouse, TargetLocation, _operator);
                                if (TransferenciasU)
                                {
                                    if (TransferenciasU)
                                    {
                                        objWhcol020.Success = true;

                                        objWhcol020.SuccessMsg = Thetransferwassuccessful;
                                    }
                                    else
                                    {
                                        objWhcol020.Error = true;
                                        objWhcol020.ErrorMsg = TransferNotUpdated;
                                        objWhcol020.TipeMsgJs = "lbl";
                                    }
                                }
                                else
                                {
                                    objWhcol020.Error = true;
                                    objWhcol020.ErrorMsg = NotInserted;
                                    objWhcol020.TipeMsgJs = "lbl";
                                }
                            }
                            else
                            {
                                bool TransferenciasI = Transfers.InsertarTransferencia(objWhcol020);

                                if (TransferenciasI)
                                {
                                    if (/*TransferenciasU == */true)
                                    {
                                        objWhcol020.Success = true;
                                        objWhcol020.SuccessMsg = Thetransferwassuccessful;
                                        objWhcol020.TipeMsgJs = "lbl";
                                    }
                                    else
                                    {
                                        objWhcol020.Error = true;
                                        objWhcol020.ErrorMsg = TransferNotUpdated;
                                        objWhcol020.TipeMsgJs = "lbl";
                                    }
                                }
                                else
                                {
                                    objWhcol020.Error = true;
                                    objWhcol020.ErrorMsg = NotInserted;
                                    objWhcol020.TipeMsgJs = "lbl";
                                }

                            }

                        }
                        else
                        {
                            objWhcol020.Error = true;
                            objWhcol020.ErrorMsg = PalletNotExist;
                            objWhcol020.TipeMsgJs = "lbl";

                        }

                    }
                    else
                    {
                        objWhcol020.Error = true;
                        objWhcol020.ErrorMsg = TargetLocationNotExist;
                        objWhcol020.TipeMsgJs = "lbl";

                    }

                }
                else
                {
                    objWhcol020.Error = true;
                    objWhcol020.ErrorMsg = CurrentNotExist;
                    objWhcol020.TipeMsgJs = "lbl";

                }
            }
            else
            {
                objWhcol020.Error = true;
                objWhcol020.ErrorMsg = LocationTransfeCannotEqual;
                objWhcol020.TipeMsgJs = "lbl";
            }

            return JsonConvert.SerializeObject(objWhcol020);

        }

        public string ListWarehouses()
        {
            DataTable DTWarehouses = Transfers.ListWarehouses();
            List<Warehouse> lstWarehouses = new List<Warehouse>();

            if (DTWarehouses.Rows.Count > 0)
            {

                foreach (DataRow item in DTWarehouses.Rows)
                {
                    Warehouse MyWareHouse = new Warehouse();
                    MyWareHouse.CWAR = item["CWAR"].ToString();
                    MyWareHouse.SLOC = item["SLOC"].ToString();
                    lstWarehouses.Add(MyWareHouse);
                }

            }

            return JsonConvert.SerializeObject(lstWarehouses);

        }

        protected void CargarIdioma()
        {
            Thetransferwassuccessful = mensajes("Thetransferwassuccessful");
            PalletNotLocate = mensajes("PalletNotLocate");
            PalletNotExist = mensajes("PalletNotExist");
            LocationTransfeCannotEqual = mensajes("LocationTransfeCannotEqual");
            LocationTypeMustBulK = mensajes("LocationTypeMustBulK");
            LocationBlockedTransfers = mensajes("LocationBlockedTransfers");
            TransferNotUpdated = mensajes("TransferNotUpdated");
            NotInserted = mensajes("NotInserted");
            TargetLocationNotExist = mensajes("TargetLocationNotExist");
            CurrentNotExist = mensajes("CurrentNotExist");
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

        public class Warehouse
        {
            public string CWAR { get; set; }
            public string SLOC { get; set; }
        }
    }
}