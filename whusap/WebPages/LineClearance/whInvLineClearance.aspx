<%@ Page Title="" Language="C#" MasterPageFile="~/MDMasterPage.Master" AutoEventWireup="true"
    CodeBehind="whInvLineClearance.aspx.cs" Inherits="whusap.WebPages.LineClearance.whInvLineClearance"
    Theme="Cuadriculas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Encabezado" runat="server">
    <script type="text/javascript">

        function limpiar(obj) {
            var objControl = document.getElementById(obj.id);
            objControl.value = "";
            objControl.focus();
            return;
        }


        function validaInfo(args, val, obj) {
            if (args === "") {
                return;
            }

            var objSend = document.getElementById('<%=btnSave.ClientID %>');

            var parametrosEnviar = "{ 'valor': '" + args + "', 'tipo':'" + val + "', 'orden':' " + document.getElementById('<%=txtWorkOrderTo.ClientID %>').value + "'  }";
            if (val == 2)
                parametrosEnviar = "{ 'valor': '" + args + "', 'tipo':'" + val + "', 'orden':'" + document.getElementById('<%=txtWorkOrderFrom.ClientID %>').value + "' }";

            // objSend.disabled = true;
            $.ajax({
                type: "POST",
                url: "whInvLineClearance.aspx/validaInfo",
                data: parametrosEnviar,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    if (msg.d != "") {
                        if (msg.d.indexOf("BAAN") > -1) {
                            var objControl = document.getElementById(obj.id);
                            objControl.value = "";
                            objControl.focus();
                            alert(msg.d);
                            return false;
                        }
                    }
                },
                error: function (msg) {
                    var objControl = document.getElementById(obj.id);
                    objControl.value = "";
                    objControl.focus();
                    alert("System Execution Error on Query : " + msg.d);
                    return false;
                }
            });
        }

        $.fn.exists = function () {
            return this.length !== 0;
        }
    
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True"
        EnableScriptLocalization="True" EnablePageMethods="true">
    </asp:ScriptManager>
    <div>
        <span><b>
            <asp:Label ID="lblDescWorkOrder" runat="server" Font-Size="Small" />
        </b></span>
    </div>

    <div class="rTable" align="center">
        <div class="rTableRow">
            <div class="rTableCellHead" style="vertical-align: middle">
                <span class="style2" style="vertical-align: middle"><b style="font-size: 12px">
                    <asp:Label ID="lblDescFrom" runat="server" />
                </b></span>
            </div>
            <div class="rTableCellLeft" align="left">
                <span style="vertical-align: middle">
                    <asp:TextBox ID="txtWorkOrderFrom" runat="server" MaxLength="9" Width="100%" CssClass="TextBox"
                        TabIndex="1" ToolTip="Enter work order number"></asp:TextBox>
                </span>
            </div>
            <div class="rTableCellError">
                <span style="vertical-align: middle">
                    <asp:RegularExpressionValidator ID="minlenght" runat="server" ControlToValidate="txtWorkOrderFrom"
                        ErrorMessage="Remember 9 characters" ValidationExpression="^[a-zA-Z0-9'@&#.\s]{9,9}$"
                        CssClass="errorMsg" SetFocusOnError="True" Display="Dynamic" />
                    <asp:RequiredFieldValidator ID="RequiredField" runat="server" ErrorMessage="Work Order [ From ] is required"
                        ControlToValidate="txtWorkOrderFrom" CssClass="errorMsg" InitialValue="" Enabled="False"></asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="OrderError" runat="server" ErrorMessage="Order doesn't exist or the status is active, release or completed."
                        SetFocusOnError="True" CssClass="errorMsg"></asp:CustomValidator>
                </span>
            </div>
        </div>
        <div class="rTableRow">
            <div class="rTableCellHead" style="vertical-align: middle">
                <span class="style2" style="vertical-align: middle"><b style="font-size: 12px">
                    <asp:Label ID="lblDescTo" runat="server" />
                </b></span>
            </div>
            <div class="rTableCellLeft" align="left">
                <span style="vertical-align: middle;">
                    <asp:TextBox ID="txtWorkOrderTo" runat="server" MaxLength="9" Width="100%" CssClass="TextBox"
                        TabIndex="2" ToolTip="Enter work order number" CausesValidation="True"></asp:TextBox>
                </span>
            </div>
            <div class="rTableCellError">
                <span style="vertical-align: middle">
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtWorkOrderTo"
                        ErrorMessage="Remember 9 characters" ValidationExpression="^[a-zA-Z0-9'@&#.\s]{9,9}$"
                        CssClass="errorMsg" Display="Dynamic" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Work Order [To] is required"
                        ControlToValidate="txtWorkOrderTo" CssClass="errorMsg" InitialValue="" Enabled="False"></asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Order doesn't exist or the status is active, release or completed."
                        CssClass="errorMsg"></asp:CustomValidator>
                </span>
            </div>
        </div>
        <div class="rTableRow">
            <div class="rTableCellHead" style="vertical-align: middle">
                <span class="style2" style="vertical-align: middle"><b style="font-size: 12px">
                    <asp:Label ID="Label1" runat="server" />
                </b></span>
            </div>
            <div class="rTableCellLeft" align="left">
                <asp:Button ID="btnSend" runat="server" Text="Query" CssClass="ButtonsSendSave" OnClick="btnSend_Click"
                    />
            </div>
        </div>
    </div>
    <div style="height: 15px" align="center">
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
            </div>
        </span>
    </div>
    <div id="HeaderGrid" style="width: 80%" runat="server">
        <span style="text-align: right">
            <asp:Button ID="btnSave" runat="server" Text="Save Items" Visible="False" CssClass="ButtonsSendSave"
                Width="90px" Height="20px" OnClick="btnSave_Click" />
        </span>
    </div>
    <div style="height: 10px; margin-bottom: 200px" align="center">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:GridView ID="grdRecords" runat="server" AutoGenerateColumns="False" DataKeyNames="ID"
                    OnRowDataBound="grdRecords_RowDataBound" SkinID="Default" Width="75%">
                    <Columns>
                        <asp:BoundField HeaderText="Item" DataField="ARTICULO">
                            <ItemStyle HorizontalAlign="Center" Width="15%" />
                            <HeaderStyle CssClass="HeaderGrid" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Description" DataField="DESCRIPCION">
                            <ItemStyle HorizontalAlign="Left" Width="40%" />
                            <HeaderStyle CssClass="HeaderGrid" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <%--<asp:BoundField HeaderText="Actual Qty" DataField="ACT_CANT" DataFormatString="{0}">
                            <ItemStyle HorizontalAlign="Center" Width="10%" />
                            <HeaderStyle CssClass="HeaderGrid" HorizontalAlign="Center" />
                        </asp:BoundField>--%>
                        <asp:TemplateField HeaderText="Actual Qty" HeaderStyle-CssClass="HeaderGrid"
                            ControlStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:TextBox ID="ActualQty" runat="server" Width="10%" MaxLength="15" CausesValidation="True"
                                    CssClass="TextBox" value="10" Enabled="false"/>
                            </ItemTemplate>
                            <ControlStyle Width="70px"></ControlStyle>
                            <HeaderStyle CssClass="HeaderGrid" HorizontalAlign="Center" />
                            <ItemStyle Width="10px"></ItemStyle>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Unit" DataField="UNIDAD">
                            <ItemStyle HorizontalAlign="Center" Width="10%" />
                            <HeaderStyle CssClass="HeaderGrid" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Quantity to Line Clearance WH" HeaderStyle-CssClass="HeaderGrid"
                            ControlStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:TextBox ID="toReturn" runat="server" Width="10%" MaxLength="15" CausesValidation="True"
                                    CssClass="TextBox" />
                                <asp:RegularExpressionValidator ID="validateReturn" runat="server" ControlToValidate="toReturn"
                                    ErrorMessage="Only numbers allowed" SetFocusOnError="true" ValidationExpression="^[0-9]{1,11}(?:\.[0-9]{1,4})?$"
                                    Display="Dynamic" ForeColor="Red" Font-Names="Arial" Font-Size="9" Font-Italic="True" />
                                <asp:CompareValidator ID="validateQuantity" runat="server" Operator="LessThanEqual"
                                    controltovalidate="toReturn" controltocompare="ActualQty"
                                    Display="Dynamic" Type="Double" SetFocusOnError="true" ErrorMessage="Quantity to return cannot be greater than Actual Quantity"
                                    ForeColor="Red" Font-Names="Arial" Font-Size="9" Font-Italic="True" Enabled="true"></asp:CompareValidator>
                            </ItemTemplate>
                            <ControlStyle Width="70px"></ControlStyle>
                            <HeaderStyle CssClass="HeaderGrid" HorizontalAlign="Center" />
                            <ItemStyle Width="10px"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="To Lot" HeaderStyle-CssClass="HeaderGrid" ControlStyle-Width="15%"
                            HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:DropDownList ID="toLot" runat="server" CausesValidation="true" Width="15%" MaxLength="15"
                                    CssClass="TextBox" />
                            </ItemTemplate>
                            <ControlStyle Width="90px"></ControlStyle>
                            <HeaderStyle CssClass="HeaderGrid" HorizontalAlign="Center" />
                            <ItemStyle Width="10px"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField Visible="False">
                            <ItemTemplate>
                                <asp:HiddenField ID="LOTE" runat="server" Value='<%# Eval("IND_LOTE") %>' Visible="False" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="mlot" HeaderText="Lote" ReadOnly="True" ShowHeader="False"
                            Visible="False" />
                    </Columns>
                    <HeaderStyle HorizontalAlign="Center" />
                    <RowStyle BackColor="#F7F8FB" Font-Names="Arial" Font-Size="9pt" ForeColor="#1B1B1B"
                        VerticalAlign="Middle" />
                </asp:GridView>
                <asp:HiddenField ID="hidden" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
