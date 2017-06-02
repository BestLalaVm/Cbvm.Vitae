<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/MasterDetail.master" AutoEventWireup="true"
    CodeBehind="StudentProjectDetail.aspx.cs" Inherits="Cbvm.Vitae.Manage.Student.StudentProjectDetail"  Theme="StudentManage"%>

<%@ Register Src="~/Manage/UserControl/EditorControl.ascx" TagName="EditorControl"
    TagPrefix="uc" %>
<%@ Register Src="~/Manage/UserControl/StudentAttachmentControl.ascx" TagName="StudentAttachmentControl"
    TagPrefix="uc" %>
<%@ Register Src="~/Manage/UserControl/GradeControl.ascx" TagName="GradeControl"
    TagPrefix="uc" %>
<%@ Register Src="~/Manage/UserControl/CommentList.ascx" TagName="CommentList" TagPrefix="uc" %>
<asp:Content ID="content" runat="server" ContentPlaceHolderID="head">
    <style type="text/css">
        table th
        {
            width: 100px;
        }
        .picker-item > div
        {
            float: left;
        }
        .picker-item .validate-time
        {
            margin-top: 4px;
        }
        .btn-select {
            width:100px !important;
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
        $(window).load(function () {
            parent.ResetSize();
        });
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceDetail" runat="server">
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
                    <table>
                        <tr>
                            <th>
                                <span class="required">项目名称</span>:
                            </th>
                            <td colspan="3">
                                <asp:TextBox ID="txt_Name_" runat="server" Width="486px" MaxLength="200" CssClass="edit-text"></asp:TextBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="rqvName" runat="server" ControlToValidate="txt_Name_"
                                    Display="Dynamic" ErrorMessage="<span class='error'><i class='msg-ico'></i><label>项目名称不能为空!</label></span>"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                <span class="required">开始时间从</span>:
                            </th>
                            <td colspan="3">
                                <div class="picker-item">
                                    <telerik:RadDatePicker ID="dtp_BeginTime_" runat="server" Width="176px">
                                        <DateInput ReadOnly="true" DateFormat="yyyy-MM-dd" runat="server" ID="dtiBeginDate">
                                        </DateInput>
                                    </telerik:RadDatePicker>
                                </div>
                                <div class="picker-item picker-label">
                                    <span>到</span>
                                </div>
                                <div class="picker-item">
                                    <telerik:RadDatePicker ID="dtp_EndTime_" runat="server">
                                        <DateInput ReadOnly="true" DateFormat="yyyy-MM-dd" ID="dtiEndDate" runat="server">
                                        </DateInput>
                                    </telerik:RadDatePicker>
                                </div>
                                <div class="clear">
                                </div>
                            </td>
                            <td>
                                <div class="validate-time">
                                    <asp:CompareValidator ID="vdtCompareData" ControlToValidate="dtp_BeginTime_" ControlToCompare="dtp_EndTime_"
                                        Operator="LessThanEqual" ErrorMessage="<span class='error'><i class='msg-ico'></i><label>开始时间不能大于结束时间!</label></span>"
                                        runat="server" Display="Dynamic"></asp:CompareValidator>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <th class="multiLine">
                                <span class="required">项目描述</span>:
                            </th>
                            <td colspan="3">
                                <asp:TextBox ID="txt_Description_" runat="server" TextMode="MultiLine" Width="486px" CssClass="multi-edit-text" MaxLength="2000"
                                    Height="70px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txt_Position_"
                                    Display="Dynamic" ErrorMessage="<span class='error'><i class='msg-ico'></i><label>职责内容不能为空!</label></span>"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                <span class="required">职责</span>:
                            </th>
                            <td colspan="3">
                                <asp:TextBox ID="txt_Position_" runat="server" Width="486px" MaxLength="50"  CssClass="edit-text"></asp:TextBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="rqvPosition" runat="server" ControlToValidate="txt_Position_"
                                    Display="Dynamic" ErrorMessage="<span class='error'><i class='msg-ico'></i><label>职责不能为空!</label></span>"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <th class="multiLine">
                                <span class="required">职责内容</span>:
                            </th>
                            <td colspan="3">
                                <asp:TextBox ID="txt_PositionDescrition_" runat="server" TextMode="MultiLine" Width="486px" MaxLength="200" CssClass="multi-edit-text"
                                    Height="70px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_Position_"
                                    Display="Dynamic" ErrorMessage="<span class='error'><i class='msg-ico'></i><label>职责内容不能为空!</label></span>"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <th class="multiLine">
                                <span class="required">项目自评</span>:
                            </th>
                            <td colspan="3">
                                <asp:TextBox ID="txt_Evaluate_" runat="server" TextMode="MultiLine" Width="486px" CssClass="multi-edit-text" MaxLength="500"
                                    Height="70px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_Evaluate_"
                                    Display="Dynamic" ErrorMessage="<span class='error'><i class='msg-ico'></i><label>项目自评不能为空!</label></span>"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <th class="multiLine">
                                <span class="required">团队描述</span>:
                            </th>
                            <td colspan="3">
                                <asp:TextBox ID="txt_TeamDescription_" runat="server" TextMode="MultiLine" Width="486px" CssClass="multi-edit-text" MaxLength="500"
                                    Height="70px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txt_TeamDescription_"
                                    Display="Dynamic" ErrorMessage="<span class='error'><i class='msg-ico'></i><label>团队描述不能为空!</label></span>"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                    <div class="caption">
                        <h4>
                            审核信息</h4>
                    </div>
                    <div class="container">
                        <table>
                            <tr>
                                <th>
                                    审核人:
                                </th>
                                <td style="width: 180px;" colspan="3">
                                    <asp:TextBox ID="txt_TeacherName_" runat="server" ReadOnly="true" Enabled="false"></asp:TextBox>
                                    <asp:HiddenField ID="hdf_TeacherNum_" runat="server" />
                                    <input type="button" value="选择审核老师" onclick="OpenTeacherList()" style="width: 120px; height: 25px;" class="btn-select" id="btnSelectTeacher" runat="server"/>
                                     <asp:RequiredFieldValidator ID="rqvTeacherNumber" runat="server" ControlToValidate="txt_TeacherName_"
                                    Display="Dynamic" ErrorMessage="<span class='error'><i class='msg-ico'></i><label>审核老师不能为空!</label></span>"></asp:RequiredFieldValidator>
                                </td>

                            </tr>
                            <tr>
                                                                <th>
                                    审核状态:
                                </th>
                                <td>
                                    <asp:Literal ID="ltl_VerifiedStatus_" runat="server"></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    实用性:
                                </th>
                                <td colspan="3">
                                    <table class="grade-container">
                                        <tr>
                                            <td>
                                                <uc:GradeControl ID="gdcUsableLevel" runat="server" ShowTitle="false" Enabled="false">
                                                </uc:GradeControl>
                                            </td>
                                            <th>
                                                技术性:
                                            </th>
                                            <td>
                                                <uc:GradeControl ID="gdcSkillLevel" runat="server" ShowTitle="false" Enabled="false">
                                                </uc:GradeControl>
                                            </td>
                                            <th>
                                                创意性:
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
                                <th class="multiLine">
                                    审核评论:
                                </th>
                                <td colspan="3">
                                    <div class="description">
                                        <asp:Label ID="lbl_EvaluateFromTeacher_" runat="server"></asp:Label>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <th class="multiLine">
                                    审核原因:
                                </th>
                                <td colspan="3">
                                    <div class="description">
                                        <asp:Label ID="lbl_VerifyStatusReason_" runat="server"></asp:Label>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                </th>
                                <td colspan="3">
                                    <asp:CheckBox ID="chk_IsOnline_" runat="server" Text="是否公开" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </telerik:RadPageView>
            <telerik:RadPageView ID="PageView2" runat="server">
                <div class="block tab-block attachment-block">
                    <uc:studentattachmentcontrol id="grdAttList" runat="server" onattachmentdeletecommmandeventhandler="grdAttList_AttachmentDeleteCommmandEventHandler"
                        onattachmentinsertcommmandeventhandler="grdAttList_AttachmentInsertCommmandEventHandler">
                    </uc:studentattachmentcontrol>
                </div>
            </telerik:RadPageView>
            <telerik:RadPageView ID="PageView3" runat="server">
                <uc:CommentList ID="cmtProjectCommentList" runat="server">
                </uc:CommentList>
            </telerik:RadPageView>
        </telerik:RadMultiPage>
    </div>
    <div class="block action">
        <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="保存"></asp:Button>
    </div>

    <script type="text/javascript">
        function OpenTeacherList() {
            window.parent.ShowIframeForm("选择审核老师", "../Common/SearchTeacherPage.aspx", "910px", "500px", {
                hideOverFlow: true,
                isShowFooter: true,
                closeCallBackHandler: function () {
                    // window.location.href = window.location.href;
                }
            });
        }

        function setSelectedTeacher(teacherNum, teacherName) {
            $("#<%=this.hdf_TeacherNum_.ClientID%>").val(teacherNum);
            $("#<%=this.txt_TeacherName_.ClientID%>").val(teacherName);
    }
</script>
</asp:Content>
