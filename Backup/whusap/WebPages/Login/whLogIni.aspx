<%@ Page Title="" Language="C#" MasterPageFile="~/WebPages/Login/MDLogin.Master" AutoEventWireup="true" CodeBehind="whLogIni.aspx.cs" Inherits="whusap.WebPages.Login.whLogIni" Theme="Cuadriculas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Encabezado" runat="server">
    <link href="../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style7
        {
            width: 266px;
        }
        .style8
        {
            width: 306px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div style="height:60px; vertical-align:middle; position: relative; top: 0px; left: 0px;">

   </div>

   <div style="border-style:solid; border-width: 0px; background-color:#BCC7D8; height: 1px;" />
    <div>
     <table style="width: 90%;">
         <tr>
             <td class="style7">
                 &nbsp;
             </td>
             <td class="style8">
                 &nbsp;
             </td>
             <td>
                 &nbsp;</td>
         </tr>
         <tr>
             <td class="style7">
                 &nbsp;
             </td>
             <td class="style8">
               <p style="font-family: tahoma; font-size: 14px">
                   Please enter your username and password.
               </p>
                 <img alt=""  src="../../images/LogoGrupoPhoenix2.png" 
                     style="height: 117px; width: 290px" />
               <p style="font-family: tahoma; font-size: 14px">
               <asp:Label id="lblErrorMsg" runat="server" Visible="true"/>
               </p>
             </td>
             <td>
<asp:Login ID="LoginUser" runat="server" EnableViewState="false" RenderOuterTable="false" 
                     onauthenticate="LoginUser_Authenticate" 
                     DestinationPageUrl="~/WebPages/Login/whMenuI.aspx">
        <LayoutTemplate>
            <span class="failureNotification">
                <asp:Literal ID="FailureText" runat="server"></asp:Literal>
            </span>
            <asp:ValidationSummary ID="LoginUserValidationSummary" runat="server" CssClass="failureNotification" 
                 ValidationGroup="LoginUserValidationGroup"/>
            <div class="accountInfo">
                <fieldset class="login">
                    <legend>Account Information</legend>
                    <p>
                        <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">Username:</asp:Label>
                        <asp:TextBox ID="UserName" runat="server" CssClass="TextBox" Width="150px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" 
                             CssClass="failureNotification" ErrorMessage="User Name is required." ToolTip="User Name is required." 
                             ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                    </p>
                    <p>
                        <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password:</asp:Label>
                        <asp:TextBox ID="Password" runat="server" CssClass="TextBox" TextMode="Password" Width="150px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" 
                             CssClass="failureNotification" ErrorMessage="Password is required." ToolTip="Password is required." 
                             ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                    </p>
                </fieldset>
                <p class="submitButton">
                    <asp:Button ID="LoginButton" CssClass="ButtonsSendSave" Width="60px" 
                        Height="20px" runat="server" CommandName="Login" Text="Log In" 
                        ValidationGroup="LoginUserValidationGroup" oncommand="LoginButton_Command"/>
                </p>
            </div>
        </LayoutTemplate>
    </asp:Login>
              
             </td>
         </tr>
         <tr>
             <td class="style7">
                 &nbsp;
             </td>
             <td class="style8">
                 &nbsp;
             </td>
             <td>
                 &nbsp;
             </td>
         </tr>
     </table>
   </div>
</asp:Content>

