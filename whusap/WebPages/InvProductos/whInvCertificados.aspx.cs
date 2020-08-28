using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.Globalization;
using whusa.Interfases;
using System.Data;
using whusa.Entidades;
using whusa.Entidades.Documentos;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Configuration;

namespace whusap.WebPages.InvProductos
{
    public partial class whInvCertificados : System.Web.UI.Page
    {
        #region Propiedades
            InterfazDAL_twhinh430 _idaltwhinh430 = new InterfazDAL_twhinh430();
            InterfazDAL_tcisli205 _idaltcisli205 = new InterfazDAL_tcisli205();
            Interfaz_Documentos _idalDocumentos = new Interfaz_Documentos();
            InterfazDAL_ttccol301 _idalttccol301 = new InterfazDAL_ttccol301();
            InterfazDAL_tticol180 _idaltticol180 = new InterfazDAL_tticol180();
            private static string _idioma = String.Empty;
            private static string _operator = String.Empty;
            private static string _documento = String.Empty;
            public string strError;
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
                    Label control = (Label)Page.Controls[0].FindControl("lblPageTitle");
                    lblError.Text = "";
                    lblConfirm.Text = "";

                    //Valida inicio sesion
                    if (Session["user"] == null)
                    {
                        Response.Redirect(ConfigurationManager.AppSettings["UrlBase"] + "/WebPages/Login/whLogIni.aspx");
                    }

                    //Valida idioma seleccionado
                    if (Session["ddlIdioma"] == null)
                    {
                        Session["ddlIdioma"] = "INGLES";
                    }
                    _operator = Session["user"].ToString();
                    _idioma = Session["ddlIdioma"].ToString();

                    //Carga idioma
                    if (_idioma == "ESPAÑOL")
                    {
                        IdiomaEspañol();
                    }
                    else
                    {
                        IdiomaIngles();
                    }

                    string strTitulo = mensajes("encabezado");
                    control.Text = strTitulo;

                    //Guarda log de ingreso
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

            protected void btnGenerateCertificate_Click(object sender, EventArgs e)
            {
                if (txtNumeroDocumento.Text != String.Empty)
                {
                    _documento = txtNumeroDocumento.Text.Trim().ToUpper();
                    lblConfirm.Text = "";
                    lblError.Text = "";
                    var validTipoDocumento = new DataTable();
                    var numeroDocumento = txtNumeroDocumento.Text.Trim().ToUpper();
                    var tipoDocumento = ddlTipoDocumento.SelectedValue.ToString();
                    List<string> pdfCreados = new List<string>();

                    //Valida que exista número documento
                    switch (ddlTipoDocumento.SelectedValue)
                    {
                        case "ENVIO":
                            validTipoDocumento = _idaltwhinh430.findByDocumentNumber(ref numeroDocumento, ref strError);
                            break;
                        case "FACTURA":
                            validTipoDocumento = _idaltcisli205.findByDocumentNumber(ref numeroDocumento, ref strError);
                            break;
                        default:
                            lblError.Text = mensajes("tipoDocumentoBlanco");
                            return;
                    }

                    if (validTipoDocumento.Rows.Count < 1)
                    {
                        switch (ddlTipoDocumento.SelectedValue)
                        {
                            case "ENVIO":
                                lblConfirm.Text = "";
                                lblError.Text = mensajes("envio");
                                return;
                            case "FACTURA":
                                lblConfirm.Text = "";
                                lblError.Text = mensajes("factura");
                                return;
                        }
                    }

                    //Valida tipo de cliente // 1 Certificado Nacional // 2 Certificado importación
                    if (validTipoDocumento.Rows[0]["TYBP"].ToString() == "1")
                    {
                        //Obtiene información para armar documento
                        var documentos = _idalDocumentos.getInformationDocuments(ref numeroDocumento, ref tipoDocumento, ref strError);

                        if (documentos.Count > 0)
                        {
                            //Crea documentos
                            foreach (var item in documentos)
                            {
                                if (item.informacionEncabezado != null && item.informacionLote != null && item.informacionProducto.Count > 0)
                                {
                                    var namePdf = crearPDFNacional(item, ddlTipoDocumento.SelectedValue.ToString().ToUpper());

                                    //Contador de documentos creados
                                    pdfCreados.Add(namePdf);
                                }
                            }
                        }

                        lblError.Text = "";
                        lblConfirm.Text = String.Format(mensajes("confirm"), pdfCreados.Count);
                        txtNumeroDocumento.Text = "";
                        txtNumeroDocumento.Focus();
                        return;
                    }
                    else if (validTipoDocumento.Rows[0]["TYBP"].ToString() == "2")
                    {
                        var documentos = _idalDocumentos.getInformationDocuments(ref numeroDocumento, ref tipoDocumento, ref strError);

                        if (documentos.Count > 0)
                        {
                            foreach (var item in documentos)
                            {
                                if (item.informacionEncabezado != null && item.informacionLote != null && item.informacionProducto.Count > 0)
                                {


                                    var namePdf = crearPDFInternational(item, ddlTipoDocumento.SelectedValue.ToString().ToUpper());

                                    pdfCreados.Add(namePdf);
                                }
                            }
                        }

                        lblError.Text = "";
                        lblConfirm.Text = String.Format(mensajes("confirm"), pdfCreados.Count);
                        txtNumeroDocumento.Text = "";
                        txtNumeroDocumento.Focus();
                    }
                }
                else
                {
                    lblError.Text = mensajes("numeroDocumentoBlanco");
                    return;
                }
            }

