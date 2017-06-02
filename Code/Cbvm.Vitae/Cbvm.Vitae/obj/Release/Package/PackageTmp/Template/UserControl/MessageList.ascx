<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MessageList.ascx.cs"
    Inherits="Cbvm.Vitae.Template.UserControl.MessageList" %>
<%@ Register Src="UserNavigateControl.ascx" TagName="UserNavigateControl" TagPrefix="uc" %>
<%@ Register Assembly="CustomControl" Namespace="CustomControl" TagPrefix="custom" %>

<div class="message-list list widget">
    <div class="bar">
        <div class="caption">
            <uc:UserNavigateControl ID="ucNaviageControl" runat="server"></uc:UserNavigateControl>
        </div>
    </div>
    <div class="container">
        <asp:Repeater ID="rptDepartMsg" runat="server">
            <HeaderTemplate>
                <ul>
            </HeaderTemplate>
            <ItemTemplate>
                <li>
                    <div class="list-item">
                        <div class="left">
                            <a href='<%#Eval("Url") %>' title='<%#Eval("Tooltip") %>'>
                                <%#Eval("Title")%></a>
                        </div>
                        <div class="right">
                            <%#Eval("LastUpdateTime")%>
                        </div>
                        <br class="clear" />
                    </div>
                </li>
            </ItemTemplate>
            <FooterTemplate>
                </ul>
            </FooterTemplate>
        </asp:Repeater>
        <div class="pager_block">
            <custom:CustomPager ID="CustomPager" runat="server" PageCountPerPage="20" OnRepeaterDataItemPropertying="CustomPager_RepeaterDataItemPropertying">
                <headertemplate>
                    <ul class="pager">
                </headertemplate>
                <itemtemplate>
                   <li class='<%#DataBinder.Eval(((RepeaterItem)Container).DataItem,"CssClass")%>'> 
                     <a href='<%#DataBinder.Eval(((RepeaterItem)Container).DataItem,"Url")%>' class='<%#DataBinder.Eval(((RepeaterItem)Container).DataItem,"CssClass")%>' >
                       <div class="page-item"><%#DataBinder.Eval(((RepeaterItem)Container).DataItem,"Text")%></div>
                       <div class="clear"></div>
                     </a>
                   </li>
                 </itemtemplate>
                <footertemplate>
                   <br class="clear" />
                    </ul>
                </footertemplate>
            </custom:CustomPager>
        </div>
        <div id="divMsg" runat="server" visible="false" class="empty-container">
            没有任何记录!
        </div>
    </div>
</div>
