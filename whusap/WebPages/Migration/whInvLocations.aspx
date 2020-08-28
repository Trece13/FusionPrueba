<%@ Page Language="C#" MasterPageFile="~/MDMasterPage.Master" AutoEventWireup="true" CodeBehind="whInvLocations.aspx.cs" Inherits="whusap.WebPages.Migration.whInvLocations" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Encabezado" runat="server">
    <script type="text/javascript">
        function validateForm() {
        debugger;
            var validate = true;
            var _idioma = '<%= _idioma %>';

            $('#lblConfirm').text('');
            $('#lblError').text('');

            if ($('#ddlOrigenMovimiento').val() == "") {
                $('#ddlOrigenMovimiento').css('border-color', 'red');
                validate = false;
            }else{$('#ddlOrigenMovimiento').css('border-color', '');}

            if ($('#txtNumeroRecepcion').val() == "") {
                $('#txtNumeroRecepcion').css('border-color', 'red');
                validate = false;
            }else{$('#txtNumeroRecepcion').css('border-color', '');}

            if (!validate) {
                if (_idioma == "INGLES") {
                    $('#lblError').text('Please, dilligence all fields');
                }else
                {
                    $('#lblError').text('Por favor diligencie todos los campos.');
                }
            }else
            {
                 $('#lblError').text('');
            }

            return validate;
        }

        function validateQuantity(field, cantexis, cantsug, contad) {
            debugger;
            var ingresada = field.value;
            var regex = /^\d*[0-9]*[,.]?[0-9]{1,4}$/;
            var re = new RegExp(regex);
            var _idioma = '<%= _idioma %>'

            // Validar ingrese solo digitos enteros
            if (ingresada.match(re)) {

                if (isNaN(field.value)) {
                    field.value = 0;
                }
                if (cantexis < ingresada) {
                    if (_idioma == "ESPAÑOL") {
                        $('#lblError').text("La cantidad ingresada es mayor a la cantidad inicial, por favor verifique.");
                    } else {
                        $('#lblError').text('Digited quantity is higher than initial quantity, please check and try again.');
                    }
                    field.value = 0;
                    field.focus();
                } else {
                    $('#lblError').text('');
                }
            } else {
                field.focus();
                field.value = 0;
                if (_idioma == "ESPAÑOL") {
                    $('#lblError').text('Por favor ingrese un número valido, maximo cuatro digitos.');
                } else {
                    $('#lblError').text('Please, enter a valid number, four digits max');
                }
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
     <table border="0">
        <tr>
            <td style="text-align:left;">
                <span style="vertical-align: middle" /><span class="style2" style="vertical-align: middle;">
                <b style="font-size: 11px;">
                    <asp:Label runat="server" ID="lblOrigenMovimiento" /></b></span>
            </td>
            <td style="width: 250px; padding:5px;">
                <span style="vertical-align: middle;">
                    <asp:DropDownList runat="server" ID="ddlOrigenMovimiento" CssClass="TextBoxBig" ClientIDMode="Static">
                    </asp:DropDownList>
                </span>
            </td>
        </tr>
        <tr>
            <td style="text-align:left;">
                <span style="vertical-align: middle" /><span class="style2" style="vertical-align: middle;">
                <b style="font-size: 11px;">
                    <asp:Label runat="server" ID="lblNumeroRecepcion" /></b></span>
            </td>
            <td style="width: 250px; padding:5px;">
                <span style="vertical-align: middle;">
                    <asp:TextBox runat="server" ID="txtNumeroRecepcion" CssClass="TextboxBig" ClientIDMode="Static" />
                </span>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align:center;">
                <hr />  
                <asp:Button Text="" runat="server" ID="btnConsultarRecepcion" OnClick="btnConsultarRecepcion_Click" OnClientClick="return validateForm();" CssClass="ButtonsSendSave" style="height:30px;" />
            </td>
        </tr>
    </table>
    <div runat="server" id="tblReceipts" style="max-height: 400px; margin-top:8px;">

    </div>

    <div style="text-align:right;">
        <asp:Button Text="" runat="server" ID="btnGuardarRecepcion" OnClick="btnGuardarRecepcion_Click" OnClientClick="return validateReceipt();" CssClass="ButtonsSendSave" style="height:30px;" visible="false" />
    </div>
    <asp:Label Text="" runat="server" ID="lblError" style="color:red; font-size:15px; font-weight:bold;" ClientIDMode="Static" />
    <asp:Label Text="" runat="server" ID="lblConfirm" style="color:green; font-size:15px; font-weight:bold;" ClientIDMode="Static" />
</asp:Content>