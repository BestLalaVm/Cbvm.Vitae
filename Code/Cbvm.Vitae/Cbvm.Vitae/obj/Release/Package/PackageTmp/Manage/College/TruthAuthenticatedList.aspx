<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/MasterList.master" AutoEventWireup="true" CodeBehind="TruthAuthenticatedList.aspx.cs" Inherits="Cbvm.Vitae.Manage.College.TruthAuthenticatedList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPlaceCondition" runat="server">
    <asp:Panel ID="pnlCondition" runat="server">
        <table>
            <tr>
                <th>请求企业:
                </th>
                <td>
                    <asp:TextBox ID="prm_EnterpriseName_" runat="server" CssClass="edit-text"></asp:TextBox>
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
                <telerik:GridTemplateColumn HeaderText="信息真实有效?">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkIsAuthenticated" runat="server" Enabled="false" Checked='<%#Eval("IsAuthenticated") %>' />
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" Width="120px" />
                    <ItemStyle HorizontalAlign="Left" Width="120px" />
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="StudentNum" HeaderText="学生学号">
                    <HeaderStyle Width="120px" HorizontalAlign="Left" />
                    <ItemStyle Width="120px" HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="StudentName" HeaderText="学生姓名">
                    <HeaderStyle Width="100px" HorizontalAlign="Left" />
                    <ItemStyle Width="100px" HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="DepartName" HeaderText="学生系">
                    <HeaderStyle Width="120px" HorizontalAlign="Left" />
                    <ItemStyle Width="120px" HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="MarjorName" HeaderText="学生专业">
                    <HeaderStyle Width="120px" HorizontalAlign="Left" />
                    <ItemStyle Width="120px" HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="EnterpriseName" HeaderText="请求企业">
                    <HeaderStyle Width="120px" HorizontalAlign="Left" />
                    <ItemStyle Width="120px" HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="StartDate" HeaderText="请求时间">
                    <HeaderStyle Width="120px" HorizontalAlign="Left" />
                    <ItemStyle Width="120px" HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn>
                    <ItemTemplate>
                        <a href='TruthAuthenticatedDetail.aspx?ID=<%#Eval("ID") %>' title="编辑" class="grid-edit"><span>编辑</span>
                        </a>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" Width="50px" />
                    <ItemStyle HorizontalAlign="Left" Width="50px" />
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn>
                    <ItemTemplate>
                        <a href='/Template/StudentTemplate/ViewStudentResume.aspx?StudentNum=<%#Eval("StudentNum") %>' target="_blank" title="查看学生简历" class="grid-edit"><span>查看学生</span>
                        </a>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" Width="120px" />
                    <ItemStyle HorizontalAlign="Left" Width="120px" />
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn>
                    <ItemTemplate>
                        <a href='/Template/EnterpriseDetail.aspx?KeyCode=<%#Eval("EnterpriseCode") %>' target="_blank" title="查看请求企业" class="grid-edit"><span>查看企业</span>
                        </a>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" Width="120px" />
                    <ItemStyle HorizontalAlign="Left" Width="120px" />
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
