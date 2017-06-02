<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LogOffAccount.aspx.cs" Inherits="Cbvm.Vitae.LogOffAccount" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <div class="full-loading-container" style="position: fixed; z-index: 999999999999999999999999; width: 100%; height: 100%; background-color: whiteSmoke; filter: alpha(opacity=80); -moz-opacity: 0.8; opacity: 0.8; top: 0px; left: 0px;">
        <div class="loading" style="margin-top: 210px; width: 250px; margin-left: auto; margin-right: auto; margin-top: 100px; text-align: center; color: red;">
            <img src="/Image/xubox_loading2.gif" alt="loading">
            <div class="loading-label">努力跳转中...</div>
        </div>
    </div>
    <div style="display: none;">
        <iframe src="<%=LkDataContext.AppConfig.JavaLogoutLink %>" width="0px" height="0px" style="display: none;"></iframe>
    </div>

    <script type="text/javascript">
        setTimeout(function () {
            window.location.href = "<%=LkDataContext.AppConfig.HomePageLink%>";
        }, 800);
    </script>
</body>
</html>
