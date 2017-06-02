<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MenuControl.ascx.cs" Inherits="Cbvm.Vitae.Manage.UserControl.MenuControl" %>
<div class="menu-container">
    <ul>
        <li><a href="<%=LkDataContext.AppConfig.HomePageLink %>" title="首页"><span>首页</span> </a></li>
        <li><a href="<%=Prefix %>/MessageList.aspx" title="新闻列表"><span>新闻中心</span> </a></li>
        <li><a href="<%=Prefix %>/StudentList.aspx" title="学生列表"><span>学生列表</span> </a></li>
        <li><a href="<%=Prefix %>/EnterpriseList.aspx" title="企业名单"><span>企业名单</span> </a></li>
        <li>
          <a href="<%=Prefix %>/EnterpriseRegister.aspx" title="企业注册"><span>企业注册</span></a>
        </li>
    </ul>
</div>
