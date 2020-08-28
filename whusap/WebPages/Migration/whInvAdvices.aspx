<%@ Page Language="C#" MasterPageFile="~/MDMasterPage.Master" AutoEventWireup="true" CodeBehind="whInvAdvices.aspx.cs" Inherits="whusap.WebPages.Migration.whInvAdvices" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Encabezado" runat="server">
    <script type="text/javascript">
        var _idioma = '<%= _idioma %>';
        var fineLote = true;
        var fineUbicacion = true;
        var fineCantida = true;

        function validateFormulario() {
            var validate = true;
            var idioma = '<%= _idioma %>';

            if ($('#txtCantSugerida').val() == "") {
                validate = false;
                $('#txtCantSugerida').css('border-color','red');
            } else { $('#txtCantSugerida').css('border-color', ''); }

            if ($('#txtUbicacion').val() == "" && $('#hdfUbicacion').val().trim() != "") {
                validate = false;
                $('#txtUbicacion').css('border-color', 'red');
            } else { $('#txtUbicacion').css('border-color', ''); }

            if ($('#txtLote').val() == "" && $('#hdfLote').val().trim() != "") {
                validate = false;
                $('#txtLote').css('border-color', 'red');
            } else { $('#txtLote').css('border-color', ''); }

            if (!validate) {
                if (idioma == "ESPAÑOL") {
                    $('#lblError').text("Por favor diligencie todos los campos");
                } else {
                    $('#lblError').text("Please, enter information on all fields");
                }
            }

            debugger;
            if (!fineCantida) {
                if (_idioma == "ESPAÑOL") {
                    $('#lblError').text('Cantidad Ingresada no corresponde con la original.  Por favor revise.');
                } else {
                    $('#lblError').text('Digited quantity not correspond whit the initial quantity, please check and try again');
                }
                validate = false;
            } else if (!fineLote) {
                if (_idioma == "ESPAÑOL") {
                    $('#lblError').text('Lote incorrecta, por favor verifique');
                } else {
                    $('#lblError').text('Incorrect lot, please verify and try again');
                }
                validate = false;
            } else if (!fineUbicacion) {
                if (_idioma == "ESPAÑOL") {
                    $('#lblError').text('Ubicación incorrecta, por favor verifique');
                } else {
                    $('#lblError').text('Incorrect location, please verify and try again');
                }
                validate = false;
            } else { $('#lblError').text("") }

            return validate;
        }

        function validarCantidad() {
            debugger;
            $('#lblConfirm').text("");
            var cantSugerida = $('#hdfCantidadSugerida').val();
            var cantIngresada = $('#txtCantSugerida').val();
            var regex = /^\d*[0-9]*[,.]?[0-9]{1,5}$/;
            var re = new RegExp(regex);

            //Validar ingrese solo digitos enteros
            if (cantIngresada.match(re)) {
                if (parseFloat(cantSugerida) < parseFloat(cantIngresada)) {
                    if (_idioma == "ESPAÑOL") {
                        $('#lblError').text('Cantidad Ingresada no corresponde con la original.  Por favor revise.');
                    } else {
                        $('#lblError').text('Digited quantity not correspond whit the initial quantity, please check and try again');
                    }
                    $('#txtCantSugerida').val("0");
                    $('#txtCantSugerida').focus();
                    fineCantida = false;

                } else {
                    $('#lblError').text('');
                    fineCantida = true;
                }
            } else {
                $('#txtCantSugerida').focus();
                $('#txtCantSugerida').val("0");
                fineCantida = false;

                if (_idioma == "ESPAÑOL") {
                    $('#lblError').text('Por favor ingrese un número valido, maximo cuatro digitos.');
                } else {
                    $('#lblError').text('Please, enter a valid number, four decimals max');
                }
            }
        }

        function validarUbicacion () {
        debugger
            var ubicacionIngresada = $('#txtUbicacion').val().toUpperCase();
            var ubicacionGuardada = $('#hdfUbicacion').val();

            if (ubicacionIngresada.trim() != ubicacionGuardada.trim()) {
                if (_idioma == "ESPAÑOL") {
                    $('#lblError').text('Ubicación incorrecta, por favor verifique');
                } else {
                    $('#lblError').text('Incorrect location, please verify and try again');
                }

                fineUbicacion = false;
                $('#txtUbicacion').focus();
            } else {
                fineUbicacion = true;
                $('#lblError').text('');
            }
        }

        function validarLote () {
        debugger
            var loteIngresado = $('#txtLote').val().toUpperCase();
            var loteGuardado = $('#hdfLote').val();

            if (loteIngresado.trim() != loteGuardado.trim()) {
                if (_idioma == "ESPAÑOL") {
                    $('#lblError').text('Lote incorrecto, por favor verifique');
                } else {
                    $('#lblError').text('Incorrect lot, please verify and try again');
                }
                fineLote = false;
                $('#txtLote').focus();
            } else {
                fineLote = true;
                $('#lblError').text('');
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
                    <asp:Label runat="server" ID="lblSugNumero" /></b></span>
            </td>
            <td style="width: 250px; padding:5px;">
                <span style="vertical-align: middle;">
                    <asp:TextBox runat="server" ID="txtNumero" CssClass="TextBoxBig" ClientIDMode="Static" />
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
    <div runat="server" id="divTable" visible="false">
        <hr />  
        <table class="table table-bordered" style='font-size:13px; border:3px solid; border-style:outset;'>
            <tr style='background-color: darkblue; color: white; font-weight:bold;'>
                <td><b><asp:Label runat="server" ID="lblOrden"></asp:Label></b></td>
                <td colspan="4"><asp:Label runat="server" ID="lblValueOrden" /></td>
            </tr>
            <tr style="background-color: lightgray;">
                <td><b><asp:Label runat="server" ID="lblArticulo"></asp:Label></b></td>
                <td colspan="4"><asp:Label runat="server" ID="lblValueArticulo" /></td>
            </tr>
            <tr style="background-color:White; font-weight:bold;">
                <td><asp:Label Text="" runat="server" ID="lblCantSugerida" /></td>
                <td><asp:Label Text="" runat="server" ID="lblUnidad" /></td>
                <td><asp:Label Text="" runat="server" ID="lblAlmacen" /></td>
                <td><asp:Label Text="" runat="server" ID="lblUbicacion" /></td>
                <td><asp:Label Text="" runat="server" ID="lblLote" /></td>
            </tr>
            <tr>
                <td><asp:Label Text="" runat="server" ID="lblValueCantSugerida" /></td>
                <td><asp:Label Text="" runat="server" ID="lblValueUnidad" /></td>
                <td><asp:Label Text="" runat="server" ID="lblValueAlmacen" /></td>
                <td><asp:Label Text="" runat="server" ID="lblValueUbicacion" /></td>
                <td><asp:Label Text="" runat="server" ID="lblValueLote" /></td>
            </tr>
            <tr>
                <td><asp:TextBox ID="txtCantSugerida" runat="server" CssClass="Textbox" ClientIDMode="Static" onblur="validarCantidad();" /></td>
                <td><asp:Label Text="" runat="server" ID="lblValueUnidadTwo" /></td>
                <td><asp:Label Text="" runat="server" ID="lblValueAlmacenTwo" /></td>
                <td><asp:TextBox ID="txtUbicacion" runat="server" CssClass="Textbox" ClientIDMode="Static" onblur="validarUbicacion();" /></td>
                <td><asp:TextBox ID="txtLote" runat="server" CssClass="Textbox" ClientIDMode="Static" onblur="validarLote();" /></td>
            </tr>
        </table>
        <div style="text-align:right;">
            <asp:Button Text="" runat="server" ID="btnGuardar" OnClick="btnGuardar_Click" OnClientClick="return validateFormulario();" CssClass="ButtonsSendSave" style="height:30px;" />
        </div>
    </div>
    
    <asp:Label Text="" runat="server" ID="lblError" style="color:red; font-size:15px; font-weight:bold;" ClientIDMode="Static" />
    <asp:Label Text="" runat="server" ID="lblConfirm" style="color:green; font-size:15px; font-weight:bold;" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="hdfCantidadPedida" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="hdfCantidadSugerida" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="hdfUbicacion" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="hdfLote" ClientIDMode="Static" />
</asp:Content>
