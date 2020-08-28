using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using whusa;
using Newtonsoft.Json;
using System.Data;
using System.Web.Services;
using whusa.Interfases;
using System.Globalization;
using System.Threading;
using System.Configuration;
using whusa.Entidades;
using whusa.Utilidades;
using System.Web.UI.WebControls.WebParts;
using whusa.DAL;


namespace whusap.WebPages.WorkOrders
{
    public partial class Picking : System.Web.UI.Page
    {


        public static string thereisnotPalletavailable = mensajes("thereisnotPalletavailable");
        public static string ThequantityassociatetonewpalletisminortooldpalletID = mensajes("ThequantityassociatetonewpalletisminortooldpalletID");
        public static string ThenewpalletIddoesnthaveItemequaltotheoldpalletIditem = mensajes("ThenewpalletIddoesnthaveItemequaltotheoldpalletIditem");
        public static string ThepalletIDDoesntexistorItsinpickingprocess = mensajes("ThepalletIDDoesntexistorItsinpickingprocess");
        public static string ThePallethasalreadylocate = mensajes("ThePallethasalreadylocate");
        public static string ThePalletIDdoesnotexistorisnotassociatedtoyouruserornothavepalletsinpickingstatus = mensajes("ThePalletIDdoesnotexistorisnotassociatedtoyouruserornothavepalletsinpickingstatus");
        public static string ThePalletIDdoesnotexist = mensajes("ThePalletIDdoesnotexist");

