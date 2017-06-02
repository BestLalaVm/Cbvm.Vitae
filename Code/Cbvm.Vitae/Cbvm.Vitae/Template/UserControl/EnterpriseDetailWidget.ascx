<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EnterpriseDetailWidget.ascx.cs"
    Inherits="Cbvm.Vitae.Template.UserControl.EnterpriseDetailWidget" %>
<%@ Register Src="UserNavigateControl.ascx" TagName="UserNavigateControl" TagPrefix="uc" %>

<div class="enterprise-detail widget">
    <div class="bar">
        <div class="caption">
            <uc:usernavigatecontrol id="ucNaviageControl" runat="server"></uc:usernavigatecontrol>
        </div>
    </div>
    <div class="container">
        <asp:PlaceHolder runat="server" ID="phContainer">
            <div class="title">
                <h1>
                    <asp:Literal ID="ltlName" runat="server"></asp:Literal>
                </h1>
            </div>
            <div class="enterprise-basic">
                <table>
                    <thead>
                        <div class="caption">
                            <h3>单位简介
                            </h3>
                        </div>
                    </thead>
                    <tbody>
                        <tr>
                            <th>公司性质:
                            </th>
                            <td>
                                <asp:Literal ID="ltlEnterpriseTypeName" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <th>公司行业:
                            </th>
                            <td>
                                <asp:Literal ID="ltlIndustryName" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <th>公司规模:
                            </th>
                            <td>
                                <asp:Literal ID="ltlScopeID" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr class="description">
                            <td colspan="2">
                                <asp:Literal ID="ltlDescription" runat="server"></asp:Literal>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <table>
                    <thead>
                        <div class="caption">
                            <h3>联系方式
                            </h3>
                        </div>
                    </thead>
                    <tr>
                        <th>联系人:
                        </th>
                        <td>
                            <asp:Literal ID="ltlContactName" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <th>联系电话:
                        </th>
                        <td>
                            <asp:Literal ID="ltlTelephone" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <th>联系地址:
                        </th>
                        <td>
                            <asp:Literal ID="ltlAddress" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <th>电子邮件:
                        </th>
                        <td>
                            <asp:Literal ID="ltlEmail" runat="server"></asp:Literal>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="enterprise-postList">
                <asp:Repeater ID="rptPostList" runat="server" OnItemDataBound="rptPostList_ItemDataBound">
                    <ItemTemplate>
                        <div class="separate">
                        </div>
                        <div class="post-item" id="<%#Eval("Name") %>">
                            <table>
                                <thead>
                                    <tr>
                                        <td colspan="4">
                                            <div class="enterprise-head">
                                                <div class="item-left">
                                                    <div class="caption">
                                                        <label>招聘职位:</label><%#Eval("Name") %>(<%#Eval("Num")%>)
                                                    </div>
                                                </div>
                                                <div class="item-right">
                                                    <div class="caption">
                                                        <label>发布时间:</label><%#Eval("CreateTime") %>
                                                    </div>
                                                </div>
                                                <div class="clear">
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                </thead>
                                <tr>
                                    <th>招聘期限:
                                    </th>
                                    <td colspan="3">
                                        <%#Eval("DateTimeScope") %>
                                    </td>
                                </tr>
                                <tr>
                                    <th>招聘部门:
                                    </th>
                                    <td colspan="3">
                                        <%#Eval("DepartName")%>
                                    </td>
                                </tr>
                                <tr>
                                    <th>联 系 人:
                                    </th>
                                    <td colspan="3">
                                        <%#Eval("ContactName")%>
                                    </td>
                                </tr>
                                <tr>
                                    <th>联系电话:
                                    </th>
                                    <td style="width: 230px;">
                                        <%#Eval("Telephone")%>
                                    </td>
                                    <th>联系传真:
                                    </th>
                                    <td>
                                        <%#Eval("Tax")%>
                                    </td>
                                </tr>
                                <tr>
                                    <th>通信地址:
                                    </th>
                                    <td colspan="3">
                                        <%#Eval("Address")%>
                                    </td>
                                </tr>
                            </table>
                            <div class="block">
                                <div class="caption">
                                    职位基本要求
                                </div>
                                <div class="container">
                                    <table width="100%">
                                        <tr>
                                            <th>学历要求:
                                            </th>
                                            <td style="width: 250px">
                                                <%#Eval("Education")%>
                                            </td>
                                            <th>性别要求:
                                            </th>
                                            <td>
                                                <%#Eval("Sex")%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th>职位性质:
                                            </th>
                                            <td>
                                                <%#Eval("Nature")%>
                                            </td>
                                            <th>招聘对象:
                                            </th>
                                            <td>
                                                <%#Eval("RecruitmentTargets")%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th>年龄要求:
                                            </th>
                                            <td colspan="3">
                                                <%#Eval("AgeScope")%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th>工作地点:
                                            </th>
                                            <td>
                                                <%#Eval("WorkPlace")%>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="content">
                                    <div class="caption">
                                        <strong>职位职责,职位要求,应聘方式(Job Responsibilities & Requirements)：</strong>
                                    </div>
                                    <div class="description">
                                        <%#Eval("Description")%>
                                    </div>
                                </div>
                                <asp:PlaceHolder ID="phRequestJob" runat="server" Visible='<%#!(bool)Eval("IsExpried") %>'>
                                    <div class="job-request">
                                        <asp:LinkButton ID="btnRequestJob" runat="server" Text="[申请这个职位]" OnClick="btnRequestJob_Click"
                                            CommandArgument='<%#Eval("Code") %>'></asp:LinkButton>
                                    </div>
                                </asp:PlaceHolder>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </asp:PlaceHolder>
    </div>
</div>
