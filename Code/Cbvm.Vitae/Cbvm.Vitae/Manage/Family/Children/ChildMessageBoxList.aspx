<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/MasterList.master" AutoEventWireup="true" CodeBehind="ChildMessageBoxList.aspx.cs"
    Inherits="Cbvm.Vitae.Manage.Family.Children.ChildMessageBoxList" Theme="FamilyManage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentHead" runat="server">
    <script type="text/javascript">
        function View(msgId) {
            parent.tb_showIFrame("查看信件明细", "../MessageBox.aspx?ID=" + msgId + "&width=920&height=600&TB_iframe=true");
        }

        function CreateNewMsg() {
            parent.tb_showIFrame("新建信件", "../MessageBox.aspx?width=920&height=600&TB_iframe=true");
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPlaceCondition" runat="server">
    <asp:Panel ID="pnlCondition" runat="server">
        <table>
            <tr>
                <th>职位:
                </th>
                <td>
                    <asp:TextBox ID="prm_Name_" runat="server" Width="260px"></asp:TextBox>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentPlaceAction" runat="server">
    <div class="left">
        <asp:Button ID="btnNew" runat="server" Text="新增" OnClientClick="return CreateNewMsg();"></asp:Button>
    </div>
    <div class="right">
        <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="搜索"></asp:Button>
        <asp:Button ID="btnReset" runat="server" OnClick="btnReset_Click" Text="重置"></asp:Button>
    </div>
    <div class="clear">
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentPlaceList" runat="server">
    <div class="list-container">
        <telerik:RadGrid ID="grdMsgList" runat="server" AutoGenerateColumns="false" OnNeedDataSource="radGrid_NeedDataSource">
            <MasterTableView>
                <Columns>
                    <telerik:GridBoundColumn DataField="Index" HeaderText="序号">
                          <HeaderStyle HorizontalAlign="Left" Width="40px" />
                    <ItemStyle HorizontalAlign="Left" Width="40px" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Content" HeaderText="信件内容">
                        <HeaderStyle HorizontalAlign="Left" Width="400px" />
                        <ItemStyle HorizontalAlign="Left" Width="400px" />
                    </telerik:GridBoundColumn>
                    <telerik:GridTemplateColumn>
                        <HeaderStyle Width="30px" HorizontalAlign="Left" />
                        <ItemStyle Width="30px" HorizontalAlign="Left" />
                        <ItemTemplate>
                            <a href='MessageBox.aspx?ID=<%#Eval("ID") %>' class="grid-edit">查看</a>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn>
                        <HeaderStyle Width="80px" HorizontalAlign="Left" />
                        <ItemStyle Width="80px" HorizontalAlign="Left" />
                        <ItemTemplate>
                            <a href='../MessageBox.aspx?ID=<%#Eval("ID") %>'><b style="color: red;"><%#Eval("NewReplyCount") %></b> 条回复数未读</a>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridBoundColumn DataField="Time" HeaderText="发送时间">
                        <HeaderStyle Width="80px" HorizontalAlign="Left" />
                        <ItemStyle Width="80px" HorizontalAlign="Left" />
                    </telerik:GridBoundColumn>
                </Columns>
                <NoRecordsTemplate>
                    没有记录.
                </NoRecordsTemplate>
            </MasterTableView>
            <ClientSettings>
                <Selecting AllowRowSelect="true" />
            </ClientSettings>
        </telerik:RadGrid>
    </div>
</asp:Content>
