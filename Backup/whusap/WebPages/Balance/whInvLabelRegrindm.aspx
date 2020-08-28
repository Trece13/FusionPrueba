<%@ Page Title="" Language="C#" MasterPageFile="~/MDMasterPage.Master" AutoEventWireup="true" CodeBehind="whInvLabelRegrindm.aspx.cs" Inherits="whusap.WebPages.Balance.whInvLabelRegrindm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Encabezado" runat="server">
    <style type="text/css">

        .style7
        {
            font-family: Arial;
            font-size: xx-small;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True" EnableScriptLocalization="True" EnablePageMethods="true">
    </asp:ScriptManager>
    <br />
    <div>
      <span>
       <div id="printResult" style="width:80%" runat="server"  align="center">
          <span style="text-align:right">
            <asp:Label ID="lblResult" runat="server" CssClass="style2" Font-Bold="True" Font-Italic="True"></asp:Label>
            <br />
            <span style="vertical-align:middle" />
                <asp:Label ID="lblError" runat="server" Text="" CssClass="lblMessage" Visible="false"></asp:Label>
            </span>
          </span>
       </div>
       <br />
       <br />
    </div>
    <div id="printerDiv" runat="server" style="color:Red;" >


    <span style="vertical-align:middle" />
        <table style="width: 60%">

            <tr>
             <td class="style8">
           <span class="style2" style="vertical-align: middle;"><b style="font-size:11px">Machine </b></span>
             </td>
             <td style="width: 120px">
      <span>


    <span style="vertical-align:middle" />
                    <asp:TextBox ID="txtMachine" runat="server" AutoPostBack="True" 
                        ontextchanged="txtMachine_listMachine"></asp:TextBox>
                </td>
            <td class="rTableCellError" style="width:50%" height="350px">
                <asp:Button ID="btnChangeMac" runat="server" Text="Change Machine" 
                 CssClass="ButtonsSendSave" Width="107px" Height="20px" Enabled="False" 
                    onclick="btnChangeMac_Click" />
                    <asp:Label ID="lblIngreso" runat="server" Text="" Visible="false"></asp:Label>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    &nbsp;</td>
                <td></td>
            </tr>
            <tr>
                <td class="style8">
           <span class="style2" style="vertical-align: middle;"><b style="font-size:11px">Regrind </b></span>
                </td>
                <td style="width: 120px">
           <span style="vertical-align:middle;" >

               <asp:DropDownList ID="listRegrind" runat="server" Width="215px" Height="20px"
                        CssClass="DropDownList" TabIndex="4" 
                           ToolTip="Select one item from list" 
                        onselectedindexchanged="listRegrind_SelectedIndexChanged" AutoPostBack="true" >
               </asp:DropDownList>

           </span>
                </td>
                <td class="rTableCellError" style="width:50%" height="350px">
                    &nbsp;</td>
            </tr>
            <tr>
                <td >
                   <span style="vertical-align:middle" />
                   <span class="style2" style="vertical-align: middle;"><b style="font-size:11px">To Issue </b></span>
                </td>
                <td style="width: 120px">
                   
                   <div id="tooltip"><span class="style7"><em>*Maximum 4 digits on decimal positions</em></span></div>
                   <span style="vertical-align:middle;" >
                      <asp:TextBox ID="txtQuantity" runat="server" MaxLength="15" 
                                   Width="160px" CssClass="TextBox" TabIndex="5" Height="45px" 
                        ToolTip="Current Weight" Font-Names="Consolas" Font-Size="14pt" ></asp:TextBox>
                      </span></td>
                <td class="rTableCellError" height="90%" style="width:350px; text-align: left;" 
                    align="left">
                    <span style="vertical-align:middle; text-align: left;" />
                      <asp:RegularExpressionValidator ID="validateReturn" runat="server" 
                                                      ControlToValidate="txtQuantity" 
                                                      ErrorMessage="Only numbers allowed and Quantity must be greater than zero" 
                                                      SetFocusOnError="true" 
                                                      ValidationExpression="[0-9]+(\.[0-9]{1,4})?" Display="Dynamic" 
                                                      ForeColor="Red" Font-Names="Arial" 
                        Font-Size="9" Font-Italic="True" CssClass="errorMsg" Font-Bold="false"></asp:RegularExpressionValidator>
                </td>
            </tr>
        </table>
    

    </div>
    <div style="padding: 1%; height:35px; vertical-align:middle; width:50%; text-align: center;" 
        align="center">
        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="ButtonsSendSave" 
             Width="107px" Height="24px" onclick="btnSave_Click" TabIndex="6" /> 

    <span style="vertical-align:middle" />
    </div>
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <asp:HiddenField ID="hOrdenMachine" runat="server" />    
    <asp:HiddenField ID="hidden" runat="server" />
    <asp:HiddenField ID="hi_indBodega" runat="server" />
    <asp:HiddenField ID="hi_indLote" runat="server" />
    <asp:HiddenField ID="hi_descItem" runat="server" />
    <asp:HiddenField ID="hi_unityItem" runat="server" />
    <asp:HiddenField ID="hi_machineItem" runat="server" />
</asp:Content>
