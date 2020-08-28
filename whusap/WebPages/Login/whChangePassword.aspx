<%@ Page Language="C#" MasterPageFile="~/WebPages/Login/MDLogin.Master" AutoEventWireup="true" CodeBehind="whChangePassword.aspx.cs" Inherits="whusap.WebPages.Login.whChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Encabezado" runat="server">
    <link href="<%= String.Concat(ConfigurationManager.AppSettings["UrlBase"], "/Styles/Site.css") %> " rel="stylesheet" type="text/css" />
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
    <script type="text/javascript">
        $(document).ready(function () {
            $('#Password').val('');
            $('#UserName').val('');
        });

    </script>
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
             <td class="style8" style="text-align:center;">
               <p style="font-family: tahoma; font-size: 14px">
                   <asp:Label runat="server" ID="lblInformation" AutoPostBack="true">Please enter your username and password.</asp:Label>
               </p>
                 <img alt=""  src="<%= String.Concat(ConfigurationManager.AppSettings["UrlBase"], "/images/LogoGrupoPhoenix2.png") %>"
                     style="height: 100px; width: 200px" />
               <p style="font-family: tahoma; font-size: 14px">
               <asp:Label id="lblErrorMsg" runat="server" Visible="true" style="color:Red;"/>
               </p>
             </td>
             <td>

            <span class="failureNotification">
                <asp:Literal ID="FailureText" runat="server"></asp:Literal>
            </span>
            <asp:ValidationSummary ID="LoginUserValidationSummary" runat="server" CssClass="failureNotification" 
                 ValidationGroup="LoginUserValidationGroup"/>
            <div class="accountInfo">
                <fieldset class="login">
                    <legend>
                        <asp:Label Text="Change password" runat="server" ID="lblEncLogin" AutoPostBack="true" /> 
                        <asp:DropDownList runat="server" ID="ddlIdioma" AutoPostBack="true" OnSelectedIndexChanged="ddlIdioma_OnSelectedIndexChanged">
                            <asp:ListItem Text="English" Value="INGLES" />
                            <asp:ListItem Text="Español" Value="ESPAÑOL"/>
                         </asp:DropDownList></legend>
                    <p>
                        <asp:Label ID="lblCurrentPassword" runat="server" AssociatedControlID="txtCurrentPassword"  AutoPostBack="true">Username:</asp:Label>
                        <asp:TextBox ID="txtCurrentPassword" runat="server" CssClass="TextBox" Width="150px" autocomplete="off" ClientIDMode="Static"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="txtCurrentPassword" 
                             CssClass="failureNotification" ErrorMessage="Current password is required." ToolTip="Current password is required." 
                             ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                    </p>
                    <p>
                        <asp:Label ID="lblNewPassword" runat="server" AssociatedControlID="txtNewPassword"  AutoPostBack="true">Password:</asp:Label>
                        <asp:TextBox ID="txtNewPassword" runat="server" CssClass="TextBox" TextMode="Password" Width="150px"  autocomplete="off" ClientIDMode="Static"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="txtNewPassword" 
                             CssClass="failureNotification" ErrorMessage="New password is required." ToolTip="New password is required." 
                             ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                    </p>
                    <p>
                        <asp:Label ID="lblConfirmPassword" runat="server" AssociatedControlID="txtConfirmPassword"  AutoPostBack="true">Password:</asp:Label>
                        <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="TextBox" TextMode="Password" Width="150px"  autocomplete="off" ClientIDMode="Static"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtConfirmPassword" 
                             CssClass="failureNotification" ErrorMessage="Confirm your password." ToolTip="Confirm your password." 
                             ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                    </p>
                </fieldset>
                <p class="submitButton">
                    
                    <asp:Button ID="LoginButton" CssClass="ButtonsSendSave" Width="60px" 
                        Height="20px" runat="server" Text="Log In" 
                        ValidationGroup="LoginUserValidationGroup" OnClick="btnUpdate_Click" />
                </p>
            </div>
              
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