            protected string crearPDFNacional(Ent_DocumentoNacional item, string tipoCertificado)
            {
                //Margenes
                Document doc = new Document(PageSize.LETTER);
                doc.SetMargins(70f, 70f, 70f, 70f);

                //Ruta de archivos utilizados
                var path = ConfigurationManager.AppSettings["SaveDocQuality"].ToString();
                var folderImages = path + "Images\\logo.png";
                var folderImages2 = path + "Images\\logoPPM.jpg";

                //Valida si existe la ruta
                if (!System.IO.Directory.Exists(path))
                {
                    System.IO.Directory.CreateDirectory(path);
                }

                //Valida tipo de documento para creación de carpeta
                if (tipoCertificado == "FACTURA")
                {
                    path = path + "Factura\\";
                }
                else if (tipoCertificado == "ENVIO")
                {
                    path = path + "Envio\\";
                }

                //Valida si existe ruta con tipo documento
                if (!System.IO.Directory.Exists(path))
                {
                    System.IO.Directory.CreateDirectory(path);
                }

                //Añade al path el documento de consulta
                var folderPdf = tipoCertificado == "FACTURA" ? path + txtNumeroDocumento.Text.Trim() : path + item.informacionEncabezado.shmp.Trim();

                //Valida si existe path
                if (!System.IO.Directory.Exists(folderPdf))
                {
                    System.IO.Directory.CreateDirectory(folderPdf);
                }

                //Crea nombre de archivo y lo añade al path
                var nameFile = item.informacionLote.numeroLote.Trim() + '-' + item.informacionLote.iorn.Trim() + ".pdf";
                var pathPdf = folderPdf + "\\" + nameFile;

                //Valida si existe el path
                if (System.IO.Directory.Exists(pathPdf))
                {
                    System.IO.Directory.Delete(pathPdf);
                }

                //Crear el path y empieza la creación del documento
                using (var output = new FileStream(pathPdf, FileMode.Create))
                {
                    //Obtiene el documento
                    var writer = PdfWriter.GetInstance(doc, output);

                    doc.AddTitle("Generador de PDF");
                    doc.AddCreator("Grupo Phoenix");

                    // Abrimos el archivo
                    doc.Open();

                    iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    iTextSharp.text.Font _standardFontBold = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    iTextSharp.text.Font _standardFontItalic = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, iTextSharp.text.Font.ITALIC, BaseColor.BLACK);

                    //Creamos tabla para logos - encabezado
                    PdfPTable tblEncabezado = new PdfPTable(2);
                    tblEncabezado.WidthPercentage = 100;


                    iTextSharp.text.Image pic = iTextSharp.text.Image.GetInstance(folderImages, false);
                    pic.ScaleAbsolute(80f, 30f);


                    iTextSharp.text.Image pic2 = iTextSharp.text.Image.GetInstance(folderImages2, false);
                    pic2.ScaleAbsolute(80f, 30f);

                    //Llenamos info con logos y texto
                    PdfPCell clLogoNombrePhoenix = new PdfPCell(pic2);
                    clLogoNombrePhoenix.BorderWidth = 0;

                    //Celda para solo logo
                    PdfPCell clLogoPhoenix = new PdfPCell(new Phrase(""));
                    clLogoPhoenix.BorderWidth = 0;
                    clLogoPhoenix.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;

                    tblEncabezado.AddCell(clLogoNombrePhoenix);
                    tblEncabezado.AddCell(clLogoPhoenix);

                    //Añadimos tabla al documento
                    doc.Add(tblEncabezado);


                    //Tabla con informacion del cliente
                    PdfPTable tblInformacionCliente = new PdfPTable(2);
                    tblInformacionCliente.WidthPercentage = 100;
                    float[] widths = new float[] { 0.9f, 2.1f };
                    tblInformacionCliente.SetWidths(widths);
                    //Celda fecha
                    PdfPCell clFecha = new PdfPCell(new Phrase("Fecha " + DateTime.Now.ToShortDateString(), _standardFont));
                    clFecha.BorderWidth = 0.75f;

                    //Celda Cliente
                    PdfPCell clCliente = new PdfPCell(new Phrase("Cliente: " + item.informacionEncabezado.codigoCliente + " " + item.informacionEncabezado.cliente, _standardFont));
                    clCliente.BorderWidth = 0.75f;

                    //Celda Articulo
                    PdfPCell clArticulo = new PdfPCell(new Phrase("Articulo: " + item.informacionLote.articulo, _standardFont));
                    clArticulo.BorderWidth = 0;
                    clArticulo.BorderWidthBottom = 0.75f;
                    clArticulo.BorderWidthLeft = 0.75f;


                    //Celda Descripcion Articulo
                    PdfPCell clDesArticulo = new PdfPCell(new Phrase(item.informacionLote.descripcionArticulo, _standardFont));
                    clDesArticulo.BorderWidth = 0;
                    clDesArticulo.BorderWidthBottom = 0.75f;
                    clDesArticulo.BorderWidthRight = 0.75f;

                    //Celda orden de fabricación
                    PdfPCell clOrdenFabricacion = new PdfPCell(new Phrase("Orden de Fabricación: " + item.informacionLote.numeroLote, _standardFont));
                    clOrdenFabricacion.BorderWidth = 0;
                    clOrdenFabricacion.BorderWidthBottom = 0.75f;
                    clOrdenFabricacion.BorderWidthRight = 0.75f;
                    clOrdenFabricacion.BorderWidthLeft = 0.75f;
                    clOrdenFabricacion.Colspan = 2;

                    PdfPCell clBlankNoBorder = new PdfPCell(new Phrase(" ", _standardFont));
                    clBlankNoBorder.BorderWidth = 0;
                    clBlankNoBorder.Colspan = 2;

                    PdfPCell clNombreDocumento = new PdfPCell(new Phrase("CERTIFICADO DE CALIDAD", _standardFontBold));
                    clNombreDocumento.BorderWidth = 0;
                    clNombreDocumento.Colspan = 2;
                    clNombreDocumento.HorizontalAlignment = PdfPCell.ALIGN_CENTER;

                    //Añadimos celdas a la tabla
                    tblInformacionCliente.AddCell(clNombreDocumento);
                    tblInformacionCliente.AddCell(clBlankNoBorder);
                    tblInformacionCliente.AddCell(clFecha);
                    tblInformacionCliente.AddCell(clCliente);
                    tblInformacionCliente.AddCell(clArticulo);
                    tblInformacionCliente.AddCell(clDesArticulo);
                    tblInformacionCliente.AddCell(clOrdenFabricacion);

                    //Añadimos tabla al doc
                    doc.Add(tblInformacionCliente);

                    //Tabla producto
                    PdfPTable tblProducto = new PdfPTable(6);
                    tblProducto.WidthPercentage = 100;
                    float[] widthsProducto = new float[] { 30f, 7.5f, 7.5f, 25f, 10f, 20f };
                    tblProducto.SetWidths(widthsProducto);

                    PdfPCell clLimitesBlank = new PdfPCell(new Phrase(" ", _standardFont));
                    clLimitesBlank.Colspan = 3;
                    clLimitesBlank.BorderWidth = 0;
                    //
                    PdfPCell clLimites = new PdfPCell(new Phrase("LIMITES DE ESPECIFICACIÓN", _standardFontBold));
                    clLimites.Colspan = 3;
                    clLimites.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    clLimites.BorderWidth = 0;
                    //
                    tblProducto.AddCell(clLimitesBlank);
                    tblProducto.AddCell(clLimites);

                    //Agregar encabezados tabla
                    PdfPCell clEncCaracteristicas = new PdfPCell(new Phrase("CARACTERISTICAS DEL PRODUCTO", _standardFontBold));
                    clEncCaracteristicas.BorderWidth = 0;

                    PdfPCell clEncResultados = new PdfPCell(new Phrase("RESULTADOS", _standardFontBold));
                    clEncResultados.BorderWidth = 0;
                    clEncResultados.Colspan = 2;
                    clEncResultados.HorizontalAlignment = PdfPCell.ALIGN_CENTER;

                    PdfPCell clEncLIE = new PdfPCell(new Phrase("LIE", _standardFontBold));
                    clEncLIE.BorderWidth = 0;

                    PdfPCell clEncLSE = new PdfPCell(new Phrase("LSE", _standardFontBold));
                    clEncLSE.BorderWidth = 0;

                    PdfPCell clEncMuestra = new PdfPCell(new Phrase("Muestra", _standardFontBold));
                    clEncMuestra.BorderWidth = 0;


                    tblProducto.AddCell(clEncCaracteristicas);
                    tblProducto.AddCell(clEncResultados);
                    tblProducto.AddCell(clEncLIE);
                    tblProducto.AddCell(clEncLSE);
                    tblProducto.AddCell(clEncMuestra);

                    foreach (var producto in item.informacionProducto)
                    {
                        PdfPCell clCarateristicas = new PdfPCell(new Phrase(producto.descripcionCaracteristica, _standardFont));
                        clCarateristicas.BorderWidth = 0;

                        PdfPCell clResultados = new PdfPCell(new Phrase(producto.resultados, _standardFont));
                        clResultados.BorderWidth = 0;

                        PdfPCell clUnidad = new PdfPCell(new Phrase(producto.unidad, _standardFont));
                        clUnidad.BorderWidth = 0;

                        PdfPCell clLIE = new PdfPCell(new Phrase(producto.limiteInferior, _standardFont));
                        clLIE.BorderWidth = 0;

                        PdfPCell clLSE = new PdfPCell(new Phrase(producto.limiteSuperior, _standardFont));
                        clLSE.BorderWidth = 0;

                        PdfPCell clMuestra = new PdfPCell(new Phrase(producto.muestra, _standardFont));
                        clMuestra.BorderWidth = 0;

                        tblProducto.AddCell(clCarateristicas);
                        tblProducto.AddCell(clResultados);
                        tblProducto.AddCell(clUnidad);
                        tblProducto.AddCell(clLIE);
                        tblProducto.AddCell(clLSE);
                        tblProducto.AddCell(clMuestra);
                    }

                    doc.Add(tblProducto);

                    //Linea blanco
                    doc.Add(new Paragraph("\n"));

                    //Tabla informacion
                    PdfPTable tblInformacion = new PdfPTable(4);
                    tblInformacion.WidthPercentage = 100;
                    float[] widthsInformacion = new float[] { 30f, 15f, 25f, 30f };
                    tblInformacion.SetWidths(widthsInformacion);

                    foreach (var producto in item.informacionProducto)
                    {
                        if (producto.atributos.Count > 0)
                        {
                            var descripcionTotal = String.Empty;

                            foreach (var itematr in producto.atributos)
                            {
                                descripcionTotal += itematr;
                            }

                            var lineas = descripcionTotal.Split('\n');

                            for (int i = 0; i < lineas.Length; i++)
                            {
                                if (i == 0)
                                {
                                    //Agregar titulo tabla
                                    PdfPCell clTitutoInf = new PdfPCell(new Phrase(lineas[i], _standardFont));
                                    clTitutoInf.BorderWidth = 0;
                                    clTitutoInf.Colspan = 3;

                                    //Agregar encabezados tabla
                                    PdfPCell clEncBlankInf = new PdfPCell(new Phrase(" ", _standardFont));
                                    clEncBlankInf.BorderWidth = 0;
                                    clEncBlankInf.Colspan = 2;

                                    tblInformacion.AddCell(clTitutoInf);
                                    tblInformacion.AddCell(clEncBlankInf);
                                }
                                else if (i == 1)
                                {
                                    var encabezado = lineas[i].Split(' ');

                                    foreach (var itemenc in encabezado)
                                    {
                                        if (itemenc != String.Empty)
                                        {
                                            PdfPCell clEncInf = new PdfPCell(new Phrase(itemenc.Trim(), _standardFont));
                                            clEncInf.BorderWidth = 0;

                                            tblInformacion.AddCell(clEncInf);
                                        }
                                    }
                                }
                                else 
                                { 
                                    var info = lineas[i].Split(new string[] { "AC" }, StringSplitOptions.None);

                                    foreach (var iteminfo in info)
                                    {
                                        if (iteminfo.Trim() != String.Empty)
                                        {
                                            PdfPCell clCellInf1 = new PdfPCell(new Phrase(iteminfo.Trim(), _standardFont));
                                            clCellInf1.BorderWidth = 0;
                                            PdfPCell clCellInf12 = new PdfPCell(new Phrase("AC", _standardFont));
                                            clCellInf12.BorderWidth = 0;

                                            tblInformacion.AddCell(clCellInf1);
                                            tblInformacion.AddCell(clCellInf12);
                                        }
                                    }
                                }
                            }
                        }
                    }

                    ////Agregar titulo tabla
                    //PdfPCell clTitutoInf = new PdfPCell(new Phrase("INSPECCIÓN DE ATRIBUTOS INYECCIÓN ENVASE", _standardFont));
                    //clTitutoInf.BorderWidth = 0;
                    //clTitutoInf.Colspan = 2;

                    ////Agregar encabezados tabla
                    //PdfPCell clEncBlankInf = new PdfPCell(new Phrase(" ", _standardFont));
                    //clEncBlankInf.BorderWidth = 0;
                    //clEncBlankInf.Colspan = 3;

                    ////Agregar encabezados tabla
                    //PdfPCell clEncInf = new PdfPCell(new Phrase(" ", _standardFont));
                    //clEncInf.BorderWidth = 0;

                    //PdfPCell clEncResultadoInf = new PdfPCell(new Phrase("RESULTADO", _standardFont));
                    //clEncResultadoInf.BorderWidth = 0;

                    //PdfPCell clEncDefectosInf = new PdfPCell(new Phrase("DEFECTOS", _standardFont));
                    //clEncDefectosInf.BorderWidth = 0;

                    //PdfPCell clEncResultado2Inf = new PdfPCell(new Phrase("RESULTADO", _standardFont));
                    //clEncResultado2Inf.BorderWidth = 0;

                    //AddEncabezados
                    //tblInformacion.AddCell(clTitutoInf);
                    //tblInformacion.AddCell(clEncBlankInf);
                    //tblInformacion.AddCell(clEncInf);
                    //tblInformacion.AddCell(clEncResultadoInf);
                    //tblInformacion.AddCell(clEncDefectosInf);
                    //tblInformacion.AddCell(clEncResultado2Inf);

                    //Fila uno informacion
                    //PdfPCell clCellInf1 = new PdfPCell(new Phrase("Cierre de tapa flojo o apretado", _standardFont));
                    //clCellInf1.BorderWidth = 0;
                    //PdfPCell clCellInf12 = new PdfPCell(new Phrase("AC", _standardFont));
                    //clCellInf12.BorderWidth = 0;
                    //PdfPCell clCellInf13 = new PdfPCell(new Phrase("Rebabas", _standardFont));
                    //clCellInf13.BorderWidth = 0;
                    //PdfPCell clCellInf14 = new PdfPCell(new Phrase("AC", _standardFont));
                    //clCellInf14.BorderWidth = 0;

                    //tblInformacion.AddCell(clCellInf1);
                    //tblInformacion.AddCell(clCellInf12);
                    //tblInformacion.AddCell(clCellInf13);
                    //tblInformacion.AddCell(clCellInf14);

                    ////Fila dos informacion
                    //PdfPCell clCellInf2 = new PdfPCell(new Phrase("Deforme", _standardFont));
                    //clCellInf2.BorderWidth = 0;
                    //PdfPCell clCellInf21 = new PdfPCell(new Phrase("AC", _standardFont));
                    //clCellInf21.BorderWidth = 0;
                    //PdfPCell clCellInf22 = new PdfPCell(new Phrase("Marcas de arrastre", _standardFont));
                    //clCellInf22.BorderWidth = 0;
                    //PdfPCell clCellInf23 = new PdfPCell(new Phrase("AC", _standardFont));
                    //clCellInf23.BorderWidth = 0;

                    //tblInformacion.AddCell(clCellInf2);
                    //tblInformacion.AddCell(clCellInf21);
                    //tblInformacion.AddCell(clCellInf22);
                    //tblInformacion.AddCell(clCellInf23);

                    ////Fila tres informacion
                    //PdfPCell clCellInf3 = new PdfPCell(new Phrase("Envase apretado", _standardFont));
                    //clCellInf3.BorderWidth = 0;
                    //PdfPCell clCellInf31 = new PdfPCell(new Phrase("AC", _standardFont));
                    //clCellInf31.BorderWidth = 0;
                    //PdfPCell clCellInf32 = new PdfPCell(new Phrase("Llorado", _standardFont));
                    //clCellInf32.BorderWidth = 0;
                    //PdfPCell clCellInf33 = new PdfPCell(new Phrase("AC", _standardFont));
                    //clCellInf33.BorderWidth = 0;

                    //tblInformacion.AddCell(clCellInf3);
                    //tblInformacion.AddCell(clCellInf31);
                    //tblInformacion.AddCell(clCellInf32);
                    //tblInformacion.AddCell(clCellInf33);

                    ////Fila cuatro informacion
                    //PdfPCell clCellInf4 = new PdfPCell(new Phrase("Envase apretado", _standardFont));
                    //clCellInf4.BorderWidth = 0;
                    //PdfPCell clCellInf41 = new PdfPCell(new Phrase("AC", _standardFont));
                    //clCellInf41.BorderWidth = 0;
                    //PdfPCell clCellInf42 = new PdfPCell(new Phrase("Llorado", _standardFont));
                    //clCellInf42.BorderWidth = 0;
                    //PdfPCell clCellInf43 = new PdfPCell(new Phrase("AC", _standardFont));
                    //clCellInf43.BorderWidth = 0;

                    //tblInformacion.AddCell(clCellInf4);
                    //tblInformacion.AddCell(clCellInf41);
                    //tblInformacion.AddCell(clCellInf42);
                    //tblInformacion.AddCell(clCellInf43);

                    //PdfPCell clBlankComplete = new PdfPCell(new Phrase(" ", _standardFont));
                    //clBlankComplete.BorderWidth = 0;
                    //clBlankComplete.Colspan = 5;

                    ////Agregar titulo tabla 2
                    //PdfPCell clTitutoInf2 = new PdfPCell(new Phrase("INSPECCIÓN DE ATRIBUTOS ENVASE IMPRESO", _standardFont));
                    //clTitutoInf2.BorderWidth = 0;
                    //clTitutoInf2.Colspan = 2;

                    ////Agregar encabezados tabla 2
                    //PdfPCell clEncBlankInf2 = new PdfPCell(new Phrase(" ", _standardFont));
                    //clEncBlankInf2.BorderWidth = 0;
                    //clEncBlankInf2.Colspan = 3;

                    ////Agregar encabezados tabla parte 2
                    //PdfPCell clEncInf2 = new PdfPCell(new Phrase("DEFECTOS", _standardFont));
                    //clEncInf2.BorderWidth = 0;

                    //PdfPCell clEncResultadoInf2 = new PdfPCell(new Phrase("RESULTADO", _standardFont));
                    //clEncResultadoInf2.BorderWidth = 0;

                    //PdfPCell clEncDefectosInf2 = new PdfPCell(new Phrase("DEFECTOS", _standardFont));
                    //clEncDefectosInf2.BorderWidth = 0;

                    //PdfPCell clEncResultado2Inf2 = new PdfPCell(new Phrase("RESULTADO", _standardFont));
                    //clEncResultado2Inf2.BorderWidth = 0;

                    ////AddEncabezados2
                    //tblInformacion.AddCell(clBlankComplete);
                    //tblInformacion.AddCell(clTitutoInf2);
                    //tblInformacion.AddCell(clEncBlankInf2);
                    //tblInformacion.AddCell(clEncInf2);
                    //tblInformacion.AddCell(clEncResultadoInf2);
                    //tblInformacion.AddCell(clEncDefectosInf2);
                    //tblInformacion.AddCell(clEncResultado2Inf2);

                    ////Fila uno informacion 2
                    //PdfPCell clCell2Inf1 = new PdfPCell(new Phrase("Adherencia de tintas", _standardFont));
                    //clCell2Inf1.BorderWidth = 0;
                    //PdfPCell clCell2Inf11 = new PdfPCell(new Phrase("AC", _standardFont));
                    //clCell2Inf11.BorderWidth = 0;
                    //PdfPCell clCell2Inf12 = new PdfPCell(new Phrase("Textos ilegibles", _standardFont));
                    //clCell2Inf12.BorderWidth = 0;
                    //PdfPCell clCell2Inf13 = new PdfPCell(new Phrase("AC", _standardFont));
                    //clCell2Inf13.BorderWidth = 0;

                    //tblInformacion.AddCell(clCell2Inf1);
                    //tblInformacion.AddCell(clCell2Inf11);
                    //tblInformacion.AddCell(clCell2Inf12);
                    //tblInformacion.AddCell(clCell2Inf13);

                    ////Fila dos informacion 2
                    //PdfPCell clCell2Inf2 = new PdfPCell(new Phrase("Bolsa sucia o rota", _standardFont));
                    //clCell2Inf2.BorderWidth = 0;
                    //PdfPCell clCell2Inf21 = new PdfPCell(new Phrase("AC", _standardFont));
                    //clCell2Inf21.BorderWidth = 0;
                    //PdfPCell clCell2Inf22 = new PdfPCell(new Phrase("Repinte interno", _standardFont));
                    //clCell2Inf22.BorderWidth = 0;
                    //PdfPCell clCell2Inf23 = new PdfPCell(new Phrase("AC", _standardFont));
                    //clCell2Inf23.BorderWidth = 0;

                    //tblInformacion.AddCell(clCell2Inf2);
                    //tblInformacion.AddCell(clCell2Inf21);
                    //tblInformacion.AddCell(clCell2Inf22);
                    //tblInformacion.AddCell(clCell2Inf23);

                    ////Fila tres informacion 2
                    //PdfPCell clCell2Inf3 = new PdfPCell(new Phrase("Caja mal identificada", _standardFont));
                    //clCell2Inf3.BorderWidth = 0;
                    //PdfPCell clCell2Inf31 = new PdfPCell(new Phrase("AC", _standardFont));
                    //clCell2Inf31.BorderWidth = 0;
                    //PdfPCell clCell2Inf32 = new PdfPCell(new Phrase("Olor inherente al producto", _standardFont));
                    //clCell2Inf32.BorderWidth = 0;
                    //PdfPCell clCell2Inf33 = new PdfPCell(new Phrase("AC", _standardFont));
                    //clCell2Inf33.BorderWidth = 0;

                    //tblInformacion.AddCell(clCell2Inf3);
                    //tblInformacion.AddCell(clCell2Inf31);
                    //tblInformacion.AddCell(clCell2Inf32);
                    //tblInformacion.AddCell(clCell2Inf33);

                    ////Fila cuatro informacion 2
                    //PdfPCell clCell2Inf4 = new PdfPCell(new Phrase("Código de barras", _standardFont));
                    //clCell2Inf4.BorderWidth = 0;
                    //PdfPCell clCell2Inf41 = new PdfPCell(new Phrase("AC", _standardFont));
                    //clCell2Inf41.BorderWidth = 0;
                    //PdfPCell clCell2Inf42 = new PdfPCell(new Phrase("Manchas de tinta o grasa", _standardFont));
                    //clCell2Inf42.BorderWidth = 0;
                    //PdfPCell clCell2Inf43 = new PdfPCell(new Phrase("AC", _standardFont));
                    //clCell2Inf43.BorderWidth = 0;

                    //tblInformacion.AddCell(clCell2Inf4);
                    //tblInformacion.AddCell(clCell2Inf41);
                    //tblInformacion.AddCell(clCell2Inf42);
                    //tblInformacion.AddCell(clCell2Inf43);

                    ////Fila cinco informacion 2
                    //PdfPCell clCell2Inf5 = new PdfPCell(new Phrase("Color fuera de estándar", _standardFont));
                    //clCell2Inf5.BorderWidth = 0;
                    //PdfPCell clCell2Inf51 = new PdfPCell(new Phrase("AC", _standardFont));
                    //clCell2Inf51.BorderWidth = 0;
                    //PdfPCell clCell2Inf52 = new PdfPCell(new Phrase("Impresión rayada", _standardFont));
                    //clCell2Inf52.BorderWidth = 0;
                    //PdfPCell clCell2Inf53 = new PdfPCell(new Phrase("AC", _standardFont));
                    //clCell2Inf53.BorderWidth = 0;

                    //tblInformacion.AddCell(clCell2Inf5);
                    //tblInformacion.AddCell(clCell2Inf51);
                    //tblInformacion.AddCell(clCell2Inf52);
                    //tblInformacion.AddCell(clCell2Inf53);

                    ////Fila seis informacion 2
                    //PdfPCell clCell2Inf6 = new PdfPCell(new Phrase("Cuerpos extraños", _standardFont));
                    //clCell2Inf6.BorderWidth = 0;
                    //PdfPCell clCell2Inf61 = new PdfPCell(new Phrase("AC", _standardFont));
                    //clCell2Inf61.BorderWidth = 0;
                    //PdfPCell clCell2Inf62 = new PdfPCell(new Phrase("Impresión fuera de registro", _standardFont));
                    //clCell2Inf62.BorderWidth = 0;
                    //PdfPCell clCell2Inf63 = new PdfPCell(new Phrase("AC", _standardFont));
                    //clCell2Inf63.BorderWidth = 0;

                    //tblInformacion.AddCell(clCell2Inf6);
                    //tblInformacion.AddCell(clCell2Inf61);
                    //tblInformacion.AddCell(clCell2Inf62);
                    //tblInformacion.AddCell(clCell2Inf63);

                    ////Fila siete informacion 2
                    //PdfPCell clCell2Inf7 = new PdfPCell(new Phrase("Impresión con grumos", _standardFont));
                    //clCell2Inf7.BorderWidth = 0;
                    //PdfPCell clCell2Inf71 = new PdfPCell(new Phrase("AC", _standardFont));
                    //clCell2Inf71.BorderWidth = 0;
                    //PdfPCell clCell2Inf72 = new PdfPCell(new Phrase("Impresión descentrada", _standardFont));
                    //clCell2Inf72.BorderWidth = 0;
                    //PdfPCell clCell2Inf73 = new PdfPCell(new Phrase("AC", _standardFont));
                    //clCell2Inf73.BorderWidth = 0;

                    //tblInformacion.AddCell(clCell2Inf7);
                    //tblInformacion.AddCell(clCell2Inf71);
                    //tblInformacion.AddCell(clCell2Inf72);
                    //tblInformacion.AddCell(clCell2Inf73);

                    doc.Add(tblInformacion);

                    doc.Add(new Paragraph("\n"));

                    //Tabla informacion final
                    PdfPTable tblInformacionFinal = new PdfPTable(4);
                    tblInformacionFinal.WidthPercentage = 100;
                    tblInformacionFinal.SetWidths(widthsInformacion);

                    PdfPCell clCellFinal1 = new PdfPCell(new Phrase("NIVEL DE INSPECCIÓN GENERAL", _standardFont));
                    clCellFinal1.BorderWidth = 0;
                    PdfPCell clCellFinal11 = new PdfPCell(new Phrase("II", _standardFont));
                    clCellFinal11.BorderWidth = 0;
                    PdfPCell clCellFinal12 = new PdfPCell(new Phrase("", _standardFont));
                    clCellFinal12.BorderWidth = 0;
                    PdfPCell clCellFinal13 = new PdfPCell(new Phrase("", _standardFont));
                    clCellFinal13.BorderWidth = 0;

                    tblInformacionFinal.AddCell(clCellFinal1);
                    tblInformacionFinal.AddCell(clCellFinal11);
                    tblInformacionFinal.AddCell(clCellFinal12);
                    tblInformacionFinal.AddCell(clCellFinal13);

                    PdfPCell clCellFinal2 = new PdfPCell(new Phrase("N.C.A DEFECTOS CRÍTICOS", _standardFont));
                    clCellFinal2.BorderWidth = 0;
                    PdfPCell clCellFinal21 = new PdfPCell(new Phrase("0.4", _standardFont));
                    clCellFinal21.BorderWidth = 0;
                    PdfPCell clCellFinal22 = new PdfPCell(new Phrase("", _standardFont));
                    clCellFinal22.BorderWidth = 0;
                    PdfPCell clCellFinal23 = new PdfPCell(new Phrase("", _standardFont));
                    clCellFinal23.BorderWidth = 0;

                    tblInformacionFinal.AddCell(clCellFinal2);
                    tblInformacionFinal.AddCell(clCellFinal21);
                    tblInformacionFinal.AddCell(clCellFinal22);
                    tblInformacionFinal.AddCell(clCellFinal23);

                    PdfPCell clCellFinal3 = new PdfPCell(new Phrase("N.C.A DEFECTOS MAYORES", _standardFont));
                    clCellFinal3.BorderWidth = 0;
                    PdfPCell clCellFinal31 = new PdfPCell(new Phrase("1", _standardFont));
                    clCellFinal31.BorderWidth = 0;
                    PdfPCell clCellFinal32 = new PdfPCell(new Phrase("", _standardFont));
                    clCellFinal32.BorderWidth = 0;
                    PdfPCell clCellFinal33 = new PdfPCell(new Phrase("", _standardFont));
                    clCellFinal33.BorderWidth = 0;

                    tblInformacionFinal.AddCell(clCellFinal3);
                    tblInformacionFinal.AddCell(clCellFinal31);
                    tblInformacionFinal.AddCell(clCellFinal32);
                    tblInformacionFinal.AddCell(clCellFinal33);

                    PdfPCell clCellFinal4 = new PdfPCell(new Phrase("N.C.A DEFECTOS MENORES", _standardFont));
                    clCellFinal4.BorderWidth = 0;
                    PdfPCell clCellFinal41 = new PdfPCell(new Phrase("2.5", _standardFont));
                    clCellFinal41.BorderWidth = 0;
                    PdfPCell clCellFinal42 = new PdfPCell(new Phrase("", _standardFont));
                    clCellFinal42.BorderWidth = 0;
                    PdfPCell clCellFinal43 = new PdfPCell(new Phrase("", _standardFont));
                    clCellFinal43.BorderWidth = 0;

                    tblInformacionFinal.AddCell(clCellFinal4);
                    tblInformacionFinal.AddCell(clCellFinal41);
                    tblInformacionFinal.AddCell(clCellFinal42);
                    tblInformacionFinal.AddCell(clCellFinal43);

                    doc.Add(tblInformacionFinal);

                    var phrase1 = new Phrase();
                    phrase1.Add(new Chunk("RESULTADO FINAL DE INSPECCIÓN Y PRUEBA DE LOTE:    APROBADO\n", _standardFontBold));

                    var phrase2 = new Phrase();
                    phrase2.Add(new Chunk("Aprobado por: Aseguramiento de Calidad", _standardFontBold));

                    doc.Add(phrase1);
                    doc.Add(phrase2);

                    //Tabla informacion lote muestra
                    PdfPTable tblLoteMuestra = new PdfPTable(4);
                    tblLoteMuestra.WidthPercentage = 100;
                    tblLoteMuestra.SetWidths(widthsInformacion);

                    PdfPCell clCellLote1 = new PdfPCell(new Phrase("FECHA DE FABRICACIÓN Y/O LOTE", _standardFont));
                    clCellLote1.BorderWidth = 0;
                    PdfPCell clCellLote11 = new PdfPCell(new Phrase("", _standardFont));
                    clCellLote11.BorderWidth = 0;
                    clCellLote11.BorderWidthBottom = 0.75f;
                    PdfPCell clCellLote12 = new PdfPCell(new Phrase(item.informacionLote.numeroLote, _standardFont));
                    clCellLote12.BorderWidth = 0;
                    clCellLote12.BorderWidthBottom = 0.75f;
                    clCellLote12.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    PdfPCell clCellLote13 = new PdfPCell(new Phrase("", _standardFont));
                    clCellLote13.BorderWidth = 0;
                    clCellLote13.BorderWidthBottom = 0.75f;
                    clCellLote13.HorizontalAlignment = PdfPCell.ALIGN_CENTER;

                    tblLoteMuestra.AddCell(clCellLote1);
                    tblLoteMuestra.AddCell(clCellLote11);
                    tblLoteMuestra.AddCell(clCellLote12);
                    tblLoteMuestra.AddCell(clCellLote13);

                    PdfPCell clCellLote2 = new PdfPCell(new Phrase("TAMAÑO LOTE", _standardFont));
                    clCellLote2.BorderWidth = 0;
                    PdfPCell clCellLote21 = new PdfPCell(new Phrase("", _standardFont));
                    clCellLote21.BorderWidth = 0;
                    clCellLote21.BorderWidthBottom = 0.75f;
                    PdfPCell clCellLote22 = new PdfPCell(new Phrase(item.informacionLote.tamañoLote, _standardFont));
                    clCellLote22.BorderWidth = 0;
                    clCellLote22.BorderWidthBottom = 0.75f;
                    clCellLote22.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    PdfPCell clCellLote23 = new PdfPCell(new Phrase("PZ", _standardFont));
                    clCellLote23.BorderWidth = 0;
                    clCellLote23.BorderWidthBottom = 0.75f;

                    tblLoteMuestra.AddCell(clCellLote2);
                    tblLoteMuestra.AddCell(clCellLote21);
                    tblLoteMuestra.AddCell(clCellLote22);
                    tblLoteMuestra.AddCell(clCellLote23);

                    PdfPCell clCellLote3 = new PdfPCell(new Phrase("TAMAÑO MUESTRA", _standardFont));
                    clCellLote3.BorderWidth = 0;
                    PdfPCell clCellLote31 = new PdfPCell(new Phrase("", _standardFont));
                    clCellLote31.BorderWidth = 0;
                    clCellLote31.BorderWidthBottom = 0.75f;
                    PdfPCell clCellLote32 = new PdfPCell(new Phrase(item.informacionLote.tamañoMuestra, _standardFont));
                    clCellLote32.BorderWidth = 0;
                    clCellLote32.BorderWidthBottom = 0.75f;
                    clCellLote32.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    PdfPCell clCellLote33 = new PdfPCell(new Phrase("PZ", _standardFont));
                    clCellLote33.BorderWidth = 0;
                    clCellLote33.BorderWidthBottom = 0.75f;

                    tblLoteMuestra.AddCell(clCellLote3);
                    tblLoteMuestra.AddCell(clCellLote31);
                    tblLoteMuestra.AddCell(clCellLote32);
                    tblLoteMuestra.AddCell(clCellLote33);

                    PdfPCell clCellBlankLote = new PdfPCell(new Phrase(" ", _standardFont));
                    clCellBlankLote.BorderWidth = 0;
                    clCellBlankLote.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    clCellBlankLote.Colspan = 5;

                    PdfPCell clCellPhrase1 = new PdfPCell(new Phrase("Este certificado es emitido electrónicamente por Aseguramiento de Calidad por lo que no se requiere firma.", _standardFont));
                    clCellPhrase1.BorderWidth = 0;
                    clCellPhrase1.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    clCellPhrase1.Colspan = 5;

                    PdfPCell clCellPhrase2 = new PdfPCell(new Phrase("PPMAC-IN37-R1", _standardFont));
                    clCellPhrase2.BorderWidth = 0;
                    clCellPhrase2.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                    clCellPhrase2.Colspan = 5;

                    PdfPCell clCellPhrase3 = new PdfPCell(new Phrase("Km 39.3 Autopista México-Querétaro, Parque Industrial La Luz, Cuautitlán Izcalli, Edo. de México, C.P. 54730,", _standardFontItalic));
                    clCellPhrase3.BorderWidth = 0;
                    clCellPhrase3.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    clCellPhrase3.Colspan = 5;

                    PdfPCell clCellPhrase4 = new PdfPCell(new Phrase("Tel: (55) 50-63-98-00, Fax: (55) 50-63-98-45", _standardFontItalic));
                    clCellPhrase4.BorderWidth = 0;
                    clCellPhrase4.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    clCellPhrase4.Colspan = 5;

                    tblLoteMuestra.AddCell(clCellBlankLote);
                    tblLoteMuestra.AddCell(clCellPhrase1);
                    tblLoteMuestra.AddCell(clCellBlankLote);
                    tblLoteMuestra.AddCell(clCellPhrase2);
                    tblLoteMuestra.AddCell(clCellBlankLote);
                    tblLoteMuestra.AddCell(clCellPhrase3);
                    tblLoteMuestra.AddCell(clCellPhrase4);

                    doc.Add(tblLoteMuestra);

                    doc.Close();
                    writer.Close();

                    Ent_tticol180 dataDoc = new Ent_tticol180()
                    {
                        docn = _documento,
                        user = _operator,
                        path = output.Name,
                        mssh = " "
                    };

                    var validate = _idaltticol180.insertRecord(ref dataDoc, ref strError);

                    return nameFile;
                };
            }

