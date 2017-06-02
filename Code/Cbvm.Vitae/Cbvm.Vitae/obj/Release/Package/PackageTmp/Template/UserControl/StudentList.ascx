<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StudentList.ascx.cs"
    Inherits="Cbvm.Vitae.Template.UserControl.StudentList" %>
<%@ Register Assembly="CustomControl" Namespace="CustomControl" TagPrefix="custom" %>

<%@ Register Src="UserNavigateControl.ascx" TagName="UserNavigateControl" TagPrefix="uc" %>
<div class="student-list list widget">
    <div class="bar">
        <div class="caption">
            <uc:UserNavigateControl ID="ucNaviageControl" runat="server"></uc:UserNavigateControl>
            <div class="advance-search"><asp:HyperLink runat="server" ID="lkAdvanceSearch">高级搜索</asp:HyperLink></div>
        </div>
    </div>
    <div class="container">
        <asp:Repeater ID="rptStudent" runat="server">
            <HeaderTemplate>
                <ul class="data-list">
            </HeaderTemplate>
            <ItemTemplate>
                <li>
                    <div class="data-item">
                        <table>
                            <tr>
                                <td>
                                    <div class="image">
                                        <a href='<%#Eval("Url") %>' title='查看明细'>
                                            <img src='<%#Eval("Photo") %>' /></a>
                                    </div>
                                </td>
                                <td class="user-content">
                                    <table>
                                        <tr>
                                            <th>姓名
                                            </th>
                                            <td class="student-name">
                                                <%#Eval("NameZh") %>
                                            </td>
                                            <td>
                                                <a href='<%#Eval("Url") %>' title='查看明细'>查看</a>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th>性别
                                            </th>
                                            <td colspan="2">
                                                <%#Eval("Sex") %>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th>所属系别
                                            </th>
                                            <td colspan="2">
                                                <%#Eval("DepartName") %>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th>所属专业
                                            </th>
                                            <td colspan="2">
                                                <%#Eval("MarjorName") %>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th>个人主页
                                            </th>
                                            <td colspan="3">
                                                <%#!String.IsNullOrEmpty((string) Eval("WebSite"))? String.Format("<a href='{0}' target='_blank'>查看个人主页</a>", (Eval("WebSite").ToString().ToLower().StartsWith("http")?Eval("WebSite").ToString():"http://"+Eval("WebSite").ToString())):""%>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
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
