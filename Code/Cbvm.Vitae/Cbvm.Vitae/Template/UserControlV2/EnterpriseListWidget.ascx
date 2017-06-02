<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EnterpriseListWidget.ascx.cs" Inherits="Cbvm.Vitae.Template.UserControlV2.EnterpriseListWidget" %>
<%@ Register Assembly="CustomControl" Namespace="CustomControl" TagPrefix="custom" %>

<div class="qylbContent clearfix">
    <div class="qylbListTit">
        <div class="qylbTitName"><a href="#">企业列表</a></div>
    </div>
    <div class="gridlist">
        <asp:Repeater ID="rptEnterprise" runat="server">
            <ItemTemplate>
                <%# (Container.ItemIndex%3==0)?"<div class='qylbList'>":"" %>
                <div class="qylbListContent"><a href="<%#Eval("Url") %>" title="<%#Eval("Tooltip") %>"><%#Eval("Name")%></a></div>
                <%# (Container.ItemIndex%3==2)?"</div>":"" %>
            </ItemTemplate>
            <FooterTemplate>
                <%# (Container.ItemIndex%3!=2)?"</div>":"" %>
            </FooterTemplate>
        </asp:Repeater>
 <%--       <%= (IsNeedDivClosed)?"</div>":"" %>--%>
        <br class="clear" />
    </div>
    <div class="page">
        <custom:CustomPager ID="CustomPager" runat="server" PageCountPerPage="20" OnRepeaterDataItemPropertying="CustomPager_RepeaterDataItemPropertying">
            <headertemplate>
        <div class="pageTopLast">
            <a href="#">
                <img src="/content/images/pageTopButton.jpg"/>
            </a>
        </div>
        <div class="pageUpDown">
            <a href="#">上一页</a>
        </div>
        </headertemplate>
            <itemtemplate>
                    <div class="pageNum"><%#DataBinder.Eval(((RepeaterItem)Container).DataItem,"Text")%></div>
                 </itemtemplate>
            <footertemplate>
        <div class="pageUpDown">
            <a href="#">下一页</a>
        </div>
        <div class="pageTopLast">
            <a href="#">
                <img src="/content/images/pageLastButton.jpg"/>
            </a>
        </div>
         </footertemplate>
        </custom:CustomPager>
        <br class="clear" />
    </div>
</div>
