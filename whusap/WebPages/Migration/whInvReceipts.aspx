<%@ Page Language="C#" AutoEventWireup=" true" MasterPageFile="~/MDMasterPage.Master" CodeBehind="whInvReceipts.aspx.cs" Inherits="whusap.WebPages.Migration.whInvReceipts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Encabezado" runat="server">
    <script type="text/javascript">
        function validateReceipt() {
        debugger
            var response = true;
            $('.validateReceipt').each(function () {
            debugger;
                if ($(this).val().trim() == "" || $(this).val().trim() == "0") {
                    response = false;  
                }
            });

            if (!response) {
                var idioma = '<%= _idioma %>';
                debugger
                if (idioma == "INGLES") {
                    $('#lblError').text("Receipt does not contains all data or are incorrect");
                } else if (idioma == "ESPAÑOL") {
                    $('#lblError').text('La recepción no tiene los datos completos o estan incorrectos.');
                }
            } else {
                $('#lblError').text('');
            }
                        
            return response;
        }

        function checklot(mlot, i, field) {
            debugger
            $('#lblError').text('');
            var idioma = '<%= _idioma %>';
            if (field.value >= 0) {
                $('#lblError').text('');
                var mlot1 = mlot;
                var j = <% = _countReceipts %>

                var cant = field.value;

                if (cant != "") {
                    var bodega = "#bodega-" + i;
                    if(mlot1 == 1){
                        $(bodega).attr('disabled', false);
                        $(bodega).addClass('validateReceipt');
                        if ($(bodega).val().trim() == "" || $(bodega).val() == "0") {
                            $(bodega).css('border-color','red');
                        }
                        $(bodega).focus();
                    }else
                    {
                        $(bodega).attr('disabled', true);
                        $(bodega).focus();
                    }
                }else{
                    field.focus();
                }
            }else
            {
                if (idioma == "INGLES") {
                    $('#lblError').text('Quantity value cannot be less than zero');        
                }
                else
                {
                    $('#lblError').text('La cantidad no puede ser menor a 0');    
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
    <hr style="border: 0px;" />

    <asp:Label Text="" runat="server" ID="lblError" style="color:red; font-size:15px; font-weight:bold;" ClientIDMode="Static" />
    <asp:Label Text="" runat="server" ID="lblConfirm" style="color:green; font-size:15px; font-weight:bold;" ClientIDMode="Static" />
    
    <div style="text-align:right;">
        <asp:Button Text="" runat="server" ID="btnGuardarRecepcion" OnClick="btnGuardarRecepcion_Click" OnClientClick="return validateReceipt();" CssClass="ButtonsSendSave" style="height:30px;" visible="false" />
    </div>
    <div runat="server" id="tblReceipts" style="max-height: 400px; overflow-y: scroll; margin-top:8px;">

    </div>
</asp:Content>
