<%@ Page Language="C#" MasterPageFile="~/MDMasterPage.Master" AutoEventWireup="true" CodeBehind="whInvPicking.aspx.cs" Inherits="whusap.WebPages.Migration.whInvPicking" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Encabezado" runat="server">
    <script type="text/javascript">
        var numberCorrect = true;

        function validateNumber(field) {
            debugger;
            var ingresada = field.value;
            var regex = /^\d*[0-9]*[,.]?[0-9]{1,4}$/;
            var re = new RegExp(regex);
            var _idioma = '<%= _idioma %>'

            // Validar ingrese solo digitos enteros
            if (!ingresada.match(re)) {
                field.focus();
                field.value = 0;
                if (_idioma == "ESPAÑOL") {
                    $('#lblError').text('Por favor ingrese un número valido, maximo cuatro digitos.');
                } else {
                    $('#lblError').text('Please, enter a valid number, four digits max');
                }
                numberCorrect = false;
            } else {
                $('#lblError').text('');
                numberCorrect = true;
            }
        }

        function validarFormulario() {
            var validate = true;

            if (!numberCorrect) {
                validate = false;
                if (_idioma == "ESPAÑOL") {
                    $('#lblError').text('Por favor ingrese un número valido, maximo cuatro digitos.');
                } else {
                    $('#lblError').text('Please, enter a valid number, four digits max');
                }
            } else {
                $('#lblError').text('');
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
                    <asp:Label runat="server" ID="lblNumeroRecepcion" /></b></span>
            </td>
            <td style="width: 250px; padding:5px;">
                <span style="vertical-align: middle;">
                    <asp:TextBox runat="server" ID="txtNumeroRecepcion" CssClass="TextboxBig" style="width:100%;" />  
                </span>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align:center;">
                <hr />  
                <asp:Button Text="" runat="server" ID="btnConsultarRecepcion" OnClick="btnConsultarRecepcion_Click" CssClass="ButtonsSendSave" style="height:30px;" />
            </td>
        </tr>
    </table>
    <div runat="server" id="divTable" visible="false">
        <hr />
        
    </div>
    <div style="text-align:right;">
        <asp:Button Text="" runat="server" ID="btnGuardarRecepcion" OnClick="btnGuardarRecepcion_Click" OnClientClick="return validarFormulario()" CssClass="ButtonsSendSave" style="height:30px;" />
    </div>
    <asp:Label Text="" runat="server" ID="lblError" name="lblError" style="color:red; font-size:15px; font-weight:bold;" ClientIDMode="Static" />
    <asp:Label Text="" runat="server" ID="lblConfirm" style="color:green; font-size:15px; font-weight:bold;" ClientIDMode="Static"/>
    <asp:HiddenField runat="server" ID="hdfFuente" />
</asp:Content>
