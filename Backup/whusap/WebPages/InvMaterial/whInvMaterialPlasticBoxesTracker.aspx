<%@ Page Title="" Language="C#" MasterPageFile="../../MDMasterPage.Master" AutoEventWireup="true" CodeBehind="whInvMaterialPlasticBoxesTracker.aspx.cs" Inherits="whusap.WebPages.InvMaterial.whInvMaterialPlasticBoxesTracker" EnableViewStateMac="false"  Theme="Cuadriculas" %>


<asp:Content ID="Content1" ContentPlaceHolderID="Encabezado" runat="server">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">    
    <script language="javascript" src="../../Scripts/script.js" type="text/javascript"></script> 
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True" EnableScriptLocalization="True" EnableCdn="true" EnablePageMethods="true" EnablePartialRendering="true">
    
    
    </asp:ScriptManager>

    <script type="text/javascript">
        function guardar(args) {
            if (args.value === "") {
                return;
            }
            var item    = document.getElementById(args.id.replace("btnContinue", "txtItem"));
            var type    = document.getElementById(args.id.replace("btnContinue", "ddlType"));
            var barcode = document.getElementById(args.id.replace("btnContinue", "txtBarCodeID"));

            if (item === null || type === null || barcode === null) {
                alert("All fields are required");
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
                            alert(msg.d);
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
    
    <table>
        <tr>
            <td><asp:label ID="lblItem" runat="server"><b>Item</b></asp:label></td>
            <td>
                <asp:TextBox ID="txtItem" runat="server" Width="200px" Height="20px"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="lblDescItem" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td><asp:label ID="lblType" runat="server"><b>Type</b></asp:label></td>
            <td colspan="2">
                <asp:DropDownList ID="ddlType" runat="server" Width="200px" Height="20px">
                    <asp:ListItem value="0" Selected="True">Select...</asp:ListItem>
                    <asp:ListItem Value="1">Box</asp:ListItem>
                    <asp:ListItem Value="2">Master</asp:ListItem>
                    <asp:ListItem Value="3">Cover</asp:ListItem>
                </asp:DropDownList> 
            
            </td>
        </tr>
        <tr>
            <td><asp:label ID="lblBarCodeID" runat="server"><b>Bar Code ID</b></asp:label></td>
            <td colspan="2"><asp:TextBox ID="txtBarCodeID" runat="server" Width="200px" Height="20px"></asp:TextBox></td>
        </tr>
        <tr>
            <td colspan="3" align="center">
                <asp:Button id="btnContinue" runat="server" Text="CONTINUE" 
                    onclick="btnContinue_Click" />
            </td>
        </tr>
    </table>

    <div>
        <asp:label ID="lblMessage" runat="server" ForeColor="Black"></asp:label>
    </div>

</asp:Content>
