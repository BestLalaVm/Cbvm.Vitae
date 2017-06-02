<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/MasterList.master" AutoEventWireup="true"
    CodeBehind="MessageManage.aspx.cs" Inherits="Cbvm.Vitae.Manage.DepartAdmin.MessageManage" Theme="DepartAdmin" %>

<%@ Register Src="~/Manage/UserControl/EditorControl.ascx" TagName="EditorControl"
    TagPrefix="uc" %>

<asp:Content ID="Content4" runat="server" ContentPlaceHolderID="contentHead">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceCondition" runat="server">
    <asp:Panel ID="pnlCondition" runat="server">
        <table>
            <tr>
                <th>新闻标题:
                </th>
                <td>
                    <asp:TextBox ID="prm_Title_" runat="server" Width="260px" CssClass="edit-text"></asp:TextBox>
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
    <telerik:radgrid id="grdMessage" runat="server" autogeneratecolumns="false" onitemdatabound="grdMessage_ItemDataBound"
        onneeddatasource="radGrid_NeedDataSource" ondeletecommand="radGrid_DeleteCommand"
        onupdatecommand="grdMessage_UpdateCommand">
        <MasterTableView DataKeyNames="ID">
            <Columns>
                <telerik:GridBoundColumn DataField="Index" HeaderText="编号">
                    <HeaderStyle HorizontalAlign="Left" Width="40px" />
                    <ItemStyle HorizontalAlign="Left" Width="40px" />
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
                        <asp:HiddenField ID="HiddenField1" runat="server" Value='<%#Eval("ID") %>' />
                        <asp:LinkButton ID="linkDelete" runat="server" OnClientClick='<%# "if(!BeforeDeletd("+DataBinder.Eval(Container,"DataItem.ID")+",\""+DataBinder.Eval(Container,"DataItem.Title")+"\"))return false;"%>' Text="删除" CommandName="delete"></asp:LinkButton>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" Width="50px" />
                    <ItemStyle HorizontalAlign="Left" Width="50px" />
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="Title" HeaderText="标题">
                    <HeaderStyle HorizontalAlign="Left" Width="480" />
                    <ItemStyle HorizontalAlign="Left" Width="480" />
                </telerik:GridBoundColumn>
                <telerik:GridDateTimeColumn DataField="CreateTime" HeaderText="创建时间" DataFormatString="{0:yyyy-MM-dd H:m:ss}">
                    <HeaderStyle HorizontalAlign="Left" Width="150px" />
                    <ItemStyle HorizontalAlign="Left" Width="150px" />
                </telerik:GridDateTimeColumn>
                <telerik:GridDateTimeColumn DataField="LastUpdateTime" HeaderText="修改时间" DataFormatString="{0:yyyy-MM-dd H:m:ss}">
                    <HeaderStyle HorizontalAlign="Left" Width="150px" />
                    <ItemStyle HorizontalAlign="Left" Width="150px" />
                </telerik:GridDateTimeColumn>
                <telerik:GridTemplateColumn HeaderText="发布">
                    <HeaderStyle HorizontalAlign="Left" Width="40px" />
                    <ItemStyle HorizontalAlign="Left" Width="40px" />
                    <ItemTemplate>
                        <asp:CheckBox ID="chkIsOnline" runat="server" Checked='<%#Eval("IsOnline") %>' OnCheckedChanged="chkIsOnline_CheckedChanged"
                            AutoPostBack="true" />
                    </ItemTemplate>
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
                            <h3>新闻明细</h3>
                        </div>
                        <div class="container">
                            <table>
                                <tr>
                                    <th>主题:<span class="required">*</span>
                                    </th>
                                    <td>
                                        <asp:HiddenField ID="hdfKey" runat="server" />
                                        <asp:TextBox ID="txtTitle" runat="server" Width="660px" CssClass="edit-text" Height="20px" MaxLength="200"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rqvTitle" runat="server" ControlToValidate="txtTitle"
                                            Display="Dynamic" ValidationGroup="message" ErrorMessage="<img src='/Image/invalid.gif' title='主题不能为空!' />"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <th class="multiLine">内容:
                                    </th>
                                    <td>
                                        <uc:EditorControl ID="edtControl" runat="server" EditorHeight="400" EditorWidth="750" />
                                    </td>
                                </tr>
                                <tr>
                                    <th></th>
                                    <td>
                                        <asp:CheckBox ID="chkIsOnline" runat="server" Text="发布" />
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
    </telerik:radgrid>
</asp:Content>