            protected string crearPDFInternational(Ent_DocumentoNacional item, string tipoCertificado)
            {

                Document doc = new Document(PageSize.LETTER);
                doc.SetMargins(70f, 70f, 70f, 70f);

                var path = ConfigurationManager.AppSettings["SaveDocQuality"].ToString();
                var folderImages = path + "Images\\logo.png";
                var folderImages2 = path + "Images\\logoPPM.jpg";

                if (!System.IO.Directory.Exists(path))
                {
                    System.IO.Directory.CreateDirectory(path);
                }

                if (tipoCertificado == "FACTURA")
                {
                    path = path + "Factura\\";
                }
                else if (tipoCertificado == "ENVIO")
                {
                    path = path + "Envio\\";
                }

                if (!System.IO.Directory.Exists(path))
                {
                    System.IO.Directory.CreateDirectory(path);
                }

                var folderPdf = tipoCertificado == "FACTURA" ? path + txtNumeroDocumento.Text.Trim() : path + item.informacionEncabezado.shmp.Trim();

                if (!System.IO.Directory.Exists(folderPdf))
                {
                    System.IO.Directory.CreateDirectory(folderPdf);
                }

                var nameFile = item.informacionLote.numeroLote.Trim() + '-' + item.informacionLote.iorn.Trim() + ".pdf";
                var pathPdf = folderPdf + "\\" + nameFile;

                if (System.IO.Directory.Exists(pathPdf))
                {
                    System.IO.Directory.Delete(pathPdf);
                }

                using (var output = new FileStream(pathPdf, FileMode.Create))
                {
                    var writer = PdfWriter.GetInstance(doc, output);

                    doc.AddTitle("Generador de PDF");
                    doc.AddCreator("Grupo Phoenix");

                    // Abrimos el archivo
                    doc.Open();

                    iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    iTextSharp.text.Font _standardFontBold = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    iTextSharp.text.Font _standardFontItalic = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, iTextSharp.text.Font.ITALIC, BaseColor.BLACK);
                    iTextSharp.text.Font _standardFontItalicBig = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.ITALIC, BaseColor.BLACK);
                    iTextSharp.text.Font _standardFontBoldSmall = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    //Creamos tabla para logos - encabezado
                    PdfPTable tblEncabezado = new PdfPTable(2);
                    tblEncabezado.WidthPercentage = 100;
                    float[] widthsEnc = new float[] { 30f, 70f };
                    tblEncabezado.SetWidths(widthsEnc);

