<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/MasterDetail.master" AutoEventWireup="true"
    CodeBehind="StudentProfessionalDetail.aspx.cs" Inherits="Cbvm.Vitae.Manage.Student.StudentProfessionalDetail" Theme="StudentManage" %>

<%@ Register Src="~/Manage/UserControl/EditorControl.ascx" TagName="EditorControl"
    TagPrefix="uc" %>
<%@ Register Src="~/Manage/UserControl/StudentAttachmentControl.ascx" TagName="StudentAttachmentControl"
    TagPrefix="uc" %>
<asp:Content ID="contentHead" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .body-content .main .wrap-container .attachment .fileLabel-Upload {
            margin-left: 30px;
        }

        .attachment-list {
            margin-top: 10px;
        }

        .body-content .main .wrap-container .wrap-right .detail .profession-detail table {
            width: auto;
        }

        .body-content .main .wrap-container .wrap-right .detail .caption h4, .body-content .main .wrap-container .wrap-right .detail .caption .validate {
            display: inline-block;
        }

        .body-content .main .wrap-container .wrap-right .detail .action {
            padding-right: 10px;
            margin-top: 10px;
        }

        html body .RadInput .riTextBox, html body .RadInputMgr {
            border-style: none !important;
            background: none !important;
        }

        label {
            line-height: 25px;
        }
                .btn-select {
            width:100px !important;
        }
    </style>
    <script type="text/javascript">
        function validateAttachment(sender, args) {
            var rgMasterTable = $(".rgMasterTable tbody");
            args.IsValid = rgMasterTable.children(".rgRow").length > 0;
        }
        function BeforeSave() {
            Page_ClientValidate("");
        }

        $(function() {
            $("input.edit-text").jqxInput({ height: 25 });
            $(".multi-edit-text").jqxInput();
            
            $(window).load(function() {
                parent.ResetSize();
            });
        });

        function BeforeUploadValidate() {
            //            return Page_ClientValidate("");
            return <%=rqvName.ClientID %>.isvalid;
        }

        function BeforeDeleteAttachmentItem() {
            //            return Page_ClientValidate("");
            return <%=rqvName.ClientID %>.isvalid;
        }
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceDetail" runat="server">
    <div class="func-block profession-detail">
        <telerik:RadTabStrip ID="radTabs" runat="server" MultiPageID="radMultiPage" Skin="Telerik"
            SelectedIndex="0">
            <Tabs>
                <telerik:RadTab Text="信息" Value="0">
                </telerik:RadTab>
            </Tabs>
        </telerik:RadTabStrip>
        <telerik:RadMultiPage ID="radMultiPage" runat="server" SelectedIndex="0">
            <telerik:RadPageView ID="PageView1" runat="server">
                <div class="block tab-block">
                    <div class="container">
                        <table>
                            <tr>
                                <th><span class="required"><%=NameTitle %>名称:*</span>
                                </th>
                                <td colspan="3">
                                    <asp:TextBox ID="txt_Name_" runat="server" Width="380px" MaxLength="200" CssClass="edit-text"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th></th>
                                <td>
                                    <asp:RequiredFieldValidator ID="rqvName" runat="server" ControlToValidate="txt_Name_"
                                        Display="Dynamic" ErrorMessage="<span class='error'><i class='msg-ico'></i><label>证书名称不能为空!</label></span>"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <th><span class="required">获取时间:</span>
                                </th>
                                <td colspan="3">
                                    <telerik:RadDatePicker ID="dtp_ObtainTime_" runat="server">
                                        <DateInput DateFormat="yyyy-MM-dd">
                                        </DateInput>
                                    </telerik:RadDatePicker>
                                </td>
                            </tr>
                            <tr>
                                <th></th>
                                <td>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="dtp_ObtainTime_"
                                        Display="Dynamic" ErrorMessage="<span class='error'><i class='msg-ico'></i><label>获取时间不能为空!</label></span>"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <th class="multiLine">描述:
                                </th>
                                <td colspan="3">
                                    <asp:TextBox ID="txt_Description_" runat="server" TextMode="MultiLine" Width="380px" CssClass="multi-edit-text" MaxLength="500"
                                        Height="100px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th></th>
                                <td colspan="3">
                                    <asp:CheckBox ID="chk_IsOnline_" runat="server" Text="是否公开" />
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
                        </table>
                    </div>
                </div>
                <div class="caption">
                    <h4>相关附件</h4>
                    <div class="validate">
                        <span class="required">*
                            <asp:CustomValidator ID="cvcAttachment" runat="server" ClientValidationFunction="validateAttachment"
                                ErrorMessage="附加列表不能为空!" Display="Dynamic"></asp:CustomValidator></span>
                    </div>
                </div>
                <uc:studentattachmentcontrol id="grdAttList" runat="server" onattachmentdeletecommmandeventhandler="grdAttList_AttachmentDeleteCommmandEventHandler"
                    onattachmentinsertcommmandeventhandler="grdAttList_AttachmentInsertCommmandEventHandler">
                </uc:studentattachmentcontrol>
            </telerik:RadPageView>
        </telerik:RadMultiPage>
        <div class="block action">
            <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="保存" OnClientClick="BeforeSave();" />
        </div>
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
