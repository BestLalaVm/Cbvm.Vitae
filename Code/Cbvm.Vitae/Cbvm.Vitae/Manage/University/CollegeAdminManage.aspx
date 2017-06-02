<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/MasterList.master" AutoEventWireup="true" CodeBehind="CollegeAdminManage.aspx.cs" Inherits="Cbvm.Vitae.Manage.University.CollegeAdminManage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPlaceCondition" runat="server">
    <asp:Panel ID="pnlCondition" runat="server">
        <table>
            <tr>
                <th>用户名:
                </th>
                <td>
                    <asp:TextBox ID="prm_UserName_" runat="server" Width="260px" CssClass="edit-text"></asp:TextBox>
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
        OnPageIndexChanged="radGrid_PageIndexChanged" OnDeleteCommand="radGrid_DeleteCommand"
        OnNeedDataSource="radGrid_NeedDataSource" OnUpdateCommand="radGrid_UpdateCommand">
        <MasterTableView DataKeyNames="ID" AllowCustomPaging="True" AllowPaging="True">
            <Columns>
                <telerik:GridBoundColumn DataField="Index" HeaderText="编号">
                    <HeaderStyle HorizontalAlign="Left" Width="40px" />
                    <ItemStyle HorizontalAlign="Left" Width="40px" />
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn>
                    <ItemTemplate>
                        <asp:LinkButton ID="linkEdit" runat="server" Text="编辑" CommandName="Edit"></asp:LinkButton>
                    </ItemTemplate>
                    <HeaderStyle Width="100px" />
                    <ItemStyle Width="100px" />
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn>
                    <ItemTemplate>
                        <asp:LinkButton CommandName="delete" Text="删除" runat="server" OnClientClick='<%# "if(!BeforeDeletd(\""+DataBinder.Eval(Container,"DataItem.ID")+"\",\""+DataBinder.Eval(Container,"DataItem.UserName")+"\"))return false;"%>' ID="linkDelete"></asp:LinkButton>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" Width="60px" />
                    <ItemStyle HorizontalAlign="Left" Width="60px" />
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="UserName" HeaderText="用户名">
                    <HeaderStyle HorizontalAlign="Left" Width="200px" />
                    <ItemStyle HorizontalAlign="Left" Width="200px" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="CollegeName" HeaderText="所属学院">
                    <HeaderStyle HorizontalAlign="Left" Width="200px" />
                    <ItemStyle HorizontalAlign="Left" Width="200px" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="CreateTime" HeaderText="创建时间" DataFormatString="{0:yyyy-MM-dd H:M:ss}">
                    <HeaderStyle HorizontalAlign="Left" Width="120" />
                    <ItemStyle HorizontalAlign="Left" Width="120" />
                </telerik:GridBoundColumn>
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
                            <h3>学院用户名称</h3>
                        </div>
                        <div class="container">
                            <table>
                                <tr>
                                    <th valign="top"><span class="required">用户名</span>:</th>
                                    <td>
                                        <asp:TextBox ID="txtUserName" runat="server" Width="240px" CssClass="edit-text" MaxLength="200"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <th></th>
                                    <td>
                                        <asp:RequiredFieldValidator runat="server" ID="rqvUserName" ControlToValidate="txtUserName" Display="Dynamic" ErrorMessage="<span class='required'>用户名不能为空</span>"></asp:RequiredFieldValidator></td>
                                </tr>
                                <tr>
                                    <th valign="top"></th>
                                    <td>
                                        <asp:CheckBox ID="chkChangePassword" runat="server" OnCheckedChanged="chkChangePassword_CheckedChanged" AutoPostBack="true" Text="更改密码" />
                                    </td>
                                </tr>
                                <tr id="trPassword" runat="server" visible="false">
                                    <th valign="top"><span class="required">密码</span>:</th>
                                    <td>
                                        <asp:TextBox ID="txtPassword" runat="server" Width="240px" CssClass="edit-text" TextMode="Password" MaxLength="200"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ID="rqvPassword" ControlToValidate="txtPassword" Display="Dynamic" ErrorMessage="<span class='required'>密码不能为空</span>"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <th valign="top">所属学院:</th>
                                    <td>
                                        <asp:DropDownList ID="drp_CollegeName_" runat="server" Width="244px"></asp:DropDownList>
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
