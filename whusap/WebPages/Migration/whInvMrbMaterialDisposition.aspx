<%@ Page Title="" Language="C#" MasterPageFile="../../MDMasterPage.Master" AutoEventWireup="true"
    CodeBehind="whInvMrbMaterialDisposition.aspx.cs" Inherits="whusap.WebPages.Migration.whInvMrbMaterialDisposition"
    EnableViewStateMac="false" Theme="Cuadriculas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Encabezado" runat="server">
    
      <script language="javascript" src="../../Scripts/script.js" type="text/javascript"></script>
    <style type="text/css">
        .style3
        {
            width: 768px;
        }
        
        .RightValidation
        {
            border-color: green;
        }
        .WrongValidation
        {
            border-color: red;
        }
        
        .LabelsError
        {
            color: Red;
            font-size: 15px;
            font-weight: bold;
        }
    </style>
    <script type="text/javascript">

       // txtItemError = '<%=lblitemError%>';
      //  txtLotError = '<%=lblLotError%>';
        //opI009584-003 
      //  LsItems = JSON.parse('<%= ListaItemsJSON%>');
        window.onload = function () {
            var e = document.getElementById("printerDiv");
        };

        function limpiar(obj) {
            var objControl = document.getElementById(obj.id);
            // objControl.value = "";
            objControl.focus();
            return;
        }

      

        $.fn.exists = function () {
            return this.length !== 0;
        }



        function printTag() {
            if ($("#Contenido_printerDiv").exists()) {
                $("#Contenido_printerDiv").width(400);
                $("#Contenido_printerDiv").height(400);
                //$("#Contenido_printerDiv").html("<iframe src='../Labels/whInvPrintLabel.aspx' width='100%'; height='100%';></iframe>"); //onload='this.contentWindow.print();
                $("#Contenido_printerDiv").html(""); //onload='this.contentWindow.print();
            }


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
                    <asp:Label class="" ID="lblMrbWarehouse" Text="MRB Warehouse"  runat="server" /></strong>
            </td>
            <td style="width: 250px; padding: 5px;">
                    <asp:DropDownList runat="server" ID="dropDownWarehouse" CssClass="TextBoxBig"></asp:DropDownList>
            
                
            </td>
            <td style="text-align: left;">
                <span>
                   
                </span>
                <asp:RequiredFieldValidator ID="RequiredField" runat="server" ErrorMessage="MRB Warrhouse is required"
                    ControlToValidate="dropDownWarehouse" SetFocusOnError="False" CssClass="errorMsg"
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
                    CausesValidation="True" MaxLength="20" CssClass="TextBoxBig" TabIndex="1" ToolTip="Enter pallet Id"  ></asp:TextBox>
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
               
                 <asp:Button ID="btnSend" runat="server"  Text="Query" Width="70px"
                Height="20px" OnClick="btnSend_Click" CssClass="ButtonsSendSave" />

                <asp:Label Text="" runat="server" ID="lblError" style="color:red; font-size:15px; font-weight:bold;" ClientIDMode="Static" />
  
              
            </td>
        </tr>
    </table>

     <div id="printerDiv">
    </div>
    <div class="style3" style="width: 80%">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <br />
                <br />
                <div id="printResult" style="width: 80%" runat="server" align="center">
                    <span style="text-align: right">
                        <asp:Label ID="lblResult" runat="server" CssClass="style2" Font-Bold="True" Font-Italic="True"></asp:Label>
                         <asp:Label Text="" runat="server" ID="lblError1" style="color:red; font-size:15px; font-weight:bold;" ClientIDMode="Static" />
  
                         <asp:HiddenField ID="lbltable" runat="server" Visible="False" />
                    </span>
                    <br />
                    <br />
                  
                </div>
                <div id="HeaderGrid" style="width: 80%; height: 40px" runat="server">
                    <span style="text-align: right;">
                        <asp:Button ID="btnSave" runat="server" Text="Save Items" Visible="False" OnClick="btnSave_Click"
                            CssClass="ButtonsSendSave" Width="90px" Height="20px" Enabled="False" />
                    </span>
                </div>
                <br />
                <asp:GridView ID="grdRecords" runat="server" AutoGenerateColumns="False" OnRowDataBound="grdRecords_RowDataBound"
                    SkinID="Default">
                    <Columns>
                        <asp:BoundField HeaderText="Item" DataField="item">
                            <ItemStyle HorizontalAlign="Center" Width="15%" />
                            <HeaderStyle CssClass="HeaderGrid" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Description" DataField="Desci">
                            <ItemStyle HorizontalAlign="Left" Width="40%" />
                            <HeaderStyle CssClass="HeaderGrid" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Warehouse" DataField="warehouse">
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle CssClass="HeaderGrid" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="stock" DataFormatString="{0:d}" Visible="False" InsertVisible="False"
                            ShowHeader="False">
                            <ItemStyle HorizontalAlign="Center" Width="10%" />
                            <HeaderStyle CssClass="HeaderGrid" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:TemplateField InsertVisible="False" ShowHeader="False" Visible="False">
                            <ItemTemplate>
                                <asp:HiddenField ID="ActualQty" runat="server" Value='<%# Eval("stock") %>' Visible="False" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Lot" DataField="lot">
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle CssClass="HeaderGrid" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Unit" DataField="unidad">
                            <ItemStyle HorizontalAlign="Center" Width="10%" />
                            <HeaderStyle CssClass="HeaderGrid" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Quantity" HeaderStyle-CssClass="HeaderColor" ControlStyle-Width="10%">
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
                        <asp:TemplateField HeaderText="Disposition" HeaderStyle-CssClass="HeaderColor" ControlStyle-Width="15%"
                            HeaderStyle-Width="10%">
                            <ItemTemplate>
                                <asp:DropDownList ID="Dispoid" runat="server" name="Dispoid" Width="70%" Font-Names="Calibri" Font-Size="Small"
                                    OnSelectedIndexChanged="Dispoid_SelectedIndexChanged" AutoPostBack="True">
                                    <asp:ListItem Value="1">Select Disposition Reason</asp:ListItem>
                                    <asp:ListItem Value="2">Return to Vendor</asp:ListItem>
                                    <asp:ListItem Value="3">Return to Stock</asp:ListItem>
                                    <asp:ListItem Value="4">Regrind</asp:ListItem>
                                    <asp:ListItem Value="5">Recycle</asp:ListItem>
                                </asp:DropDownList>
                            </ItemTemplate>
                            <ControlStyle Width="80px"></ControlStyle>
                            <HeaderStyle CssClass="HeaderColor" HorizontalAlign="Center" />
                            <ItemStyle Width="10px"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Reason" HeaderStyle-CssClass="HeaderColor" ControlStyle-Width="15%"
                            HeaderStyle-Width="10%">
                            <ItemTemplate>
                                <asp:DropDownList ID="Reasonid" runat="server" Width="20%" Font-Names="Calibri" Font-Size="Small">
                                </asp:DropDownList>
                            </ItemTemplate>
                            <ControlStyle Width="80px"></ControlStyle>
                            <HeaderStyle CssClass="HeaderColor" HorizontalAlign="Center" />
                            <ItemStyle Width="10px"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Supplier" HeaderStyle-CssClass="HeaderColor" ControlStyle-Width="15%"
                            HeaderStyle-Width="10%">
                            <ItemTemplate>
                                <asp:DropDownList ID="Supplier" runat="server" Font-Names="Calibri" Font-Size="Small"
                                    Enabled="False">
                                </asp:DropDownList>
                            </ItemTemplate>
                            <ControlStyle Width="80px"></ControlStyle>
                            <HeaderStyle CssClass="HeaderColor" HorizontalAlign="Center" />
                            <ItemStyle Width="10px"></ItemStyle>
                        </asp:TemplateField>
                        <asp:BoundField DataField="mlot" HeaderText="Lote" ReadOnly="True" ShowHeader="False"
                            Visible="False" />
                        <asp:TemplateField HeaderText="Stock Warehouse">
                            <ControlStyle Width="80px"></ControlStyle>
                            <ItemTemplate>
                                <asp:DropDownList ID="Stockwareh" runat="server" Font-Names="Calibri" Font-Size="Small"
                                    Width="70%" Enabled="False">
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Regrind Item">
                            <ControlStyle Width="80px"></ControlStyle>
                            <ItemTemplate>
                                <asp:DropDownList ID="Regrind" runat="server" Font-Names="Calibri" Font-Size="Small"
                                    Width="70%" Enabled="False">
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Comments">
                            <EditItemTemplate>
                                <asp:TextBox ID="Comments" runat="server" Font-Names="Calibri" Font-Size="Small"
                                    TextMode="MultiLine">Comments</asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:TextBox ID="Comments" runat="server" Font-Names="Calibri" Font-Size="Small"
                                    TextMode="MultiLine"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <RowStyle BackColor="#F7F8FB" Font-Names="Arial" Font-Size="9pt" ForeColor="#1B1B1B"
                        VerticalAlign="Middle" />
                </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    
  
</asp:Content>
