<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/MasterDetail.master" AutoEventWireup="true" CodeBehind="ChildCareerPlanDetail.aspx.cs" Inherits="Cbvm.Vitae.Manage.Family.Children.ChildCareerPlanDetail" %>
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
                    <asp:TextBox ID="txt_Title_" runat="server" Width="380px" MaxLength="200" CssClass="edit-text" Enabled="false"></asp:TextBox>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <th class="required">开始时间从:
                </th>
                <td style="width: 300px;">
                    <div class="picker-item">
                        <telerik:RadDatePicker ID="dtp_BeginTime_" runat="server" Width="176px" Enabled="false">
                            <DateInput ReadOnly="true" DateFormat="yyyy-MM-dd" runat="server" ID="dtiBeginDate">
                            </DateInput>
                        </telerik:RadDatePicker>
                    </div>
                    <div class="picker-item picker-label">
                        <span class="required">到</span>
                    </div>
                    <div class="picker-item">
                        <telerik:RadDatePicker ID="dtp_EndTime_" runat="server" Enabled="false">
                            <DateInput ReadOnly="true" DateFormat="yyyy-MM-dd" ID="dtiEndDate" runat="server">
                            </DateInput>
                        </telerik:RadDatePicker>
                    </div>
                    <div class="clear">
                    </div>
                </td>
                <td>

                </td>
            </tr>


                        <tr>
                <td></td>
                <td colspan="2"><asp:CheckBox ID="chk_IsOnline_" runat="server" Text="是否发布" /></td>
            </tr>
                        <tr>
                <td></td>
                <td colspan="2"><asp:CheckBox ID="chk_IsImplemented_" runat="server" Text="是否已实现" Enabled="false"/></td>
            </tr>
            <tr>
                <th class="multiLine">内容:
                </th>
                <td colspan="2">
                    <asp:Literal ID="ltlContent" runat="server"></asp:Literal>
                </td>
            </tr>
        </table>
    </div>
</div>

</asp:Content>
