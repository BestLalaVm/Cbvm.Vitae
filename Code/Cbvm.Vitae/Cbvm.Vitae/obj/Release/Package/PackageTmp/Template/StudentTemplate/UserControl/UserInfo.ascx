<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserInfo.ascx.cs" Inherits="Cbvm.Vitae.Template.StudentTemplate.UserControl.UserInfo" %>
<div class="user-info widget">
    <div class="bar">
        <div class="caption">
            <a href="<%=URL %>">基本资料</a>
        </div>
    </div>
    <div class="container">
        <div class="image">
            <asp:Image ID="imgPhoto" runat="server" />
        </div>
        <div class="basic-info">
            <div class="row-item">
                <span class="label">中文名:</span>
                <asp:Literal ID="ltlNameZh" runat="server"></asp:Literal>
            </div>
            <% if (!String.IsNullOrEmpty(StudentInfo.NameEn))
               { %>
            <div class="row-item">
                <span class="label">英文名:</span>
                <asp:Literal ID="ltlNameEn" runat="server"></asp:Literal>
            </div>
            <% } %>
            <div class="row-item">
                <span class="label">访问数:</span>
                <asp:Literal ID="ltlVisitedCount" runat="server"></asp:Literal>次
            </div>
            <div class="row-item">
                <asp:HyperLink runat="server" ID="lnkDetail">
                    <span>查看个人其他信息</span>
                </asp:HyperLink>
            </div>
        </div>
        <asp:PlaceHolder ID="phPublish" runat="server">
            <div class="personal-info">
                <div class="row-caption"><span class="label">系别<asp:Literal ID="ltlPeriod" runat="server"></asp:Literal></span></div>
                <div class="row-item">
                    <asp:Literal ID="ltlDepartName" runat="server"></asp:Literal>
                </div>
                <div class="row-caption"><span class="label">专业与班级</span></div>
                <div class="row-item">
                    <asp:Literal ID="ltlMarjorClass" runat="server"></asp:Literal>
                </div>
                <% if (!String.IsNullOrEmpty(StudentInfo.NativePlace))
                   { %>
                <div class="row-caption"><span class="label">籍贯</span></div>
                <div class="row-item">
                    <asp:Literal ID="ltlNativePlace" runat="server"></asp:Literal>
                </div>
                <% } %>
                <div class="row-caption"><span class="label">政治面貌</span></div>
                <div class="row-item">
                    <asp:Literal ID="ltlPolitics" runat="server"></asp:Literal>
                </div>
                <% if (!String.IsNullOrEmpty(StudentInfo.Tall))
                   { %>
                <div class="row-caption"><span class="label">身高</span></div>
                <div class="row-item">
                    <asp:Literal ID="ltlTall" runat="server"></asp:Literal>
                </div>
                <% } %>
                <% if (StudentInfo.Birthday.HasValue)
                   { %>
                <div class="row-caption"><span class="label">生日</span></div>
                <div class="row-item">
                    <asp:Literal ID="ltlBirthday" runat="server"></asp:Literal>
                </div>
                <% } %>
                <% if (!String.IsNullOrEmpty(StudentInfo.WebSite))
                   { %>
                <div class="row-caption"><span class="label">个人主页</span></div>
                <div class="row-item">
                    <asp:HyperLink ID="linkWebSite" runat="server" Target="_blank"></asp:HyperLink>
                </div>
                <% } %>
            </div>
        </asp:PlaceHolder>
    </div>
</div>