        public static string ADVS = string.Empty;
        public object GObject = new object();
        public EventArgs Ge = new EventArgs();
        public static InterfazDAL_twhcol122 twhcolDAL = new InterfazDAL_twhcol122();
        public static InterfazDAL_twhcol130 twhcol130DAL = new InterfazDAL_twhcol130();
        private static InterfazDAL_ttccol301 _idalttccol301 = new InterfazDAL_ttccol301();
        string formName = string.Empty;
        public static string _operator = string.Empty;
        string _idioma = string.Empty;
        private static Mensajes _mensajesForm = new Mensajes();
        private static string globalMessages = "GlobalMessages";
        public static int flag022, flag042, flag131, flag307;
        DataTable resultado = new DataTable();
        public static string descombo = "";
        protected void Page_Load(object sender, EventArgs e)
        {




            GObject = sender;
            Ge = e;

            string PAID = "";
            string mensaje = Clic_Pick(PAID);

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

                string strTitulo = mensajes("encabezado");
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

            }
            if (!IsPostBack)
            {
                _operator = Session["user"].ToString();
                EntidadPicking MyObj = new EntidadPicking();
                Ent_ttccol307 MyObj307 = new Ent_ttccol307();
                MyObj307.PAID = PAID;
                MyObj307.USRR = _operator;
                DataTable DTttccol307 = twhcolDAL.ConsultarTt307140(MyObj307);
                List<EntidadPicking> LstPallet22 = new List<EntidadPicking>();
                List<EntidadPicking> LstPallet042 = new List<EntidadPicking>();
                List<EntidadPicking> LstPallet131 = new List<EntidadPicking>();
                if (DTttccol307.Rows.Count < 1)
                {
                    LstPallet131 = twhcolDAL.ConsultarPalletPicking131(PAID, string.Empty, _operator);
                    if (LstPallet131.Count > 0)
                    {
                        MyObj.PALLETID = LstPallet131[0].PALLETID.ToString();
                        Session["originalPallet"] = MyObj.PALLETID;
                        bool res = twhcolDAL.InsertarTccol307140(_operator, "1", MyObj.PALLETID.ToString(), "7", "0", "0");
                        if (res == false)
                        {
                            Page_Load(sender, e);
                        }
                    }

                    LstPallet042 = twhcolDAL.ConsultarPalletPicking042(PAID, string.Empty, _operator);
                    if (LstPallet042.Count > 0)
                    {
                        MyObj.PALLETID = LstPallet042[0].PALLETID.ToString();
                        Session["originalPallet"] = MyObj.PALLETID;
                        bool res = twhcolDAL.InsertarTccol307140(_operator, "1", MyObj.PALLETID.ToString(), "7", "0", "0");
                        if (res == false)
                        {
                            Page_Load(sender, e);
                        }
                    }

                    LstPallet22 = twhcolDAL.ConsultarPalletPicking22(PAID, string.Empty, _operator);
                    if (LstPallet22.Count > 0)
                    {
                        MyObj.PALLETID = LstPallet22[0].PALLETID.ToString();
                        Session["originalPallet"] = MyObj.PALLETID;
                        bool res = twhcolDAL.InsertarTccol307140(_operator, "1", MyObj.PALLETID.ToString(), "7", "0", "0");
                        if (res == false)
                        {
                            Page_Load(sender, e);
                        }
                    }
                }



                Random generator = new Random();
                int t = generator.Next(1, 1000000);
                string maximo = string.Format("{0:0000000000}", t);

                limpiarControles();
                if (DTttccol307.Rows.Count > 0)
                {
                    List<EntidadPicking> LstPallet22PAID = twhcolDAL.ConsultarPalletPicking22PAID(DTttccol307.Rows[0]["T$PAID"].ToString().Trim(), string.Empty, _operator);
                    List<EntidadPicking> LstPallet042PAID = twhcolDAL.ConsultarPalletPicking042PAID(DTttccol307.Rows[0]["T$PAID"].ToString().Trim(), string.Empty, _operator);
                    List<EntidadPicking> LstPallet131PAID = twhcolDAL.ConsultarPalletPicking131PAID(DTttccol307.Rows[0]["T$PAID"].ToString().Trim(), string.Empty, _operator);
                    if (LstPallet22PAID.Count > 0)
                    {
                        MyObj = LstPallet22PAID[0];
                        Session["originalPallet"] = MyObj.PALLETID.ToString();
                        lblPalletID.Text = MyObj.PALLETID.ToString();
                        lblItemID.Text = MyObj.ITEM.ToString();
                        HttpContext.Current.Session["ITEM"] = MyObj.ITEM.ToString();
                        lblItemDesc.Text = MyObj.DESCRIPTION.ToString();
                        LblLotId.Text = MyObj.LOT.ToString();
                        lblWarehouse.Text = MyObj.WRH.ToString();
                        lblWareDescr.Text = MyObj.DESCWRH.ToString();
                        lbllocation.Text = MyObj.LOCA.ToString();
                        lblQuantity.Text = MyObj.QTY.ToString();
                        HttpContext.Current.Session["QTY"] = MyObj.QTYT.ToString();
                        lblQuantityDesc.Text = MyObj.UN.ToString();
                        lblOORG.Text = MyObj.OORG.ToString();
                        lblORNO.Text = MyObj.ORNO.ToString();
                        lblOSET.Text = MyObj.OSET.ToString();
                        lblPONO.Text = MyObj.PONO.ToString();
                        lblSQNB.Text = MyObj.SQNB.ToString();
                        lblADVS.Text = MyObj.ADVS.ToString() + " PRIO:" + MyObj.PRIO.ToString() + " PONO:" + MyObj.PONO.ToString() + " ORNO:" + MyObj.ORNO.ToString();
                        ADVS = MyObj.ADVS.ToString();
                        flag022 = 1;
                        flag131 = 0;
                        flag042 = 0;
                    }
                    else if (LstPallet042PAID.Count > 0)
                    {
                        MyObj = LstPallet042PAID[0];
                        Session["originalPallet"] = MyObj.PALLETID.ToString();
                        lblPalletID.Text = MyObj.PALLETID.ToString();
                        lblItemID.Text = MyObj.ITEM.ToString();
                        HttpContext.Current.Session["ITEM"] = MyObj.ITEM.ToString();
                        lblItemDesc.Text = MyObj.DESCRIPTION.ToString();
                        LblLotId.Text = MyObj.LOT.ToString();
                        lblWarehouse.Text = MyObj.WRH.ToString();
                        lblWareDescr.Text = MyObj.DESCWRH.ToString();
                        lbllocation.Text = MyObj.LOCA.ToString();
                        lblQuantity.Text = MyObj.QTY.ToString();
                        HttpContext.Current.Session["QTY"] = MyObj.QTYT.ToString();
                        lblQuantityDesc.Text = MyObj.UN.ToString();
                        lblOORG.Text = MyObj.OORG.ToString();
                        lblORNO.Text = MyObj.ORNO.ToString();
                        lblOSET.Text = MyObj.OSET.ToString();
                        lblPONO.Text = MyObj.PONO.ToString();
                        lblSQNB.Text = MyObj.SQNB.ToString();
                        lblADVS.Text = MyObj.ADVS.ToString() + " PRIO:" + MyObj.PRIO.ToString() + " PONO:" + MyObj.PONO.ToString() + " ORNO:" + MyObj.ORNO.ToString();

                        ADVS = MyObj.ADVS.ToString();
                        flag022 = 0;
                        flag131 = 0;
                        flag042 = 1;
                    }
                    else if (LstPallet131PAID.Count > 0)
                    {
                        MyObj = LstPallet131PAID[0];
                        Session["originalPallet"] = MyObj.PALLETID.ToString();
                        lblPalletID.Text = MyObj.PALLETID.ToString();
                        lblItemID.Text = MyObj.ITEM.ToString();
                        HttpContext.Current.Session["ITEM"] = MyObj.ITEM.ToString();
                        lblItemDesc.Text = MyObj.DESCRIPTION.ToString();
                        LblLotId.Text = MyObj.LOT.ToString();
                        lblWarehouse.Text = MyObj.WRH.ToString();
                        lblWareDescr.Text = MyObj.DESCWRH.ToString();
                        lbllocation.Text = MyObj.LOCA.ToString();
                        lblQuantity.Text = MyObj.QTY.ToString();
                        HttpContext.Current.Session["QTY"] = MyObj.QTY.ToString();
                        lblQuantityDesc.Text = MyObj.UN.ToString();
                        lblOORG.Text = MyObj.OORG.ToString();
                        lblORNO.Text = MyObj.ORNO.ToString();
                        lblOSET.Text = MyObj.OSET.ToString();
                        lblPONO.Text = MyObj.PONO.ToString();
                        lblSQNB.Text = MyObj.SQNB.ToString();
                        lblADVS.Text = MyObj.ADVS.ToString() + " PRIO:" + MyObj.PRIO.ToString() + " PONO:" + MyObj.PONO.ToString() + " ORNO:" + MyObj.ORNO.ToString();
                        ADVS = MyObj.ADVS.ToString();
                        flag022 = 0;
                        flag131 = 1;
                        flag042 = 0;
                    }
                }
                else if (LstPallet22.Count > 0)
                {
                    MyObj = LstPallet22[0];
                    Session["originalPallet"] = MyObj.PALLETID.ToString();
                    lblPalletID.Text = MyObj.PALLETID.ToString();
                    lblItemID.Text = MyObj.ITEM.ToString();
                    HttpContext.Current.Session["ITEM"] = MyObj.ITEM.ToString();
                    lblItemDesc.Text = MyObj.DESCRIPTION.ToString();
                    LblLotId.Text = MyObj.LOT.ToString();
                    lblWarehouse.Text = MyObj.WRH.ToString();
                    lblWareDescr.Text = MyObj.DESCWRH.ToString();
                    lbllocation.Text = MyObj.LOCA.ToString();
                    lblQuantity.Text = MyObj.QTY.ToString();
                    HttpContext.Current.Session["QTY"] = MyObj.QTY.ToString();
                    lblQuantityDesc.Text = MyObj.UN.ToString();
                    lblOORG.Text = MyObj.OORG.ToString();
                    lblORNO.Text = MyObj.ORNO.ToString();
                    lblOSET.Text = MyObj.OSET.ToString();
                    lblPONO.Text = MyObj.PONO.ToString();
                    lblSQNB.Text = MyObj.SQNB.ToString();
                    lblADVS.Text = MyObj.ADVS.ToString() + " PRIO:" + MyObj.PRIO.ToString() + " PONO:" + MyObj.PONO.ToString() + " ORNO:" + MyObj.ORNO.ToString();
                    ADVS = MyObj.ADVS.ToString();
                    flag022 = 1;
                    flag131 = 0;
                    flag042 = 0;
                    
                    twhcolDAL.actRegtticol082140(_operator, " ", " ", 5, " ", MyObj.OORG.ToString(), MyObj.ORNO.ToString(), MyObj.OSET.ToString(), MyObj.PONO.ToString(), MyObj.SQNB.ToString(), MyObj.ADVS.ToString());
                }
                else if (LstPallet042.Count > 0)
                {
                    MyObj = LstPallet042[0];
                    Session["originalPallet"] = MyObj.PALLETID.ToString();
                    lblPalletID.Text = MyObj.PALLETID.ToString();
                    lblItemID.Text = MyObj.ITEM.ToString();
                    HttpContext.Current.Session["ITEM"] = MyObj.ITEM.ToString();
                    lblItemDesc.Text = MyObj.DESCRIPTION.ToString();
                    LblLotId.Text = MyObj.LOT.ToString();
                    lblWarehouse.Text = MyObj.WRH.ToString();
                    lblWareDescr.Text = MyObj.DESCWRH.ToString();
                    lbllocation.Text = MyObj.LOCA.ToString();
                    lblQuantity.Text = MyObj.QTY.ToString();
                    HttpContext.Current.Session["QTY"] = MyObj.QTY.ToString();
                    lblQuantityDesc.Text = MyObj.UN.ToString();
                    lblOORG.Text = MyObj.OORG.ToString();
                    lblORNO.Text = MyObj.ORNO.ToString();
                    lblOSET.Text = MyObj.OSET.ToString();
                    lblPONO.Text = MyObj.PONO.ToString();
                    lblSQNB.Text = MyObj.SQNB.ToString();
                    lblADVS.Text = MyObj.ADVS.ToString() + " PRIO:" + MyObj.PRIO.ToString() + " PONO:" + MyObj.PONO.ToString() + " ORNO:" + MyObj.ORNO.ToString();
                    ADVS = MyObj.ADVS.ToString();
                    flag022 = 0;
                    flag131 = 0;
                    flag042 = 1;
                    twhcolDAL.actRegtticol082140(_operator, " ", " ", 5, " ", MyObj.OORG.ToString(), MyObj.ORNO.ToString(), MyObj.OSET.ToString(), MyObj.PONO.ToString(), MyObj.SQNB.ToString(), MyObj.ADVS.ToString());

                    ////                twhcolDAL.actRegtticol082140(_operator, MyObj.PALLETID.ToString(), MyObj.LOCA.ToString(), 5, maximo, MyObj.OORG.ToString(), MyObj.ORNO.ToString(), MyObj.OSET.ToString(), MyObj.PONO.ToString(), MyObj.SQNB.ToString(), MyObj.ADVS.ToString());
                    //bool res = twhcolDAL.InsertarTccol307140(_operator, "1", MyObj.PALLETID.ToString(), "7", "0", "0");
                    //if (res == false)
                    //{
                    //    Page_Load(sender, e);
                    //}
                }
                else if (LstPallet131.Count > 0)
                {
                    MyObj = LstPallet131[0];
                    Session["originalPallet"] = MyObj.PALLETID.ToString();
                    lblPalletID.Text = MyObj.PALLETID.ToString();
                    lblItemID.Text = MyObj.ITEM.ToString();
                    HttpContext.Current.Session["ITEM"] = MyObj.ITEM.ToString();
                    lblItemDesc.Text = MyObj.DESCRIPTION.ToString();
                    LblLotId.Text = MyObj.LOT.ToString();
                    lblWarehouse.Text = MyObj.WRH.ToString();
                    lblWareDescr.Text = MyObj.DESCWRH.ToString();
                    lbllocation.Text = MyObj.LOCA.ToString();
                    lblQuantity.Text = MyObj.QTY.ToString();
                    HttpContext.Current.Session["QTY"] = MyObj.QTY.ToString();
                    lblQuantityDesc.Text = MyObj.UN.ToString();
                    lblOORG.Text = MyObj.OORG.ToString();
                    lblORNO.Text = MyObj.ORNO.ToString();
                    lblOSET.Text = MyObj.OSET.ToString();
                    lblPONO.Text = MyObj.PONO.ToString();
                    lblSQNB.Text = MyObj.SQNB.ToString();
                    lblADVS.Text = MyObj.ADVS.ToString() + " PRIO:" + MyObj.PRIO.ToString() + " PONO:" + MyObj.PONO.ToString() + " ORNO:" + MyObj.ORNO.ToString();
                    ADVS = MyObj.ADVS.ToString();
                    flag022 = 0;
                    flag131 = 1;
                    flag042 = 0;
                    twhcolDAL.actRegtticol082140(_operator, " ", " ", 5, " ", MyObj.OORG.ToString(), MyObj.ORNO.ToString(), MyObj.OSET.ToString(), MyObj.PONO.ToString(), MyObj.SQNB.ToString(), MyObj.ADVS.ToString());

                    ////twhcolDAL.actRegtticol082140(_operator, MyObj.PALLETID.ToString(), MyObj.LOCA.ToString(), 5, maximo, MyObj.OORG.ToString(), MyObj.ORNO.ToString(), MyObj.OSET.ToString(), MyObj.PONO.ToString(), MyObj.SQNB.ToString(), MyObj.ADVS.ToString());
                    //bool res = twhcolDAL.InsertarTccol307140(_operator, "1", MyObj.PALLETID.ToString(), "7", "0", "0");
                    //if (res == false)
                    //{
                    //    Page_Load(sender, e);
                    //}
                }
                if ((LstPallet22.Count == 0) && (LstPallet042.Count == 0) && (LstPallet131.Count == 0) && (DTttccol307.Rows.Count == 0))
                {
                    mensaje = thereisnotPalletavailable;
                    //Response.Write("<script language=javascript>alert('" + mensaje + "');window.location = '/WebPages/Login/whMenuI.aspx';</script>");

                }
            }
        }

