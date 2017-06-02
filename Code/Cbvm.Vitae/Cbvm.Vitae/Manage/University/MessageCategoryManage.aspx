<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/MasterList.master" AutoEventWireup="true" CodeBehind="MessageCategoryManage.aspx.cs" Inherits="Cbvm.Vitae.Manage.University.MessageCategoryManage" %>

<asp:Content ID="Content4" runat="server" ContentPlaceHolderID="contentHead">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceCondition" runat="server">
    <asp:Panel ID="pnlCondition" runat="server">
        <table>
            <tr>
                <th>名称:
                </th>
                <td>
                    <asp:TextBox ID="prm_Name_" runat="server" Width="260px" CssClass="edit-text"></asp:TextBox>
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
    <br class="clear" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentPlaceList" runat="server">
    <telerik:RadGrid ID="grdMessage" runat="server" AutoGenerateColumns="false" OnItemDataBound="grdMessage_ItemDataBound"
        OnNeedDataSource="radGrid_NeedDataSource" OnDeleteCommand="radGrid_DeleteCommand"
        OnUpdateCommand="grdMessage_UpdateCommand">
        <MasterTableView DataKeyNames="Code">
            <Columns>
                <telerik:GridBoundColumn DataField="Index" HeaderText="编号">
                    <HeaderStyle HorizontalAlign="Left" Width="50px" />
                    <ItemStyle HorizontalAlign="Left" Width="50px" />
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn>
                    <ItemTemplate>
                        <asp:LinkButton ID="linkEdit" runat="server" Text="编辑" CommandName="Edit"></asp:LinkButton>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" Width="50px" />
                    <ItemStyle HorizontalAlign="Left" Width="50px" />
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn>
                    <ItemTemplate>
                        <asp:LinkButton ID="linkDelete" runat="server" OnClientClick='<%# "if(!BeforeDeletd("+DataBinder.Eval(Container,"DataItem.Code")+",\""+DataBinder.Eval(Container,"DataItem.Name")+"\"))return false;"%>' Text="删除" CommandName="delete"></asp:LinkButton>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" Width="50px" />
                    <ItemStyle HorizontalAlign="Left" Width="50px" />
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="Name" HeaderText="名称">
                    <HeaderStyle HorizontalAlign="Left" Width="480" />
                    <ItemStyle HorizontalAlign="Left" Width="480" />
                </telerik:GridBoundColumn>
                <telerik:GridDateTimeColumn DataField="CreateTime" HeaderText="创建时间" DataFormatString="{0:yyyy-MM-dd H:m:ss}">
                    <HeaderStyle HorizontalAlign="Left" Width="150px" />
                    <ItemStyle HorizontalAlign="Left" Width="150px" />
                </telerik:GridDateTimeColumn>
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
                            <h3>新闻类别明细</h3>
                        </div>
                        <div class="container">
                            <table>
                                <tr>
                                    <th>名称:<span class="required">*</span>
                                    </th>
                                    <td>
                                        <asp:HiddenField ID="hdfKey" runat="server" />
                                        <asp:TextBox ID="txtName" runat="server" Width="400px" CssClass="edit-text" MaxLength="50"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rqvTitle" runat="server" ControlToValidate="txtName"
                                            Display="Dynamic" ValidationGroup="message" ErrorMessage="<img src='/Image/invalid.gif' title='名称不能为空!' />"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <th class="multiLine">描述:
                                    </th>
                                    <td>
                                         <asp:TextBox ID="txtDescription" runat="server" Width="400px" TextMode="MultiLine" CssClass="edit-text"  MaxLength="100"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <div class="action">
                        <asp:Button ID="btnSave" runat="server" CommandName="Update" Text="保存" ValidationGroup="message" />
                        <asp:Button ID="btnCancel" runat="server" CommandName="Cancel" Text="取消" />
                    </div>
                </FormTemplate>
            </EditFormSettings>
        </MasterTableView>
        <ClientSettings>
            <Selecting AllowRowSelect="true" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
