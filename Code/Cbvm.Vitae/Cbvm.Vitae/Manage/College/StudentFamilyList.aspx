<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/MasterList.master" AutoEventWireup="true" CodeBehind="StudentFamilyList.aspx.cs"
    Inherits="Cbvm.Vitae.Manage.College.StudentFamilyList" Theme="UniversityManage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentHead" runat="server">
    <script type="text/javascript">
        function RowClickEx() {
            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPlaceCondition" runat="server">
    <asp:Panel ID="pnlCondition" runat="server">
        <table>
            <tr>
                <th>学生姓名:</th>
                <td>
                    <asp:TextBox ID="prm_StudentName_" runat="server" Width="180px" CssClass="edit-text"></asp:TextBox>
                </td>
                <th>学号:</th>
                <td>
                    <asp:TextBox ID="prm_StudentNum_" runat="server" Width="180px" CssClass="edit-text"></asp:TextBox>
                </td>
                <th>家长姓名:</th>
                <td>
                    <asp:TextBox ID="prm_Name_" runat="server" Width="180px" CssClass="edit-text"></asp:TextBox>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentPlaceAction" runat="server">
    <div class="right">
        <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="搜索"></asp:Button>
        <asp:Button ID="btnReset" runat="server" OnClick="btnReset_Click" Text="重置"></asp:Button>
    </div>
    <br class="clear" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentPlaceList" runat="server">
    <telerik:RadGrid ID="grdStudentFamily" runat="server" AutoGenerateColumns="false"
        OnNeedDataSource="radGrid_NeedDataSource">
        <MasterTableView>
            <Columns>
                <telerik:GridBoundColumn DataField="Index" HeaderText="编号">
                        <HeaderStyle HorizontalAlign="Left" Width="50px" />
                    <ItemStyle HorizontalAlign="Left" Width="50px" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="StudentNum" HeaderText="学号">
                    <HeaderStyle HorizontalAlign="Left" Width="120" />
                    <ItemStyle HorizontalAlign="Left" Width="120" />
                </telerik:GridBoundColumn>
                <telerik:GridDateTimeColumn DataField="StudentName" HeaderText="姓名">
                    <HeaderStyle HorizontalAlign="Left" Width="100" />
                    <ItemStyle HorizontalAlign="Left" Width="100" />
                </telerik:GridDateTimeColumn>
                <telerik:GridTemplateColumn HeaderText="">
                    <ItemTemplate>
                        <img src="<%#Eval("StudentThumbPath") %>" width="50" height="50" />
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" Width="60" />
                    <ItemStyle HorizontalAlign="Left" Width="60" />
                </telerik:GridTemplateColumn>

                <telerik:GridDateTimeColumn DataField="StudentTelephone" HeaderText="电话">
                    <HeaderStyle HorizontalAlign="Left" Width="100" />
                    <ItemStyle HorizontalAlign="Left" Width="100" />
                </telerik:GridDateTimeColumn>
                <telerik:GridDateTimeColumn DataField="StudentIDentityNum" HeaderText="身份证">
                    <HeaderStyle HorizontalAlign="Left" Width="150" />
                    <ItemStyle HorizontalAlign="Left" Width="150" />
                </telerik:GridDateTimeColumn>
                <telerik:GridDateTimeColumn DataField="UserName" HeaderText="家长帐号">
                    <HeaderStyle HorizontalAlign="Left" Width="120" />
                    <ItemStyle HorizontalAlign="Left" Width="120" />
                </telerik:GridDateTimeColumn>
                <telerik:GridDateTimeColumn DataField="Name" HeaderText="家长姓名">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridDateTimeColumn>
            </Columns>
            <NoRecordsTemplate>
                <div class="empty">
                    没有记录
                </div>
            </NoRecordsTemplate>
        </MasterTableView>
        <ClientSettings>
            <Selecting AllowRowSelect="true" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
