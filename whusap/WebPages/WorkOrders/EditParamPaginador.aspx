<%@ Page Title="" Language="C#" MasterPageFile="~/MDMasterPage.Master" AutoEventWireup="true"
    CodeBehind="EditParamPaginador.aspx.cs" Inherits="whusap.WebPages.WorkOrders.EditParamPaginador" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Encabezado" runat="server">
    <script src="https://code.jquery.com/jquery-3.3.1.js"></script>
    <script type="text/javascript" src="https://cdn.datatables.net/1.10.20/js/jquery.dataTables.min.js"></script>
    <style>
        #divForm{
            margin-bottom : 800px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div id="divForm">
        <div id="divL" class="col-sm-6">
            <div class="form-group row ">
                <label class="col-sm-4 col-form-label-lg" for="txCustomer" id="lblpalletID">
                    Time Paginator
                </label>
                <div class="col-sm-6">
                    <input type="text" class="form-control form-control-lg col-sm-12" id="txTimePaginator"
                        placeholder="Time Paginator"/>
                </div>
            </div>
            <div class="form-group row ">
                <label class="col-sm-4 col-form-label-lg" for="" id="Label1">
                    Time Query Refresh</label>
                <div class="col-sm-6">
                    <input type="number" class="form-control form-control-lg col-sm-12" id="txTimeRefresh"
                        placeholder="CicloActualizacion" />
                </div>
            </div>
            <div class="form-group row ">
                <label class="col-sm-4 col-form-label-lg" for="" id="Label1">
                    Number RetryOnSave</label>
                <div class="col-sm-6">
                    <input type="number" class="form-control form-control-lg col-sm-12" id="TxnumberRetryOnSave"
                        placeholder="numberRetryOnSave" />
                </div>
            </div>
            <div class="form-group row ">
                <label class="col-sm-4 col-form-label-lg" for="" id="Label1">
                    Body Color</label>
                <div class="col-sm-6">
                    <input type="text" class="form-control form-control-lg col-sm-12" id="TxbodyColor"
                        placeholder="bodyColor" />
                </div>
            </div>