        public void limpiarControles()
        {
            lblPalletID.Text = "";
            lblItemID.Text = "";
            lblItemDesc.Text = "";
            LblLotId.Text = "";
            lblWarehouse.Text = "";
            lblWareDescr.Text = "";
            lbllocation.Text = "";
            lblQuantity.Text = "";
            lblQuantityDesc.Text = "";
            lblOORG.Text = "";
            lblORNO.Text = "";
            lblOSET.Text = "";
            lblPONO.Text = "";
            lblSQNB.Text = "";
            lblADVS.Text = "";

        }
        [WebMethod]
        public static bool VerificarLocate(string CWAR, string LOCA)
        {
            DataTable DTLote = twhcolDAL.VerificarLocate(CWAR, LOCA);
            if (DTLote.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [WebMethod]
        public static string VerificarPalletID(string PAID_NEW, string PAID_OLD)
        {

            EntidadPicking ObjPicking = new EntidadPicking();

            DataTable DTPalletID = twhcolDAL.VerificarPalletID(PAID_NEW);
            if (DTPalletID.Rows.Count > 0)
            {


                if (DTPalletID.Rows[0]["ITEM"].ToString().Trim() == HttpContext.Current.Session["ITEM"].ToString().Trim())
                {
                    if (Convert.ToDecimal(DTPalletID.Rows[0]["QTYT"].ToString()) >= Convert.ToDecimal(HttpContext.Current.Session["QTY"].ToString()))
                    {
                        ObjPicking.error = false;

                        ObjPicking.LOT = DTPalletID.Rows[0]["ORNO"].ToString();
                        ObjPicking.WRH = DTPalletID.Rows[0]["CWAT"].ToString();
                        ObjPicking.DESCWRH = DTPalletID.Rows[0]["DESCAW"].ToString();
                        ObjPicking.LOCA = DTPalletID.Rows[0]["ACLO"].ToString();
                        ObjPicking.QTY = DTPalletID.Rows[0]["QTYT"].ToString();
                        ObjPicking.UN = DTPalletID.Rows[0]["UNIT"].ToString();

                        bool ret = false;
                        ret = twhcolDAL.Actualizar307(PAID_NEW, PAID_OLD);
                        if (ret)
                        {
                            Ent_ttccol307 MyObj307 = new Ent_ttccol307();
                            MyObj307.PAID = PAID_NEW;
                            MyObj307.USRR = _operator;
                            DataTable DTttccol307 = twhcolDAL.ConsultarTt307140(MyObj307);
                            if (DTttccol307.Rows.Count > 0)
                            {
                                string Tbl = DTPalletID.Rows[0]["TBL"].ToString().Trim();
                                switch (Tbl)
                                {
                                    case "ticol022":
                                        flag022 = 1;
                                        flag042 = 0;
                                        flag131 = 0;
                                        break;

                                    case "ticol042":
                                        flag042 = 1;
                                        flag131 = 0;
                                        flag022 = 0;
                                        break;

                                    case "whcol131":
                                        flag131 = 1;
                                        flag022 = 0;
                                        flag042 = 0;
                                        break;
                                }

                            }
                        }

                        return JsonConvert.SerializeObject(ObjPicking);
                    }
                    else
                    {
                        // cantidadd de pallet new menor a pallet old
                        ObjPicking.error = true;
                        ObjPicking.errorMsg = ThequantityassociatetonewpalletisminortooldpalletID;
                        return JsonConvert.SerializeObject(ObjPicking);
                    }
                }
                else
                {
                    //pallet new con item diferente
                    ObjPicking.error = true;
                    ObjPicking.errorMsg = ThenewpalletIddoesnthaveItemequaltotheoldpalletIditem;
                    return JsonConvert.SerializeObject(ObjPicking);
                }
            }
            else
            {
                // pallet no existe
                ObjPicking.error = true;
                ObjPicking.errorMsg = ThepalletIDDoesntexistorItsinpickingprocess;
                return JsonConvert.SerializeObject(ObjPicking);
            }
        }



        [WebMethod]
        public static string Click_Query(string PAID)
        {
            flag022 = 0;
            flag042 = 0;
            flag131 = 0;

            try
            {
                EntidadPicking MyObj = new EntidadPicking();

                List<EntidadPicking> LstPallet = twhcolDAL.ConsultarPalletPicking22(PAID, string.Empty, _operator);
                if (LstPallet.Count > 0)
                {
                    MyObj = LstPallet[0];

                    if (string.IsNullOrEmpty(MyObj.PALLETID) || string.IsNullOrEmpty(MyObj.LOCA))
                    {
                        MyObj.error = true;
                        MyObj.errorMsg = ThePallethasalreadylocate;
                    }
                }
                else
                {
                    MyObj.error = true;
                    MyObj.errorMsg = ThePalletIDdoesnotexistorisnotassociatedtoyouruserornothavepalletsinpickingstatus;
                }

                return JsonConvert.SerializeObject(MyObj);

            }
            catch (Exception e)
            {
                return ThePalletIDdoesnotexist;
            }

        }


        [WebMethod]
        public static bool Click_confirPKG(string PAID_OLD, string PAID, string LOCA, string OORG, string ORNO, string OSET, string PONO, string SQNB)
        {


            try
            {
                String pallet = PAID;
                String Location = LOCA;

                //Generar Ramdom
                Random generator = new Random();
                int t = generator.Next(1, 1000000);
                string maximo = string.Format("{0:0000000000}", t);


                if (flag022 == 1)
                {

                    twhcolDAL.actRegtticol082140(_operator, pallet.ToUpper(), Location.ToUpper(), 2, maximo, OORG, ORNO, OSET, PONO, SQNB, ADVS);
                    //twhcolDAL.IngRegistrott307140(_operator, 2, pallet, 0, 0);
                    twhcolDAL.actRegtticol022140(pallet);
                    twhcolDAL.EliminarTccol307140(pallet.Trim());
                    return true;

                }
                else if (flag042 == 1)
                {

                    twhcolDAL.actRegtticol082140(_operator, pallet.ToUpper(), Location.ToUpper(), 2, maximo, OORG, ORNO, OSET, PONO, SQNB, ADVS);
                    //twhcolDAL.IngRegistrott307140(_operator, 2, pallet, 0, 0);
                    twhcolDAL.actRegtticol042140(pallet);
                    twhcolDAL.EliminarTccol307140(pallet.Trim());

                    return true;


                }

                else if (flag131 == 1)
                {
                    twhcolDAL.actRegtticol082140(_operator, pallet.ToUpper(), Location.ToUpper(), 2, maximo, OORG, ORNO, OSET, PONO, SQNB, ADVS);
                    //twhcolDAL.IngRegistrott307140(_operator, 2, pallet, 0, 0);
                    twhcolDAL.actRegtticol131140(pallet);
                    twhcolDAL.EliminarTccol307140(pallet.Trim());

                    return true;


                }
                else
                {

                    return false;
                }


            }
            catch (Exception e)
            {
                return false;
            }
        }

        [WebMethod]
        public static bool Click_confirCausal(string PAID, string Causal, string txtPallet, string LOCA, string OORG, string ORNO, string OSET, string PONO, string SQNB, string ADVS)
        {
            String pallet = PAID;
            String Location = LOCA.ToUpper();
            LOCA = LOCA.ToUpper();
            int stat = 0;
            int statCausal = 0;
            string de = Causal;

            if (de == "1")
            {

                statCausal = 1;
            }
            else if (de == "2")
            {
                statCausal = 2;
            }
            else if (de == "3")
            {

                statCausal = 3;
            }
            try
            {
                Random generator = new Random();
                int t = generator.Next(1, 1000000);
                string maximo = string.Format("{0:0000000000}", t);

                if (flag022 == 1)
                {
                    stat = 12;
                    twhcolDAL.ingRegTticol092140(maximo, pallet, txtPallet, statCausal, _operator);
                    //twhcolDAL.actRegtticol082140(_operator, pallet, Location, 2, maximo, OORG, ORNO, OSET, PONO, SQNB, ADVS);
                    twhcolDAL.InsertRegCausalCOL084(pallet, _operator, statCausal);
                    if (HttpContext.Current.Session["originalPallet"].ToString() != PAID.ToString())
                    {
                        twhcolDAL.ActCausalTICOL022(pallet, stat);
                    }
                    return true;

                }
                else if (flag042 == 1)
                {

                    stat = 12;
                    twhcolDAL.ingRegTticol092140(maximo, pallet, txtPallet, statCausal, _operator);
                    //twhcolDAL.actRegtticol082140(_operator, pallet, Location, 2, maximo, OORG, ORNO, OSET, PONO, SQNB, ADVS);
                    twhcolDAL.InsertRegCausalCOL084(pallet, _operator, statCausal);
                    if (HttpContext.Current.Session["originalPallet"].ToString() != PAID.ToString()){
                        twhcolDAL.ActCausalTICOL042(pallet, stat);
                    }
                    
                    return true;

                }

                else if (flag131 == 1)
                {
                    stat = 10;
                    twhcolDAL.ingRegTticol092140(maximo, pallet, txtPallet, statCausal, _operator);
                    //twhcolDAL.actRegtticol082140(_operator, pallet, Location, 2, maximo, OORG, ORNO, OSET, PONO, SQNB, ADVS);
                    twhcolDAL.InsertRegCausalCOL084(pallet, _operator, statCausal);
                    if (HttpContext.Current.Session["originalPallet"].ToString() != PAID.ToString())
                    {
                        twhcolDAL.ActCausalcol131140(pallet, stat);
                    }
                    return true;
                }
                else
                {
                    return false;
                }


            }
            catch (Exception e)
            {
                return false;
            }
        }


        public class Error
        {
            public string MyError { get; set; }
        };

        [WebMethod]
        public string Clic_Pick(string PAID)
        {

            try
            {
                string retorno = string.Empty;
                string USRR = _operator;
                DataTable DTtccol307PalletID = twhcol130DAL.Consultarttccol307(PAID, string.Empty);


                if (DTtccol307PalletID.Rows.Count > 0)
                {
                    retorno = "The ID palled: " + DTtccol307PalletID.Rows[0]["PAID"].ToString().Trim() + " pending to locate by user: " + DTtccol307PalletID.Rows[0]["USRR"].ToString().Trim();
                }

                return retorno;

            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }

        protected static string mensajes(string tipoMensaje)
        {
            Mensajes mensajesForm = new Mensajes();
            string idioma = "INGLES";
            var retorno = mensajesForm.readStatement("Picking.aspx", idioma, ref tipoMensaje);

            if (retorno.Trim() == String.Empty)
            {
                retorno = mensajesForm.readStatement(globalMessages, idioma, ref tipoMensaje);
            }

            return retorno;
        }

        protected void Reload_Click(object sender, EventArgs e)
        {
            flag022 = 0;
            flag042 = 0;
            flag131 = 0;
            string PAID = "";
            string mensaje = "";
            //Page_Load(GObject, Ge);
            _operator = Session["user"].ToString();
            EntidadPicking MyObj = new EntidadPicking();
            Ent_ttccol307 MyObj307 = new Ent_ttccol307();
            MyObj307.PAID = PAID;
            MyObj307.USRR = _operator;
            DataTable DTttccol307 = twhcolDAL.ConsultarTt307140(MyObj307);
            List<EntidadPicking> LstPallet22 = new List<EntidadPicking>();
            List<EntidadPicking> LstPallet042 = new List<EntidadPicking>();
            List<EntidadPicking> LstPallet131 = new List<EntidadPicking>();
            if (DTttccol307.Rows.Count < 1)
            {
                LstPallet22 = twhcolDAL.ConsultarPalletPicking22(PAID, string.Empty, _operator);
                if (LstPallet22.Count > 0)
                {
                    MyObj.PALLETID = LstPallet22[0].PALLETID.ToString();
                    Session["originalPallet"] = MyObj.PALLETID;
                    bool res = twhcolDAL.InsertarTccol307140(_operator, "1", MyObj.PALLETID.ToString(), "7", "0", "0");
                    if (res == false)
                    {
                        Page_Load(sender, e);
                    }
                }
                LstPallet042 = twhcolDAL.ConsultarPalletPicking042(PAID, string.Empty, _operator);
                if (LstPallet042.Count > 0)
                {
                    MyObj.PALLETID = LstPallet042[0].PALLETID.ToString();
                    Session["originalPallet"] = MyObj.PALLETID;
                    bool res = twhcolDAL.InsertarTccol307140(_operator, "1", MyObj.PALLETID.ToString(), "7", "0", "0");
                    if (res == false)
                    {
                        Page_Load(sender, e);
                    }
                }
                LstPallet131 = twhcolDAL.ConsultarPalletPicking131(PAID, string.Empty, _operator);
                if (LstPallet131.Count > 0)
                {
                    MyObj.PALLETID = LstPallet131[0].PALLETID.ToString();
                    Session["originalPallet"] = MyObj.PALLETID;
                    bool res = twhcolDAL.InsertarTccol307140(_operator, "1", MyObj.PALLETID.ToString(), "7", "0", "0");
                    if (res == false)
                    {
                        Page_Load(sender, e);
                    }
                }
            }



            Random generator = new Random();
            int t = generator.Next(1, 1000000);
            string maximo = string.Format("{0:0000000000}", t);

            limpiarControles();
            if (DTttccol307.Rows.Count > 0)
            {
                List<EntidadPicking> LstPallet22PAID = twhcolDAL.ConsultarPalletPicking22PAID(DTttccol307.Rows[0]["T$PAID"].ToString().Trim(), string.Empty, _operator);
                List<EntidadPicking> LstPallet042PAID = twhcolDAL.ConsultarPalletPicking042PAID(DTttccol307.Rows[0]["T$PAID"].ToString().Trim(), string.Empty, _operator);
                List<EntidadPicking> LstPallet131PAID = twhcolDAL.ConsultarPalletPicking131PAID(DTttccol307.Rows[0]["T$PAID"].ToString().Trim(), string.Empty, _operator);
                if (LstPallet22PAID.Count > 0)
                {
                    MyObj = LstPallet22PAID[0];
                    Session["originalPallet"] = MyObj.PALLETID.ToString();
                    lblPalletID.Text = MyObj.PALLETID.ToString();
                    lblItemID.Text = MyObj.ITEM.ToString();
                    HttpContext.Current.Session["ITEM"] = MyObj.ITEM.ToString();
                    lblItemDesc.Text = MyObj.DESCRIPTION.ToString();
                    LblLotId.Text = MyObj.LOT.ToString();
                    lblWarehouse.Text = MyObj.WRH.ToString();
                    lblWareDescr.Text = MyObj.DESCWRH.ToString();
                    lbllocation.Text = MyObj.LOCA.ToString();
                    lblQuantity.Text = MyObj.QTY.ToString();
                    HttpContext.Current.Session["QTY"] = MyObj.QTYT.ToString();
                    lblQuantityDesc.Text = MyObj.UN.ToString();
                    lblOORG.Text = MyObj.OORG.ToString();
                    lblORNO.Text = MyObj.ORNO.ToString();
                    lblOSET.Text = MyObj.OSET.ToString();
                    lblPONO.Text = MyObj.PONO.ToString();
                    lblSQNB.Text = MyObj.SQNB.ToString();
                    lblADVS.Text = MyObj.ADVS.ToString() + " PRIO:" + MyObj.PRIO.ToString() + " PONO:" + MyObj.PONO.ToString() + " ORNO:" + MyObj.ORNO.ToString();
                    ADVS = MyObj.ADVS.ToString();
                    flag022 = 1;
                }
                else if (LstPallet042PAID.Count > 0)
                {
                    MyObj = LstPallet042PAID[0];
                    Session["originalPallet"] = MyObj.PALLETID.ToString();
                    lblPalletID.Text = MyObj.PALLETID.ToString();
                    lblItemID.Text = MyObj.ITEM.ToString();
                    HttpContext.Current.Session["ITEM"] = MyObj.ITEM.ToString();
                    lblItemDesc.Text = MyObj.DESCRIPTION.ToString();
                    LblLotId.Text = MyObj.LOT.ToString();
                    lblWarehouse.Text = MyObj.WRH.ToString();
                    lblWareDescr.Text = MyObj.DESCWRH.ToString();
                    lbllocation.Text = MyObj.LOCA.ToString();
                    lblQuantity.Text = MyObj.QTY.ToString();
                    HttpContext.Current.Session["QTY"] = MyObj.QTYT.ToString();
                    lblQuantityDesc.Text = MyObj.UN.ToString();
                    lblOORG.Text = MyObj.OORG.ToString();
                    lblORNO.Text = MyObj.ORNO.ToString();
                    lblOSET.Text = MyObj.OSET.ToString();
                    lblPONO.Text = MyObj.PONO.ToString();
                    lblSQNB.Text = MyObj.SQNB.ToString();
                    lblADVS.Text = MyObj.ADVS.ToString() + " PRIO:" + MyObj.PRIO.ToString() + " PONO:" + MyObj.PONO.ToString() + " ORNO:" + MyObj.ORNO.ToString();

                    ADVS = MyObj.ADVS.ToString();
                    flag042 = 1;
                }
                else if (LstPallet131PAID.Count > 0)
                {
                    MyObj = LstPallet131PAID[0];
                    Session["originalPallet"] = MyObj.PALLETID.ToString();
                    lblPalletID.Text = MyObj.PALLETID.ToString();
                    lblItemID.Text = MyObj.ITEM.ToString();
                    HttpContext.Current.Session["ITEM"] = MyObj.ITEM.ToString();
                    lblItemDesc.Text = MyObj.DESCRIPTION.ToString();
                    LblLotId.Text = MyObj.LOT.ToString();
                    lblWarehouse.Text = MyObj.WRH.ToString();
                    lblWareDescr.Text = MyObj.DESCWRH.ToString();
                    lbllocation.Text = MyObj.LOCA.ToString();
                    lblQuantity.Text = MyObj.QTY.ToString();
                    HttpContext.Current.Session["QTY"] = MyObj.QTYT.ToString();
                    lblQuantityDesc.Text = MyObj.UN.ToString();
                    lblOORG.Text = MyObj.OORG.ToString();
                    lblORNO.Text = MyObj.ORNO.ToString();
                    lblOSET.Text = MyObj.OSET.ToString();
                    lblPONO.Text = MyObj.PONO.ToString();
                    lblSQNB.Text = MyObj.SQNB.ToString();
                    lblADVS.Text = MyObj.ADVS.ToString() + " PRIO:" + MyObj.PRIO.ToString() + " PONO:" + MyObj.PONO.ToString() + " ORNO:" + MyObj.ORNO.ToString();
                    ADVS = MyObj.ADVS.ToString();
                    flag131 = 1;
                }
            }
            else if (LstPallet22.Count > 0)
            {
                MyObj = LstPallet22[0];
                Session["originalPallet"] = MyObj.PALLETID.ToString();
                lblPalletID.Text = MyObj.PALLETID.ToString();
                lblItemID.Text = MyObj.ITEM.ToString();
                HttpContext.Current.Session["ITEM"] = MyObj.ITEM.ToString();
                lblItemDesc.Text = MyObj.DESCRIPTION.ToString();
                LblLotId.Text = MyObj.LOT.ToString();
                lblWarehouse.Text = MyObj.WRH.ToString();
                lblWareDescr.Text = MyObj.DESCWRH.ToString();
                lbllocation.Text = MyObj.LOCA.ToString();
                lblQuantity.Text = MyObj.QTY.ToString();
                HttpContext.Current.Session["QTY"] = MyObj.QTYT.ToString();
                lblQuantityDesc.Text = MyObj.UN.ToString();
                lblOORG.Text = MyObj.OORG.ToString();
                lblORNO.Text = MyObj.ORNO.ToString();
                lblOSET.Text = MyObj.OSET.ToString();
                lblPONO.Text = MyObj.PONO.ToString();
                lblSQNB.Text = MyObj.SQNB.ToString();
                lblADVS.Text = MyObj.ADVS.ToString() + " PRIO:" + MyObj.PRIO.ToString() + " PONO:" + MyObj.PONO.ToString() + " ORNO:" + MyObj.ORNO.ToString();
                ADVS = MyObj.ADVS.ToString();
                flag022 = 1;
                twhcolDAL.actRegtticol082140(_operator, " ", " ", 5, " ", MyObj.OORG.ToString(), MyObj.ORNO.ToString(), MyObj.OSET.ToString(), MyObj.PONO.ToString(), MyObj.SQNB.ToString(), MyObj.ADVS.ToString());
                //bool res = twhcolDAL.InsertarTccol307140(_operator, "1", MyObj.PALLETID.ToString(), "7", "0", "0");
                //if (res == false)
                //{
                //    Page_Load(sender, e);
                //}
            }
            else if (LstPallet042.Count > 0)
            {
                MyObj = LstPallet042[0];
                Session["originalPallet"] = MyObj.PALLETID.ToString();
                lblPalletID.Text = MyObj.PALLETID.ToString();
                lblItemID.Text = MyObj.ITEM.ToString();
                HttpContext.Current.Session["ITEM"] = MyObj.ITEM.ToString();
                lblItemDesc.Text = MyObj.DESCRIPTION.ToString();
                LblLotId.Text = MyObj.LOT.ToString();
                lblWarehouse.Text = MyObj.WRH.ToString();
                lblWareDescr.Text = MyObj.DESCWRH.ToString();
                lbllocation.Text = MyObj.LOCA.ToString();
                lblQuantity.Text = MyObj.QTY.ToString();
                HttpContext.Current.Session["QTY"] = MyObj.QTYT.ToString();
                lblQuantityDesc.Text = MyObj.UN.ToString();
                lblOORG.Text = MyObj.OORG.ToString();
                lblORNO.Text = MyObj.ORNO.ToString();
                lblOSET.Text = MyObj.OSET.ToString();
                lblPONO.Text = MyObj.PONO.ToString();
                lblSQNB.Text = MyObj.SQNB.ToString();
                lblADVS.Text = MyObj.ADVS.ToString() + " PRIO:" + MyObj.PRIO.ToString() + " PONO:" + MyObj.PONO.ToString() + " ORNO:" + MyObj.ORNO.ToString();
                ADVS = MyObj.ADVS.ToString();
                flag042 = 1;
                twhcolDAL.actRegtticol082140(_operator, " ", " ", 5, " ", MyObj.OORG.ToString(), MyObj.ORNO.ToString(), MyObj.OSET.ToString(), MyObj.PONO.ToString(), MyObj.SQNB.ToString(), MyObj.ADVS.ToString());

                ////                twhcolDAL.actRegtticol082140(_operator, MyObj.PALLETID.ToString(), MyObj.LOCA.ToString(), 5, maximo, MyObj.OORG.ToString(), MyObj.ORNO.ToString(), MyObj.OSET.ToString(), MyObj.PONO.ToString(), MyObj.SQNB.ToString(), MyObj.ADVS.ToString());
                //bool res = twhcolDAL.InsertarTccol307140(_operator, "1", MyObj.PALLETID.ToString(), "7", "0", "0");
                //if (res == false)
                //{
                //    Page_Load(sender, e);
                //}
            }
            else if (LstPallet131.Count > 0)
            {
                MyObj = LstPallet131[0];
                Session["originalPallet"] = MyObj.PALLETID.ToString();
                lblPalletID.Text = MyObj.PALLETID.ToString();
                lblItemID.Text = MyObj.ITEM.ToString();
                HttpContext.Current.Session["ITEM"] = MyObj.ITEM.ToString();
                lblItemDesc.Text = MyObj.DESCRIPTION.ToString();
                LblLotId.Text = MyObj.LOT.ToString();
                lblWarehouse.Text = MyObj.WRH.ToString();
                lblWareDescr.Text = MyObj.DESCWRH.ToString();
                lbllocation.Text = MyObj.LOCA.ToString();
                lblQuantity.Text = MyObj.QTY.ToString();
                HttpContext.Current.Session["QTY"] = MyObj.QTY.ToString();
                lblQuantityDesc.Text = MyObj.UN.ToString();
                lblOORG.Text = MyObj.OORG.ToString();
                lblORNO.Text = MyObj.ORNO.ToString();
                lblOSET.Text = MyObj.OSET.ToString();
                lblPONO.Text = MyObj.PONO.ToString();
                lblSQNB.Text = MyObj.SQNB.ToString();
                lblADVS.Text = MyObj.ADVS.ToString() + " PRIO:" + MyObj.PRIO.ToString() + " PONO:" + MyObj.PONO.ToString() + " ORNO:" + MyObj.ORNO.ToString();
                ADVS = MyObj.ADVS.ToString();
                flag131 = 1;
                twhcolDAL.actRegtticol082140(_operator, " ", " ", 5, " ", MyObj.OORG.ToString(), MyObj.ORNO.ToString(), MyObj.OSET.ToString(), MyObj.PONO.ToString(), MyObj.SQNB.ToString(), MyObj.ADVS.ToString());

                ////twhcolDAL.actRegtticol082140(_operator, MyObj.PALLETID.ToString(), MyObj.LOCA.ToString(), 5, maximo, MyObj.OORG.ToString(), MyObj.ORNO.ToString(), MyObj.OSET.ToString(), MyObj.PONO.ToString(), MyObj.SQNB.ToString(), MyObj.ADVS.ToString());
                //bool res = twhcolDAL.InsertarTccol307140(_operator, "1", MyObj.PALLETID.ToString(), "7", "0", "0");
                //if (res == false)
                //{
                //    Page_Load(sender, e);
                //}
            }
            if ((LstPallet22.Count == 0) && (LstPallet042.Count == 0) && (LstPallet131.Count == 0) && (DTttccol307.Rows.Count == 0))
            {
                mensaje = thereisnotPalletavailable;
                //Response.Write("<script language=javascript>alert('" + mensaje + "');window.location = '/WebPages/Login/whMenuI.aspx';</script>");

            }

        }


    }
}