<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/MasterDetail.master" AutoEventWireup="true" CodeBehind="CareerPlanDetail.aspx.cs" Inherits="Cbvm.Vitae.Manage.Student.CareerPlanDetail" %>
    <%@ Register Src="~/Manage/UserControl/EditorControl.ascx" TagName="EditorControl"
    TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPlaceDetail" runat="server">

<div class="func-block">
    <div class="caption">
        <h3>职业规划明细</h3>
    </div>
    <div class="block tab-block detail">
        <table>
            <tr>
                <th>
                    <span class="required">规划标题</span>:
                </th>
                <td>
                    <asp:TextBox ID="txt_Title_" runat="server" Width="380px" MaxLength="200" CssClass="edit-text"></asp:TextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="rqvName" runat="server" ControlToValidate="txt_Title_"
                        Display="Dynamic" ErrorMessage="<span class='error'><i class='msg-ico'></i><label>规划标题不能为空!</label></span>"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <th class="required">开始时间从:
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
                        Operator="LessThanEqual" ErrorMessage="<span class='error'><i class='msg-ico'></i><label>开始时间不能大于结束时间!</label></span>"
                        runat="server" Display="Dynamic"></asp:CompareValidator>
                </td>
            </tr>

            <tr>
                <th></th>
                <td colspan="2">
                    <asp:RequiredFieldValidator ID="rqvBeginTime" runat="server" ControlToValidate="dtp_BeginTime_"
                        Display="Dynamic" ErrorMessage="<span class='error'><i class='msg-ico'></i><label>开始时间不能为空!</label></span>"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="dtp_EndTime_"
                        Display="Dynamic" ErrorMessage="<span class='error'><i class='msg-ico'></i><label>结束时间不能为空!</label></span>"></asp:RequiredFieldValidator>
                </td>
            </tr>
                        <tr>
                <td></td>
                <td colspan="2"><asp:CheckBox ID="chk_IsOnline_" runat="server" Text="是否发布" /></td>
            </tr>
                        <tr>
                <td></td>
                <td colspan="2"><asp:CheckBox ID="chk_IsImplemented_" runat="server" Text="是否已实现" /></td>
            </tr>
            <tr>
                <th class="multiLine">内容:
                </th>
                <td colspan="2">
                    <uc:EditorControl ID="txt_Content_" runat="server" EditorHeight="400" EditorWidth="720" />
                </td>
            </tr>
        </table>
    </div>
</div>
<div class="block action">
    <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="保存"></asp:Button>
</div>

</asp:Content>
