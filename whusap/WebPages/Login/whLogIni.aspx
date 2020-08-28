<%@ Page Title="" Language="C#" MasterPageFile="~/WebPages/Login/MDLogin.Master" AutoEventWireup="true" CodeBehind="whLogIni.aspx.cs" Inherits="whusap.WebPages.Login.whLogIni" Theme="Cuadriculas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Encabezado" runat="server">
    
    <link href="<%= String.Concat(ConfigurationManager.AppSettings["UrlBase"], "/Styles/Site.css") %> " rel="stylesheet" type="text/css" />
    <style type="text/css">
        #Cuerpo {
            background-color: white !important;
        }
        .style7
        {
            width: 266px;
        }
        .style8
        {
            width: 306px;
        }
        #TblForgotPass {
            display: none;
        }
    </style>
    <script type="text/javascript">
        
        var _idioma = '<%= _idioma %>';
        $(document).ready(function () {

            $('#Password').val('');
            $('#UserName').val('');

            $('#ForgotLink').click(function () {
                ForgotLinkClick();
            });

            $('#LoginLink').click(function () {
                LoginLinkClick();
            });

        });


        function validarPassword() {
            var regex = /(?=.{8,15})(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@#$%^&+=.*]).*$/;
            var re = new RegExp(regex);
            var str = document.getElementById("txtNewPassword").value;
            if (!str.match(re)) {
                alert(_idioma == "ESPAÑOL" ? 'Contraseña invalida - Especificaciones de contraseña:\n Minimo 8 caracteres\n Maximo 15\n Al menos una letra mayúscula\n Al menos una letra minuscula\n Al menos un dígito\n No espacios en blanco\n Al menos 1 caracter especial'
                : 'Invalid password - Password specifications: \n Minimum 8 characters \n Maximum 15 \n At least one uppercase letter \n At least one lowercase letter \n At least one digit \n No white space \n At least 1 special character');
                document.getElementById("txtNewPassword").value = "";
                document.getElementById("txtConfirmPassword").value = "";
                document.getElementById("txtNewPassword").focus();
                return false;
            }
            else {
                return true; 
            }
        };

        function ForgotLinkClick() {
            $("#TblLogin").hide("slow");
            $("#TblForgotPass").show("slow");
        }

        function LoginLinkClick() {
            $("#TblForgotPass").hide("slow");
            $("#TblLogin").show("slow");
        }
        $('#Contenido_LoginButton').click(function () { sessionStorage.setItem("namePage", ""); });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <!--<div style="height:60px; vertical-align:middle; position: relative; top: 0px; left: 0px;">

   </div>-->

   <div style="border-style:solid; border-width: 0px; background-color:#ffffff; height: 1px; margin-right:auto;" />
    <div runat="server" id="divLogin" style="margin-right:auto;">
        <table id="TblLogin" width="20%" style = "margin: auto; margin-top:15em">
        <!--<tr><td><legend>
                        <asp:Label Text="Account Information" runat="server" ID="lblEncLogin" AutoPostBack="true" style="font-style:italic;" /> 
                        <asp:DropDownList runat="server" ID="ddlIdioma" AutoPostBack="true" OnSelectedIndexChanged="ddlIdioma_OnSelectedIndexChanged">
                            <asp:ListItem Text='Ingles' Value="INGLES" />
                            <asp:ListItem Text="Español" Value="ESPAÑOL"/>
                         </asp:DropDownList>
                    </legend></td></tr>-->
            <tr>
                <!--<td><p style="font-family: tahoma; font-size: 14px">
                   <asp:Label runat="server" ID="lblInformation" AutoPostBack="true" style="font-style:italic;">Please enter your username and password.</asp:Label>
               </p>
                 <img alt=""  src="<%= String.Concat(ConfigurationManager.AppSettings["UrlBase"], "/images/LogoGrupoPhoenix2.png") %>"
                     style="height: 100px; width: 200px" />
               <p style="font-family: tahoma; font-size: 14px">
               <asp:Label id="lblErrorMsg" runat="server" Visible="true" style="color:Red;"/>
               </p>
             </td>-->
             
            </tr>
            <tr> 
                <td colspan="2" height="80px">
                    
                    <p align="center"><img alt=""  
                        src="<%= String.Concat(ConfigurationManager.AppSettings["UrlBase"], "/images/Grupo-Phoenix-1.png") %>" 
                        align="middle" width="100px"/></p>
                </td>
            </tr>
            <tr> 
                <td colspan="2"><h1 align="center" style="font-family: 'Segoe UI'">LOGIN</h1>
                    <br />
                </td>
                
            </tr>
            <tr> 
                <td><asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName"  
                        AutoPostBack="true" Font-Size="X-Large">Username:</asp:Label></td>
                <td><asp:TextBox ID="UserName" runat="server" CssClass="TextBox form-control form-control-sm" Width="100%" autocomplete="off" ClientIDMode="Static"></asp:TextBox></td>
                <td><asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" 
                             CssClass="failureNotification" ErrorMessage="User Name is required." ToolTip="User Name is required." 
                             ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td></td>   
                <td></td>
            </tr>
            <tr> 
                <td><asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password" 
                        AutoPostBack="true" Font-Size="X-Large">Password:</asp:Label></td>
                <td><asp:TextBox ID="Password" runat="server" CssClass="TextBox form-control form-control-sm" TextMode="Password" Width="100%" autocomplete="off" ClientIDMode="Static"></asp:TextBox>
                        </td>
                <td><asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" 
                             CssClass="failureNotification" ErrorMessage="Password is required." ToolTip="Password is required." 
                             ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator><br/>
                </td>
            <tr> 
                <td></td><td><a  id="ForgotLink" href="#" style="color: #C0C0C0; display:none">¿forgot password?</a></td>
            </tr>
            <tr> 
                <td colspan="2">
                <br />
                <br />
                <p align="center">
                    <asp:Button ID="LoginButton" CssClass="ButtonsSendSave"
                        Height="30px" runat="server" CommandName="Login" Text="Login" 
                        ValidationGroup="LoginUserValidationGroup" oncommand="LoginButton_Command" 
                        BackColor="#2F80ED" BorderColor="#2F80ED" Font-Bold="False" ForeColor="White" 
                        Width="60%" Font-Size="Medium"/></p>
                        <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>  
                    <p align="center">
                    <asp:Label ID="LblErrorLogin" runat="server" ForeColor="Red"></asp:Label></p>
                    </td>
            </tr>
            <tr>
                <td  colspan="2">
                    <br />
                    <br />
                    <br />
                    <br />
                    <span class="failureNotification">
                    <asp:Literal ID="FailureText" runat="server"></asp:Literal>
                    </span>
                    <asp:ValidationSummary ID="LoginUserValidationSummary" runat="server" CssClass="failureNotification" 
                    ValidationGroup="LoginUserValidationGroup"/>
                 </td>
            </tr>
        </table>

        <table id="TblForgotPass" width="20%" style = "margin: auto; margin-top:15em">

             <tr> 
                <td colspan="2" height="80px">
                    <p align="center"><img alt=""  
                        src="<%= String.Concat(ConfigurationManager.AppSettings["UrlBase"], "/images/Grupo-Phoenix-1.png") %>" 
                        align="middle" width="100px"/></p>
                </td>
            </tr>

            <tr> 
                <td colspan="2"><h2 align="center" style="font-family: 'Segoe UI'">	¿forgot password?</h2>
                    <br />
                </td>
                
            </tr>
            <tr> 
                <td><asp:Label ID="Label1" runat="server" AssociatedControlID="UserName"  
                        AutoPostBack="true" Font-Size="X-Large">Username </asp:Label></td>
                <td><asp:TextBox ID="TxtUserForgotPass" runat="server" CssClass="TextBox" Width="100%" autocomplete="on" ClientIDMode="Static"></asp:TextBox></td>
                <td><asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="UserName" 
                             CssClass="failureNotification" ErrorMessage="User Name is required." ToolTip="User Name is required." 
                             ValidationGroup="ForgotPasswordValidationGroup">*</asp:RequiredFieldValidator></td>
            </tr>
            
            <tr> 
                <td colspan="2">
                <br />
                <br />
                <p align="center">
   
                    <asp:Button ID="BtnForgotPass" runat="server"
                            onclick="btnForgotPass_Click" CssClass="ButtonsSendSave" 
                                Height="30px" runat="server" CommandName="Forgot Pass" Text="Send Mail"
                                BackColor="#2F80ED" BorderColor="#2F80ED" Font-Bold="false" ForeColor="White" 
                                Width="60%" Font-Size="Medium" />
                

            </tr>
            <tr>
                <td></td><td><a  id="LoginLink" href="#" style="color: #C0C0C0">Go to Login</a></td>
            </tr> 
            <tr>
                <td  colspan="2">
                    <br />
                    <br />
                    <br />
                    <br />
                    <span class="failureNotification">
                    <asp:Literal ID="Literal2" runat="server"></asp:Literal>
                    </span>
                </td>
            </tr>
        </table>
        
        

   </div>

   <div runat="server" id="divChangePassword" visible="false">
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
                   <asp:Label runat="server" ID="lblInformation1" AutoPostBack="true">Please enter your username and password.</asp:Label>
               </p>
                 <img alt=""  src="<%= String.Concat(ConfigurationManager.AppSettings["UrlBase"], "/images/LogoGrupoPhoenix2.png") %>"
                     style="height: 100px; width: 200px" />
               <p style="font-family: tahoma; font-size: 14px">
               <asp:Label id="lblErrorMessage1" runat="server" Visible="true" style="color:Red;"/>
               </p>
             </td>
             <td>

            <span class="failureNotification">
                <asp:Literal ID="Literal1" runat="server"></asp:Literal>
            </span>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="failureNotification" 
                 ValidationGroup="LoginUserValidationGroup"/>
            <div class="accountInfo">
                <fieldset class="login">
                    <legend>
                        <asp:Label Text="Change password" runat="server" ID="lblEncLogin1" AutoPostBack="true" /> 
                        <asp:DropDownList runat="server" ID="ddlIdioma1" AutoPostBack="true" OnSelectedIndexChanged="ddlIdioma_OnSelectedIndexChanged">
                            <asp:ListItem Text="English" Value="INGLES" />
                            <asp:ListItem Text="Español" Value="ESPAÑOL"/>
                         </asp:DropDownList></legend>
                    <p>
                        <asp:Label ID="lblCurrentPassword" runat="server" AssociatedControlID="txtCurrentPassword"  AutoPostBack="true">Username:</asp:Label>
                        <asp:TextBox ID="txtCurrentPassword" TextMode="Password" runat="server" CssClass="TextBox" Width="150px" autocomplete="off" ClientIDMode="Static"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCurrentPassword" 
                             CssClass="failureNotification" ErrorMessage="Current password is required." ToolTip="Current password is required." 
                             ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                    </p>
                    <p>
                        <asp:Label ID="lblNewPassword" runat="server" AssociatedControlID="txtNewPassword"  AutoPostBack="true">Password:</asp:Label>
                        <asp:TextBox ID="txtNewPassword" runat="server" CssClass="TextBox" TextMode="Password" onchange="validarPassword();" Width="150px"  autocomplete="off" ClientIDMode="Static"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtNewPassword" 
                             CssClass="failureNotification" ErrorMessage="New password is required." ToolTip="New password is required." 
                             ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                    </p>
                    <p>
                        <asp:Label ID="lblConfirmPassword" runat="server" AssociatedControlID="txtConfirmPassword"  AutoPostBack="true">Password:</asp:Label>
                        <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="TextBox" TextMode="Password" Width="150px"  autocomplete="off" ClientIDMode="Static"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtConfirmPassword" 
                             CssClass="failureNotification" ErrorMessage="Confirm your password." ToolTip="Confirm your password." 
                             ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                    </p>
                </fieldset>
                <p class="submitButton">
                    <asp:Button ID="btnChangePassword" CssClass="ButtonsSendSave" Width="60px" 
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

