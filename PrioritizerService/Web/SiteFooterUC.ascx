<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SiteFooterUC.ascx.cs" Inherits="PrioritizerService.FooterUC" %>

<img src="Images/white.gif" alt="" style="width:1px;height:20px;" /><br />

<table id="FooterTable" border="0" cellpadding="0" cellspacing="0">
<tr>
    <td class="tl"><img src="../Images/corner_tl.gif" class="cornerSize" alt="" /></td>
    <td class="tm" rowspan="2">
        <div class="floatLeft">
        <span id="tabCampaigns" runat="server"><a href="">Priori</a>&nbsp;|&nbsp;</span>        
        <span id="tabHelp" runat="server"><a href="javascript:openWin('Help/index.html',800,600);">Help</a></span>
        </div>
        
    </td>
    <td class="tr"><img src="../Images/corner_tr.gif" class="cornerSize" alt="" /></td>
</tr>
<tr>
    <td class="bl"><img src="../Images/corner_bl.gif" class="cornerSize" alt="" /></td>
    <td class="br"><img src="../Images/corner_br.gif" class="cornerSize" alt="" /></td>
</tr>
</table>

<br />

<script type="text/javascript">
//if (MSIE_VER() > 0) {
//	document.onreadystatechange = fnStartInit;
//	function fnStartInit() {
//	    if (document.readyState == "complete") {
//		    setPageHeight();
//	    }
//    }
//} else {
//	window.onload = setPageHeight();
//}
</script>