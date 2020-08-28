<%@ Page Title="" Language="C#" MasterPageFile="~/MDMasterPage.Master" AutoEventWireup="true"
    CodeBehind="whMenuI.aspx.cs" Inherits="whusap.WebPages.Login.whMenuI" Theme="Cuadriculas"
    EnableViewState="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Encabezado" runat="server">
    <script src="https://code.jquery.com/jquery-3.3.1.slim.min.js" integrity="sha384-q8i/X+965DzO0rT7abK41JStQIAqVgRVzpbzo5smXKp4YfRvH+8abtTE1Pi6jizo"
        crossorigin="anonymous"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.7/umd/popper.min.js"
        integrity="sha384-UO2eT0CpHqdSJQ6hJty5KVphtPhzWj9WO1clHTMGa3JDZwrnQq4sF86dIHNDz0W1"
        crossorigin="anonymous"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js"
        integrity="sha384-JjSmVgyd0p3pXB1rRibZUAYoIIy6OrQ6VrjIEaFf/nJGzIxFDsf4x0xIM+B07jRM"
        crossorigin="anonymous"></script>
    <script type="text/javascript">
        function redirigirMenu(field) {
            var href = $(field).attr("data-url");
            window.location.href = href;
        }

        function saveNamePage(namePage) {

            sessionStorage.setItem("namePage", namePage.replace('_', ' ').replace('_', ' ').replace('_', ' ').replace('_', ' '));
        }

        function navegador() {
            var agente = window.navigator.userAgent;
            var navegadores = ["Chrome", "Firefox", "Safari", "Opera", "Trident", "MSIE", "Edge"];
            for (var i in navegadores) {
                if (agente.indexOf(navegadores[i]) != -1) {
                    return navegadores[i];
                }
                else {
                    return "Explorer"
                }
            }
        }

        function RedimencionExplorer1() {
            var Navegador = navegador();
            if (Navegador == "Explorer") {
                $("#ContentMainMenu").css({ 'zoom': '110%' });
            }
        }

        function TamTabletitle() {
            $('#tblTitleMain').width($('#Contenido_tblMenu').width());
        }

        function MyFnc(e) {
            console.log(e.id);
            x = e.id.replace("btn", "");
            x = x.replace(" ", "");
            if (e.value == "Ver Mas") {
                $("." + x + "Class :nth-child(1n+11)").show(500);
                e.value = "Ver Menos";
            }

            if (e.value == "View More") {
                $("." + x + "Class :nth-child(1n+11)").show(500);
                e.value = "View Less";
            }

            else if (e.value == "View Less") {
                $("." + x + "Class :nth-child(1n+11)").hide(500);
                e.value = "View More";
            }

            else if (e.value == "Ver Menos") {
                $("." + x + "Class :nth-child(1n+11)").hide(500);
                e.value = "Ver Mas";
            }
        };


        $(function () {
            TamTabletitle();
            RedimencionExplorer1();

            $('#FlowModeMenuSwitch').click(function () {
                if ($('#FlowModeMenuSwitch').attr('checked')) {
                    $('#ModeCategoryMenu').hide(1000);
                    $('#MenuFlow').show(1000);
                }
                else {
                    $('#MenuFlow').hide(100);
                    $('#ModeCategoryMenu').show(1000);


                }
            });

            function validarUsuarioMenu(x) {
                if (x == "1") {

                    $('#FlowModeMenuSwitch').click();
                    if ($('#FlowModeMenuSwitch').attr('checked')) {
                        $('#ModeCategoryMenu').hide(1000)
                        $('#MenuFlow').show(1000);
                    }
                    else {
                        $('#MenuFlow').show(1000);
                        $('#ModeCategoryMenu').hide(1000)


                    }
                }


            }

            var x = '<%=Session["ROLE"]%>';
            validarUsuarioMenu(x);



        }
        );

    </script>
    <style type="text/css">
        .carousel-control-prev, .carousel-control-next
        {
            width: 5% !important;
        }
        
        #tblTitleMain
        {
            display: none;
        }
        
        h1
        {
            font-family: 'Roboto';
            font: Roboto;
        }
        
        .btx
        {
            background-color: white;
            border: 0px solid white;
            color: gray;
        }
        
        a:hover
        {
            text-decoration: none;
        }
        
        li
        {
            list-style: none;
        }
        
        #MenuFlow li
        {
            padding-bottom: 10px;
        }
        .card-header
        {
            background-color: #2F80ED;
        }
        #Reportissues
        {
            bottom: 10%;
            right: 3%;
            position: fixed;
        }
        #lblCategoryTitle
        {
            font-family: Segoe UI Light;
        }
        #ContentMainMenu
        {
            zoom: 130%;
        }
        .btn
        {
            font-size: 1.2rem;
        }
        .bd-title
        {
            font-size: 1.5em;
        }
        .carousel-control-prev-icon
        {
            background-image: url("data:image/svg+xml;charset=utf8,%3Csvg xmlns='http://www.w3.org/2000/svg' fill='black' viewBox='0 0 8 8'%3E%3Cpath d='M5.25 0l-4 4 4 4 1.5-1.5-2.5-2.5 2.5-2.5-1.5-1.5z'/%3E%3C/svg%3E");
        }
        
        .carousel-control-next-icon
        {
            background-image: url("data:image/svg+xml;charset=utf8,%3Csvg xmlns='http://www.w3.org/2000/svg' fill='black' viewBox='0 0 8 8'%3E%3Cpath d='M2.75 0l-1.5 1.5 2.5 2.5-2.5 2.5 1.5 1.5 4-4-4-4z'/%3E%3C/svg%3E");
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div id="ContentMainMenu">
        <table id="tblTitleMain">
            <tr>
                <td>
                    <h1 id="LblTitleMainMenu" class="bd-title" style="font-family: 'Segoe UI Light';
                        text-align: left;">
                        Web portal main menu</h1>
                </td>
                <td>
                    <img alt="" src="<%= String.Concat(ConfigurationManager.AppSettings["UrlBase"], "/images/Grupo-Phoenix-1.png") %>"
                        align="middle" width="70px" />
                </td>
            </tr>
        </table>
        <!--<asp:Label ID="lblCategory" runat="server" Text="Label" Font-Size="Large" 
        ForeColor="Gray"></asp:Label>-->
        <br />
        <div runat="server" id="tblMenu">
        </div>
        <div id="Reportissues">
            <button type="button" class="btn btn-danger btn-lg">
                Report Issues</button>
        </div>
    </div>
</asp:Content>
