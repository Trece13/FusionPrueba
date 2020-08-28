<%@ Page Title="" Language="C#" MasterPageFile="~/MDMasterPage.Master" AutoEventWireup="true" CodeBehind="whLoadXMLtoTable.aspx.cs" Inherits="whusap.WebPages.Reports.whLoadXMLtoTable" Theme="Cuadriculas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Encabezado" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <script src="../../Scripts/jquery.dynDateTime.min.js" type="text/javascript"></script>
    <script src="../../Scripts/calendar-en.min.js" type="text/javascript"></script>
    
    <script type="text/javascript">
        $(document).ready(function () {
            $("#<%=txtStartDate.ClientID %>").dynDateTime({
                showsTime: true,
                ifFormat: "%m/%d/%Y %H:%M",
                daFormat: "%l;%M %p, %e %m,  %Y",
                align: "BR",
                electric: false,
                singleClick: false,
                displayArea: ".siblings('.dtcDisplayArea')",
                button: ".next()"
            });
        });

        $(document).ready(function () {
            $("#<%=txtEndDate.ClientID %>").dynDateTime({
                showsTime: true,
                ifFormat: "%m/%d/%Y %H:%M",
                daFormat: "%l;%M %p, %e %m,  %Y",
                align: "BR",
                electric: false,
                singleClick: false,
                displayArea: ".siblings('.dtcDisplayArea')",
                button: ".next()"
            });
        });
        
        function CheckAllEmp(Checkbox) {
            var GridVwHeaderChckbox = document.getElementById("<%=gvMachines.ClientID %>");
            for (i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
                GridVwHeaderChckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
            }
        };
    </script>
    <div>
        <p>
            <asp:Label ID="lblMessage" runat="server" CssClass="lblMessage"></asp:Label>
        </p>
    </div>
    <hr />
    <div>
        <table>
            <%--<tr>
                <td>
                    <asp:Label ID="lblMachines" runat="server" Text="Máquina" />
                </td>
                <td colspan="3">
                    <asp:DropDownList ID="ddlMachines" runat="server" Width="400px" ></asp:DropDownList>
                </td>
            </tr>--%>
            <tr>
                <td><asp:Label ID="lblStartDate" runat="server" Text="Start date"></asp:Label></td>
                <td>
                    <asp:TextBox ID="txtStartDate" runat="server" ReadOnly = "true"></asp:TextBox>
                    <img src="../../images/calender.png" alt="iconCalendar"/>
                </td>
                <td><asp:Label ID="lblEndDate" runat="server" Text="End date"></asp:Label></td>
                <td>
                    <asp:TextBox ID="txtEndDate" runat="server" ReadOnly = "true"></asp:TextBox>
                    <img src="../../images/calender.png" alt="iconCalendar"/>    
                </td>
            </tr>
            <tr>
                <td colspan="4" align="center">
                    <asp:GridView id="gvMachines" runat="server" SkinID="Default"
                            onpageindexchanging="gvMachines_PageIndexChanged">
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:CheckBox ID="chkRowAll" runat="server" onclick="CheckAllEmp(this);" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkRow" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Machine"   HeaderText="Machine" ItemStyle-Width="150" />
                            <asp:BoundField DataField="MachineId" HeaderText="Description" ItemStyle-Width="150" />
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
              <td colspan="4" align="center">
                <asp:Button ID="btnGenerate" 
                    runat="server" 
                    CssClass="ButtonsSendSave" 
                    Text="Load info."  
                    onclick="btnGenerate_Click" />  
              </td> 
            </tr>
        </table>     
    </div>
</asp:Content>