                    iTextSharp.text.Image pic = iTextSharp.text.Image.GetInstance(folderImages2, false);
                    pic.ScaleAbsolute(80f, 30f);

                    //Llenamos info con logos y texto
                    PdfPCell clLogoNombrePhoenix = new PdfPCell(pic);
                    clLogoNombrePhoenix.BorderWidth = 0;

                    //Celda para solo logo
                    PdfPCell clTituloPagina = new PdfPCell(new Phrase("CERTIFICADO DE CONFORMIDAD DE PRODUCTO CALIDAD\n PRODUCT CONFORMITY CERTIFICATE(COA)", _standardFontBold));
                    clTituloPagina.BorderWidth = 0;
                    clTituloPagina.HorizontalAlignment = PdfPCell.ALIGN_LEFT;

                    PdfPCell clBlank = new PdfPCell(new Phrase(" ", _standardFontBold));
                    clBlank.BorderWidth = 0;
                    clBlank.Colspan = 2;

                    PdfPCell clDescripcionUno = new PdfPCell(new Phrase("ASEGURAMIENTO DE CALIDAD", _standardFontBold));
                    clDescripcionUno.BorderWidth = 0;
                    clDescripcionUno.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    clDescripcionUno.Colspan = 2;

                    PdfPCell clDescripcionDos = new PdfPCell(new Phrase("QUALITY ASSURANCE", _standardFontBold));
                    clDescripcionDos.BorderWidth = 0;
                    clDescripcionDos.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    clDescripcionDos.Colspan = 2;

