<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/ThickBox.Master" AutoEventWireup="true" CodeBehind="RequestNewJob.aspx.cs" Inherits="Cbvm.Vitae.Manage.Student.RequestNewJob" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        table th {
            width: 80px;
        }

        table tr th:first-child {
            width: 110px;
            text-align: right;
            vertical-align: top;
        }

        table td.action {
            padding-right: 30px;
            padding-top: 6px;
        }
        .condition {
            display: none;
        }
        #btnReferral {
            width:80px !important;
        }
        .action{
            padding-right:108px !important;
        }
    </style>
    <script type="text/javascript">
        function GetComponentSize(current, config) {
            return {
                Width: "60px"
            };
        }
        $(function () {
            EndRequest();
        });
        function OpenTeacherList() {
            window.parent.ShowIframeForm("选择教师", "../Common/SearchTeacherPageMulip.aspx", "910px", "500px", {
                hideOverFlow: true,
                isShowFooter: true,
                closeCallBackHandler: function () {
                   // window.location.href = window.location.href;
                }
            });
        }

        function setSelectedTeacher(teacherNums, teacherNames) {
            $("#<%=txt_TeacherNum_.ClientID%>").val(teacherNums);
            $("#<%=txt_TeacherName_.ClientID%>").val(teacherNames);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPlaceCondition" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentPlaceAction" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentPlaceList" runat="server">
    <table width="100%">
        <tr>
            <th>公司名称:</th>
            <td colspan="3">
                <asp:Literal runat="server" ID="ltlEnterpriseName"></asp:Literal>
            </td>
        </tr>
        <tr>
            <th>职位名称:</th>
            <td colspan="3">
                <asp:Literal runat="server" ID="ltlJobName"></asp:Literal>
            </td>
        </tr>
        <tr>
            <th>招聘人数:</th>
            <td>
                <asp:Literal runat="server" ID="ltlNum"></asp:Literal>
            </td>
            <th>薪资范围:</th>
            <td>
                <asp:Literal runat="server" ID="ltlSalaryScope"></asp:Literal></td>
        </tr>
        <tr>
            <th>工作地点:</th>
            <td colspan="3">
                <asp:Literal runat="server" ID="ltlWorkPlace"></asp:Literal>
            </td>
        </tr>
        <tr>
            <th>备注:</th>
            <td colspan="3">
                <asp:TextBox runat="server" ID="txtNote" Width="323px" Height="60px" TextMode="MultiLine" CssClass="multi-edit-text" MaxLength="150"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th>推荐人(老师):</th>
            <td colspan="3">
                <asp:TextBox ID="txt_TeacherName_" runat="server" ReadOnly="true" Enabled="false" Width="320px"></asp:TextBox>
                    <asp:HiddenField ID="txt_TeacherNum_" runat="server" />
                    <input type="button" value="选择推荐人" id="btnReferral" onclick="OpenTeacherList()" style="width: 110px; height: 25px;" />
            </td>
        </tr>
        <tr>
            <td colspan="4" align="right" class="action">
                <asp:Button runat="server" ID="btnSubmit" OnClick="btnSubmit_Click" Text="申请职位" />
            </td>
        </tr>
    </table>
</asp:Content>
