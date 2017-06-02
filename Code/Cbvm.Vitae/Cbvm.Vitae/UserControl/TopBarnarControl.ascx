<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TopBarnarControl.ascx.cs" Inherits="Cbvm.Vitae.UserControl.TopBarnarControl" %>
<%--<div class="headDown">
    <div class="comContent">
        <span><a href="#">返回首页</a></span>
    </div>
</div>
<div class="activity">
    <a href="###">
        <img src="/Content/images/activity.jpg" width="1000"></a>
</div>--%>


<div class="headDown">
    <div class="comContent" style="line-height: 50px;">
        <span style="width: 50px; float: left;"><a href="<%=LkDataContext.AppConfig.HomePageLink %>">首页</a></span>
        <span style="width: 100px; float: left;"><a href="/Student">学生列表</a></span>
        <span style="width: 100px; float: left;"><a href="/Enterprise">企业名单</a></span>
        <span style="width: 100px; float: left;"><a href="/Enterprise/Register">企业注册</a></span>
    </div>
</div>
<div class="activity">
    <a href="###">
        <img src="/Content/images/activity.jpg" width="1000"></a>
</div>