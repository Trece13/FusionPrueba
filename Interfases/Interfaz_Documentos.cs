using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using whusa.Entidades;
using whusa.Entidades.Documentos;
using System.Data;

namespace whusa.Interfases
{
    public class Interfaz_Documentos
    {
        private static InterfazDAL_tcisli245 _idaltcisli245 = new InterfazDAL_tcisli245();
        private static InterfazDAL_twhinh431 _idaltwhinh431 = new InterfazDAL_twhinh431();
        private static InterfazDAL_tqmptc101 _idaltqmptc101 = new InterfazDAL_tqmptc101();
        private static InterfazDAL_twhinh430 _idaltwhinh430 = new InterfazDAL_twhinh430();
        private static InterfazDAL_tqmcol040 _idaltqmcol040 = new InterfazDAL_tqmcol040();


        public List<Ent_DocumentoNacional> getInformationDocuments(ref string numeroFactura, ref string tipoDocumento ,ref string strError) 
        {
            List<Ent_DocumentoNacional> documentInfo = new List<Ent_DocumentoNacional>();

            var encabezadoInfo = new DataTable();

            if (tipoDocumento == "FACTURA")
            {
                encabezadoInfo = _idaltcisli245.getInformationDocumentNational(ref numeroFactura, ref strError);
            }
            else 
            {
                encabezadoInfo = _idaltwhinh430.getInformationDocumentNational(ref numeroFactura, ref strError);
            }


            if (encabezadoInfo.Rows.Count < 1)
            {
                return documentInfo;
            }


            foreach (DataRow rowEncabezado in encabezadoInfo.Rows)
            {
                var SHPM = rowEncabezado["SHPM"].ToString();
                var STPB = rowEncabezado["STBP"].ToString();
                var NAMA = rowEncabezado["NAMA"].ToString();

                var lotInfo = _idaltwhinh431.getLotInfo(ref SHPM, ref strError);

                if (lotInfo.Rows.Count > 0)
                {
                    List<string> listaLotesImpresos = new List<string>();

                    foreach (DataRow rowLot in lotInfo.Rows)
                    {
                        Ent_DocumentoNacional documento = new Ent_DocumentoNacional();

                        Ent_EncabezadoNacional encabezado = new Ent_EncabezadoNacional()
                        {
                            shmp = SHPM,
                            codigoCliente = STPB,
                            cliente = NAMA
                        };

                        documento.informacionEncabezado = encabezado;
                        var ITEM = rowLot["ITEM"].ToString();
                        var DSCA = rowLot["DSCA"].ToString();
                        var CLOT = rowLot["CLOT"].ToString();
                        var IORN = rowLot["IORN"].ToString();
                        var SHPMLot = rowLot["SHPM"].ToString();

                        Ent_LoteDespacho dataLot = new Ent_LoteDespacho() 
                        {
                            articulo = ITEM,
                            descripcionArticulo = DSCA,
                            numeroLote = CLOT,
                            iorn = IORN,
                            shpm = SHPMLot
                        };

                        var validLote = listaLotesImpresos.Where(x => x == CLOT).FirstOrDefault();

                        if (validLote == null)
                        {
                            var tamaLote = _idaltwhinh431.getLotSize(ref SHPMLot, ref CLOT, ref strError);

                            if (tamaLote.Rows.Count > 0)
                            {
                                var tamLote = tamaLote.Rows[0]["TAMLOTE"].ToString();
                                dataLot.tamañoLote = tamLote;

                                var tamañoMuestra = _idaltqmcol040.getSampleSize(ref STPB, ref tamLote, ref strError);

                                if (tamañoMuestra.Rows.Count > 0)
                                {
                                    var tamMuestra = tamañoMuestra.Rows[0]["SZSM"].ToString();
                                    dataLot.tamañoMuestra = tamMuestra;
                                }
                            }

                            documento.informacionLote = dataLot;

                            var productInfo101 = _idaltqmptc101.getProductInfo(ref IORN, ref strError);

                            if (productInfo101.Rows.Count > 0)
                            {
                                foreach (DataRow rowProduct in productInfo101.Rows)
                                {

                                    var IORNProduct = rowProduct["IORN"].ToString();
                                    var DSCAProduct = rowProduct["DSCA"].ToString();
                                    var CHUN = rowProduct["CHUN"].ToString();
                                    var LLMT = rowProduct["LLMT"].ToString();
                                    var ULMT = rowProduct["ULMT"].ToString();
                                    var MVAL = rowProduct["MVAL"].ToString();
                                    var RESL = rowProduct["RESL"].ToString();
                                    var NORM = rowProduct["NORM"].ToString();
                                    var PRODENG = rowProduct["PRODENG"];
                                    var TXTA = rowProduct["TXTA"].ToString();

                                    if (IORNProduct.Equals(IORN))
                                    {
                                        Ent_InformacionProducto dataProduct = new Ent_InformacionProducto()
                                        {
                                            iorn = IORNProduct,
                                            descripcionCaracteristica = DSCAProduct,
                                            unidad = CHUN,
                                            limiteInferior = LLMT,
                                            limiteSuperior = ULMT,
                                            muestra = RESL == "1" ? "Satisfactorio" : "No satisfactorio",
                                            resultados = MVAL,
                                            valorNormalizado = NORM,
                                            descripcionCaracteristicaIngles = PRODENG.ToString() != "" ? PRODENG.ToString() : "--",
                                            muestraIngles = RESL == "1" ? "Satisfactory" : "Unsatisfactory"
                                        };

                                        if (TXTA != "0")
                                        {
                                            var atributos = _idaltqmptc101.getAttributesProduct(ref TXTA, ref strError);

                                            foreach (DataRow itemAtr in atributos.Rows)
                                            {
                                                dataProduct.atributos.Add(itemAtr["TEXT"].ToString());
                                            }   
                                        }

                                        documento.informacionProducto.Add(dataProduct);
                                    }
                                }
                                listaLotesImpresos.Add(CLOT);
                            }
                            documentInfo.Add(documento);
                        }
                    }  
                } 
            }
            return documentInfo;
        }
    }
}
