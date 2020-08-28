<%@ Page Title="" Language="C#" MasterPageFile="~/MDMasterPage.Master" AutoEventWireup="true" CodeBehind="whInvMaterialPlasticUsedBoxesTracker.aspx.cs" Inherits="whusap.WebPages.InvMaterial.whInvMaterialPlasticUsedBoxesTracker" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Encabezado" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    
    <script type="text/javascript">
        function validarMaquina(args) {
            if (args.value === "") {
                return;
            }
            var parametrosEnviar = "{ 'sMachine': '" + document.getElementById(args.id).value + "'}";
            $.ajax({
                type: "POST",
                url: "../../WebServices/WebService.asmx/ValidarMaquina",
                data: parametrosEnviar,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    if (msg.d != "") {
                        var objControl = document.getElementById(args.id);
                        if (msg.d === "0") {
                            alert("Machine doesn't exists");
                        }
                        else {
                            alert(msg.d);
                        }
                        objControl.value = "";
                        return false;
                    }
                    return true;
                },
                error: function (msg) {
                    var objControl = document.getElementById(args.id);
                    objControl.value = "";
                    objControl.focus();
                    alert(msg.d);
                    return false;
                }
            });
        };


        function validarIdentifier(args) {
            if (args.value === "") {
                return;
            }
            var parametrosEnviar = "{ 'sIdentifier': '" + document.getElementById(args.id).value + "'}";
            $.ajax({
                type: "POST",
                url: "../../WebServices/WebService.asmx/ValidarIdentifier",
                data: parametrosEnviar,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    if (msg.d != "") {
                        var objControl = document.getElementById(args.id);
                        if (msg.d === "0") {
                            alert("Identifier doesn't exist");
                        }
                        else {
                            alert(msg.d);
                        }
                        objControl.value = "";
                        return false;
                    }
                    return true;
                },
                error: function (msg) {
                    var objControl = document.getElementById(args.id);
                    objControl.value = "";
                    objControl.focus();
                    alert(msg.d);
                    return false;
                }
            });
        };

        function _toUpper(args) {
            var objControl = document.getElementById(args.id);
            var _value = objControl.value;
            objControl.value = _value.toUpperCase();
        }


    </script>


    <table>
        <tr>
            <td><asp:label ID="lblMachine" runat="server"><b>Machine:</b></asp:label></td>
            <td>
                <asp:TextBox ID="txtMachine" runat="server" Width="200px" Height="20px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td><asp:label ID="lblIdentifier" runat="server"><b>Identifier</b></asp:label></td>
            <td><asp:TextBox ID="txtIdentifier" runat="server" Width="200px" Height="20px"></asp:TextBox></td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:Button id="btnContinue" runat="server" Text="CONTINUE" 
                    onclick="btnContinue_Click"/>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:Label id="lblMensaje" runat="server" Text="" ForeColor="Black"/>
            </td>
        </tr>
    </table>

</asp:Content>
