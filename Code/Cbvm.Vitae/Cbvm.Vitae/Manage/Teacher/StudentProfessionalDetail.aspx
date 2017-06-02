<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/MasterDetail.master" AutoEventWireup="true" CodeBehind="StudentProfessionalDetail.aspx.cs" Inherits="Cbvm.Vitae.Manage.Teacher.StudentProfessionalDetail" %>

<%@ Register Src="../UserControl/AttachmentReadonlyControl.ascx" TagName="AttachmentReadonlyControl" TagPrefix="uc" %>

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
                                    <asp:TextBox ID="txt_Name_" runat="server" Width="380px" MaxLength="200" Enabled="false" ></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th><span class="required">获取时间:</span>
                                </th>
                                <td colspan="3">
                                    <telerik:RadDatePicker ID="dtp_ObtainTime_" runat="server" Enabled="false">
                                        <DateInput DateFormat="yyyy-MM-dd">
                                        </DateInput>
                                    </telerik:RadDatePicker>
                                </td>
                            </tr>
                            <tr>
                                <th class="multiLine">描述:
                                </th>
                                <td colspan="3">
                                    <asp:TextBox ID="txt_Description_" runat="server" TextMode="MultiLine" Width="380px" MaxLength="500"
                                        Height="100px" Enabled="false"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th></th>
                                <td colspan="3">
                                    <asp:CheckBox ID="chk_IsOnline_" runat="server" Text="是否公开" Enabled="false" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="caption">
                        <h4>审核信息</h4>
                    </div>
                    <table>
                        <tr>
                            <th>审核状态:
                            </th>
                            <td colspan="3">
                                <asp:RadioButtonList ID="rdoVerify" runat="server" RepeatDirection="Horizontal">
                                         <asp:ListItem Text="审核通过" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="审核未通过" Value="1"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <th class="multiLine">审核评论:
                            </th>
                            <td colspan="3">
                                <asp:TextBox ID="txt_EvaluateFromTeacher_" runat="server" TextMode="MultiLine" Width="474px" CssClass="edit-text"  MaxLength="500"
                                    Height="100px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th class="multiLine">审核原因:
                            </th>
                            <td colspan="3">
                                <asp:TextBox ID="txt_VerifyStatusReason_" runat="server" TextMode="MultiLine" Width="474px" CssClass="edit-text"  MaxLength="500"
                                    Height="100px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="caption">
                    <h4>相关附件</h4>
                    <div class="validate">
                        <span class="required">*
                            <asp:CustomValidator ID="cvcAttachment" runat="server" ClientValidationFunction="validateAttachment"
                                ErrorMessage="附加列表不能为空!" Display="Dynamic"></asp:CustomValidator></span>
                    </div>
                </div>
                    <uc:AttachmentReadonlyControl ID="attachmentList" runat="server"></uc:AttachmentReadonlyControl>
            </telerik:RadPageView>
        </telerik:RadMultiPage>
        <div class="block action">
            <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="保存" OnClientClick="BeforeSave();" />
        </div>
    </div>
</asp:Content>
