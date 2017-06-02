<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/MasterList.master" AutoEventWireup="true" CodeBehind="JobRequestList.aspx.cs"
    Inherits="Cbvm.Vitae.Manage.Student.JobRequestList" Theme="StudentManage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentHead" runat="server">
    <style type="text/css">
        .body-content .main .wrap-container .wrap-right .action td {
            text-align: left;
        }
    </style>
    <script type="text/javascript">
        function ViewDetail(jobCode) {
            window.parent.ShowIframeForm("查看岗位明细要求", "ViewJobDetail.aspx?JobCode=" + jobCode, "910px", "500px");
        }

        function ViewNote(desc) {
            parent.showAlterResultMsg("查看备注", desc);
        }

        function RowClickEx() {
            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPlaceCondition" runat="server">
    <asp:Panel ID="pnlCondition" runat="server">
        <table>
            <tr>
                <th>应聘公司:
                </th>
                <td>
                    <asp:TextBox ID="prm_EnterpriseName_" runat="server" Width="120px" CssClass="edit-text"></asp:TextBox>
                </td>
                <th>应聘岗位:
                </th>
                <td>
                    <asp:TextBox ID="prm_JobName_" runat="server" Width="120px" CssClass="edit-text"></asp:TextBox>
                </td>
                <th>应聘时间从:
                </th>
                <td>
                    <telerik:RadDatePicker ID="prm_RequestTimeFrom_" runat="server" Width="100px">
                        <DateInput DateFormat="yyyy-MM-dd">
                        </DateInput>
                    </telerik:RadDatePicker>
                </td>
                <th>到:
                </th>
                <td>
                    <telerik:RadDatePicker ID="prm_RequestTimeTo_" runat="server" Width="100px">
                        <DateInput DateFormat="yyyy-MM-dd">
                        </DateInput>
                    </telerik:RadDatePicker>
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
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentPlaceList" runat="server">
    <telerik:RadGrid ID="grdJobRequest" runat="server" OnNeedDataSource="radGrid_NeedDataSource"
        OnPageIndexChanged="radGrid_PageIndexChanged" AutoGenerateColumns="false" PageSize="30" AllowCustomPaging="true" AllowPaging="true">
        <MasterTableView>
            <Columns>
                <telerik:GridBoundColumn DataField="Index" HeaderText="序号">
                    <HeaderStyle HorizontalAlign="Left" Width="50px" />
                    <ItemStyle HorizontalAlign="Left" Width="50px" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="EnterpriseName" HeaderText="应聘公司">
                    <HeaderStyle Width="200px" HorizontalAlign="Left" />
                    <ItemStyle Width="200px" HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="JobName" HeaderText="应聘岗位">
                    <HeaderStyle Width="100px" HorizontalAlign="Left" />
                    <ItemStyle Width="100px" HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="JobStage" HeaderText="应聘状态">
                    <HeaderStyle HorizontalAlign="Left" Width="120px"/>
                    <ItemStyle HorizontalAlign="Left" Width="120px"/>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="ContactName" HeaderText="联系人">
                    <HeaderStyle Width="80px" HorizontalAlign="Left" />
                    <ItemStyle Width="80px" HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="RequestTime" HeaderText="应聘时间" DataFormatString="{0:yyyy-MM-dd hh:mm}">
                    <HeaderStyle Width="140px" HorizontalAlign="Left" />
                    <ItemStyle Width="140px" HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                 <telerik:GridTemplateColumn>
                    <ItemTemplate>
                        <a href='#' title="查看备注" onclick="ViewNote('<%#Eval("StageDescription") %>')" class="grid-edit">
                            <span>查看备注</span>
                        </a>
                    </ItemTemplate>
                    <HeaderStyle Width="120px" HorizontalAlign="Left" />
                    <ItemStyle Width="120px" HorizontalAlign="Left" />
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn>
                    <ItemTemplate>
                        <a href='#' title="查看岗位明细" onclick="ViewDetail('<%#Eval("JobCode") %>')" class="grid-edit">
                            <span>查看岗位要求</span>
                        </a>
                    </ItemTemplate>
                    <HeaderStyle Width="120px" HorizontalAlign="Left" />
                    <ItemStyle Width="120px" HorizontalAlign="Left" />
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
