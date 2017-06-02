<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/MasterList.master" AutoEventWireup="true" CodeBehind="RecruitFlowList.aspx.cs"
    Inherits="Cbvm.Vitae.Manage.DepartAdmin.RecruitFlowList" Theme="DepartAdmin" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="contentHead">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="contentPlaceCondition" runat="server">
    <asp:Panel ID="pnlCondition" runat="server">
        <table>
            <tr>
                <th>名称:</th>
                <td>
                    <asp:TextBox ID="prm_Name_" runat="server" Width="200px" CssClass="edit-text"></asp:TextBox>
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
    <div class="clear">
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentPlaceList" runat="server">
    <telerik:RadGrid ID="grdRecruitFlowSetted" runat="server" AutoGenerateColumns="false" OnPageIndexChanged="radGrid_PageIndexChanged"
        OnNeedDataSource="radGrid_NeedDataSource" OnItemDataBound="radGrid_ItemDataBound" OnItemCommand="radGrid_ItemCommand">
        <MasterTableView DataKeyNames="ID">
            <Columns>
                <telerik:GridBoundColumn DataField="Index" HeaderText="编号">
                    <HeaderStyle HorizontalAlign="Left" Width="40px" />
                    <ItemStyle HorizontalAlign="Left" Width="40px" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Name" HeaderText="名称">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Description" HeaderText="描述">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn UniqueName="TemplateColumn">
                    <ItemTemplate>
                        <asp:LinkButton ID="imgSubUp" runat="server" CommandName="subUp" CssClass="icons icon_up" />
                    </ItemTemplate>
                    <HeaderStyle Width="5%" />
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn UniqueName="TemplateColumn">
                    <ItemTemplate>
                        <asp:LinkButton ID="imgSubDown" runat="server" CommandName="subDown" CssClass="icons icon_down" />
                    </ItemTemplate>
                    <HeaderStyle Width="5%" />
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn>
                    <ItemTemplate>
                        <asp:LinkButton ID="linkEdit" runat="server" Text="编辑" CommandName="edit"></asp:LinkButton>
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
                            <h3>招聘流程项明细</h3>
                        </div>
                        <div class="container">
                            <table>
                                <tr>
                                    <th>名称:</th>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtName" MaxLength="200" CssClass="edit-text"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <th valign="top">描述:</th>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtDescription" TextMode="MultiLine" Rows="6" Columns="65" MaxLength="2000"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <div class="action">
                        <asp:Button ID="btnUpdate" runat="server" Text="保存" CommandName='<%# (Container is GridEditFormInsertItem)? "PerformInsert" : "Update" %>' />
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
