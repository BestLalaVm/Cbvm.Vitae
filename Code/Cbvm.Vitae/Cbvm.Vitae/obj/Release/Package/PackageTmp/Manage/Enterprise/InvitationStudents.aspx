<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/MasterList.master" AutoEventWireup="true" CodeBehind="InvitationStudents.aspx.cs"
    Inherits="Cbvm.Vitae.Manage.Enterprise.InvitationStudents" Theme="EnterpriseManage" %>
<%@ Import Namespace="WebLibrary.Helper" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentHead" runat="server">
    <script type="text/javascript">
        function RowClickEx() {
            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentPlaceCondition" runat="server">
    <asp:Panel ID="pnlCondition" runat="server">
        <table>
            <tr>
                <th>学生名称:
                </th>
                <td>
                    <asp:TextBox ID="prm_StudentName_" runat="server" CssClass="edit-text"></asp:TextBox>
                </td>
                                <th>开始时间从:
                </th>
                <td>
                    <telerik:RadDatePicker ID="prm_DateFrom_" runat="server">
                        <DateInput DateFormat="yyyy-MM-dd" runat="server">
                        </DateInput>
                    </telerik:RadDatePicker>
                </td>
                <th>到
                </th>
                <td>
                    <telerik:RadDatePicker ID="prm_DateTo_" runat="server">
                        <DateInput ID="DateInput1" DateFormat="yyyy-MM-dd" runat="server">
                        </DateInput>
                    </telerik:RadDatePicker>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPlaceAction" runat="server">
    <div class="right">
        <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="搜索"></asp:Button>
        <asp:Button ID="btnReset" runat="server" OnClick="btnReset_Click" Text="重置"></asp:Button>
    </div>
    <div class="clear">
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentPlaceList" runat="server">
    <telerik:RadGrid ID="grdEnterpriseJob" runat="server" OnNeedDataSource="radGrid_NeedDataSource"
        AutoGenerateColumns="false" PageSize="30" AllowCustomPaging="true" AllowPaging="true">
        <MasterTableView>
            <Columns>
                <telerik:GridBoundColumn DataField="Index" HeaderText="序号">
                    <HeaderStyle HorizontalAlign="Left" Width="50px" />
                    <ItemStyle HorizontalAlign="Left" Width="50px" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="CollegeName" HeaderText="学院">
                    <HeaderStyle Width="200px" HorizontalAlign="Left" />
                    <ItemStyle Width="200px" HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="MarjorName" HeaderText="专业">
                    <HeaderStyle Width="200px" HorizontalAlign="Left" />
                    <ItemStyle Width="200px" HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="StudentNum" HeaderText="学号">
                    <HeaderStyle Width="150px" HorizontalAlign="Left" />
                    <ItemStyle Width="150px" HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridHyperLinkColumn DataTextField="StudentName" HeaderText="学生姓名" Target="_blank"
                    DataNavigateUrlFields="StudentNum" DataNavigateUrlFormatString="/Template/StudentResume.aspx?StudentNum={0}">
                     <HeaderStyle Width="150px" HorizontalAlign="Left" />
                    <ItemStyle Width="150px" HorizontalAlign="Left" />
                </telerik:GridHyperLinkColumn>
                <telerik:GridBoundColumn HeaderText="邀请时间" DataField="InvitedDate" DataFormatString="{0:yyyy-MM-dd hh:mm:ss}">
                    <ItemStyle Width="160px" HorizontalAlign="Left" />
                    <HeaderStyle Width="160px" HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridCheckBoxColumn DataField="HasRequested" HeaderText="已发简历">
                    <HeaderStyle Width="100px" HorizontalAlign="Left" />
                    <ItemStyle Width="100px" HorizontalAlign="Left" />
                </telerik:GridCheckBoxColumn>
                <telerik:GridCheckBoxColumn DataField="IsViewed" HeaderText="已查看">
                    <HeaderStyle Width="100px" HorizontalAlign="Left" />
                    <ItemStyle Width="100px" HorizontalAlign="Left" />
                </telerik:GridCheckBoxColumn>
            </Columns>
            <NoRecordsTemplate>
                <div>
                    没有记录!
                </div>
            </NoRecordsTemplate>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
