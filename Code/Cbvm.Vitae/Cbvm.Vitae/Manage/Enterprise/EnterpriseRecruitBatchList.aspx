<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/MasterList.master" AutoEventWireup="true" CodeBehind="EnterpriseRecruitBatchList.aspx.cs"
    Inherits="Cbvm.Vitae.Manage.Enterprise.EnterpriseRecruitBatchList" Theme="EnterpriseManage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentHead" runat="server">
    <script type="text/javascript">
        function OpenDetail(batchId) {
            window.location.href = "EnterpriseRecruitBatchDetail.aspx?Id=" + batchId;
        }

        window.parent.IframeRemovedFunc = function () {
            __doPostBack('ctl00$ctl00$ContentPlaceHolder1$contentPlaceAction$btnSearch', '');
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPlaceCondition" runat="server">
    <asp:Panel ID="pnlCondition" runat="server">
        <table>
            <tr>
                <th>批次名称:</th>
                <td>
                    <asp:TextBox ID="prm_Title_" runat="server" Width="260px" CssClass="edit-text"></asp:TextBox>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentPlaceAction" runat="server">
    <div class="left">
        <input type="button" value="新增" onclick="OpenDetail()" style="height: 25px;" />
    </div>
    <div class="right">
        <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="搜索"></asp:Button>
        <asp:Button ID="btnReset" runat="server" OnClick="btnReset_Click" Text="重置"></asp:Button>
    </div>
    <div class="clear">
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentPlaceList" runat="server">
    <telerik:RadGrid ID="grdRecruitBatch" runat="server" AutoGenerateColumns="false" OnPageIndexChanged="radGrid_PageIndexChanged"
        OnNeedDataSource="radGrid_NeedDataSource" OnDeleteCommand="radGrid_DeleteCommand">
        <MasterTableView DataKeyNames="ID">
            <Columns>
                <telerik:GridBoundColumn DataField="Index" HeaderText="编号">
                    <HeaderStyle HorizontalAlign="Left" Width="40px" />
                    <ItemStyle HorizontalAlign="Left" Width="40px" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Title" HeaderText="批次名称">
                    <HeaderStyle HorizontalAlign="Left" Width="250px" />
                    <ItemStyle HorizontalAlign="Left" Width="250px" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="RequestNum" HeaderText="申请人数">
                    <HeaderStyle HorizontalAlign="Left" Width="80px" />
                    <ItemStyle HorizontalAlign="Left" Width="80px" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="ProcessLessNum" HeaderText="未处理人数">
                    <HeaderStyle HorizontalAlign="Left" Width="80px" />
                    <ItemStyle HorizontalAlign="Left" Width="80px" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="InterviewNum" HeaderText="处理中人数">
                    <HeaderStyle HorizontalAlign="Left" Width="80px" />
                    <ItemStyle HorizontalAlign="Left" Width="80px" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="PassedNum" HeaderText="录用人数">
                    <HeaderStyle HorizontalAlign="Left" Width="80px" />
                    <ItemStyle HorizontalAlign="Left" Width="80px" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="DenyNum" HeaderText="淘汰人数">
                    <HeaderStyle HorizontalAlign="Left" Width="80px" />
                    <ItemStyle HorizontalAlign="Left" Width="80px" />
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn>
                    <ItemTemplate>
                        <%--<a href="#" onclick='OpenDetail(<%#Eval("ID") %>)'><span>查看</span></a>--%>
                        <a href="EnterpriseRecruitBatchDetail.aspx?Id=<%#Eval("ID") %>" class="grid-edit"><span>查看</span></a>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" Width="40px" />
                    <ItemStyle HorizontalAlign="Left" Width="40px" />
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn>
                    <ItemTemplate>
                        <asp:LinkButton ID="linkDelete" runat="server" OnClientClick='<%# "if(!BeforeDeletd("+DataBinder.Eval(Container,"DataItem.ID")+",\""+DataBinder.Eval(Container,"DataItem.Title")+"\"))return false;"%>' Text="删除" CommandName="delete"></asp:LinkButton>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" Width="40px" />
                    <ItemStyle HorizontalAlign="Left" Width="40px" />
                </telerik:GridTemplateColumn>
            </Columns>
            <NoRecordsTemplate>
                <div class="empty">
                    没有记录
                </div>
            </NoRecordsTemplate>
        </MasterTableView>
        <ClientSettings>
            <Selecting AllowRowSelect="true" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
