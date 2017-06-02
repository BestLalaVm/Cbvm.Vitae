<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/MasterDetail.master" AutoEventWireup="true" CodeBehind="StudentVoluntaryServiceDetail.aspx.cs" Inherits="Cbvm.Vitae.Manage.Student.StudentVoluntaryServiceDetail" %>
<%@ Register Src="~/Manage/UserControl/EditorControl.ascx" TagName="EditorControl"
    TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPlaceDetail" runat="server">
    <div class="func-block">
        <telerik:RadTabStrip ID="radTabStrip" runat="server" OnClientTabSelected="TabSelected" MultiPageID="radMultipage">
            <Tabs>
                <telerik:RadTab Text="自愿信息服务" Value="0" runat="server" Selected="true">
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
                                    <span class="required">标题</span>:
                                </th>
                                <td style="width:300px;">
                                    <asp:TextBox ID="txt_Title_" runat="server" Width="380px" MaxLength="50" CssClass="edit-text"></asp:TextBox>
                                </td>
                                <td>
                                  <asp:RequiredFieldValidator ID="rqvName" runat="server" ControlToValidate="txt_Title_"
                                    Display="Dynamic" ErrorMessage="<span class='error'><i class='msg-ico'></i><label>标题不能为空!</label></span>"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                                   <tr>
                                <th>时间从:
                                </th>
                                <td style="width: 300px;">
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
                                    <asp:CompareValidator ID="vdtCompareData" ControlToValidate="dtp_BeginTime_" ControlToCompare="dtp_EndTime_"
                                        Operator="LessThanEqual" ErrorMessage="<span class='error'><i class='msg-ico'></i><label>开始时间不能大于结束时间!</label></span>"
                                        runat="server" Display="Dynamic"></asp:CompareValidator>
                                </td>
                            </tr>                     
                            <tr>
                                <th class="multiLine">
                                    描述:
                                </th>
                                <td colspan="2">
                                    <uc:EditorControl ID="txt_Content_" runat="server" EditorHeight="460" EditorWidth="700">
                                    </uc:EditorControl>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                </th>
                                <td>
                                    <asp:CheckBox ID="chk_IsOnline_" runat="server" Text="是否公开" />
                                </td>
                                <td>
                                
                                </td>
                            </tr>
                            <tr id="trVerifyStatrus" runat="server">
                                <th style="width: 100px">审核状态:
                                </th>
                                <td>
                                    <asp:Literal ID="ltl_VerifiedStatus_" runat="server"></asp:Literal>
                                </td>
                            </tr>
                            <tr id="trVerifyComment" runat="server">
                                <th class="multiLine">审核评论:
                                </th>
                                <td colspan="2">
                                    <div class="description">
                                        <asp:Label ID="lbl_VerifiedComment_" runat="server"></asp:Label>
                                    </div>
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
