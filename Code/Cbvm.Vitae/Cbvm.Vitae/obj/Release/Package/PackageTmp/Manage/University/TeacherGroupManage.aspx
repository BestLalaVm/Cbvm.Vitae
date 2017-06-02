<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/MasterList.master" AutoEventWireup="true" CodeBehind="TeacherGroupManage.aspx.cs" Inherits="Cbvm.Vitae.Manage.University.TeacherGroupManage" Theme="UniversityManage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPlaceCondition" runat="server">
    <asp:Panel ID="pnlCondition" runat="server">
        <table>
            <tr>
                <th>职能组名称:
                </th>
                <td>
                    <asp:TextBox ID="prm_Name_" runat="server" Width="260px" CssClass="edit-text"></asp:TextBox>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentPlaceAction" runat="server">
        <div class="left">
        <asp:Button ID="btnNew" runat="server" OnClick="btnNew_Click" Text="新增"></asp:Button>
    </div>
    <div class="right">
        <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="搜索"></asp:Button>
        <asp:Button ID="btnReset" runat="server" OnClick="btnReset_Click" Text="重置"></asp:Button>
    </div>
    <br class="clear" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentPlaceList" runat="server">
    <telerik:RadGrid ID="grdTeacherGroup" runat="server" OnItemDataBound="radGrid_ItemDataBound"
        AutoGenerateColumns="false" AllowCustomPaging="True" AllowPaging="True"
        OnPageIndexChanged="radGrid_PageIndexChanged"
        OnNeedDataSource="radGrid_NeedDataSource" OnUpdateCommand="radGrid_UpdateCommand">
        <MasterTableView DataKeyNames="Code" AllowCustomPaging="True" AllowPaging="True">
            <Columns>
                <telerik:GridBoundColumn DataField="Index" HeaderText="编号">
                    <HeaderStyle HorizontalAlign="Left" Width="50px" />
                    <ItemStyle HorizontalAlign="Left" Width="50px" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Code" HeaderText="编码">
                    <HeaderStyle HorizontalAlign="Left" Width="60px" />
                    <ItemStyle HorizontalAlign="Left" Width="60px" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Name" HeaderText="职能组名称">
                    <HeaderStyle HorizontalAlign="Left" Width="250px" />
                    <ItemStyle HorizontalAlign="Left" Width="250px" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Description" HeaderText="职能组描述">
                    <HeaderStyle HorizontalAlign="Left" Width="250px" />
                    <ItemStyle HorizontalAlign="Left" Width="250px" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="LastUpdateTime" HeaderText="最后修改时间" DataFormatString="{0:yyyy-MM-dd H:M:ss}">
                    <HeaderStyle HorizontalAlign="Left" Width="120" />
                    <ItemStyle HorizontalAlign="Left" Width="120" />
                </telerik:GridBoundColumn>

                <telerik:GridTemplateColumn>
                    <ItemTemplate>
                        <asp:LinkButton ID="linkEdit" runat="server" Text="编辑" CommandName="Edit"></asp:LinkButton>
                    </ItemTemplate>
                    <HeaderStyle Width="100px" />
                    <ItemStyle Width="100px" />
                </telerik:GridTemplateColumn>
            </Columns>
            <NoRecordsTemplate>
                <div class="empty">
                    没有记录
                </div>
            </NoRecordsTemplate>
            <EditFormSettings EditFormType="Template">
                <FormTemplate>
                    <div class="data-detail">
                        <div class="caption">
                            <h3>职能组明细</h3>
                        </div>
                        <div class="container">
                            <table>
                                <tr>
                                    <th valign="top"><span class="required">职能组名称</span>:</th>
                                    <td>
                                        <asp:TextBox ID="txtName" runat="server" Width="240px" CssClass="edit-text" Text='<%#Eval("Name") %>' MaxLength="200"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <th></th>
                                    <td>
                                        <asp:RequiredFieldValidator runat="server" ID="rqvName" ControlToValidate="txtName" Display="Dynamic" ErrorMessage="<span class='required'>职能组名称不能为空</span>"></asp:RequiredFieldValidator></td>
                                </tr>
                                <tr>
                                    <th valign="top">职能组描述:</th>
                                    <td>
                                        <asp:TextBox ID="txtDescription" MaxLength="500" runat="server" TextMode="MultiLine" Width="400px" Height="60" CssClass="edit-text" Text='<%#Eval("Description") %>'></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <div class="action">
                        <asp:Button ID="btnUpdate" runat="server" Text="保存" CommandName="Update" />
                        <asp:Button ID="btnCancel" Text="取消" runat="server" CausesValidation="False" CommandName="Cancel" />
                    </div>
                </FormTemplate>
            </EditFormSettings>
        </MasterTableView>
        <ClientSettings>
            <Selecting AllowRowSelect="true" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
