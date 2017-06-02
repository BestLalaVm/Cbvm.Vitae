<%@ Page Title="" Language="C#" MasterPageFile="~/Manage/SubMaster.Master" AutoEventWireup="true" CodeBehind="AlreadyReferralStudentList.aspx.cs"
    Inherits="Cbvm.Vitae.Manage.Teacher.AlreadyReferralStudentList" Theme="TeacherManage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .body-content .main .detail {
            margin-top: 10px;
        }

            .body-content .main .detail table th {
                font-weight: bold;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="detail">
        <table>
            <tr>
                <th>企业名称:</th>
                <td>
                    <asp:Literal runat="server" ID="ltlEnterpriseName"></asp:Literal>
                </td>
                <th>岗位名称:</th>
                <td>
                    <asp:Literal runat="server" ID="ltlJobName"></asp:Literal>
                </td>
            </tr>
        </table>
    </div>
    <div class="list">
        <telerik:radgrid id="grdStudent" runat="server" onneeddatasource="radGrid_NeedDataSource"
            autogeneratecolumns="false" pagesize="20"
            onpageindexchanged="radGrid_PageIndexChanged">
        <MasterTableView DataKeyNames="StudentNum" PageSize="20" >
            <Columns>
                <telerik:GridBoundColumn DataField="Index" HeaderText="序号">
                    <HeaderStyle HorizontalAlign="Left" Width="50px" />
                    <ItemStyle HorizontalAlign="Left" Width="50px" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="CreayeTime" HeaderText="推荐时间" DataFormatString="{0:yyyy-MM-dd hh:mm:ss}">
                    <HeaderStyle Width="120px" HorizontalAlign="Left" />
                    <ItemStyle Width="120px" HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="StudentNum" HeaderText="学号">
                    <HeaderStyle Width="150px" HorizontalAlign="Left" />
                    <ItemStyle Width="150px" HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="NameZh" HeaderText="学生姓名">
                    <HeaderStyle Width="120px" HorizontalAlign="Left" />
                    <ItemStyle Width="120px" HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="SexLabel" HeaderText="性别">
                    <HeaderStyle Width="60px" HorizontalAlign="Left" />
                    <ItemStyle Width="60px" HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Period" HeaderText="届">
                    <HeaderStyle Width="100px" HorizontalAlign="Left" />
                    <ItemStyle Width="100px" HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="MarjorName" HeaderText="专业">
                    <HeaderStyle Width="180px" HorizontalAlign="Left" />
                    <ItemStyle Width="180px" HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Class" HeaderText="班级">
                    <HeaderStyle Width="100px" HorizontalAlign="Left" />
                    <ItemStyle Width="100px" HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
            </Columns>
            <NoRecordsTemplate>
                <div>
                    没有记录!
                </div>
            </NoRecordsTemplate>
        </MasterTableView>
    </telerik:radgrid>
    </div>
</asp:Content>
