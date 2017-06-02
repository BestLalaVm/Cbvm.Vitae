<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="_AssessmentDetail.ascx.cs" Inherits="Cbvm.Vitae.Manage.College.UserControl._AssessmentDetail" %>
<%@ Register Src="~/Manage/UserControl/EditorControl.ascx" TagName="EditorControl"
    TagPrefix="uc" %>

<script type="text/javascript">
    function OpenStudentList() {
        window.parent.ShowIframeForm("选择学生信息", "../Common/SearchStudentPageSingle.aspx", "910px", "500px", {
            hideOverFlow: true,
            isShowFooter: true,
            closeCallBackHandler: function () {
                // window.location.href = window.location.href;
            }
        });
    }

    function setSelectedStudent(studentNum, studentName) {
        $("#<%=this.txt_StudentNum_.ClientID%>").val(studentNum);
            $("#<%=this.txt_StudentName_.ClientID%>").val(studentName);
        }
</script>
<div class="func-block">
    <div class="caption">
        <h3><%=Caption %></h3>
    </div>
    <div class="block tab-block detail">
        <table>
            <tr>
                <th>
                    <span class="required">标题</span>:
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
                <th><span class="required">学生当事人</span>:
                </th>
                <td>
                    <%--       <asp:DropDownList ID="drp_Student_" runat="server"></asp:DropDownList>--%>
                    <asp:TextBox ID="txt_StudentName_" runat="server" ReadOnly="true" Enabled="false"></asp:TextBox>
                    <asp:HiddenField ID="txt_StudentNum_" runat="server" />
                    <input type="button" value="添加学生" onclick="OpenStudentList()" style="width: 90px; height: 25px;" />
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_StudentName_"
                        Display="Dynamic" ErrorMessage="<span class='error'><i class='msg-ico'></i><label>学生当事人单位不能为空!</label></span>"></asp:RequiredFieldValidator>
                </td>
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