                    tblEncabezado.AddCell(clLogoNombrePhoenix);
                    tblEncabezado.AddCell(clTituloPagina);
                    tblEncabezado.AddCell(clBlank);
                    tblEncabezado.AddCell(clDescripcionUno);
                    tblEncabezado.AddCell(clDescripcionDos);
                    //Añadimos tabla al documento
                    doc.Add(tblEncabezado);


                    PdfPTable tblDatosIniciales = new PdfPTable(3);
                    tblDatosIniciales.WidthPercentage = 100;
                    float[] widthsDatosIniciales = new float[] { 30f, 30f, 40f };
                    tblDatosIniciales.SetWidths(widthsDatosIniciales);

                    PdfPCell clFecha = new PdfPCell(new Phrase("Fecha: " + DateTime.Now.ToShortDateString(), _standardFont));
                    clFecha.BorderWidth = 0;
                    clFecha.BorderWidthTop = 0.75f;
                    clFecha.BorderWidthLeft = 0.75f;
                    clFecha.BorderWidthRight = 0.75f;

                    PdfPCell clProducto = new PdfPCell(new Phrase("Producto/Product", _standardFont));
                    clProducto.BorderWidth = 0;
                    clProducto.BorderWidthTop = 0.75f;

                    PdfPCell clNumeroProducto = new PdfPCell(new Phrase(item.informacionLote.articulo, _standardFont));
                    clNumeroProducto.BorderWidth = 0;
                    clNumeroProducto.BorderWidthTop = 0.75f;
                    clNumeroProducto.BorderWidthRight = 0.75f;

