<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ArticleTop20ListWidget.ascx.cs"
    Inherits="Cbvm.Vitae.Template.UserControl.ArticleTop20ListWidget" %>
<div class="message-list widget-list show-widget widget">
    <div class="bar">
        <div class="caption">
            最新新闻</div>
        <div class="more">
            <a href="../Template/MessageList.aspx" title="新闻列表"><span></span></a>
        </div>
        <div class="clear">
        </div>
    </div>
    <div class="container">
        <asp:Repeater ID="rptMessage" runat="server">
            <HeaderTemplate>
                <ul>
            </HeaderTemplate>
            <ItemTemplate>
                <li>
                    <div class="list-item">
                        <div class="left">
                            <%#Eval("Title")%></div>
                        <div class="right">
                            <%#Eval("LastUpdateTime")%>
                        </div>
                        <div class="clear">
                        </div>
                    </div>
                </li>
            </ItemTemplate>
            <FooterTemplate>
                </ul>
            </FooterTemplate>
        </asp:Repeater>
    </div>
</div>
