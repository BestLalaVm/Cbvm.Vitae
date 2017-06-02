<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ThirdLogoutControl.ascx.cs" Inherits="Cbvm.Vitae.UserControl.ThirdLogoutControl" %>

<%if (WebLibrary.Helper.AuthorizeHelper.IsNotifyThirdLogout())
  { %>
<div style="display: none;">
    <iframe src="<%=LkDataContext.AppConfig.JavaLogoutLink %>" width="0px" height="0px" style="display: none;"></iframe>
</div>
<script type="text/javascript">
    window.location.reload();
</script>
<%} %>
