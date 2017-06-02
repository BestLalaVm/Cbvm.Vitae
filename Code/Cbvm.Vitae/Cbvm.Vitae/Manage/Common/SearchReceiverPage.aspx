﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/ThickBox.Master" AutoEventWireup="true" CodeBehind="SearchReceiverPage.aspx.cs" 
    Inherits="Cbvm.Vitae.Manage.Common.SearchReceiverPage"  Theme="BaseManage"%>

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
                <th>用户类型:</th>
                <td>
                    <asp:DropDownList runat="server" ID="prm_UserType_" Width="200px" />
                </td>
                <th>关键字:</th>
                <td>
                    <asp:TextBox runat="server" ID="prm_Keyword_" Width="180px"></asp:TextBox>

                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentPlaceAction" runat="server">
    <div class="right">
        <asp:Button runat="server" ID="btnSearch" OnClick="btnSearch_Click" Text="搜索" Width="60px" />
        <asp:Button runat="server" ID="btnReset" OnClick="btnReset_Click" Text="重置" Width="60px" />
    </div>
    <div class="clear">
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentPlaceList" runat="server">
    <telerik:RadGrid ID="grdTeacher" runat="server" OnNeedDataSource="radGrid_NeedDataSource" AllowPaging="true" AllowCustomPaging="true"
        AutoGenerateColumns="false" PageSize="20"
        OnPageIndexChanged="radGrid_PageIndexChanged">
        <MasterTableView DataKeyNames="UserName" PageSize="20">
            <Columns>
                <telerik:GridTemplateColumn>
                    <HeaderStyle HorizontalAlign="Left" Width="80px" />
                    <ItemStyle HorizontalAlign="Left" Width="80px" />
                    <ItemTemplate>
                        <asp:Button runat="server" ID="btnSelected" OnClick="btnSelected_Click" Text="选择" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="Index" HeaderText="序号">
                    <HeaderStyle HorizontalAlign="Left" Width="50px" />
                    <ItemStyle HorizontalAlign="Left" Width="50px" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="UserTypeString" HeaderText="用户类型">
                    <HeaderStyle Width="120px" HorizontalAlign="Left" />
                    <ItemStyle Width="120px" HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="UserName" HeaderText="用户名">
                    <HeaderStyle Width="150px" HorizontalAlign="Left" />
                    <ItemStyle Width="150px" HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Name" HeaderText="姓名">
                    <HeaderStyle Width="120px" HorizontalAlign="Left" />
                    <ItemStyle Width="120px" HorizontalAlign="Left" />
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
