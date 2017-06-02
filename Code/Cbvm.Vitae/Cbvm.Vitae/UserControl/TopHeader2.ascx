<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TopHeader2.ascx.cs" Inherits="Cbvm.Vitae.UserControl.TopHeader2" %>
<div class="headTop">
    <div class="comContent">

        <a style="float: left;" href="<%=LkDataContext.AppConfig.HomePageLink %>">
            <img src="/Content/images/logo.png"></a>
        <div class="headNav">
            <a href="<%=LkDataContext.AppConfig.HomePageLink %>">首页</a>
        </div>
        <div class="headNav">
            <a href="http://www.xmtcluck.com:8080/jxq/front/search.jhtml?p=find&key=&searchType=job">找职位</a>
        </div>
        <div class="headNav">
            <a href="<%=LkDataContext.AppConfig.JavaFindStudentLink %>">找学生</a>
        </div>
        <div class="headNav">
            <a href="<%=LkDataContext.AppConfig.JavaFindEnterpriseLink %>">名企</a>
        </div>
        <div class="headNav">
            <a href="/Enterprise/Register">企业注册</a>
        </div>
        <div class="headNav" style="float: right; width: 150px; font-size: 12px;">
            <%if (CurrentUser == null || CurrentUser.UserType == Presentation.Enum.UserType.Guest)
              { %>
<%--            <a class="" href="<%=LkDataContext.AppConfig.JavaAdminLoginLink %>">管理员入口</a>--%>
            <a class="" href="<%=LkDataContext.AppConfig.JavaLoginLink %>">登入</a>
            <%}
              else
              { %>
            <asp:HyperLink ID="link2Backend" runat="server" CssClass="backend-class"><span>进入后台</span></asp:HyperLink>
            <a href="/LogOffAccount.aspx" style="color: #fff;">注销</a>
            <%} %>
        </div>
    </div>
</div>
