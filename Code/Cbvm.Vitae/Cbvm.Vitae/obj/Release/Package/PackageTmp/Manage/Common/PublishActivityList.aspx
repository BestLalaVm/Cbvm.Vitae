<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/ThickBox.Master" AutoEventWireup="true"
    CodeBehind="PublishActivityList.aspx.cs" Inherits="Cbvm.Vitae.Manage.Common.PublishActivityList" Theme="ThickBox" %>
<%@ Import Namespace="Presentation.Cache" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .wrap-right .condition .caption {
            display: none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPlaceCondition" runat="server">
    <asp:Panel ID="pnlCondition" runat="server">
        <table>
            <tr>
                <th>标题:
                </th>
                <td>
                    <asp:TextBox ID="prm_Title_" runat="server" CssClass="edit-text" Width="200px"></asp:TextBox>
                </td>
                <th>活动时间从:
                </th>
                <td>
                    <telerik:raddatepicker id="prm_DateFrom_" runat="server">
                        <DateInput DateFormat="yyyy-MM-dd">
                        </DateInput>
                    </telerik:raddatepicker>
                </td>
                <th>到
                </th>
                <td>
                    <telerik:raddatepicker id="prm_DateTo_" runat="server">
                        <DateInput ID="DateInput1" DateFormat="yyyy-MM-dd" runat="server">
                        </DateInput>
                    </telerik:raddatepicker>
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
    <telerik:radgrid id="grdPublishActivity" runat="server" onneeddatasource="radGrid_NeedDataSource" allowpaging="False" allowcustompaging="False"
        onpageindexchanged="radGrid_PageIndexChanged" onitemdatabound="grdPublishActivity_ItemDataBound">
        <MasterTableView DataKeyNames="ID" AllowPaging="False" AllowCustomPaging="False">
            <Columns>
                <telerik:GridBoundColumn DataField="Index" HeaderText="序号">
                    <HeaderStyle HorizontalAlign="Left" Width="50px" />
                    <ItemStyle HorizontalAlign="Left" Width="50px" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Title" HeaderText="标题">
                    <HeaderStyle Width="250px" HorizontalAlign="Left" />
                    <ItemStyle Width="250px" HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn HeaderText="发布人">
                    <ItemTemplate>
                        <%#GlobalBaseDataCache.GetTeacherName((string)DataBinder.Eval((Container).DataItem,"Publisher"))%>
                    </ItemTemplate>
                    <HeaderStyle Width="100px" HorizontalAlign="Left" />
                    <ItemStyle Width="100px" HorizontalAlign="Left" />
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="活动类型">
                    <ItemTemplate>
                        <%#GlobalBaseDataCache.GetActivityTypeLabel((string)DataBinder.Eval((Container).DataItem,"ActivityType"))%>
                    </ItemTemplate>
                    <HeaderStyle Width="100px" HorizontalAlign="Left" />
                    <ItemStyle Width="100px" HorizontalAlign="Left" />
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn HeaderText="开始时间" DataField="BeginTime" DataFormatString="{0:yyyy-MM-dd}">
                    <HeaderStyle Width="90px" HorizontalAlign="Left" />
                    <ItemStyle Width="90px" HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="结束时间" DataField="EndTime" DataFormatString="{0:yyyy-MM-dd}">
                    <HeaderStyle Width="90px" HorizontalAlign="Left" />
                    <ItemStyle Width="90px" HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn HeaderText="活动状态">
                    <ItemTemplate>
                        <asp:LinkButton ID="linkStatus" runat="server" OnClick="linkStatus_Click" Text="开始应用"></asp:LinkButton>
                    </ItemTemplate>
                    <HeaderStyle Width="100px" HorizontalAlign="Left" />
                    <ItemStyle Width="100px" HorizontalAlign="Left" />
                </telerik:GridTemplateColumn>
            </Columns>
            <NoRecordsTemplate>
                <div>
                    没有记录!
                </div>
            </NoRecordsTemplate>
        </MasterTableView>
        <ClientSettings>
            <Selecting AllowRowSelect="true"></Selecting>
            <Scrolling AllowScroll="true" UseStaticHeaders="true" SaveScrollPosition="true" ScrollHeight="500px" />
        </ClientSettings>
    </telerik:radgrid>
</asp:Content>
