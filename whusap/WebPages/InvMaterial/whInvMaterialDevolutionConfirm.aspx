<%@ Page Title="" Language="C#" MasterPageFile="../../MDMasterPage.Master" AutoEventWireup="true"
    CodeBehind="whInvMaterialDevolutionConfirm.aspx.cs" Inherits="whusap.WebPages.InvMaterial.whInvMaterialDevolutionConfirm"
    EnableViewStateMac="false" Theme="Cuadriculas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Encabezado" runat="server">
    <script src="https://code.jquery.com/jquery-3.3.1.js"></script>
    <script type="text/javascript" src="https://cdn.datatables.net/1.10.20/js/jquery.dataTables.min.js"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
     <script type="text/javascript">
         function updateStatus(confirmStatus) {
           //  console.log(row);
           //  var comfirmSelectId = row.getAttribute('id').val();
            // var confirmStatus=("#" + comfirmSelectId + " option:selected").val();
             //var rowId = 4;
             //alert(val);
             var palletId = $("#Contenido_txtPalletId").val();
             var parametrosEnviar = "{ 'confirmstatus': '" + confirmStatus + "', 'palletId' : '" + palletId + "'}";
             var objSend = document.getElementById('<%=btnSave.ClientID %>');
             objSend.disabled = true;
             $.ajax({
                 type: "POST",
                 url: "whInvMaterialDevolutionConfirm.aspx/updataPalletStatus",
                 data: parametrosEnviar,
                 contentType: "application/json; charset=utf-8",
                 dataType: "json",
                 success: function (msg) {
                     alert(msg.d);
                 },
                 error: function (msg) {
                     alert(msg.d);
                 }
             });
         }
      </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True"
        EnableScriptLocalization="True" EnablePageMethods="true">
    </asp:ScriptManager>
    <br />
    <table border="0">
        <tr>
            <td style="text-align: left;">
                <strong>
                    <asp:Label class="" ID="lblWorkOrder" runat="server" /></strong>
            </td>
            <td style="width: 250px; padding: 5px;">
                <asp:TextBox ID="txtWorkOrder" class="form-control form-control-lg" runat="server"
                    CausesValidation="True" MaxLength="9" CssClass="TextBoxBig" TabIndex="1" ToolTip="Enter work order number"></asp:TextBox>
            </td>
            <td style="text-align: left;">
                <span>
                    <asp:RegularExpressionValidator ID="minlenght" runat="server" ControlToValidate="txtWorkOrder"
                        ErrorMessage="Remember 9 characters" ValidationExpression="^[a-zA-Z0-9'@&#.\s]{9,9}$"
                        CssClass="errorMsg" SetFocusOnError="True" Display="Dynamic" />
                </span>
                <asp:RequiredFieldValidator ID="RequiredField" runat="server" ErrorMessage="Work Order is required"
                    ControlToValidate="txtWorkOrder" SetFocusOnError="False" CssClass="errorMsg"
                    Enabled="false"></asp:RequiredFieldValidator>
                <asp:CustomValidator ID="OrderError" runat="server" ErrorMessage="Order doesn't exist or don´t have items"
                    SetFocusOnError="True" CssClass="errorMsg"></asp:CustomValidator>
            </td>
        </tr>
        <tr>
            <td style="text-align: left;">
                <strong>
                    <asp:Label class="" ID="lblPalletId" Text="Pallet ID" runat="server" /></strong>
            </td>
            <td style="width: 250px; padding: 5px;">
                <asp:TextBox ID="txtPalletId" class="form-control form-control-lg" runat="server"
                    CausesValidation="True" MaxLength="20" CssClass="TextBoxBig" TabIndex="1" ToolTip="Enter pallet Id"></asp:TextBox>
            </td>
            <td style="text-align: left;">
                <span>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtPalletId"
                        ErrorMessage="Pallet ID can Have More than 12 But less than 21 Characters." ValidationExpression="^[a-zA-Z0-9'@&#-.\/\s]{12,20}$"
                        CssClass="errorMsg" SetFocusOnError="True" Display="Dynamic" />
                </span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Pallet Id is required"
                    ControlToValidate="txtPalletId" SetFocusOnError="False" CssClass="errorMsg"
                    Enabled="false"></asp:RequiredFieldValidator>
                <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Pallet Id doesn't exist"
                    SetFocusOnError="True" CssClass="errorMsg"></asp:CustomValidator>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: center;">
                <hr />
                <asp:Button ID="btnSend" runat="server" Text="Query" OnClick="btnSend_Click" CssClass="ButtonsSendSave" />
            </td>
        </tr>
    </table>
    <div>
        <span>
            <br />
            <div id="printResult" style="width: 80%" runat="server" align="center">
                <span style="text-align: right">
                    <asp:Label ID="lblResult" runat="server" CssClass="style2" Font-Bold="True" Font-Italic="True"></asp:Label>
                </span>
                <br />
                <br />
                <div id="printerDiv" runat="server" style="background-color: White; color: Red;">
                </div>
            </div>
        </span>
    </div>
    <div class="style3">
        <div id="HeaderGrid" style="width: 80%" runat="server">
            <span>
                <asp:Label ID="lblOrder" runat="server" Text="" CssClass="HeaderGrid" Style="display: none"></asp:Label>
            </span><span style="text-align: right">
                <asp:Button ID="btnSave" runat="server" Text="Save Items" Visible="False" OnClick="btnSave_Click"
                    CssClass="ButtonsSendSave" Height="60%" />
            </span>
        </div>
        <br />
        <asp:GridView ID="grdRecords" runat="server" AutoGenerateColumns="False" BorderStyle="Solid"
            BorderWidth="1px" Width="82%" BorderColor="#F1F3F8" Font-Names="Arial" Font-Size="9pt"
            DataKeyNames="IDRECORD" OnRowDataBound="grdRecords_RowDataBound">
            <Columns>
            
                <asp:BoundField HeaderText="Position" DataField="pos">
                    <ItemStyle HorizontalAlign="Center" Width="10%" />
                    <HeaderStyle CssClass="HeaderGrid" HorizontalAlign="Center" />
                </asp:BoundField>
                 
                <asp:BoundField HeaderText="Item" DataField="articulo">
                    <ItemStyle HorizontalAlign="Center" Width="15%" />
                    <HeaderStyle CssClass="HeaderGrid" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="Description" DataField="Descripcion">
                    <ItemStyle HorizontalAlign="Left" Width="25%" />
                    <HeaderStyle CssClass="HeaderGrid" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="Warehouse" DataField="Almacen">
                    <ItemStyle HorizontalAlign="Center" Width="10%"/>
                    <HeaderStyle CssClass="HeaderGrid" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="lot" HeaderText="Lot Code">
                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                    <HeaderStyle CssClass="HeaderGrid" HorizontalAlign="Center" />
                </asp:BoundField>

                <asp:BoundField DataField="palletID" HeaderText="Pallet Id" >
                    <ItemStyle HorizontalAlign="Center" Width="13%" />
                    <HeaderStyle CssClass="HeaderGrid" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="Return Qty." DataField="retQty" DataFormatString="{0}">
                    <ItemStyle HorizontalAlign="Center" Width="4%" />
                    <HeaderStyle CssClass="HeaderGrid" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="Unit" DataField="unidad">
                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                    <HeaderStyle CssClass="HeaderGrid" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField  ControlStyle-Width="70px"
                    HeaderText="Confirmed">
                    <ItemTemplate>
                        <asp:DropDownList ID="OptionsList" runat="server"  BackColor="#F4F5F7" CssClass="DropDownList"
                            Width="5%" Height="20px"   DataField="conf">
                            <asp:ListItem Value="2">No</asp:ListItem>
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                        </asp:DropDownList>
                    </ItemTemplate>
                    <ControlStyle Width="70px"></ControlStyle>
                    <HeaderStyle CssClass="HeaderGrid" HorizontalAlign="Center" />
                    <ItemStyle Width="10px"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Confirmed" HeaderStyle-CssClass="HeaderColor" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="prin" runat="server" Text="Label" Visible="false"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <RowStyle BackColor="#F7F8FB" Font-Names="Arial" Font-Size="9pt" ForeColor="Black" />
        </asp:GridView>
    </div>
</asp:Content>
