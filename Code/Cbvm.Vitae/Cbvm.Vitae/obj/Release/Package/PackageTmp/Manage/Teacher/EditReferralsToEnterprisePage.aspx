<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/ThickBox.Master" AutoEventWireup="true" CodeBehind="EditReferralsToEnterprisePage.aspx.cs" Inherits="Cbvm.Vitae.Manage.Teacher.EditReferralsToEnterprisePage" %>

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
            padding-right: 35px;
            padding-top: 6px;
        }
        .condition {
            display: none;
        }
    </style>
    <script type="text/javascript">
        function GetComponentSize(current, config) {
            return {
                Width: "50px"
            };
        }

        function ConfirmToPassed() {
            if (!confirm("确定推荐该学生?")) return false;
            return true;
        }

        function ConfirmToDeny() {
            if (!confirm("确定拒绝推荐该学生?")) return false;
            return true;
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
            <th>学生姓名:</th>
            <td>
                <asp:Literal runat="server" ID="ltlStudentNum"></asp:Literal>
            </td>
            <th>学号:</th>
            <td>
                <asp:Literal runat="server" ID="ltlStudentName"></asp:Literal>
            </td>
        </tr>
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
            <th>工作地点:</th>
            <td colspan="3">
                <asp:Literal runat="server" ID="ltlWorkPlace"></asp:Literal>
            </td>
        </tr>
        <tr>
            <th>当前状态:</th>
            <td colspan="3">
                <asp:Literal runat="server" ID="ltlReferralState"></asp:Literal>
            </td>
        </tr>
        <tr>
            <th>申请人描述:</th>
            <td colspan="3">
                <asp:Literal runat="server" ID="ltlNote"></asp:Literal>
            </td>
        </tr>
        <tr>
            <th>推荐描述:</th>
            <td colspan="3">
                <asp:TextBox runat="server" ID="txtDescription" Width="323px" Height="60px" TextMode="MultiLine" CssClass="multi-edit-text" MaxLength="150"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="5" align="right" class="action">
                <asp:Button runat="server" ID="btnPassed" Text="推荐" OnClick="btnPassed_Click" OnClientClick="if(!ConfirmToPassed())return false;" />
                <asp:Button runat="server" ID="btnDeny" Text="拒绝" OnClick="btnDeny_Click" OnClientClick="if(!ConfirmToDeny())return false;" />
            </td>
        </tr>
    </table>
</asp:Content>
