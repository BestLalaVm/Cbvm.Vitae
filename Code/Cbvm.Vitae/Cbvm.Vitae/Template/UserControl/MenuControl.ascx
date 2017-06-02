<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MenuControl.ascx.cs" Inherits="Cbvm.Vitae.Template.UserControl.MenuControl" %>
<div class="menu-container">
    <ul>
        <li><a href="<%=LkDataContext.AppConfig.HomePageLink %>" title="首页"><span>首页</span> </a></li>
        <li><a href="/Message/1" title="新闻列表"><span>新闻中心</span> </a></li>
        <li><a href="/Student/1" title="学生列表"><span>学生列表</span> </a></li>
        <li><a href="/Enterprise/1" title="企业名单"><span>企业名单</span> </a></li>
        <li><a href="/Enterprise/Register" title="企业注册"><span>企业注册</span></a></li>
    </ul>
</div>