                    PdfPCell clDate = new PdfPCell(new Phrase("Date", _standardFont));
                    clDate.BorderWidth = 0;
                    clDate.BorderWidthBottom = 0.75f;
                    clDate.BorderWidthLeft = 0.75f;
                    clDate.BorderWidthRight = 0.75f;

                    PdfPCell clDescripcion = new PdfPCell(new Phrase("Descripción/Description", _standardFont));
                    clDescripcion.BorderWidth = 0;
                    clDescripcion.BorderWidthBottom = 0.75f;

                    PdfPCell clDescripcionTexto = new PdfPCell(new Phrase(item.informacionLote.descripcionArticulo, _standardFont));
                    clDescripcionTexto.BorderWidth = 0;
                    clDescripcionTexto.BorderWidthBottom = 0.75f;
                    clDescripcionTexto.BorderWidthRight = 0.75f;

                    tblDatosIniciales.AddCell(clFecha);
                    tblDatosIniciales.AddCell(clProducto);
                    tblDatosIniciales.AddCell(clNumeroProducto);
                    tblDatosIniciales.AddCell(clDate);
                    tblDatosIniciales.AddCell(clDescripcion);
                    tblDatosIniciales.AddCell(clDescripcionTexto);

                    doc.Add(tblDatosIniciales);

                    PdfPTable tblDatosInicialesTwo = new PdfPTable(2);
                    tblDatosInicialesTwo.WidthPercentage = 100;
                    float[] widthsDatosInicialesTwo = new float[] { 45f, 55f };
                    tblDatosInicialesTwo.SetWidths(widthsDatosInicialesTwo);

                    PdfPCell clOrden = new PdfPCell(new Phrase("Orden de Fabricación/ Work order:", _standardFont));
                    clOrden.BorderWidth = 0;
                    clOrden.BorderWidthLeft = 0.75f;
                    clOrden.BorderWidthRight = 0.75f;

                    PdfPCell clLote = new PdfPCell(new Phrase("Lote/ Batch", _standardFont));
                    clLote.BorderWidth = 0;
                    clLote.BorderWidthRight = 0.75f;

                    PdfPCell clOrdenText = new PdfPCell(new Phrase(item.informacionLote.numeroLote, _standardFont));
                    clOrdenText.BorderWidth = 0;
                    clOrdenText.BorderWidthLeft = 0.75f;
                    clOrdenText.BorderWidthRight = 0.75f;
                    clOrdenText.BorderWidthBottom = 0.75f;

                    PdfPCell clLoteText = new PdfPCell(new Phrase(item.informacionLote.numeroLote, _standardFont));
                    clLoteText.BorderWidth = 0;
                    clLoteText.BorderWidthBottom = 0.75f;
                    clLoteText.BorderWidthRight = 0.75f;

                    PdfPCell clEnunciadoUno = new PdfPCell(new Phrase("PHOENIX PACKAGING GROUP Se permite presentar los resultades de análisis de lote despachado al cliente." +
                                        "Producto elaborado con resinas que cumplen especificaciones para contacto con alimentos. Se dispone de los certificados" +
                                        "F.D.A para cuando estos sean requeridos por los clientes.\n", _standardFontItalicBig));
                    clEnunciadoUno.BorderWidth = 0;
                    clEnunciadoUno.Colspan = 2;

                    PdfPCell clEnunciadoDos = new PdfPCell(new Phrase("PHOENIX PACKAGING GROUP is allowed to present the results of the batch shipped the client. This product is made from resin," +
                    "wich meets specification for food contact; the certificate is available to FDA when they are required by customers", _standardFontItalicBig));
                    clEnunciadoDos.BorderWidth = 0;
                    clEnunciadoDos.BorderWidthBottom = 0.75f;
                    clEnunciadoDos.Colspan = 2;

                    tblDatosInicialesTwo.AddCell(clOrden);
                    tblDatosInicialesTwo.AddCell(clLote);
                    tblDatosInicialesTwo.AddCell(clOrdenText);
                    tblDatosInicialesTwo.AddCell(clLoteText);
                    tblDatosInicialesTwo.AddCell(clEnunciadoUno);
                    tblDatosInicialesTwo.AddCell(clEnunciadoDos);

                    doc.Add(tblDatosInicialesTwo);

                    //Tabla producto
                    PdfPTable tblProducto = new PdfPTable(7);
                    tblProducto.WidthPercentage = 100;
                    float[] widthsProducto = new float[] { 35f, 7.5f, 7.5f, 10f, 10f, 10f, 20f };
                    tblProducto.SetWidths(widthsProducto);

                    //Agregar encabezados tabla
                    PdfPCell clEncCaracteristicas = new PdfPCell(new Phrase("CARACTERISTICAS DEL PRODUCTO", _standardFontBold));
                    clEncCaracteristicas.BorderWidth = 0;

                    PdfPCell clEncResultados = new PdfPCell(new Phrase("RESULTADOS", _standardFontBold));
                    clEncResultados.BorderWidth = 0;
                    clEncResultados.Colspan = 2;
                    clEncResultados.HorizontalAlignment = PdfPCell.ALIGN_CENTER;

                    PdfPCell clEncLimites = new PdfPCell(new Phrase("LIMITES DE ESPECIFICACIÓN", _standardFontBold));
                    clEncLimites.BorderWidth = 0;
                    clEncLimites.Colspan = 3;
                    clEncLimites.HorizontalAlignment = PdfPCell.ALIGN_CENTER;

                    PdfPCell clEncMuestra = new PdfPCell(new Phrase("Muestra", _standardFontBold));
                    clEncMuestra.BorderWidth = 0;


                    tblProducto.AddCell(clEncCaracteristicas);
                    tblProducto.AddCell(clEncResultados);
                    tblProducto.AddCell(clEncLimites);
                    tblProducto.AddCell(clEncMuestra);


