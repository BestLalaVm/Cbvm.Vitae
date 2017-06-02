<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/SubMaster.Master" AutoEventWireup="true" CodeBehind="EnterJobRecommendPage.aspx.cs" Inherits="Cbvm.Vitae.Manage.Teacher.EnterJobRecommendPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        table th {
            width: 80px;
        }

        table tr th:first-child {
            width: 110px;
            text-align: right;
            vertical-align: top;
        }

        .action {
            padding: 5px;
            background-color: rgb(249, 243, 243);
            border-top-right-radius: 15px;
            border-top-left-radius: 15px;
            margin: 0px !important;
            text-align: left !important;
        }

        .list {
            width: 500px;
        }

        body {
            margin: 0px !important;
        }

        .student-list-bar {
            margin-top: 10px;
            padding-left: 10px;
        }
    </style>
    <script type="text/javascript">
        function GetComponentSize(current, config) {
            return {
                Width: "60px"
            };
        }

        function OpenStudentList() {
            window.parent.ShowIframeForm("选择学生信息", "../Common/SearchStudentPage.aspx", "910px", "500px", {
                hideOverFlow: true,
                isShowFooter: true,
                closeCallBackHandler: function () {
                    window.location.href = window.location.href;
                }
            });
        }

        function GoBackJobList() {
            window.location.href = "EnterpriseJobList.aspx";
        }

        function RefreshData() {
            window.location.href = window.location.href;
        }
    </script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="action">
        <asp:Button runat="server" ID="btnSubmit" OnClick="btnSubmit_Click" Text="推荐" />
        <input type="button" id="btnRefresh" value="刷新" onclick="RefreshData()" style="height:25px;" />
        <input type="button" id="btnGoBack" value="返回列表" onclick="GoBackJobList()" style="height:25px;"  />
    </div>

    <table width="100%">
        <tr>
            <th>公司名称:</th>
            <td colspan="3">
                <asp:Literal runat="server" ID="ltlEnterpriseName"></asp:Literal>
            </td>
        </tr>
        <tr>
            <th>职位名称:</th>
            <td style="width: 300px;">
                <asp:Literal runat="server" ID="ltlJobName"></asp:Literal>
            </td>
            <th>招聘人数:</th>
            <td>
                <asp:Literal runat="server" ID="ltlNum"></asp:Literal>
            </td>
        </tr>
        <tr>
            <th>工作地点:</th>
            <td colspan="3">
                <asp:Literal runat="server" ID="ltlWorkPlace"></asp:Literal>
            </td>
        </tr>
        <tr>
            <th>备注:</th>
            <td colspan="3">
                <asp:TextBox runat="server" ID="txtNote" Width="390px" Height="60px" TextMode="MultiLine" CssClass="multi-edit-text" MaxLength="150"></asp:TextBox>
            </td>
        </tr>
    </table>
    <div class="student-list">
        <div>推荐人列表</div>
        <telerik:RadGrid ID="grdStudent" runat="server" OnNeedDataSource="grdStudent_NeedDataSource"
            OnDeleteCommand="grdStudent_DeleteCommand"
            AutoGenerateColumns="false" PageSize="30" AllowCustomPaging="false" AllowPaging="false">
            <MasterTableView DataKeyNames="StudentNum" AllowPaging="false" AllowCustomPaging="false">
                <Columns>
                    <telerik:GridBoundColumn DataField="Index" HeaderText="序号">
                        <HeaderStyle HorizontalAlign="Left" Width="50px" />
                        <ItemStyle HorizontalAlign="Left" Width="50px" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="StudentNum" HeaderText="学号">
                        <HeaderStyle Width="150px" HorizontalAlign="Left" />
                        <ItemStyle Width="150px" HorizontalAlign="Left" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="姓名" DataField="Name">
                        <ItemStyle Width="150px" HorizontalAlign="Left" />
                        <HeaderStyle Width="150px" HorizontalAlign="Left" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="性别" DataField="SexLabel">
                        <ItemStyle Width="80px" HorizontalAlign="Left" />
                        <HeaderStyle Width="80px" HorizontalAlign="Left" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="专业名称" DataField="MarjorName">
                        <ItemStyle Width="200px" HorizontalAlign="Left" />
                        <HeaderStyle Width="200px" HorizontalAlign="Left" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText="班级" DataField="Class">
                        <ItemStyle Width="120px" HorizontalAlign="Left" />
                        <HeaderStyle Width="120px" HorizontalAlign="Left" />
                    </telerik:GridBoundColumn>
                    <telerik:GridTemplateColumn>
                        <ItemTemplate>
                            <asp:LinkButton CommandName="delete" Text="删除" OnClientClick='<%# "if(!BeforeDeletd("+DataBinder.Eval(Container,"DataItem.StudentNum")+",\""+DataBinder.Eval(Container,"DataItem.StudentNum")+"\"))return false;"%>' ID="btnDelete" runat="server"></asp:LinkButton>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                </Columns>
                <NoRecordsTemplate>
                    <div>
                        没有记录!
                    </div>
                </NoRecordsTemplate>
            </MasterTableView>
            <ClientSettings AllowAutoScrollOnDragDrop="False">
                <Scrolling AllowScroll="false" UseStaticHeaders="false" SaveScrollPosition="false" />
            </ClientSettings>
        </telerik:RadGrid>
    </div>
    <div class="student-list-bar">
        <input type="button" value="添加学生" onclick="OpenStudentList()" />
    </div>
</asp:Content>
