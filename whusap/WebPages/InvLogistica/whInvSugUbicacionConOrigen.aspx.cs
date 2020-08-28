using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using whusa.Interfases;
using System.Threading;
using System.Globalization;
using System.Configuration;
using whusa.Entidades;
using System.Data;
using whusa.Utilidades;

namespace whusap.WebPages
{
    public partial class whInvSugUbicacionConOrigen : System.Web.UI.Page
    {
        #region Propiedades
            private static InterfazDAL_ttccol301 _idalttccol301 = new InterfazDAL_ttccol301();
            private static InterfazDAL_twhcol124 _idaltwhcol124 = new InterfazDAL_twhcol124();
            private static InterfazDAL_twhwmd300 _idaltwhwmd300 = new InterfazDAL_twhwmd300();
            private static InterfazDAL_tqmptc011 _idaltqmptc011 = new InterfazDAL_tqmptc011();
            private static InterfazDAL_ttcibd001 _idalttcibd001 = new InterfazDAL_ttcibd001();
            private static InterfazDAL_tticol032 _idaltticol032 = new InterfazDAL_tticol032();
            private static InterfazDAL_twhinh210 _idaltwhinh210 = new InterfazDAL_twhinh210();
            private static DataTable _validaAlmacen = new DataTable();
            private static Mensajes _mensajesForm = new Mensajes();
            private static LabelsText _textoLabels = new LabelsText();
            private static string _operator;
            public static string _idioma;
            private static string strError;
            private static string formName;
            private static string globalMessages = "GlobalMessages";
        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
            {
                // Cambiar cultura para manejo de separador decimal
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
                    lblError.Text = "";
                    lblConfirm.Text = "";

                    if (Session["user"] == null)
                    {
                        if (Request.QueryString["Valor1"] == null || Request.QueryString["Valor1"] == "")
                        {
                            Response.Redirect(ConfigurationManager.AppSettings["UrlBase"] + "/WebPages/Login/whLogIni.aspx");
                        }
                        else
                        {
                            _operator = Request.QueryString["Valor1"];
                            Session["user"] = _operator;
                            Session["logok"] = "OKYes";
                            //txtNumeroOrden.Enabled = false;
                        }
                    }
                    else
                    {
                        _operator = Session["user"].ToString();
                    }


                    //if (Session["user"] == null)
                    //{
                    //    Response.Redirect(ConfigurationManager.AppSettings["UrlBase"] + "/WebPages/Login/whLogIni.aspx");
                    //}

                    //_operator = Session["user"].ToString();

                    try
                    {
                        _idioma = Session["ddlIdioma"].ToString();
                    }
                    catch (Exception)
                    {
                        _idioma = "INGLES";
                    }

                    CargarIdioma();

                    //if (_idioma == "ESPAÑOL")
                    //{
                    //    IdiomaEspañol();
                    //}
                    //else
                    //{
                    //    IdiomaIngles();
                    //}

                    string strTitulo = mensajes("encabezado");
                    control.Text = strTitulo;

                    Ent_ttccol301 data = new Ent_ttccol301()
                    {
                        user = _operator,
                        come = mensajes("encabezado"),
                        refcntd = 0,
                        refcntu = 0
                    };

                    List<Ent_ttccol301> datalog = new List<Ent_ttccol301>();
                    datalog.Add(data);

                    _idalttccol301.insertarRegistro(ref datalog, ref strError);

                    cargarOrigen();
                }
            }

