<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/MasterDetail.master" AutoEventWireup="true" CodeBehind="StudentExercitationDetail.aspx.cs"
    Inherits="Cbvm.Vitae.Manage.Student.StudentExercitationDetail" Theme="StudentManage" %>

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

        .body-content .main table.wrap-container .wrap-right .caption {
            margin-top: 10px;
        }

        .body-content .main table.wrap-container .wrap-right .container .description {
            width: 775px;
            max-width: 100%;
        }

        .body-content .main table.wrap-container tr td.wrap-right .detail .func-block .tab-block.detail {
            padding: 10px 15px !important;
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
        $(function () {
            $("input.edit-text").jqxInput({ height: 25 });
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
                <div class="block tab-block detail">
                    <table>
                        <tr>
                            <th>
                                <span class="required">实习单位</span>:
                            </th>
                            <td>
                                <asp:TextBox ID="txt_Title_" runat="server" Width="380px" MaxLength="200" CssClass="edit-text"></asp:TextBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="rqvName" runat="server" ControlToValidate="txt_Title_"
                                    Display="Dynamic" ErrorMessage="<span class='error'><i class='msg-ico'></i><label>实习单位不能为空!</label></span>"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <th class="required">实习时间从:
                            </th>
                            <td style="width: 300px;">
                                <div class="picker-item">
                                    <telerik:RadDatePicker ID="dtp_BeginTime_" runat="server" Width="176px">
                                        <DateInput ReadOnly="true" DateFormat="yyyy-MM-dd" runat="server" ID="dtiBeginDate">
                                        </DateInput>
                                    </telerik:RadDatePicker>
                                </div>
                                <div class="picker-item picker-label">
                                    <span class="required">到</span>
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
                                <asp:CompareValidator ID="vdtCompareData" ControlToValidate="dtp_BeginTime_" ControlToCompare="dtp_EndTime_"
                                    Operator="LessThanEqual" ErrorMessage="<span class='error'><i class='msg-ico'></i><label>实习时间不能大于结束时间!</label></span>"
                                    runat="server" Display="Dynamic"></asp:CompareValidator>
                            </td>
                        </tr>
                        <tr>
                            <th></th>
                            <td colspan="2">
                                 <asp:RequiredFieldValidator ID="rqvBeginTime" runat="server" ControlToValidate="dtp_BeginTime_"
                                    Display="Dynamic" ErrorMessage="<span class='error'><i class='msg-ico'></i><label>开始实习时间从不能为空!</label></span>"></asp:RequiredFieldValidator>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="dtp_EndTime_"
                                    Display="Dynamic" ErrorMessage="<span class='error'><i class='msg-ico'></i><label>结束实习时间从不能为空!</label></span>"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <th>单位地址:
                            </th>
                            <td>
                                <asp:TextBox ID="txt_Address_" runat="server" Width="380px" CssClass="edit-text" MaxLength="100"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th class="multiLine">实习内容:
                            </th>
                            <td colspan="2">
                                <uc:EditorControl id="txt_Content_" runat="server" editorheight="400" editorwidth="720" />
                            </td>
                        </tr>
                    </table>
                    <div class="caption">
                        <h4>审核信息</h4>
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
                                    <asp:CheckBox ID="chk_IsOnline_" runat="server" Text="是否公开" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </telerik:RadPageView>
            <telerik:RadPageView ID="PageView2" runat="server">
                <div class="block tab-block attachment-block">
                    <uc:StudentAttachmentControl ID="grdAttList" runat="server" OnAttachmentDeleteCommmandEventHandler="grdAttList_AttachmentDeleteCommmandEventHandler"
                        OnAttachmentInsertCommmandEventHandler="grdAttList_AttachmentInsertCommmandEventHandler">
                    </uc:StudentAttachmentControl>
                </div>
            </telerik:RadPageView>
            <telerik:RadPageView ID="PageView3" runat="server">
                <uc:CommentList ID="cmtActivityList" runat="server">
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
