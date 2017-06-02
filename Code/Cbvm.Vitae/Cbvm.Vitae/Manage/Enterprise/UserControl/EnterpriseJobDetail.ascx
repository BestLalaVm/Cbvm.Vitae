<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EnterpriseJobDetail.ascx.cs"
    Inherits="Cbvm.Vitae.Manage.Enterprise.UserControl.EnterpriseJobDetail" %>
<%@ Register Src="~/Manage/UserControl/EditorControl.ascx" TagName="EditorControl"
    TagPrefix="uc" %>
<div class="enterprise-job">
    <div class="head-bar">
        <div class="caption">
            <label>
                招聘职位:<span class="required">*</span></label>
            <asp:TextBox ID="txtName" runat="server" Width="400px" MaxLength="40"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rqvName" runat="server" ControlToValidate="txtName"
                ErrorMessage="<img src='/Image/invalid.gif' title='招聘职位不能为空!' />"></asp:RequiredFieldValidator>
        </div>
    </div>
    <div class="container">
        <table>
            <tr>
                <th>招聘数量:<span class="required">*</span>
                </th>
                <td colspan="3">
                    <telerik:RadNumericTextBox ID="radNum" runat="server" Width="120px" MaxValue="99999"
                        MaxLength="5">
                        <NumberFormat AllowRounding="false" DecimalDigits="0" />
                    </telerik:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="rqvNum" runat="server" ControlToValidate="radNum"
                        ErrorMessage="<img src='/Image/invalid.gif' title='招聘数量不能为空!' />"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <th>招聘期限:
                </th>
                <td colspan="3">
                    <div class="time-scope">
                        <telerik:RadDatePicker ID="radStartTime" runat="server">
                        </telerik:RadDatePicker>
                        <div class="label">
                            至:
                        </div>
                        <telerik:RadDatePicker ID="radEndTime" runat="server">
                        </telerik:RadDatePicker>
                    </div>
                </td>
            </tr>
            <tr>
                <th>联 系 人:<span class="required">*</span>
                </th>
                <td colspan="3">
                    <asp:TextBox ID="txtContactName" runat="server" Width="364px" MaxLength="10"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rqvContactName" runat="server" ControlToValidate="txtContactName"
                        ErrorMessage="<img src='/Image/invalid.gif' title='联 系 人不能为空!' />"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <th>联系电话:<span class="required">*</span>
                </th>
                <td colspan="3">
                    <asp:TextBox ID="txtTelephone" runat="server" Width="364px" MaxLength="15"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rqvTelephone" runat="server" ControlToValidate="txtTelephone"
                        ErrorMessage="<img src='/Image/invalid.gif' title='联系电话不能为空!' />"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <th valign="top">岗位所在地区:
                </th>
                <td colspan="3">
                    <%--                    <asp:TextBox ID="txtAddress" runat="server" Width="600px" MaxLength="100"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rqvAddress" runat="server" ControlToValidate="txtAddress"
                        ErrorMessage="<img src='/Image/invalid.gif' title='通信地址不能为空!' />"></asp:RequiredFieldValidator>--%>
                    <asp:DropDownList ID="drp_ProvinceName_" runat="server" Width="120px" OnSelectedIndexChanged="drp_ProvinceName__SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                    <asp:DropDownList ID="drp_CityName_" runat="server" Width="120px" Enabled="false" OnSelectedIndexChanged="drp_CityName__SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                    <asp:DropDownList ID="drp_DistrictName_" runat="server" Width="120px" Enabled="false"></asp:DropDownList>
                </td>
            </tr>
        </table>
    </div>
    <div class="head-bar">
        <div class="caption">
            职位基本要求
        </div>
    </div>
    <div class="conatiner">
        <table>
            <tr>
                <th>学历要求:<span class="required">*</span>
                </th>
                <td>
                    <asp:DropDownList ID="drpEducation" runat="server" Width="160px">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rqvEducation" runat="server" ControlToValidate="drpEducation"
                        ErrorMessage="<img src='/Image/invalid.gif' title='学历要求不能为空!' />"></asp:RequiredFieldValidator>
                </td>
                <th>性别要求:
                </th>
                <td>
                    <asp:DropDownList ID="drpSex" runat="server" Width="160px">
                        <asp:ListItem Text="不限" Value="-1"></asp:ListItem>
                        <asp:ListItem Text="男" Value="1"></asp:ListItem>
                        <asp:ListItem Text="女" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th>职位性质:<span class="required">*</span>
                </th>
                <td>
                    <asp:DropDownList ID="drpNature" runat="server" Width="160px">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rqvNature" runat="server" ControlToValidate="drpNature"
                        ErrorMessage="<img src='/Image/invalid.gif' title='职位性质不能为空!' />"></asp:RequiredFieldValidator>
                </td>
                <th>招聘对象:<span class="required">*</span>
                </th>
                <td>
                    <asp:DropDownList ID="drpRecruitmentTarget" runat="server" Width="150px">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rqvRecruitmentTarget" runat="server" ControlToValidate="drpRecruitmentTarget"
                        ErrorMessage="<img src='/Image/invalid.gif' title='招聘对象不能为空!' />"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <th>年龄要求:
                </th>
                <td colspan="3">
                    <asp:TextBox ID="txtAgeScope" runat="server" Width="156px" MaxLength="10"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>工作地点:<span class="required">*</span>
                </th>
                <td colspan="3">
                    <asp:TextBox ID="txtWorkPlace" runat="server" Width="478px" MaxLength="40"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rqvWorkPlace" runat="server" ControlToValidate="txtWorkPlace"
                        ErrorMessage="<img src='/Image/invalid.gif' title='工作地点不能为空!' />"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <th>参考月薪:<span class="required">*</span>
                </th>
                <td colspan="3">
                    <asp:TextBox ID="txtSalaryScope" runat="server" Width="156px" MaxLength="30"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rqvSalaryScope" runat="server" ControlToValidate="txtSalaryScope"
                        ErrorMessage="<img src='/Image/invalid.gif' title='参考月薪不能为空!' />"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <th></th>
                <td colspan="3">
                    <asp:CheckBox ID="chkIsOnline" runat="server" Text="发布职位" />
                </td>
            </tr>
        </table>
    </div>
    <div class="head-bar">
        <div class="caption">
            <strong>职位职责,职位要求,应聘方式(Job Responsibilities & Requirements)：</strong>
        </div>
    </div>
    <div class="conatiner">
        <uc:EditorControl ID="radEditor" runat="server" EditorHeight="380" EditorWidth="750" />
    </div>
</div>
