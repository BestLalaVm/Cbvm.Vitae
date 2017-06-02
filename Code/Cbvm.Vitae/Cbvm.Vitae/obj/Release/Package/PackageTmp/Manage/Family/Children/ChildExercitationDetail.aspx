<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/MasterDetail.master" AutoEventWireup="true" CodeBehind="ChildExercitationDetail.aspx.cs"
    Inherits="Cbvm.Vitae.Manage.Family.Children.ChildExercitationDetail" Theme="FamilyManage" %>


<%@ Register Src="~/Manage/UserControl/EditorControl.ascx" TagName="EditorControl"
    TagPrefix="uc" %>
<%@ Register Src="~/Manage/UserControl/StudentAttachmentControl.ascx" TagName="StudentAttachmentControl"
    TagPrefix="uc" %>
<%@ Register Src="~/Manage/UserControl/CommentList.ascx" TagName="CommentList" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .body-content .main .wrap-container .wrap-right .container table th {
            width: 80px;
        }

        .body-content .main table.wrap-container .wrap-right .container .description {
            width: 775px;
            max-width: 100%;
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
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPlaceDetail" runat="server">
    <div class="func-block">
        <telerik:RadTabStrip ID="radTabs" runat="server" MultiPageID="radMultiPage" Skin="Telerik"
            OnClientTabSelected="TabSelected" SelectedIndex="0">
            <Tabs>
                <telerik:RadTab Text="实习基本信息" Value="0">
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
                                <th>实习时间从:
                                </th>
                                <td style="width: 300px;">
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
                                <th>
                                    <span class="required">主题</span>:
                                </th>
                                <td>
                                    <asp:TextBox ID="txt_Title_" runat="server" Width="380px" MaxLength="50" CssClass="edit-text"></asp:TextBox>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <th>地址:
                                </th>
                                <td>
                                    <asp:TextBox ID="txt_Address_" runat="server" Width="380px" CssClass="edit-text"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th class="multiLine">内容:
                                </th>
                                <td colspan="2">
                                    <uc:EditorControl id="txt_Content_" runat="server" editorheight="400" editorwidth="700" EditorEnabled="False" />
                                </td>
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
                                <td style="width: 160px;">
                                  <asp:Literal runat="server" ID="ltl_TeacherNum_"></asp:Literal>
                                </td>
                                <th style="width: 100px">审核状态:
                                </th>
                                <td>
                                    <asp:Literal ID="ltl_VerifiedStatus_" runat="server"></asp:Literal>
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
                                <td colspan="4">
                                    <asp:CheckBox ID="chk_IsOnline_" runat="server" Text="是否公开" Enabled="False" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </telerik:RadPageView>
            <telerik:RadPageView ID="PageView2" runat="server">
                <div class="block tab-block attachment-block">
                    <uc:StudentAttachmentControl ID="grdAttList" runat="server" Enable="False">
                    </uc:StudentAttachmentControl>
                </div>
            </telerik:RadPageView>
            <telerik:RadPageView ID="PageView3" runat="server">
                <uc:CommentList ID="cmtActivityList" runat="server">
                </uc:CommentList>
            </telerik:RadPageView>
        </telerik:RadMultiPage>
    </div>
</asp:Content>
