<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/MasterList.master" AutoEventWireup="true"
    CodeBehind="StudentProfessionalList.aspx.cs" Inherits="Cbvm.Vitae.Manage.Student.StudentProfessionalList" Theme="StudentManage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceCondition" runat="server">
    <asp:Panel ID="pnlCondition" runat="server">
        <table>
            <tr>
                <th><%=NameTitle %>名称:
                </th>
                <td>
                    <asp:TextBox ID="prm_Name_" runat="server" CssClass="edit-text"></asp:TextBox>
                </td>
                <th>获取时间从:
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
<asp:Content ID="Content2" ContentPlaceHolderID="contentPlaceAction" runat="server">
    <div class="left">
        <asp:Button ID="btnNew" runat="server" OnClick="btnNew_Click" Text="新增"></asp:Button>
    </div>
    <div class="right">
        <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="搜索"></asp:Button>
        <asp:Button ID="btnReset" runat="server" OnClick="btnReset_Click" Text="重置"></asp:Button>
    </div>
    <div class="clear">
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentPlaceList" runat="server">
    <telerik:RadGrid ID="grdProfessional" runat="server" OnNeedDataSource="radGrid_NeedDataSource"
        OnPageIndexChanged="radGrid_PageIndexChanged" OnDeleteCommand="radGrid_DeleteCommand"
        AutoGenerateColumns="false" PageSize="30" AllowCustomPaging="true" AllowPaging="true">
        <MasterTableView DataKeyNames="ID" AllowCustomPaging="true" AllowPaging="true">
            <Columns>
                <telerik:GridBoundColumn DataField="Index" HeaderText="序号">
                    <HeaderStyle HorizontalAlign="Left" Width="50px" />
                    <ItemStyle HorizontalAlign="Left" Width="50px" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Name" HeaderText="名称">
                    <HeaderStyle Width="360px" HorizontalAlign="Left" />
                    <ItemStyle Width="360px" HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="获奖时间" DataField="ObtainTime">
                    <HeaderStyle HorizontalAlign="Left" Width="120px" />
                    <ItemStyle HorizontalAlign="Left" Width="120px" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="VerfyStatus" HeaderText="审核状态">
                    <HeaderStyle Width="70px" HorizontalAlign="Left" />
                    <ItemStyle Width="70px" HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn HeaderText="公开">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkIsOnline" runat="server" OnCheckedChanged="chkIsOnline_CheckedChanged"  AutoPostBack="True"
                            Checked='<%#Eval("IsOnline") %>' />
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" Width="40px" />
                    <ItemStyle HorizontalAlign="Left" Width="40px" />
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn>
                    <ItemTemplate>
                        <a href='StudentProfessionalDetail.aspx?SourceType=<%#Eval("Type") %>&ID=<%#Eval("ID") %>' title="编辑" class="grid-edit"><span>编辑</span>
                        </a>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" Width="40px" />
                    <ItemStyle HorizontalAlign="Left" Width="40px" />
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn>
                    <ItemTemplate>
                        <asp:LinkButton CommandName="delete" OnClientClick='<%# "if(!BeforeDeletd("+DataBinder.Eval(Container,"DataItem.ID")+",\""+DataBinder.Eval(Container,"DataItem.Name")+"\"))return false;"%>' Text="删除" ID="btnDelete" runat="server"></asp:LinkButton>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" Width="40px" />
                    <ItemStyle HorizontalAlign="Left" Width="40px" />
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
