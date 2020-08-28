<%@ Page Title="" Language="C#" MasterPageFile="~/MDMasterPage.Master" AutoEventWireup="true" CodeBehind="whInvMaterialPlasticReturnBoxesTracker.aspx.cs" Inherits="whusap.WebPages.InvMaterial.whInvMaterialPlasticReturnBoxesTracker" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Encabezado" runat="server">
     <script type="text/javascript">
     var _idioma = '<%= _idioma %>';
     var tCoverBarCode = document.getElementById("<%= txtCoverBarCode.ClientID %>");
     var tMasterBarCode1 = document.getElementById("<%= txtMasterBarCode1.ClientID %>");
     var tMasterBarCode2 = document.getElementById("<%= txtMasterBarCode2.ClientID %>");
     var tMasterBarCode3 = document.getElementById("<%= txtMasterBarCode3.ClientID %>");
     var tBoxBarCode = document.getElementById("<%= txtBoxBarCode.ClientID %>");

     var tCover = document.getElementById("<%= txtCover.ClientID %>");
     var tMaster1 = document.getElementById("<%= txtMaster1.ClientID %>");
     var tMaster2 = document.getElementById("<%= txtMaster2.ClientID %>");
     var tMaster3 = document.getElementById("<%= txtMaster3.ClientID %>");
     var tBox = document.getElementById("<%= txtBox.ClientID %>");

     function checkNumber(evt) {
         evt = (evt) ? evt : window.event;
         var charCode = (evt.which) ? evt.which : evt.keyCode;
         if (charCode > 31 && (charCode < 48 || charCode > 57)) {
             status = _idioma == "INGLES" ?  "This field accepts numbers only." : "Este campo solo admite números";
             alert(status);
             return false;
         }
         status = "";
         return true;
     };

     function _toUpperCase(args) {
         if (args.value === "") {
             return;
         }
         var objControl = document.getElementById(args.id);
         objControl.value = objControl.value.toUpperCase();
     }

     function validarItemByBarCode(args, sTipo, idMaster) {
         if (args.value === "") {
             return;
         }
         var parametrosEnviar = "{ sBarCode: '" + document.getElementById(args.id).value + "', " +
                                     "sTipo: '" + sTipo + "'}";
         $.ajax({
             type: "POST",
             url: "../../WebServices/WebService.asmx/GetItemByBarCode",
             data: parametrosEnviar,
             contentType: "application/json; charset=utf-8",
             dataType: "json",
             success: function (msg) {
                 if (msg.d != "") {
                     var idRow = "Contenido_";
                     var array = eval(msg.d);
                     var js = JSON.stringify(msg.d);
                     var controlTexto;
                     var controlLabel;

                     if (idMaster === 1) {
                         controlTexto = document.getElementById("<%= txtMaster1.ClientID %>");
                         controlLabel = document.getElementById("<%= lblDescMaster1.ClientID %>");
                         idRow += "tMaster1";
                     }
                     else if (idMaster === 2) {
                         controlTexto = document.getElementById("<%= txtMaster2.ClientID %>");
                         controlLabel = document.getElementById("<%= lblDescMaster2.ClientID %>");
                         idRow += "tMaster2";
                     }
                     else if (idMaster === 3) {
                         controlTexto = document.getElementById("<%= txtMaster3.ClientID %>");
                         controlLabel = document.getElementById("<%= lblDescMaster3.ClientID %>");
                         idRow += "tMaster3";
                     }
                     else {
                         controlTexto = document.getElementById("<%= txtCover.ClientID %>");
                         controlLabel = document.getElementById("<%= lblDescCover.ClientID %>");
                         idRow += "tCover";
                     }

                     if (array.length > 0) {
                         controlTexto.value = array[0][0];
                         controlLabel.innerHTML = array[0][1];
                         menuActivacion(idRow, false);
                     }
                     else {
                         controlTexto.value = ("Bar Code doesn't exist").toUpperCase();
                         controlLabel.innerHTML = "";
                         menuActivacion(idRow, true);
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

     function validarItem(args, idText, idDesc, name) {
         if (args.value === "") {
             return;
         }
         var parametrosEnviar = "{ sItem: '" + document.getElementById(args.id).value + "'}";
         $.ajax({
             type: "POST",
             url: "../../WebServices/WebService.asmx/GetItems",
             data: parametrosEnviar,
             contentType: "application/json; charset=utf-8",
             dataType: "json",
             success: function (msg) {
                 if (msg.d != "") {
                     var objControl = document.getElementById(args.id);
                     var objLabel = document.getElementById(args.id.replace(idText, idDesc));
                     msg.d = msg.d.replace("Item", name);
                     objLabel.innerText = msg.d;
                     if (msg.d === "Box doesn't exist" || msg.d === "Search returned more than one item") {
                         if (msg.d.includes("Box")) {
                             alert(_idioma == "INGLES" ? msg.d : "Caja no existe");
                         } else {
                             alert(_idioma == "INGLES" ? msg.d : "La busqueda retorno mas de un articulo");
                         }

                         alert(msg.d);
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

     function _toUpper(args) {
         var objControl = document.getElementById(args.id);
         var _value = objControl.value;
         objControl.value = _value.toUpperCase();
     };

     function menuActivacion(idRow, siDesactivo) {
         switch (idRow) {
             case "Contenido_tCover":
                 desactivarOActivarFila("Contenido_tMaster1", siDesactivo);
                 desactivarOActivarFila("Contenido_tMaster2", siDesactivo);
                 desactivarOActivarFila("Contenido_tMaster3", siDesactivo);
                 desactivarOActivarFila("Contenido_tBox", siDesactivo);
                 desactivarOActivarFila("Contenido_tBoton", siDesactivo);
                 break;
             case "Contenido_tMaster1":
                 desactivarOActivarFila("Contenido_tMaster2", siDesactivo);
                 desactivarOActivarFila("Contenido_tMaster3", siDesactivo);
                 desactivarOActivarFila("Contenido_tBox", siDesactivo);
                 desactivarOActivarFila("Contenido_tBoton", siDesactivo);
                 break;
             case "Contenido_tMaster2":
                 desactivarOActivarFila("Contenido_tMaster3", siDesactivo);
                 desactivarOActivarFila("Contenido_tBox", siDesactivo);
                 desactivarOActivarFila("Contenido_tBoton", siDesactivo);
                 break;
             case "Contenido_tMaster3":
                 desactivarOActivarFila("Contenido_tBox", siDesactivo);
                 desactivarOActivarFila("Contenido_tBoton", siDesactivo);
                 break;
             case "Contenido_tBox":
                 desactivarOActivarFila("Contenido_tBoton", siDesactivo);
                 break;
             default:
                 desactivarOActivarFila("Contenido_tBody", siDesactivo);
                 break;

         }
     };

     function desactivarOActivarFila(idRow, siDesactivo) {
         var nodes = document.getElementById(idRow).getElementsByTagName("*");
         for (var i = 0; i < nodes.length; i++) {
             if (nodes[i].nodeName === "INPUT") {
                 if (siDesactivo)
                     nodes[i].disabled = true;
                 else
                     nodes[i].disabled = false;
             }
         }
     }
     </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
  
    <table runat="server"> 
        <thead>
            <tr>
                <th>&nbsp;</th>
                <th><strong><asp:Label ID="lblBarCode" runat="server" /></strong></th>
                <th><strong><asp:label ID="lblItem" runat="server" /></strong></th>
                <th><strong><asp:label ID="lblDescDescription" runat="server" /></strong></th>
                <th><strong><asp:label ID="lblDescQuantity" runat="server" /></strong></th>
            </tr>
        </thead>
        <tbody id="tBody">
            <tr id="tCover">
                <td>
                    <b><asp:label ID="lblCover" runat="server"></asp:label></b>
                </td>
                <td>
                    <asp:TextBox ID="txtCoverBarCode" runat="server" Width="200px" Height="20px"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txtCover" runat="server" Width="200px" Height="20px"></asp:TextBox>
                </td>
                 <td>
                    <asp:Label ID="lblDescCover" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtQCover" runat="server" Width="200px" Height="20px"  Text="1"></asp:TextBox>
                </td>
            </tr>
            <tr id="tMaster1">
                <td>
                    <b><asp:label ID="lblMaster1" runat="server"></asp:label></b>
                </td>
                <td>
                    <asp:TextBox ID="txtMasterBarCode1" runat="server" Width="200px" Height="20px"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txtMaster1" runat="server" Width="200px" Height="20px"></asp:TextBox>
                </td>
                 <td>
                    <asp:Label ID="lblDescMaster1" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtQMaster1" runat="server" Width="200px" Height="20px" Text="1"></asp:TextBox>
                </td>
            </tr>
            <tr id="tMaster2">
                <td>
                    <b><asp:label ID="lblMaster2" runat="server"></asp:label></b>
                </td>
                <td>
                    <asp:TextBox ID="txtMasterBarCode2" runat="server" Width="200px" Height="20px"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txtMaster2" runat="server" Width="200px" Height="20px" ></asp:TextBox>
                </td>
                 <td>
                    <asp:Label ID="lblDescMaster2" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtQMaster2" runat="server" Width="200px" Height="20px"  Text="1"></asp:TextBox>
                </td>
            </tr>
			<tr id="tMaster3">
                <td>
                   <b><asp:label ID="lblMaster3" runat="server"></asp:label></b>
                </td>
                <td>
                    <asp:TextBox ID="txtMasterBarCode3" runat="server" Width="200px" Height="20px"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txtMaster3" runat="server" Width="200px" Height="20px" ></asp:TextBox>
                </td>
                 <td>
                    <asp:Label ID="lblDescMaster3" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtQMaster3" runat="server" Width="200px" Height="20px"  Text="1"></asp:TextBox>
                </td>
            </tr>
            <tr id="tBox">
                <td>
                    <b><asp:label ID="lblBox" runat="server"></asp:label></b>
                </td>
                <td>
                    <asp:TextBox ID="txtBoxBarCode" runat="server" Width="200px" Height="20px" ></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txtBox" runat="server" Width="200px" Height="20px"></asp:TextBox>
                </td>
                 <td>
                    <asp:Label ID="lblDescBox" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtQBox" runat="server" Width="200px" Height="20px" Text="72"></asp:TextBox>
                </td>
            </tr>
            <tr id="tBoton">
                <td colspan="4" align="center">
                    <asp:Button id="btnContinue" runat="server" Text="CONTINUE" onclick="btnContinue_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="4" align="center">
                    <asp:Label id="lblMensaje" runat="server" Text="" ForeColor="Black"/>
                </td>
            </tr>
        </tbody>
    </table>

</asp:Content>