<%--            <div class="form-group row ">
                <label class="col-sm-4 col-form-label-lg" for="" id="Label1">
                    Enlace Ret</label>
                <div class="col-sm-6">
                    <input type="text" class="form-control form-control-lg col-sm-12" id="TxenlaceRet"
                        placeholder="enlaceRet" />
                </div>
            </div>--%>
            <div class="form-group row ">
                <label class="col-sm-4 col-form-label-lg" for="" id="Label1">
                    Owner</label>
                <div class="col-sm-6">
                    <input type="text" class="form-control form-control-lg col-sm-12" id="Txowner" placeholder="owner" />
                </div>
            </div>
            <div class="form-group row ">
                <label class="col-sm-4 col-form-label-lg" for="" id="Label1">
                    Env</label>
                <div class="col-sm-6">
                    <input type="text" class="form-control form-control-lg col-sm-12" id="Txenv" placeholder="env" />
                </div>
            </div>
            <div class="form-group row ">
                <label class="col-sm-4 col-form-label-lg" for="" id="Label1">
                    Initial Vector</label>
                <div class="col-sm-6">
                    <input type="text" class="form-control form-control-lg col-sm-12" id="TxInitialVector"
                        placeholder="InitialVector" />
                </div>
            </div>
            <div class="form-group row ">
                <label class="col-sm-4 col-form-label-lg" for="" id="Label1">
                    Key Algorithm</label>
                <div class="col-sm-6">
                    <input type="text" class="form-control form-control-lg col-sm-12" id="TxKeyAlgorithm"
                        placeholder="KeyAlgorithm" />
                </div>
            </div>
            <div class="form-group row ">
                <label class="col-sm-4 col-form-label-lg" for="" id="Label1">
                    Env Col</label>
                <div class="col-sm-6">
                    <input type="text" class="form-control form-control-lg col-sm-12" id="TxenvCol" placeholder="envCol" />
                </div>
            </div>
            <div class="form-group row ">
                <label class="col-sm-4 col-form-label-lg" for="" id="Label1">
                    Envt</label>
                <div class="col-sm-6">
                    <input type="text" class="form-control form-control-lg col-sm-12" id="Txenvt" placeholder="envt" />
                </div>
            </div>
            <div class="form-group row ">
                <label class="col-sm-4 col-form-label-lg" for="" id="Label1">
                    Cia</label>
                <div class="col-sm-6">
                    <input type="text" class="form-control form-control-lg col-sm-12" id="Txcia" placeholder="cia" />
                </div>
            </div>
            <div class="form-group row ">
                <label class="col-sm-4 col-form-label-lg" for="" id="Label1">
                    Default Zone</label>
                <div class="col-sm-6">
                    <input type="text" class="form-control form-control-lg col-sm-12" id="TxDefaultZone"
                        placeholder="DefaultZone" />
                </div>
            </div>
            <div class="form-group row ">
                <label class="col-sm-4 col-form-label-lg" for="" id="Label1">
                    Warehouse Req</label>
                <div class="col-sm-6">
                    <input type="text" class="form-control form-control-lg col-sm-12" id="TxWarehouseReq"
                        placeholder="WarehouseReq" />
                </div>
            </div>
            <div class="form-group row ">
                <label class="col-sm-4 col-form-label-lg" for="" id="Label1">
                    Balance Machines</label>
                <div class="col-sm-6">
                    <input type="text" class="form-control form-control-lg col-sm-12" id="TxBalanceMachines"
                        placeholder="BalanceMachines" />
                </div>
            </div>
            <div class="form-group row ">
                <label class="col-sm-4 col-form-label-lg" for="" id="Label1">
                    Balance Machines Retail</label><div class="col-sm-6">
                        <input type="text" class="form-control form-control-lg col-sm-12" id="TxBalanceMachinesRetail"
                            placeholder="BalanceMachinesRetail" />
                    </div>
            </div>
        </div>
        <div id="divR"  class="col-sm-6">
        <div class="form-group row ">
                <label class="col-sm-4 col-form-label-lg" for="" id="Label2">
                    Disposition</label>
                <div class="col-sm-6">
                    <input type="text" class="form-control form-control-lg col-sm-12" id="TxDisposition"
                        placeholder="Disposition" />
                </div>
            </div>
            <div class="form-group row ">
                <label class="col-sm-4 col-form-label-lg" for="" id="Label3">
                    Time Out Roll Save</label>
                <div class="col-sm-6">
                    <input type="text" class="form-control form-control-lg col-sm-12" id="TxtimeOutRollSave"
                        placeholder="timeOutRollSave" />
                </div>
            </div>
            <div class="form-group row ">
                <label class="col-sm-4 col-form-label-lg" for="" id="Label4">
                    Initial Consec Tag Id</label>
                <div class="col-sm-6">
                    <input type="text" class="form-control form-control-lg col-sm-12" id="TxinitialConsecTagId"
                        placeholder="initialConsecTagId" />
                </div>
            </div>
            <%--<div class="form-group row ">
                <label class="col-sm-4 col-form-label-lg" for="" id="Label1">
                    UrlBaseBarcode</label>
                <div class="col-sm-6">
                    <input type="text" class="form-control form-control-lg col-sm-12" id="TxUrlBaseBarcode"
                        placeholder="UrlBaseBarcode" />
                </div>
            </div>--%>
            <%--<div class="form-group row ">
                <label class="col-sm-4 col-form-label-lg" for="" id="Label1">
                    Urlshoplogixxml</label>
                <div class="col-sm-6">
                    <input type="text" class="form-control form-control-lg col-sm-12" id="Txurlshoplogixxml"
                        placeholder="urlshoplogixxml" />
                </div>
            </div>--%>
            <%--<div class="form-group row ">
                <label class="col-sm-4 col-form-label-lg" for="" id="Label1">
                    PathResourcesSQL</label>
                <div class="col-sm-6">
                    <input type="text" class="form-control form-control-lg col-sm-12" id="TxpathResourcesSQL"
                        placeholder="pathResourcesSQL" />
                </div>
            </div>--%>
            <%--<div class="form-group row ">
                <label class="col-sm-4 col-form-label-lg" for="" id="Label1">
                    SaveFilesInFolder</label>
                <div class="col-sm-6">
                    <input type="text" class="form-control form-control-lg col-sm-12" id="TxSaveFilesInFolder"
                        placeholder="SaveFilesInFolder" />
                </div>
            </div>--%>
            <%--<div class="form-group row ">
                <label class="col-sm-4 col-form-label-lg" for="" id="Label1">
                    PDFImages</label>
                <div class="col-sm-6">
                    <input type="text" class="form-control form-control-lg col-sm-12" id="TxPDFImages"
                        placeholder="PDFImages" />
                </div>
            </div>--%>
            <div class="form-group row ">
                <label class="col-sm-4 col-form-label-lg" for="" id="Label5">
                    User shop logix</label>
                <div class="col-sm-6">
                    <input type="text" class="form-control form-control-lg col-sm-12" id="Txusershoplogix"
                        placeholder="usershoplogix" />
                </div>
            </div>
            <div class="form-group row ">
                <label class="col-sm-4 col-form-label-lg" for="" id="Label6">
                    Pass Shop Logix</label>
                <div class="col-sm-6">
                    <input type="text" class="form-control form-control-lg col-sm-12" id="Txpassshoplogix"
                        placeholder="passshoplogix" />
                </div>
            </div>
            <%--<div class="form-group row ">
                <label class="col-sm-4 col-form-label-lg" for="" id="Label1">
                    PathDownload</label>
                <div class="col-sm-6">
                    <input type="text" class="form-control form-control-lg col-sm-12" id="TxpathDownload"
                        placeholder="pathDownload" />
                </div>
            </div>--%>
            <div class="form-group row ">
                <label class="col-sm-4 col-form-label-lg" for="" id="Label7">
                    User Impersonation</label>
                <div class="col-sm-6">
                    <input type="text" class="form-control form-control-lg col-sm-12" id="TxuserImpersonation"
                        placeholder="userImpersonation" />
                </div>
            </div>
            <div class="form-group row ">
                <label class="col-sm-4 col-form-label-lg" for="" id="Label8">
                    Pass Impersonation</label>
                <div class="col-sm-6">
                    <input type="text" class="form-control form-control-lg col-sm-12" id="TxpassImpersonation"
                        placeholder="passImpersonation" />
                </div>
            </div>
            <div class="form-group row ">
                <label class="col-sm-4 col-form-label-lg" for="" id="Label9">
                    Doma Impersonation</label>
                <div class="col-sm-6">
                    <input type="text" class="form-control form-control-lg col-sm-12" id="TxdomaImpersonation"
                        placeholder="domaImpersonation" />
                </div>
            </div>
           <%-- <div class="form-group row ">
                <label class="col-sm-4 col-form-label-lg" for="" id="Label1">
                    SaveDocQuality</label>
                <div class="col-sm-6">
                    <input type="text" class="form-control form-control-lg col-sm-12" id="TxSaveDocQuality"
                        placeholder="SaveDocQuality" />
                </div>
            </div>--%>
