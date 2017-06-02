<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/MasterList.master" AutoEventWireup="true" 
    CodeBehind="MailQueueList.aspx.cs" Inherits="Cbvm.Vitae.Manage.Supper.MailQueueList" Theme="DepartAdmin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPlaceCondition" runat="server">
    <asp:Panel ID="pnlCondition" runat="server">
        <table>
            <tr>
                <th>邮件主题:
                </th>
                <td>
                    <asp:TextBox ID="prm_Name_" runat="server" Width="180px" CssClass="edit-text"></asp:TextBox>
                </td>
                <th>邮件内容:</th>
                <td>
                    <asp:TextBox ID="prm_Message_" runat="server" Width="180px" CssClass="edit-text"></asp:TextBox>
                </td>
                <th>发送类型:</th>
                <td>
                    <asp:DropDownList runat="server" ID="prm_IsSended_" Width="100px"/>
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
    <telerik:RadGrid ID="grdEmailQueue" runat="server" AutoGenerateColumns="false" OnNeedDataSource="radGrid_NeedDataSource">
        <MasterTableView>
            <Columns>
                <telerik:GridBoundColumn DataField="Index" HeaderText="编号">
                    <HeaderStyle HorizontalAlign="Left" Width="40px" />
                    <ItemStyle HorizontalAlign="Left" Width="40px" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="LogType" HeaderText="邮件主题">
                    <HeaderStyle HorizontalAlign="Left" Width="120" />
                    <ItemStyle HorizontalAlign="Left" Width="120" />
                </telerik:GridBoundColumn>
                <telerik:GridDateTimeColumn DataField="Name" HeaderText="邮件内容">
                    <HeaderStyle HorizontalAlign="Left" Width="400" />
                    <ItemStyle HorizontalAlign="Left" Width="400" />
                </telerik:GridDateTimeColumn>
                <telerik:GridDateTimeColumn DataField="Message" HeaderText="接收人">
                    <HeaderStyle HorizontalAlign="Left" Width="400" />
                    <ItemStyle HorizontalAlign="Left" Width="400" />
                </telerik:GridDateTimeColumn>
                <telerik:GridDateTimeColumn DataField="CreateTime" HeaderText="时间" DataFormatString="{0:yyyy-MM-dd hh:mm:ss}">
                    <HeaderStyle HorizontalAlign="Left" Width="100" />
                    <ItemStyle HorizontalAlign="Left" Width="100" />
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
