<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TopHeader.ascx.cs" Inherits="Cbvm.Vitae.UserControl.TopHeader" %>
<style type="text/css">
    .backend-class {
        color: #fff;
    }
</style>

<div class="headTop">
    <div class="comContent">
        <p style="width: 300px; float: left">
            <a style="float: left;" href="#">
                <img src="/Content/images/logo.png"></a>
        </p>
        <%if (CurrentUser != null && CurrentUser.UserType != Presentation.Enum.UserType.Guest)
          { %>
        <p style="float: right">
            <a style="color: #fff; font-size: 23px; line-height: 60px;" href="#">你好，<%=CurrentUser.FullName %></a>
            <%if (Request.Path.ToLower().Contains("/manage/"))
              { %>
            <a href="<%=LkDataContext.AppConfig.HomePageLink %>" class="backend-class"><span>回到首页</span></a>
            <%}
              else
              { %>
            <asp:HyperLink ID="link2Backend" runat="server" CssClass="backend-class"><span>进入后台</span></asp:HyperLink>
            <%} %>
            <a href="/LogOffAccount.aspx" style="color: #fff;">注销</a>
        </p>
        <p style="width: 100px; float: right"></p>
        <p onmouseover="show()" onmouseout="hide()" style="width: 60px; height: 60px;">
            <%if (!String.IsNullOrEmpty(CurrentUser.Image))
              { %>
            <a href="#">
                <img src="images/u204.png" id="userimg" style="padding-top: 5px; width: 50px; height: 50px;">
            </a>
            <%} %>
        </p>
        <%}
          else
          {
              var returnUrl = HttpContext.Current.Request.Url.PathAndQuery;
              if (String.IsNullOrEmpty(returnUrl))
              {
                  returnUrl = "/Template/Default.aspx";
              }
              var loginUrl = "/Login.aspx?ReturnUrl=" + HttpUtility.UrlEncode(returnUrl);
              
        %>
        <p id="useroption" style="position: absolute; right: 460px; color: #fff; padding-left: 400px; font-size: 23px; line-height: 60px;">
            <a href="<%=loginUrl %>" style="color: #fff;">登入</a>
        </p>
        <%} %>

        <p></p>
        <p></p>
    </div>
</div>
