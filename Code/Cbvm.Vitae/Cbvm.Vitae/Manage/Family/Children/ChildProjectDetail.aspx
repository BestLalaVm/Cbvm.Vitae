<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/MasterDetail.master" AutoEventWireup="true" CodeBehind="ChildProjectDetail.aspx.cs"
    Inherits="Cbvm.Vitae.Manage.Family.Children.ChildProjectDetail" Theme="FamilyManage" %>

<%@ Register Src="~/Manage/UserControl/EditorControl.ascx" TagName="EditorControl"
    TagPrefix="uc" %>
<%@ Register Src="~/Manage/UserControl/StudentAttachmentControl.ascx" TagName="StudentAttachmentControl"
    TagPrefix="uc" %>
<%@ Register Src="~/Manage/UserControl/GradeControl.ascx" TagName="GradeControl"
    TagPrefix="uc" %>
<%@ Register Src="~/Manage/UserControl/CommentList.ascx" TagName="CommentList" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .picker-item > div {
            float: left;
        }

        .picker-item .validate-time {
            margin-top: 4px;
        }

        .body-content .main {
            width: 850px !important;
        }

            .body-content .main table.wrap-container {
                height: auto !important;
            }
    </style>
    <script type="text/javascript">
        function TabSelectedEx(sender, args) {
            if (args.get_tab().get_value() == "2") {
                $(".action").hide();
            } else {
                $(".action").show();
            }
        }

        $(function () {
            $(window).load(function () {
                parent.ResetSize();
            });
            $(".edit-text").jqxInput({ height: 25, disabled: true });
            $(".multi-edit-text").jqxInput({ disabled: true });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPlaceDetail" runat="server">
    <div class="func-block">
        <telerik:RadTabStrip ID="radTabs" runat="server" MultiPageID="radMultiPage" Skin="Telerik"
            OnClientTabSelected="TabSelected" SelectedIndex="0">
            <Tabs>
                <telerik:RadTab Text="项目基本信息" Value="0">
                </telerik:RadTab>
                <telerik:RadTab Text="相关附件" Value="1">
                </telerik:RadTab>
                <telerik:RadTab Text="相关评论" Value="2">
                </telerik:RadTab>
            </Tabs>
        </telerik:RadTabStrip>
        <telerik:RadMultiPage ID="radMultiPage" runat="server" SelectedIndex="0">
            <telerik:RadPageView ID="PageView1" runat="server">
                <div class="block tab-block">
                    <div class="container">
                        <table>
                            <tr>
                                <th>
                                    <span>项目名称</span>:
                                </th>
                                <td colspan="3">
                                    <asp:TextBox ID="txt_Name_" runat="server" Width="484px" MaxLength="50" Enabled="False" CssClass="edit-text"></asp:TextBox>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <th>
                                    <span>开始时间从</span>:
                                </th>
                                <td colspan="3">
                                    <div class="picker-item">
                                        <asp:TextBox runat="server" ID="txt_BeginTime_" Enabled="False" CssClass="edit-text"></asp:TextBox>
                                    </div>
                                    <div class="picker-item picker-label">
                                        <span>到</span>
                                    </div>
                                    <div class="picker-item">
                                        <asp:TextBox runat="server" ID="txt_EndTime_" Enabled="False" CssClass="edit-text"></asp:TextBox>
                                    </div>
                                    <div class="clear">
                                    </div>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <th class="multiLine">
                                    <span>项目描述</span>:
                                </th>
                                <td colspan="3">
                                    <asp:TextBox ID="txt_Description_" runat="server" TextMode="MultiLine" Width="486px" Height="70px" Enabled="False" CssClass="multi-edit-text"></asp:TextBox>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <th>
                                    <span>职责</span>:
                                </th>
                                <td colspan="3">
                                    <asp:TextBox ID="txt_Position_" runat="server" Width="484px" MaxLength="50" Enabled="False" CssClass="multi-edit-text"></asp:TextBox>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <th class="multiLine">
                                    <span>职责内容</span>:
                                </th>
                                <td colspan="3">
                                    <asp:TextBox ID="txt_PositionDescrition_" runat="server" TextMode="MultiLine" Width="486px" Height="70px" Enabled="False" CssClass="multi-edit-text"></asp:TextBox>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <th class="multiLine">
                                    <span>项目自评</span>:
                                </th>
                                <td colspan="3">
                                    <asp:TextBox ID="txt_Evaluate_" runat="server" TextMode="MultiLine" Width="486px" Height="70px" Enabled="False" CssClass="multi-edit-text"></asp:TextBox>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <th class="multiLine">
                                    <span>团队描述</span>:
                                </th>
                                <td colspan="3">
                                    <asp:TextBox ID="txt_TeamDescription_" runat="server" TextMode="MultiLine" Width="486px" Enabled="False" CssClass="multi-edit-text"
                                        Height="70px"></asp:TextBox>
                                </td>
                                <td></td>
                            </tr>
                        </table>
                    </div>
                    <div class="caption">
                        <h4>审核信息</h4>
                    </div>
                    <div class="container">
                        <table>
                            <tr>
                                <th>审核人:
                                </th>
                                <td style="width: 180px;">
                                    <asp:Literal runat="server" ID="ltl_TeacherNum_"></asp:Literal>
                                </td>
                                <th>审核状态:
                                </th>
                                <td>
                                    <asp:Literal ID="ltl_VerifiedStatus_" runat="server"></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <th>实用性:
                                </th>
                                <td colspan="3">
                                    <table class="grade-container">
                                        <tr>
                                            <td>
                                                <uc:GradeControl ID="gdcUsableLevel" runat="server" ShowTitle="false" Enabled="false">
                                                </uc:GradeControl>
                                            </td>
                                            <th>技术性:
                                            </th>
                                            <td>
                                                <uc:GradeControl ID="gdcSkillLevel" runat="server" ShowTitle="false" Enabled="false">
                                                </uc:GradeControl>
                                            </td>
                                            <th>创意性:
                                            </th>
                                            <td>
                                                <uc:GradeControl ID="gdcDreativeLevel" runat="server" ShowTitle="false" Enabled="false">
                                                </uc:GradeControl>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <th class="multiLine">审核评论:
                                </th>
                                <td colspan="3">
                                    <div class="description">
                                        <asp:Label ID="lbl_EvaluateFromTeacher_" runat="server"></asp:Label>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <th class="multiLine">审核原因:
                                </th>
                                <td colspan="3">
                                    <div class="description">
                                        <asp:Label ID="lbl_VerifyStatusReason_" runat="server"></asp:Label>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <th></th>
                                <td colspan="3">
                                    <asp:CheckBox ID="chk_IsOnline_" runat="server" Text="是否公开" Enabled="False" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </telerik:RadPageView>
            <telerik:RadPageView ID="PageView2" runat="server">
                <div class="block tab-block attachment-block">
                    <uc:studentattachmentcontrol id="grdAttList" runat="server" Enable="False"></uc:studentattachmentcontrol>
                </div>
            </telerik:RadPageView>
            <telerik:RadPageView ID="PageView3" runat="server">
                <uc:CommentList ID="cmtProjectCommentList" runat="server">
                </uc:CommentList>
            </telerik:RadPageView>
        </telerik:RadMultiPage>
    </div>
</asp:Content>
