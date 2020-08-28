using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.Configuration;
using whusa.Entidades;
using System.Globalization;
using whusa.Interfases;
using System.Data;
using whusa.Utilidades;

namespace whusap.WebPages.InvLogistica
{
    public partial class whInvSugUbicacion : System.Web.UI.Page
    {
        #region Propiedades
            private static InterfazDAL_ttccol301 _idalttccol301 = new InterfazDAL_ttccol301();
            private static InterfazDAL_ttisfc001 _idalttisfc001 = new InterfazDAL_ttisfc001();
            private static InterfazDAL_twhwmd300 _idaltwhwmd300 = new InterfazDAL_twhwmd300();
            private static InterfazDAL_tqmptc011 _idaltqmptc011 = new InterfazDAL_tqmptc011();
            private static InterfazDAL_ttcibd001 _idalttcibd001 = new InterfazDAL_ttcibd001();
            private static InterfazDAL_tticol030 _idaltticol030 = new InterfazDAL_tticol030();
            private static InterfazDAL_tticol022 _idaltticol022 = new InterfazDAL_tticol022();
            private static Mensajes _mensajesForm = new Mensajes();
            private static LabelsText _textoLabels = new LabelsText();
            private static string _operator;
            public static string _idioma;
            private static string strError;
            private static string formName;
            private static string globalMessages = "GlobalMessages";
            private static DataTable _validaAlmacen = new DataTable();
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

                    CargarIdioma();



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
                }
            }

        protected void btnConsultar_Click(object sender, EventArgs e)
            {
                if (txtNumeroOrden.Text != String.Empty)
                {
                    txtLocation.Text = String.Empty;
                    var numeroOrden = txtNumeroOrden.Text.ToUpper();

                    var consultaRegistro = _idalttisfc001.findByOrderNumberSugUbicacion(ref numeroOrden, ref strError);

                    if (consultaRegistro.Rows.Count > 0)
                    {
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

        protected void btnGuardar_Click(object sender, EventArgs e)
            {
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
                var dsca = lblValueArticulo.Text.Split('-')[2].Trim().ToUpper();
                var validaUbicacion = _idaltqmptc011.findRecordByItem(ref item, ref strError);

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
                    consultaOrden = _idalttisfc001.findBySqnbPdnoNoClot(ref location, ref cwar, ref numeroOrden, ref numeroOrdenPallet, ref strError);
                }
                else
                {
                    consultaOrden = _idalttisfc001.findBySqnbPdnoYesClot(ref location, ref cwar, ref numeroOrden, ref numeroOrdenPallet, ref strError);
                }

                if (consultaOrden.Rows.Count > 0)
                {
                    var QTYPEND = float.Parse(consultaOrden.Rows[0]["QTYPEND"].ToString()).ToString("0.00");
                    var QTDL = float.Parse(lblValueQuantity.Text == "" ? "0" : lblValueQuantity.Text).ToString("0.00");

                    //var STRQDPU = float.Parse((float.Parse(QTYPEND) - float.Parse(QTDL)).ToString()).ToString("0.00");
                    var STRQDPU = float.Parse(QTYPEND).ToString("0.00");
                    var CWAR = consultaOrden.Rows[0]["CWAR"].ToString();

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
                        
                        //Validar que la cantidad no sea mayor a la cantidad pendiente
                        if (float.Parse(QTDL) > float.Parse(QTYPEND))
                        {
                            lblError.Text = String.Format(mensajes("suggestedquantityannounced"), QTDL, QTYPEND);
                            return;
                        }


                        //Validar que la cantidad ingresada no sea mayor a la cantidad disponible por ubicar
                        if (float.Parse(QTDL) > float.Parse(STRQDPU))
                        {
                            lblError.Text = String.Format(mensajes("suggestedquantityavailable"), numeroOrden, QTDL, STRQDPU);
                            return;
                        }

                        //Verificar si ya existe un registro para esta orden
                        var consecutivo = _idaltticol030.consultarConsecutivoRegistro(ref numeroOrden, ref strError);

                        consecutivo = consecutivo + 1;

                        Ent_tticol030 data030 = new Ent_tticol030()
                        {
                            pdno = numeroOrden,
                            sqnb = consecutivo,
                            mitm = "         " + item,
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

                        List<Ent_tticol030> lista = new List<Ent_tticol030>();
                        lista.Add(data030);

                        var validSave = _idaltticol030.insertarRegistro(ref lista, ref strError);

                        if (validSave > 0)
                        {
                            Ent_tticol022 data022 = new Ent_tticol022()
                            {
                                log2 = _operator,
                                qtd2 = Convert.ToInt32(data030.qtdl),
                                loca = data030.loca,
                                pdno = data030.pdno,
                                sqnb = numeroOrdenPallet,
                                dele = 7
                            };

                            var validUpdate = _idaltticol022.actualizaRegistroSugUbicaciones(ref data022, ref strError);

                            if (validUpdate)
                            {
                                //_idaltticol022.ActualizarRegistroTicol222
                                var actubicticol222 = _idaltticol022.ActualizarUbicacionTicol222(data022.pdno, data022.sqnb, data022.loca, data030.cwar);
                                lblConfirm.Text = mensajes("msjsave");
                                divTable.Visible = false;
                                txtNumeroOrden.Text = String.Empty;
                                return;
                            }
                            else
                            {
                                lblError.Text = mensajes("errorupdt");
                                return;
                            }
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

        protected void txtLocation_OnTextChanged(object sender, EventArgs e)
            {
                if (txtLocation.Text.Trim() != "")
                {
                    var LOCA = txtLocation.Text.Trim().ToUpper();
                    var PDNO = lblValueOrden.Text.Trim().ToUpper();

                    //Buscar almacen de la ubicación
                    _validaAlmacen = _idaltwhwmd300.validaLocationWithPdno(ref LOCA, ref PDNO, ref strError);

                    if (_validaAlmacen.Rows.Count > 0)
                    {
                        lblError.Text = String.Empty;
                        lblValueLocation2.Text = txtLocation.Text.ToUpper();
                        lblValueWareHouse.Text = _validaAlmacen.Rows[0]["ALM"].ToString();
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

        #endregion

        #region Metodos

        protected void CargarIdioma()
        {
            lblNumeroOrden.Text = _textoLabels.readStatement(formName, _idioma, "lblNumeroOrden");
            btnConsultar.Text = _textoLabels.readStatement(formName, _idioma, "btnConsultar");
            lblOrden.Text = _textoLabels.readStatement(formName, _idioma, "lblOrden");
            lblArticulo.Text = _textoLabels.readStatement(formName, _idioma, "lblArticulo");
            lblWareHouse.Text = _textoLabels.readStatement(formName, _idioma, "lblWareHouse");
            lblLocation2.Text = _textoLabels.readStatement(formName, _idioma, "lblLocation2");
            lblLocation.Text = _textoLabels.readStatement(formName, _idioma, "lblLocation2");
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

        #endregion
    }
}