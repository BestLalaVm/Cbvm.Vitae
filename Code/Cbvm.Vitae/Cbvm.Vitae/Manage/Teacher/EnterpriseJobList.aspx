<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/MasterList.master" AutoEventWireup="true" CodeBehind="EnterpriseJobList.aspx.cs" Inherits="Cbvm.Vitae.Manage.Teacher.EnterpriseJobList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentHead" runat="server">
    <script type="text/javascript">
        function RowClickEx() {
            return false;
        }
        function ViewDetail(jobCode) {
            window.parent.ShowIframeForm("查看岗位明细要求", "ViewJobDetail.aspx?JobCode=" + jobCode, "910px", "500px");
        }

        function EnterJobRecommendPage(jobCode) {
            window.parent.ShowIframeForm("查看岗位明细要求", "EnterJobRecommendPage.aspx?JobCode=" + jobCode, "910px", "500px");
        }
        
        function ViewReferralList(jobCode) {
            window.parent.ShowIframeForm("查看已推荐学生列表", "AlreadyReferralStudentList.aspx?JobCode=" + jobCode, "910px", "500px");
        }

    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentPlaceCondition" runat="server">
    <asp:Panel ID="pnlCondition" runat="server">
        <table>
            <tr>
                <th>公司名称:
                </th>
                <td>
                    <asp:TextBox ID="prm_EnterpriseName_" runat="server" CssClass="edit-text"></asp:TextBox>
                </td>
                <th>职位名称:
                </th>
                <td>
                    <asp:TextBox ID="prm_Name_" runat="server" CssClass="edit-text"></asp:TextBox>
                </td>
                <th>工作地点:
                </th>
                <td>
                    <asp:TextBox ID="prm_WorkPlace_" runat="server" CssClass="edit-text">
                    </asp:TextBox>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPlaceAction" runat="server">
    <div class="right">
        <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="搜索"></asp:Button>
        <asp:Button ID="btnReset" runat="server" OnClick="btnReset_Click" Text="重置"></asp:Button>
    </div>
    <div class="clear">
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentPlaceList" runat="server">
    <telerik:radgrid id="grdEnterpriseJob" runat="server" onneeddatasource="radGrid_NeedDataSource"
        autogeneratecolumns="false" pagesize="30" allowcustompaging="true" allowpaging="true">
        <MasterTableView>
            <Columns>
                <telerik:GridBoundColumn DataField="Index" HeaderText="序号">
                    <HeaderStyle HorizontalAlign="Left" Width="50px" />
                    <ItemStyle HorizontalAlign="Left" Width="50px" />
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn>
                    <HeaderStyle HorizontalAlign="Left" Width="80px" />
                    <ItemStyle HorizontalAlign="Left" Width="80px" />
                    <ItemTemplate>
                        <a href="EnterJobRecommendPage.aspx?JobCode=<%#Eval("Code") %>" title="推荐职员" class="grid-edit">
                            <span>推荐职员</span>
                        </a>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn>
                    <HeaderStyle HorizontalAlign="Left" Width="120px" />
                    <ItemStyle HorizontalAlign="Left" Width="120px" />
                    <ItemTemplate>
                        <a href='#' title="查看岗位明细" onclick="ViewDetail('<%#Eval("Code") %>')" class="grid-edit">
                            <span>查看岗位要求</span>
                        </a>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn>
                    <HeaderStyle HorizontalAlign="Left" Width="120px" />
                    <ItemStyle HorizontalAlign="Left" Width="120px" />
                    <ItemTemplate>
                        <a href='#' title="查看岗位明细" onclick="ViewReferralList('<%#Eval("Code") %>')" class="grid-edit">
                            <span>查看推荐人员</span>
                        </a>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="EnterpriseName" HeaderText="公司名称">
                    <HeaderStyle Width="200px" HorizontalAlign="Left" />
                    <ItemStyle Width="200px" HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Name" HeaderText="职位名称">
                    <HeaderStyle Width="150px" HorizontalAlign="Left" />
                    <ItemStyle Width="150px" HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Num" HeaderText="数量">
                    <HeaderStyle Width="60px" HorizontalAlign="Left" />
                    <ItemStyle Width="60px" HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="SexRequired" HeaderText="性别要求">
                    <HeaderStyle Width="90px" HorizontalAlign="Left" />
                    <ItemStyle Width="90px" HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Education" HeaderText="学历要求">
                    <HeaderStyle Width="90px" HorizontalAlign="Left" />
                    <ItemStyle Width="90px" HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="WorkPlace" HeaderText="工作地点">
                    <HeaderStyle Width="200px" HorizontalAlign="Left" />
                    <ItemStyle Width="200px" HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="SalaryScope" HeaderText="薪资范围">
                    <HeaderStyle Width="80px" HorizontalAlign="Left" />
                    <ItemStyle Width="80px" HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="开始时间" DataField="StartTime" DataFormatString="{0:yyyy-MM-dd}">
                    <ItemStyle Width="90px" HorizontalAlign="Left" />
                    <HeaderStyle Width="90px" HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="结束时间" DataField="EndTime" DataFormatString="{0:yyyy-MM-dd}">
                    <ItemStyle Width="90px" HorizontalAlign="Left" />
                    <HeaderStyle Width="90px" HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
            </Columns>
            <NoRecordsTemplate>
                <div>
                    没有记录!
                </div>
            </NoRecordsTemplate>
        </MasterTableView>
    </telerik:radgrid>
</asp:Content>