        protected void btnConsultar_Click(object sender, EventArgs e)
            {
                lblError.Text = String.Empty;
                lblConfirm.Text = String.Empty;

                if (txtNumeroOrden.Text != String.Empty || slOrigen.SelectedValue == "")
                {
                    txtLocation.Text = String.Empty;
                    var numeroOrden = txtNumeroOrden.Text.ToUpper();
                    var origen = slOrigen.SelectedValue;

                    var consultaRegistro = _idaltwhcol124.findByOrderNumberSugUbicacionConOrigen(ref numeroOrden, ref origen, ref strError);

                    if (consultaRegistro.Rows.Count > 0)
                    {
                        var cwar = consultaRegistro.Rows[0]["WHTA"].ToString();
                        var orno = consultaRegistro.Rows[0]["SHPO"].ToString();

                        var validaLocalizacion = _idaltticol032.consultaPorPalletBodegas(ref numeroOrden, ref cwar, ref orno, ref strError);

                        if (validaLocalizacion.Rows.Count > 0)
                        {
                            switch (validaLocalizacion.Rows[0]["ORG"].ToString())
                            {
                                case "ticol032":
                                    lblError.Text = mensajes("palletinprocess");
                                    return;
                                case "ticol033":
                                    lblError.Text = mensajes("palletprocessed");
                                    return;
                            }
                        }
                        hdfNumeroOrdenPallet.Value = numeroOrden;
                        lblValueOrden.Text = consultaRegistro.Rows[0]["PDNO"].ToString();
                        lblValueArticulo.Text = consultaRegistro.Rows[0]["MITM"].ToString()
                                            + " - " + consultaRegistro.Rows[0]["DSCA"].ToString();
                        lblValueWareHouse.Text = String.Empty;
                        lblValueLocation2.Text = String.Empty;
                        lblValuePorAprobar.Text = consultaRegistro.Rows[0]["STPND"].ToString();
                        lblValueQuantity.Text = consultaRegistro.Rows[0]["QTDL"].ToString();
                        var factor = consultaRegistro.Rows[0]["CONV"].ToString();
                        lblValueQuantityPLT.Text = factor == String.Empty ? Math.Round((float.Parse(lblValueQuantity.Text) / 1), 2).ToString()
                                                                        : Math.Round((float.Parse(lblValueQuantity.Text) / float.Parse(factor)), 2).ToString();
                        lblValueUnit.Text = consultaRegistro.Rows[0]["CUNI"].ToString();

                        divTable.Visible = true;
                        txtNumeroOrden.Enabled = false;
                    }
                    else
                    {
                        lblError.Text = mensajes("orderempty");
                        return;
                    }
                }
                else
                {
                    lblError.Text = mensajes("numberempty");
                    return;
                }
            }

        protected void txtLocation_OnTextChanged(object sender, EventArgs e)
            {
                lblError.Text = String.Empty;
                lblConfirm.Text = String.Empty;

                if (txtLocation.Text.Trim() != "")
                {
                    var LOCA = txtLocation.Text.Trim().ToUpper();
                    var PDNO = hdfNumeroOrdenPallet.Value.Trim().ToUpper();

                    //Buscar almacen de la ubicación
                    _validaAlmacen = _idaltwhwmd300.validaLocationWithPdnoConOrigen(ref LOCA, ref PDNO, ref strError);

                    if (_validaAlmacen.Rows.Count > 0)
                    {
                        var loca = txtLocation.Text.ToUpper();
                        var cwar = _validaAlmacen.Rows[0]["ALM"].ToString();

                        lblError.Text = String.Empty;
                        lblValueLocation2.Text = loca;
                        lblValueWareHouse.Text = cwar;
                    }
                    else
                    {
                        lblError.Text = String.Format(mensajes("warehouseempty"), PDNO);
                        return;
                    }
                }
                else
                {
                    lblLocation2.Text = String.Empty;
                    lblValueWareHouse.Text = String.Empty;
                }
            }

