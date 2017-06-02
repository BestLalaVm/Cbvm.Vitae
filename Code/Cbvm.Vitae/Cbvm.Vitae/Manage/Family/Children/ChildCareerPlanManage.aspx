<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/MasterList.master" AutoEventWireup="true" CodeBehind="ChildCareerPlanManage.aspx.cs" Inherits="Cbvm.Vitae.Manage.Family.Children.ChildCareerPlanManage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPlaceCondition" runat="server">
    <asp:Panel ID="pnlCondition" runat="server">
        <table>
            <tr>
                <th>规划标题:
                </th>
                <td>
                    <asp:TextBox ID="prm_Title_" runat="server" CssClass="edit-text"></asp:TextBox>
                </td>
                <th>开始时间从:
                </th>
                <td>
                    <telerik:RadDatePicker ID="prm_DateFrom_" runat="server">
                        <DateInput DateFormat="yyyy-MM-dd">
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
<asp:Content ID="Content3" ContentPlaceHolderID="contentPlaceAction" runat="server">
    <div class="right">
        <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="搜索"></asp:Button>
        <asp:Button ID="btnReset" runat="server" OnClick="btnReset_Click" Text="重置"></asp:Button>
    </div>
    <br class="clear"/>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentPlaceList" runat="server">
    <telerik:RadGrid ID="grdActivity" runat="server" OnNeedDataSource="radGrid_NeedDataSource"
        OnPageIndexChanged="radGrid_PageIndexChanged" OnDeleteCommand="radGrid_DeleteCommand"
        AutoGenerateColumns="false" PageSize="30" AllowCustomPaging="true" AllowPaging="true">
        <MasterTableView DataKeyNames="ID">
            <Columns>
                <telerik:GridBoundColumn DataField="Index" HeaderText="序号">
                                        <HeaderStyle HorizontalAlign="Left" Width="50px" />
                    <ItemStyle HorizontalAlign="Left" Width="50px" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Title" HeaderText="规划标题">
                    <HeaderStyle Width="300px" HorizontalAlign="Left" />
                    <ItemStyle Width="300px" HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="开始时间" DataField="StartDate" DataFormatString="{0:yyyy-MM-dd H:M:ss}">
                    <ItemStyle Width="120px" HorizontalAlign="Left" />
                    <HeaderStyle Width="120px" HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="结束时间" DataField="EndDate" DataFormatString="{0:yyyy-MM-dd H:M:ss}">
                    <ItemStyle Width="120px" HorizontalAlign="Left" />
                    <HeaderStyle Width="120px" HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn HeaderText="发布">
                                        <ItemStyle Width="60px" HorizontalAlign="Left" />
                    <HeaderStyle Width="60px" HorizontalAlign="Left" />
                    <ItemTemplate>
                        <asp:CheckBox ID="chkIsOnline" runat="server" Enabled="false" Checked='<%#Eval("IsOnline") %>'/>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                 <telerik:GridTemplateColumn HeaderText="已实现">
                    <ItemStyle Width="80px" HorizontalAlign="Left" />
                    <HeaderStyle Width="80px" HorizontalAlign="Left" />
                    <ItemTemplate>
                        <asp:CheckBox ID="chkIsImplemented" runat="server" Enabled="false" Checked='<%#Eval("IsImplemented") %>'/>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn>
                    <ItemTemplate>
                        <a href='ChildCareerPlanDetail.aspx?ID=<%#Eval("ID") %>' title="查看" class="grid-edit">
                            <span>编辑</span>
                        </a>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
            </Columns>
            <NoRecordsTemplate>
                <div>
                    没有记录!
                </div>
            </NoRecordsTemplate>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>
