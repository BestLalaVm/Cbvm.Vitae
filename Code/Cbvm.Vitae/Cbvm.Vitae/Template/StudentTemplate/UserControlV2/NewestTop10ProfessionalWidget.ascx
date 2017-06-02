<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewestTop10ProfessionalWidget.ascx.cs" Inherits="Cbvm.Vitae.Template.StudentTemplate.UserControlV2.NewestTop10ProfessionalWidget" %>
<div class="stu-detail clearfix style-1">
    <h4><span>最新技能</span></h4>
    <asp:Repeater ID="rptProfessional" runat="server">
        <ItemTemplate>
                <p>
                    <a href='<%#Eval("Url") %>'>
                        <%#Eval("Name")%></a>
                </p>
        </ItemTemplate>
    </asp:Repeater>
    <div class="empty-container">
        <asp:Literal runat="server" ID="ltlEmptyMessage"></asp:Literal>
    </div>
</div>
