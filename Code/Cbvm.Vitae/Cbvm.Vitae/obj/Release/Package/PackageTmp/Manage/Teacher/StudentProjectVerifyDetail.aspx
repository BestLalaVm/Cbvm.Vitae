<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/MasterDetail.master" AutoEventWireup="true"
    CodeBehind="StudentProjectVerifyDetail.aspx.cs" Inherits="Cbvm.Vitae.Manage.Teacher.StudentProjectVerifyDetail" %>

<%@ Register Src="~/UserControl/GradeControl.ascx" TagName="GradeControl" TagPrefix="uc" %>
<%@ Register Src="~/Manage/UserControl/AttachmentReadonlyControl.ascx" TagName="AttachmentReadonlyControl"
    TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .verify table th
        {
            width: 81px;
        }
        .picker-item > div
        {
            float: left;
        }
        .picker-item .validate-time
        {
            margin-top: 4px;
        }
        .body-content .main .wrap-container .wrap-right table th
        {
            width: 120px;
        }
        .body-content .main .wrap-container .wrap-right table .grade-container th {
            width: 60px;
        }
        .body-content .main .wrap-container .wrap-right .action
        {
            padding-right: 240px;
        }
    </style>
        <script type="text/javascript">
            $(function () {
                $("input.edit-text").jqxInput({ height: 25, disabled: true });
                $(".multi-edit-text").jqxInput();
            });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPlaceDetail" runat="server">
    <div class="func-block">
        <telerik:RadTabStrip ID="radTabs" runat="server" MultiPageID="radMultiPage" Skin="Telerik"
            SelectedIndex="0">
            <Tabs>
                <telerik:RadTab Text="项目基本信息" Value="0">
                </telerik:RadTab>
                <telerik:RadTab Text="相关附件" Value="1">
                </telerik:RadTab>
            </Tabs>
        </telerik:RadTabStrip>
        <telerik:RadMultiPage ID="radMultiPage" runat="server" SelectedIndex="0">
            <telerik:RadPageView ID="PageView1" runat="server">
                <div class="block tab-block">
                    <table>
                        <tr>
                            <th>
                                项目名称:
                            </th>
                            <td colspan="3">
                                <asp:TextBox ID="txt_Name_" runat="server" Width="472px" MaxLength="50" Enabled="false" ></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                开始时间从:
                            </th>
                            <td colspan="3">
                                <div class="picker-item">
                                    <asp:TextBox ID="txtBeginTime" runat="server" Enabled="false"></asp:TextBox>
                                </div>
                                <div class="picker-item picker-label">
                                    <span>到</span>
                                </div>
                                <div class="picker-item">
                                    <asp:TextBox ID="txtEndTime" runat="server" Enabled="false" ></asp:TextBox>
                                </div>
                                <div class="clear">
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <th class="multiLine">
                                项目描述:
                            </th>
                            <td colspan="3">
                                <div class="description">
                                    <asp:Literal ID="ltl_Description_" runat="server"></asp:Literal>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <th class="multiLine">
                                职责:
                            </th>
                            <td colspan="3">
                                <div class="description">
                                    <asp:Literal ID="ltl_Position_" runat="server"></asp:Literal>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <th class="multiLine">
                                职责内容:
                            </th>
                            <td colspan="3">
                                <div class="description">
                                    <asp:Literal ID="ltl_PositionDescrition_" runat="server"></asp:Literal>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <th class="multiLine">
                                项目自评:
                            </th>
                            <td colspan="3">
                                <div class="description">
                                    <asp:Literal ID="ltl_Evaluate_" runat="server"></asp:Literal>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <th class="multiLine">
                                团队描述:
                            </th>
                            <td colspan="3">
                                <div class="description">
                                    <asp:Literal ID="ltl_TeamDescription_" runat="server"></asp:Literal>
                                </div>
                            </td>
                        </tr>
                    </table>
                    <div class="caption">
                        <h4>
                            审核信息</h4>
                    </div>
                    <table>
                        <tr>
                            <th>
                                审核状态:
                            </th>
                            <td colspan="3">
                                <asp:RadioButtonList ID="rdoVerify" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Text="审核通过" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="审核未通过" Value="1"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr class="grade-container">
                            <th>
                                实用性:
                            </th>
                            <td colspan="3">
                                <table>
                                    <tr>
                                        <td>
                                            <uc:GradeControl ID="gdcUsableLevel" runat="server" ShowTitle="false">
                                            </uc:GradeControl>
                                        </td>
                                        <th>
                                            技术性:
                                        </th>
                                        <td>
                                            <uc:GradeControl ID="gdcSkillLevel" runat="server" ShowTitle="false">
                                            </uc:GradeControl>
                                        </td>
                                        <th>
                                            创意性:
                                        </th>
                                        <td>
                                            <uc:GradeControl ID="gdcDreativeLevel" runat="server" ShowTitle="false">
                                            </uc:GradeControl>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <th class="multiLine">
                                审核评论:
                            </th>
                            <td colspan="3">
                                <asp:TextBox ID="txt_EvaluateFromTeacher_" runat="server" TextMode="MultiLine" Width="476px" CssClass="multi-edit-text"  MaxLength="500"
                                    Height="100px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th class="multiLine">
                                审核原因:
                            </th>
                            <td colspan="3">
                                <asp:TextBox ID="txt_VerifyStatusReason_" runat="server" TextMode="MultiLine" Width="476px" CssClass="multi-edit-text"  MaxLength="500"
                                    Height="100px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="block action">
                    <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="保存"></asp:Button>
                </div>
            </telerik:RadPageView>
            <telerik:RadPageView ID="PageView2" runat="server">
                <div class="block tab-block attachment-block">
                    <uc:AttachmentReadonlyControl ID="attachmentList" runat="server">
                    </uc:AttachmentReadonlyControl>
                </div>
            </telerik:RadPageView>
        </telerik:RadMultiPage>
    </div>
</asp:Content>