<%--            <div class="form-group row ">
                <label class="col-sm-4 col-form-label-lg" for="" id="Label1">
                    UrlBase</label>
                <div class="col-sm-6">
                    <input type="text" class="form-control form-control-lg col-sm-12" id="TxUrlBase"
                        placeholder="UrlBase" />
                </div>
            </div>--%>
            <div class="form-group row ">
                <label class="col-sm-4 col-form-label-lg" for="" id="Label10">
                    Automatic Ad</label>
                <div class="col-sm-6">
                    <input type="text" class="form-control form-control-lg col-sm-12" id="TxanuncioAutomatico"
                        placeholder="anuncioAutomatico" />
                </div>
            </div>
            <div class="form-group row ">
                <label class="col-sm-4 col-form-label-lg" for="" id="Label11">
                    Password Change Time</label>
                <div class="col-sm-6">
                    <input type="text" class="form-control form-control-lg col-sm-12" id="TxtiempoCambioContrasena"
                        placeholder="tiempoCambioContrasena" /></div>
            </div>
            <div class="form-group row ">
                <label class="col-sm-4 col-form-label-lg" for="" id="Label12">   
                Return Site</label>
                <div class="col-sm-6">
                    <input type="text" class="form-control form-control-lg col-sm-12" id="TxsitioConRetorno"
                        placeholder="sitioConRetorno" />
                </div>
            </div>
            <div class="form-group row ">
                <label class="col-sm-4 col-form-label-lg" for="" id="Label13">
                    Calc Label Pallet Tag</label>
                <div class="col-sm-6">
                    <input type="text" class="form-control form-control-lg col-sm-12" id="TxcalcLabelPalletTag"
                        placeholder="calcLabelPalletTag" />
                </div>
            </div>
            <div class="form-group row ">
                <label class="col-sm-4 col-form-label-lg" for="" id="Label14">
                    Calc Ad Ord</label>
                <div class="col-sm-6">
                    <input type="text" class="form-control form-control-lg col-sm-12" id="TxcalcAnuncioOrd"
                        placeholder="calcAnuncioOrd" />
                </div>
            </div>
            <div class="form-group row ">
                <label class="col-sm-4 col-form-label-lg" for="" id="Label15">
                    Calc Inv Label</label>
                <div class="col-sm-6">
                    <input type="text" class="form-control form-control-lg col-sm-12" id="TxcalcInvLabel"
                        placeholder="calcInvLabel" />
                </div>
            </div>
        </div>
        <div class="form-group row col-sm-12" id="divQueryAction" >
                <div class="col-sm-12">
                </div>
                <div class="col-sm-12">
                    <input type="button" class="btn btn-primary btn-lg col-sm-12 " id="btnSave" value="Save"
                        onclick="ClickSave()" />
                </div>
            </div>
    </div>
    <script>
        $(function () {
            txTimePaginator = $('#txTimePaginator');
            txTimeRefresh = $('#txTimeRefresh')
            TxnumberRetryOnSave = $('#TxnumberRetryOnSave')
            TxbodyColor = $('#TxbodyColor')
//            TxenlaceRet = $('#TxenlaceRet')
            Txowner = $('#Txowner')
            Txenv = $('#Txenv')
            TxInitialVector = $('#TxInitialVector')
            TxKeyAlgorithm = $('#TxKeyAlgorithm')
            TxenvCol = $('#TxenvCol')
            Txenvt = $('#Txenvt')
            Txcia = $('#Txcia')
            TxDefaultZone = $('#TxDefaultZone')
            TxWarehouseReq = $('#TxWarehouseReq')
            TxBalanceMachines = $('#TxBalanceMachines')
            TxBalanceMachinesRetail = $('#TxBalanceMachinesRetail')
            TxDisposition = $('#TxDisposition')
            TxtimeOutRollSave = $('#TxtimeOutRollSave')
            TxinitialConsecTagId = $('#TxinitialConsecTagId')
//            TxUrlBaseBarcode = $('#TxUrlBaseBarcode')
//            Txurlshoplogixxml = $('#Txurlshoplogixxml')
//            TxpathResourcesSQL = $('#TxpathResourcesSQL')
//            TxSaveFilesInFolder = $('#TxSaveFilesInFolder')
//            TxPDFImages = $('#TxPDFImages')
            Txusershoplogix = $('#Txusershoplogix')
            Txpassshoplogix = $('#Txpassshoplogix')
//            TxpathDownload = $('#TxpathDownload')
            TxuserImpersonation = $('#TxuserImpersonation')
            TxpassImpersonation = $('#TxpassImpersonation')
            TxdomaImpersonation = $('#TxdomaImpersonation')
//            TxSaveDocQuality = $('#TxSaveDocQuality')
//            TxUrlBase = $('#TxUrlBase')
            TxanuncioAutomatico = $('#TxanuncioAutomatico')
            TxtiempoCambioContrasena = $('#TxtiempoCambioContrasena')
            TxsitioConRetorno = $('#TxsitioConRetorno')
            TxcalcLabelPalletTag = $('#TxcalcLabelPalletTag')
            TxcalcAnuncioOrd = $('#TxcalcAnuncioOrd')
            TxcalcInvLabel = $('#TxcalcInvLabel')

            txTimePaginator.val('<%=CicloPaginacion %>');
            txTimeRefresh.val('<%=CicloActualizacion %>');
            TxnumberRetryOnSave.val('<%= numberRetryOnSave%>');
            TxbodyColor.val('<%= bodyColor%>');
            Txowner.val('<%= owner%>');
            Txenv.val('<%= env%>');
            TxInitialVector.val('<%= InitialVector%>');
            TxKeyAlgorithm.val('<%= KeyAlgorithm%>');
            TxenvCol.val('<%= envCol%>');
            Txenvt.val('<%= envt%>');
            Txcia.val('<%= cia%>');
            TxDefaultZone.val('<%= DefaultZone%>');
            TxWarehouseReq.val('<%= WarehouseReq%>');
            TxBalanceMachines.val('<%= BalanceMachines%>');
            TxBalanceMachinesRetail.val('<%= BalanceMachinesRetail%>');
            TxDisposition.val('<%= Disposition%>');
            TxtimeOutRollSave.val('<%= timeOutRollSave%>');
            TxinitialConsecTagId.val('<%= initialConsecTagId%>');
            Txusershoplogix.val('<%= usershoplogix%>');
            Txpassshoplogix.val('<%= passshoplogix%>');
            TxuserImpersonation.val('<%= userImpersonation%>');
            TxpassImpersonation.val('<%= passImpersonation%>');
            TxdomaImpersonation.val('<%= domaImpersonation%>');
            TxanuncioAutomatico.val('<%= anuncioAutomatico%>');
            TxtiempoCambioContrasena.val('<%= tiempoCambioContrasena%>');   
            TxsitioConRetorno.val('<%= sitioConRetorno%>');
            TxcalcLabelPalletTag.val('<%= calcLabelPalletTag%>');
            TxcalcAnuncioOrd.val('<%= calcAnuncioOrd%>');
            TxcalcInvLabel.val('<%= calcInvLabel%>');



        });
        function sendAjax(WebMethod, Data, FuncitionSucces, asyncMode) {
            var options = {
                type: "POST",
                url: "EditParamPaginador.aspx/" + WebMethod,
                data: Data,
                contentType: "application/json; charset=utf-8",
                async: asyncMode != undefined ? asyncMode : true,
                dataType: "json",
                success: FuncitionSucces
            };
            $.ajax(options);

            WebMethod = "";
        }
        function Save() {

        }
        var ClickSave = function () {
            //var Data = "{'key':'" + value + "'}";
            var Data = "{'PCicloPaginacion':'"+$('#TxCicloPaginacion').val()+"','PCicloActualizacion':'"+$('#TxCicloActualizacion').val()+"','PnumberRetryOnSave':'"+$('#TxnumberRetryOnSave').val()+"','PbodyColor':'"+$('#TxbodyColor').val()+"','Powner':'"+$('#Txowner').val()+"','Penv':'"+$('#Txenv').val()+"','PInitialVector':'"+$('#TxInitialVector').val()+"','PKeyAlgorithm':'"+$('#TxKeyAlgorithm').val()+"','PenvCol':'"+$('#TxenvCol').val()+"','Penvt':'"+$('#Txenvt').val()+"','Pcia':'"+$('#Txcia').val()+"','PDefaultZone':'"+$('#TxDefaultZone').val()+"','PWarehouseReq':'"+$('#TxWarehouseReq').val()+"','PBalanceMachines':'"+$('#TxBalanceMachines').val()+"','PBalanceMachinesRetail':'"+$('#TxBalanceMachinesRetail').val() +"','PDisposition':'"+$('#TxDisposition').val()+"','PtimeOutRollSave':'"+$('#TxtimeOutRollSave').val()+"','PinitialConsecTagId':'"+$('#TxinitialConsecTagId').val()+"','Pusershoplogix':'"+$('#Txusershoplogix').val()+"','Ppassshoplogix':'"+$('#Txpassshoplogix').val()+"','PuserImpersonation':'"+$('#TxuserImpersonation').val()+"','PpassImpersonation':'"+$('#TxpassImpersonation').val()+"','PdomaImpersonation':'"+$('#TxdomaImpersonation').val()+"','PanuncioAutomatico':'"+$('#TxanuncioAutomatico').val()+"','PtiempoCambioContrasena':'"+$('#TxtiempoCambioContrasena').val()+"','PsitioConRetorno':'"+$('#TxsitioConRetorno').val()+"','PcalcLabelPalletTag':'"+$('#TxcalcLabelPalletTag').val()+"','PcalcAnuncioOrd':'"+$('#TxcalcAnuncioOrd').val()+"','PcalcInvLabel':'"+$('#TxcalcInvLabel').val()+"'}";
            
            WebMethod = "ClickSave";
            sendAjax(WebMethod, Data, ClickSaveSuccess, false);
        }

        function ClickSaveSuccess(r) {
            alert(r.d);
        }
    </script>
</asp:Content>
