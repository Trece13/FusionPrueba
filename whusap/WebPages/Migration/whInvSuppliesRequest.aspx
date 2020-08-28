<%@ Page Language="C#" MasterPageFile="~/MDMasterPage.Master" AutoEventWireup="true" CodeBehind="whInvSuppliesRequest.aspx.cs" Inherits="whusap.WebPages.Migration.whInvSuppliesRequest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Encabezado" runat="server">
<style>
    .CampoOculto
    {
        display:none;
    }
</style>
    <script type="text/javascript">
        var _idioma = '<%= _idioma %>';
        function validarMachine() {
            var str = document.getElementById("machine").value;
            var lenght = str.length;
            if (str != "") {
                if (lenght < 4 || lenght > 6) {
                    alert(_idioma == "INGLES" ? "Machine Code beetwen 4 to 6 characters" : "El codigo de la maquina debe estar entre 4 y 6 caracteres")
                    document.getElementById("txtMachine").focus();
                    document.getElementById("txtMachine").value = "";
                }
            }
        };

        function obtenerValor(field, i) {
            debugger;
            var validaItem = true;
            var valueField = $(field).val();

            if (valueField.trim() != "") {
                for (var a = 1; a < 6; a++) {
                    var fieldValidate = 'txtValueSelect' + a;
                    var val = $('#' + fieldValidate).val();

                    if (val == valueField) {
                        validaItem = false;
                        alert(_idioma == "INGLES" ? "Item was selected in the sequence " + a : "El item ya ha sido seleccionado en la secuencia " + a);
                    }
                }
            }

            if (validaItem) {
                var txtValue = 'txtValueSelect' + i;
                $('#' + txtValue).val(valueField);
            } else {
                debugger;
                field.value = "";
            }
        };

        function validarFormulario() {
            debugger;
            var validaFormulario = true;
            var mensaje = "";
            var validItemSelected = false;

            for (var i = 1; i < 6; i++) {
            
                var itemSelected = $('#slItem' + i).val();

                if (itemSelected.trim() != "") {
                    validItemSelected = true;
                    var txtQuantity = $('#txtQuantity' + i).val()
                    var txtMachine = $('#txtMachine' + i).val();

                    if (txtQuantity <= 0) {
                        mensaje = _idioma == "INGLES" ? "Quantity can not be equal or minor zero. Sequence " + i : "La cantidad no puede ser menor o igual a cero. Sencuencia " + i;
                        validaFormulario = false;
                    }else if(txtMachine.trim() == "")
                    {
                        mensaje = _idioma == "INGLES" ? "Machine code field is blank. Sequence " + i : "El campo del código de la maquina esta en blanco. Sencuencia " + i;
                        validaFormulario = false;
                    }
                }
            }
            debugger;

            if (!validItemSelected) {
                mensaje = _idioma == "INGLES" ? "Please, enter data to save" : "Por favor ingrese datos para guardar.";
                validaFormulario = false;
            }

            if (!validaFormulario) {
                alert(mensaje);
            }

            return validaFormulario;
        };
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <table border="0">
        <tr>
            <td style="text-align:left;">
                <span style="vertical-align: middle" /><span class="style2" style="vertical-align: middle;">
                <b style="font-size: 11px;">
                    <asp:Label runat="server" ID="lblMachine" /></b></span>
            </td>
            <td style="width: 250px; padding:5px;">
                <span style="vertical-align: middle;">
                    <asp:TextBox runat="server" ID="txtMachine" onblur="validarOrden()" CssClass="TextBoxBig" ClientIDMode="Static" />
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
    
    <div runat="server" id="divTable" visible="false" style="text-align:center;">
        <hr />
        <table class='table table-bordered myTable' style='font-size:13px;  text-align:center; font-weight:bold;'>
	        <tr style='background-color: lightgray;'>
		        <td><asp:Label runat="server" ID="lblMachineTable" /></td>
		        <td><asp:Label runat="server" ID="lblValueMachine" /></td>
                <td colspan="2">
                    <asp:Button ID="btnSaveOrder" OnClick="btnSaveOrder_Click" OnClientClick="return validarFormulario();" runat="server" CssClass="ButtonsSendSave" />
                </td>
                <td  class = "CampoOculto"><asp:Label runat="server" /></td>
	        </tr>
	        <tr style='background-color: white;'>
		        <td><asp:Label runat="server" ID="lblSequence" /></td>
		        <td><asp:Label runat="server" ID="lblItem" /></td>
		        <td><asp:Label runat="server" ID="lblQuantity" /></td>
		        <td class = "CampoOculto"><asp:Label runat="server" ID="lblMachine1" /></td>
                <td class = "CampoOculto"><asp:Label runat="server" ID="lblValueSelect" /></td>
	        </tr>

            <tr>
                <td>1</td>
                <td><asp:DropDownList runat="server" ClientIDMode="Static" ID="slItem1" CssClass="TextBoxBig MyDrop" onchange="obtenerValor(this,1);"></asp:DropDownList></td>
                <td><asp:TextBox TextMode="Number" ClientIDMode="Static" min="0" step="any" runat="server" ID="txtQuantity1" CssClass="TextBoxBig MyTextNum" /></td>
                <td class = "CampoOculto"><asp:TextBox ID="txtMachine1" ClientIDMode="Static" runat="server" CssClass="TextBoxBig MyTextNum"></asp:TextBox></td>
                <td class = "CampoOculto"><asp:TextBox ID="txtValueSelect1" name="txtValueSelect1" ClientIDMode="Static" runat="server" CssClass="TextBoxBig MyText" Enabled="false"></asp:TextBox></td>
            </tr>
            <tr>
                <td>2</td>
                <td><asp:DropDownList ID="slItem2" ClientIDMode="Static" runat="server" CssClass="TextBoxBig MyDrop" onchange="obtenerValor(this,2);"></asp:DropDownList></td>
                <td><asp:TextBox min="0" step="any" ClientIDMode="Static" ID="txtQuantity2" TextMode="Number" runat="server" CssClass="TextBoxBig MyTextNum" /></td>
                <td class = "CampoOculto"><asp:TextBox ID="txtMachine2" ClientIDMode="Static" runat="server" CssClass="TextBoxBig MyTextNum"></asp:TextBox></td>
                <td class = "CampoOculto"><asp:TextBox ID="txtValueSelect2" ClientIDMode="Static" runat="server" CssClass="TextBoxBig MyText" Enabled="false"></asp:TextBox></td>
            </tr>
            <tr>
                <td>3</td>
                <td><asp:DropDownList ID="slItem3" ClientIDMode="Static" runat="server" CssClass="TextBoxBig MyDrop" onchange="obtenerValor(this,3);"></asp:DropDownList></td>
                <td><asp:TextBox min="0" step="any" ID="txtQuantity3" ClientIDMode="Static" TextMode="Number" runat="server" CssClass="TextBoxBig MyTextNum" /></td>
                <td class = "CampoOculto"><asp:TextBox ID="txtMachine3" ClientIDMode="Static" runat="server" CssClass="TextBoxBig MyTextNum"> </asp:TextBox></td>
                <td class = "CampoOculto"><asp:TextBox ID="txtValueSelect3" ClientIDMode="Static" runat="server" CssClass="TextBoxBig MyText" Enabled="false"></asp:TextBox></td>
            </tr>
            <tr>
                <td>4</td>
                <td><asp:DropDownList ID="slItem4" ClientIDMode="Static" runat="server" CssClass="TextBoxBig MyDrop" onchange="obtenerValor(this,4);"></asp:DropDownList></td>
                <td><asp:TextBox min="0" step="any" ID="txtQuantity4" ClientIDMode="Static" TextMode="Number" runat="server" CssClass="TextBoxBig MyTextNum"/></td>
                <td class = "CampoOculto"><asp:TextBox ID="txtMachine4" ClientIDMode="Static" runat="server" CssClass="TextBoxBig MyTextNum"></asp:TextBox></td>
                <td class = "CampoOculto"><asp:TextBox ID="txtValueSelect4" ClientIDMode="Static" runat="server" CssClass="TextBoxBig MyText" Enabled="false"></asp:TextBox></td>
            </tr>
            <tr>
                <td>5</td>
                <td><asp:DropDownList ID="slItem5" ClientIDMode="Static" runat="server" CssClass="TextBoxBig MyDrop" onchange="obtenerValor(this,5);"></asp:DropDownList></td>
                <td><asp:TextBox min="0" step="any" ID="txtQuantity5" ClientIDMode="Static" TextMode="Number" runat="server" CssClass="TextBoxBig MyTextNum" /></td>
                <td class = "CampoOculto"><asp:TextBox ID="txtMachine5" ClientIDMode="Static" runat="server" CssClass="TextBoxBig MyTextNum"></asp:TextBox></td>
                <td class = "CampoOculto"><asp:TextBox ID="txtValueSelect5" ClientIDMode="Static" runat="server" CssClass="TextBoxBig MyText" Enabled="false"></asp:TextBox></td>
            </tr>
        </table>
    </div>

    <asp:Label Text="" runat="server" ID="lblError" style="color:red; font-size:15px; font-weight:bold;" ClientIDMode="Static" />
    <asp:Label Text="" runat="server" ID="lblConfirm" style="color:green; font-size:15px; font-weight:bold;" ClientIDMode="Static" />

    <style>
        .MyDrop
        {
            width:180px !important;
        }
        
        .MyTextNum
        {
            width:80px !important;
        }
        
        .MyText
        {
            width:100px !important;
        }
        .myTable
        {
            width:70% !important;
        }
    </style>
</asp:Content>
