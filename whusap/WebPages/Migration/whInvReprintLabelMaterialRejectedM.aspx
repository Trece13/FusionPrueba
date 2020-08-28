<%@ Page Language="C#" MasterPageFile="~/MDMasterPage.Master" AutoEventWireup="true" CodeBehind="whInvReprintLabelMaterialRejectedM.aspx.cs" Inherits="whusap.WebPages.Migration.whInvReprintLabelMaterialRejectedM" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Encabezado" runat="server">
    <style>
        #divTable
        {
            margin-bottom: 100px;
        }
    </style>
    <script type="text/javascript">
        
        function validarOrden() {
            if (document.getElementById("txtOrder").value != "") {
                if (document.getElementById("txtOrder").value.length < 9 || document.getElementById("txtOrder").value.length > 9) {
                    alert(_idioma == "INGLES" ? "Please use this format WORKORDER, remember only 9 characters"
                    : "Por favor use el forma de order de trabajo, solo 9 caracteres.");
                    document.getElementById("txtOrder").focus();
                    document.getElementById("txtOrder").value = "";
                    return false;
                }
            }
            else {
                document.getElementById("txtOrder").focus();
                document.getElementById("txtOrder").value = "";
            }
        };

        function validarSecuencia() {
            var regex = /^-?\d*[0-9]*[.]?[0-9]*$/;
            var re = new RegExp(regex);
            var lenght = document.getElementById("txtSecuence").value.length;
            var str = document.getElementById("txtSecuence").value.trim();
            if (str.match(re)) {
                if (str != "") {
                    if (str.length < 3 || str.length > 3) {
                        alert(_idioma == "INGLES" ? "Please use this format Sequence, remember only 3 characters" : "Por favor use el formato de secuencia, solo 3 caracteres.")
                        document.getElementById("txtSecuence").focus();
                        document.getElementById("txtSecuence").value = "";
                        return false;
                    }
                }
            }
            else {
                document.getElementById("txtSecuence").focus();
                document.getElementById("txtSecuence").value = "";
                alert(_idioma == "INGLES" ? "Only numbers allowed on secuence" : "Solo se permiten números en la secuencia");
            }
        };

        function printDiv(divID) {
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
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <table border="0">
        <tr>
            <td style="text-align:left;">
                <span style="vertical-align: middle" /><span class="style2" style="vertical-align: middle;">
                <b style="font-size: 11px;">
                    <asp:Label runat="server" ID="lblOrder" /></b></span>
            </td>
            <td style="width: 250px; padding:5px;">
                <span style="vertical-align: middle;">
                    <asp:TextBox runat="server" ID="txtOrder" onblur="validarOrden()" CssClass="TextBoxBig" ClientIDMode="Static" />
                </span>
            </td>
        </tr>
        <tr>
            <td style="text-align:left;">
                <span style="vertical-align: middle" /><span class="style2" style="vertical-align: middle;">
                <b style="font-size: 11px;">
                    <asp:Label runat="server" ID="lblLot" /></b></span>
            </td>
            <td style="width: 250px; padding:5px;">
                <span style="vertical-align: middle;">
                    <asp:TextBox runat="server" ID="txtLot" CssClass="TextBoxBig" ClientIDMode="Static" />
                </span>
            </td>
        </tr>
        <tr>
            <td style="text-align:left;">
                <span style="vertical-align: middle" /><span class="style2" style="vertical-align: middle;">
                <b style="font-size: 11px;">
                    <asp:Label runat="server" ID="lblSecuence" /></b></span>
            </td>
            <td style="width: 250px; padding:5px;">
                <span style="vertical-align: middle;">
                    <asp:TextBox runat="server" ID="txtSecuence" onblur="validarSecuencia()" CssClass="TextBoxBig" ClientIDMode="Static" />
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
         <a href="#" runat="server" id="linkPrint" onclick="javascript:printDiv('divTable')" style="color: #000000; font-size: medium"><button class="buttonMenu" runat="server" id="btnPrint" style="width:25%;"></button></a>
    </div>

    <div runat="server" id="divTable" clientidmode="Static" visible="false">
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
                <td style="border-bottom:0px;"><asp:Label Text="" runat="server" ID="lblDescLot" /></td>
                <td>
                    <asp:Label runat="server" ID="lblRejected"></asp:Label> -
                    <asp:Label ID="lblValueQuantity" runat="server"></asp:Label> -
                    <asp:Label ID="lblValueUnit" runat="server"></asp:Label>
                </td>
                <td><img src="~/images/logophoenix_login.jpg" runat="server" id="imgQuantity" alt="" hspace="60" vspace="5" style="width: 2in; height: .5in;  margin:0px;"/></td>
            </tr>
            <tr>
                <td style="border-top:0px;"><img src="~/images/logophoenix_login.jpg" runat="server" id="imgLot" alt="" hspace="60" vspace="5" style="width: 1in; height: .5in;  margin:0px;"/></td>
                <td>
                    <asp:Label runat="server" ID="lblWorkOrder" /> -
                    <asp:Label ID="lblValueWorkOrder" runat="server" />
                </td>
                <td><img src="~/images/logophoenix_login.jpg" runat="server" id="imgWorkOrder" alt="" hspace="60" vspace="5" style="width: 2in; height: .5in;  margin:0px;"/></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblPrintedBy" runat="server" />:
                    <asp:Label ID="lblValuePrintedBy" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblFecha" runat="server" />:
                    <asp:Label ID="lblValueFecha" runat="server" />
                </td>
                <td>
                    <asp:Label ID="lblMachine" runat="server" />:
                    <asp:Label ID="lblValueMachine" runat="server" />
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
        <hr />
    </div>

    <asp:Label Text="" runat="server" ID="lblError" style="color:red; font-size:15px; font-weight:bold;" ClientIDMode="Static" />
    <asp:Label Text="" runat="server" ID="lblConfirm" style="color:green; font-size:15px; font-weight:bold;" ClientIDMode="Static" />
</asp:Content>