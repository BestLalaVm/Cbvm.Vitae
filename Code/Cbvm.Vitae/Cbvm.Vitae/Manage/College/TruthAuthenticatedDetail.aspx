<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/MasterDetail.master" AutoEventWireup="true" CodeBehind="TruthAuthenticatedDetail.aspx.cs" Inherits="Cbvm.Vitae.Manage.College.TruthAuthenticatedDetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPlaceDetail" runat="server">
    <div class="func-block">
        <telerik:RadTabStrip ID="radTabStrip" runat="server" OnClientTabSelected="TabSelected" MultiPageID="radMultipage">
            <Tabs>
                <telerik:RadTab Text="学生信息真实性验证" Value="0" runat="server" Selected="true">
                </telerik:RadTab>
            </Tabs>
        </telerik:RadTabStrip>
        <telerik:RadMultiPage ID="radMultipage" runat="server">
            <telerik:RadPageView ID="PageView1" runat="server" Selected="true">
                <div class="block tab-block">
                    <div class="container">
                        <table>
                            <tr>
                                <th>
                                    <span class="required">学生姓名</span>:
                                </th>
                                <td style="width: 300px;">
                                    <asp:HyperLink ID="lnkStudentName" runat="server" Target="_blank"></asp:HyperLink>
                                </td>
                            </tr>
<%--                            <tr>
                                <th>
                                    <span class="required">学生所属系别</span>:
                                </th>
                                <td style="width: 300px;">
                                    <asp:TextBox ID="txt_DepartName_" runat="server" Width="380px" MaxLength="50" Enabled="false"></asp:TextBox>
                                </td>

                            </tr>--%>
                            <tr>
                                <th>
                                    <span class="required">学生所属专业</span>:
                                </th>
                                <td style="width: 300px;">
                                    <asp:TextBox ID="txt_MarjorName_" runat="server" Width="380px" MaxLength="50" Enabled="false"></asp:TextBox>
                                </td>

                            </tr>
                            <tr>
                                <th>
                                    <span class="required">学生学号</span>:
                                </th>
                                <td style="width: 300px;">
                                    <asp:TextBox ID="txt_StudentNum_" runat="server" Width="380px" MaxLength="50" Enabled="false"></asp:TextBox>
                                </td>

                            </tr>
                            <tr>
                                <th>
                                    <span class="required">请求验证企业</span>:
                                </th>
                                <td style="width: 300px;">
                                    <asp:HyperLink ID="lnkEnterpriseName" runat="server" Target="_blank"></asp:HyperLink>
                                </td>
                            </tr>
                            <tr>
                                <th>请求验证时间:
                                </th>
                                <td style="width: 300px;">
                                    <div class="picker-item">
                                        <telerik:RadDatePicker ID="dtp_RequestDate_" runat="server" Width="176px" Enabled="false">
                                            <DateInput ReadOnly="true" DateFormat="yyyy-MM-dd" runat="server" ID="dtiBeginDate">
                                            </DateInput>
                                        </telerik:RadDatePicker>
                                    </div>
                                </td>
                            </tr>
                            <tr id="trVerifyStatrus" runat="server">
                                <th style="width: 100px">信息是否真实有效?:
                                </th>
                                <td>
                                    <asp:RadioButtonList ID="rdoVerify" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Text="是的,真实的" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="不是的" Value="1"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr id="trVerifyComment" runat="server">
                                <th class="multiLine">审核评论:
                                </th>
                                <td colspan="2">
                                    <asp:TextBox ID="txt_AuthenticatedComment_" runat="server" TextMode="MultiLine" Width="476px" CssClass="multi-edit-text" MaxLength="500"
                                        Height="100px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="block action">
                    <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click"
                        Text="保存"></asp:Button>
                </div>
            </telerik:RadPageView>
        </telerik:RadMultiPage>
    </div>
</asp:Content>
