<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="JobRequestDetail.ascx.cs" Inherits="Cbvm.Vitae.UserControl.JobRequestDetail" %>
<div class="enterprise-detail widget">
    <div class="bar">
        <div class="caption">
            <asp:Literal ID="ltlName" runat="server"></asp:Literal>
        </div>
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
        <div class="separate">
        </div>
        <div class="post-item">
            <table>
                <thead>
                    <tr>
                        <td colspan="4">
                            <div class="enterprise-head">
                                <div class="item-left">
                                    <div class="caption">
                                        <label>招聘职位:</label>
                                        <asp:Literal runat="server" ID="ltlJobName"></asp:Literal>(<asp:Literal runat="server" ID="ltlJobNum"></asp:Literal>)
                                    </div>
                                </div>
                                <div class="item-right">
                                    <div class="caption">
                                        <label>发布时间:</label><asp:Literal runat="server" ID="ltlJobCreateTime"></asp:Literal>
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
                        <asp:Literal runat="server" ID="ltlJobDateTimeScope"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <th>招聘部门:
                    </th>
                    <td colspan="3">
                        <asp:Literal runat="server" ID="ltlJobDepartName"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <th>联 系 人:
                    </th>
                    <td colspan="3">
                        <asp:Literal runat="server" ID="ltlJobContactName"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <th>联系电话:
                    </th>
                    <td style="width: 230px;">
                        <asp:Literal runat="server" ID="ltlJobTelephone"></asp:Literal>
                    </td>
                    <th>联系传真:
                    </th>
                    <td>
                        <asp:Literal runat="server" ID="ltlJobTax"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <th>通信地址:
                    </th>
                    <td colspan="3">
                        <asp:Literal runat="server" ID="ltlJobAddress"></asp:Literal>
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
                                <asp:Literal runat="server" ID="ltlJobEducation"></asp:Literal>
                            </td>
                            <th>性别要求:
                            </th>
                            <td>
                                <asp:Literal runat="server" ID="ltlJobSex"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <th>职位性质:
                            </th>
                            <td>
                                <asp:Literal runat="server" ID="ltlJobNature"></asp:Literal>
                            </td>
                            <th>招聘对象:
                            </th>
                            <td>
                                <asp:Literal runat="server" ID="ltlJobRecruitmentTargets"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <th>年龄要求:
                            </th>
                            <td colspan="3">
                                <asp:Literal runat="server" ID="ltlJobAgeScope"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <th>工作地点:
                            </th>
                            <td>
                                <asp:Literal runat="server" ID="ltlJobWorkPlace"></asp:Literal>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="content">
                    <div class="caption">
                        <strong>职位职责,职位要求,应聘方式(Job Responsibilities & Requirements)：</strong>
                    </div>
                    <div class="description">
                        <asp:Literal runat="server" ID="ltlJobDescription"></asp:Literal>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
