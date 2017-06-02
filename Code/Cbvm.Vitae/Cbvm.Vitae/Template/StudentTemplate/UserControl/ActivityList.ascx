<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ActivityList.ascx.cs"
    Inherits="Cbvm.Vitae.Template.StudentTemplate.UserControl.ActivityList" %>
<%@ Register Src="~/Template/UserControl/CommonSearchWidget.ascx" TagName="CommonSearchWidget"
    TagPrefix="uc" %>
<%--<div class="widget widget-list activity-list">
    <div class="bar">
        <div class="caption">
            参加活动列表</div>
    </div>
    <div class="container">--%>
<%--        <uc:CommonSearchWidget ID="cmSearchWidget" runat="server" OnSearchClicked="CommonSearchWidget_SearchClicked">
        </uc:CommonSearchWidget>--%>
   <%--     <asp:Repeater ID="rptActivity" runat="server">
            <HeaderTemplate>
                <ul>
            </HeaderTemplate>
            <ItemTemplate>
                <li class="list-item">
                    <div>
                        <a href='<%#Eval("Url") %>'>
                            <%#Eval("Title")%></a>
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
    <div class="qylbTitName"><a style="float: left;" href="#">参加活动列表</a></div>
</div>
<asp:Repeater ID="rptActivity" runat="server">
    <ItemTemplate>
        <%# (Container.ItemIndex%3==0)?"<div class='qylbList'>":"" %>
        <div class="qylbListDiv"><a href='<%#Eval("Url") %>'><%#Eval("Title")%></a></div>
        <%# (Container.ItemIndex%3==2)?"</div>":"" %>
    </ItemTemplate>
    <FooterTemplate>
        <%# (Container.ItemIndex%3!=2)?"</div>":"" %>
    </FooterTemplate>
</asp:Repeater>
<div class="empty-container qylbList">
    <asp:Literal runat="server" ID="ltlEmptyMessage"></asp:Literal>
</div>