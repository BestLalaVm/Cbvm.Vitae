<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/MasterDetail.master" AutoEventWireup="true"
    CodeBehind="PublishActivityDetail.aspx.cs" Inherits="Cbvm.Vitae.Manage.Teacher.PublishActivityDetail" %>

<%@ Register Src="~/Manage/UserControl/EditorControl.ascx" TagName="EditorControl"
    TagPrefix="uc" %>
<%@ Register Src="~/Manage/UserControl/StudentAttachmentControl.ascx" TagName="StudentAttachmentControl"
    TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .picker-item > div
        {
            display: inline-block;
        }
        .body-content .main .wrap-container .wrap-right .action
        {
            padding-right: 35px;
        }
        .body-content .main table th
        {
            width: 100px;
            padding-right: 10px;
        }
    </style>
    <script type="text/javascript">

        $(window).load(function () {
            parent.ResetSize();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPlaceDetail" runat="server">
    <div class="func-block">
        <telerik:RadTabStrip ID="radTabs" runat="server" MultiPageID="radMultiPage" Skin="Telerik"
            SelectedIndex="0">
            <Tabs>
                <telerik:RadTab Text="活动基本信息" Value="0">
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
                                类型:
                            </th>
                            <td colspan="3">
                                <asp:DropDownList ID="drp_ActivityType_" runat="server" Width="172px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <th class="required">
                                活动时间从:
                            </th>
                            <td colspan="3">
                                <div class="picker-item">
                                    <telerik:RadDateTimePicker ID="dtp_BeginTime_" runat="server" Width="176px">
                                        <DateInput ReadOnly="true" DateFormat="yyyy-MM-dd H:M:ss" runat="server" ID="dtiBeginDate">
                                        </DateInput>
                                    </telerik:RadDateTimePicker>
                                </div>
                                <div class="picker-item picker-label">
                                    <span>到</span>
                                </div>
                                <div class="picker-item">
                                    <telerik:RadDateTimePicker ID="dtp_EndTime_" runat="server">
                                        <DateInput ReadOnly="true" DateFormat="yyyy-MM-dd H:M:ss" ID="dtiEndDate" runat="server">
                                        </DateInput>
                                    </telerik:RadDateTimePicker>
                                </div>
                                <div class="clear">
                                </div>
                            </td>
                            <td>
                                 <asp:RequiredFieldValidator ID="vdtStartDate" runat="server" ControlToValidate="dtp_BeginTime_"
                                        Display="Dynamic" ErrorMessage="<span class='error'><i class='msg-ico'></i><label>开始时间不能为空!</label></span>"></asp:RequiredFieldValidator>
                                     <asp:RequiredFieldValidator ID="vdtEndDate" runat="server" ControlToValidate="dtp_EndTime_"
                                        Display="Dynamic" ErrorMessage="<span class='error'><i class='msg-ico'></i><label>结束时间不能为空!</label></span>"></asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="vdtCompareData" ControlToValidate="dtp_BeginTime_" ControlToCompare="dtp_EndTime_"
                                    Operator="LessThanEqual" ErrorMessage="<span class='error'><i class='msg-ico'></i><label>开始时间不能大于结束时间!</label></span>"
                                    runat="server" Display="Dynamic"></asp:CompareValidator>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                <span class="required">主题</span>:
                            </th>
                            <td colspan="3">
                                <asp:TextBox ID="txt_Title_" runat="server" Width="380px" MaxLength="50" CssClass="edit-text"></asp:TextBox>
                            </td>
                            <td style="width: 380px;">
                                <asp:RequiredFieldValidator ID="rvcTitle" runat="server" ControlToValidate="txt_Title_"
                                    Display="Dynamic" ErrorMessage="<span class='error'><i class='msg-ico'></i><label>主题不能为空!</label></span>"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                地址:
                            </th>
                            <td colspan="3">
                                <asp:TextBox ID="txt_Address_" runat="server" Width="380px" CssClass="edit-text"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th class="multiLine">
                                内容:
                            </th>
                            <td colspan="4">
                                <uc:EditorControl id="txt_Content_" runat="server" editorheight="350" editorwidth="760" />
                            </td>
                        </tr>
                        <tr>
                            <th>
                            </th>
                            <td>
                                <asp:CheckBox ID="chkIsOnline" runat="server" Text=" 公开" />
                            </td>
                        </tr>
                    </table>
                </div>
            </telerik:RadPageView>
            <telerik:RadPageView ID="PageView2" runat="server">
                <div class="block tab-block attachment-block">
                    <uc:StudentAttachmentControl ID="grdAttList" runat="server" OnAttachmentDeleteCommmandEventHandler="grdAttList_AttachmentDeleteCommmandEventHandler"
                        OnAttachmentInsertCommmandEventHandler="grdAttList_AttachmentInsertCommmandEventHandler">
                    </uc:StudentAttachmentControl>
                </div>
            </telerik:RadPageView>
        </telerik:RadMultiPage>
        <div class="block action">
            <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="保存"></asp:Button>
        </div>
    </div>
</asp:Content>
