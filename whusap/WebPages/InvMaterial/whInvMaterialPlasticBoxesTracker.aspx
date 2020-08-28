<%@ Page Title="" Language="C#" MasterPageFile="../../MDMasterPage.Master" AutoEventWireup="true" CodeBehind="whInvMaterialPlasticBoxesTracker.aspx.cs" Inherits="whusap.WebPages.InvMaterial.whInvMaterialPlasticBoxesTracker" EnableViewStateMac="false" Theme="Cuadriculas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Encabezado" runat="server">
    <script language="javascript" src="../../Scripts/script.js" type="text/javascript"></script>
    <script type="text/javascript">
        var _idioma = '<%= _idioma %>';
        function guardar(args) {
            if (args.value === "") {
                return;
            }
            var item = document.getElementById(args.id.replace("btnContinue", "txtItem"));
            var type = document.getElementById(args.id.replace("btnContinue", "ddlType"));
            var barcode = document.getElementById(args.id.replace("btnContinue", "txtBarCodeID"));

            if (item === null || type === null || barcode === null) {
                alert(_idioma == "INGLES" ? "All fields are required" : "Todos los campos son obligatorios");
                return;
            }
            var params = "{ 'sItem': 'p1', 'sType': 'p2', 'sBarCode': 'p3', 'sUser': 'p4' }";
            params = params.replace("p1", item.value);
            params = params.replace("p2", type.options[type.selectedIndex].value);
            params = params.replace("p3", barcode.value);
            params = params.replace("p4", '<%= Session["user"] %>');
            $.ajax({
                type: "POST",
                url: "whInvMaterialPlasticBoxesTracker.aspx/GuardarDatos",
                data: params,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    if (msg.d != "") {
                        alert(msg.d);
                        return false;
                    }
                    alert("Save successfully");

                    var item = document.getElementById(args.id.replace("btnContinue", "txtItem"));
                    var type = document.getElementById(args.id.replace("btnContinue", "ddlType"));
                    var barcode = document.getElementById(args.id.replace("btnContinue", "txtBarCodeID"));
                    var message = document.getElementById(args.id.replace("btnContinue", "lblMessage"));

                    item.value = "";
                    type.options[0].selected = true;
                    barcode.value = "";
                    message.value = "";
                    return true;
                },
                error: function (msg) {
                    alert(msg.d);
                    return false;
                }
            });

        }


        function validarItem(args) {
            if (args.value === "") {
                return;
            }
            var parametrosEnviar = "{ 'sItem': '" + document.getElementById(args.id).value + "'}";
            $.ajax({
                type: "POST",
                url: "../../WebServices/WebService.asmx/Getitems",
                data: parametrosEnviar,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    if (msg.d != "") {
                        var objControl = document.getElementById(args.id);
                        var objLabel = document.getElementById(args.id.replace("txtItem", "lblDescItem"));
                        objLabel.innerText = msg.d;
                        if (msg.d === "Item doesn't exist" || msg.d === "Search returned more than one item") {

                            if (msg.d.includes("exist")) {
                                alert(_idioma == "INGLES" ? msg.d : "Articulo no existe");
                            }
                            else {
                                alert(_idioma == "INGLES" ? msg.d : "La busqueda retorno mas de un articulo");
                            }

                            objControl.value = "";
                            return false;
                        }
                        else {
                            return true;
                        }
                    }

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
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True" EnableScriptLocalization="True" EnableCdn="true" EnablePageMethods="true" EnablePartialRendering="true"></asp:ScriptManager>

    <table>
        <tr>
            <td>
                <b>
                    <asp:Label ID="lblItem" runat="server"></asp:Label>
                </b>
            </td>
            <td>
                <asp:TextBox ID="txtItem" runat="server" Width="200px" Height="20px"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="lblDescItem" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <b>
                    <asp:Label ID="lblType" runat="server"></asp:Label>
                </b>
            </td>
            <td colspan="2">
                <asp:DropDownList ID="ddlType" runat="server" Width="200px" Height="20px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <b>
                    <asp:Label ID="lblBarCodeID" runat="server"></asp:Label>
                </b>
            </td>
            <td colspan="2">
                <asp:TextBox ID="txtBarCodeID" runat="server" Width="200px" Height="20px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="3" align="center">
                <asp:Button ID="btnContinue" runat="server" Text="CONTINUE" OnClick="btnContinue_Click" />
            </td>
        </tr>
    </table>
    <div>
        <asp:Label ID="lblMessage" runat="server" ForeColor="Black"></asp:Label>
    </div>

</asp:Content>
