<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserInfo.ascx.cs" Inherits="Cbvm.Vitae.Template.StudentTemplate.UserControlV2.UserInfo" %>
<%@ Register Src="~/Template/StudentTemplate/UserControlV2/NewestTop10ProfessionalWidget.ascx" TagName="NewestTop10ProfessionalWidget" TagPrefix="uc1" %>

<style type="text/css">
    .stu-detail > img {
        max-height:90px;
        max-width:90px;
    }
</style>
<div class="stu-detail clearfix">
    <asp:Image ID="imgPhoto" runat="server" />
    <h2>
        <asp:HyperLink runat="server" ID="lnkDetail">
            <asp:Literal ID="ltlNameZh" runat="server"></asp:Literal>
        </asp:HyperLink></h2>
    <h3>
        <asp:Literal ID="ltlNameEn" runat="server"></asp:Literal></h3>
    <p class="school">
        <asp:Literal ID="ltlCollegeName" runat="server"></asp:Literal>
        <asp:Literal ID="ltlPeriod" runat="server"></asp:Literal>
    </p>
</div>
<div class="stu-detail clearfix">
    <ul>
        <li>
            <label>系别</label><asp:Literal ID="ltlDepartName" runat="server"></asp:Literal>
        </li>
        <li>
            <label>专业班级</label><asp:Literal ID="ltlMarjorClass" runat="server"></asp:Literal>
        </li>
        <asp:PlaceHolder ID="phPublish" runat="server">
            <% if (!String.IsNullOrEmpty(StudentInfo.NativePlace))
               { %>
            <li>
                <label>籍贯</label><asp:Literal ID="ltlNativePlace" runat="server"></asp:Literal>
            </li>
            <% } %>
            <li>
                <label>政治面貌</label><asp:Literal ID="ltlPolitics" runat="server"></asp:Literal>
            </li>
            <% if (!String.IsNullOrEmpty(StudentInfo.Tall))
               { %>
            <li>
                <label>身高</label><asp:Literal ID="ltlTall" runat="server"></asp:Literal>
            </li>
            <% } %>
            <% if (StudentInfo.Birthday.HasValue)
               { %>
            <li>
                <label>生日</label><asp:Literal ID="ltlBirthday" runat="server"></asp:Literal>
            </li>
            <% } %>
            <% if (!String.IsNullOrEmpty(StudentInfo.WebSite))
               { %>
            <li>
                <label>个人主页</label><asp:HyperLink ID="linkWebSite" runat="server" Target="_blank"></asp:HyperLink>
            </li>
            <% } %>
        </asp:PlaceHolder>
    </ul>

</div>
<uc1:NewestTop10ProfessionalWidget id="nwTopProfessional" runat="server"></uc1:NewestTop10ProfessionalWidget>