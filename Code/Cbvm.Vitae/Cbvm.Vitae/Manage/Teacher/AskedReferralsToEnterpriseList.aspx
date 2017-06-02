<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/MasterList.master" AutoEventWireup="true" CodeBehind="AskedReferralsToEnterpriseList.aspx.cs" Inherits="Cbvm.Vitae.Manage.Teacher.AskedReferralsToEnterpriseList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentHead" runat="server">
    <script type="text/javascript">
        function RowClickEx() {
            return false;
        }
        function ViewDetail(jobCode) {
            window.parent.ShowIframeForm("查看岗位明细要求", "ViewJobDetail.aspx?JobCode=" + jobCode, "910px", "500px");
        }
        function EditReferralsToEnterprisePage(referralID, studentName, studentNum) {
            window.parent.ShowIframeForm("编辑推荐信息", "EditReferralsToEnterprisePage.aspx?ReferralsQueueID=" + referralID + "&StudentNum=" + studentNum + "&StudentName=" + studentName, "500px", "280px", { hideOverFlow: true, isShowFooter: true });
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentPlaceCondition" runat="server">
    <asp:Panel ID="pnlCondition" runat="server">
        <table>
            <tr>
                <th>专业名称:
                </th>
                <td>
                    <asp:DropDownList runat="server" ID="prm_MarjorCode_" Width="160px" />
                </td>
                <th>学生姓名:
                </th>
                <td>
                    <asp:TextBox ID="prm_StudentName_" runat="server" CssClass="edit-text"></asp:TextBox>
                </td>

                <th>学号:
                </th>
                <td>
                    <asp:TextBox ID="prm_StudentNum_" runat="server" CssClass="edit-text">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>公司名称:
                </th>
                <td>
                    <asp:TextBox ID="prm_EnterpriseName_" runat="server" CssClass="edit-text"></asp:TextBox>
                </td>
                <th>职位名称:
                </th>
                <td>
                    <asp:TextBox ID="prm_JobName_" runat="server" CssClass="edit-text"></asp:TextBox>
                </td>
                <th>状态:
                </th>
                <td>
                    <asp:DropDownList runat="server" ID="prm_ReferralStateFromReferraler_" Width="160px" />
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
    <telerik:RadGrid ID="grdEnterpriseJob" runat="server" OnNeedDataSource="radGrid_NeedDataSource"
        AutoGenerateColumns="false" PageSize="30" AllowCustomPaging="true" AllowPaging="true">
        <MasterTableView DataKeyNames="ID">
            <Columns>
                <telerik:GridBoundColumn DataField="Index" HeaderText="序号">
                    <HeaderStyle HorizontalAlign="Left" Width="50px" />
                    <ItemStyle HorizontalAlign="Left" Width="50px" />
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn HeaderText="">
                    <HeaderStyle HorizontalAlign="Left" Width="80px" />
                    <ItemStyle HorizontalAlign="Left" Width="80px" />
                    <ItemTemplate>
                        <%#DataBinder.Eval(Container,"DataItem.RenderHtml") %>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn>
                    <HeaderStyle HorizontalAlign="Left" Width="120px" />
                    <ItemStyle HorizontalAlign="Left" Width="120px" />
                    <ItemTemplate>
                        <a href='#' title="查看岗位明细" onclick="ViewDetail('<%#Eval("JobCode") %>')" class="grid-edit">
                            <span>查看岗位要求</span>
                        </a>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="ReferralState" HeaderText="状态">
                    <HeaderStyle Width="90px" HorizontalAlign="Left" />
                    <ItemStyle Width="90px" HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="AskedReferralNote" HeaderText="邀请推荐描述">
                    <HeaderStyle Width="200px" HorizontalAlign="Left" />
                    <ItemStyle Width="200px" HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="邀请时间" DataField="RequestTime" DataFormatString="{0:yyyy-MM-dd hh:mm}">
                    <ItemStyle Width="150px" HorizontalAlign="Left" />
                    <HeaderStyle Width="150px" HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="StudentName" HeaderText="学生姓名">
                    <HeaderStyle Width="80px" HorizontalAlign="Left" />
                    <ItemStyle Width="80px" HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="EnterpriseName" HeaderText="公司名称">
                    <HeaderStyle Width="200px" HorizontalAlign="Left" />
                    <ItemStyle Width="200px" HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="JobName" HeaderText="职位名称">
                    <HeaderStyle Width="150px" HorizontalAlign="Left" />
                    <ItemStyle Width="150px" HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="StudentNum" HeaderText="学号">
                    <HeaderStyle Width="120px" HorizontalAlign="Left" />
                    <ItemStyle Width="120px" HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="StudentMarjorName" HeaderText="专业名称">
                    <HeaderStyle Width="120px" HorizontalAlign="Left" />
                    <ItemStyle Width="120px" HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="StudentClass" HeaderText="班级">
                    <HeaderStyle Width="80px" HorizontalAlign="Left" />
                    <ItemStyle Width="80px" HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="SexRequired" HeaderText="性别要求">
                    <HeaderStyle Width="80px" HorizontalAlign="Left" />
                    <ItemStyle Width="80px" HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Education" HeaderText="学历要求">
                    <HeaderStyle Width="100px" HorizontalAlign="Left" />
                    <ItemStyle Width="100px" HorizontalAlign="Left" />
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
                    <ItemStyle Width="120px" HorizontalAlign="Left" />
                    <HeaderStyle Width="120px" HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="结束时间" DataField="EndTime" DataFormatString="{0:yyyy-MM-dd}">
                    <ItemStyle Width="120px" HorizontalAlign="Left" />
                    <HeaderStyle Width="120px" HorizontalAlign="Left" />
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
