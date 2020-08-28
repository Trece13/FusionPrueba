<%@ Page Title="" Language="C#" MasterPageFile="../../MDMasterPage.Master" AutoEventWireup="true"
    EnableViewStateMac="false" CodeBehind="whInvMaterialRejectedD.aspx.cs" Inherits="whusap.WebPages.InvMaterial.whInvMaterialRejectedD"
    Theme="Cuadriculas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Encabezado" runat="server">
    <script src="https://code.jquery.com/jquery-3.3.1.js"></script>
    <script type="text/javascript" src="https://cdn.datatables.net/1.10.20/js/jquery.dataTables.min.js"></script>
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

        txtItemError = '<%=lblitemError%>';
        txtLotError = '<%=lblLotError%>';

        LsItems = JSON.parse('<%= ListaItemsJSON%>');
        window.onload = function () {
            var e = document.getElementById("printerDiv");
        };

        function limpiar(obj) {
            var objControl = document.getElementById(obj.id);
            // objControl.value = "";
            objControl.focus();
            return;
        }

        //        function validaLot(args, val, obj) {
        //            if (val == "") {
        //                return;
        //            }
        //            var parametrosEnviar = "{ 'Fila': '" + JSON.stringify(args) + "', 'valor':'" + val + "'}";
        //            var objSend = document.getElementById('<%=btnSave.ClientID %>');
        //            objSend.disabled = true;
        //            
        //            $.ajax({
        //                type: "POST",
        //                url: "whInvMaterialRejectedD.aspx/validaExistLot",
        //                data: parametrosEnviar,
        //                contentType: "application/json; charset=utf-8",
        //                dataType: "json",
        //                success: function (msg) {
        //                    if (msg.d != "") {
        //                        var objControl = document.getElementById(obj.id);
        //                        objControl.value = "";
        //                        objControl.focus();
        //                        alert(msg.d);
        //                        return false;
        //                    } else { objSend.disabled = false; }
        //                },
        //                error: function (msg) {
        //                    var objControl = document.getElementById(obj.id);
        //                    objControl.value = "";
        //                    objControl.focus();
        //                    alert(msg.d);
        //                    return false;
        //                }
        //            });
        //        }

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
        EnableScriptLocalization="True" EnableCdn="true" EnablePageMethods="true" EnablePartialRendering="true">
    </asp:ScriptManager>
    <table>
        <tr>
            <td style="text-align: left;">
                <span class="style2"><b style="font-size: 12px;">
                    <asp:Label ID="lblDescItem" runat="server" />
                </b></span>
            </td>
            <td style="width: 250px; padding: 5px;">
                <span><b style="font-size: 12px;">
                    <asp:TextBox ID="txtItem" runat="server" disabled="disabled" CausesValidation="True"
                        MaxLength="47" Width="200px" CssClass="TextBoxBig InputForm" TabIndex="1" ToolTip="Enter item code"></asp:TextBox>
                </b></span>
            </td>
        </tr>
        <tr>
            <td style="text-align: left;">
                <span><b style="font-size: 11px;">
                    <asp:Label ID="lblDescLot" runat="server" />
                </b></span>
            </td>
            <td style="width: 250px; padding: 5px;">
                <asp:TextBox ID="txtLot" runat="server" disabled="disabled" CausesValidation="True"
                    MaxLength="20" Width="200px" CssClass="TextBoxBig" TabIndex="2" ToolTip="Enter lot code"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="text-align: left;">
            </td>
            <td style="width: 250px; padding: 5px;">
                <label id="lblItemError" class="LabelsError">
                </label>
                </br>
                <label id="lblLotError" class="LabelsError">
                </label>
                </br>
            </td>
        </tr>
    </table>
    <asp:RequiredFieldValidator ID="RequiredField" runat="server" ErrorMessage="Item code is required"
        SetFocusOnError="False" Display="Dynamic" ControlToValidate="txtItem" CssClass="errorMsg"
        Enabled="False"></asp:RequiredFieldValidator>
    <asp:RequiredFieldValidator ID="RequiredFieldLot" runat="server" ErrorMessage="Lot code is required"
        SetFocusOnError="False" Display="Dynamic" ControlToValidate="txtLot" CssClass="errorMsg"
        Enabled="False"></asp:RequiredFieldValidator>
    <div>
        <span>
            <asp:Button ID="btnSend" runat="server" disabled="disabled" Text="Query" Width="70px"
                Height="20px" OnClick="btnSend_Click" CssClass="ButtonsSendSave" />
        </span>
    </div>
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
                    </span>
                    <br />
                    <br />
                    <!--
                <span style="text-align: center">
                    <asp:Button ID="printLabel" runat="server" Height="24px" Text="Print Tag" Visible="False" OnClick="printLabel_Click" CssClass="ButtonsSendSave" />
                </span>
                -->
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
                                <asp:DropDownList ID="Dispoid" runat="server" Width="70%" Font-Names="Calibri" Font-Size="Small"
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
    <script type="text/javascript">

        function IniciarControles() {
            lblItemError = $('#lblItemError');
            lblLotError = $('#lblLotError');
        };

        $('#Contenido_txtLot').attr("disabled", true);
        $('#Contenido_btnSend').attr("disabled", true);

        console.log(LsItems);

        //        function ListaItems() {
        //            LsItems = JSON.parse('<%= ListaItemsJSON%>');
        //            console.log(LsItems);
        //            return LsItems;
        //        }

        function ListaLotes() {

            lstItems = JSON.parse('<%= ListaLotesJSON%>');
            console.log(LstLotes);
            return LstLotes;
        }

        function ValidarInputsActivacionBoton() {
            var Inputs = $('.InputForm');

            if (Inputs.hasClass("WrongValidation") == true) {
                $('#Contenido_btnSend').attr("disabled", true);
                return;
            } else {
                $('#Contenido_btnSend').attr("disabled", false);
            }

        }
        //ListaItems();
        $(function () {
            IniciarControles();

            function validaLote(clot) {

                var options = {
                    type: "POST",
                    url: "whInvMaterialRejectedD.aspx/TraerListaLotes",
                    data: "{'clot':'" + clot + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        ValidacionInput(msg.d, $("#Contenido_txtLot"));
                        if (msg.d == true) {
                            lblLotError.html("");
                        }
                        else {
                            lblLotError.html(txtLotError);
                        }
                    }
                };
                $.ajax(options);
            }

            // $('#Contenido_btnSend').attr("disabled", true);

            $("#Contenido_txtItem").change(function () {

                var TxItem = $("#Contenido_txtItem").val().toUpperCase().trim();

                var item = undefined;
                LsItems.forEach(function (x) {
                    if (x.item.toUpperCase().trim() == TxItem) {
                        item = x;
                    }
                    else {
                        if (item == undefined) {
                            item = undefined
                        }
                    }
                });

                //console.log(LstItems);

                if (item != undefined) {
                    ValidacionInput(true, $("#Contenido_txtItem"));
                    if (item.kltc == "1") {
                        $('#Contenido_txtLot').attr("disabled", false);
                        $('#Contenido_txtLot').addClass("InputForm");
                        $('#Contenido_txtLot').addClass("WrongValidation");
                        lblLotError.html(txtLotError);
                        lblItemError.html("");
                    }
                    else {
                        $('#Contenido_txtLot').attr("disabled", true);
                        $('#Contenido_txtLot').removeClass(".InputForm");
                        $('#Contenido_txtLot').removeClass("WrongValidation");
                        $('#Contenido_txtLot').removeClass("RightValidation");
                        $('#Contenido_txtLot').val("");
                        lblLotError.html("");
                        lblItemError.html("");
                    }
                } else {
                    ValidacionInput(false, $("#Contenido_txtItem"));
                    $('#Contenido_txtLot').attr("disabled", true);
                    $('#Contenido_txtLot').removeClass("InputForm");
                    $('#Contenido_txtLot').removeClass("WrongValidation");
                    $('#Contenido_txtLot').removeClass("RightValidation");
                    $('#Contenido_txtLot').val("");
                    lblItemError.html(txtItemError);
                    lblLotError.html("");
                }
                ValidarInputsActivacionBoton();
            });



            $("#Contenido_txtLot").change(function (e) {
                console.log(e.currentTarget);
                var TxILot = $("#Contenido_txtLot");
                validaLote(TxILot.val().toUpperCase().trim());
                ValidarInputsActivacionBoton();
            });

            //            $(".InputForm").keyup(function(e) {

            //                ValidarInputsActivacionBoton();
            //            });

            function ValidacionInput(status, MyInput) {
                status ? ValidacionCampoCorrecta(MyInput) : ValidacionCampoIorrecta(MyInput);

            }
            function ValidacionCampoCorrecta(MyInput) {
                $(MyInput).removeClass("WrongValidation");
                $(MyInput).addClass("RightValidation");
                ValidarInputsActivacionBoton();
            };

            function ValidacionCampoIorrecta(MyInput) {
                $(MyInput).removeClass("RightValidation");
                $(MyInput).addClass("WrongValidation");
                ValidarInputsActivacionBoton();
            };
            $('#Contenido_txtItem').attr("disabled", false);
        });
    </script>
</asp:Content>
