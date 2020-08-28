<%@ Page Language="C#" MasterPageFile="~/MDMasterPage.Master" AutoEventWireup="true" CodeBehind="whInvReprintLabelDmt.aspx.cs" Inherits="whusap.WebPages.Migration.whInvReprintLabelDmt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Encabezado" runat="server">
    <script type="text/javascript">
        var _tipoFormulario = '<%= _tipoFormulario %>';

        function addZero(i) {
            if (i < 10) {
                i = "0" + i;

            }
            return i;
        }; 

        function printDiv(divID) {

            var monthNames = [
                "1", "2", "3",
                "4", "5", "6", "7",
                "8", "9", "10",
                "11", "12"
              ];

            //PRINT LOCAL HOUR
            var d = new Date();
            var LbdDate = $("#Contenido_lblFecha");
            LbdDate.text(

                monthNames[d.getMonth()] +
                "/" +
                d.getDate() +
                "/" +
                d.getFullYear() +
                " - " +
                addZero(d.getHours()) +
                ":" +
                addZero(d.getMinutes()) +
                ":" +
                addZero(d.getSeconds())

                );
            //Get the HTML of div
            var divElements = document.getElementById(divID).innerHTML;
            //Get the HTML of whole page
            var oldPage = document.body.innerHTML;
            //Reset the page's HTML with div's HTML only
            document.body.innerHTML = "<html><head><title></title></head><body>" + divElements + "</body>";
            //Print Page
            window.print();
            //Restore orignal HTML
            document.body.innerHTML = oldPage;
        };
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <table border="0">
        <tr>
            <td style="text-align:left;">
                <span style="vertical-align: middle" /><span class="style2" style="vertical-align: middle;">
                <b style="font-size: 11px;">
                    <asp:Label runat="server" ID="lblWorkOrder" /></b></span>
            </td>
            <td style="width: 250px; padding:5px;">
                <span style="vertical-align: middle;">
                    <asp:TextBox runat="server" ID="txtWorkOrder" CssClass="TextBoxBig" ClientIDMode="Static" />
                </span>
            </td>
        </tr>
        <tr>
            <td style="text-align:left;">
                <span style="vertical-align: middle" /><span class="style2" style="vertical-align: middle;">
                <b style="font-size: 11px;">
                    <asp:Label runat="server" ID="lblSequence" /></b></span>
            </td>
            <td style="width: 250px; padding:5px;">
                <span style="vertical-align: middle;">
                    <asp:TextBox runat="server" ID="txtSequence" CssClass="TextBoxBig" ClientIDMode="Static" />
                </span>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align:center;">
                <hr />  
                <asp:Button Text="" runat="server" ID="btnConsultar" OnClick="btnConsultar_Click" CssClass="ButtonsSendSave" style="height:30px;" />
            </td>
        </tr>
    </table>

    <div runat="server" id="divBotones" style="text-align:left;" visible="false">
        <hr />
         <a href="#" runat="server" id="linkPrint" onclick="javascript:printDiv('divTableFinish')" style="color: #000000; font-size: medium"><button class="buttonMenu" runat="server" id="btnPrint" style="width:25%;"></button></a>
    </div>

    <div id="divTableFinish" runat="server" visible="false" clientidmode="static">
                <table style="font-size:small; font-weight:bold; text-align:center; width: 5.8in; height: 3.8in" border="1" cellspacing="0" cellpadding="0">
            <tr>
                <td><asp:Label runat="server" ID="lblDmtNumber"></asp:Label></td>
                <td><asp:Label runat="server" ID="lblOrdPonoSeqn"></asp:Label></td>
                <td><img src="~/images/logophoenix_login.jpg" runat="server" id="imgOrdPonoSeqn" alt="" hspace="60" vspace="5" style="width: 2in; height: .5in;  margin:0px;" /></td>
            </tr>
            <tr>
                <td colspan="3"><img src="~/images/logophoenix_login.jpg" runat="server" id="imgItem" alt="" hspace="60" vspace="5" style="width: 4in; height: .5in; margin:0px;"/></td>
            </tr>
            <tr>
                <td><asp:Label runat="server" ID="lblDescription"></asp:Label></td>
                <td colspan="2"><asp:Label runat="server" ID="lblValueDescripcion"></asp:Label></td>
            </tr>
            <tr>
                <td style="border-bottom:0px;">
                    <asp:Label ID="lblMachineTitle" runat="server"></asp:Label>
                    -<asp:Label ID="LblMachineId" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:Label runat="server" ID="lblRejected"></asp:Label> -
                    <asp:Label ID="lblValueQuantity" runat="server"></asp:Label> -
                    <asp:Label ID="lblValueUnit" runat="server"></asp:Label>
                </td>
                <td><img src="~/images/logophoenix_login.jpg" runat="server" id="imgQuantity" alt="" hspace="60" vspace="5" style="width: 2in; height: .5in;  margin:0px;"/></td>
            </tr>
            <tr>
                <td style="border-top:0px;">
                    <asp:Label ID="LblDisposition" runat="server"></asp:Label>
                    -<asp:Label ID="LblDispositionValue" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:Label runat="server" ID="LblWorkOrderTitle" /> -
                    <asp:Label ID="lblValueWorkOrder" runat="server" />
                </td>
                <td><img src="~/images/logophoenix_login.jpg" runat="server" id="imgWorkOrder" alt="" hspace="60" vspace="5" style="width: 2in; height: .5in;  margin:0px;"/></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblPrintedBy" runat="server" />:
                    <asp:Label ID="lblValuePrintedBy" runat="server" />
                </td>
                <td colspan="2">
                    <asp:Label ID="lblValueFecha" runat="server" />
                    <asp:Label ID="lblFecha" runat="server" />
                    
                </td>
              </tr>
            <tr>
                <td><asp:Label ID="lblReason" runat="server" /></td>
                <td colspan="2"><asp:Label ID="lblValueReason" runat="server" /></td>
            </tr>
            <tr>
                <td colspan="3" rowspan="2">
                    <asp:Label ID="lblComments" runat="server" />: 
                    <asp:Label ID="lblValueComments" runat="server" />
                </td>
            </tr>
        </table>

    </div>

    <div id="divTableRaw" runat="server" visible="false" clientidmode="static">
        <table class='table table-bordered' style='font-weight:bold;max-width:500px;font-size:13px; border:3px solid; border-style:outset; text-align:center;'>
            <tr>
                <td colspan="2"><asp:Label ID="lblDefectiveMaterial2" runat="server" /></td>
                <td><asp:Label ID="lblDmtNumber2" runat="server" /></td>
            </tr>
            <tr>
                <td colspan="2"><asp:Label ID="lblPurchased" runat="server" /></td>
                <td><asp:Label ID="lblOrderSeqn2" runat="server" /></td>
            </tr>
            <tr>
                <td colspan="3"><asp:Label ID="lblProductCode2" runat="server" /></td>
            </tr>
            <tr>
                <td colspan="3"><asp:Label ID="lblValueProductCode2" runat="server" /></td>
            </tr>
            <tr>
                <td colspan="2"><asp:Label ID="lblProductDescription2" runat="server" /></td>
                <td><asp:Label ID="lblDate2" runat="server" /></td>
            </tr>
            <tr>
                <td colspan="2"><asp:Label ID="lblValueProductDescription2" runat="server" /></td>
                <td><asp:Label ID="lblValueDate2" runat="server" ClientIDMode="Static" /></td>
            </tr>
            <tr>
                <td><asp:Label ID="lblPrintedBy2" runat="server" /></td>
                <td><asp:Label ID="lblWorkOrder2" runat="server" /></td>
                <td><asp:Label ID="lblInternalMaterial" runat="server" /></td>
            </tr>
            <tr>
                <td><asp:Label ID="lblValuePrintedBy2" runat="server" /></td>
                <td><asp:Label ID="lblValueWorkOrder2" runat="server" /></td>
                <td><asp:Label ID="lblValueInternalMaterial" runat="server" /></td>
            </tr>
             <tr>
                <td><asp:Label ID="lblReason2" runat="server" /></td>
                <td colspan="2"><asp:Label ID="lblValueReason2" runat="server" /></td>
            </tr>
            <tr>
                <td colspan="3"><asp:Label ID="lblValueObs2" runat="server" /></td>
            </tr>
        </table>
    </div>

    <asp:Label Text="" runat="server" ID="lblError" style="color:red; font-size:15px; font-weight:bold;" ClientIDMode="Static" />
    <asp:Label Text="" runat="server" ID="lblConfirm" style="color:green; font-size:15px; font-weight:bold;" ClientIDMode="Static" />
    <script type="text/javascript">
        function InicilizarObjetos()
        {
            lblDescription = $('#lblDescription');
            //-------------------------------------
            lblMachineTitle = $('#MachineTitle');
            LblMachineId = $('#LblMachineId');
            //
            lblRejected = $('#lblRejected');
            //--------------------------------------
            LblDisposition = $('#LblDisposition');
            LblDispositionValue = $('#LblDispositionValue');
            //
            lblWorkOrder = $('#LblWorkOrderTitle');
            //---------------------------------------
            lblValueFecha = $('#lblValueFecha');
            //---------------------------------------
            lblReason = $('#lblReason');
            //---------------------------------------
            lblComments = $('#lblComments');
            lblValueComments = $('#lblValueComments');

        }

        var _idioma = '<%= Session["ddlIdioma"].ToString()%>';

        function CargarIdioma() {
            if (_idioma == "INGLES") {
                lblDescription.val("Description");
                //-------------------------------------
                lblMachineTitle.val("Machine");
                //LblMachineId.val();
                //
                lblRejected.val("Rejected Qty");
                //--------------------------------------
                LblDisposition.val("Disposition");
                //LblDispositionValue.val();
                //
                lblWorkOrder.val("Work Order");
                //---------------------------------------
                lblValueFecha.val("Date");
                //---------------------------------------
                lblReason.val("Reason");
                //---------------------------------------
                lblComments.val("Comments");
                //lblValueComments.val();
            } else {
                lblDescription.val("Descripción");
                //-------------------------------------
                lblMachineTitle.val("Maquina");
                //LblMachineId.val();
                //
                lblRejected.val("Rechazado Qty");
                //--------------------------------------
                LblDisposition.val("Disposición");
                //LblDispositionValue.val();
                //
                lblWorkOrder.val("Orden de Trabajo");
                //---------------------------------------
                lblValueFecha.val("Fecha");
                //---------------------------------------
                lblReason.val("Razon");
                //---------------------------------------
                lblComments.val("Comentarios");
                //lblValueComments.val();
            }
        }


    </script>
</asp:Content>