<%@ Page Language="C#" MasterPageFile="~/MDMasterPage.Master"  AutoEventWireup="true" CodeBehind="whInvTime.aspx.cs" Inherits="whusap.WebPages.Migration.whInvTime" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Encabezado" runat="server">
    <script type="text/javascript">
        var _idioma = '<%= _idioma %>';
        function validarOrden() {
            if (document.getElementById("txtWorkOrder").value != "") {
                if (document.getElementById("txtWorkOrder").value.length < 9 || document.getElementById("txtWorkOrder").value.length > 9) {
                    alert(_idioma == "INGLES" ? "Please use this format WORKORDER, remember only 9 characters"
                    : "Por favor use el forma de order de trabajo, solo 9 caracteres.");
                    document.getElementById("txtWorkOrder").focus();
                    document.getElementById("txtWorkOrder").value = "";
                    return false;
                }
            }
            else {
                document.getElementById("txtWorkOrder").focus();
                document.getElementById("txtWorkOrder").value = "";
            }
        };

        function validarFormulario() {
            var validate = true;
            var mensaje = "";
            var _idioma = '<%= _idioma %>';

            var slHoursLaborType = $('#slHoursLaborType').val();
            var slHours = $('#slHours').val();
            var slMinutes = $('#slMinutes').val();
            var slComments = $('#slComments').val();

            if (slHoursLaborType.trim() == "") {
                validate = false;
                mensaje += _idioma == "INGLES" ? "-Hourly Labor Type\n" : "-Tipo de trabajo por hora\n";
            }

            if (slHours.trim() == "") {
                validate = false;
                mensaje += _idioma == "INGLES" ? "-Hours\n" : "-Horas\n";
            }

            if (slMinutes.trim() == "") {
                validate = false;
                mensaje += _idioma == "INGLES" ? "-Minutes\n" : "-Minutos\n";
            }

            if (slComments.trim() == "") {
                validate = false;
                mensaje += _idioma == "INGLES" ? "-Comments\n" : "-Comentarios\n";
            }

            debugger;
            if (!validate) {
                alert((_idioma == "INGLES" ? "Please, enter this fields:\n" : "Por favor ingrese los siguientes campos:\n") + mensaje);
            }

            return validate;
        }
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
                    <asp:TextBox runat="server" ID="txtWorkOrder" onblur="validarOrden();" CssClass="TextBoxBig" ClientIDMode="Static" />
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


    <div runat="server" id="divBtnGuardar" visible="false">
        <hr />
        <asp:Button ID="btnGuardar" runat="server" OnClick="btnGuardar_Click" OnClientClick="return validarFormulario();" CssClass="ButtonsSendSave"/>
    </div>

    <div runat="server" id="divTable">

    </div>

    <asp:Label Text="" runat="server" ID="lblError" style="color:red; font-size:15px; font-weight:bold;" ClientIDMode="Static" />
    <asp:Label Text="" runat="server" ID="lblConfirm" style="color:green; font-size:15px; font-weight:bold;" ClientIDMode="Static" />
</asp:Content>
