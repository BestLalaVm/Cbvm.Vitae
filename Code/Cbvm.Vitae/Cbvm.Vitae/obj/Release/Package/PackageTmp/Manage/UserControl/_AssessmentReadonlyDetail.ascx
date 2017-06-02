<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="_AssessmentReadonlyDetail.ascx.cs" Inherits="Cbvm.Vitae.Manage.UserControl._AssessmentReadonlyDetail" %>
<div class="func-block">
    <div class="caption">
        <h3><%=Caption %></h3>
    </div>
    <div class="block tab-block detail">
        <table>
            <tr>
                <th>
                    <span>标题</span>:
                </th>
                <td>
                    <asp:TextBox ID="txt_Title_" runat="server" Width="380px" MaxLength="200" CssClass="edit-text" Enabled="false"></asp:TextBox>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <th>开始时间从:</th>
                <td style="width: 300px;">
                    <div class="picker-item">
                        <telerik:RadDatePicker ID="dtp_BeginTime_" runat="server" Width="176px" Enabled="false">
                            <DateInput ReadOnly="true" DateFormat="yyyy-MM-dd" runat="server" ID="dtiBeginDate">
                            </DateInput>
                        </telerik:RadDatePicker>
                    </div>
                    <div class="picker-item picker-label">
                        <span>到</span>
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
                <th><span>学生当事人</span>:</th>
                <td>
                    <asp:TextBox ID="txt_StudentName_" runat="server" ReadOnly="true" Enabled="false"></asp:TextBox>
                </td>
                <td></td>
            </tr>
            <tr>
                <th class="multiLine">内容:
                </th>
                <td colspan="2">
                   <asp:Literal ID="ltl_Description_" runat="server"></asp:Literal>
                </td>
            </tr>
        </table>
    </div>
</div>