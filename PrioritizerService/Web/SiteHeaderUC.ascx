<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SiteHeaderUC.ascx.cs" Inherits="PrioritizerService.SiteHeaderUC" %>

<table id="HeaderTable" border="0" cellpadding="0" cellspacing="0">
<tr>
	<td id="" background= "Images/header.png" style="height:73px;width:1000px" align="right" >
        <%--<img src="Images/header.png" alt="Priori" title="Priori" />--%>
        <asp:LoginName ID="LoginName2" runat="server" FormatString="Active User: {0}" />  &nbsp; | &nbsp;<asp:LoginStatus ID="LoginStatus1" runat="server" />&nbsp;
    </td>	
</tr>
<tr>
    <td valign="bottom" align="right" colspan="3" style="padding-right:15px;">
        <table border="0" cellspacing="0" cellpadding="0" id="HeaderMenuItems">
        <tr>
            <td><div id="tabCampaigns" class="tabOff" runat="server"><div class="ItemTab" onclick="JavaScript:redirect2Page('#');"> </div></div></td>
            <td><div id="tabReports" class="tabOff" runat="server"><div class="ItemTab" onclick="JavaScript:redirect2Page('#');"></div></div></td>
            <td><div id="tabAccount" class="tabOff" runat="server"><div class="ItemTab" onclick="JavaScript:redirect2Page('#');"></div></div></td>
        </tr>
        </table>
    </td>
</tr>
</table>