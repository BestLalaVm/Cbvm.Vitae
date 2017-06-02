<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProjectList.ascx.cs"
    Inherits="Cbvm.Vitae.Template.StudentTemplate.UserControl.ProjectList" %>
<%@ Register Src="~/Template/UserControl/CommonSearchWidget.ascx" TagName="CommonSearchWidget"
    TagPrefix="uc" %>
<%--<div class="widget widget-list project-list">
    <div class="bar">
        <div class="caption">
            个人项目列表</div>
    </div>
    <div class="container">
        <uc:CommonSearchWidget ID="cmSearchWidget" runat="server" OnSearchClicked="CommonSearchWidget_SearchClicked">
        </uc:CommonSearchWidget>
        <asp:Repeater ID="rptProject" runat="server">
            <HeaderTemplate>
                <ul>
            </HeaderTemplate>
            <ItemTemplate>
                <li>
                    <div class="list-item">
                        <a href='<%#Eval("Url") %>'>
                            <%#Eval("Name")%></a>
                    </div>
                </li>
            </ItemTemplate>
            <FooterTemplate>
                <br class="clear" />
                </ul>
            </FooterTemplate>
        </asp:Repeater>
    </div>
</div>--%>

<div class="qylbListTit">
    <div class="qylbTitName"><a style="float: left;" href="#">项目列表</a></div>
</div>
<asp:Repeater ID="rptProject" runat="server">
    <ItemTemplate>
        <%# (Container.ItemIndex%3==0)?"<div class='qylbList'>":"" %>
        <div class="qylbListDiv"><a href='<%#Eval("Url") %>'><%#Eval("Name")%></a></div>
        <%# (Container.ItemIndex%3==2)?"</div>":"" %>
    </ItemTemplate>
    <FooterTemplate>
        <%# (Container.ItemIndex%3!=2)?"</div>":"" %>
    </FooterTemplate>
</asp:Repeater>
<div class="empty-container qylbList">
    <asp:Literal runat="server" ID="ltlEmptyMessage"></asp:Literal>
</div>