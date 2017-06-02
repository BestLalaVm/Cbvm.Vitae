<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/MasterList.master" AutoEventWireup="true"
    CodeBehind="JobRequesterList.aspx.cs" Inherits="Cbvm.Vitae.Manage.Enterprise.JobRequesterList" Theme="EnterpriseManage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentHead" runat="server">
    <style type="text/css">
        .list-container input[type='button'],
        .list-container input[type='submit'] {
            width:90px !important; 
        }
    </style>
    <script type="text/javascript">
        function ViewResume(studentNum, jobRequestID) {
            window.parent.ShowIframeForm("求职者简历", "ViewRequesterResume.aspx?StudentNum=" + studentNum + "&JobRequestID=" + jobRequestID, "980px", "550px");
        }

        window.parent.IframeRemovedFunc = function () {
            __doPostBack('ctl00$ctl00$ContentPlaceHolder1$contentPlaceAction$btnSearch', '');
        }
        function RowClickEx() {
            return false;
        }

        function ViewTruthAuthenticatedList(studentNum,studentName) {
            window.parent.ShowIframeForm(studentName+"的企业信息验证记录", "../Common/StudentTruthAuthenticatedList.aspx?studentNum=" + studentNum, "910px", "500px", {
                hideOverFlow: true,
                isShowFooter: true,
                closeCallBackHandler: function () {
                    // window.location.href = window.location.href;
                }
            });
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentPlaceCondition" runat="server">
    <asp:Panel ID="pnlCondition" runat="server">
        <table>
            <tr>
                <th>职位:
                </th>
                <td>
                    <asp:TextBox ID="prm_JobName_" runat="server" Width="200px" CssClass="edit-text"></asp:TextBox>
                </td>
                <th>申请人:
                </th>
                <td>
                    <asp:TextBox ID="prm_StudentName_" runat="server" Width="200px" CssClass="edit-text"></asp:TextBox>
                </td>
                <th>状态:
                </th>
                <td>
                    <asp:DropDownList ID="prm_RecruitFlowID_" runat="server" Width="124px"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th>推荐人:</th>
                <td>
                    <asp:TextBox ID="prm_Referraler_" runat="server" Width="200px" CssClass="edit-text"></asp:TextBox></td>
                <th>申请时间从:</th>
                <td>
                    <telerik:raddatepicker id="prm_RequestTimeFrom_" runat="server">
                        <DateInput DateFormat="yyyy-MM-dd" runat="server">
                        </DateInput>
                    </telerik:raddatepicker>
                </td>
                <th>到:</th>
                <td>
                    <telerik:raddatepicker id="prm_RequestTimeTo_" runat="server">
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
        <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="搜索"></asp:Button>
        <asp:Button ID="btnReset" runat="server" OnClick="btnReset_Click" Text="重置"></asp:Button>
    </div>
    <div class="clear">
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentPlaceList" runat="server">
    <div class="list-container">
        <telerik:radgrid id="grdEnterpriseJob" runat="server" autogeneratecolumns="false"
            onneeddatasource="radGrid_NeedDataSource" OnItemDataBound="grdEnterpriseJob_ItemDataBound">
            <MasterTableView DataKeyNames="StudentNum,JobCode,ID">
                <Columns>
                    <telerik:GridBoundColumn DataField="Index" HeaderText="编号">
                        <HeaderStyle HorizontalAlign="Left" Width="50px" />
                        <ItemStyle HorizontalAlign="Left" Width="50px" />
                    </telerik:GridBoundColumn>
                    <telerik:GridTemplateColumn>
                        <ItemTemplate>
                            <a href="ViewRequesterResume.aspx?StudentNum=<%#Eval("StudentNum") %>&JobRequestID=<%#Eval("ID") %>" target="_blank">查看简历
                            </a>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" Width="80px" />
                        <ItemStyle HorizontalAlign="Left" Width="80px" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn>
                        <ItemTemplate>
                            <asp:Button ID="btnSendAuthenticate" runat="server"  Text="真实性验证" OnClick="btnSendAuthenticate_Click" />
                        </ItemTemplate>
                         <HeaderStyle HorizontalAlign="Left" Width="105px" />
                        <ItemStyle HorizontalAlign="Left" Width="105px" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridBoundColumn DataField="AuthenticatedRequestTime" HeaderText="验证请求时间">
                        <HeaderStyle HorizontalAlign="Left" Width="150px" />
                        <ItemStyle HorizontalAlign="Left" Width="150px" />
                    </telerik:GridBoundColumn>
                     <telerik:GridTemplateColumn>
                        <ItemTemplate>
                            <a href="#" onclick="ViewTruthAuthenticatedList('<%#Eval("StudentNum") %>','<%#Eval("StudentName") %>')" target="_self">查看验证记录</a>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" Width="120px" />
                        <ItemStyle HorizontalAlign="Left" Width="120px" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridBoundColumn DataField="JobName" HeaderText="职位名称">
                        <HeaderStyle HorizontalAlign="Left" Width="200px" />
                        <ItemStyle HorizontalAlign="Left" Width="200px" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="RequestStatus" HeaderText="状态">
                        <HeaderStyle HorizontalAlign="Left" Width="80px" />
                        <ItemStyle HorizontalAlign="Left" Width="80px" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="JobNum" HeaderText="招聘人数">
                        <HeaderStyle HorizontalAlign="Left" Width="100px" />
                        <ItemStyle HorizontalAlign="Left" Width="100px" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="StudentName" HeaderText="申请人">
                        <HeaderStyle HorizontalAlign="Left" Width="80px" />
                        <ItemStyle HorizontalAlign="Left" Width="80px" />
                    </telerik:GridBoundColumn>
                    <telerik:GridCheckBoxColumn DataField="IsInviated" HeaderText="来自邀请">
                        <HeaderStyle HorizontalAlign="Left" Width="120px" />
                        <ItemStyle HorizontalAlign="Left" Width="120px" />
                    </telerik:GridCheckBoxColumn>
                    <telerik:GridBoundColumn DataField="InviatedDateValue" HeaderText="邀请时间" DataFormatString="{0:yyyy-MM-dd hh:mm:ss}">
                        <HeaderStyle HorizontalAlign="Left" Width="150px" />
                        <ItemStyle HorizontalAlign="Left" Width="150px" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Referralers" HeaderText="推荐人">
                        <HeaderStyle HorizontalAlign="Left" Width="150px" />
                        <ItemStyle HorizontalAlign="Left" Width="150px" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Sex" HeaderText="性别">
                        <HeaderStyle HorizontalAlign="Left" Width="60px" />
                        <ItemStyle HorizontalAlign="Left" Width="60px" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="StudentTelephone" HeaderText="联系方式">
                        <HeaderStyle HorizontalAlign="Left" Width="110px" />
                        <ItemStyle HorizontalAlign="Left" Width="110px" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="RequestTime" HeaderText="申请时间" DataFormatString="{0:yyyy-MM-dd hh:mm:ss}">
                        <HeaderStyle HorizontalAlign="Left" Width="150px" />
                        <ItemStyle HorizontalAlign="Left" Width="150px" />
                    </telerik:GridBoundColumn>
                </Columns>
                <NoRecordsTemplate>
                    没有记录.
                </NoRecordsTemplate>
            </MasterTableView>
            <ClientSettings>
                <Selecting AllowRowSelect="true" />
            </ClientSettings>
        </telerik:radgrid>
    </div>
</asp:Content>
