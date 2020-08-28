<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MDMasterPage.Master" CodeBehind="whInvSupervisorTruck.aspx.cs" Inherits="whusap.WebPages.InvTrans.whInvSupervisorTruck" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Encabezado" runat="server">

<script>
    $(function () {
        getPagination('#grdPalletsUID');

        function getPagination(table) {

            debugger;
            var lastPage = 1;

            $('#maxRows').change(function (evt) {
                $('.paginationprev').html('');


                lastPage = 1;
                $('.pagination').find("li").slice(1, -1).remove();
                var trnum = 0;
                var maxRows = parseInt($(this).val());

                if (maxRows == 5000) {
                    $('.pagination').hide();
                } else {
                    $('.pagination').show();
                }

                var totalRows = $(table + ' tbody tr').length;
                $(table + ' tr:gt(0)').each(function () {
                    trnum++;
                    if (trnum > maxRows) {
                        $(this).hide();
                    } if (trnum <= maxRows) { $(this).show(); } 
                });
                if (totalRows > maxRows) {
                    var pagenum = Math.ceil(totalRows / maxRows);
                    //	numbers of pages 
                    for (var i = 1; i <= pagenum; ) {
                        $('.pagination #prev').before('<li data-page="' + i + '">\
								      <span>' + i++ + '<span class="sr-only">(current)</span></span>\
								    </li>').show();
                    } 										
                } 												
                $('.pagination [data-page="1"]').addClass('active');
                $('.pagination li').click(function (evt) {
                    evt.stopImmediatePropagation();
                    evt.preventDefault();
                    var pageNum = $(this).attr('data-page');

                    var maxRows = parseInt($('#maxRows').val());

                    if (pageNum == "prev") {
                        if (lastPage == 1) { return; }
                        pageNum = --lastPage;
                    }
                    if (pageNum == "next") {
                        if (lastPage == ($('.pagination li').length - 2)) { return; }
                        pageNum = ++lastPage;
                    }

                    lastPage = pageNum;
                    var trIndex = 0;
                    $('.pagination li').removeClass('active');
                    $('.pagination [data-page="' + lastPage + '"]').addClass('active'); 
                    $(table + ' tr:gt(0)').each(function () {
                        trIndex++;
                        if (trIndex > (maxRows * pageNum) || trIndex <= ((maxRows * pageNum) - maxRows)) {
                            $(this).hide();
                        } else { $(this).show(); }
                    });
                });
            }).val(5).change();
        }
    });
</script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <table>
        <tr>
           <td colspan="2" style="font-weight:bold; text-align:center;">
                <asp:Label ID="lblNameForm" Text="" runat="server" />
               <hr />   
           </td>
        </tr>
        <tr>
            <td style="text-align:center;">
                <span style="vertical-align: middle" /><span class="style2" style="vertical-align: middle;">
                <b style="font-size: 11px;"><asp:Label runat="server" ID="lblScan"></asp:Label></b></span>
            </td>
            <td style="width: 250px">
                <span style="vertical-align: middle;">
                    <asp:TextBox runat="server" ID="txtUniqueID" CssClass="TextboxBig" OnTextChanged="txtUniqueID_OnTextChanged" AutoPostBack="true" style="width:100%;"/>    
                </span>
            </td>
        </tr>
    </table>

    <fieldset runat="server" id="dvDataTruck" style="width:80%;" >
        <span style="vertical-align: middle" /><span class="style2" style="vertical-align: middle;">
        <b style="font-size: 11px;" runat="server" id="bMessage"></b></span>
        <hr />  
        <div>
        <select class  ="form-control" name="state" id="maxRows">
						 <option value="5000">Show ALL Rows</option>
						 <option value="5">5</option>
						 <option value="10">10</option>
						 <option value="15">15</option>
						 <option value="20">20</option>
						 <option value="50">50</option>
						 <option value="70">70</option>
						 <option value="100">100</option>
						</select>
            <asp:GridView ID="grdPalletsUID" ClientIDMode="Static" runat="server" AutoGenerateColumns="False" 
            EmptyDataText="Not found Pallets ID to this UID" Font-Name="Verdana" Font-Size="12px" Cellpadding="4" HeaderStyle-BackColor="#444444"
            HeaderStyle-ForeColor="White" AlternatingRowStyle-BackColor="#dddddd" style="width:100%;">
            <Columns>
                <asp:BoundField DataField="PAID" ReadOnly="True" HeaderText="Pallet ID" InsertVisible="False" SortExpression="ProductID"></asp:BoundField>
                <asp:BoundField DataField="ITEM" ReadOnly="True" HeaderText="Item" InsertVisible="False" SortExpression="ProductID"></asp:BoundField>
                <asp:BoundField DataField="CLOT" ReadOnly="True" HeaderText="Lot" InsertVisible="False" SortExpression="ProductID"></asp:BoundField>
                <asp:BoundField DataField="QTDT" ReadOnly="True" HeaderText="Quantity" InsertVisible="False" SortExpression="ProductID"></asp:BoundField>
                <asp:BoundField DataField="LOSO" ReadOnly="True" HeaderText="Location" InsertVisible="False" SortExpression="ProductID"></asp:BoundField>
            </Columns>
            </asp:GridView>
            <div class='pagination-container' >
				<nav>
				  <ul class="pagination">
                    <li data-page="prev" >
					    <span> < <span class="sr-only">(current)</span></span>
					</li>
                    <li data-page="next" id="prev">
						<span> > <span class="sr-only">(current)</span></span>
					</li>
				  </ul>
				</nav>
			</div>
        </div>
        <hr />
        
        <div style="margin-left:30%; margin-right:30%;">
            <asp:Button Text="End Picking" runat="server" ID="btnEndPicking" CssClass="ButtonsSendSave" OnClick="btnEndPicking_Click" style="height:30px; width:100%" />
            <asp:Button Text="End Receipt" runat="server" ID="btnEndReceipt" CssClass="ButtonsSendSave" OnClick="btnEndReceive_Click" style="height:30px; width:100%" />
        </div>
    </fieldset>   
    <br />  
     <asp:Label Text="" runat="server" ID="lblError" style="color:red; font-weight:bold; font-size:15px;" />    
    <asp:Label Text="" runat="server" ID="lblConfirm" style="color:green; font-weight:bold; font-size:15px;" /> 
</asp:Content>