<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/MasterList.master" AutoEventWireup="true" CodeBehind="MessageBoxList.aspx.cs"
    Inherits="Cbvm.Vitae.Manage.MessageBox.MessageBoxList" Theme="BaseManage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPlaceCondition" runat="server">
    <asp:Panel ID="pnlCondition" runat="server">
        <table>
            <tr>
                <th>标题:
                </th>
                <td>
                    <asp:TextBox ID="prm_Subject_" runat="server" CssClass="edit-text"></asp:TextBox>
                </td>
                <th>发送时间从:
                </th>
                <td>
                    <telerik:raddatepicker id="prm_DateFrom_" runat="server" Width="100px">
                        <DateInput DateFormat="yyyy-MM-dd">
                        </DateInput>
                    </telerik:raddatepicker>
                </td>
                <th>到
                </th>
                <td>
                    <telerik:raddatepicker id="prm_DateTo_" runat="server" Width="100px">
                        <DateInput ID="DateInput1" DateFormat="yyyy-MM-dd" runat="server">
                        </DateInput>
                    </telerik:raddatepicker>
                </td>
                                <th>状态:</th>
                <td>
                    <asp:DropDownList runat="server" ID="prm_IsReaded_">
                    </asp:DropDownList>
                </td>
                <th>类型:</th>
                <td>
                    <asp:DropDownList runat="server" ID="prm_MsgType_">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentPlaceAction" runat="server">
    <div class="left">
        <input type="button" value="新建" onclick="CreateNewMsg();" style="height: 25px;" />
    </div>
    <div class="right">
        <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="搜索"></asp:Button>
        <asp:Button ID="btnReset" runat="server" OnClick="btnReset_Click" Text="重置"></asp:Button>
    </div>
    <div class="clear">
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentPlaceList" runat="server">
    <telerik:radgrid id="grdMsgBox" runat="server" onneeddatasource="radGrid_NeedDataSource"
        onpageindexchanged="radGrid_PageIndexChanged" autogeneratecolumns="false" pagesize="30" allowcustompaging="true" allowpaging="true">
        <MasterTableView DataKeyNames="ID">
            <Columns>
                <telerik:GridBoundColumn DataField="Index" HeaderText="序号">
                    <HeaderStyle HorizontalAlign="Left" Width="50px" />
                    <ItemStyle HorizontalAlign="Left" Width="50px" />
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn>
                    <ItemTemplate>
                        <a href='../MessageBox/ViewMessageBox.aspx?ID=<%#Eval("ID") %>' class="grid-edit">查看</a>
                    </ItemTemplate>
                    <HeaderStyle Width="40px" HorizontalAlign="Left" />
                    <ItemStyle Width="40px" HorizontalAlign="Left" />
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="ReadLabel" HeaderText="状态">
                    <HeaderStyle HorizontalAlign="Left" Width="50px" />
                    <ItemStyle HorizontalAlign="Left" Width="50px" />
                </telerik:GridBoundColumn>
                 <telerik:GridBoundColumn DataField="MsgType" HeaderText="类型">
                    <HeaderStyle HorizontalAlign="Left" Width="100px" />
                    <ItemStyle HorizontalAlign="Left" Width="100px" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Subject" HeaderText="标题">
                    <HeaderStyle HorizontalAlign="Left" Width="350px" />
                    <ItemStyle HorizontalAlign="Left" Width="350px" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Time" HeaderText="发送时间" DataFormatString="{0:yyyy-MM-dd hh:mm:ss}">
                    <HeaderStyle Width="120px" HorizontalAlign="Left" />
                    <ItemStyle Width="120px" HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
            </Columns>
            <NoRecordsTemplate>
                <div>
                    没有记录!
                </div>
            </NoRecordsTemplate>
        </MasterTableView>
    </telerik:radgrid>
    <script type="text/javascript">
        function CreateNewMsg() {
            window.location.href = "../MessageBox/NewMessageBox.aspx";
        }
    </script>
</asp:Content>
