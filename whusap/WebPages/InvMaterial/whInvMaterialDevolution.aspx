<%@ Page Title="" Language="C#" MasterPageFile="../../MDMasterPage.Master" AutoEventWireup="true"
    EnableViewStateMac="false" CodeBehind="whInvMaterialDevolution.aspx.cs" Inherits="whusap.WebPages.InvMaterial.whInvMaterialDevolution"
    Theme="Cuadriculas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Encabezado" runat="server">
    <script language="javascript" src="../../Scripts/script.js" type="text/javascript"></script>
    <script type="text/javascript" src="https://code.jquery.com/jquery-3.3.1.js"></script>
    <script type="text/javascript" src="https://cdn.datatables.net/1.10.20/js/jquery.dataTables.min.js"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css"/>
    <style type="text/css">
        .style3
        {
            width: 768px;
        }
        .none
        {
            display:none;
        }
    </style>
    <script type="text/javascript">
        var _idioma = '<%= _idioma %>';
        window.onload = function () {
            var e = document.getElementById("printerDiv");
        };

        function limpiar(obj) {
            var objControl = document.getElementById(obj.id);
            // objControl.value = "";
            objControl.focus();
            return;
        }

        function validaPaid(args, rowIndex) {
            //var parametrosEnviar = "{ 'valor':'" + args.value + "', 'quantityToReturn': '" + quantityToReturn + "'}";
            var quantityToReturn = $("#Contenido_grdRecords_toReturn_"+ rowIndex).val();
            var parametrosEnviar = "{ 'palletID': '" + args.value + "', 'quantity':'" + quantityToReturn + "'}";
            //alert(" pId " + args.value + " toReturn " + quantityToReturn);
            var objSend = document.getElementById('<%=btnSave.ClientID %>');
            objSend.disabled = true;
            $.ajax({
                type: "POST",
                url: "whInvMaterialDevolution.aspx/vallidatePalletID",
                data: parametrosEnviar,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    if (msg.d != "") {
                        var objControl = document.getElementById(args.id);
                        objControl.value = "";
                        alert(msg.d);
                        return false;
                    } else { objSend.disabled = false; }
                },
                error: function (msg) {
                    alert("This is msg " + msg.d);
                    return false;
                }
            });
        }

        function validaLot(args, val, obj) {
            if (args.value == "") {
                return;
            }
            var parametrosEnviar = "{ 'Fila': '" + JSON.stringify(val) + "', 'valor':'" + obj + "'}";
            var objSend = document.getElementById('<%=btnSave.ClientID %>');
            objSend.disabled = true;
            $.ajax({
                type: "POST",
                url: "whInvMaterialDevolution.aspx/validaExistLot",
                data: parametrosEnviar,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    if (msg.d != "") {
                        var objControl = document.getElementById(args.id);
                        objControl.value = "";
                        objControl.focus();
                        alert(_idioma == "INGLES" ? "Pending to Confirm" : "Pendiente por confirmar");
                        return false;
                    } else { objSend.disabled = false; }

                   
                },
                error: function (msg) {
                    var objControl = document.getElementById(args.id);
                    objControl.value = "";
                    objControl.focus();
                    alert(msg.d);
                    return false;
                }
            });
        }

        $.fn.exists = function () {
            return this.length !== 0;
        }

        function printTag() {
            if ($("#Contenido_printerDiv").exists()) {
                $("#Contenido_printerDiv").width(400);
                $("#Contenido_printerDiv").height(400);
                $("#Contenido_printerDiv").html("<iframe src='../Labels/whInvPrintLabel.aspx' width='100%'; height='100%';></iframe>"); //onload='this.contentWindow.print();
                $("#Contenido_printerDiv").html(""); //onload='this.contentWindow.print();
            }
        }

        function validarCantidadMaxima(args, val, opcion, rowIndex) {
            if (args.value === "" || val.value === "") {
                return;
            }
            
            var rowSelected = "";
            var valor = "";
            var lote = "";

            if (opcion === 0) {
                rowSelected = document.getElementById(args.id);
                valor = rowSelected.value; //"1000";
                lote = document.getElementById(args.id.replace("toReturn", "toLot")).value;
            }
            else if (opcion === 1) {
                rowSelected = document.getElementById(args.id);
                valor = document.getElementById(args.id.replace("toLot", "toReturn")).value; //"1000";
                lote = rowSelected.value;
            }
            else
                return;
            var parametrosEnviar = "{ 'fila': '" + JSON.stringify(val) + "', 'valor':'" + valor + "', 'lote':'" + lote + "','qty':'" + (args.id.indexOf("toReturn") != -1 ? ($("#Contenido_grdRecords_toReturn_" + args.id.substring(30, args.id.length)).val()) : ($("#Contenido_grdRecords_toReturn_" + args.id.substring(27, args.id.length)).val())) + "'}";
            var objSend = document.getElementById('<%=btnSave.ClientID %>');
            objSend.disabled = true;
            $.ajax({
                type: "POST",
                url: "whInvMaterialDevolution.aspx/validarCantidades",
                data: parametrosEnviar,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    if (msg.d != "") {
                        var objControl = document.getElementById(args.id);

                        if (objControl.type === "select-one") {
                            objControl.selectedIndex = 0;
                            objControl.focus();
                        }
                        else {
                            objControl.value = "";
                            objControl.focus();
                        }
                        alert(_idioma == "INGLES" ? msg.d : "La cantidad a retornar es invalida.");
                        return false;
                    } else { objSend.disabled = false; }

                    
                },
                error: function (msg) {
                    var objControl = document.getElementById(args.id);
                    var objSend = document.getElementById('<%=btnSave.ClientID %>');
                    objControl.value = "  ";
                    objControl.focus();
                    alert(msg.d);
                    return false;
                }
            });
        }

        function validateQty(rowIndex) {
            var quantityToReturn = $("#Contenido_grdRecords_toReturn_" + rowIndex).val();
            if (quantityToReturn > 0) {
                $("#Contenido_grdRecords_palletId_" + rowIndex).attr("disabled", false)

            } else {
                $("#Contenido_grdRecords_palletId_" + rowIndex).attr("disabled", true)
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True"
        EnableScriptLocalization="True" EnableCdn="true" EnablePageMethods="true" EnablePartialRendering="true">
    </asp:ScriptManager>
    <table border="0">
        <tr>
            <td style="text-align: left;">
                <strong>
                    <asp:Label class="" ID="lblWorkOrder" runat="server" /></strong>
            </td>
            <td style="width: 250px; padding: 5px;">
                <asp:TextBox ID="txtWorkOrder" class="form-control form-control-lg" runat="server"
                    CausesValidation="True" MaxLength="9" CssClass="TextBoxBig" TabIndex="1" ToolTip="Enter work order number"></asp:TextBox>
                <span>
                    <asp:RegularExpressionValidator ID="minlenght" runat="server" ControlToValidate="txtWorkOrder"
                        ErrorMessage="Remember 9 characters" ValidationExpression="^[a-zA-Z0-9'@&#.\s]{9,9}$"
                        CssClass="errorMsg" SetFocusOnError="True" Display="Dynamic" />
                </span>
            </td>
            <td style="text-align: left;">
                <asp:RequiredFieldValidator ID="RequiredField" runat="server" ErrorMessage="Work Order is required"
                    SetFocusOnError="False" Display="Dynamic" ControlToValidate="txtWorkOrder" CssClass="errorMsg"
                    Enabled="False" />
                <asp:CustomValidator ID="OrderError" runat="server" ErrorMessage="Order doesn't exist or the status is active, release or completed."
                    SetFocusOnError="True" CssClass="errorMsg" Style="color: red;"></asp:CustomValidator>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: center;">
                <hr />
                <asp:Button ID="btnSend" runat="server" Text="Query" OnClick="btnSend_Click" CssClass="ButtonsSendSave" />
            </td>
        </tr>
    </table>
    <div runat="server" id="divBtnGuardar" visible="false">
    </div>
    <div>
        <span>
            <br />
            <br />
            <div id="printResult" style="width: 80%" runat="server" align="center">
                <span style="text-align: right">
                    <asp:Label ID="lblResult" runat="server" CssClass="style2" Font-Bold="True" Font-Italic="True"></asp:Label>
                </span>
                <br />
                <br />
                <span style="text-align: center">
                    <asp:Button ID="printLabel" runat="server" Height="30px" Text="Print Work Order Tags"
                        Visible="False" OnClick="printLabel_Click" CssClass="ButtonsSendSave" />
                </span>
            </div>
        </span>
    </div>
    <div id="printerDiv">
    </div>
    <div class="style3" style="width: 80%">
        <div id="HeaderGrid" style="width: 80%; height: 40px; margin-left: 40px;" runat="server">
            <span style="text-align: right;">
                <asp:Button ID="btnSave" runat="server" Text="Save Items" Visible="False" OnClick="btnSave_Click"
                    CssClass="ButtonsSendSave" Width="90px" Height="20px" />
            </span>
        </div>
        <br />

        <span style="text-align: right">
              <asp:Label ID="lblValidationResult" runat="server" CssClass="style2" Font-Bold="True" Font-Italic="True" Visible="true"></asp:Label>
        </span>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:GridView ID="grdRecords" runat="server" AutoGenerateColumns="False" DataKeyNames="ID"
                    OnRowDataBound="grdRecords_RowDataBound" SkinID="Default">
                    <Columns>
                        <asp:BoundField HeaderText="Position" DataField="pos">
                            <ItemStyle HorizontalAlign="Center" Width="10%" />
                            <HeaderStyle CssClass="HeaderGrid" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Item" DataField="ARTICULO">
                            <ItemStyle HorizontalAlign="Center" Width="15%" />
                            <HeaderStyle CssClass="HeaderGrid" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Description" DataField="Descripcion">
                            <ItemStyle HorizontalAlign="Left" Width="40%" />
                            <HeaderStyle CssClass="HeaderGrid" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Warehouse" DataField="Almacen">
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle CssClass="HeaderGrid" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Actual Qty" DataField="cant" DataFormatString="{0}">
                            <ItemStyle HorizontalAlign="Center" Width="10%" />
                            <HeaderStyle CssClass="HeaderGrid" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Unit" DataField="unidad">
                            <ItemStyle HorizontalAlign="Center" Width="10%" />
                            <HeaderStyle CssClass="HeaderGrid" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="To Return" HeaderStyle-CssClass="HeaderColor" ControlStyle-Width="10%">
                            <ItemTemplate>
                                <asp:TextBox ID="toReturn" runat="server" Width="12%" MaxLength="12" CausesValidation="True"
                                    CssClass="TextBox" />
                                <asp:RangeValidator ID="validateQuantity" runat="server" Type="Double" ControlToValidate="toReturn"
                                    ErrorMessage="Quantity to return cannot be greater than Actual Quantity" Display="Dynamic"
                                    ForeColor="Red" Font-Names="Arial" Font-Size="9" Font-Italic="True" SetFocusOnError="true"
                                    MaximumValue="0" MinimumValue="0" />
                                <asp:RegularExpressionValidator ID="validateReturn" runat="server" ControlToValidate="toReturn"
                                    ErrorMessage="Only numbers allowed" SetFocusOnError="true" ValidationExpression="[0-9]+(\.[0-9]{1,4})?"
                                    Display="Dynamic" ForeColor="Red" Font-Names="Arial" Font-Size="9" Font-Italic="True" />
                            </ItemTemplate>
                            <ControlStyle Width="70px"></ControlStyle>
                            <HeaderStyle CssClass="HeaderColor" HorizontalAlign="Center" />
                            <ItemStyle Width="10px"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Lote" HeaderStyle-CssClass="HeaderGrid" ControlStyle-Width="15%"
                            HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:DropDownList ID="toLot" runat="server" CausesValidation="true" Width="15%" MaxLength="15"
                                    CssClass="TextBox" />
                            </ItemTemplate>
                            <ControlStyle Width="90px"></ControlStyle>
                            <HeaderStyle CssClass="HeaderGrid" HorizontalAlign="Center" />
                            <ItemStyle Width="10px"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField Visible="false">
                            <ItemTemplate>
                                <asp:HiddenField ID="LOTE" runat="server" Value='<%# Eval("LOTE") %>' Visible="False" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:BoundField DataField="mlot" HeaderText="Lote" ReadOnly="True" ShowHeader="False"
                            Visible="False" />
                        <asp:TemplateField HeaderText="Pallet ID" HeaderStyle-CssClass="HeaderColor"  ControlStyle-Width="35%"
                            HeaderStyle-Width="35%" InsertVisible="False" ShowHeader="False" 
                            Visible="False">
                            <ItemTemplate>
                                <asp:TextBox ID="palletId" runat="server" MinLength="12" MaxLength="13" CausesValidation="True" 
                                    CssClass="TextBox" Enabled="false" />
                            </ItemTemplate>
                            <ControlStyle Width="100px"></ControlStyle>
                            <HeaderStyle CssClass="HeaderColor" HorizontalAlign="Center" />
                            <ItemStyle Width="10px"></ItemStyle>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Stat" DataField="Stat" ShowHeader="False">
                            <ItemStyle HorizontalAlign="Left" Width="0%" CssClass="none" />
                            <ControlStyle CssClass="none" />
                        <FooterStyle CssClass="none" />
                            <HeaderStyle CssClass="none" HorizontalAlign="Center" />
                        </asp:BoundField>
                    </Columns>
                    <HeaderStyle HorizontalAlign="Center" Height="20px" />
                    <RowStyle BackColor="#F7F8FB" Font-Names="Arial" Font-Size="9pt" ForeColor="#1B1B1B"
                        VerticalAlign="Middle" />
                </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
