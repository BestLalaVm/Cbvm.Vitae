<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/ThickBox.Master" AutoEventWireup="true" CodeBehind="SearchEnterprisePage.aspx.cs" 
    Inherits="Cbvm.Vitae.Manage.Common.SearchEnterprisePage" Theme="BaseManage"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .condition .caption {
            display: none;
        }

        body {
            margin: 0px !important;
        }
    </style>
    <script type="text/javascript">
        function RowClickEx() {
            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPlaceCondition" runat="server">
    <asp:Panel ID="pnlCondition" runat="server">
        <table>
            <tr>
                <th>企业名称:</th>
                <td>
                    <asp:TextBox ID="prm_Name_" runat="server" CssClass="edit-text"></asp:TextBox>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentPlaceAction" runat="server">
    <div class="left">
        <asp:Button runat="server" ID="btnSelected" OnClick="btnSelected_Click" Text="选择" />
    </div>
    <div class="right">
        <asp:Button runat="server" ID="btnSearch" OnClick="btnSearch_Click" Text="搜索" Width="60px" />
        <asp:Button runat="server" ID="btnReset" OnClick="btnReset_Click" Text="重置" Width="60px" />
    </div>
    <div class="clear">
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentPlaceList" runat="server">
    <telerik:RadGrid ID="grdStudent" runat="server" OnNeedDataSource="radGrid_NeedDataSource" AllowPaging="true" AllowCustomPaging="true"
        AutoGenerateColumns="false" PageSize="20"
        OnPageIndexChanged="radGrid_PageIndexChanged">
        <MasterTableView DataKeyNames="Code" PageSize="20">
            <Columns>
                <telerik:GridTemplateColumn>
                    <HeaderStyle HorizontalAlign="Left" Width="40px" />
                    <ItemStyle HorizontalAlign="Left" Width="40px" />
                    <ItemTemplate>
                        <asp:CheckBox runat="server" ID="chkEnterprise" Checked='<%#DataBinder.Eval(Container,"DataItem.Selected") %>' />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="Index" HeaderText="序号">
                    <HeaderStyle HorizontalAlign="Left" Width="50px" />
                    <ItemStyle HorizontalAlign="Left" Width="50px" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Code" HeaderText="编码">
                    <HeaderStyle Width="120px" HorizontalAlign="Left" />
                    <ItemStyle Width="120px" HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Name" HeaderText="企业名称">
                    <HeaderStyle Width="150px" HorizontalAlign="Left" />
                    <ItemStyle Width="150px" HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
            </Columns>
            <NoRecordsTemplate>
                <div>
                    没有记录!
                </div>
            </NoRecordsTemplate>
        </MasterTableView>
    </telerik:RadGrid>
</asp:Content>

