<%@ Page Language="C#" MasterPageFile="~/MDMasterPage.Master"  AutoEventWireup="true" CodeBehind="whInvLabelPalletTags.aspx.cs" Inherits="whusap.WebPages.Migration.whInvLabelPalletTags" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Encabezado" runat="server">
    <script type="text/javascript">
        var _idioma = '<%= _idioma %>';
        var _tipoFormulario = '<%= _tipoFormulario %>'

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

        function printDiv(divID) {
            //PRINT LOCAL HOUR
            if (_tipoFormulario != "REPRINT") {
                var d = new Date();
                var x = document.getElementById("lblValueDate");
                var h = addZero(d.getHours());
                var m = addZero(d.getMinutes());
                var s = addZero(d.getSeconds());
                //x.innerHTML = d.toUTCString();
                x.innerHTML = d.toLocaleString();
            }

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

        function addZero(i) {
            if (i < 10) {
                i = "0" + i;
            }
            return i;
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
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <asp:Label Text="" runat="server" ID="lblInfo" style="color:Black; font-size:medium;" />
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
        <tr runat="server" id="trSecuence" visible="false">
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
    <hr />
    <asp:Label Text="" runat="server" ID="lblError" style="color:red; font-size:15px; font-weight:bold;" ClientIDMode="Static" />
    <asp:Label Text="" runat="server" ID="lblConfirm" style="color:green; font-size:15px; font-weight:bold;" ClientIDMode="Static" />

    <div runat="server" id="divTable" visible="false" clientidmode="Static">
    <hr />
        <table style="width: 5.8in; height: 3.8in; text-align:center; font-weight:bold;" border="1" cellspacing="0" cellpadding="0">
            <tr>
                <td colspan="2"><asp:Label runat="server" ID="lblDescItem"></asp:Label></td>
                <td><asp:Label runat="server" ID="lblMadeIn"></asp:Label> <asp:Label runat="server" ID="lblValueMadeIn"></asp:Label></td>
            </tr>
            <tr>
                <td colspan="3"><img src="~/images/logophoenix_login.jpg" runat="server" id="imgCodeItem" alt="" hspace="60" vspace="5" style="width: 3in; height: .5in;"/></td>
            </tr>
            <tr>
                <td colspan="3"><asp:Label runat="server" ID="lblItem"></asp:Label></td>
            </tr>
            <tr>
                <td colspan="3"><img src="~/images/logophoenix_login.jpg" runat="server" id="imgCodeSqnb" alt="" hspace="60" vspace="5" style="width: 3in; height: .5in;"/></td>
            </tr>
            <tr>
                <td><asp:Label runat="server" ID="lblWorkOrder"></asp:Label></td>
                <td><asp:Label runat="server" ID="lblPalletNumber"></asp:Label></td>
                <td><asp:Label runat="server" ID="lblInspectorInitial"></asp:Label></td>
            </tr>
            <tr>
                <td><asp:Label runat="server" ID="lblValueWorkOrder"></asp:Label></td>
                <td><asp:Label runat="server" ID="lblValuePalletNumber"></asp:Label></td>
                <td><asp:Label runat="server" ID="lblValueInspectorInitial"></asp:Label></td>
            </tr>
            <tr>
                <td><asp:Label runat="server" ID="lblDate"></asp:Label></td>
                <td><asp:Label runat="server" ID="lblShift"></asp:Label></td>
                <td><asp:Label runat="server" ID="lblCasePerPallet"></asp:Label></td>
            </tr>
            <tr>
                <td><asp:Label runat="server" ID="lblValueDate" ClientIDMode="Static"></asp:Label></td>
                <td><asp:Label runat="server" ID="lblValueShift"></asp:Label></td>
                <td><asp:Label runat="server" ID="lblValueCasePerPallet"></asp:Label></td>
            </tr>
        </table>
    </div>
    <table runat="server" id="divBotones" visible="false" style="margin-bottom:10px; width: 5.8in; text-align:center; font-weight:bold;" border="1" cellspacing="0" cellpadding="0">
        <tr>
            <td colspan="2"><a href="#" onclick="javascript:printDiv('divTable')" runat="server" id="linkPrint"></a></td>
            <td runat="server" id="tdBtnExit"><asp:Button class="buttonMenu" runat="server" ID="btnSalir" OnClick="btnExit_Click" AutoPostBack="true" /></td>
        </tr>
    </table>
</asp:Content>