        protected void btnGuardar_Click(object sender, EventArgs e)
            {
                lblError.Text = String.Empty;
                lblConfirm.Text = String.Empty;

                if (txtLocation.Text.Trim() == String.Empty || lblValueLocation2.Text.Trim() == String.Empty)
                {
                    lblError.Text = mensajes("locationblank");
                    return;
                }

                if (float.Parse(lblValueQuantity.Text) < 1)
                {
                    lblError.Text = mensajes("quantityzero");
                    return;
                }

                //Valida la ubicacion del producto (INS o REC)
                var item = lblValueArticulo.Text.Split('-')[0].Trim().ToUpper() + "-" + lblValueArticulo.Text.Split('-')[1].Trim().ToUpper();
                var origen = slOrigen.SelectedValue;
                var dsca = lblValueArticulo.Text.Split('-')[2].Trim().ToUpper();
                var validaUbicacion = _idaltqmptc011.findRecordByItemConOrigen(ref item, ref origen, ref strError);

                var location = validaUbicacion.Rows.Count > 0 ? "INS" : "REC";

                //Valida si el articulo maneja lote
                Ent_ttcibd001 data001 = new Ent_ttcibd001()
                {
                    item = item
                };

                var validaArticulo = _idalttcibd001.listaRegistro_ObtieneDescripcionUnidad(ref data001, ref strError);

                var consultaOrden = new DataTable();
                var numeroOrden = lblValueOrden.Text.Trim();
                var numeroOrdenPallet = txtNumeroOrden.Text.Trim().ToUpper();
                var clot = validaArticulo.Rows[0]["KLTC"].ToString();
                var cwar = _validaAlmacen.Rows[0]["ALM"].ToString();


                if (clot != "1")
                {
                    consultaOrden = _idaltwhcol124.findBySqnbPdnoNoClot(ref location, ref cwar, ref numeroOrdenPallet, ref strError);
                }
                else
                {
                    consultaOrden = _idaltwhcol124.findBySqnbPdnoYesClot(ref location, ref cwar, ref numeroOrdenPallet, ref strError);
                }

                if (consultaOrden.Rows.Count > 0)
                {
                    var QTYPEND = float.Parse(consultaOrden.Rows[0]["QTYPEND"].ToString()).ToString("0.00");
                    var QTDL = float.Parse(lblValueQuantity.Text == "" ? "0" : lblValueQuantity.Text).ToString("0.00");

                    var STRQDPU = float.Parse((float.Parse(QTYPEND) - float.Parse(lblValuePorAprobar.Text.Trim())).ToString()).ToString("0.00");

                    var CWAR = consultaOrden.Rows[0]["CWAR"].ToString();
                    var LOTE = consultaOrden.Rows[0]["CLOT"].ToString();
                    //Validar que el almacen sea el mismo de la orden
                    if (CWAR != cwar)
                    {
                        lblError.Text = String.Format(mensajes("diferentcwar"), txtLocation.Text.ToUpper(), CWAR);
                        return;
                    }

                    //Validar que exista ubicacion, que sea tipo 5-carga y que no este bloqueado 
                    Ent_twhwmd300 data300 = new Ent_twhwmd300()
                    {
                        loca = txtLocation.Text.Trim(),
                        cwar = cwar
                    };

                    var validarUbicacionTipo = _idaltwhwmd300.listaRegistro_AlmacenUbicaion(ref data300, ref strError);

                    if (validarUbicacionTipo.Rows.Count > 0)
                    {
                        if (float.Parse(QTDL) > float.Parse(QTYPEND))
                        {
                            lblError.Text = String.Format(mensajes("suggestedquantityannounced"), QTDL, QTYPEND);
                            return;
                        }

                        //
                        //Validar que la cantidad ingresada no sea mayor a la cantidad disponible por ubicar
                        if (float.Parse(QTDL) > float.Parse(STRQDPU))
                        {
                            lblError.Text = String.Format(mensajes("suggestedquantityavailable"), numeroOrden, QTDL, STRQDPU);
                            return;
                        }

                        //Verificar si ya existe un registro para esta orden
                        //var consecutivo = _idaltticol032.consultarConsecutivoRegistro(ref numeroOrden, ref strError);

                        //consecutivo = consecutivo + 1;

                        Ent_twhinh210 datawhinh210 = new Ent_twhinh210()
                        {
                            orno = lblValueOrden.Text,
                            oorg = origen,
                            item = item,
                            clot = LOTE
                        };

                        var consultaDatosOrden = _idaltwhinh210.findByOrderNumberAndOrigin(ref datawhinh210, ref strError);

                        if (consultaDatosOrden.Rows.Count < 1)
                        {
                            lblError.Text = mensajes("210empty");
                            return;
                        }

                        //T$OORG AS OORG, T$ORNO AS ORNO, T$OSET AS OSET, T$PONO AS PONO, T$SEQN AS SEQN
                        var oorg = consultaDatosOrden.Rows[0]["OORG"].ToString();
                        var orno = consultaDatosOrden.Rows[0]["ORNO"].ToString();
                        var oset = consultaDatosOrden.Rows[0]["OSET"].ToString();
                        var pono = consultaDatosOrden.Rows[0]["PONO"].ToString();
                        var seqn = consultaDatosOrden.Rows[0]["SEQN"].ToString();

                        Ent_tticol032 dataticol032 = new Ent_tticol032()
                        {
                            oorg = Convert.ToInt32(oorg),
                            orno = orno,
                            sqnb = numeroOrdenPallet,
                            oset = Convert.ToInt32(oset),
                            pono = Convert.ToInt32(pono),
                            seqn = Convert.ToInt32(seqn),
                            mitm = item.Trim(),
                            dsca = dsca,
                            cwar = cwar,
                            loca = lblValueLocation2.Text,
                            qtdl = Convert.ToDecimal(QTDL),
                            cuni = lblValueUnit.Text,
                            mess = " ",
                            user = _operator,
                            refcntd = 0,
                            refcntu = 0



                        };

                        var validSave = _idaltticol032.insertarRegistro(ref dataticol032, ref strError);

                        if (validSave > 0)
                        {
                            lblConfirm.Text = mensajes("msjsave");


                            divTable.Visible = false;
                            txtNumeroOrden.Enabled = true;
                            txtNumeroOrden.Text = String.Empty;
                            return;
                        }
                        else
                        {
                            lblError.Text = mensajes("errorsave");
                            return;
                        }
                    }
                    else
                    {
                        lblError.Text = String.Format(mensajes("locationnotexist"), data300.loca);
                        return;
                    }
                }
                else
                {
                    lblError.Text = mensajes("orderemptytwo");
                }
            }