                    //Agregar encabezados tabla
                    PdfPCell clEncCaracteristicasEn = new PdfPCell(new Phrase("PRODUCT FEATURES", _standardFontBold));
                    clEncCaracteristicasEn.BorderWidth = 0;

                    PdfPCell clEncResultadosEn = new PdfPCell(new Phrase("RESULTS", _standardFontBold));
                    clEncResultadosEn.BorderWidth = 0;
                    clEncResultadosEn.Colspan = 2;
                    clEncResultadosEn.HorizontalAlignment = PdfPCell.ALIGN_CENTER;

                    PdfPCell clEncLimitesEn = new PdfPCell(new Phrase("SPECIFICATIONS LIMIT", _standardFontBold));
                    clEncLimitesEn.BorderWidth = 0;
                    clEncLimitesEn.Colspan = 3;
                    clEncLimitesEn.HorizontalAlignment = PdfPCell.ALIGN_CENTER;

                    PdfPCell clEncMuestraEn = new PdfPCell(new Phrase("SAMPLES STATUS", _standardFontBold));
                    clEncMuestraEn.BorderWidth = 0;

                    tblProducto.AddCell(clEncCaracteristicasEn);
                    tblProducto.AddCell(clEncResultadosEn);
                    tblProducto.AddCell(clEncLimitesEn);
                    tblProducto.AddCell(clEncMuestraEn);

                    PdfPCell clBlank3 = new PdfPCell(new Phrase(" ", _standardFontBold));
                    clBlank3.BorderWidth = 0;
                    clBlank3.Colspan = 3;

                    PdfPCell clEncUSL = new PdfPCell(new Phrase("USL", _standardFontBold));
                    clEncUSL.BorderWidth = 0;

                    PdfPCell clEncSTD = new PdfPCell(new Phrase("STD", _standardFontBold));
                    clEncSTD.BorderWidth = 0;

                    PdfPCell clEncLSL = new PdfPCell(new Phrase("LSL", _standardFontBold));
                    clEncLSL.BorderWidth = 0;

                    PdfPCell clBlank1 = new PdfPCell(new Phrase(" ", _standardFontBold));
                    clBlank1.BorderWidth = 0;
                    clBlank1.Colspan = 1;

                    tblProducto.AddCell(clBlank3);
                    tblProducto.AddCell(clEncUSL);
                    tblProducto.AddCell(clEncSTD);
                    tblProducto.AddCell(clEncLSL);
                    tblProducto.AddCell(clBlank1);


                    foreach (var producto in item.informacionProducto)
                    {
                        PdfPCell clCarateristicas = new PdfPCell(new Phrase(producto.descripcionCaracteristica, _standardFont));
                        clCarateristicas.BorderWidth = 0;

                        PdfPCell clResultados = new PdfPCell(new Phrase(producto.resultados, _standardFont));
                        clResultados.BorderWidth = 0;

                        PdfPCell clUnidad = new PdfPCell(new Phrase(producto.unidad, _standardFont));
                        clUnidad.BorderWidth = 0;

                        PdfPCell clUSL = new PdfPCell(new Phrase(producto.valorNormalizado, _standardFont));
                        clUSL.BorderWidth = 0;

                        PdfPCell clLIE = new PdfPCell(new Phrase(producto.limiteInferior, _standardFont));
                        clLIE.BorderWidth = 0;

                        PdfPCell clLSE = new PdfPCell(new Phrase(producto.limiteSuperior, _standardFont));
                        clLSE.BorderWidth = 0;

                        PdfPCell clMuestra = new PdfPCell(new Phrase(producto.muestra, _standardFont));
                        clMuestra.BorderWidth = 0;

                        PdfPCell clEnglishProduct = new PdfPCell(new Phrase(producto.descripcionCaracteristicaIngles, _standardFont));
                        clEnglishProduct.BorderWidth = 0;

                        PdfPCell clBlank5 = new PdfPCell(new Phrase(" ", _standardFont));
                        clBlank5.BorderWidth = 0;
                        clBlank5.Colspan = 5;

                        PdfPCell clEnglishSample = new PdfPCell(new Phrase(producto.muestraIngles, _standardFont));
                        clEnglishSample.BorderWidth = 0;


                        tblProducto.AddCell(clCarateristicas);
                        tblProducto.AddCell(clResultados);
                        tblProducto.AddCell(clUnidad);
                        tblProducto.AddCell(clUSL);
                        tblProducto.AddCell(clLIE);
                        tblProducto.AddCell(clLSE);
                        tblProducto.AddCell(clMuestra);
                        tblProducto.AddCell(clEnglishProduct);
                        tblProducto.AddCell(clBlank5);
                        tblProducto.AddCell(clEnglishSample);
                    }

                    doc.Add(tblProducto);

                    PdfPTable tblMuestreo = new PdfPTable(7);
                    tblMuestreo.WidthPercentage = 100;
                    //float[] widthsProducto = new float[] { 30f, 7.5f, 7.5f, 12f, 12f, 11f, 20f };
                    tblMuestreo.SetWidths(widthsProducto);

                    PdfPCell clEncMuestreo = new PdfPCell(new Phrase("MUESTREO DE DESPACHO", _standardFontBold));
                    clEncMuestreo.BorderWidth = 0;
                    clEncMuestreo.Colspan = 7;

                    PdfPCell clEncMuestreoEn = new PdfPCell(new Phrase("SHIPMENT SAMPLES", _standardFontBold));
                    clEncMuestreoEn.BorderWidth = 0;
                    clEncMuestreoEn.Colspan = 7;

                    PdfPCell clBlankMuestreo = new PdfPCell(new Phrase(" ", _standardFontBold));
                    clBlankMuestreo.BorderWidth = 0;
                    clBlankMuestreo.Colspan = 7;

                    PdfPCell clFirstText = new PdfPCell(new Phrase("NIVEL DE INSPECCIÓN", _standardFont));
                    clFirstText.BorderWidth = 0;

                    PdfPCell clFirstTextLast = new PdfPCell(new Phrase("//", _standardFont));
                    clFirstTextLast.BorderWidth = 0;

                    PdfPCell clFirstTextEn = new PdfPCell(new Phrase("LEVEL OF INSPECTION", _standardFont));
                    clFirstTextEn.BorderWidth = 0;

                    PdfPCell clFirstTextLastEn = new PdfPCell(new Phrase("//", _standardFont));
                    clFirstTextLastEn.BorderWidth = 0;

                    PdfPCell clBlank5Muestreo = new PdfPCell(new Phrase("", _standardFont));
                    clBlank5Muestreo.BorderWidth = 0;
                    clBlank5Muestreo.Colspan = 5;

                    tblMuestreo.AddCell(clEncMuestreo);
                    tblMuestreo.AddCell(clEncMuestreoEn);
                    tblMuestreo.AddCell(clBlankMuestreo);
                    tblMuestreo.AddCell(clFirstText);
                    tblMuestreo.AddCell(clFirstTextLast);
                    tblMuestreo.AddCell(clBlank5Muestreo);
                    tblMuestreo.AddCell(clFirstTextEn);
                    tblMuestreo.AddCell(clFirstTextLastEn);
                    tblMuestreo.AddCell(clBlank5Muestreo);


                    PdfPCell clSecondText = new PdfPCell(new Phrase("N.C.A DEFECTOS CRITICOS", _standardFont));
                    clSecondText.BorderWidth = 0;

                    PdfPCell clSecondTextLast = new PdfPCell(new Phrase(" ", _standardFont));
                    clSecondTextLast.BorderWidth = 0;
                    clSecondTextLast.Colspan = 2;

                    PdfPCell clSecondTextLast2 = new PdfPCell(new Phrase("0.65", _standardFont));
                    clSecondTextLast2.BorderWidth = 0;

                    PdfPCell clBlank4 = new PdfPCell(new Phrase(" ", _standardFont));
                    clBlank4.BorderWidth = 0;
                    clBlank4.Colspan = 4;

                    PdfPCell clSecondTextEn = new PdfPCell(new Phrase("A.Q.L CRITICAL DEFECTS", _standardFont));
                    clSecondTextEn.BorderWidth = 0;

                    PdfPCell clSecondTextLastEn = new PdfPCell(new Phrase(" ", _standardFont));
                    clSecondTextLastEn.BorderWidth = 0;

                    tblMuestreo.AddCell(clSecondText);
                    tblMuestreo.AddCell(clSecondTextLast);
                    tblMuestreo.AddCell(clSecondTextLast2);
                    tblMuestreo.AddCell(clBlank4);
                    tblMuestreo.AddCell(clSecondTextEn);
                    tblMuestreo.AddCell(clSecondTextLastEn);
                    tblMuestreo.AddCell(clBlank5Muestreo);


                    PdfPCell clThirdText = new PdfPCell(new Phrase("N.C.A DEFECTOS MAYORES", _standardFont));
                    clThirdText.BorderWidth = 0;

                    PdfPCell clThirdTextLast = new PdfPCell(new Phrase(" ", _standardFont));
                    clThirdTextLast.BorderWidth = 0;
                    clThirdTextLast.Colspan = 2;

