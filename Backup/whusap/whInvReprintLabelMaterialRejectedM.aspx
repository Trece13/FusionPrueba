<%@ Page AspCompat="true" Debug="true" %>
<%
if	Session("logok") <> "OKYes" then
	Session("Message")= "You must authenticate before you can execute this option."
	Response.Redirect ("whlogini.aspx?flg=Y")
End if
Dim strSQL, Odbcon, objrs
%>
<!-- #include file="include/dbxcononusa.inc" -->
<%
	strSQL = "insert into baan.ttccol301" & Session("env") & " (t$user,t$fein,t$come,t$refcntd,t$refcntu) values('" & Session("user") & "',sysdate+5/24,'RePrint Label Dmt RM',0,0)"
	objrs = Server.CreateObject("ADODB.Recordset")
	objrs.Open (strSQL, Odbcon)
'Desconectar a base de datos
%>
<!-- #include file="include/dbxconoff.inc" -->
<html>
	<head>
		<link href="../basic.css" rel="stylesheet" type="text/css" />
		<meta name="viewport" content="width=300, user-scalable=no" />
        <title>Material Rejected</title>
		<script type="text/javascript">
		    function CheckLength() {
		        //var regex = /^-?\d*[0-9]*[.]?[0-9]*$/;
		        //var re = new RegExp(regex);
		        var lenght = document.getElementById("orden").value.length;
		        var str = document.getElementById("orden").value;
		        var strs = str.substr(9, 1);
		        var strp = str.substr(10, 3);
		        //if (strp.match(re)) {
		        if (document.getElementById("orden").value != "") {
		            if (document.getElementById("orden").value.length < 9 || document.getElementById("orden").value.length > 9) {
		                alert("Please use this format WORKORDER, remember only 9 characters")
		                document.getElementById("orden").focus();
		                document.getElementById("orden").value = "";
		                return false;
		            }
		            // else {
		            //     if (strs != "-") {
		            //         alert("Please use this format WORKORDER, remember 9 characters workorder, simbol minus, 3 characters pallet id");
		            //         document.getElementById("txtorden").focus();
		            //         document.getElementById("txtorden").value = "";
		            //         return false;
		            //     }
		            // }
		        }
		        //}
		        else {
		            document.getElementById("orden").focus();
		            document.getElementById("orden").value = "";
		            //alert("Only numbers allowed on pallet id");
		        }
		    };
            
            function CheckLengthseq() {
		        var regex = /^-?\d*[0-9]*[.]?[0-9]*$/;
		        var re = new RegExp(regex);
		        var lenght = document.getElementById("seq").value.length;
		        var str = document.getElementById("seq").value;
		        var strp = str.substr(1, 3);
		        if (strp.match(re)) {
		            if (document.getElementById("seq").value != "") {
		                if (document.getElementById("seq").value.length < 0 || document.getElementById("seq").value.length > 3) {
		                    alert("Please use this format Sequence, remember maximun 3 characters")
		                    document.getElementById("seq").focus();
		                    document.getElementById("seq").value = "";
		                    return false;
		                }
		            }
		        }
		        else {
		            document.getElementById("seq").focus();
		            document.getElementById("seq").value = "";
		            alert("Only numbers allowed on pallet id");
		        }
		    };

		    function validate(evt) {
                var theEvent = evt || window.event;
		        var key = theEvent.keyCode || theEvent.which;
		        key = String.fromCharCode(key);
		        var regex = /[0-9]|\./;
		        if (!regex.test(key)) {
		            theEvent.returnValue = false;
		            if (theEvent.preventDefault) theEvent.preventDefault();
		        }
		    }


		</script>
		<style type="text/css">
			.errorMsg
			{
				color: Black;
				font-weight: bold;
				font-size: medium;
			}
		</style>
	</head>
	
<%
Dim strord, strseq, strlen, strflag, strmsg, strlote
strflag = Request.QueryString("flag")
If strflag = "Y" then
    strord = ""
    strseq = ""
    strlote = ""
    strmsg = Session("strmsg")
Else
    strord = ""
    strseq = ""
    strlote = ""
    strmsg = Session("strmsg")
End if
%>
    <body bgcolor="#87CEEB">
		<form id="frmrm"  method="post" action="whInvReprintLabelMaterialRejectedM2.aspx">
			<div>
                <table>
				    <tr>
					    <td><img alt="Logo Phoenix" src = "images/logophoenix_s.jpg" /></td>
					    <td></td>
					    <td>
                            <h2>Reprint Label Material Rejected</h2>
                        </td>
				    </tr>
			    </table>
			    <p style="width: 381px; height: 28px">
				    <a href="whLogoffi.aspx">
                        <img alt="Close Session" src="Images/btn_closesesion.jpg" />
                    </a>
				    <a href="whMenui.aspx">
                        <img alt="Main Menu" src="images/btn_Mainmenu.JPG" />
                    </a>
			    </p>
            </div>
			
            <div>
                <table align="left" class="tableDefault4" width="30%" border="0" cellspacing="0" cellpadding="0">
				    <tr>
					    <td class="titulog2"> <b>User</b>: </td>
					    <td class="titulog2"> <b style="width: 131px; height: 23px;"><%=Session("user")%></b> </td>
				    </tr>
				    <tr>
					    <td class="titulog2"> <b>Name</b>: </td>
					    <td class="titulog2"> <b style="width: 131px; height: 23px;"><%=Session("username")%></b> </td>
				    </tr>
				    <tr>
					    <td class="titulog2"> <b>Work Order</b>:</td>
					    <td class="titulog2"> 
						    <input type="text" id="orden" name="orden" size="9" style="width: 131px; height: 23px;" onchange="CheckLength();" value='<%=strord%>' /> 
					    </td>
				    </tr>
				    <tr>
					    <td class="titulog2"> <b>Lot</b>: </td>
					    <td class="titulog2"> 
						    <input type="text" id="lot" name="lot" size="9" style="width: 131px; height: 23px;" onchange="CheckLengthseq();" value='<%=strlote%>' />
					    </td>
				    </tr>
				    <tr>
					    <td class="titulog2"> <b>Sequence</b>: </td>
					    <td class="titulog2"> 
						    <input type="text" id="seq" name="seq" size="9" style="width: 131px; height: 23px;" onkeypress="validate(event)" value='<%=strseq%>' />
					    </td>
				    </tr>
				    <tr>
					    <td class="errorMsg" colspan="2">
						    <%=strmsg%>
					    </td>
				    </tr>
				    <tr>
					    <td colspan="2" align="center">
						    <input type="submit" name="btnOK" value="  OK  " />
					    </td>
				    </tr>
			    </table>
            </div>
		</form>
	</body>
</html>