        #endregion

        #region Metodos

        protected void CargarIdioma()
        {
            lblOrigen.Text = _textoLabels.readStatement(formName, _idioma, "lblOrigen");
            lblNumeroOrden.Text = _textoLabels.readStatement(formName, _idioma, "lblNumeroOrden");
            btnConsultar.Text = _textoLabels.readStatement(formName, _idioma, "btnConsultar");
            lblOrden.Text = _textoLabels.readStatement(formName, _idioma, "lblOrden");
            lblArticulo.Text = _textoLabels.readStatement(formName, _idioma, "lblArticulo");
            lblWareHouse.Text = _textoLabels.readStatement(formName, _idioma, "lblWareHouse");
            lblLocation.Text = _textoLabels.readStatement(formName, _idioma, "lblLocation");
            lblLocation2.Text = _textoLabels.readStatement(formName, _idioma, "lblLocation2");
            lblPorAprobar.Text = _textoLabels.readStatement(formName, _idioma, "lblPorAprobar");
            lblQuantity.Text = _textoLabels.readStatement(formName, _idioma, "lblQuantity");
            lblQuantityPLT.Text = _textoLabels.readStatement(formName, _idioma, "lblQuantityPLT");
            lblUnit.Text = _textoLabels.readStatement(formName, _idioma, "lblUnit");
            btnGuardar.Text = _textoLabels.readStatement(formName, _idioma, "btnGuardar");
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

        protected void cargarOrigen()
        {
            ListItem itemS = new ListItem()
            {
                Text = _idioma == "INGLES" ? "-- Select an option -- " : " -- Seleccione --",
                Value = ""
            };

            ListItem item1 = new ListItem()
            {
                Text = _idioma == "INGLES" ? "Purchase" : "Compras",
                Value = "2"
            };

            ListItem item2 = new ListItem()
            {
                Text = _idioma == "INGLES" ? "Sales" : "Ventas",
                Value = "1"
            };

            ListItem item3 = new ListItem()
            {
                Text = _idioma == "INGLES" ? "Manufacturing" : "Manufactura",
                Value = "4"
            };

            ListItem item4 = new ListItem()
            {
                Text = _idioma == "INGLES" ? "Supplies" : "Suministros",
                Value = "21"
            };

            ListItem item5 = new ListItem()
            {
                Text = _idioma == "INGLES" ? "Transfer" : "Transferencia",
                Value = "22"
            };

            slOrigen.Items.Insert(0, itemS);
            slOrigen.Items.Insert(1, item1);
            slOrigen.Items.Insert(2, item2);
            slOrigen.Items.Insert(3, item3);
            slOrigen.Items.Insert(4, item4);
            slOrigen.Items.Insert(5, item5);
        }

        #endregion
    }
}