                    PdfPCell clThirdTextLast2 = new PdfPCell(new Phrase("4.0", _standardFont));
                    clThirdTextLast2.BorderWidth = 0;

                    PdfPCell clThirdTextEn = new PdfPCell(new Phrase("A.Q.L MAJOR DEFECTS", _standardFont));
                    clThirdTextEn.BorderWidth = 0;

                    PdfPCell clThirdTextLastEn = new PdfPCell(new Phrase(" ", _standardFont));
                    clThirdTextLastEn.BorderWidth = 0;

                    tblMuestreo.AddCell(clThirdText);
                    tblMuestreo.AddCell(clThirdTextLast);
                    tblMuestreo.AddCell(clThirdTextLast2);
                    tblMuestreo.AddCell(clBlank4);
                    tblMuestreo.AddCell(clThirdTextEn);
                    tblMuestreo.AddCell(clThirdTextLastEn);
                    tblMuestreo.AddCell(clBlank5Muestreo);

                    PdfPCell clFourText = new PdfPCell(new Phrase("N.C.A DEFECTOS MENORES", _standardFont));
                    clFourText.BorderWidth = 0;

                    PdfPCell clFourTextLast = new PdfPCell(new Phrase(" ", _standardFont));
                    clFourTextLast.BorderWidth = 0;
                    clFourTextLast.Colspan = 2;

                    PdfPCell clFourTextLast2 = new PdfPCell(new Phrase("6.5", _standardFont));
                    clFourTextLast2.BorderWidth = 0;

                    PdfPCell clFourTextEn = new PdfPCell(new Phrase("A.Q.L MINOR DEFECTS", _standardFont));
                    clFourTextEn.BorderWidth = 0;

                    PdfPCell clFourTextLastEn = new PdfPCell(new Phrase(" ", _standardFont));
                    clFourTextLastEn.BorderWidth = 0;

                    tblMuestreo.AddCell(clFourText);
                    tblMuestreo.AddCell(clFourTextLast);
                    tblMuestreo.AddCell(clFourTextLast2);
                    tblMuestreo.AddCell(clBlank4);
                    tblMuestreo.AddCell(clFourTextEn);
                    tblMuestreo.AddCell(clFourTextLastEn);
                    tblMuestreo.AddCell(clBlank5Muestreo);
                    tblMuestreo.AddCell(clBlankMuestreo);

                    PdfPCell clResultadoText = new PdfPCell(new Phrase("RESULTADO FINAL DEL LOTE:", _standardFontBold));
                    clResultadoText.BorderWidth = 0;

                    PdfPCell clResultadoTextEn = new PdfPCell(new Phrase("APROBADO", _standardFontBold));
                    clResultadoTextEn.BorderWidth = 0;
                    clResultadoTextEn.Colspan = 2;

                    tblMuestreo.AddCell(clResultadoText);
                    tblMuestreo.AddCell(clResultadoTextEn);
                    tblMuestreo.AddCell(clBlank5Muestreo);


                    PdfPCell clResultadoText2 = new PdfPCell(new Phrase("FINAL RESULT OF THE BATCH:", _standardFontBold));
                    clResultadoText2.BorderWidth = 0;

                    PdfPCell clResultadoTextEn2 = new PdfPCell(new Phrase("APPROVED", _standardFontBold));
                    clResultadoTextEn2.BorderWidth = 0;
                    clResultadoTextEn2.Colspan = 2;

                    tblMuestreo.AddCell(clResultadoText2);
                    tblMuestreo.AddCell(clResultadoTextEn2);
                    tblMuestreo.AddCell(clBlank4);
                    tblMuestreo.AddCell(clBlankMuestreo);

                    PdfPCell clAprobadoText = new PdfPCell(new Phrase("Aprobado por: Aseguramiento de calidad", _standardFontItalicBig));
                    clAprobadoText.BorderWidth = 0;
                    clAprobadoText.Colspan = 7;

                    PdfPCell clAprobadoTextTwo = new PdfPCell(new Phrase("Este certificado es emitidio electrónicamente por Aseguramiento de calidad por lo que no requiere firma.", _standardFontItalicBig));
                    clAprobadoTextTwo.BorderWidth = 0;
                    clAprobadoTextTwo.Colspan = 7;

                    tblMuestreo.AddCell(clAprobadoText);
                    tblMuestreo.AddCell(clAprobadoTextTwo);

                    PdfPCell clAprobadoTextEn = new PdfPCell(new Phrase("Approved by: Quality Assurance", _standardFontItalicBig));
                    clAprobadoTextEn.BorderWidth = 0;
                    clAprobadoTextEn.Colspan = 7;

                    PdfPCell clAprobadoTextTwoEn = new PdfPCell(new Phrase("This certificate is issued electronically by quality assuarance no signature requires.", _standardFontItalicBig));
                    clAprobadoTextTwoEn.BorderWidth = 0;
                    clAprobadoTextTwoEn.Colspan = 7;

                    PdfPCell clAprobadoNumber = new PdfPCell(new Phrase("PPMAC-IN37-R1", _standardFont));
                    clAprobadoNumber.BorderWidth = 0;
                    clAprobadoNumber.Colspan = 7;
                    clAprobadoNumber.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                    clAprobadoNumber.BorderWidthBottom = 0.75f;

                    PdfPCell clBlankBorderBottom = new PdfPCell(new Phrase(" ", _standardFont));
                    clBlankBorderBottom.BorderWidth = 0;
                    clBlankBorderBottom.BorderWidthBottom = 0.75f;
                    clBlankBorderBottom.Colspan = 7;
                    clBlankBorderBottom.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;

                    tblMuestreo.AddCell(clAprobadoTextEn);
                    tblMuestreo.AddCell(clAprobadoTextTwoEn);
                    tblMuestreo.AddCell(clAprobadoNumber);

                    doc.Add(tblMuestreo);

                    var phrase1 = new Phrase();
                    phrase1.Add(new Chunk("USA 3900 Pembroke Road Hollywood, FL 33021 - Tel (954) 241-0023 - Fax: (954) 367-0355" +
                                            "COLOMBIA Calle 17F Nº 126-90 - Tel: (1)422 2000 - Fax: (1)418 2712 Bogotá, D. C." +
                                            "MEXICO Km 39.3 Auopista México Querétaro Colonia Parque Industrial la Luz Cuautitlan Izcalli. Estado de México - " +
                                            "Tel: (55)5063 98 36 - Fax: (55) 5063 9845 VENEZUELA                  " +
                                            "Av. Francisco de Miranda, Edificio Seguros Venezuela, Piso 10, Urbanizacion " +
                                            "Campo Alegre Caracas - Tel: (212) 953 4106 - Fax: (212) 953 9627                                                " +
                                            "                                                                                                                  " +
                                            "www.grupophoenix.com", _standardFontBoldSmall));

                    doc.Add(phrase1);

                    doc.Close();
                    writer.Close();

                    Ent_tticol180 dataDoc = new Ent_tticol180()
                    {
                        docn = _documento,
                        user = _operator,
                        path = output.Name,
                        mssh = " "
                    };

                    var validate = _idaltticol180.insertRecord(ref dataDoc, ref strError);

                    return nameFile;
                };
            }

        #endregion

        #region Metodos

            protected void IdiomaIngles()
            {
                Session["ddlIdioma"] = "INGLES";
                _idioma = "INGLES";
                lblTipoDocumento.Text = "Document type: ";
                lblNumeroDocumento.Text = "Document number: ";
                btnGenerateCertificate.Text = "Create";
            }

            protected void IdiomaEspañol()
            {
                Session["ddlIdioma"] = "ESPAÑOL";
                _idioma = "ESPAÑOL";
                lblTipoDocumento.Text = "Tipo de documento: ";
                lblNumeroDocumento.Text = "Número de documento: ";
                btnGenerateCertificate.Text = "Generar";
            }

            protected string mensajes(string tipoMensaje)
            {
                var retorno = String.Empty;

                switch (tipoMensaje)
                {
                    case "tipoDocumentoBlanco":
                        retorno = _idioma == "ESPAÑOL" ? "Seleccione un tipo de documento" : "Select document type";
                        break;
                    case "numeroDocumentoBlanco":
                        retorno = _idioma == "ESPAÑOL" ? "Ingrese un número de documento" : "Enter a document number";
                        break;
                    case "envio":
                        retorno = _idioma == "ESPAÑOL" ? "Número de Envío no existe." : "Shipment number doesn't exist";
                        break;
                    case "factura":
                        retorno = _idioma == "ESPAÑOL" ? "Número de Factura no existe." : "Invoice number doesn't exist";
                        break;
                    case "confirm":
                        retorno = _idioma == "ESPAÑOL" ? "Se han generado {0} documentos correctamente." : "{0} documents have been generated.";
                        break;
                    case "encabezado":
                        retorno = _idioma == "ESPAÑOL" ? "Certificado de calidad" : "Quality Certificate";
                        break;
                }

                return retorno;
            }

        #endregion
    }
}