<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/MasterList.master" AutoEventWireup="true" CodeBehind="VoluntaryServiceList.aspx.cs" Inherits="Cbvm.Vitae.Manage.College.VoluntaryServiceList" %>


<asp:Content ID="Content1" ContentPlaceHolderID="contentHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPlaceCondition" runat="server">
    <asp:Panel ID="pnlCondition" runat="server">
        <table>
            <tr>
                <th>标题:
                </th>
                <td>
                    <asp:TextBox ID="prm_Title_" runat="server" CssClass="edit-text"></asp:TextBox>
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
            <tr>
                <th>所属系:
                </th>
                <td>
                    <asp:DropDownList ID="prm_DepartCode_" runat="server" Width="180px"></asp:DropDownList>
                </td>
                <th>所属专业:
                </th>
                <td>
                    <asp:DropDownList ID="prm_MarjorCode_" runat="server" Width="140px"></asp:DropDownList>
                </td>
                <th>姓名:
                </th>
                <td>
                    <asp:TextBox ID="prm_StudentName_" runat="server" Width="140px" CssClass="edit-text"></asp:TextBox>
                </td>

            </tr>
            <tr>
                <th>学号:
                </th>
                <td>
                    <asp:TextBox ID="prm_StudentNum_" runat="server" CssClass="edit-text"></asp:TextBox>
                </td>
                <th>审核状态:
                </th>
                <td>
                    <asp:DropDownList ID="prm_VerfyStatus_" runat="server" Width="140px">
                    </asp:DropDownList>
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
    <telerik:RadGrid ID="grdProject" runat="server" OnNeedDataSource="radGrid_NeedDataSource"
        OnPageIndexChanged="radGrid_PageIndexChanged" AutoGenerateColumns="false" PageSize="30" AllowCustomPaging="true" AllowPaging="true">
        <MasterTableView DataKeyNames="ID">
            <Columns>
                <telerik:GridBoundColumn DataField="Index" HeaderText="序号">
                    <HeaderStyle HorizontalAlign="Left" Width="50px" />
                    <ItemStyle HorizontalAlign="Left" Width="50px" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Title" HeaderText="标题">
                    <HeaderStyle Width="280px" HorizontalAlign="Left" />
                    <ItemStyle Width="280px" HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="VerfyStatus" HeaderText="审核状态">
                    <HeaderStyle Width="70px" HorizontalAlign="Left" />
                    <ItemStyle Width="70px" HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="StartDate" HeaderText="开始时间">
                    <HeaderStyle Width="80px" HorizontalAlign="Left" />
                    <ItemStyle Width="80px" HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="EndDate" HeaderText="结束时间">
                    <HeaderStyle Width="80px" HorizontalAlign="Left" />
                    <ItemStyle Width="80px" HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn HeaderText="公开">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkIsOnline" runat="server" Enabled="false" Checked='<%#Eval("IsOnline") %>' />
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" Width="60px" />
                    <ItemStyle HorizontalAlign="Left" Width="60px" />
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="StudentName" HeaderText="学生姓名">
                    <HeaderStyle Width="100px" HorizontalAlign="Left" />
                    <ItemStyle Width="100px" HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="StudentDepart" HeaderText="学生系">
                    <HeaderStyle Width="120px" HorizontalAlign="Left" />
                    <ItemStyle Width="120px" HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="StudentMarjor" HeaderText="学生专业">
                    <HeaderStyle Width="120px" HorizontalAlign="Left" />
                    <ItemStyle Width="120px" HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn>
                    <ItemTemplate>
                        <a href='VoluntaryServiceDetail.aspx?ID=<%#Eval("ID") %>' title="查看" class="grid-edit"><span>查看</span>
                        </a>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" Width="50px" />
                    <ItemStyle HorizontalAlign="Left" Width="50px" />
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
